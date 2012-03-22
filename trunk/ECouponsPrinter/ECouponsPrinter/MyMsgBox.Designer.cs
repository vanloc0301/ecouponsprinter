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
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msg
            // 
            this.msg.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.msg.ForeColor = System.Drawing.Color.Red;
            this.msg.Location = new System.Drawing.Point(51, 31);
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
            // confirm
            // 
            this.confirm.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.confirm.ForeColor = System.Drawing.Color.Red;
            this.confirm.Location = new System.Drawing.Point(48, 230);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(183, 65);
            this.confirm.TabIndex = 1;
            this.confirm.Text = "确 定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.cancel.ForeColor = System.Drawing.Color.Red;
            this.cancel.Location = new System.Drawing.Point(387, 230);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(183, 65);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "取 消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // MyMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(689, 318);
            this.ControlBox = false;
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.msg);
            this.Name = "MyMsgBox";
            this.Text = "MyMsgBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Timer closetimer;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
    }
}