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
        ///  未知
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// 提示
        /// </summary>
        Info,
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 询问
        /// </summary>
        Question,
        /// <summary>
        /// 确认
        /// </summary>
        Comfirm,
        /// <summary>
        /// 带报警警告
        /// </summary>
        WarningWithAlarm,

    }
}