using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

            MainForm mf = new MainForm();
            GlobalVariables.Set_Homepage(mf);
            ShopInfoForm sif = new ShopInfoForm();
            GlobalVariables.Set_ShopInfoPage(sif);
            ShopSecondForm ssf = new ShopSecondForm();
            GlobalVariables.Set_ShopPage(ssf);
            MyInfoForm mip = new MyInfoForm();
            GlobalVariables.Set_MyInfoPage(mip);
            CouponsSecondForm csf = new CouponsSecondForm();
            GlobalVariables.Set_CouponsPage(csf);
            CouponsPopForm cpf = new CouponsPopForm();
            GlobalVariables.Set_CouponsPopPage(cpf);

            Application.Run(new Option());
        }
    }
}
