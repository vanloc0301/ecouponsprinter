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
        public KeyBoard()
        {
            InitializeComponent();
        //    this.FormBorderStyle = FormBorderStyle.None;
        }

        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;

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
        }

        private void button22_Click(object sender, EventArgs e)
        {

        }   

        [DllImport("User32.dll",EntryPoint="FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName,string lpWindowName);

        [DllImport("user32")]
        static extern int SetForegroundWindow(IntPtr hwnd);

        IntPtr hWnd = FindWindow(null, "Option");
        

        #region 定义键盘按键事件的代码

        private void Key_MouseUp(object sender, MouseEventArgs e)
        {
            SetForegroundWindow(hWnd);

            Button btn = (Button)sender;

            String name = btn.Name;
            int length = name.Length;
            String str = name.Substring(length - 1,1);

    //        MessageBox.Show(str);

            SendKeys.Send(str);

        }

        #endregion

        private void Key_MouseDown(object sender, MouseEventArgs e)
        {
      //      this.Key_0.BackgroundImage = Image.FromFile(path+"
        }
    }
}
