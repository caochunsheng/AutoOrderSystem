using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Common
{
    public static class LogHelper
    {

        public static void WriteLog(string message,LogType logtype)
        {
            string strPath;                                    
            DateTime dt = DateTime.Now;
            try
            {
                strPath = Directory.GetCurrentDirectory() + "\\Log";    

                if (Directory.Exists(strPath) == false)   
                {
                    Directory.CreateDirectory(strPath);     
                }
                strPath = strPath + "\\" + dt.Year.ToString() + "-" + dt.Month.ToString() +"-"+dt.Day.ToString()+ ".txt";

                StreamWriter FileWriter = new StreamWriter(strPath, true);           //创建日志文件
                FileWriter.WriteLine("[" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "]  "+$"[{logtype.ToString()}]" + message);
                FileWriter.Close();                                                 //关闭StreamWriter对象
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }

        }
    }
    public enum LogType
    {
        Status,
        Error
    }
}
