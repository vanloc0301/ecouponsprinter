namespace ECouponsPrinter
{
    partial class MyMsgBox
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
            this.msg = new System.Windows.Forms.Label();
            this.closetimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // msg
            // 
            this.msg.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.msg.Location = new System.Drawing.Point(54, 51);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(563, 185);
            this.msg.TabIndex = 0;
            this.msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // closetimer
            // 
            this.closetimer.Interval = 2000;
            this.closetimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
            // 
            // MyMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 307);
            this.ControlBox = false;
            this.Controls.Add(this.msg);
            this.Name = "MyMsgBox";
            this.Text = "MyMsgBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Timer closetimer;
    }
}