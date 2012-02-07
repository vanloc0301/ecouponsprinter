﻿namespace ECouponsPrinter
{
    partial class CouponsPopForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponsPopForm));
            this.Panel_Background = new System.Windows.Forms.Panel();
            this.Code = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Button_Close = new System.Windows.Forms.Button();
            this.Button_Print = new System.Windows.Forms.Button();
            this.Button_Document = new System.Windows.Forms.Button();
            this.PB_Couponpop = new System.Windows.Forms.PictureBox();
            this.Label_Top = new System.Windows.Forms.Label();
            this.Panel_Background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Couponpop)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Background
            // 
            this.Panel_Background.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Background.BackgroundImage")));
            this.Panel_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Background.Controls.Add(this.Code);
            this.Panel_Background.Controls.Add(this.pictureBox2);
            this.Panel_Background.Controls.Add(this.Button_Close);
            this.Panel_Background.Controls.Add(this.Button_Print);
            this.Panel_Background.Controls.Add(this.Button_Document);
            this.Panel_Background.Controls.Add(this.PB_Couponpop);
            this.Panel_Background.Controls.Add(this.Label_Top);
            this.Panel_Background.Location = new System.Drawing.Point(0, 0);
            this.Panel_Background.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Background.Name = "Panel_Background";
            this.Panel_Background.Size = new System.Drawing.Size(436, 768);
            this.Panel_Background.TabIndex = 2;
            // 
            // Code
            // 
            this.Code.BackColor = System.Drawing.Color.Transparent;
            this.Code.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.Code.ForeColor = System.Drawing.Color.White;
            this.Code.Location = new System.Drawing.Point(6, 602);
            this.Code.Name = "Code";
            this.Code.Size = new System.Drawing.Size(424, 82);
            this.Code.TabIndex = 4;
            this.Code.Text = "验证码: 12345678";
            this.Code.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(11, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(99, 76);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // Button_Close
            // 
            this.Button_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Close.BackgroundImage")));
            this.Button_Close.FlatAppearance.BorderSize = 0;
            this.Button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Close.Location = new System.Drawing.Point(291, 694);
            this.Button_Close.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(137, 63);
            this.Button_Close.TabIndex = 2;
            this.Button_Close.UseVisualStyleBackColor = true;
            this.Button_Close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_Close_MouseDown);
            this.Button_Close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_Close_MouseUp);
            // 
            // Button_Print
            // 
            this.Button_Print.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Print.BackgroundImage")));
            this.Button_Print.FlatAppearance.BorderSize = 0;
            this.Button_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Print.Location = new System.Drawing.Point(148, 694);
            this.Button_Print.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Print.Name = "Button_Print";
            this.Button_Print.Size = new System.Drawing.Size(137, 63);
            this.Button_Print.TabIndex = 2;
            this.Button_Print.UseVisualStyleBackColor = true;
            this.Button_Print.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_Print_MouseDown);
            this.Button_Print.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_Print_MouseUp);
            // 
            // Button_Document
            // 
            this.Button_Document.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Document.BackgroundImage")));
            this.Button_Document.FlatAppearance.BorderSize = 0;
            this.Button_Document.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Document.Location = new System.Drawing.Point(5, 694);
            this.Button_Document.Name = "Button_Document";
            this.Button_Document.Size = new System.Drawing.Size(137, 63);
            this.Button_Document.TabIndex = 2;
            this.Button_Document.UseVisualStyleBackColor = true;
            this.Button_Document.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_Document_MouseDown);
            this.Button_Document.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_Document_MouseUp);
            // 
            // PB_Couponpop
            // 
            this.PB_Couponpop.Image = ((System.Drawing.Image)(resources.GetObject("PB_Couponpop.Image")));
            this.PB_Couponpop.Location = new System.Drawing.Point(6, 95);
            this.PB_Couponpop.Name = "PB_Couponpop";
            this.PB_Couponpop.Size = new System.Drawing.Size(425, 497);
            this.PB_Couponpop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB_Couponpop.TabIndex = 1;
            this.PB_Couponpop.TabStop = false;
            // 
            // Label_Top
            // 
            this.Label_Top.BackColor = System.Drawing.Color.Transparent;
            this.Label_Top.Font = new System.Drawing.Font("宋体", 32F, System.Drawing.FontStyle.Bold);
            this.Label_Top.ForeColor = System.Drawing.Color.White;
            this.Label_Top.Location = new System.Drawing.Point(135, 9);
            this.Label_Top.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Top.Name = "Label_Top";
            this.Label_Top.Size = new System.Drawing.Size(298, 70);
            this.Label_Top.TabIndex = 0;
            this.Label_Top.Text = "0551-6868668";
            this.Label_Top.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CouponsPopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 746);
            this.Controls.Add(this.Panel_Background);
            this.Name = "CouponsPopForm";
            this.Text = "CouponsPopForm";
            this.Load += new System.EventHandler(this.CouponPop_OnLoad);
            this.Panel_Background.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Couponpop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Background;
        private System.Windows.Forms.Label Code;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Button Button_Print;
        private System.Windows.Forms.Button Button_Document;
        private System.Windows.Forms.PictureBox PB_Couponpop;
        private System.Windows.Forms.Label Label_Top;

    }
}