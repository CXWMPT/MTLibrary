
namespace MTLibrary
{
    partial class FrmTips
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LblMsg = new System.Windows.Forms.Label();
            this.TmrCloseTime = new System.Windows.Forms.Timer(this.components);
            this.BtnClose = new System.Windows.Forms.PictureBox();
            this.PctStat = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctStat)).BeginInit();
            this.SuspendLayout();
            // 
            // LblMsg
            // 
            this.LblMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblMsg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.LblMsg.Location = new System.Drawing.Point(32, 0);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(279, 46);
            this.LblMsg.TabIndex = 4;
            this.LblMsg.Text = "提示信息";
            this.LblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TmrCloseTime
            // 
            this.TmrCloseTime.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.Image = global::MTLibrary.Properties.Resources.qty_delete;
            this.BtnClose.Location = new System.Drawing.Point(313, 11);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(25, 25);
            this.BtnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnClose.TabIndex = 5;
            this.BtnClose.TabStop = false;
            this.BtnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnClose_MouseDown);
            // 
            // PctStat
            // 
            this.PctStat.BackColor = System.Drawing.Color.Transparent;
            this.PctStat.Image = global::MTLibrary.Properties.Resources.error;
            this.PctStat.Location = new System.Drawing.Point(7, 13);
            this.PctStat.Name = "PctStat";
            this.PctStat.Size = new System.Drawing.Size(20, 20);
            this.PctStat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PctStat.TabIndex = 3;
            this.PctStat.TabStop = false;
            // 
            // FrmTips
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(350, 50);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LblMsg);
            this.Controls.Add(this.PctStat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTips";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmTips";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTips_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTips_FormClosed);
            this.Load += new System.EventHandler(this.FrmTips_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctStat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BtnClose;
        private System.Windows.Forms.Label LblMsg;
        private System.Windows.Forms.PictureBox PctStat;
        private System.Windows.Forms.Timer TmrCloseTime;
    }
}