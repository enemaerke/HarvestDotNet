namespace HarvestDotNet.TestHarness
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_textUsername = new System.Windows.Forms.TextBox();
            this.m_textUrl = new System.Windows.Forms.TextBox();
            this.m_textPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.m_comboMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_textMessage = new System.Windows.Forms.TextBox();
            this.m_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // m_textUsername
            // 
            this.m_textUsername.Location = new System.Drawing.Point(73, 6);
            this.m_textUsername.Name = "m_textUsername";
            this.m_textUsername.Size = new System.Drawing.Size(186, 20);
            this.m_textUsername.TabIndex = 1;
            // 
            // m_textUrl
            // 
            this.m_textUrl.Location = new System.Drawing.Point(73, 32);
            this.m_textUrl.Name = "m_textUrl";
            this.m_textUrl.Size = new System.Drawing.Size(402, 20);
            this.m_textUrl.TabIndex = 2;
            // 
            // m_textPassword
            // 
            this.m_textPassword.Location = new System.Drawing.Point(326, 6);
            this.m_textPassword.Name = "m_textPassword";
            this.m_textPassword.PasswordChar = '*';
            this.m_textPassword.Size = new System.Drawing.Size(149, 20);
            this.m_textPassword.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Url";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(513, 35);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(149, 23);
            this.buttonRun.TabIndex = 6;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // m_comboMethod
            // 
            this.m_comboMethod.FormattingEnabled = true;
            this.m_comboMethod.Location = new System.Drawing.Point(571, 6);
            this.m_comboMethod.Name = "m_comboMethod";
            this.m_comboMethod.Size = new System.Drawing.Size(121, 21);
            this.m_comboMethod.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(510, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Method";
            // 
            // m_textMessage
            // 
            this.m_textMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textMessage.Location = new System.Drawing.Point(15, 77);
            this.m_textMessage.Multiline = true;
            this.m_textMessage.Name = "m_textMessage";
            this.m_textMessage.Size = new System.Drawing.Size(677, 295);
            this.m_textMessage.TabIndex = 9;
            // 
            // m_backgroundWorker
            // 
            this.m_backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_backgroundWorker_DoWork);
            this.m_backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_backgroundWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 384);
            this.Controls.Add(this.m_textMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_comboMethod);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_textPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_textUrl);
            this.Controls.Add(this.m_textUsername);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_textUsername;
        private System.Windows.Forms.TextBox m_textUrl;
        private System.Windows.Forms.TextBox m_textPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.ComboBox m_comboMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_textMessage;
        private System.ComponentModel.BackgroundWorker m_backgroundWorker;
    }
}