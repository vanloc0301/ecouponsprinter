namespace ECouponsPrinter
{
    partial class Wait
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
            this.prcBar = new System.Windows.Forms.ProgressBar();
            this.Info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prcBar
            // 
            this.prcBar.ForeColor = System.Drawing.Color.Lime;
            this.prcBar.Location = new System.Drawing.Point(79, 113);
            this.prcBar.Name = "prcBar";
            this.prcBar.Size = new System.Drawing.Size(598, 41);
            this.prcBar.TabIndex = 0;
            // 
            // Info
            // 
            this.Info.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.Info.Location = new System.Drawing.Point(79, 161);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(432, 30);
            this.Info.TabIndex = 1;
            // 
            // Wait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 326);
            this.ControlBox = false;
            this.Controls.Add(this.Info);
            this.Controls.Add(this.prcBar);
            this.Name = "Wait";
            this.Text = "Wait";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prcBar;
        private System.Windows.Forms.Label Info;
    }
}