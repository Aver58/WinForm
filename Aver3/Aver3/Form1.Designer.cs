namespace Aver3
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.浏览器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iP工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regex工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spiderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu1,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(378, 25);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Menu1
            // 
            this.Menu1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(67, 21);
            this.Menu1.Text = "Menu(&F)";
            this.Menu1.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.浏览器ToolStripMenuItem,
            this.iP工具ToolStripMenuItem,
            this.cmdToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.regex工具ToolStripMenuItem,
            this.spiderToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.hackerToolStripMenuItem,
            this.aToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(67, 21);
            this.toolsToolStripMenuItem.Text = "Tools(&T)";
            // 
            // 浏览器ToolStripMenuItem
            // 
            this.浏览器ToolStripMenuItem.Name = "浏览器ToolStripMenuItem";
            this.浏览器ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.浏览器ToolStripMenuItem.Text = "浏览器";
            this.浏览器ToolStripMenuItem.Click += new System.EventHandler(this.浏览器ToolStripMenuItem_Click);
            // 
            // iP工具ToolStripMenuItem
            // 
            this.iP工具ToolStripMenuItem.Name = "iP工具ToolStripMenuItem";
            this.iP工具ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.iP工具ToolStripMenuItem.Text = "IP工具";
            this.iP工具ToolStripMenuItem.Click += new System.EventHandler(this.iP工具ToolStripMenuItem_Click);
            // 
            // cmdToolStripMenuItem
            // 
            this.cmdToolStripMenuItem.Name = "cmdToolStripMenuItem";
            this.cmdToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cmdToolStripMenuItem.Text = "cmd";
            this.cmdToolStripMenuItem.Click += new System.EventHandler(this.cmdToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            this.sortToolStripMenuItem.Click += new System.EventHandler(this.sortToolStripMenuItem_Click);
            // 
            // regex工具ToolStripMenuItem
            // 
            this.regex工具ToolStripMenuItem.Name = "regex工具ToolStripMenuItem";
            this.regex工具ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.regex工具ToolStripMenuItem.Text = "Regex工具";
            this.regex工具ToolStripMenuItem.Click += new System.EventHandler(this.regex工具ToolStripMenuItem_Click);
            // 
            // spiderToolStripMenuItem
            // 
            this.spiderToolStripMenuItem.Name = "spiderToolStripMenuItem";
            this.spiderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spiderToolStripMenuItem.Text = "Spider";
            this.spiderToolStripMenuItem.Click += new System.EventHandler(this.spiderToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // hackerToolStripMenuItem
            // 
            this.hackerToolStripMenuItem.Name = "hackerToolStripMenuItem";
            this.hackerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hackerToolStripMenuItem.Text = "hacker";
            this.hackerToolStripMenuItem.Click += new System.EventHandler(this.hackerToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(222, 160);
            this.listBox1.TabIndex = 22;
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aToolStripMenuItem.Text = "A*";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(378, 297);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Aver";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Main_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Menu1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 浏览器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem iP工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regex工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spiderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hackerToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
    }
}

