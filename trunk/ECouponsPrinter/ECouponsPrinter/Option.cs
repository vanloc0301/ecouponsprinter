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
        public Option()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ID_Enter(object sender, EventArgs e)
        {
            KeyBoard kb = new KeyBoard();
            kb.Show();
            
        }
    }
}
