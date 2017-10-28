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
            this.button1 = new System.Windows.Forms.Button();
            this.angel = new System.Windows.Forms.Label();
            this.level = new System.Windows.Forms.Label();
            this.maxLevel = new System.Windows.Forms.Label();
            this.maxAngle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(151, 278);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // angel
            // 
            this.angel.AutoSize = true;
            this.angel.Location = new System.Drawing.Point(12, 9);
            this.angel.Name = "angel";
            this.angel.Size = new System.Drawing.Size(41, 12);
            this.angel.TabIndex = 3;
            this.angel.Text = "label1";
            // 
            // level
            // 
            this.level.AutoSize = true;
            this.level.Location = new System.Drawing.Point(12, 68);
            this.level.Name = "level";
            this.level.Size = new System.Drawing.Size(41, 12);
            this.level.TabIndex = 4;
            this.level.Text = "label1";
            // 
            // maxLevel
            // 
            this.maxLevel.AutoSize = true;
            this.maxLevel.Location = new System.Drawing.Point(12, 125);
            this.maxLevel.Name = "maxLevel";
            this.maxLevel.Size = new System.Drawing.Size(41, 12);
            this.maxLevel.TabIndex = 5;
            this.maxLevel.Text = "label1";
            // 
            // maxAngle
            // 
            this.maxAngle.AutoSize = true;
            this.maxAngle.Location = new System.Drawing.Point(12, 185);
            this.maxAngle.Name = "maxAngle";
            this.maxAngle.Size = new System.Drawing.Size(41, 12);
            this.maxAngle.TabIndex = 6;
            this.maxAngle.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 42);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(316, 204);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "";
          
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 313);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxAngle);
            this.Controls.Add(this.maxLevel);
            this.Controls.Add(this.level);
            this.Controls.Add(this.angel);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label angel;
        private System.Windows.Forms.Label level;
        private System.Windows.Forms.Label maxLevel;
        private System.Windows.Forms.Label maxAngle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

