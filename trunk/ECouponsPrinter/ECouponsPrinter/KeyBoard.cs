using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace ECouponsPrinter
{
    public partial class KeyBoard : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private TextBox id, pwd, url;
        IntPtr hWnd;

        public KeyBoard(TextBox id, TextBox pwd, TextBox url)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            this.id = id;
            this.pwd = pwd;
            this.url = url;

            if (url == null)
            {
                hWnd = FindWindow(null, "Login");
            }
            else
            {
                hWnd = FindWindow(null, "Option");
            }
        }

        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private int KeyType = 0;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                cp.Parent = IntPtr.Zero; // Keep this line only if you used UserControl
                return cp;

                //return base.CreateParams;
            }
        }
        
        private void Button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            GlobalVariables.isKeyBoardExist = false;
        }

        [DllImport("User32.dll",EntryPoint="FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName,string lpWindowName);

        [DllImport("user32")]
        static extern int SetForegroundWindow(IntPtr hwnd);
        

        #region 定义键盘按键事件的代码

        private void Key_MouseUp(object sender, MouseEventArgs e)
        {
            if(hWnd != IntPtr.Zero)
                SetForegroundWindow(hWnd);
            else
                MessageBox.Show("error");

            Button btn = (Button)sender;

            String name = btn.Name;
            int length = name.Length;
            String str = name.Substring(length - 1,1);

      //      if(hWnd != null)
      //      MessageBox.Show(hWnd.ToString("aa"));

            String sec = null;

            switch(str)
            {
                case "S":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "BS")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\退格.jpg");
                        SendKeys.Send("{BS}");
                        return;
                    }
                    break;

                case "E":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "CE")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\关闭.jpg");
                        this.Close();
                        GlobalVariables.isKeyBoardExist = false;
                        return;
                    }
                    break;

                case "H":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "CH")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\大小写.jpg");
                        if(KeyType == 0)
                            KeyType = 1;
                        else
                            KeyType = 0;
                        return;
                    }
                    break;
                
                case "L":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "CL")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\清空.jpg");
                        if(id.Focused)
                            id.Text = "";
                        if (pwd.Focused)
                            pwd.Text = "";
                        if (url.Focused)
                            url.Text = "";

                        return;
                    }
                    break;
            }

            if (KeyType == 1)
            {
                str = str.ToUpperInvariant();
            }

            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\" + str + ".jpg");
            SendKeys.Send(str);

        }

        #endregion

        private void Key_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            String name = btn.Name;
            int length = name.Length;
            String str = name.Substring(length - 1, 1);
            String sec = null;

            switch (str)
            {
                case "S":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "BS")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\退格_1.jpg");
                        return;
                    }
                    break;

                case "E":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "CE")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\关闭_1.jpg");
                        return;
                    }
                    break;

                case "H":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "CH")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\大小写_1.jpg");
                        return;
                    }
                    break;

                case "L":
                    sec = name.Substring(length - 2, 2);
                    if (sec == "CL")
                    {
                        btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\KeyBoard\\清空_1.jpg");
                        return;
                    }
                    break;
            }

            btn.BackgroundImage = Image.FromFile(path+"\\images\\切图\\KeyBoard\\"+str+"_1.jpg");
        }

        private void KeyBoard_Load(object sender, EventArgs e)
        {
            this.Location = new Point(10, 600);
        }

    }
}
