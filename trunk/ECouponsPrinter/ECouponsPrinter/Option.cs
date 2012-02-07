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
            //赋值
            this.ID.Text = GlobalVariables.StrTerminalNo;
            this.URL.Text = GlobalVariables.StrServerUrl;
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
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\修改参数_1.jpg");
                    return;
                case "Button_Exit":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\退出系统_1.jpg");
                    return;
                case "Buttom_Close":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\关闭_1.jpg");
                    return;
                case "Buttom_Paper":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\换纸结束_1.jpg");
                    return;
            }
        }

        private void Option_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            
            switch (btn.Name)
            {
                case "Button_ModifyID":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\修改参数.jpg");
                    if (this.Pwd.Text.Equals(GlobalVariables.StrExitPwd))
                    {
                        if (this.ID.Text.Trim().Equals(""))
                        {
                            this.Label_Warning.Text = "请输入本机编码！";
                        }
                        else if (this.URL.Text.Trim().Equals(""))
                        {
                            this.Label_Warning.Text = "请输入远程URL！";
                        }
                        else
                        {
                            AccessCmd cmd = new AccessCmd();
                            cmd.ExecuteNonQuery("delete from t_bz_terminal_param where strParamName='strTerminalNo' or strParamName='strServerUrl'");
                            cmd.ExecuteNonQuery("insert into t_bz_terminal_param(strId,strParamName,strParamValue) values('" + 99 + "','strTerminalNo','" + this.ID.Text.Trim() + "')");
                            cmd.ExecuteNonQuery("insert into t_bz_terminal_param(strId,strParamName,strParamValue) values('" + 99 + "','strServerUrl','" + this.URL.Text.Trim() + "')");
                            cmd.Close();
                            this.Label_Warning.Text = "操作成功！";
                        }
                    }
                    else
                    {
                        this.Label_Warning.Text = "密码错误，请重新输入！";
                    }
                    return;
                case "Button_Exit":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\退出系统.jpg");
                    if (this.Pwd.Text.Equals(GlobalVariables.StrExitPwd))
                    {
                        Program.Exit();
                    }
                    else
                    {
                        this.Label_Warning.Text = "密码错误，请重新输入！";
                    }
                    return;
                case "Buttom_Close":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\关闭.jpg");
                    this.Close();
                    return;
                case "Button_Paper":
                    btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\Option\\换纸结束.jpg");
                    if (this.Pwd.Text.Equals(GlobalVariables.StrExitPwd))
                    {
                        AccessCmd cmd = new AccessCmd();
                        cmd.ExecuteNonQuery("update t_bz_print_total set intPrintTotal=0");
                        cmd.Close();
                        this.Label_Warning.Text = "操作成功！";
                    }
                    else
                    {
                        this.Label_Warning.Text = "密码错误，请重新输入！";
                    }
                    return;
            }
        }

        private void Textbox_MouseDown(object sender, MouseEventArgs e)
        {
            this.Label_Warning.Text = "";
        }



    }
}
