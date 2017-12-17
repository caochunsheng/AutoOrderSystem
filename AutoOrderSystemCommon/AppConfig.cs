using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AutoOrderSystemCommon
{
    public class AppConfig
    {
        public static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        /// <summary>  
        /// 获取配置值  
        /// </summary>  
        /// <param name="key">配置标识</param>  
        /// <returns></returns>  
        public static string GetValue(string key)
        {
            string result = string.Empty;
            if (config.AppSettings.Settings[key] != null)
                result = config.AppSettings.Settings[key].Value;
            return result;
        }
        /// <summary>  
        /// 修改或增加配置值  
        /// </summary>  
        /// <param name="key">配置标识</param>  
        /// <param name="value">配置值</param>  
        public static void SetValue(string key, string value)
        {
            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings[key].Value = value;
            else
                config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
        }
        /// <summary>  
        /// 删除配置值  
        /// </summary>  
        /// <param name="key">配置标识</param>  
        public static void DeleteValue(string key)
        {
            config.AppSettings.Settings.Remove(key);
        }
    }
}
