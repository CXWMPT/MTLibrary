using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public partial class FrmTips : Form
    {
        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        private ContentAlignment m_showAlign = ContentAlignment.BottomLeft;
        /// <summary>
        /// 显示的位置
        /// </summary>
        public ContentAlignment ShowAlign
        {
            get { return m_showAlign; }
            set { m_showAlign = value; }
        }


        /// <summary>
        /// 窗体倒计时
        /// </summary>
        private int WaitedTime = 0;

        private static KeyValuePair<string, FrmTips> m_lastTips = new KeyValuePair<string, FrmTips>();

        private static List<FrmTips> m_lstTips = new List<FrmTips>();


        /// <summary>
        /// Shows the tips success.
        /// </summary>
        /// <param name="frm">The FRM.</param>
        /// <param name="strMsg">The string MSG.</param>
        /// <returns>FrmTips.</returns>
        public static FrmTips ShowTipsSuccess(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 3000, false, NTDMsgTypeEnum.Success);
        }

        /// <summary>
        /// Shows the tips error.
        /// </summary>
        /// <param name="frm">The FRM.</param>
        /// <param name="strMsg">The string MSG.</param>
        /// <returns>FrmTips.</returns>
        public static FrmTips ShowTipsError(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 3000, false, NTDMsgTypeEnum.Error, MTSizeModeEnum.Medium);
        }

        /// <summary>
        /// Shows the tips information.
        /// </summary>
        /// <param name="frm">The FRM.</param>
        /// <param name="strMsg">The string MSG.</param>
        /// <returns>FrmTips.</returns>
        public static FrmTips ShowTipsInfo(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 3000, false, NTDMsgTypeEnum.Info, MTSizeModeEnum.Medium);
        }
        /// <summary>
        /// Shows the tips warning.
        /// </summary>
        /// <param name="frm">The FRM.</param>
        /// <param name="strMsg">The string MSG.</param>
        /// <returns>FrmTips.</returns>
        public static FrmTips ShowTipsWarning(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 3000, false, NTDMsgTypeEnum.Warning, MTSizeModeEnum.Medium);
        }


        public static FrmTips ShowTips(Form frm, string strMsg,int waitedTime = 0,bool blnShowCoseBtn = true, NTDMsgTypeEnum msgTypeEnum = NTDMsgTypeEnum.Info, MTSizeModeEnum sizeModeEnum = MTSizeModeEnum.Medium)
        {

            if (m_lastTips.Key == strMsg + msgTypeEnum && !m_lastTips.Value.IsDisposed && m_lastTips.Value.Visible)
            {
                m_lastTips.Value.ResetTimer();
                return m_lastTips.Value;
            }
            else
            {
                FrmTips frmTips = new FrmTips();

                if ((int)msgTypeEnum==0) 
                {
                    msgTypeEnum = NTDMsgTypeEnum.Info;
                }

                switch (sizeModeEnum)
                {
                    default:
                    case MTSizeModeEnum.Medium:
                        frmTips.Size = new Size(350, 50);
                        break;
                    case MTSizeModeEnum.Small:
                        frmTips.Size = new Size(350, 35);
                        break;

                    case MTSizeModeEnum.Large:
                        frmTips.Size = new Size(350, 65);
                        break;
                }

                frmTips.BackColor = Color.FromArgb((int)msgTypeEnum);
                frmTips.LblMsg.ForeColor = Color.White;

              
                switch (msgTypeEnum)
                {
                    default:
                    case NTDMsgTypeEnum.Info:
                        frmTips.PctStat.Image = Properties.Resources.alarm;
                        break;
                    case NTDMsgTypeEnum.Success:
                        frmTips.PctStat.Image = Properties.Resources.success;
                        break;
                    case NTDMsgTypeEnum.Warning:
                        frmTips.PctStat.Image = Properties.Resources.warning;
                        break;
                    case NTDMsgTypeEnum.Error:
                        frmTips.PctStat.Image = Properties.Resources.error;
                        break;
                }

                frmTips.LblMsg.Text = strMsg;
                frmTips.WaitedTime = waitedTime;
                frmTips.BtnClose.Visible = blnShowCoseBtn;


                frmTips.ShowAlign = ContentAlignment.BottomCenter;
                frmTips.Owner = frm;
                FrmTips.m_lstTips.Add(frmTips);
                FrmTips.ReshowTips();
                frmTips.Show(frm);
                if (frm != null && !frm.IsDisposed)
                {
                   SetForegroundWindow(frm.Handle);
                }
                //frmTips.BringToFront();
                m_lastTips = new KeyValuePair<string, FrmTips>(strMsg + msgTypeEnum, frmTips);
                return frmTips;
            }
        }


        public FrmTips()
        {
            InitializeComponent();
        }

        private void FrmTips_Load(object sender, EventArgs e)
        {
            if (WaitedTime > 0)
            {
                this.TmrCloseTime.Interval = WaitedTime;
                this.TmrCloseTime.Enabled = true;
            }
        }

        /// <summary>
        /// 重置倒计时
        /// </summary>
        public void ResetTimer()
        {
            if (WaitedTime > 0)
            {
                TmrCloseTime.Enabled = false;
                TmrCloseTime.Enabled = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.TmrCloseTime.Enabled = false;
            this.Close();
        }

        #region 刷新显示
        /// <summary>
        /// 功能描述:刷新显示
        /// 作　　者:HZH
        /// 创建日期:2019-02-28 15:33:06
        /// 任务编号:POS
        /// </summary>
        public static void ReshowTips()
        {
            lock (FrmTips.m_lstTips)
            {
                FrmTips.m_lstTips.RemoveAll(p => p.IsDisposed == true);
                var enumerable = from p in FrmTips.m_lstTips
                                 group p by new
                                 {
                                     p.ShowAlign
                                 };
                Screen currentScreen = Screen.PrimaryScreen;

                var firstTip = FrmTips.m_lstTips.FirstOrDefault();
                if (firstTip != null && firstTip.Owner != null)
                {
                    currentScreen = Screen.FromControl(firstTip.Owner);
                }

                Size size = currentScreen.Bounds.Size;
                foreach (var item in enumerable)
                {
                    List<FrmTips> list = FrmTips.m_lstTips.FindAll((FrmTips p) => p.ShowAlign == item.Key.ShowAlign);
                    for (int i = 0; i < list.Count; i++)
                    {
                        FrmTips frmTips = list[i];
                        if (frmTips.InvokeRequired)
                        {
                            frmTips.BeginInvoke(new MethodInvoker(delegate ()
                            {
                                switch (item.Key.ShowAlign)
                                {
                                    case ContentAlignment.BottomCenter:
                                        frmTips.Location = GetScreenLocation(currentScreen, (size.Width - frmTips.Width) / 2, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.BottomLeft:
                                        frmTips.Location = GetScreenLocation(currentScreen, 10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.BottomRight:
                                        frmTips.Location = GetScreenLocation(currentScreen, size.Width - frmTips.Width - 10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.MiddleCenter:
                                        frmTips.Location = GetScreenLocation(currentScreen, (size.Width - frmTips.Width) / 2, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.MiddleLeft:
                                        frmTips.Location = GetScreenLocation(currentScreen, 10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.MiddleRight:
                                        frmTips.Location = GetScreenLocation(currentScreen, size.Width - frmTips.Width - 10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.TopCenter:
                                        frmTips.Location = GetScreenLocation(currentScreen, (size.Width - frmTips.Width) / 2, 10 + (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.TopLeft:
                                        frmTips.Location = GetScreenLocation(currentScreen, 10, 10 + (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.TopRight:
                                        frmTips.Location = GetScreenLocation(currentScreen, size.Width - frmTips.Width - 10, 10 + (i + 1) * (frmTips.Height + 10));
                                        break;
                                    default:
                                        break;
                                }
                            }));
                        }
                        else
                        {
                            switch (item.Key.ShowAlign)
                            {
                                case ContentAlignment.BottomCenter:
                                    frmTips.Location =GetScreenLocation(currentScreen, (size.Width - frmTips.Width) / 2, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.BottomLeft:
                                    frmTips.Location =GetScreenLocation(currentScreen, 10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.BottomRight:
                                    frmTips.Location =GetScreenLocation(currentScreen, size.Width - frmTips.Width - 10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.MiddleCenter:
                                    frmTips.Location =GetScreenLocation(currentScreen, (size.Width - frmTips.Width) / 2, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.MiddleLeft:
                                    frmTips.Location =GetScreenLocation(currentScreen, 10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.MiddleRight:
                                    frmTips.Location =GetScreenLocation(currentScreen, size.Width - frmTips.Width - 10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.TopCenter:
                                    frmTips.Location =GetScreenLocation(currentScreen, (size.Width - frmTips.Width) / 2, 10 + (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.TopLeft:
                                    frmTips.Location =GetScreenLocation(currentScreen, 10, 10 + (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.TopRight:
                                    frmTips.Location =GetScreenLocation(currentScreen, size.Width - frmTips.Width - 10, 10 + (i + 1) * (frmTips.Height + 10));
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                }
            }
        }


        /// <summary>
        /// 相对于屏幕显示的位置
        /// </summary>
        /// <param name="screen">窗体需要显示的屏幕</param>
        /// <param name="left">left</param>
        /// <param name="top">top</param>
        /// <returns></returns>
        public static Point GetScreenLocation(Screen screen, int left, int top)
        {
            return new Point(screen.Bounds.Left + left, screen.Bounds.Top + top);
        }
        #endregion

        private void FrmTips_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_lastTips.Value == this)
                m_lastTips = new KeyValuePair<string, FrmTips>();
            m_lstTips.Remove(this);
            ReshowTips();

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].IsDisposed || !Application.OpenForms[i].Visible || Application.OpenForms[i] is FrmTips)
                {
                    continue;
                }
                else
                {
                    Timer t = new Timer();
                    t.Interval = 100;
                    var frm = Application.OpenForms[i];
                    t.Tick += (a, b) =>
                    {
                        t.Enabled = false;
                        if (!frm.IsDisposed)
                            SetForegroundWindow(frm.Handle);
                    };
                    t.Enabled = true;
                    break;
                }
            }
        }

        private void FrmTips_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.TmrCloseTime.Enabled = false;
            this.Close();
        }
    }

}
