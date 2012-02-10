namespace ECouponsPrinter
{
    partial class Info
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
            this.print = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // print
            // 
            this.print.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.print.Location = new System.Drawing.Point(34, 263);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(167, 71);
            this.print.TabIndex = 0;
            this.print.Text = "打印";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.button1_Click);
            // 
            // close
            // 
            this.close.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.close.Location = new System.Drawing.Point(350, 263);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(167, 71);
            this.close.TabIndex = 0;
            this.close.Text = "取消";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.labelInfo.Location = new System.Drawing.Point(38, 13);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(479, 228);
            this.labelInfo.TabIndex = 1;
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 357);
            this.ControlBox = false;
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.close);
            this.Controls.Add(this.print);
            this.Name = "Info";
            this.Text = "打印提示";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label labelInfo;
    }
}