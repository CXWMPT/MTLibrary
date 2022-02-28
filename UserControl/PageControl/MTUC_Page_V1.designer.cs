
namespace MTLibrary
{
    partial class MTUC_Page_V1
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.HomePage = new System.Windows.Forms.Button();
            this.PrevPage = new System.Windows.Forms.Button();
            this.NextPage = new System.Windows.Forms.Button();
            this.LastPage = new System.Windows.Forms.Button();
            this.JumpPageValue = new System.Windows.Forms.TextBox();
            this.Jump = new System.Windows.Forms.Button();
            this.ControlRecordCount = new System.Windows.Forms.Label();
            this.CmbLimits = new System.Windows.Forms.ComboBox();
            this.TlpPage = new System.Windows.Forms.TableLayoutPanel();
            this.ControlPageInfo = new System.Windows.Forms.Label();
            this.TlpPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // HomePage
            // 
            this.HomePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HomePage.Location = new System.Drawing.Point(3, 3);
            this.HomePage.Name = "HomePage";
            this.HomePage.Size = new System.Drawing.Size(74, 29);
            this.HomePage.TabIndex = 0;
            this.HomePage.Text = "<<";
            this.HomePage.UseVisualStyleBackColor = true;
            this.HomePage.Click += new System.EventHandler(this.HomePage_Click);
            // 
            // PrevPage
            // 
            this.PrevPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrevPage.Location = new System.Drawing.Point(83, 3);
            this.PrevPage.Name = "PrevPage";
            this.PrevPage.Size = new System.Drawing.Size(74, 29);
            this.PrevPage.TabIndex = 1;
            this.PrevPage.Text = "<";
            this.PrevPage.UseVisualStyleBackColor = true;
            this.PrevPage.Click += new System.EventHandler(this.PrevPage_Click);
            // 
            // NextPage
            // 
            this.NextPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NextPage.Location = new System.Drawing.Point(363, 3);
            this.NextPage.Name = "NextPage";
            this.NextPage.Size = new System.Drawing.Size(74, 29);
            this.NextPage.TabIndex = 2;
            this.NextPage.Text = ">";
            this.NextPage.UseVisualStyleBackColor = true;
            this.NextPage.Click += new System.EventHandler(this.NextPage_Click);
            // 
            // LastPage
            // 
            this.LastPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LastPage.Location = new System.Drawing.Point(443, 3);
            this.LastPage.Name = "LastPage";
            this.LastPage.Size = new System.Drawing.Size(74, 29);
            this.LastPage.TabIndex = 3;
            this.LastPage.Text = ">>";
            this.LastPage.UseVisualStyleBackColor = true;
            this.LastPage.Click += new System.EventHandler(this.LastPage_Click);
            // 
            // JumpPageValue
            // 
            this.JumpPageValue.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.JumpPageValue.Location = new System.Drawing.Point(523, 3);
            this.JumpPageValue.Name = "JumpPageValue";
            this.JumpPageValue.Size = new System.Drawing.Size(74, 34);
            this.JumpPageValue.TabIndex = 4;
            this.JumpPageValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Jump
            // 
            this.Jump.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Jump.Location = new System.Drawing.Point(603, 3);
            this.Jump.Name = "Jump";
            this.Jump.Size = new System.Drawing.Size(74, 29);
            this.Jump.TabIndex = 5;
            this.Jump.Text = "跳转";
            this.Jump.UseVisualStyleBackColor = true;
            this.Jump.Click += new System.EventHandler(this.Jump_Click);
            // 
            // ControlRecordCount
            // 
            this.ControlRecordCount.AutoSize = true;
            this.ControlRecordCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlRecordCount.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ControlRecordCount.Location = new System.Drawing.Point(683, 0);
            this.ControlRecordCount.Name = "ControlRecordCount";
            this.ControlRecordCount.Size = new System.Drawing.Size(194, 35);
            this.ControlRecordCount.TabIndex = 6;
            this.ControlRecordCount.Text = "共0条数据";
            this.ControlRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmbLimits
            // 
            this.CmbLimits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbLimits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLimits.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbLimits.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CmbLimits.FormattingEnabled = true;
            this.CmbLimits.Location = new System.Drawing.Point(883, 3);
            this.CmbLimits.Name = "CmbLimits";
            this.CmbLimits.Size = new System.Drawing.Size(114, 35);
            this.CmbLimits.TabIndex = 7;
            this.CmbLimits.TabStop = false;
            this.CmbLimits.SelectedValueChanged += new System.EventHandler(this.Limits_SelectedValueChanged);
            // 
            // TlpPage
            // 
            this.TlpPage.ColumnCount = 10;
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.TlpPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TlpPage.Controls.Add(this.HomePage, 0, 0);
            this.TlpPage.Controls.Add(this.JumpPageValue, 5, 0);
            this.TlpPage.Controls.Add(this.CmbLimits, 8, 0);
            this.TlpPage.Controls.Add(this.PrevPage, 1, 0);
            this.TlpPage.Controls.Add(this.ControlRecordCount, 7, 0);
            this.TlpPage.Controls.Add(this.NextPage, 3, 0);
            this.TlpPage.Controls.Add(this.Jump, 6, 0);
            this.TlpPage.Controls.Add(this.LastPage, 4, 0);
            this.TlpPage.Controls.Add(this.ControlPageInfo, 2, 0);
            this.TlpPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TlpPage.Location = new System.Drawing.Point(0, 0);
            this.TlpPage.Name = "TlpPage";
            this.TlpPage.RowCount = 1;
            this.TlpPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TlpPage.Size = new System.Drawing.Size(1004, 35);
            this.TlpPage.TabIndex = 8;
            // 
            // ControlPageInfo
            // 
            this.ControlPageInfo.AutoSize = true;
            this.ControlPageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPageInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ControlPageInfo.Location = new System.Drawing.Point(163, 0);
            this.ControlPageInfo.Name = "ControlPageInfo";
            this.ControlPageInfo.Size = new System.Drawing.Size(194, 35);
            this.ControlPageInfo.TabIndex = 8;
            this.ControlPageInfo.Text = "1/1";
            this.ControlPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MTUC_Page_V1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.TlpPage);
            this.DoubleBuffered = true;
            this.Name = "MTUC_Page_V1";
            this.Size = new System.Drawing.Size(1004, 35);
            this.TlpPage.ResumeLayout(false);
            this.TlpPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HomePage;
        private System.Windows.Forms.Button PrevPage;
        private System.Windows.Forms.Button NextPage;
        private System.Windows.Forms.Button LastPage;
        private System.Windows.Forms.TextBox JumpPageValue;
        private System.Windows.Forms.Button Jump;
        private System.Windows.Forms.Label ControlRecordCount;
        private System.Windows.Forms.ComboBox CmbLimits;
        private System.Windows.Forms.TableLayoutPanel TlpPage;
        private System.Windows.Forms.Label ControlPageInfo;
    }
}
