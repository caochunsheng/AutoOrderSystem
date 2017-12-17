using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetWebFileLib.Models
{
    [Serializable]
    public enum WebParams
    {
        /// <summary>
        /// 要获取文件的服务器目录.如果notserverpath参数为false,则只能为dataset目录下文件.
        /// </summary>
        filedirectory,

        /// <summary>
        /// 要获取的文件名称.单文件名:"新建文本",多个文件名:"新建文本|新建文本2|"
        /// </summary>
        filename,

        /// <summary>
        /// bool值,说明FileName参数是否是带后缀名的文件名称.
        /// </summary>
        fullname,

        /// <summary>
        /// 要获取对应的后缀名类型文件.例:单个后缀名".xml",多个后缀名".xml|.jpg|"
        /// </summary>
        suffix,

        /// <summary>
        /// 标识当前路径是否为服务器目录.false=是;
        /// 为Ture则可以下载服务器上所有目录文件,只要filedirectory为全路径.
        /// </summary>
        notserverpath,
    }
}

