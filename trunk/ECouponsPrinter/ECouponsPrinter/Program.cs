using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;

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

            string strSql = "select * from t_bz_terminal_param";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            while (reader.Read())
            {
                string strParamName = reader.GetString(1);
                if (strParamName.Equals("strExitPwd"))
                    GlobalVariables.StrExitPwd = reader.GetString(2);
                else if (strParamName.Equals("intMemberSec"))
                    GlobalVariables.UserWaitTime = Int16.Parse(reader.GetString(2));
                else if (strParamName.Equals("intRefreshSec"))
                    GlobalVariables.IntRefreshSec = Int16.Parse(reader.GetString(2));
                else if (strParamName.Equals("strPhone"))
                    GlobalVariables.StrPhone = reader.GetString(2);
                else if (strParamName.Equals("intAdSec"))
                    GlobalVariables.WindowWaitTime = Int16.Parse(reader.GetString(2));
                else if (strParamName.Equals("intAdImg"))
                    GlobalVariables.IntAdImg = Int16.Parse(reader.GetString(2));
                else if (strParamName.Equals("intHistory"))
                    GlobalVariables.IntHistory = Int16.Parse(reader.GetString(2));
                else if (strParamName.Equals("intCouponPrint"))
                    GlobalVariables.IntCouponPrint = Int16.Parse(reader.GetString(2));
                else if (strParamName.Equals("strTerminalNo"))
                    GlobalVariables.StrTerminalNo = reader.GetString(2);
                else if (strParamName.Equals("strServerUrl"))
                    GlobalVariables.StrServerUrl = reader.GetString(2);
            }
            cmd.Close();

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
