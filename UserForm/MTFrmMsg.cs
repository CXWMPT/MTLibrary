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

        MTMsgTypeEnum MTMsgTypeEnum = MTMsgTypeEnum.Info;
        public static DialogResult ShowMTFrmMsgQuestion()
        {
            return MTFrmMsg.ShowMsg(MTMsgTypeEnum.Question);
        }

        public static DialogResult ShowMsg(MTMsgTypeEnum msgTypeEnum = MTMsgTypeEnum.Info)
        {
            return new MTFrmMsg(msgTypeEnum).ShowDialog();
        }

        public MTFrmMsg(MTMsgTypeEnum type)
        {
            InitializeComponent();

            BtnOK.ForeColor = BtnCancel.ForeColor = Color.Black;

            switch (type)
            {

                case MTMsgTypeEnum.Question:
                    {
                      
                        this.BackColor = BtnOK.BackColor = BtnCancel.BackColor = Color.FromArgb(235, 243, 255);
                        PnlTop.BackColor = BtnOK.FlatAppearance.MouseOverBackColor = BtnOK.FlatAppearance.BorderColor= BtnCancel.FlatAppearance.MouseOverBackColor = BtnCancel.FlatAppearance.BorderColor= Color.FromArgb(80, 160, 255);
                        lblTitle.Text = "询问";
                        break;
                    }

                default:

                    break;

            }

        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MTFrmMsg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void BtnOK_MouseMove(object sender, MouseEventArgs e)
        {
            var b = (Button)sender;
            switch (MTMsgTypeEnum)
            {
                default:
                case MTMsgTypeEnum.Question:
                    {
                        b.ForeColor = Color.White;
                        break;
                    }
            }
        }

        private void BtnOK_MouseLeave(object sender, EventArgs e)
        {
            var b = (Button)sender;
            switch (MTMsgTypeEnum)
            {
                default:
                case MTMsgTypeEnum.Question:
                    {
                        b.ForeColor = Color.Black;
                        break;
                    }
            }
        }

    }
}
