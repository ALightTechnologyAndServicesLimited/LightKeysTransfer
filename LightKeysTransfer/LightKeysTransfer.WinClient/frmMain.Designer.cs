namespace LightKeysTransfer.WinClient
{
    partial class frmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnGenerateKeyPair = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCopyPublicKey = new System.Windows.Forms.Button();
            this.btnSendPublicKey = new System.Windows.Forms.Button();
            this.txtEncrypted = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnSendPlainText = new System.Windows.Forms.Button();
            this.btnCopyPlainText = new System.Windows.Forms.Button();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnClearClipBoard = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(25, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(647, 382);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClearClipBoard);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.btnSendPlainText);
            this.tabPage1.Controls.Add(this.btnCopyPlainText);
            this.tabPage1.Controls.Add(this.btnDecrypt);
            this.tabPage1.Controls.Add(this.txtEncrypted);
            this.tabPage1.Controls.Add(this.btnSendPublicKey);
            this.tabPage1.Controls.Add(this.btnCopyPublicKey);
            this.tabPage1.Controls.Add(this.btnGenerateKeyPair);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(639, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnGenerateKeyPair
            // 
            this.btnGenerateKeyPair.Location = new System.Drawing.Point(6, 16);
            this.btnGenerateKeyPair.Name = "btnGenerateKeyPair";
            this.btnGenerateKeyPair.Size = new System.Drawing.Size(177, 23);
            this.btnGenerateKeyPair.TabIndex = 1;
            this.btnGenerateKeyPair.Text = "Generate KeyPair";
            this.btnGenerateKeyPair.UseVisualStyleBackColor = true;
            this.btnGenerateKeyPair.Click += new System.EventHandler(this.btnGenerateKeyPair_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCopyPublicKey
            // 
            this.btnCopyPublicKey.Location = new System.Drawing.Point(16, 57);
            this.btnCopyPublicKey.Name = "btnCopyPublicKey";
            this.btnCopyPublicKey.Size = new System.Drawing.Size(75, 23);
            this.btnCopyPublicKey.TabIndex = 2;
            this.btnCopyPublicKey.Text = "Copy";
            this.btnCopyPublicKey.UseVisualStyleBackColor = true;
            this.btnCopyPublicKey.Click += new System.EventHandler(this.btnCopyPublicKey_Click);
            // 
            // btnSendPublicKey
            // 
            this.btnSendPublicKey.Location = new System.Drawing.Point(108, 57);
            this.btnSendPublicKey.Name = "btnSendPublicKey";
            this.btnSendPublicKey.Size = new System.Drawing.Size(75, 23);
            this.btnSendPublicKey.TabIndex = 3;
            this.btnSendPublicKey.Text = "Send";
            this.btnSendPublicKey.UseVisualStyleBackColor = true;
            this.btnSendPublicKey.Click += new System.EventHandler(this.btnSendPublicKey_Click);
            // 
            // txtEncrypted
            // 
            this.txtEncrypted.Location = new System.Drawing.Point(16, 98);
            this.txtEncrypted.Name = "txtEncrypted";
            this.txtEncrypted.PasswordChar = '*';
            this.txtEncrypted.Size = new System.Drawing.Size(167, 20);
            this.txtEncrypted.TabIndex = 4;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(16, 138);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(167, 23);
            this.btnDecrypt.TabIndex = 5;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnSendPlainText
            // 
            this.btnSendPlainText.Location = new System.Drawing.Point(108, 185);
            this.btnSendPlainText.Name = "btnSendPlainText";
            this.btnSendPlainText.Size = new System.Drawing.Size(75, 23);
            this.btnSendPlainText.TabIndex = 7;
            this.btnSendPlainText.Text = "Send";
            this.btnSendPlainText.UseVisualStyleBackColor = true;
            this.btnSendPlainText.Click += new System.EventHandler(this.btnSendPlainText_Click);
            // 
            // btnCopyPlainText
            // 
            this.btnCopyPlainText.Location = new System.Drawing.Point(16, 185);
            this.btnCopyPlainText.Name = "btnCopyPlainText";
            this.btnCopyPlainText.Size = new System.Drawing.Size(75, 23);
            this.btnCopyPlainText.TabIndex = 6;
            this.btnCopyPlainText.Text = "Copy";
            this.btnCopyPlainText.UseVisualStyleBackColor = true;
            this.btnCopyPlainText.Click += new System.EventHandler(this.btnCopyPlainText_Click);
            // 
            // tmr
            // 
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(220, 16);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(169, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // btnClearClipBoard
            // 
            this.btnClearClipBoard.Location = new System.Drawing.Point(220, 57);
            this.btnClearClipBoard.Name = "btnClearClipBoard";
            this.btnClearClipBoard.Size = new System.Drawing.Size(169, 23);
            this.btnClearClipBoard.TabIndex = 8;
            this.btnClearClipBoard.Text = "Clear Now";
            this.btnClearClipBoard.UseVisualStyleBackColor = true;
            this.btnClearClipBoard.Click += new System.EventHandler(this.btnClearClipBoard_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 422);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "WinClient";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnGenerateKeyPair;
        private System.Windows.Forms.Button btnCopyPublicKey;
        private System.Windows.Forms.Button btnSendPublicKey;
        private System.Windows.Forms.TextBox txtEncrypted;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnSendPlainText;
        private System.Windows.Forms.Button btnCopyPlainText;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Button btnClearClipBoard;
    }
}

