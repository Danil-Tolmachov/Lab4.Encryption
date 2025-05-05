namespace Lab4.MainForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            txtInputFile = new TextBox();
            btnBrowseInput = new Button();
            label2 = new Label();
            txtOutputFile = new TextBox();
            btnBrowseOutput = new Button();
            label3 = new Label();
            txtPassword = new TextBox();
            btnEncrypt = new Button();
            btnDecrypt = new Button();
            progressBar = new ProgressBar();
            lblProgress = new Label();
            lblStatus = new Label();
            lblTimer = new Label();
            btnPause = new Button();
            btnResume = new Button();
            btnCancel = new Button();
            uiTimer = new System.Windows.Forms.Timer(components);
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(155, 68);
            label1.Name = "label1";
            label1.Size = new Size(53, 13);
            label1.TabIndex = 0;
            label1.Text = "Input File:";
            // 
            // txtInputFile
            // 
            txtInputFile.Location = new Point(50, 93);
            txtInputFile.Name = "txtInputFile";
            txtInputFile.Size = new Size(272, 22);
            txtInputFile.TabIndex = 1;
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(344, 84);
            btnBrowseInput.Name = "btnBrowseInput";
            btnBrowseInput.Size = new Size(95, 37);
            btnBrowseInput.TabIndex = 2;
            btnBrowseInput.Text = "Browse...";
            btnBrowseInput.UseVisualStyleBackColor = true;
            btnBrowseInput.Click += btnBrowseInput_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(660, 68);
            label2.Name = "label2";
            label2.Size = new Size(61, 13);
            label2.TabIndex = 3;
            label2.Text = "Output File:";
            // 
            // txtOutputFile
            // 
            txtOutputFile.Location = new Point(551, 93);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(272, 22);
            txtOutputFile.TabIndex = 4;
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(845, 84);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(95, 37);
            btnBrowseOutput.TabIndex = 5;
            btnBrowseOutput.Text = "Browse...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(450, 225);
            label3.Name = "label3";
            label3.Size = new Size(56, 13);
            label3.TabIndex = 6;
            label3.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(344, 252);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(272, 22);
            txtPassword.TabIndex = 7;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Location = new Point(364, 310);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(95, 37);
            btnEncrypt.TabIndex = 8;
            btnEncrypt.Text = "Encrypt";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Location = new Point(486, 310);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(95, 37);
            btnDecrypt.TabIndex = 9;
            btnDecrypt.Text = "Decrypt";
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(89, 430);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(805, 20);
            progressBar.TabIndex = 10;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.BackColor = Color.Gainsboro;
            lblProgress.Location = new Point(450, 434);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(68, 13);
            lblProgress.TabIndex = 11;
            lblProgress.Text = "Progress: 0%";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(315, 405);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(60, 13);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "Status: Idle";
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(565, 405);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(93, 13);
            lblTimer.TabIndex = 13;
            lblTimer.Text = "Elapsed: 00:00:00";
            // 
            // btnPause
            // 
            btnPause.Enabled = false;
            btnPause.Location = new Point(227, 494);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(95, 37);
            btnPause.TabIndex = 14;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnResume
            // 
            btnResume.Enabled = false;
            btnResume.Location = new Point(439, 494);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(95, 37);
            btnResume.TabIndex = 15;
            btnResume.Text = "Resume";
            btnResume.UseVisualStyleBackColor = true;
            btnResume.Click += btnResume_Click;
            // 
            // btnCancel
            // 
            btnCancel.Enabled = false;
            btnCancel.Location = new Point(642, 494);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(95, 37);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // uiTimer
            // 
            uiTimer.Tick += uiTimer_Tick;
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(970, 594);
            Controls.Add(btnCancel);
            Controls.Add(btnResume);
            Controls.Add(btnPause);
            Controls.Add(lblTimer);
            Controls.Add(lblStatus);
            Controls.Add(lblProgress);
            Controls.Add(progressBar);
            Controls.Add(btnDecrypt);
            Controls.Add(btnEncrypt);
            Controls.Add(txtPassword);
            Controls.Add(label3);
            Controls.Add(btnBrowseOutput);
            Controls.Add(txtOutputFile);
            Controls.Add(label2);
            Controls.Add(btnBrowseInput);
            Controls.Add(txtInputFile);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "File - Encryptor / Decryptor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Timer uiTimer;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
