namespace CipherApp
{
    partial class Form1
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
            this.lbCiphers = new System.Windows.Forms.ListBox();
            this.tbOutputText = new System.Windows.Forms.TextBox();
            this.tbInputText = new System.Windows.Forms.TextBox();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.labelKey = new System.Windows.Forms.Label();
            this.labelInputText = new System.Windows.Forms.Label();
            this.bEncrypt = new System.Windows.Forms.Button();
            this.bDecrypt = new System.Windows.Forms.Button();
            this.labelOutputText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbCiphers
            // 
            this.lbCiphers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCiphers.FormattingEnabled = true;
            this.lbCiphers.ItemHeight = 16;
            this.lbCiphers.Location = new System.Drawing.Point(12, 12);
            this.lbCiphers.Name = "lbCiphers";
            this.lbCiphers.Size = new System.Drawing.Size(169, 180);
            this.lbCiphers.TabIndex = 0;
            // 
            // tbOutputText
            // 
            this.tbOutputText.Location = new System.Drawing.Point(265, 242);
            this.tbOutputText.Multiline = true;
            this.tbOutputText.Name = "tbOutputText";
            this.tbOutputText.ReadOnly = true;
            this.tbOutputText.Size = new System.Drawing.Size(430, 95);
            this.tbOutputText.TabIndex = 1;
            // 
            // tbInputText
            // 
            this.tbInputText.Location = new System.Drawing.Point(265, 41);
            this.tbInputText.Multiline = true;
            this.tbInputText.Name = "tbInputText";
            this.tbInputText.Size = new System.Drawing.Size(430, 72);
            this.tbInputText.TabIndex = 1;
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(265, 12);
            this.tbKey.Multiline = true;
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(86, 23);
            this.tbKey.TabIndex = 1;
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKey.Location = new System.Drawing.Point(225, 13);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(34, 16);
            this.labelKey.TabIndex = 3;
            this.labelKey.Text = "Key:";
            this.labelKey.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelInputText
            // 
            this.labelInputText.AutoSize = true;
            this.labelInputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInputText.Location = new System.Drawing.Point(191, 41);
            this.labelInputText.Name = "labelInputText";
            this.labelInputText.Size = new System.Drawing.Size(68, 16);
            this.labelInputText.TabIndex = 3;
            this.labelInputText.Text = "Input Text:";
            this.labelInputText.Click += new System.EventHandler(this.label1_Click);
            // 
            // bEncrypt
            // 
            this.bEncrypt.Location = new System.Drawing.Point(346, 156);
            this.bEncrypt.Name = "bEncrypt";
            this.bEncrypt.Size = new System.Drawing.Size(101, 36);
            this.bEncrypt.TabIndex = 2;
            this.bEncrypt.Text = "Encrypt";
            this.bEncrypt.UseVisualStyleBackColor = true;
            this.bEncrypt.Click += new System.EventHandler(this.bEncrypt_Click);
            // 
            // bDecrypt
            // 
            this.bDecrypt.Location = new System.Drawing.Point(503, 156);
            this.bDecrypt.Name = "bDecrypt";
            this.bDecrypt.Size = new System.Drawing.Size(101, 36);
            this.bDecrypt.TabIndex = 2;
            this.bDecrypt.Text = "Decrypt";
            this.bDecrypt.UseVisualStyleBackColor = true;
            this.bDecrypt.Click += new System.EventHandler(this.bDecrypt_Click);
            // 
            // labelOutputText
            // 
            this.labelOutputText.AutoSize = true;
            this.labelOutputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutputText.Location = new System.Drawing.Point(181, 243);
            this.labelOutputText.Name = "labelOutputText";
            this.labelOutputText.Size = new System.Drawing.Size(78, 16);
            this.labelOutputText.TabIndex = 3;
            this.labelOutputText.Text = "Output Text:";
            this.labelOutputText.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 354);
            this.Controls.Add(this.labelOutputText);
            this.Controls.Add(this.labelInputText);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.bDecrypt);
            this.Controls.Add(this.bEncrypt);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.tbInputText);
            this.Controls.Add(this.tbOutputText);
            this.Controls.Add(this.lbCiphers);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbCiphers;
        private System.Windows.Forms.TextBox tbOutputText;
        private System.Windows.Forms.TextBox tbInputText;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelInputText;
        private System.Windows.Forms.Button bEncrypt;
        private System.Windows.Forms.Button bDecrypt;
        private System.Windows.Forms.Label labelOutputText;
    }
}

