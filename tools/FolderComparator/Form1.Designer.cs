namespace FolderComparator
{
    partial class DirCompare
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tPath1 = new System.Windows.Forms.TextBox();
            this.tPath2 = new System.Windows.Forms.TextBox();
            this.btnPath1 = new System.Windows.Forms.Button();
            this.btnPath2 = new System.Windows.Forms.Button();
            this.lPath1 = new System.Windows.Forms.Label();
            this.lPath2 = new System.Windows.Forms.Label();
            this.tvPath1 = new System.Windows.Forms.TreeView();
            this.tvPath2 = new System.Windows.Forms.TreeView();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnClearPath = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.btnShowList = new System.Windows.Forms.Button();
            this.chkHiddenSame = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tPath1
            // 
            this.tPath1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPath1.Location = new System.Drawing.Point(65, 14);
            this.tPath1.Name = "tPath1";
            this.tPath1.Size = new System.Drawing.Size(663, 21);
            this.tPath1.TabIndex = 0;
            // 
            // tPath2
            // 
            this.tPath2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPath2.Location = new System.Drawing.Point(65, 43);
            this.tPath2.Name = "tPath2";
            this.tPath2.Size = new System.Drawing.Size(663, 21);
            this.tPath2.TabIndex = 1;
            // 
            // btnPath1
            // 
            this.btnPath1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPath1.Location = new System.Drawing.Point(734, 12);
            this.btnPath1.Name = "btnPath1";
            this.btnPath1.Size = new System.Drawing.Size(23, 23);
            this.btnPath1.TabIndex = 2;
            this.btnPath1.UseVisualStyleBackColor = true;
            this.btnPath1.Click += new System.EventHandler(this.btnPath1_Click);
            // 
            // btnPath2
            // 
            this.btnPath2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPath2.Location = new System.Drawing.Point(734, 41);
            this.btnPath2.Name = "btnPath2";
            this.btnPath2.Size = new System.Drawing.Size(23, 23);
            this.btnPath2.TabIndex = 3;
            this.btnPath2.UseVisualStyleBackColor = true;
            this.btnPath2.Click += new System.EventHandler(this.btnPath2_Click);
            // 
            // lPath1
            // 
            this.lPath1.AutoSize = true;
            this.lPath1.Location = new System.Drawing.Point(12, 17);
            this.lPath1.Name = "lPath1";
            this.lPath1.Size = new System.Drawing.Size(47, 12);
            this.lPath1.TabIndex = 4;
            this.lPath1.Text = "路径1：";
            // 
            // lPath2
            // 
            this.lPath2.AutoSize = true;
            this.lPath2.Location = new System.Drawing.Point(12, 46);
            this.lPath2.Name = "lPath2";
            this.lPath2.Size = new System.Drawing.Size(47, 12);
            this.lPath2.TabIndex = 5;
            this.lPath2.Text = "路径2：";
            // 
            // tvPath1
            // 
            this.tvPath1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvPath1.Location = new System.Drawing.Point(12, 100);
            this.tvPath1.Name = "tvPath1";
            this.tvPath1.Size = new System.Drawing.Size(370, 371);
            this.tvPath1.TabIndex = 6;
            // 
            // tvPath2
            // 
            this.tvPath2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvPath2.Location = new System.Drawing.Point(387, 100);
            this.tvPath2.Name = "tvPath2";
            this.tvPath2.Size = new System.Drawing.Size(370, 371);
            this.tvPath2.TabIndex = 7;
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompare.Location = new System.Drawing.Point(682, 70);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 9;
            this.btnCompare.Text = "开始比较";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnClearPath
            // 
            this.btnClearPath.Location = new System.Drawing.Point(85, 70);
            this.btnClearPath.Name = "btnClearPath";
            this.btnClearPath.Size = new System.Drawing.Size(75, 23);
            this.btnClearPath.TabIndex = 10;
            this.btnClearPath.Text = "清空路径";
            this.btnClearPath.UseVisualStyleBackColor = true;
            this.btnClearPath.Click += new System.EventHandler(this.btnClearPath_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadConfig.Location = new System.Drawing.Point(520, 70);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(75, 23);
            this.btnLoadConfig.TabIndex = 11;
            this.btnLoadConfig.Text = "加载配置";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // btnShowList
            // 
            this.btnShowList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowList.Location = new System.Drawing.Point(601, 70);
            this.btnShowList.Name = "btnShowList";
            this.btnShowList.Size = new System.Drawing.Size(75, 23);
            this.btnShowList.TabIndex = 12;
            this.btnShowList.Text = "列出明细";
            this.btnShowList.UseVisualStyleBackColor = true;
            this.btnShowList.Click += new System.EventHandler(this.btnShowList_Click);
            // 
            // chkHiddenSame
            // 
            this.chkHiddenSame.AutoSize = true;
            this.chkHiddenSame.Location = new System.Drawing.Point(12, 74);
            this.chkHiddenSame.Name = "chkHiddenSame";
            this.chkHiddenSame.Size = new System.Drawing.Size(72, 16);
            this.chkHiddenSame.TabIndex = 8;
            this.chkHiddenSame.Text = "隐藏相同";
            this.chkHiddenSame.UseVisualStyleBackColor = true;
            // 
            // DirCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 482);
            this.Controls.Add(this.btnShowList);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.btnClearPath);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.chkHiddenSame);
            this.Controls.Add(this.tvPath2);
            this.Controls.Add(this.tvPath1);
            this.Controls.Add(this.lPath2);
            this.Controls.Add(this.lPath1);
            this.Controls.Add(this.btnPath2);
            this.Controls.Add(this.btnPath1);
            this.Controls.Add(this.tPath2);
            this.Controls.Add(this.tPath1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DirCompare";
            this.Text = "文件夹比较工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tPath1;
        private System.Windows.Forms.TextBox tPath2;
        private System.Windows.Forms.Button btnPath1;
        private System.Windows.Forms.Button btnPath2;
        private System.Windows.Forms.Label lPath1;
        private System.Windows.Forms.Label lPath2;
        private System.Windows.Forms.TreeView tvPath1;
        private System.Windows.Forms.TreeView tvPath2;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnClearPath;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.Button btnShowList;
        private System.Windows.Forms.CheckBox chkHiddenSame;
    }
}

