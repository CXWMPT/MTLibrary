using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    /// <summary>
    /// 弹出框枚举
    /// </summary>
    public enum NTDMsgTypeEnum
    {
        /// <summary>
        /// 提示
        /// </summary>
        Info = -7299687,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = -693140,
        /// <summary>
        /// 错误
        /// </summary>
        Error= -1097849,

        /// <summary>
        /// 成功
        /// </summary>
        Success = -9977286,
        /// <summary>
        /// 询问
        /// </summary>
        Question=0,
        /// <summary>
        /// 确认
        /// </summary>
        Comfirm=0,


    }
}