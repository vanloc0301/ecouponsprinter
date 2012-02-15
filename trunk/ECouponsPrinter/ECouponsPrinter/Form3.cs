﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String id = listBox1.SelectedItem.ToString();

            UploadInfo ui = new UploadInfo();
            Member m = null;
            m = ui.MemberAuth(id);

            if (m != null)
            {
                if (m.StrMobileNo.Length == 0)
                {
                    Login login = new Login(id);
                    if (DialogResult.Yes == login.ShowDialog())
                    {
                        MessageBox.Show("登录成功");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("登录失败");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("登录成功！");
                    GlobalVariables.isUserLogin = true;
                    GlobalVariables.testM = m;
                    GlobalVariables.LoginUserId = id;
                }
            }
            else
                MessageBox.Show("登录失败");
     
        }

    }
}