﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace ECouponsPrinter
{
    public partial class Login : Form
    {
        private static int TickNum = GlobalVariables.MessageRegetTime;
        private String checkCode;
        Thread th;
        String loginId;
        private KeyBoard kb = null;
        MainFrame mf;

        public Login(String id, MainFrame frame)
        {
            InitializeComponent();
            loginId = id;
            GlobalVariables.isKeyBoardExist = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(10, 105);
            this.mf = frame;
            mf.InitTimer();
        }

        private void CodeTimer_Tick(object sender, EventArgs e)
        {
            this.Reget.Text = "重新获取(" + --TickNum + ")";
            if (TickNum <= 0)
            {
                this.CodeTimer.Stop();
                this.Reget.Text = "获取验证码";
                this.Reget.Enabled = true;
            }
        }

        private void Reget_Click(object sender, EventArgs e)
        {
            mf.InitTimer();
            if (th != null)
            {
                if (th.IsAlive)
                {
                    th.Abort();
                    th.Join();
                }
            }
            if (this.phone.Text.Trim().Length != 11)
            {
                info.Text = "手机号必须为11位纯数字！";
                return;
            }
            else
            {
                Regex rx = new Regex("^[0-9]*$");
                Match match = rx.Match(this.phone.Text.Trim());
                if (match.Success)
                {
                    th = new Thread(new ThreadStart(sendMes));
                    th.Start();
                    this.Reget.Enabled = false;
                    TickNum = GlobalVariables.MessageRegetTime;
                    this.Reget.Text = "重新获取(" + TickNum + ")";
                    this.CodeTimer.Start();
                    this.code.Focus();
                }
                else
                {
                    info.Text = "手机号必须为11位纯数字！";
                }
            }
        }

        private void sendCode()
        {
            Random rand = new Random();
            String code = rand.Next(0, 10000).ToString("D4");
            checkCode = code;
            UploadInfo ui = new UploadInfo();
            String tel = "";

            if (this.info.InvokeRequired)
            {
                this.info.Invoke((MethodInvoker)delegate
                    {
                        tel = phone.Text;
                    }, null);
            }
            else
            {
                tel = phone.Text;
            }

            if (tel == "")
            {
                MessageBox.Show("发生错误");
                return;
            }

            if (ui.MemberLogon(loginId, tel, code))
            {
                if (this.info.InvokeRequired)
                {
                    this.info.Invoke((MethodInvoker)delegate
                    {
                        info.Text = "信息已发送";
                    }, null);
                }
                else
                {
                    info.Text = "信息已发送";
                }
            }
            else
            {
                info.Text = "您的手机号码可能已被绑定或出现通讯错误，请稍后重试或取消退出！";
            }
        }

        private void sendMes()
        {
            sendCode();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            String code = this.code.Text.Trim();
            if (code == checkCode)
            {
                this.DialogResult = DialogResult.Yes;
                UploadInfo ui = new UploadInfo();
                if (ui.MemberLogon(loginId, phone.Text))
                {
                    this.info.Text = "注册成功";
                    info.Refresh();
                    this.DialogResult = DialogResult.Yes;
                }
                else
                {
                    this.info.Text = "注册失败！";
                    info.Refresh();
                    this.DialogResult = DialogResult.No;
                }

                Thread.Sleep(1000);
                this.Close();
            }
            else
            {
                this.info.Text = "您的验证码输入错误!";
            }
        }

        private void ShowKeyBoard(object sender, EventArgs e)
        {
            if (!GlobalVariables.isKeyBoardExist)
            {
                KeyBoard kb = new KeyBoard(this.phone, this.code, null);
                kb.Location = new Point(10, 600);
                this.kb = kb;
                kb.Show();
                GlobalVariables.isKeyBoardExist = true;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVariables.isKeyBoardExist)
            {
                this.kb.Close();
                GlobalVariables.isKeyBoardExist = false;
            }
        }

        private void phone_TextChanged(object sender, EventArgs e)
        {
            mf.InitTimer();
        }
    }
}
