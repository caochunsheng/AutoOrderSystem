//Created by HEJS, 2016-05-21

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace Threads
{
    public class ThreadScheduler<T>
    {
        public const int MODE_EXIT_ON_FINISHED = 0;
        public const int MODE_CONSUME_QUEUE = 1;
        public const int MODE_REPEAT = 2;

        public const int LOG_INFO = 1;
        public const int LOG_WARNING = 2;
        public const int LOG_FATAL = 3;

        private int _Mode; //循环调度模式
        private int _Parallelism; //最大并发数

        private Thread _threadMaster; //调度线程

        private List<ConsumingWorker<T>> _threadWorkers;  //工作线程池
        private List<ManualResetEvent> _eventWorkers; //工作线程任务结束通知事件

        private ManualResetEvent _evtStop; //停止调度线程信号
        private int _Finished; //调度线程已终止

        private ManualResetEvent _evtTaskReady; //调度任务队列增加信号
        private ConcurrentQueue<T> _taskWaitQueue; //任务队列

        public delegate void DownloadJob(T req); //任务处理函数
        public DownloadJob _Processor; //任务处理函数

        private ConcurrentQueue<string> _logList; //调度线程日志

        private void DebugLog(int level, string format, params object[] paramList)
        {
            string log = String.Format(format, paramList);
            _logList.Enqueue(log);

            while (_logList.Count > 200)
            {
                string rmlog = "";
                if (_logList.TryDequeue(out rmlog))
                {
                }
            }
        }

        public List<String> GetLog() //获取调度运行日志
        {
            List<String> logList = new List<string>();
            while (_logList.Count > 0)
            {
                string log = "";
                if (_logList.TryDequeue(out log))
                {
                    logList.Add(log);
                }
            }
            return logList;
        }

        public ThreadScheduler()
        {
            _logList = new ConcurrentQueue<string>(); //调度线程日志

            _Mode = MODE_EXIT_ON_FINISHED;
            _Parallelism = 2;

            _Processor = null;

            _evtStop = new ManualResetEvent(false);
            _Finished = 1;

            _evtTaskReady = new ManualResetEvent(false);
            _taskWaitQueue = new ConcurrentQueue<T>();

            _threadWorkers = new List<ConsumingWorker<T>>();
            _eventWorkers = new List<ManualResetEvent>();
        }

        public void SetMode(int mode)
        {
            _Mode = mode;
        }

        public void EnqueueTask(T task)
        {
            _taskWaitQueue.Enqueue(task);
            _evtTaskReady.Set();
        }

        public Boolean Start(int concurrent)
        {
            _Parallelism = concurrent;

            _Finished = 0;
            _evtStop.Reset();

            _threadMaster = new Thread(ThreadScheduleProc);
            _threadMaster.IsBackground = true;
            _threadMaster.Name = "Thread Scheduler";

            _threadMaster.Start();

            DebugLog(LOG_INFO, "Scheduler started");
            return true;
        }

        public void Stop()
        {
            _Finished = 0;
            _evtStop.Set();

            DateTime tStart = DateTime.Now;
            while (_Finished <= 0)
            {
                try
                {
                    TimeSpan tSpan = DateTime.Now - tStart;
                    if (tSpan.TotalMilliseconds >= 3000)
                    {
                        _threadMaster.Abort();
                        break;
                    }
                }
                catch (Exception e)
                {
                    break;
                }
            }
            DebugLog(LOG_INFO, "Scheduler stopped");
        }

        private void ThreadScheduleProc()
        {
            WaitHandle[] waitEvent = new WaitHandle[_Parallelism + 2];

            waitEvent[0] = _evtStop;
            waitEvent[1] = _evtTaskReady;

            for (int i = 0; i < _Parallelism; i ++)
            {
                ManualResetEvent event4Owner = new ManualResetEvent(false);
                _eventWorkers.Add(event4Owner);

                ConsumingWorker<T> newWorker = new ConsumingWorker<T>();
                newWorker._threadIndex = i;
                newWorker._Processor = _Processor;
                _threadWorkers.Add(newWorker);

                waitEvent[i + 2] = event4Owner;
                newWorker.Start(event4Owner);
            }

            while (true)
            {
                int idxEvent = WaitHandle.WaitAny(waitEvent, 500);
                if (idxEvent == 0)
                { //_evtStop
                    DebugLog(LOG_INFO, "Stopping workers ...");

                    for (int i = 0; i < _Parallelism; i++)
                        _threadWorkers[i].Stop();
                    break;
                }
                else if (idxEvent == WaitHandle.WaitTimeout)
                {
                    continue;
                }
                else if (idxEvent > 1)
                { //finished
                    int idxWorker = idxEvent - 2;
                    _eventWorkers[idxWorker].Reset();

                    //DebugLog(LOG_INFO, "Thread#{0}: end", idxWorker.ToString());

                    List<T> finished = _threadWorkers[idxWorker].GetFinished();
                    for (int i = 0; i < finished.Count; i++)
                    {
                        if (_Mode == MODE_REPEAT)
                        {
                            EnqueueTask(finished[i]);
                            //DebugLog(LOG_INFO, "Thread#{0}: repeating", idxWorker.ToString());
                        }
                    }
                }

                if (_taskWaitQueue.Count > 0)
                {
                    Boolean bAllBusy = true;
                    for (int i = 0; i < _Parallelism; i++)
                    {
                        if (_threadWorkers[i].IsIdle())
                        {
                            bAllBusy = false;

                            T nextTask;
                            if ((_taskWaitQueue.Count > 0) && _taskWaitQueue.TryDequeue(out nextTask))
                            {
                                _threadWorkers[i].EnqueueTask(nextTask);
                                //DebugLog(LOG_INFO, "Thread#{0}: re-scheduled", i.ToString());
                            }

                            if (_taskWaitQueue.Count <= 0)
                            {
                                _evtTaskReady.Reset();
                                //DebugLog(LOG_INFO, "Task queue empty");
                                break;
                            }
                        }
                    }

                    if (bAllBusy)
                    {
                        //DebugLog(LOG_INFO, "Thread pool busy");
                        _evtTaskReady.Reset();
                    }
                }
            }
            Interlocked.Exchange(ref _Finished, 1);
        }
    }

    public class ConsumingWorker<T>
    {
        public int _threadIndex; //线程id
        private Thread _threadWorker; //工作线程

        private ManualResetEvent _evtTaskReady; //队列有任务
        private ManualResetEvent _evtStop; //停止信号

        private ManualResetEvent _evtForOwner; //任务结束信号，给调度线程
        public ThreadScheduler<T>.DownloadJob _Processor; //任务函数

        private int _Finished; //工作线程已终止
        private int _Busy; //工作线程正在忙

        private ConcurrentQueue<T> _taskQueue; //任务队列
        private ConcurrentQueue<T> _taskFinished; //结束的任务

        public ConsumingWorker()
        {
            _threadIndex = -1;

            _Busy = 0;
            _Finished = 1;

            _evtForOwner = null;
            _Processor = null;

            _evtStop = new ManualResetEvent(false);
            _evtTaskReady = new ManualResetEvent(false);

            _taskQueue = new ConcurrentQueue<T>();
            _taskFinished = new ConcurrentQueue<T>();
        }

        public void EnqueueTask(T task)
        {
            _taskQueue.Enqueue(task);
            _evtTaskReady.Set();
        }

        public List<T> GetFinished()
        {
            List<T> finished = new List<T>();
            if (_taskFinished.Count > 0)
            {
                T nextTask;
                if (_taskFinished.TryDequeue(out nextTask))
                    finished.Add(nextTask);
            }
            return finished;
        }

        public Boolean IsIdle()
        {
            return !((_Busy > 0) || (_taskQueue.Count > 0));
        }

        public Boolean Start(ManualResetEvent owner)
        {
            _Busy = 0;
            _Finished = 0;
            _evtForOwner = owner;

            _threadWorker = new Thread(ThreadConsumingProc);
            _threadWorker.IsBackground = true;
            _threadWorker.Name = "Thread Worker";

            _threadWorker.Start();

            return true;
        }

        public void Stop()
        {
            _Finished = 0;
            _evtStop.Set();

            DateTime tStart = DateTime.Now;
            while (_Finished <= 0)
            {
                try
                {
                    TimeSpan tSpan = DateTime.Now - tStart;
                    if (tSpan.TotalMilliseconds >= 3000)
                    {
                        _threadWorker.Abort();
                        break;
                    }
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }

        private void ThreadConsumingProc()
        {
            WaitHandle[] waitEvent = new WaitHandle[2];
            waitEvent[0] = _evtStop;
            waitEvent[1] = _evtTaskReady;

            if (_evtForOwner != null)
                _evtForOwner.Set();

            while (true)
            {
                int idxEvent = WaitHandle.WaitAny(waitEvent, 500);
                if (idxEvent == 0)
                { //_evtStop
                    break;
                }

                if (_taskQueue.Count > 0)
                {
                    Interlocked.Exchange(ref _Busy, 1);

                    T nextTask;
                    if (_taskQueue.TryDequeue(out nextTask))
                    {
                        if (_Processor != null)
                            _Processor(nextTask);
                        _taskFinished.Enqueue(nextTask);
                    }

                    if (_taskQueue.Count == 0)
                    {
                        _evtTaskReady.Reset();

                        if (_evtForOwner != null)
                            _evtForOwner.Set();

                        Interlocked.Exchange(ref _Busy, 0);
                    }
                }
            }

            Interlocked.Exchange(ref _Finished, 1);
        }
    }
}
