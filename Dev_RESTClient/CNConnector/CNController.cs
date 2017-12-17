using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;

namespace CNConnector
{
    public enum CheckBits : ulong
    {
        STATUS_BIT_CODE = 0x00000001,
        STATUS_BIT_RSAKEY = 0x00000002,
        STATUS_BIT_LOGO = 0x00000004,
        STATUS_BIT_UPGRADE_MODE = 0x00000008,
        STATUS_BIT_FONT = 0x00000010,
        STATUS_BIT_ENCRP = 0x00000020,
        STATUS_BIT_SDCARD = 0x00000040,
        STATUS_BIT_USB = 0x00000080,
        STATUS_BIT_SDPROTECT = 0x00000100,
        STATUS_BIT_DOG = 0x00000200,
        STATUS_BIT_PRINTING = 0x00000400,
        STATUS_BIT_PAUSED = 0x00000800,
        STATUS_BIT_CPUMATCHED = 0x00001000,
        STATUS_BIT_RIBBON_ALARM = 0x00002000,
        STATUS_BIT_MOVE_MODE = 0x00004000,
        STATUS_BIT_NAND = 0x00008000,
        STATUS_BIT_LIMIT_OK = 0x00010000,
    }

    public enum CncMsg : ulong
    {
        CNCMSG_ERROR = 0xFFFF,
        CNCMSG_PROFILE = 0x0001,
        CNCMSG_STATUS = 0x0002,
        CNCMSG_CONFIG = 0x0003,
        CNCMSG_OOB = 0x0004,
        CNCMSG_BYE = 0x0005,
        CNCMSG_LOG = 0x0006,
    };

    public enum VKey : uint
    {
        VKEY_ML_ZPUPKEY = 0x11,//	//数字"1"键
        VKEY_ML_ESCKEY = 0x12,//	//"取消"键
        VKEY_ML_XYOKEY = 0x13,//	//数字"5"或"XY HOME"键
        VKEY_ML_TOOLDOWNKEY = 0x14,//	//"落刀"键
        VKEY_ML_XINCKEY = 0x15,//	//数字"2"或"X+"键, old key: "y+"
        VKEY_ML_DECKEY = 0x16,//	//数字"9"或"-"键
        VKEY_ML_ZPDOWNKEY = 0x21,//	//数字"7"键
        VKEY_ML_FACTORYKEY = 0x22,//	//"机器原点键"键
        VKEY_ML_ZDOWNKEY = 0x23,//	//"z+"键
        VKEY_ML_ZUPKEY = 0x24,//	//"z-"键
        VKEY_ML_YDECKEY = 0x25,//	//数字"6"或"Y-"键, old key: "x+"
        VKEY_ML_INCKEY = 0x26,//	//数字"3"或"+"键=
        VKEY_ML_USERORGKEY = 0x31,//	//数字"0"键或用户原点
        VKEY_ML_TOOLPAUSEKEY = 0x32,//	//"+/-"键或"刀停/转"键
        VKEY_ML_ZOKEY = 0x33,//	//"z轴原点"键
        VKEY_ML_HEADKEY = 0x34,//	//"主轴选择"键
        //#define		ML_INDEPTHKEY		=0x34,//	//"雕刻深度"键
        VKEY_ML_XDECKEY = 0x35,//	//数字"8"或"X-"键, old key: "y-"
        VKEY_ML_MENUKEY = 0x36,//	//"菜单"键
        VKEY_ML_KILLUSER = 0x41,//	//小数点"."键或取消用户原点键
        VKEY_ML_ENTERKEY = 0x42,//	//"确认"键
        VKEY_ML_TOOLUPKEY = 0x43,//	//"抬刀"键
        VKEY_ML_ABVHEIGHT = 0x44,//	//"测刀高度"键
        VKEY_ML_YINCKEY = 0x45,//	//数字"4"或"Y+"键, old key: "x-"
        VKEY_ML_ONLINEKEY = 0x46,//   	//"联机/暂停"键
        //新增加的健值20070311
        VKEY_ML_FNKEY = 0x51,//	//Dustc up and down in LATC and RATC mode
        VKEY_ML_F1KEY = 0x52,//	//locate tool in RATC mode
        VKEY_ML_ATCKEY = 0x53,//	//ATC menu 
        VKEY_ML_SOFTHOMEKEY = 0x54,//	//save current home to eeprom
        VKEY_ML_SHIFTKEY = 0x55,//	//shift heads
        VKEY_ML_PAUSEKEY = 0x56,//	//pause function
        VKEY_ML_KBUP = 0x8000,//	//按键抬起
    }

    public class CncStatusChangedArgs : EventArgs
    {
        public CultureInfo _culture;
        public CncStatusChangedArgs(CultureInfo culture)
        {
            _culture = culture;
        }
    }

    public class CNController
    {
        #region 控制器状态位.
         enum IOState1 : ulong
        {
            /// <summary>
            /// bit0--限位X(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitX = 0x00000001,
            /// <summary>
            /// bit1--限位Y(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitY = 0x00000002,
            /// <summary>
            /// bit2--限位Z(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitZ = 0x00000004,
            /// <summary>
            /// bit3--限位U(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitU = 0x00000008,
            /// <summary>
            /// bit4--限位V(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitV = 0x00000010,
            /// <summary>
            /// bit5--限位W(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitW = 0x00000020,
            /// <summary>
            /// bit6--限位A(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitA = 0x00000040,
            /// <summary>
            /// bit7--限位B(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitB = 0x00000080,
            /// <summary>
            /// bit8--限位C(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitC = 0x00000100,
            /// <summary>
            /// bit9--限位T(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitT = 0x00000200,
            /// <summary>
            /// bit10--限位S(1--限位正常并已找到,0--未找到限位)
            /// </summary>
            bLimitS = 0x00000400,
            /// <summary>
            /// bit11--输出口1状态(1--使能状态,0--禁止状态)
            /// </summary>
            bRelay1 = 0x00000800,
            /// <summary>
            /// bit12--输出口2状态(1--使能状态,0--禁止状态)
            /// </summary>
            bRelay2 = 0x00001000,
            /// <summary>
            /// bit13--输出口3状态(1--使能状态,0--禁止状态)	
            /// </summary>
            bRelay3 = 0x00002000,
            /// <summary>
            /// bit14--输出口4状态(1--使能状态,0--禁止状态)
            /// </summary>
            bRelay4 = 0x00004000,
            /// <summary>
            /// bit15--输出口5状态(1--使能状态,0--禁止状态)
            /// </summary>
            bRelay5 = 0x00008000,
            /// <summary>
            /// bit16--输出口6状态(1--使能状态,0--禁止状态)
            /// </summary>
            bRelay6 = 0x00010000,
            /// <summary>
            /// bit17--输出口7状态(1--使能状态,0--禁止状态)
            /// </summary>
            bRelay7 = 0x00020000,
            /// <summary>
            /// bit18--输出口8状态(1--使能状态,0--禁止状态)
            /// </summary>
            bRelay8 = 0x00040000,
            /// <summary>
            /// bit19--U盘插入状态(1--插入,0--未插入	)
            /// </summary>
            bUDiskStus = 0x00080000,
            /// <summary>
            /// bit20--SD卡插入状态(1--插入,0--未插入	)
            /// </summary>
            bSDCardStus = 0x00100000,
            /// <summary>
            /// bit21--网络插入状态(1--存在,0--不存在)
            /// </summary>
            bNetLinkedStus = 0x00200000,
            /// <summary>
            /// bit22--字库是否存在(1--存在,0--不存在)
            /// </summary>
            bPadFontExist = 0x00400000,
            /// <summary>
            /// bit23--图标是否存在(1--存在,0--不存在)
            /// </summary>
            bPadLogoExist = 0x00800000,
            bInverterExist = 0x01000000,
            /// <summary>
            /// ATC是否存在(1--存在,0--不存在)
            /// </summary>
            bMLATCExist = 0x02000000,
            /// <summary>
            /// bit25--PLC1是否存在(1--存在,0--不存在)
            /// </summary>
            bAtcPLCExist = 0x04000000,
            /// <summary>
            /// bit25--PLC2是否存在(1--存在,0--不存在)
            /// </summary>
            bPipePLCExist = 0x08000000,
            bGangDrillPLCExist = 0x10000000,
            /// <summary>
            /// bit26--CAN键盘是否存在
            /// </summary>
            bKeypadExist = 0x20000000,
            bFireStus = 0x40000000,
            /// <summary>
            /// bit27-28--上料状态(0--idle空闲,1--ready上料完成,2--busy上料完成并在加工状态,3--fail上料失败)
            /// </summary>
            bPlankLoadStus = 0x80000000
        }

         enum IOState2 : ulong
        {
            /// <summary>
            /// bit0-bit7--系统工作状态(参系统状态定义enumCNCSysStateDef)
            /// </summary>
            bCNCSysState = 0x000000FF,
            /// <summary>
            /// bit8-11--文件输出状态(0)	
            /// </summary>
            bFileStus = 0x00000F00,
            /// <summary>
            /// 打标机上料平台是否有料.12bit:0无料,1有料;//Zhao,2015.08.13
            /// </summary>
            bPlankExistInOnPlat = 0x00001000,
            /// <summary>
            /// bit12-15--
            /// </summary>
            bReserved1 = 0x0000F000,
            /// <summary>
            /// bit16--网络通讯(0：错误；1：正常)	
            /// </summary>
            bNetPhy = 0x00010000,
            /// <summary>
            /// bit17--U盘(0：错误；1：正常)	
            /// </summary>
            bUFLASH = 0x00020000,
            /// <summary>
            /// bit18--SD卡(0：错误；1：正常)	
            /// </summary>
            bMMC = 0x00040000,
            /// <summary>
            /// bit19--RS485通讯(0：错误；1：正常)
            /// </summary>
            bRS485 = 0x00080000,
            /// <summary>
            /// bit20--加密芯片(0：错误；1：正常)
            /// </summary>
            bSAM = 0x00100000,
            /// <summary>
            /// 
            /// </summary>
            bNORFLASH = 0x00200000,
            /// <summary>
            /// bit21--NORFLASH芯片(0：错误；1：正常)	
            /// </summary>
            bDRAM = 0x00400000,
            /// <summary>
            /// bit23--SPIFLASH芯片(0：错误；1：正常)	
            /// </summary>
            bSPIFLASH = 0x00800000,
            /// <summary>
            /// bit24--时钟芯片(0：错误；1：正常)	
            /// </summary>
            bRTC = 0x01000000,
            bReserved = 0xFE000000,
        }
        #endregion

        #region Falsh存储体类型.
        /// <summary>
        /// alsh存储体类型.
        /// </summary>
        public enum FalshType
        {
            /// <summary>
            /// SD卡.
            /// </summary>
            SD,
            /// <summary>
            /// 移动U盘.
            /// </summary>
            USB,
            /// <summary>
            /// 加工作业数据缓冲区.
            /// </summary>
            B,
        }
        #endregion

        #region 获取Debug调试错误文件名及错误代码行号.
        /// <summary>
        /// 获取代码文件名及当前执行代码行数.
        /// </summary>
        /// <returns>返回当前代码文件名及执行代码行数.</returns>
        //[Conditional("DEBUG")]
        public static string GetCodeLineAndFileName()
        {
            StackTrace insStackTrace = new StackTrace(true);
            StackFrame insStackFrame = insStackTrace.GetFrame(1);
            return String.Format("ErrorFile: {0}, ExecutionMethod: {1}, CodeErrorLine: {2};", insStackFrame.GetFileName(), insStackFrame.GetMethod(), insStackFrame.GetFileLineNumber());
        }
        #endregion

        #region 调用CNCAPI.
        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_Init", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_CNC_Init(int hWnd, Int64 param, [MarshalAs(UnmanagedType.LPStr)] string pWorkingPath);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_Exit", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_CNC_Exit();

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_Discover", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_CNC_Discover(StringBuilder sProfile, int nMaxLength);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_AddLogListener", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_CNC_AddLogListener(IntPtr hWnd);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_EnableLog", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_CNC_EnableLog(Int64 idCNC, Int64 nEnableLog);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_ReadLog", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_CNC_ReadLog(StringBuilder sLog, int nMaxLength);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_Connect", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_CNC_Connect(Int64 idCNC, IntPtr hWnd);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_IsConnected", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_CNC_IsConnected(Int64 idCNC);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_IsReady2Work", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_CNC_IsReady2Work(Int64 idCNC);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_GetProfile", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_CNC_GetProfile(Int64 idCNC, StringBuilder sInbfo, int nMaxLength);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_GetMachineProfile", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_CNC_GetMachineProfile(Int64 idCNC, StringBuilder sInbfo, int nMaxLength);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_GetJobSummary", CallingConvention = CallingConvention.Cdecl)] //2016-11-20,获取作业摘要信息
        private static extern Int32 CADM_CNC_GetJobSummary(Int64 idCNC, [MarshalAs(UnmanagedType.LPStr)] string pJobName, StringBuilder sJobSummary, int nMaxLength);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_GetStatus", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_CNC_GetStatus(Int64 idCNC, StringBuilder sInbfo, int nMaxLength);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_SendVKey", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_CNC_SendVKey(Int64 idCNC, UInt32 dwVKey);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_Disconnect", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_CNC_Disconnect(Int64 idCNC);

        //CADM_JOB_SendJob
        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_SendJob", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_JOB_SendJob(Int64 idCNC, [MarshalAs(UnmanagedType.LPStr)] string pJobName, [MarshalAs(UnmanagedType.LPStr)] string pDisk);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_SendFile", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_JOB_SendFile(Int64 idCNC, [MarshalAs(UnmanagedType.LPStr)] string pFilePath, Int64 type);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_GetJobProgress", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_JOB_GetJobProgress(Int64 idCNC); //2015-01-24, HE

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_GetSendProgress", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CADM_JOB_GetSendProgress(Int64 idCNC);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_IsSendingDone", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_JOB_IsSendingDone(Int64 idCNC);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_CancelSend", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_JOB_CancelSend(Int64 idCNC);
        /// <summary>
        /// 第一组IO状态查询函数.
        /// </summary>
        /// <param name="idCNC"></param>
        /// <returns></returns>
        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_GetIOStatus", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 CADM_CNC_GetIOStatus(Int64 idCNC);
        /// <summary>
        /// 第二组IO状态查询函数.
        /// </summary>
        /// <param name="idCNC"></param>
        /// <returns></returns>

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_CNC_GetSysStatus", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 CADM_CNC_GetSysStatus(Int64 idCNC); //dwSysStatus2

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_SendCycle", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_JOB_SendCycle(Int64 idCNC, Int64 idCycle, Int64 speedStart, Int64 speedTravel, [MarshalAs(UnmanagedType.LPStr)] string sCycleCodes, int nMaxLength);

        [DllImport("CNCDriver.dll", EntryPoint = "CADM_JOB_GetCycleStatus", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_JOB_GetCycleStatus(Int64 idCNC, Int64 idCycle);

        [DllImport("CNCImport.dll", EntryPoint = "CADM_API_Init", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_API_Init(int hWnd, Int64 param, [MarshalAs(UnmanagedType.LPStr)] string pWorkingPath);

        [DllImport("CNCImport.dll", EntryPoint = "CADM_API_Exit", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_API_Exit();

        [DllImport("CNCImport.dll", EntryPoint = "CADM_BatchImport", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_BatchImport(IntPtr hWnd, [MarshalAs(UnmanagedType.LPStr)] string pModel, [MarshalAs(UnmanagedType.LPStr)] string pBatchList);

        [DllImport("CNCImport.dll", EntryPoint = "CADM_BinPacking", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_BinPacking(IntPtr hWnd, [MarshalAs(UnmanagedType.LPStr)] string pModel, StringBuilder sPackingResultFile, int nMaxLength);

        [DllImport("CNCImport.dll", EntryPoint = "CADM_BuildJobTask", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_BuildJobTask(IntPtr hWnd, [MarshalAs(UnmanagedType.LPStr)] string pModel, [MarshalAs(UnmanagedType.LPStr)] string pBuildParam, Int64 mode); //准备创建作业

        [DllImport("CNCImport.dll", EntryPoint = "CADM_GetTaskProgress", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_GetTaskProgress(int handler); //创建作业进度，phase | progress

        [DllImport("CNCImport.dll", EntryPoint = "CADM_GetTaskResult", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CADM_GetTaskResult(int handler, StringBuilder result, int nMaxLen);

        [DllImport("CNCImport.dll", EntryPoint = "CADM_CancelTask", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CADM_CancelTask(int handler);

        [DllImport("CNCImport.dll", EntryPoint = "CADM_ConvertXlsCFG", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CADM_ConvertXlsCFG(String xlsFile, String cfgFile);

        #endregion

        #region 引用系统API.
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();   //WINAPI 获取当前活动窗体的句柄         

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);   //WINAPI 设置当前活动窗体的句柄        
        #endregion

        #region 公共变量.
        private Boolean _bSending;
        private Int64 _cncID = -1;
        private long _nConnectState = -1;

        private string _lastProfile = "";
        private Dictionary<string, string> _proflePairs = new Dictionary<string, string>();

        private string _lastStatus = "";
        private Dictionary<string, string> _statusPairs = new Dictionary<string, string>();

        private int _curTaskID = -1;
        private long _cycleId = 0;
        public long CycleId
        {
            get
            {
                return _cycleId;
            }
        }
        /// <summary>
        /// CycleId.
        /// </summary>
        public void AddCycleId()
        {
            ++_cycleId;
        }
        #endregion

        //下面是全局函数，无需创建，可以直接调用
        static public bool CNC_Init(string workingPath = "")//全局函数，程序进入时，只调用一次
        {
            String sCodeBase = workingPath;// Assembly.GetExecutingAssembly().CodeBase;
            if (String.IsNullOrEmpty(sCodeBase))
                sCodeBase = AppDomain.CurrentDomain.BaseDirectory;

            CADM_API_Init(0, 0, sCodeBase);
            return CADM_CNC_Init(0, 0, sCodeBase);
        }

        static public void CNC_Exit()//全局函数， 程序退出时，只调用一次
        {
            CADM_CNC_Exit();
            CADM_API_Exit();
        }

        static public string CNC_Discover()//全局函数， 查询控制器后返回设备字串.
        {
            StringBuilder sProfileList = new StringBuilder(8192);
            CADM_CNC_Discover(sProfileList, 8192);
            return sProfileList.ToString();
        }

        static public void CNC_AddLogWnd(IntPtr hWnd)//全局函数
        {
            CADM_CNC_AddLogListener(hWnd);
        }

        static public string CNC_ReadLog()//全局函数
        {
            StringBuilder sLog = new StringBuilder(8192);
            if (CADM_CNC_ReadLog(sLog, 8192) > 0)
                return sLog.ToString();
            return "";
        }

        public static long UpdateCncList(ComboBox listCombo)
        {
            List<string> _foundDevices = CNController.GetAllDevices(); //搜索到的设备串
            Dictionary<Int64, string> foundDeviceID = new Dictionary<long, string>(); //搜索到的设备ID
            for (int i = 0; i < _foundDevices.Count; i++)
            {
                Int64 id = CNController.GetDeviceID(_foundDevices[i]);
                if (!foundDeviceID.ContainsKey(id))
                    foundDeviceID.Add(id, _foundDevices[i]);
            }

            Int64 nCurDevID = -1;
            String sCurDevice = "";
            if ((listCombo.Items.Count > 0) && (listCombo.SelectedIndex != -1))
            { //更新之前，获取当前选中设备
                sCurDevice = listCombo.Text;
                nCurDevID = CNController.GetDeviceID(sCurDevice);
            }

            Boolean bNeedUpdate = false;
            //删除掉线的设备.
            List<Int64> listDeviceID = new List<long>(); //搜索到的设备ID
            for (int i = listCombo.Items.Count - 1; i >= 0; i--)
            {
                Int64 did = CNController.GetDeviceID(listCombo.Items[i].ToString());
                if (!foundDeviceID.ContainsKey(did))
                { //已经掉线的设备
                    listCombo.Items.RemoveAt(i);
                    bNeedUpdate = true;
                }
                else
                {
                    if (!foundDeviceID[did].Equals(listCombo.Items[i].ToString()))
                        bNeedUpdate = true;
                    listDeviceID.Add(did);
                }
            }

            foreach (string dev in _foundDevices)
            {
                Int64 did = CNController.GetDeviceID(dev);
                if (!listDeviceID.Contains(did))
                { //新增
                    listCombo.Items.Add(dev);
                    bNeedUpdate = true;
                }
            }

            if (bNeedUpdate)
            {
                listCombo.Items.Clear();
                if (_foundDevices.Count > 0)
                {
                    foreach (string dev in _foundDevices)
                        listCombo.Items.Add(dev);

                    int nSel = 0;
                    for (int j = 0; j < listCombo.Items.Count; j++)
                    {
                        if (nCurDevID == CNController.GetDeviceID(listCombo.Items[j].ToString()))
                        {
                            nSel = j;
                            break;
                        }
                    }
                    listCombo.SelectedIndex = nSel;
                }
            }
            return nCurDevID;
        }

        public static string CADM_BinPacking(string sModel)
        {
            StringBuilder sPackingResultFile = new StringBuilder(8192);

            if (CADM_BinPacking((IntPtr)0, sModel, sPackingResultFile, 8192))
            {
                return sPackingResultFile.ToString();
            }
            return "";
        }

        static public bool CADM_BatchImport(string sModel, string sBatchList)//全局函数，传入要生成的作业参数字串
        {
            return CADM_BatchImport((IntPtr)0, sModel, sBatchList);
        }

        static public string IPTransform(string IPstr)//全局函数
        {
            char[] group = IPstr.ToCharArray();
            return Convert.ToInt32(group[0].ToString() + group[1].ToString(), 16) + "." + Convert.ToInt32(group[2].ToString() + group[3].ToString(), 16) + "." + Convert.ToInt32(group[4].ToString() + group[5].ToString(), 16) + "." + Convert.ToInt32(group[6].ToString() + group[7].ToString(), 16);
        }

        static public Int64 GetDeviceID(string deviceString)//全局函数，从设备显示串提取DeviceID
        {
            string sID = "";
            int idx = deviceString.IndexOf(":");
            if (idx >= 0)
            {
                sID = deviceString.Substring(idx + 1);
                idx = sID.IndexOf(",");
                if (idx >= 0)
                    sID = sID.Substring(0, idx);
                else
                    sID = "";
            }
            if (String.IsNullOrEmpty(sID))
                return -1;
            return Convert.ToInt64(sID);
        }

        /// <summary>
        /// 全局函数，获得设备清单
        /// </summary>
        /// <returns></returns>
        static public List<string> GetAllDevices()
        {
            List<string> devices = new List<string>();
            List<string> CNCparameter = null;

            try
            {
                string sLictCNC = CNC_Discover(); //调用CADM_CNC_Discover探测接口,探测是否有机器.并且返回机器字串.
                if (sLictCNC.Length > 0)
                {
                    string[] cncList = sLictCNC.ToString().Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < cncList.Length; i++)
                    {
                        string[] CNC_Online = cncList[i].Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        try
                        {
                            CNCparameter = new List<string>();
                            for (int n = 0; n < CNC_Online.Length; n++)
                            {
                                if (CNC_Online[n].Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries)[0] != "Model")
                                {
                                    CNCparameter.Add(CNC_Online[n].Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                }
                            }
                            string deviceStr = String.Format("设备ID:{0},设备名称:{1},设备IP:{2},当前连接:{3},设备状态:{4}",
                                CNCparameter[0],
                                CNCparameter[2],
                                CNCparameter[1],
                                CNCparameter[3],
                                CNCparameter[4].ToString() == "1" ? "联机" : "脱机");
                            //临时获取Cycle状态.
                            //temp_ReturnCycleID = int.Parse(CNCparameter[5]);
                            //temp_ReturnCycleState = int.Parse(CNCparameter[6]);
                            devices.Add(deviceStr);
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine(CNController.GetCodeLineAndFileName() + err.Message);

                            Debug.WriteLine("设备:" + CNCparameter[0] + " Model暂未获取...");
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(CNController.GetCodeLineAndFileName() + err.Message);

            }

            return devices;
        }

        static public string GetDeviceString(UInt32 xid) //获取连接串
        {
            List<string> allDevices = GetAllDevices();
            foreach (string dev in allDevices)
            {
                if (xid == GetDeviceID(dev))//全局函数，从设备显示串提取DeviceID
                    return dev;
            }
            return "";
        }

        //以下是针对单个设备连接的处理
        public CNController()
        {
            _bSending = false;
            _cncID = -1;
        }

        /// 负值：表示返回错误码；正值：表示设备CNC handle,供后续调用
        public Int32 Connect(Int64 idCNC, IntPtr hWnd)
        {
            int connectState = CADM_CNC_Connect(idCNC, hWnd);
            if (connectState > 0)
            {
                _bSending = false;
                _cncID = idCNC;
                _nConnectState = 1;
                return connectState;
            }

            _bSending = false;
            _cncID = -1;
            _nConnectState = -1;
            return connectState;
        }

        /// 断开设备方法.
        public int Disconnect()
        {
            if (_cncID <= 0)
                return 0;

            int disStatus = CADM_CNC_Disconnect(_cncID);
            _cncID = -1;
            _nConnectState = -1;
            return disStatus;
        }
        /// <summary>
        /// 系统是否连接检测.
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if (_cncID <= 0)
                return false;

            bool bConnected = CADM_CNC_IsConnected(_cncID);
            return bConnected;
        }

        /// <summary>
        ///  查询控制器是否处于可下发作业状态.
        /// </summary>
        /// <param name="getIsOnlineState">为ture则返回带联机状态值</param>
        /// <returns>状态值</returns>
        public bool DeviceIsStateOk(bool getIsOnlineState)
        {
            if (getIsOnlineState)
            {
                return ((_cncID > 0) && IsConnected() && IsReady2Work() && IsOnline());
            }
            else
            {
                return ((_cncID > 0) && IsConnected() && IsReady2Work());
            }

        }

        /// <summary>
        /// 控制器是否准备成功.
        /// </summary>
        /// <returns></returns>
        public bool IsReady2Work()// 查询控制器是否处于联机状态
        {
            if (_cncID <= 0)
                return false;
            return CADM_CNC_IsReady2Work(_cncID);
        }

        private UInt32 Hex2UInt32(string hex)
        {
            string value = hex.ToUpper();
            string numbers = "0123456789ABCDEF";
            UInt32 result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                result <<= 4;
                int index = numbers.IndexOf(value[i]);
                if (index < 0)
                    break;
                result += (UInt32)index;
            }
            return result;
        }

        public bool IsUpgradeMode()// 查询控制器是否升级模式
        {
            if ((_cncID <= 0) || !IsConnected())
                return false;
            try
            {
                String strDeviceStatus = GetStatus().ToString();
                if (_statusPairs.ContainsKey("CHKBIT"))
                {
                    UInt32 checkbits = Hex2UInt32(_statusPairs["CHKBIT"]);
                    return ((checkbits & (UInt32)CheckBits.STATUS_BIT_UPGRADE_MODE) > 0);
                }
            }
            catch
            { }

            return false;
        }

        public bool IsLogEnabled()// 查询控制器是否输出日志
        {
            if ((_cncID <= 0) || !IsConnected())
                return false;
            try
            {
                String strDeviceStatus = GetStatus().ToString();
                if (_statusPairs.ContainsKey("LOG"))
                    return (Int32.Parse(_statusPairs["LOG"]) > 0);
            }
            catch
            { }

            return false;
        }

        public bool IsOnline() /// CNC是否处于连接状态
        {
            if (_cncID <= 0)
                return false;
            try
            {
                String strDeviceStatus = GetStatus().ToString();
                if (_statusPairs.ContainsKey("ONLINE"))
                    return (Int32.Parse(_statusPairs["ONLINE"]) > 0);
            }
            catch
            { }
            return false;
        }

        public Int32 GetSendingProgress()
        {
            if (_cncID <= 0)
                return 0;
            return CADM_JOB_GetSendProgress(_cncID);
        }

        public Int32 GetJobProgress() //2015-01-24, HE
        {
            if (_cncID <= 0)
                return 0;
            return CADM_JOB_GetJobProgress(_cncID);
        }

        public void CancelSend()
        {
            if (_cncID <= 0)
                return;
            CADM_JOB_CancelSend(_cncID);
        }

        public bool IsSendingDone()
        {
            if (_cncID <= 0)
                return true;
            return CADM_JOB_IsSendingDone(_cncID);
        }
        #region 包装查询控制器状态函数代码块.---2015.10.29 Zhao
        /// <summary>
        /// 获取板材到位信号.
        /// </summary>
        /// <param name="type">返回值类型.</param>
        /// <returns>true到位,False未到位.</returns>
        public T GetDeviceDoorGoToSignal<T>()
        {
            uint mask = (uint)IOState1.bPlankLoadStus;
            uint st1 = GetStatusCallMthod(CADM_CNC_GetIOStatus, mask);
            if (typeof(T) == typeof(bool))
            {
                return (T)(object)(st1 == 1);
            }
            return (T)(object)st1;
        }

        #endregion
        #region 获取系统状态.
        private delegate uint GetStatusNumber(long cncId);
        /// <summary>
        /// 传入相应状态查询函数和查询状态代码.返回状态值.
        /// </summary>
        /// <param name="gsn">状态查询函数委托.</param>
        /// <param name="mask">状态代码.</param>
        /// <returns>状态对应值.</returns>
        private uint GetStatusCallMthod(GetStatusNumber gsn,uint mask)//zhao 2015/11/20
        {
            UInt32 allIOStatus = gsn(_cncID);
            allIOStatus &= mask;

            while ((mask & 0x01) == 0)
            {
                mask >>= 1;
                allIOStatus >>= 1;
            }
            return allIOStatus;
        }
        #endregion

        public static Dictionary<string, string> GetKeyValues(string sPairString, char[] splitPairs, char[] splitValue)
        { //将格式化的字串转成值对儿
            Dictionary<string, string> valuePairs = new Dictionary<string, string>();
            string[] pairs = sPairString.Split(splitPairs, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pair in pairs)
            {
                string[] valuePair = pair.Split(splitValue, StringSplitOptions.RemoveEmptyEntries);
                if (valuePair.Length == 2)
                {
                    valuePairs[valuePair[0].ToUpper()] = valuePair[1];
                }
            }
            return valuePairs;
        }

        public StringBuilder GetProfile()
        {
            StringBuilder sInbfo = new StringBuilder(8192);
            if (_cncID <= 0)
                return sInbfo;

            CADM_CNC_GetProfile(_cncID, sInbfo, 8192);
            string newProfile = sInbfo.ToString();
            if (!_lastProfile.Equals(newProfile, StringComparison.InvariantCultureIgnoreCase))
            {
                _lastProfile = newProfile;
                _proflePairs = GetKeyValues(_lastProfile, new char[] { ';' }, new char[] { '=' });
            }
            return sInbfo;
        }

        public StringBuilder GetMachineProfile()
        {
            StringBuilder sInbfo = new StringBuilder(8192);
            if (_cncID <= 0)
                return sInbfo;

            if (CADM_CNC_GetMachineProfile(_cncID, sInbfo, 8192) > 0)
                return sInbfo;
            return sInbfo;
        }

        public StringBuilder GetStatus() //Status1， Status2， CycleID， CycleStatus
        {
            StringBuilder sStatus = new StringBuilder(8192);
            if (_cncID <= 0)
                return sStatus;

            CADM_CNC_GetStatus(_cncID, sStatus, 8192);
            string newStatus = sStatus.ToString();
            if (!_lastStatus.Equals(newStatus, StringComparison.InvariantCultureIgnoreCase))
            {
                _lastStatus = newStatus;
                _statusPairs = GetKeyValues(_lastStatus, new char[] { ';' }, new char[] { '=' });
            }
            return sStatus;
        }

        /// <summary>
        /// 发送G代码指令方法.
        /// </summary>
        /// <param name="cncID">设备ID.</param>
        /// <param name="idCycle">指令流水ID.</param>
        /// <param name="speedStart">启动速度.</param>
        /// <param name="speedTravel">空走速度.</param>
        /// <param name="sCycleCodes">指令内容.</param>
        /// <returns></returns>
        public int SendCycle(Int64 idCycle, Int64 speedStart, Int64 speedTravel, string sCycleCodes)
        {
            if ((_cncID <= 0) || !IsConnected() || !IsReady2Work())//|| !IsOnline()
                return 0;

            return CADM_JOB_SendCycle(_cncID, idCycle, speedStart, speedTravel, sCycleCodes, sCycleCodes.Length);
        }

        /// <summary>
        /// 查询G代码执行状态.
        /// </summary>
        /// <param name="cncID">设备ID.</param>
        /// <param name="idCycle">指令流水ID.</param>
        /// <returns>状态值:负值：失败；正值：2成功结束,1设备忙..</returns>
        public int GetCycleStatus(Int64 idCycle)
        {
            if ((_cncID <= 0) || !IsConnected() || !IsReady2Work())//|| !IsOnline()
                return 0;
            return CADM_JOB_GetCycleStatus(_cncID, idCycle);
        }
        /// <summary>
        /// 启动作业下发方法.
        /// </summary>
        /// <param name="sJobName">作业名称.</param>
        /// <param name="ft">作业盘符.</param>
        /// <returns> 负值：表示返回错误码；正值：表示设备CNC handle,供后续调用,发送作业.</returns>
        public bool SendJob(string sJobName, FalshType ft)
        {
            if ((_cncID <= 0) || !IsConnected() || !IsReady2Work() || !IsOnline())
                return false;
            return CADM_JOB_SendJob(_cncID, sJobName, ft.ToString());
        }

        public String GetJobSummary(string sJobName)
        {
            if ((_cncID <= 0) || !IsConnected())
                return null;
            StringBuilder sumarry = new StringBuilder(8192);
            if (CADM_CNC_GetJobSummary(_cncID, sJobName, sumarry, 8190) > 0)
                return sumarry.ToString();
            return null;
        }

        public bool SendFile(string sFilePath, Int64 type) //通用发送/获取文件（作业、固件、xls配置、字库、logo）到控制器
        {
            if ((_cncID <= 0) || !IsConnected())
                return false;
            return CADM_JOB_SendFile(_cncID, sFilePath, type);
        }

        /// <summary>
        /// 返回连接设备后设备ID.
        /// </summary>
        /// <returns>连接的设备ID.</returns>
        public long GetConnectedDeviceID()
        {
            return _cncID > 0 ? _cncID : 0;
        }

        public String GetModelName() //获取控制器型号
        {
            if ((_cncID <= 0) || !IsConnected())
                return "";
            String strDeviceInfo = GetProfile().ToString();
            if (_proflePairs.ContainsKey("MODEL"))
                return _proflePairs["MODEL"];
            return "";
        }

        public double GetThickness() //获取板材测量厚度
        {
            if ((_cncID <= 0) || !IsConnected())
                return -1;
            try
            {
                String strDeviceStatus = GetStatus().ToString();
                if (_statusPairs.ContainsKey("SURF"))
                    return Double.Parse(_statusPairs["SURF"]);
            }
            catch
            { }

            return -1;
        }

        public void SendVKey(UInt32 vKey) //发送虚拟键值
        {
            if ((_cncID <= 0) || !IsConnected())
                return;
            CADM_CNC_SendVKey(_cncID, vKey);
        }

        public void EnableLog(Boolean bEnable) //允许设备输出日志
        {
            if ((_cncID <= 0) || !IsConnected())
                return;

            if (bEnable)
                CADM_CNC_EnableLog(_cncID, 1);
            else
                CADM_CNC_EnableLog(_cncID, 0);
        }

        public int StartJobTask(IntPtr hWnd, string sModel, string sParam, Int64 mode) //根据参数列表和设备型号导入新作业，返回 task id
        {
            return CADM_BuildJobTask(hWnd, sModel, sParam, mode);
        }

        public int GetTaskProgress(int taskID) //导入新作业进度
        {
            return CADM_GetTaskProgress(taskID);
        }

        public string GetTaskResult(int taskID) //导入新作业结果说明
        {
            StringBuilder result = new StringBuilder(8192);
            CADM_GetTaskResult(taskID, result, 8192);
            return result.ToString();
        }

        public bool IsTaskSuccess(int taskID) //导入新作业是否成功？
        {
            StringBuilder result = new StringBuilder(8192);
            return CADM_GetTaskResult(taskID, result, 8192);
        }

        public void TerminateTask(int taskID) //结束导入作业
        {
            try
            {
                CADM_CancelTask(taskID);
                Debug.WriteLine(String.Format("Job#{0} ended", taskID.ToString()));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public int ConvertXls2Cfg(String xlsFile, String cfgFile) //将xls格式的控制器参数，转换为控制器可识别的格式
        {
            return CADM_ConvertXlsCFG(xlsFile, cfgFile);
        }
    }
}
