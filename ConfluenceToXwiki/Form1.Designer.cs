namespace ConfluenceToXwiki
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
            this.components = new System.ComponentModel.Container();
            this.uxURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uxNavigate = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.uxConvert = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // uxURL
            // 
            this.uxURL.Location = new System.Drawing.Point(55, 10);
            this.uxURL.Name = "uxURL";
            this.uxURL.Size = new System.Drawing.Size(573, 20);
            this.uxURL.TabIndex = 0;
            this.uxURL.Text = "http://kb.printaudit.com/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL:";
            // 
            // uxNavigate
            // 
            this.uxNavigate.Location = new System.Drawing.Point(634, 8);
            this.uxNavigate.Name = "uxNavigate";
            this.uxNavigate.Size = new System.Drawing.Size(75, 23);
            this.uxNavigate.TabIndex = 2;
            this.uxNavigate.Text = "Navigate";
            this.uxNavigate.UseVisualStyleBackColor = true;
            this.uxNavigate.Click += new System.EventHandler(this.uxNavigate_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(17, 40);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1247, 875);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // uxConvert
            // 
            this.uxConvert.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uxConvert.Location = new System.Drawing.Point(1189, 8);
            this.uxConvert.Name = "uxConvert";
            this.uxConvert.Size = new System.Drawing.Size(75, 23);
            this.uxConvert.TabIndex = 4;
            this.uxConvert.Text = "Convert";
            this.uxConvert.UseVisualStyleBackColor = true;
            this.uxConvert.Click += new System.EventHandler(this.uxConvert_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 927);
            this.Controls.Add(this.uxConvert);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.uxNavigate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxURL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uxURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uxNavigate;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button uxConvert;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

