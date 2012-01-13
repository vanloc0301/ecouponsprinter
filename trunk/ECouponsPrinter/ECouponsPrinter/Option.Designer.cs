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
            this.Buttom_Close = new System.Windows.Forms.Button();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.Button_ModifyID = new System.Windows.Forms.Button();
            this.Pwd = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.TextBox();
            this.Panel_Background.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Background
            // 
            this.Panel_Background.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Background.BackgroundImage")));
            this.Panel_Background.Controls.Add(this.Buttom_Close);
            this.Panel_Background.Controls.Add(this.Button_Exit);
            this.Panel_Background.Controls.Add(this.Button_ModifyID);
            this.Panel_Background.Controls.Add(this.Pwd);
            this.Panel_Background.Controls.Add(this.ID);
            this.Panel_Background.Location = new System.Drawing.Point(0, 0);
            this.Panel_Background.Name = "Panel_Background";
            this.Panel_Background.Size = new System.Drawing.Size(528, 310);
            this.Panel_Background.TabIndex = 0;
            // 
            // Buttom_Close
            // 
            this.Buttom_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Buttom_Close.BackgroundImage")));
            this.Buttom_Close.Location = new System.Drawing.Point(340, 206);
            this.Buttom_Close.Name = "Buttom_Close";
            this.Buttom_Close.Size = new System.Drawing.Size(149, 63);
            this.Buttom_Close.TabIndex = 1;
            this.Buttom_Close.UseVisualStyleBackColor = true;
            this.Buttom_Close.Click += new System.EventHandler(this.button3_Click);
            // 
            // Button_Exit
            // 
            this.Button_Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Exit.BackgroundImage")));
            this.Button_Exit.Location = new System.Drawing.Point(185, 206);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(149, 63);
            this.Button_Exit.TabIndex = 1;
            this.Button_Exit.UseVisualStyleBackColor = true;
            // 
            // Button_ModifyID
            // 
            this.Button_ModifyID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_ModifyID.BackgroundImage")));
            this.Button_ModifyID.Location = new System.Drawing.Point(30, 206);
            this.Button_ModifyID.Name = "Button_ModifyID";
            this.Button_ModifyID.Size = new System.Drawing.Size(149, 63);
            this.Button_ModifyID.TabIndex = 1;
            this.Button_ModifyID.UseVisualStyleBackColor = true;
            // 
            // Pwd
            // 
            this.Pwd.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pwd.Location = new System.Drawing.Point(165, 142);
            this.Pwd.Margin = new System.Windows.Forms.Padding(0);
            this.Pwd.Name = "Pwd";
            this.Pwd.Size = new System.Drawing.Size(277, 30);
            this.Pwd.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ID.Location = new System.Drawing.Point(165, 80);
            this.ID.Margin = new System.Windows.Forms.Padding(0);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(277, 30);
            this.ID.TabIndex = 0;
            this.ID.Enter += new System.EventHandler(this.ID_Enter);
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 310);
            this.Controls.Add(this.Panel_Background);
            this.Name = "Option";
            this.Text = "Option";
            this.Load += new System.EventHandler(this.Option_Load);
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
    }
}