using NTDCommLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public class MTKeyBoardHelper
    {
        #region 

        /// <summary>
        /// 数字键盘按钮的click方法
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static void KeyboardClickFunc(Button b, ComboBox t)
        {
            switch (b.Text.Trim())
            {
                case ("退格"):
                case ("删除"):
                case ("清除"):
                case ("BAK"):
                    t.Text = t.Text.Remove(t.Text.Length - 1, 1);
                    break;
                case ("清空"):
                case ("CLR"):
                    t.Text = "";
                    break;
                default:
                    //（光标处插入）
                    t.Text += b.Text.Trim();
                    break;
            }
        }

        /// <summary>
        /// 数字键盘按钮的click方法
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static void KeyboardClickFunc(Button b, TextBox t)
        {
            int SelectionStart = t.SelectionStart;//光标位置
            switch (b.Text.Trim())
            {
                case ("退格"):
                case ("删除"):
                case ("清除"):
                case ("BAK"):
                    //（光标处退格）
                    if (t.SelectionStart > 0)
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                        t.SelectionStart = SelectionStart - 1;
                        t.Focus();
                    }
                    break;
                case ("清空"):
                case ("CLR"):
                    //（光标处退格）
                    if (t.SelectionStart > 0)
                    {
                        t.Text = "";
                        t.Select(t.Text.Length, 0);//光标位于文字末尾
                        t.Focus();
                    }
                    break;
                default:
                    //（光标处插入）
                    t.Text = t.Text.Insert(t.SelectionStart, b.Text.Trim());
                    t.SelectionStart = SelectionStart + 1;
                    t.Focus();
                    break;
            }
        }

        /// <summary>
        /// 字母大小写转换
        /// </summary>
        /// <param name="button"></param>
        /// <param name="control"></param>
        public static void LargeSmallConvertsByButton(Button button, Control control)
        {
            bool IsUpper = false;
            switch (button.Text.Trim())
            {
                default:
                case ("大写"):
                    IsUpper = true;
                    button.Text = "小写";
                    break;
                case ("小写"):
                    IsUpper = false;
                    button.Text = "大写";
                    break;
            }
            foreach (Control c in control.Controls)
            {
                if (c is Button)
                {
                    var btn = c as Button;
                    if (IsUpper)
                    {
                        btn.Text = btn.Text.ToLower();
                    }
                    else
                    {
                        btn.Text = btn.Text.ToUpper();
                    }
                }
            }
        }
        #endregion
    }
}
