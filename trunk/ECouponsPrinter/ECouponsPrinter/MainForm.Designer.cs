namespace ECouponsPrinter
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Button_LastShop = new System.Windows.Forms.Button();
            this.Panel_Background = new System.Windows.Forms.Panel();
            this.Label_ScrollText = new System.Windows.Forms.Label();
            this.Button_NextCouponsPage = new System.Windows.Forms.Button();
            this.Button_LastCouponsPage = new System.Windows.Forms.Button();
            this.Button_MyInfoPage = new System.Windows.Forms.Button();
            this.Button_CouponsPage = new System.Windows.Forms.Button();
            this.Button_ShopPage = new System.Windows.Forms.Button();
            this.Button_HomePage = new System.Windows.Forms.Button();
            this.Label_ShopName = new System.Windows.Forms.Label();
            this.Label_Countdown = new System.Windows.Forms.Label();
            this.Panel_CouponsBig = new System.Windows.Forms.Panel();
            this.PictureBox_CouponsBig = new System.Windows.Forms.PictureBox();
            this.Button_ShopInfo = new System.Windows.Forms.Button();
            this.Panel_Shop = new System.Windows.Forms.Panel();
            this.PictureBox_Shop = new System.Windows.Forms.PictureBox();
            this.Button_NextShop = new System.Windows.Forms.Button();
            this.Timer_Countdown = new System.Windows.Forms.Timer(this.components);
            this.Panel_Background.SuspendLayout();
            this.Panel_CouponsBig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_CouponsBig)).BeginInit();
            this.Panel_Shop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Shop)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_LastShop
            // 
            this.Button_LastShop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_LastShop.BackgroundImage")));
            this.Button_LastShop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Button_LastShop.FlatAppearance.BorderSize = 0;
            this.Button_LastShop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_LastShop.Location = new System.Drawing.Point(658, 715);
            this.Button_LastShop.Margin = new System.Windows.Forms.Padding(0);
            this.Button_LastShop.Name = "Button_LastShop";
            this.Button_LastShop.Size = new System.Drawing.Size(134, 116);
            this.Button_LastShop.TabIndex = 0;
            this.Button_LastShop.UseVisualStyleBackColor = true;
            this.Button_LastShop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_LastShop_MouseDown);
            this.Button_LastShop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_LastShop_MouseUp);
            // 
            // Panel_Background
            // 
            this.Panel_Background.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel_Background.BackgroundImage")));
            this.Panel_Background.Controls.Add(this.Label_ScrollText);
            this.Panel_Background.Controls.Add(this.Button_NextCouponsPage);
            this.Panel_Background.Controls.Add(this.Button_LastCouponsPage);
            this.Panel_Background.Controls.Add(this.Button_MyInfoPage);
            this.Panel_Background.Controls.Add(this.Button_CouponsPage);
            this.Panel_Background.Controls.Add(this.Button_ShopPage);
            this.Panel_Background.Controls.Add(this.Button_HomePage);
            this.Panel_Background.Controls.Add(this.Label_ShopName);
            this.Panel_Background.Controls.Add(this.Label_Countdown);
            this.Panel_Background.Controls.Add(this.Panel_CouponsBig);
            this.Panel_Background.Controls.Add(this.Button_ShopInfo);
            this.Panel_Background.Controls.Add(this.Panel_Shop);
            this.Panel_Background.Controls.Add(this.Button_NextShop);
            this.Panel_Background.Controls.Add(this.Button_LastShop);
            this.Panel_Background.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Background.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Panel_Background.Location = new System.Drawing.Point(0, 0);
            this.Panel_Background.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Background.Name = "Panel_Background";
            this.Panel_Background.Size = new System.Drawing.Size(1080, 1920);
            this.Panel_Background.TabIndex = 1;
            // 
            // Label_ScrollText
            // 
            this.Label_ScrollText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label_ScrollText.Font = new System.Drawing.Font("微软简粗黑", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ScrollText.ForeColor = System.Drawing.Color.White;
            this.Label_ScrollText.Location = new System.Drawing.Point(25, 12);
            this.Label_ScrollText.Margin = new System.Windows.Forms.Padding(0);
            this.Label_ScrollText.Name = "Label_ScrollText";
            this.Label_ScrollText.Size = new System.Drawing.Size(514, 115);
            this.Label_ScrollText.TabIndex = 14;
            this.Label_ScrollText.Text = "1234";
            this.Label_ScrollText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_NextCouponsPage
            // 
            this.Button_NextCouponsPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_NextCouponsPage.BackgroundImage")));
            this.Button_NextCouponsPage.FlatAppearance.BorderSize = 0;
            this.Button_NextCouponsPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_NextCouponsPage.Location = new System.Drawing.Point(893, 1794);
            this.Button_NextCouponsPage.Margin = new System.Windows.Forms.Padding(0);
            this.Button_NextCouponsPage.Name = "Button_NextCouponsPage";
            this.Button_NextCouponsPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Button_NextCouponsPage.Size = new System.Drawing.Size(183, 79);
            this.Button_NextCouponsPage.TabIndex = 13;
            this.Button_NextCouponsPage.UseVisualStyleBackColor = true;
            this.Button_NextCouponsPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_NextCouponsPage_MouseDown);
            this.Button_NextCouponsPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_NextCouponsPage_MouseUp);
            // 
            // Button_LastCouponsPage
            // 
            this.Button_LastCouponsPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_LastCouponsPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_LastCouponsPage.BackgroundImage")));
            this.Button_LastCouponsPage.FlatAppearance.BorderSize = 0;
            this.Button_LastCouponsPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_LastCouponsPage.Location = new System.Drawing.Point(704, 1796);
            this.Button_LastCouponsPage.Name = "Button_LastCouponsPage";
            this.Button_LastCouponsPage.Size = new System.Drawing.Size(189, 76);
            this.Button_LastCouponsPage.TabIndex = 12;
            this.Button_LastCouponsPage.UseVisualStyleBackColor = true;
            this.Button_LastCouponsPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_LastCouponsPage_MouseDown);
            this.Button_LastCouponsPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_LastCouponsPage_MouseUp);
            // 
            // Button_MyInfoPage
            // 
            this.Button_MyInfoPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_MyInfoPage.BackgroundImage")));
            this.Button_MyInfoPage.FlatAppearance.BorderSize = 0;
            this.Button_MyInfoPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_MyInfoPage.Location = new System.Drawing.Point(561, 1801);
            this.Button_MyInfoPage.Margin = new System.Windows.Forms.Padding(0);
            this.Button_MyInfoPage.Name = "Button_MyInfoPage";
            this.Button_MyInfoPage.Size = new System.Drawing.Size(134, 116);
            this.Button_MyInfoPage.TabIndex = 11;
            this.Button_MyInfoPage.UseVisualStyleBackColor = true;
            this.Button_MyInfoPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MyInfoPage_MouseDown);
            this.Button_MyInfoPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MyInfoPage_MouseUp);
            // 
            // Button_CouponsPage
            // 
            this.Button_CouponsPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_CouponsPage.BackgroundImage")));
            this.Button_CouponsPage.FlatAppearance.BorderSize = 0;
            this.Button_CouponsPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_CouponsPage.Location = new System.Drawing.Point(378, 1801);
            this.Button_CouponsPage.Margin = new System.Windows.Forms.Padding(0);
            this.Button_CouponsPage.Name = "Button_CouponsPage";
            this.Button_CouponsPage.Size = new System.Drawing.Size(136, 116);
            this.Button_CouponsPage.TabIndex = 10;
            this.Button_CouponsPage.UseVisualStyleBackColor = true;
            this.Button_CouponsPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_CouponsPage_MouseDown);
            this.Button_CouponsPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_CouponsPage_MouseUp);
            // 
            // Button_ShopPage
            // 
            this.Button_ShopPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_ShopPage.BackgroundImage")));
            this.Button_ShopPage.FlatAppearance.BorderSize = 0;
            this.Button_ShopPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ShopPage.Location = new System.Drawing.Point(199, 1801);
            this.Button_ShopPage.Margin = new System.Windows.Forms.Padding(0);
            this.Button_ShopPage.Name = "Button_ShopPage";
            this.Button_ShopPage.Size = new System.Drawing.Size(134, 116);
            this.Button_ShopPage.TabIndex = 9;
            this.Button_ShopPage.UseVisualStyleBackColor = true;
            this.Button_ShopPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_ShopPage_MouseDown);
            this.Button_ShopPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_ShopPage_MouseUp);
            // 
            // Button_HomePage
            // 
            this.Button_HomePage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_HomePage.BackgroundImage")));
            this.Button_HomePage.FlatAppearance.BorderSize = 0;
            this.Button_HomePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_HomePage.Location = new System.Drawing.Point(25, 1801);
            this.Button_HomePage.Margin = new System.Windows.Forms.Padding(0);
            this.Button_HomePage.Name = "Button_HomePage";
            this.Button_HomePage.Size = new System.Drawing.Size(133, 116);
            this.Button_HomePage.TabIndex = 8;
            this.Button_HomePage.UseVisualStyleBackColor = true;
            this.Button_HomePage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_HomePage_MouseDown);
            this.Button_HomePage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_HomePage_MouseUp);
            // 
            // Label_ShopName
            // 
            this.Label_ShopName.Image = ((System.Drawing.Image)(resources.GetObject("Label_ShopName.Image")));
            this.Label_ShopName.Location = new System.Drawing.Point(8, 712);
            this.Label_ShopName.Name = "Label_ShopName";
            this.Label_ShopName.Size = new System.Drawing.Size(598, 116);
            this.Label_ShopName.TabIndex = 7;
            // 
            // Label_Countdown
            // 
            this.Label_Countdown.Font = new System.Drawing.Font("微软简粗黑", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Countdown.Image = ((System.Drawing.Image)(resources.GetObject("Label_Countdown.Image")));
            this.Label_Countdown.Location = new System.Drawing.Point(901, 12);
            this.Label_Countdown.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Countdown.Name = "Label_Countdown";
            this.Label_Countdown.Size = new System.Drawing.Size(164, 115);
            this.Label_Countdown.TabIndex = 6;
            this.Label_Countdown.Text = "100";
            this.Label_Countdown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_CouponsBig
            // 
            this.Panel_CouponsBig.Controls.Add(this.PictureBox_CouponsBig);
            this.Panel_CouponsBig.Location = new System.Drawing.Point(2, 857);
            this.Panel_CouponsBig.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel_CouponsBig.Name = "Panel_CouponsBig";
            this.Panel_CouponsBig.Size = new System.Drawing.Size(1074, 552);
            this.Panel_CouponsBig.TabIndex = 5;
            // 
            // PictureBox_CouponsBig
            // 
            this.PictureBox_CouponsBig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox_CouponsBig.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_CouponsBig.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PictureBox_CouponsBig.Name = "PictureBox_CouponsBig";
            this.PictureBox_CouponsBig.Size = new System.Drawing.Size(1074, 552);
            this.PictureBox_CouponsBig.TabIndex = 0;
            this.PictureBox_CouponsBig.TabStop = false;
            // 
            // Button_ShopInfo
            // 
            this.Button_ShopInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_ShopInfo.BackgroundImage")));
            this.Button_ShopInfo.FlatAppearance.BorderSize = 0;
            this.Button_ShopInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ShopInfo.Location = new System.Drawing.Point(942, 715);
            this.Button_ShopInfo.Margin = new System.Windows.Forms.Padding(0);
            this.Button_ShopInfo.Name = "Button_ShopInfo";
            this.Button_ShopInfo.Size = new System.Drawing.Size(134, 115);
            this.Button_ShopInfo.TabIndex = 4;
            this.Button_ShopInfo.UseVisualStyleBackColor = true;
            this.Button_ShopInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_ShopInfo_MouseDown);
            this.Button_ShopInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_ShopInfo_MouseUp);
            // 
            // Panel_Shop
            // 
            this.Panel_Shop.Controls.Add(this.PictureBox_Shop);
            this.Panel_Shop.Location = new System.Drawing.Point(2, 157);
            this.Panel_Shop.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Shop.Name = "Panel_Shop";
            this.Panel_Shop.Size = new System.Drawing.Size(1074, 540);
            this.Panel_Shop.TabIndex = 3;
            // 
            // PictureBox_Shop
            // 
            this.PictureBox_Shop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox_Shop.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Shop.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBox_Shop.Name = "PictureBox_Shop";
            this.PictureBox_Shop.Size = new System.Drawing.Size(1074, 540);
            this.PictureBox_Shop.TabIndex = 1;
            this.PictureBox_Shop.TabStop = false;
            // 
            // Button_NextShop
            // 
            this.Button_NextShop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_NextShop.BackgroundImage")));
            this.Button_NextShop.FlatAppearance.BorderSize = 0;
            this.Button_NextShop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_NextShop.Location = new System.Drawing.Point(798, 715);
            this.Button_NextShop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Button_NextShop.Name = "Button_NextShop";
            this.Button_NextShop.Size = new System.Drawing.Size(134, 115);
            this.Button_NextShop.TabIndex = 2;
            this.Button_NextShop.UseVisualStyleBackColor = true;
            this.Button_NextShop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_NextShop_MouseDown);
            this.Button_NextShop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_NextShop_MouseUp);
            // 
            // Timer_Countdown
            // 
            this.Timer_Countdown.Interval = 1000;
            this.Timer_Countdown.Tick += new System.EventHandler(this.Timer_Countdown_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1130, 520);
            this.Controls.Add(this.Panel_Background);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.Text = "首页";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Panel_Background.ResumeLayout(false);
            this.Panel_CouponsBig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_CouponsBig)).EndInit();
            this.Panel_Shop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Shop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_LastShop;
        private System.Windows.Forms.Panel Panel_Background;
        private System.Windows.Forms.PictureBox PictureBox_Shop;
        private System.Windows.Forms.Button Button_NextShop;
        private System.Windows.Forms.Panel Panel_Shop;
        private System.Windows.Forms.Button Button_ShopInfo;
        private System.Windows.Forms.Panel Panel_CouponsBig;
        private System.Windows.Forms.PictureBox PictureBox_CouponsBig;
        private System.Windows.Forms.Label Label_Countdown;
        private System.Windows.Forms.Timer Timer_Countdown;
        private System.Windows.Forms.Label Label_ShopName;
        private System.Windows.Forms.Button Button_HomePage;
        private System.Windows.Forms.Button Button_CouponsPage;
        private System.Windows.Forms.Button Button_ShopPage;
        private System.Windows.Forms.Button Button_MyInfoPage;
        private System.Windows.Forms.Button Button_LastCouponsPage;
        private System.Windows.Forms.Button Button_NextCouponsPage;
        private System.Windows.Forms.Label Label_ScrollText;

    }
}

