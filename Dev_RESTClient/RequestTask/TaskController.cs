using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace CaxaAPI
{
    public class TaskController
    {
        const String dllFolder = "";

        [DllImport(dllFolder + "CNCImport.dll", EntryPoint = "CADM_API_Init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_API_Init(int hWnd, Int64 param, [MarshalAs(UnmanagedType.LPStr)] string pWorkingPath);

        [DllImport(dllFolder + "CNCImport.dll", EntryPoint = "CADM_API_Exit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_API_Exit();

        [DllImport(dllFolder + "CNCImport.dll", EntryPoint = "CADM_TaskSubmit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_TaskSubmit(IntPtr hWnd, [MarshalAs(UnmanagedType.LPStr)] string pBuildParam); //准备创建作业

        [DllImport(dllFolder + "CNCImport.dll", EntryPoint = "CADM_TaskProgress", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_TaskProgress(int handler, StringBuilder jsonMessage, int nMaxLen); //创建作业进度，phase | progress

        [DllImport(dllFolder + "CNCImport.dll", EntryPoint = "CADM_TaskFinished", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_TaskFinished(int handler); //作业是否结束

        [DllImport(dllFolder + "CNCImport.dll", EntryPoint = "CADM_TaskAbort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_TaskAbort(int handler);

        [DllImport(dllFolder + "CNCImport.dll", EntryPoint = "CADM_TaskResult", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_TaskResult(int handler, StringBuilder jsonResult, int nMaxLen);

        static public bool CadmInit(string workingPath = "")//全局函数，程序进入时，只调用一次
        {
            string sCodeBase = workingPath;// Assembly.GetExecutingAssembly().CodeBase;
            if (string.IsNullOrEmpty(sCodeBase))
                sCodeBase = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                CADM_API_Init(0, 0, sCodeBase);

                //if (WebServices._OutputLog != null)
                    //WebServices._OutputLog("TaskController @ CadmInit, loaded");
            }
            catch (Exception e)
            {
                //if (WebServices._OutputLog != null)
                    //WebServices._OutputLog("TaskController @ CadmInit, " + e.Message );
            }
            return true;
        }

        static public void CadmExit()//全局函数， 程序退出时，只调用一次
        {
            try
            {
                CADM_API_Exit();

                //if (WebServices._OutputLog != null)
                    //WebServices._OutputLog("TaskController @ CadmInit, unloaded");
            }
            catch (Exception e)
            {
                //if (WebServices._OutputLog != null)
                    //WebServices._OutputLog("TaskController @ CadmExit, " + e.Message);
            }
        }

        static public int SubmitTask(IntPtr hWnd, String sParam) //根据参数列表和设备型号导入新作业，返回 task id
        {
            int hTask = -1;
            try
            {
                hTask = CADM_TaskSubmit(hWnd, sParam);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return hTask;
        }

        static public int GetTaskProgress(int taskID, StringBuilder message) //导入新作业进度
        {
            StringBuilder result = new StringBuilder(8192);
            int progress = 0;
            try
            {
                progress = CADM_TaskProgress(taskID, result, 8192);
                message.Append(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return progress;
        }

        static public Boolean IsTaskFinished(int taskID) //导入新作业进度
        {
            StringBuilder result = new StringBuilder(8192);
            Boolean finished = false;
            try
            {
                finished = CADM_TaskFinished(taskID);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return finished;
        }

        static public void CancelTask(int taskID) //结束导入作业
        {
            try
            {
                CADM_TaskAbort(taskID);
                Debug.WriteLine(String.Format("Job#{0} ended", taskID.ToString()));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        static public string GetTaskResult(int taskID) //导入新作业结果说明
        {
            StringBuilder result = new StringBuilder(8192);
            try
            {
                if (CADM_TaskResult(taskID, result, 8192))
                {
                    if (File.Exists(result.ToString()))
                    {
                        return result.ToString();
                    }
                    else if (Directory.Exists(result.ToString()))
                    {
                        String zipFile = Util.Util.GetTempFileName(".zip");
                        if (RESTClient.GZip.ZipFolder(result.ToString(), zipFile))
                        {
                            Util.Util.RecursiveDelete(result.ToString());
                            return zipFile;
                        }
                    }
                }
                else
                {
                    result.Clear();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return String.Empty;
        }
    }
}
