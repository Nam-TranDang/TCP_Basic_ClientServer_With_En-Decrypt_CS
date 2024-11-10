namespace TCPChatClientServerGUI
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
            this.radClient = new System.Windows.Forms.RadioButton();
            this.radServer = new System.Windows.Forms.RadioButton();
            this.butOK = new System.Windows.Forms.Button();
            this.butThoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radClient
            // 
            this.radClient.AutoSize = true;
            this.radClient.Checked = true;
            this.radClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radClient.Location = new System.Drawing.Point(56, 28);
            this.radClient.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radClient.Name = "radClient";
            this.radClient.Size = new System.Drawing.Size(172, 24);
            this.radClient.TabIndex = 0;
            this.radClient.TabStop = true;
            this.radClient.Text = "Chay o che do Client";
            this.radClient.UseVisualStyleBackColor = true;
            // 
            // radServer
            // 
            this.radServer.AutoSize = true;
            this.radServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radServer.Location = new System.Drawing.Point(56, 59);
            this.radServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radServer.Name = "radServer";
            this.radServer.Size = new System.Drawing.Size(178, 24);
            this.radServer.TabIndex = 1;
            this.radServer.Text = "Chay o che do Server";
            this.radServer.UseVisualStyleBackColor = true;
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(36, 136);
            this.butOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(88, 35);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butThoat
            // 
            this.butThoat.Location = new System.Drawing.Point(162, 133);
            this.butThoat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butThoat.Name = "butThoat";
            this.butThoat.Size = new System.Drawing.Size(83, 37);
            this.butThoat.TabIndex = 3;
            this.butThoat.Text = "Thoat";
            this.butThoat.UseVisualStyleBackColor = true;
            this.butThoat.Click += new System.EventHandler(this.butThoat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(12, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "Phạm Thanh Trúc - 22H1120055";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 227);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butThoat);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.radServer);
            this.Controls.Add(this.radClient);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Chon che do chay Client/Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radClient;
        private System.Windows.Forms.RadioButton radServer;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butThoat;
        private System.Windows.Forms.Label label1;
    }
}

