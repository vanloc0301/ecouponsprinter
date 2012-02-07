namespace ECouponsPrinter
{
    partial class Option
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Option));
            this.Panel_Background = new System.Windows.Forms.Panel();
            this.Button_Paper = new System.Windows.Forms.Button();
            this.Label_Warning = new System.Windows.Forms.Label();
            this.Buttom_Close = new System.Windows.Forms.Button();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.Button_ModifyID = new System.Windows.Forms.Button();
            this.URL = new System.Windows.Forms.TextBox();
            this.Pwd = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.TextBox();
            this.Panel_Background.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Background
            // 
            this.Panel_Background.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Background.BackgroundImage")));
            this.Panel_Background.Controls.Add(this.Button_Paper);
            this.Panel_Background.Controls.Add(this.Label_Warning);
            this.Panel_Background.Controls.Add(this.Buttom_Close);
            this.Panel_Background.Controls.Add(this.Button_Exit);
            this.Panel_Background.Controls.Add(this.Button_ModifyID);
            this.Panel_Background.Controls.Add(this.URL);
            this.Panel_Background.Controls.Add(this.Pwd);
            this.Panel_Background.Controls.Add(this.ID);
            this.Panel_Background.Location = new System.Drawing.Point(0, 0);
            this.Panel_Background.Name = "Panel_Background";
            this.Panel_Background.Size = new System.Drawing.Size(528, 342);
            this.Panel_Background.TabIndex = 0;
            // 
            // Button_Paper
            // 
            this.Button_Paper.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Paper.BackgroundImage")));
            this.Button_Paper.FlatAppearance.BorderSize = 0;
            this.Button_Paper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Paper.Location = new System.Drawing.Point(151, 252);
            this.Button_Paper.Name = "Button_Paper";
            this.Button_Paper.Size = new System.Drawing.Size(114, 53);
            this.Button_Paper.TabIndex = 3;
            this.Button_Paper.UseVisualStyleBackColor = true;
            this.Button_Paper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Option_MouseDown);
            this.Button_Paper.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Option_MouseUp);
            // 
            // Label_Warning
            // 
            this.Label_Warning.AutoSize = true;
            this.Label_Warning.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Warning.ForeColor = System.Drawing.Color.Red;
            this.Label_Warning.Location = new System.Drawing.Point(47, 47);
            this.Label_Warning.MinimumSize = new System.Drawing.Size(300, 20);
            this.Label_Warning.Name = "Label_Warning";
            this.Label_Warning.Size = new System.Drawing.Size(300, 20);
            this.Label_Warning.TabIndex = 2;
            // 
            // Buttom_Close
            // 
            this.Buttom_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Buttom_Close.BackgroundImage")));
            this.Buttom_Close.FlatAppearance.BorderSize = 0;
            this.Buttom_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Buttom_Close.Location = new System.Drawing.Point(395, 252);
            this.Buttom_Close.Name = "Buttom_Close";
            this.Buttom_Close.Size = new System.Drawing.Size(114, 53);
            this.Buttom_Close.TabIndex = 1;
            this.Buttom_Close.UseVisualStyleBackColor = true;
            this.Buttom_Close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Option_MouseDown);
            this.Buttom_Close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Option_MouseUp);
            // 
            // Button_Exit
            // 
            this.Button_Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Exit.BackgroundImage")));
            this.Button_Exit.FlatAppearance.BorderSize = 0;
            this.Button_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Exit.Location = new System.Drawing.Point(273, 252);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(114, 53);
            this.Button_Exit.TabIndex = 1;
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Option_MouseDown);
            this.Button_Exit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Option_MouseUp);
            // 
            // Button_ModifyID
            // 
            this.Button_ModifyID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_ModifyID.BackgroundImage")));
            this.Button_ModifyID.FlatAppearance.BorderSize = 0;
            this.Button_ModifyID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ModifyID.Location = new System.Drawing.Point(30, 252);
            this.Button_ModifyID.Name = "Button_ModifyID";
            this.Button_ModifyID.Size = new System.Drawing.Size(114, 53);
            this.Button_ModifyID.TabIndex = 1;
            this.Button_ModifyID.UseVisualStyleBackColor = true;
            this.Button_ModifyID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Option_MouseDown);
            this.Button_ModifyID.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Option_MouseUp);
            // 
            // URL
            // 
            this.URL.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.URL.Location = new System.Drawing.Point(165, 203);
            this.URL.Margin = new System.Windows.Forms.Padding(0);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(277, 30);
            this.URL.TabIndex = 0;
            this.URL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Textbox_MouseDown);
            this.URL.Enter += new System.EventHandler(this.Show_KeyBoard);
            // 
            // Pwd
            // 
            this.Pwd.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pwd.Location = new System.Drawing.Point(165, 142);
            this.Pwd.Margin = new System.Windows.Forms.Padding(0);
            this.Pwd.Name = "Pwd";
            this.Pwd.PasswordChar = '*';
            this.Pwd.Size = new System.Drawing.Size(277, 30);
            this.Pwd.TabIndex = 0;
            this.Pwd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Textbox_MouseDown);
            this.Pwd.Enter += new System.EventHandler(this.Show_KeyBoard);
            // 
            // ID
            // 
            this.ID.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ID.Location = new System.Drawing.Point(165, 80);
            this.ID.Margin = new System.Windows.Forms.Padding(0);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(277, 30);
            this.ID.TabIndex = 0;
            this.ID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Textbox_MouseDown);
            this.ID.Enter += new System.EventHandler(this.Show_KeyBoard);
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 336);
            this.Controls.Add(this.Panel_Background);
            this.Name = "Option";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Option";
            this.Load += new System.EventHandler(this.Option_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Option_FromClosing);
            this.Panel_Background.ResumeLayout(false);
            this.Panel_Background.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Background;
        private System.Windows.Forms.TextBox ID;
        private System.Windows.Forms.TextBox Pwd;
        private System.Windows.Forms.Button Button_ModifyID;
        private System.Windows.Forms.Button Buttom_Close;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Label Label_Warning;
        private System.Windows.Forms.Button Button_Paper;
    }
}