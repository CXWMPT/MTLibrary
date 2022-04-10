
namespace MTLibrary
{
    partial class MTFrmMsg
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
            this.LblTitle = new System.Windows.Forms.Label();
            this.TmrSystemTime = new System.Windows.Forms.Timer(this.components);
            this.LblCountDown = new System.Windows.Forms.Label();
            this.PnlTop = new System.Windows.Forms.Panel();
            this.LblMessage = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.PnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.Transparent;
            this.LblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTitle.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(398, 40);
            this.LblTitle.TabIndex = 1;
            this.LblTitle.Text = "系统提示";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TmrSystemTime
            // 
            this.TmrSystemTime.Interval = 1000;
            this.TmrSystemTime.Tick += new System.EventHandler(this.TmrSystemTime_Tick);
            // 
            // LblCountDown
            // 
            this.LblCountDown.BackColor = System.Drawing.Color.Transparent;
            this.LblCountDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblCountDown.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold);
            this.LblCountDown.ForeColor = System.Drawing.Color.White;
            this.LblCountDown.Location = new System.Drawing.Point(398, 0);
            this.LblCountDown.Margin = new System.Windows.Forms.Padding(0);
            this.LblCountDown.Name = "LblCountDown";
            this.LblCountDown.Size = new System.Drawing.Size(54, 40);
            this.LblCountDown.TabIndex = 86;
            this.LblCountDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlTop
            // 
            this.PnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.PnlTop.Controls.Add(this.LblTitle);
            this.PnlTop.Controls.Add(this.LblCountDown);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(452, 40);
            this.PnlTop.TabIndex = 319;
            // 
            // LblMessage
            // 
            this.LblMessage.BackColor = System.Drawing.Color.Transparent;
            this.LblMessage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.LblMessage.ForeColor = System.Drawing.Color.Black;
            this.LblMessage.Location = new System.Drawing.Point(16, 51);
            this.LblMessage.Name = "LblMessage";
            this.LblMessage.Size = new System.Drawing.Size(422, 158);
            this.LblMessage.TabIndex = 321;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.BtnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(226, 216);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(223, 48);
            this.BtnCancel.TabIndex = 322;
            this.BtnCancel.TabStop = false;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            this.BtnCancel.MouseLeave += new System.EventHandler(this.BtnOK_MouseLeave);
            this.BtnCancel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnOK_MouseMove);
            // 
            // BtnOK
            // 
            this.BtnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.BtnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOK.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnOK.ForeColor = System.Drawing.Color.White;
            this.BtnOK.Location = new System.Drawing.Point(3, 216);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(0);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(223, 48);
            this.BtnOK.TabIndex = 320;
            this.BtnOK.TabStop = false;
            this.BtnOK.Text = "确定";
            this.BtnOK.UseVisualStyleBackColor = false;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            this.BtnOK.MouseLeave += new System.EventHandler(this.BtnOK_MouseLeave);
            this.BtnOK.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnOK_MouseMove);
            // 
            // MTFrmMsg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(452, 270);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblMessage);
            this.Controls.Add(this.PnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MTFrmMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTFrmMsg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MTFrmMsg_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MTFrmMsg_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MTFrmMsg_Paint);
            this.PnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Timer TmrSystemTime;
        private System.Windows.Forms.Label LblCountDown;
        private System.Windows.Forms.Panel PnlTop;
        private System.Windows.Forms.Label LblMessage;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
    }
}