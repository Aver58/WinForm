namespace Aver3
{
    partial class Cmd
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
            this.angel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.command = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.param = new System.Windows.Forms.TextBox();
            this.Screen = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // angel
            // 
            this.angel.AutoSize = true;
            this.angel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.angel.Location = new System.Drawing.Point(226, 9);
            this.angel.Name = "angel";
            this.angel.Size = new System.Drawing.Size(109, 25);
            this.angel.TabIndex = 4;
            this.angel.Text = "cmd模拟器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "Command";
            // 
            // command
            // 
            this.command.Location = new System.Drawing.Point(74, 50);
            this.command.Multiline = true;
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(111, 23);
            this.command.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(191, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "param";
            // 
            // param
            // 
            this.param.Location = new System.Drawing.Point(239, 50);
            this.param.Multiline = true;
            this.param.Name = "param";
            this.param.Size = new System.Drawing.Size(90, 23);
            this.param.TabIndex = 18;
            // 
            // Screen
            // 
            this.Screen.Location = new System.Drawing.Point(12, 79);
            this.Screen.Multiline = true;
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(401, 210);
            this.Screen.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(335, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 299);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Screen);
            this.Controls.Add(this.param);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.command);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.angel);
            this.Name = "Cmd";
            this.Text = "Cmd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label angel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox command;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox param;
        private System.Windows.Forms.TextBox Screen;
        private System.Windows.Forms.Button button1;
    }
}