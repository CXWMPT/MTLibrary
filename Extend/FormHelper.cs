using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MTLibrary
{
    public class FormHelper
    {

        #region 

        /// <summary>
        /// 窗体居中
        /// </summary>
        /// <param name="form"></param>
        public static void FormCenter(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point((Screen.PrimaryScreen.Bounds.Width - form.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - form.Height) / 2);//居中
        }

        /// <summary>
        /// 控件居中
        /// </summary>
        /// <param name="control"></param>
        public static void FormCenter(Control control)
        {
            FormCenter(control, 0, 0);
        }

        /// <summary>
        /// 控件居中
        /// </summary>
        /// <param name="control"></param>
        /// <param name="formWidth"></param>
        /// <param name="formHeight"></param>
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
        public static void FormBottom(Form form)
        {
            FormBottom(form, 0, 0);
        }

        /// <summary>
        /// 窗体位置居中
        /// </summary>
        /// <param name="form"></param>
        /// <param name="formWidth"></param>
        /// <param name="formHeight"></param>
        public static void FormBottom(Form form, int formWidth, int formHeight)
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

    }
}
