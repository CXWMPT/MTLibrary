using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public partial class MTFrmMsg : Form
    {

        Color ThemeColor = Color.FromArgb(80, 160, 255);

        Color ThemeBackColor = Color.FromArgb(235, 243, 255);

        public static bool DefaultCanCountDown = false;

        public static int DefaultTimeout = 60;

        //倒计时参数
        private bool CanCountDown;

        private int WaitedTime;

        private int Timeout;

        public static DialogResult ShowMTFrmMsgInfo(string msg)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Info, msg, DefaultCanCountDown, DefaultTimeout);

        }
        public static DialogResult ShowMTFrmMsgQuestion(string msg)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Question, msg, DefaultCanCountDown, DefaultTimeout);
        }

        public static DialogResult ShowMTFrmMsgSuccess(string msg)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Success, msg, DefaultCanCountDown, DefaultTimeout);
        }
        public static DialogResult ShowMTFrmMsgWarning(string msg)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Warning, msg, DefaultCanCountDown, DefaultTimeout);

        }
        public static DialogResult ShowMTFrmMsgError(string msg)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Error, msg, DefaultCanCountDown, DefaultTimeout);

        }

        public static DialogResult ShowMTFrmMsgInfo(string msg, bool canCountDown, int timeout)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Info, msg, canCountDown, timeout);
        }
        public static DialogResult ShowMTFrmMsgQuestion(string msg, bool canCountDown, int timeout)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Question, msg, canCountDown, timeout);
        }
        public static DialogResult ShowMTFrmMsgSuccess(string msg, bool canCountDown, int timeout)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Success, msg, canCountDown, timeout);
        }
        public static DialogResult ShowMTFrmMsgWarning(string msg, bool canCountDown, int timeout)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Warning, msg, canCountDown, timeout);
        }
        public static DialogResult ShowMTFrmMsgError(string msg, bool canCountDown, int timeout)
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Error, msg, canCountDown, timeout);
        }


        public static DialogResult ShowMsg(MTMsgTypeEnum msgTypeEnum, string msg, bool canCountDown, int timeout)
        {
            return new MTFrmMsg(msgTypeEnum, msg, canCountDown, timeout).ShowDialog();
        }

        public MTFrmMsg(MTMsgTypeEnum type, string msg, bool canCountDown, int timeout)
        {
            InitializeComponent();

            BtnOK.ForeColor = BtnCancel.ForeColor = Color.Black;

            switch (type)
            {
                default:
                case MTMsgTypeEnum.Info:
                    {
                        BtnOK.Width = BtnOK.Width *2;
                        BtnCancel.Visible = false;
                        ThemeColor = Color.FromArgb(80, 160, 255);
                        ThemeBackColor = Color.FromArgb(235, 243, 255);
                        LblTitle.Text = "提示";
                        break;
                    }
                case MTMsgTypeEnum.Question:
                    {
                        ThemeColor = Color.FromArgb(80, 160, 255);
                        ThemeBackColor = Color.FromArgb(235, 243, 255);
                        LblTitle.Text = "询问";
                        break;
                    }
                case MTMsgTypeEnum.Success:
                    {
                        BtnOK.Width = BtnOK.Width * 2;
                        BtnCancel.Visible = false;
                        ThemeColor = Color.FromArgb(110, 190, 40);
                        ThemeBackColor = Color.FromArgb(239, 248, 232);
                        LblTitle.Text = "成功";
                        break;
                    }
                case MTMsgTypeEnum.Warning:
                    {
                        BtnOK.Width = BtnOK.Width * 2;
                        BtnCancel.Visible = false;
                        ThemeColor = Color.FromArgb(220, 155, 40);
                        ThemeBackColor = Color.FromArgb(251, 249, 229);
                        LblTitle.Text = "警告";
                        break;
                    }
                case MTMsgTypeEnum.Error:
                    {
                        BtnOK.Width = BtnOK.Width * 2;
                        BtnCancel.Visible = false;
                        ThemeColor = Color.FromArgb(230, 80, 80);
                        ThemeBackColor = Color.FromArgb(251, 238, 238);
                        LblTitle.Text = "错误";
                        break;
                    }

            }

            PnlTop.BackColor = BtnOK.FlatAppearance.MouseOverBackColor = BtnOK.FlatAppearance.BorderColor = BtnCancel.FlatAppearance.MouseOverBackColor = BtnCancel.FlatAppearance.BorderColor = ThemeColor;
            this.BackColor = BtnOK.BackColor = BtnCancel.BackColor = ThemeBackColor;

            LblMessage.Text = msg;

            Timeout = timeout;
            CanCountDown = canCountDown;

            if (CanCountDown)
            {
                LblCountDown.Text = Timeout.ToString();
                ResetCountDownWaitedTime(ref WaitedTime, TmrSystemTime);
            }
        }

        private void MTFrmMsg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void BtnOK_MouseMove(object sender, MouseEventArgs e)
        {
            var b = (Button)sender;
            b.ForeColor = Color.White;
        }

        private void BtnOK_MouseLeave(object sender, EventArgs e)
        {
            var b = (Button)sender;
            b.ForeColor = Color.Black;
        }


        /// <summary>
        /// 窗体设置圆角
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTFrmMsg_Paint(object sender, PaintEventArgs e)
        {

            //设置圆角
            GraphicsPath oPath = new GraphicsPath();

            int x = 0;
            int y = 0;
            int w = Width;
            int h = Height;
            int a = 10;
            Graphics g = CreateGraphics();
            oPath.AddArc(x, y, a, a, 180, 90); //边框格式
            oPath.AddArc(w - a, y, a, a, 270, 90);
            oPath.AddArc(w - a / 2, h - a / 2, a / 2, a / 2, 0, 90);
            oPath.AddArc(x, h - a, a, a, 90, 90);
            oPath.CloseAllFigures();
            Region = new Region(oPath);

        }


        /// <summary>
        /// 窗体划线
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
            //圆弧半径
            int cornerRadius = 7;
            //边框颜色
            Color c = ThemeColor;
            Pen p = new Pen(c);
            //边框工作区
            Rectangle rect = this.ClientRectangle;
            GraphicsPath Rect = new GraphicsPath();
            // 添加圆弧
            Rect.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
            Rect.AddArc(rect.Width - cornerRadius * 2 - 1, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
            Rect.AddArc(rect.Width - cornerRadius * 2 - 1, rect.Height - cornerRadius * 2 - 1, cornerRadius * 2, cornerRadius * 2, 0, 90);
            Rect.AddArc(0, rect.Height - cornerRadius * 2 - 1, cornerRadius * 2, cornerRadius * 2, 90, 90);
            Rect.CloseFigure();
            e.Graphics.DrawPath(p, Rect);
        }

        #region 内部方法
       
        /// <summary>
        /// 窗口倒计时时间重置
        /// </summary>
        /// <param name="waitedTime"></param>
        /// <param name="timer"></param>
        private void ResetCountDownWaitedTime(ref int waitedTime, System.Windows.Forms.Timer timer)
        {
            waitedTime = 0;
            if (!timer.Enabled) timer.Enabled = true;
        }

        /// <summary>
        /// 窗口倒计时停止定时器记时
        /// </summary>
        /// <param name="waitedTime"></param>
        /// <param name="timer"></param>
        private void StopCountDownWaitedTime(ref int waitedTime, System.Windows.Forms.Timer timer)
        {
            waitedTime = 0;
            if (timer.Enabled) timer.Enabled = false;
        }
        /// <summary>
        /// 开启定时器记时
        /// </summary>
        /// <param name="timer"></param>
        private void ResetWaitedTime(System.Windows.Forms.Timer timer)
        {
            if (!timer.Enabled) timer.Enabled = true;
        }
        /// <summary>
        /// 停止定时器记时
        /// </summary>
        /// <param name="timer"></param>
        private void StopWaitedTime(System.Windows.Forms.Timer timer)
        {
            if (timer.Enabled) timer.Enabled = false;
        }
        #endregion

        private void TmrSystemTime_Tick(object sender, EventArgs e)
        {
            if (CanCountDown)
            {
                if (WaitedTime >= Timeout)
                {
                    TmrSystemTime.Enabled = false;
                    this.Close();
                }
                else
                {
                    WaitedTime++; //等待时间计时
                    LblCountDown.Text = (Timeout - WaitedTime).ToString();
                }
            }
        }

        private void MTFrmMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            TmrSystemTime.Enabled = false;
        }


    }
}
