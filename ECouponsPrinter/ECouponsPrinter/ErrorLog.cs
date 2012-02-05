using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ECouponsPrinter
{
    class ErrorLog
    {
        public static void log(Exception e)
        {
            StreamWriter sw = File.AppendText(System.Windows.Forms.Application.StartupPath + "\\error.log");
            sw.WriteLine("[Time]" + DateTime.Now.ToString());
            sw.WriteLine("[Message]" + e.Message);
            sw.WriteLine("[StackTrace]" + e.StackTrace);
            sw.Close();
        }
    }
}
