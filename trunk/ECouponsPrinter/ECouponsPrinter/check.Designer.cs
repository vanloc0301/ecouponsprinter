namespace ECouponsPrinter
{
    partial class check
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
            this.panelShow = new System.Windows.Forms.Panel();
            this.codeText = new System.Windows.Forms.TextBox();
            this.Reget = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.Label();
            this.Confirm = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.getCode = new System.Windows.Forms.Timer(this.components);
            this.Notice = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.panelShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelShow
            // 
            this.panelShow.Controls.Add(this.codeText);
            this.panelShow.Controls.Add(this.Reget);
            this.panelShow.Controls.Add(this.label1);
            this.panelShow.Location = new System.Drawing.Point(55, 176);
            this.panelShow.Name = "panelShow";
            this.panelShow.Size = new System.Drawing.Size(601, 78);
            this.panelShow.TabIndex = 6;
            // 
            // codeText
            // 
            this.codeText.Font = new System.Drawing.Font("宋体", 30F);
            this.codeText.Location = new System.Drawing.Point(184, 16);
            this.codeText.Name = "codeText";
            this.codeText.Size = new System.Drawing.Size(233, 53);
            this.codeText.TabIndex = 2;
            this.codeText.TextChanged += new System.EventHandler(this.codeText_TextChanged);
            this.codeText.Enter += new System.EventHandler(this.codeText_Enter);
            // 
            // Reget
            // 
            this.Reget.Enabled = false;
            this.Reget.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.Reget.Location = new System.Drawing.Point(436, 14);
            this.Reget.Name = "Reget";
            this.Reget.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Reget.Size = new System.Drawing.Size(155, 59);
            this.Reget.TabIndex = 4;
            this.Reget.Text = "重新获取";
            this.Reget.UseVisualStyleBackColor = true;
            this.Reget.Click += new System.EventHandler(this.Reget_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "手机验证码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // text
            // 
            this.text.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.text.Location = new System.Drawing.Point(39, 64);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(647, 110);
            this.text.TabIndex = 7;
            this.text.Text = "为了保障您的购物安全，我们已经给您的手机发送了验证码，请输入验证码后完成打印。";
            // 
            // Confirm
            // 
            this.Confirm.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.Confirm.Location = new System.Drawing.Point(55, 325);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(202, 74);
            this.Confirm.TabIndex = 8;
            this.Confirm.Text = "确  定";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cancel.Location = new System.Drawing.Point(454, 325);
            this.Cancel.Name = "Cancel";
            this.Cancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Cancel.Size = new System.Drawing.Size(202, 74);
            this.Cancel.TabIndex = 8;
            this.Cancel.Text = "取  消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // getCode
            // 
            this.getCode.Interval = 1000;
            this.getCode.Tick += new System.EventHandler(this.getCode_Tick);
            // 
            // Notice
            // 
            this.Notice.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.Notice.Location = new System.Drawing.Point(39, 12);
            this.Notice.Name = "Notice";
            this.Notice.Size = new System.Drawing.Size(641, 45);
            this.Notice.TabIndex = 9;
            // 
            // error
            // 
            this.error.Font = new System.Drawing.Font("宋体", 25F);
            this.error.ForeColor = System.Drawing.Color.Red;
            this.error.Location = new System.Drawing.Point(53, 266);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(603, 38);
            this.error.TabIndex = 10;
            this.error.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // check
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(721, 414);
            this.ControlBox = false;
            this.Controls.Add(this.error);
            this.Controls.Add(this.Notice);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.text);
            this.Controls.Add(this.panelShow);
            this.Name = "check";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "check";
            this.panelShow.ResumeLayout(false);
            this.panelShow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelShow;
        private System.Windows.Forms.TextBox codeText;
        private System.Windows.Forms.Button Reget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Timer getCode;
        private System.Windows.Forms.Label Notice;
        private System.Windows.Forms.Label error;
    }
}