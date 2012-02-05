using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;
using System.IO;

namespace ECouponsPrinter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Add the event handler for handling UI thread exceptions to the event.
            //Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            //AppDomain.CurrentDomain.UnhandledException +=
            //    new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //创建日志文件
            if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\error.log"))
            {
                File.CreateText(System.Windows.Forms.Application.StartupPath + "\\error.log");
            }

            Application.Run(new MainFrame());
        }

        public static void Exit()
        {
            System.Environment.Exit(0);
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("发现异常！");
            Application.Restart();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("发现异常！");
            Application.Restart();
        }
    }
}
