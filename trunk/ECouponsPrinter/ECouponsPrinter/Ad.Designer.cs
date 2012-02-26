namespace ECouponsPrinter
{
    partial class Ad
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
            this.Panel_Ad = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Panel_Ad
            // 
            this.Panel_Ad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Panel_Ad.Location = new System.Drawing.Point(0, 0);
            this.Panel_Ad.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Ad.Name = "Panel_Ad";
            this.Panel_Ad.Size = new System.Drawing.Size(768, 1265);
            this.Panel_Ad.TabIndex = 20;
            // 
            // Ad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(829, 746);
            this.ControlBox = false;
            this.Controls.Add(this.Panel_Ad);
            this.Name = "Ad";
            this.Text = "Ad";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Ad_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ad_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Ad;
    }
}