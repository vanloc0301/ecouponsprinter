using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    public partial class Option : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private KeyBoard kb = null;
        public Option()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            GlobalVariables.isKeyBoardExist = false;
        }

        private void Show_KeyBoard(object sender, EventArgs e)
        {

            if (!GlobalVariables.isKeyBoardExist)
            {
                KeyBoard kb = new KeyBoard(this.ID, this.Pwd, this.URL);
                this.kb = kb;
                kb.Show();
                GlobalVariables.isKeyBoardExist = true;
            }
        }

        private void Option_Load(object sender, EventArgs e)
        {
            this.Location = new Point(150, 10);        
        }

        private void Option_FromClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVariables.isKeyBoardExist)
            {
                this.kb.Close();
                GlobalVariables.isKeyBoardExist = false;
            }
        }

        private void Option_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            { 
                case "Button_ModifyID":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\修改编码_1.jpg");
                    return;
                case "Button_Exit":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\退出系统_1.jpg");
                    return;
                case "Buttom_Close":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\关闭_1.jpg");
                    return;
            }
        }

        private void Option_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "Button_ModifyID":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\修改编码.jpg");
                    return;
                case "Button_Exit":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\退出系统.jpg");
                    return;
                case "Buttom_Close":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\关闭.jpg");
                    this.Close();
                    return;
            }
        }



    }
}
