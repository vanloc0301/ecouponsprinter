namespace ECouponsPrinter
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.TextBox();
            this.code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.Reget = new System.Windows.Forms.Button();
            this.CodeTimer = new System.Windows.Forms.Timer(this.components);
            this.info = new System.Windows.Forms.Label();
            this.import = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(688, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "为了保护您的账户余额和积分安全，请进行手机绑定。";
            // 
            // phone
            // 
            this.phone.Font = new System.Drawing.Font("宋体", 25F);
            this.phone.Location = new System.Drawing.Point(137, 196);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(256, 46);
            this.phone.TabIndex = 1;
            this.phone.TextChanged += new System.EventHandler(this.phone_TextChanged);
            this.phone.Enter += new System.EventHandler(this.ShowKeyBoard);
            // 
            // code
            // 
            this.code.Font = new System.Drawing.Font("宋体", 25F);
            this.code.Location = new System.Drawing.Point(137, 257);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(145, 46);
            this.code.TabIndex = 2;
            this.code.Enter += new System.EventHandler(this.ShowKeyBoard);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(36, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "手机号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(39, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "验证码";
            // 
            // confirm
            // 
            this.confirm.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.confirm.ForeColor = System.Drawing.Color.Black;
            this.confirm.Location = new System.Drawing.Point(40, 377);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(219, 69);
            this.confirm.TabIndex = 4;
            this.confirm.Text = "确  定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.cancel.ForeColor = System.Drawing.Color.Black;
            this.cancel.Location = new System.Drawing.Point(408, 377);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(219, 69);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "取  消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // Reget
            // 
            this.Reget.BackColor = System.Drawing.SystemColors.Control;
            this.Reget.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.Reget.ForeColor = System.Drawing.Color.Black;
            this.Reget.Location = new System.Drawing.Point(415, 196);
            this.Reget.Name = "Reget";
            this.Reget.Size = new System.Drawing.Size(212, 46);
            this.Reget.TabIndex = 5;
            this.Reget.Text = "获取验证码";
            this.Reget.UseVisualStyleBackColor = false;
            this.Reget.Click += new System.EventHandler(this.Reget_Click);
            // 
            // CodeTimer
            // 
            this.CodeTimer.Interval = 1000;
            this.CodeTimer.Tick += new System.EventHandler(this.CodeTimer_Tick);
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.Transparent;
            this.info.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.info.ForeColor = System.Drawing.Color.Red;
            this.info.Location = new System.Drawing.Point(35, 321);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(402, 39);
            this.info.TabIndex = 6;
            // 
            // import
            // 
            this.import.BackColor = System.Drawing.Color.Transparent;
            this.import.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.import.ForeColor = System.Drawing.Color.Red;
            this.import.Location = new System.Drawing.Point(108, 80);
            this.import.Margin = new System.Windows.Forms.Padding(0);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(519, 31);
            this.import.TabIndex = 7;
            this.import.Text = "我们承诺，绑定手机不会收取您的任何费用。";
            this.import.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(20, 120);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(691, 59);
            this.label4.TabIndex = 8;
            this.label4.Text = "首先请输入手机号码，我们会以短信的方式将验证码发送至您的手机，您录入正确的验证码即可完成绑定。\r\n";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(727, 471);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.import);
            this.Controls.Add(this.info);
            this.Controls.Add(this.Reget);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.code);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.TextBox code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button Reget;
        private System.Windows.Forms.Timer CodeTimer;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Label import;
        private System.Windows.Forms.Label label4;
    }
}