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
    public partial class CouponsPopForm : Form
    {
        private static int num = 10;
        private String path = System.Windows.Forms.Application.StartupPath;
        private Bitmap SourceBitmap;
        private Bitmap MyBitmap;
        private static int CountDownNumber = 5;
        private string _stringScrollText = "欢迎使用本系统";
        private BackgroundWorker _workerScrollText = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };

        public CouponsPopForm()
        {
            InitializeComponent();
    //        InitializeScrollTextWorker();
        }

  
    }
}
