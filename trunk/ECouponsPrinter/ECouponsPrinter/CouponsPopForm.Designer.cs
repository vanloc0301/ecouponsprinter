namespace ECouponsPrinter
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
            this.Button_Close = new System.Windows.Forms.Button();
            this.Button_Print = new System.Windows.Forms.Button();
            this.Button_Document = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label_Top = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.Panel_Background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Background
            // 
            this.Panel_Background.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Background.BackgroundImage")));
            this.Panel_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Background.Controls.Add(this.Button_Close);
            this.Panel_Background.Controls.Add(this.Button_Print);
            this.Panel_Background.Controls.Add(this.Button_Document);
            this.Panel_Background.Controls.Add(this.pictureBox1);
            this.Panel_Background.Controls.Add(this.Label_Top);
            this.Panel_Background.Location = new System.Drawing.Point(0, 0);
            this.Panel_Background.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Background.Name = "Panel_Background";
            this.Panel_Background.Size = new System.Drawing.Size(436, 768);
            this.Panel_Background.TabIndex = 0;
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
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 95);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(425, 497);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Label_Top
            // 
            this.Label_Top.Location = new System.Drawing.Point(3, 9);
            this.Label_Top.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Top.Name = "Label_Top";
            this.Label_Top.Size = new System.Drawing.Size(430, 70);
            this.Label_Top.TabIndex = 0;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // CouponsPopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 746);
            this.Controls.Add(this.Panel_Background);
            this.Name = "CouponsPopForm";
            this.Text = "CouponsPopForm";
            this.Panel_Background.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Background;
        private System.Windows.Forms.Label Label_Top;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Button_Document;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Button Button_Print;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}