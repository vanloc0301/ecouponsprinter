namespace ECouponsPrinter
{
    partial class TranslateForm
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
            this.SCardTimer = new System.Windows.Forms.Timer(this.components);
            this.LoginText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SCardTimer
            // 
            this.SCardTimer.Interval = 500;
            this.SCardTimer.Tick += new System.EventHandler(this.SCardTimer_Tick);
            // 
            // LoginText
            // 
            this.LoginText.Location = new System.Drawing.Point(659, 713);
            this.LoginText.Name = "LoginText";
            this.LoginText.Size = new System.Drawing.Size(97, 21);
            this.LoginText.TabIndex = 2;
            // 
            // TranslateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 746);
            this.ControlBox = false;
            this.Controls.Add(this.LoginText);
            this.Name = "TranslateForm";
            this.Opacity = 0.01;
            this.Text = "TranslateForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TranslateForm_Load);
            this.Click += new System.EventHandler(this.TranslateClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer SCardTimer;
        private System.Windows.Forms.TextBox LoginText;
    }
}