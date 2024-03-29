﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ECouponsPrinter
{
    public partial class check : Form
    {
        private static int TickNum = 30;
        public static String id;
        private String checkCode;
        Thread th;
        private KeyBoard kb = null;
        private MainFrame mf;

        public check(double _price, String _id, MainFrame frame)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.Location = new Point(10, 150);
            this.mf = frame;
            id = _id;
            this.Reget.Text = "重新获取(" + TickNum + ")";
            this.Reget.Enabled = false;
            GlobalVariables.isKeyBoardExist = false;
            this.Notice.Text = "您即将进行交易的金额为:  " + _price + "  元";
            this.getCode.Enabled = true;
            this.getCode.Start();
            TickNum = 30;
            th = new Thread(new ThreadStart(sendMes));
            th.Start();
        }

        private void getCode_Tick(object sender, EventArgs e)
        {
            this.Reget.Text = "重新获取(" + --TickNum + ")";
            if (TickNum <= 0)
            {
                this.getCode.Stop();
                this.Reget.Text = "获取";
                this.Reget.Enabled = true;
            }
        }

        private void sendCode()
        {
            Random rand = new Random();
            String code = rand.Next(0, 10000).ToString("D4");
            checkCode = code;
            UploadInfo ui = new UploadInfo();

            string strReturn = ui.CouponAuth(GlobalVariables.LoginUserId, code, id);

            if (strReturn.Equals("sms_error"))
            {
                this.error.Text ="短信发送失败！请稍后重试。";
                return;
            }
            if (strReturn.Equals("balance_error"))
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("对不起，您的余额不足！\n请及时充值", 2) ;
                this.Close();
            }

        }

        private void Reget_Click(object sender, EventArgs e)
        {
            mf.InitUserQuitTime();

            if (th != null)
            {
                th.Abort();
                th.Join();
            }

            th = new Thread(new ThreadStart(sendMes));
            th.Start();
            this.Reget.Enabled = false;
            TickNum = 30;
            this.getCode.Start();
            this.codeText.Focus();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            mf.InitUserQuitTime();

            String code = this.codeText.Text;
            if (code == checkCode)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                this.error.Text = "您的验证码输入错误!";
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void sendMes()
        {
            sendCode();
        }

        private void codeText_Enter(object sender, EventArgs e)
        {
            if (!GlobalVariables.isKeyBoardExist)
            {
                KeyBoard kb = new KeyBoard(codeText, null, null);
                this.kb = kb;
                kb.Show();
                GlobalVariables.isKeyBoardExist = true;
            }
        }

        private void codeText_TextChanged(object sender, EventArgs e)
        {
            mf.InitUserQuitTime();
        }

        private void check_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVariables.isKeyBoardExist)
            {
                this.kb.Close();
                GlobalVariables.isKeyBoardExist = false;
            }
        }

    }
}
