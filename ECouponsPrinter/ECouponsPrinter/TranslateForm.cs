using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ECouponsPrinter
{
    public partial class TranslateForm : Form
    {
        private SCard sc;
        private static bool isFirstKey = true;
        private Form par;
        Panel Home;

        public TranslateForm(Form parent, Panel home)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            par = parent;
            Home = home;
        }

        private void TranslateClick(object sender, EventArgs e)
        {
            if (Home.Visible)
            {
                if (!GlobalVariables.isUserLogin)
                {
                    MyMsgBox mb = new MyMsgBox();
                    mb.ShowMsg("请您先刷卡！", 1);
                    return;
                }
            }
            else
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }

        }

        #region 磁卡检测处理
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private extern static int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        private static extern bool GetWindowText(IntPtr hWnd, StringBuilder title, int maxBufSize);

        private const int WM_CLOSE = 0x0010;

        private void CloseAllDialog()
        {
            IntPtr hwnd;
            hwnd = GetForegroundWindow();
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            int length = GetWindowTextLength(hwnd);
            StringBuilder stringBuilder = new StringBuilder(2 * length + 1);
            GetWindowText(hwnd, stringBuilder, stringBuilder.Capacity);

            String strTitle = stringBuilder.ToString();
            if (strTitle.CompareTo("MainFrame") != 0)
            {
                SendMessage(hwnd, WM_CLOSE, 0, 0);
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData) //激活回车键
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;

            if (isFirstKey)
            {
                this.LoginText.Text = "";
                if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
                {
                    LoginText.Focus();
                    isFirstKey = false;
                }
            }
            else
            {
                if (keyData.Equals(Keys.Enter))
                {
                    String cardtext = ""; ;
                    int i = 0, j = 0;

                    for (i = 0; i < LoginText.Text.Length; i++)
                    {
                        if (LoginText.Text[i] >= '0' && LoginText.Text[i] <= '9')
                        {
                            cardtext += LoginText.Text[i].ToString();
                            j++;
                        }
                    }

                    if (!UserLogin(cardtext))
                    {
                        isFirstKey = true;
                    }
                    else
                    {
                        this.SCardTimer.Stop();
                        this.SCardTimer.Enabled = false;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 射频卡检测处理
        private void SCardStart()
        {
            sc = new SCard();
            sc.Init();
            this.SCardTimer.Enabled = true;
            this.SCardTimer.Interval = 500;
            this.SCardTimer.Start();
        }

        private void SCardTimer_Tick(object sender, EventArgs e)
        {
            if (sc.searchCard() == null)
            {
                return;
            }
            else
            {
                string cardNo = sc.searchCard();
                if (UserLogin(cardNo))
                {
                    this.SCardTimer.Stop();
                    this.SCardTimer.Enabled = false;
                }
                else
                    return;
            }
        }

        #endregion

        #region 用户登录
        private bool UserLogin(string userid)
        {
            UploadInfo ui = new UploadInfo();
            Member m = ui.MemberAuth(userid);
            MyMsgBox mb = new MyMsgBox();
            if (m == null)
            {
                mb.ShowMsg("无效的用户！", 2);
                return false;
            }
            else
            {
                if (m.StrMobileNo.Length == 0)
                {
                    Login login = new Login(userid);
                    login.TopMost = true;
                    this.Visible = false;
                    //         this.Controls.Add(login);
                    if (DialogResult.Yes == login.ShowDialog(par))
                    {
                        mb.ShowMsg("登录成功！", 2);
                        GlobalVariables.isUserLogin = true;
                        GlobalVariables.LoginUserId = userid;
                        GlobalVariables.M = m;
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                        return true;
                    }
                    else
                    {
                        mb.ShowMsg("登录失败！\n请先绑定手机！", 2);
                        this.Visible = true;
                        return false;
                    }
                }
                else
                {
                    mb.ShowMsg("登录成功！", 2);
                    GlobalVariables.isUserLogin = true;
                    GlobalVariables.LoginUserId = userid;
                    GlobalVariables.M = m;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                    return true;
                }
            }
        }

        #endregion

        private void TranslateForm_Load(object sender, EventArgs e)
        {
            //启动射频卡检测程序
            this.SCardStart();
        }

        private void LabelOption_DoubleClick(object sender, EventArgs e)
        {
            Option op = new Option();
            op.Location = new Point(100, 30);
            op.TopMost = true;
            op.Show();
        }

    }
}
