namespace Mem_Show
{
    partial class FormMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIconMS = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBoot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconMS
            // 
            this.notifyIconMS.ContextMenuStrip = this.contextMenuStripMS;
            this.notifyIconMS.Text = "可用内存";
            this.notifyIconMS.Visible = true;
            this.notifyIconMS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMS_MouseDoubleClick);
            // 
            // contextMenuStripMS
            // 
            this.contextMenuStripMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTheme,
            this.tsmiBoot,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.contextMenuStripMS.Name = "contextMenuStripMS";
            this.contextMenuStripMS.Size = new System.Drawing.Size(101, 76);
            // 
            // tsmiTheme
            // 
            this.tsmiTheme.Checked = true;
            this.tsmiTheme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiTheme.Name = "tsmiTheme";
            this.tsmiTheme.Size = new System.Drawing.Size(100, 22);
            this.tsmiTheme.Text = "暗夜";
            this.tsmiTheme.Click += new System.EventHandler(this.tsmiTheme_Click);
            // 
            // tsmiBoot
            // 
            this.tsmiBoot.Name = "tsmiBoot";
            this.tsmiBoot.Size = new System.Drawing.Size(100, 22);
            this.tsmiBoot.Text = "自启";
            this.tsmiBoot.Click += new System.EventHandler(this.tsmiBoot_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(100, 22);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "奇迹银巧";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Violet;
            this.ClientSize = new System.Drawing.Size(212, 149);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "内存显示";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.formMain_Load);
            this.contextMenuStripMS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconMS;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMS;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem tsmiTheme;
        private System.Windows.Forms.ToolStripMenuItem tsmiBoot;
    }
}

