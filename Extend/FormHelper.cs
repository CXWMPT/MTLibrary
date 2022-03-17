using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MTLibrary
{
    public class FormHelper
    {

        #region 
        
        /// <summary>
        /// 系统重启（参数是否程序唯一）
        /// </summary>
        /// <param name="AppIsonly"></param>
        public static void RestartProject()
        {
            //开启新的实例
            Process.Start(Application.ExecutablePath);
            //关闭当前实例
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// 窗体居中
        /// </summary>
        /// <param name="form"></param>
        public static void FormCenter(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point((Screen.PrimaryScreen.Bounds.Width - form.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - form.Height) / 2);//居中
        }

        public static void FormCenter(Control control)
        {
            FormCenter(control, 0, 0);
        }

        public static void FormCenter(Control control, int formWidth, int formHeight)
        {
            if (formWidth <= 0 || formHeight <= 0)
            {
                control.Location = new Point((Screen.PrimaryScreen.Bounds.Width - control.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - control.Height) / 2);
            }
            else
            {
                control.Location = new Point((formWidth - control.Width) / 2, (formHeight - control.Height) / 2);
            }
        }
        /// <summary>
        /// 窗体位置居中
        /// </summary>
        /// <param name="form"></param>
        public static void FormBottonCenter(Form form)
        {
            FormBottonCenter(form, 0, 0);
        }

        /// <summary>
        /// 窗体位置居中
        /// </summary>
        /// <param name="form"></param>
        /// <param name="formWidth"></param>
        /// <param name="formHeight"></param>
        public static void FormBottonCenter(Form form, int formWidth, int formHeight)
        {
            form.StartPosition = FormStartPosition.Manual;
            if (formWidth <= 0 || formHeight <= 0)
            {
                form.Location = new Point((Screen.PrimaryScreen.Bounds.Width - form.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - form.Height));
            }
            else
            {
                form.Location = new Point((Screen.PrimaryScreen.Bounds.Width - form.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - formHeight) / 2 + formHeight - form.Height);
            }
        }


        /// <summary>
        /// 设置实线边框粗细[默认窗体上左下右] 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="width"></param>
        /// <param name="color"></param>
        /// <param name="position"></param>
        public static void BorderSettings(object sender, PaintEventArgs e, float width, Color color, string position)
        {
            Control control = (Control)sender;
            Pen pen = new Pen(color, width);
            pen.DashStyle = DashStyle.Solid;//实线
            var positionArray = position.Split(',');
            foreach (string data in positionArray)
            {
                switch (data)
                {
                    case "上":
                        e.Graphics.DrawLine(pen, 0, 0, control.Width - 0, 0);//上
                        break;
                    case "左":
                        e.Graphics.DrawLine(pen, 0, 0, 0, control.Height - 0);//左
                        break;
                    case "下":
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, 0, control.Height - 1);//下
                        break;
                    case "右":
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, control.Width - 1, 0);//右
                        break;
                    default:
                        e.Graphics.DrawLine(pen, 0, 0, control.Width - 0, 0);//上
                        e.Graphics.DrawLine(pen, 0, 0, 0, control.Height - 0);//左
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, 0, control.Height - 1);//下
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, control.Width - 1, 0);//右
                        break;
                }
            }
        }
        #endregion

        #region 废弃方法



        public static void FormTopCenter(Form form, int width = 0, int height = 0)
        {

        }

        /// <summary>
        /// 窗体居下(已废弃)
        /// </summary>
        /// <param name="form"></param>
        public static void FormBotton(Form form)
        {
            form.Location = new Point((Screen.PrimaryScreen.Bounds.Width - form.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - form.Height) / 2);
        }



        /// <summary>
        /// 设置实线边框粗细[默认窗体上左下右]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="dashStyle">Solid：指实线边框。Dash：指定包含短划线的行。DashDot：指定包含重复的点划线图案的行。DashDotDot：指定包含重复的短划线-点点图案的行。Dot：指定由点构成的行。Custom：指定用户定义自定义的短划线样式。</param>
        /// <param name="width"></param>
        /// <param name="color"></param>
        /// <param name="position"></param>
        public static void BorderSettings(object sender, PaintEventArgs e, DashStyle dashStyle, float width, Color color, string position)
        {
            Control control = (Control)sender;
            Pen pen = new Pen(color, width);
            pen.DashStyle = dashStyle;
            var positionArray = position.Split(',');
            foreach (string data in positionArray)
            {
                switch (data)
                {
                    case "上":
                        e.Graphics.DrawLine(pen, 0, 0, control.Width - 0, 0);//上
                        break;
                    case "左":
                        e.Graphics.DrawLine(pen, 0, 0, 0, control.Height - 0);//左
                        break;
                    case "下":
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, 0, control.Height - 1);//下
                        break;
                    case "右":
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, control.Width - 1, 0);//右
                        break;
                    default:
                        e.Graphics.DrawLine(pen, 0, 0, control.Width - 0, 0);//上
                        e.Graphics.DrawLine(pen, 0, 0, 0, control.Height - 0);//左
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, 0, control.Height - 1);//下
                        e.Graphics.DrawLine(pen, control.Width - 1, control.Height - 1, control.Width - 1, 0);//右
                        break;
                }
            }
        }

        /// <summary>
        /// 设置窗口圆角
        /// </summary>
        /// <param name="frm"></param>
        public static void SetWindowRegion(Control frm)
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, frm.Width, frm.Height);
            FormPath = GetRoundedRectPath(rect, 8);
            frm.Region = new Region(FormPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">窗体大小</param>
        /// <param name="radius">圆角大小</param>
        /// <returns></returns>
        private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角

            arcRect.X = rect.Right - diameter;//右上角
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 清空并赋值dgv
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dgv"></param>
        /// <param name="list"></param>
        /// <param name="IsRowsSelected"></param>
        public static void CleanAndAssignmentDgv<T>(Form frm, DataGridView dgv, List<T> list, bool IsRowsSelected = false)
        {
            if (dgv.DataSource != null)
            {
                frm.BindingContext[dgv.DataSource].SuspendBinding();
            }
            dgv.DataSource = new List<object>();
            dgv.DataSource = list;
            frm.BindingContext[dgv.DataSource].ResumeBinding();
            if (IsRowsSelected)
            {
                if (list.Count > 0)
                {
                    dgv.Rows[0].Selected = false;
                }
            }
        }


        /// <summary>
        /// 获取Treeview选中的值
        /// </summary>
        /// <param name="treeView"></param>
        /// <returns></returns>
        public static List<TreeNode> GetSelectCheckedByTreeView(TreeView treeView)
        {
            List<TreeNode> listNodes = new List<TreeNode>();
            foreach (TreeNode node in treeView.Nodes)
            {
                FindCheckNode(node, listNodes);
            }
            return listNodes;
        }

        public static void FindCheckNode(TreeNode node, List<TreeNode> listNodes)
        {
            if (node.Checked)
            {
                listNodes.Add(node);
            }
            foreach (TreeNode childnode in node.Nodes)
            {
                FindCheckNode(childnode, listNodes);
            }
        }




        //限制TextBox为int double(在KeyPress的事件里使用)
        public static void TextBoxIsIntOrDouble(TextBox Txt, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46) e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (Txt.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f; float oldf; bool b1 = false, b2 = false;
                    b1 = float.TryParse(Txt.Text, out oldf);
                    b2 = float.TryParse(Txt.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }


        /// <summary>
        /// //限制TextBox为int(在KeyPress的事件里使用)
        /// </summary>
        /// <param name="Txt">控件</param>
        /// <param name="e"></param>
        public static void TextBoxIsNumber(TextBox Txt, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8) e.Handled = true;

        }

        //清除容器控件的值
        public static bool ClearControlByControlType(Control control, string type)
        {
            try
            {
                foreach (Control c in control.Controls)
                {
                    switch (type)
                    {
                        case ("TextBox"):
                            if (c is TextBox)
                            {
                                ((TextBox)c).Text = "";
                            }
                            break;
                        case ("ComboBox"):
                            if (c is ComboBox)
                            {
                                ((ComboBox)c).SelectedIndex = -1;
                                ((ComboBox)c).Text = "";
                            }
                            break;
                        default:
                            break;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //清除Form控件的值
        public static bool ClearTextBoxAndComboBox(Form frm)
        {
            try
            {
                foreach (Control c in frm.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)c).Text = "";
                    }
                    else if (c is ComboBox)
                    {
                        ((ComboBox)c).SelectedIndex = -1;
                        ((ComboBox)c).Text = "";
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        //清除Form控件的值
        public static bool ClearForm(Form frm, bool textBox = false, bool comboBox = false, bool pictureBox = false)
        {
            try
            {
                foreach (Control c in frm.Controls)
                {
                    if (c is TextBox)
                    {
                        if (!textBox) continue;
                        ((TextBox)c).Text = "";
                    }
                    else if (c is ComboBox)
                    {
                        if (!comboBox) continue;
                        ((ComboBox)c).SelectedIndex = -1;
                        ((ComboBox)c).Text = "";
                    }
                    else if (c is PictureBox)
                    {
                        if (!pictureBox) continue;
                        ((PictureBox)c).Image = null;

                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //清除Pnl控件的值
        public static bool ClearForm(Panel frm, bool textBox = false, bool comboBox = false)
        {
            try
            {
                foreach (Control c in frm.Controls)
                {
                    if (c is TextBox)
                    {
                        if (!textBox) continue;
                        ((TextBox)c).Text = "";
                    }
                    else if (c is ComboBox)
                    {
                        if (!comboBox) continue;
                        ((ComboBox)c).SelectedIndex = -1;
                        ((ComboBox)c).Text = "";
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }



        /// 选中控件后改变字体颜色，其他相同控件恢复颜色
        /// </summary>参数一 当前控件所在容器，参数二 当前控件，参数三 改变之后的颜色
        /// <param name="pnl"></param>
        public static void ChangeForeColor(Control controlList, Control control, Color color)
        {
            var Color = control.ForeColor;
            if (Color.Equals(color)) return;
            control.ForeColor = color;
            foreach (Control date in controlList.Controls)
            {
                if (date != control) date.ForeColor = Color;

            }
        }
        /// 选中控件后改变背景颜色，其他相同控件恢复颜色
        /// </summary>参数一 当前控件所在容器，参数二 当前控件，参数三 改变之后的颜色
        /// <param name="pnl"></param>
        public static void ChangeBackColor(Control controlList, Control control, Color color)
        {
            var Color = control.BackColor;
            if (Color.Equals(color)) return;
            control.BackColor = color;
            foreach (Control date in controlList.Controls)
            {
                if (date != control) date.BackColor = Color;
            }
        }
        public static void ChangeBackColor(List<Button> controlList, Button control, Color color)
        {
            var Color = control.BackColor;
            if (Color.Equals(color)) return;
            control.BackColor = color;
            foreach (Control date in controlList)
            {
                if (date != control) date.BackColor = Color;
            }
        }

        /// <summary>
        /// 窗口倒计时时间重置
        /// </summary>
        /// <param name="waitedTime"></param>
        /// <param name="timer"></param>
        public static void ResetCountDownWaitedTime(ref int waitedTime, System.Windows.Forms.Timer timer)
        {
            waitedTime = 0;
            if (!timer.Enabled) timer.Enabled = true;
        }

        /// <summary>
        /// 窗口倒计时停止定时器记时
        /// </summary>
        /// <param name="waitedTime"></param>
        /// <param name="timer"></param>
        public static void StopCountDownWaitedTime(ref int waitedTime, System.Windows.Forms.Timer timer)
        {
            waitedTime = 0;
            if (timer.Enabled) timer.Enabled = false;
        }
        /// <summary>
        /// 开启定时器记时
        /// </summary>
        /// <param name="timer"></param>
        public static void ResetWaitedTime(System.Windows.Forms.Timer timer)
        {
            if (!timer.Enabled) timer.Enabled = true;
        }
        /// <summary>
        /// 停止定时器记时
        /// </summary>
        /// <param name="timer"></param>
        public static void StopWaitedTime(System.Windows.Forms.Timer timer)
        {
            if (timer.Enabled) timer.Enabled = false;
        }
        //解决WinForm中TableLayout控件闪烁的问题
        public static bool FormTableLayoutPanelFlickeringProblem(TableLayoutPanel t)
        {
            try
            {
                t.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(t, true, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion




    }
}
