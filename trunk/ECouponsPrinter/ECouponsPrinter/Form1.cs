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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadInfo di = new DownloadInfo();
            di.form1 = this;
            di.download();
        }

        public void setText(string str)
        {
            this.textBox1.Text = str;
        }
    }
}
