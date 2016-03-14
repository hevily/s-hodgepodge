namespace IISJournalAnalyzer
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtLogFile1 = new System.Windows.Forms.TextBox();
            this.btnSelectLogFile1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogFile2 = new System.Windows.Forms.TextBox();
            this.btnSelectLogFile2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRequestType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRequestUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUrlParams = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtClientIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUserAgent = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtDestRows = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日志文件1：";
            // 
            // txtLogFile1
            // 
            this.txtLogFile1.Location = new System.Drawing.Point(89, 6);
            this.txtLogFile1.Name = "txtLogFile1";
            this.txtLogFile1.Size = new System.Drawing.Size(318, 21);
            this.txtLogFile1.TabIndex = 1;
            // 
            // btnSelectLogFile1
            // 
            this.btnSelectLogFile1.Location = new System.Drawing.Point(413, 4);
            this.btnSelectLogFile1.Name = "btnSelectLogFile1";
            this.btnSelectLogFile1.Size = new System.Drawing.Size(75, 23);
            this.btnSelectLogFile1.TabIndex = 2;
            this.btnSelectLogFile1.Text = "选择";
            this.btnSelectLogFile1.UseVisualStyleBackColor = true;
            this.btnSelectLogFile1.Click += new System.EventHandler(this.btnSelectLogFile1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "日志文件2：";
            // 
            // txtLogFile2
            // 
            this.txtLogFile2.Location = new System.Drawing.Point(89, 33);
            this.txtLogFile2.Name = "txtLogFile2";
            this.txtLogFile2.Size = new System.Drawing.Size(318, 21);
            this.txtLogFile2.TabIndex = 1;
            // 
            // btnSelectLogFile2
            // 
            this.btnSelectLogFile2.Location = new System.Drawing.Point(413, 31);
            this.btnSelectLogFile2.Name = "btnSelectLogFile2";
            this.btnSelectLogFile2.Size = new System.Drawing.Size(75, 23);
            this.btnSelectLogFile2.TabIndex = 2;
            this.btnSelectLogFile2.Text = "选择";
            this.btnSelectLogFile2.UseVisualStyleBackColor = true;
            this.btnSelectLogFile2.Click += new System.EventHandler(this.btnSelectLogFile2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtUserAgent);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtClientIP);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtServerIP);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtUrlParams);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtRequestUrl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtRequestType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(794, 98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检索条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "请求方式：";
            // 
            // txtRequestType
            // 
            this.txtRequestType.Location = new System.Drawing.Point(75, 14);
            this.txtRequestType.Name = "txtRequestType";
            this.txtRequestType.Size = new System.Drawing.Size(318, 21);
            this.txtRequestType.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "请求地址：";
            // 
            // txtRequestUrl
            // 
            this.txtRequestUrl.Location = new System.Drawing.Point(75, 41);
            this.txtRequestUrl.Name = "txtRequestUrl";
            this.txtRequestUrl.Size = new System.Drawing.Size(318, 21);
            this.txtRequestUrl.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "查询参数：";
            // 
            // txtUrlParams
            // 
            this.txtUrlParams.Location = new System.Drawing.Point(75, 68);
            this.txtUrlParams.Name = "txtUrlParams";
            this.txtUrlParams.Size = new System.Drawing.Size(318, 21);
            this.txtUrlParams.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(401, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "服务器IP：";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(470, 14);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(318, 21);
            this.txtServerIP.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(401, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "客户端IP：";
            // 
            // txtClientIP
            // 
            this.txtClientIP.Location = new System.Drawing.Point(470, 41);
            this.txtClientIP.Name = "txtClientIP";
            this.txtClientIP.Size = new System.Drawing.Size(318, 21);
            this.txtClientIP.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(419, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Agent：";
            // 
            // txtUserAgent
            // 
            this.txtUserAgent.Location = new System.Drawing.Point(470, 68);
            this.txtUserAgent.Name = "txtUserAgent";
            this.txtUserAgent.Size = new System.Drawing.Size(318, 21);
            this.txtUserAgent.TabIndex = 1;
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.Location = new System.Drawing.Point(378, 164);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 4;
            this.btnScan.Text = "分析日志";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtDestRows
            // 
            this.txtDestRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestRows.Location = new System.Drawing.Point(14, 196);
            this.txtDestRows.Multiline = true;
            this.txtDestRows.Name = "txtDestRows";
            this.txtDestRows.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDestRows.Size = new System.Drawing.Size(794, 419);
            this.txtDestRows.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 627);
            this.Controls.Add(this.txtDestRows);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectLogFile2);
            this.Controls.Add(this.btnSelectLogFile1);
            this.Controls.Add(this.txtLogFile2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLogFile1);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IIS日志分析";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLogFile1;
        private System.Windows.Forms.Button btnSelectLogFile1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogFile2;
        private System.Windows.Forms.Button btnSelectLogFile2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRequestType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUrlParams;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRequestUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClientIP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserAgent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtDestRows;
    }
}

