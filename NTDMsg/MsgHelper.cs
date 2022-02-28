using System.Windows.Forms;

namespace MTLibrary
{
    /// <summary>
    /// 信息提示弹出窗口（弹出框默认是倒计时60s）
    /// </summary>
    public static class MsgHelper
    {
     
        /// <summary>
        /// 信息提示弹出窗口（弹出框默认是倒计时60s）
        /// </summary>
        public static bool CanCountDown = true;
        public static int Timeout = 60;

        #region 系统自带弹出框

        /// <summary>
        /// 提示弹出框[微软自带]
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult PopInfo(string msg)
        {
            return MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 警告弹出框[微软自带]
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult PopWarning(string msg)
        {
            return MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 错误弹出框[微软自带]
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult PopError(string msg)
        {
            return MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 询问弹出框[微软自带]，继续操作返回OK
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult PopQuestion(string msg)
        {
            return MessageBox.Show(msg, "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 确认弹出框[微软自带]，继续操作返回OK
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DialogResult PopComfirm(string msg)
        {
            return MessageBox.Show(msg, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        #endregion

    }
}
