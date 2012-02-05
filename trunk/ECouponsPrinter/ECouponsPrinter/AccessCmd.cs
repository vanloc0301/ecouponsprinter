using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace ECouponsPrinter
{
    class AccessCmd
    {
        String strConn = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + 
            System.Windows.Forms.Application.StartupPath + "\\ecoupons.mdb";
        OleDbConnection objConn;
        OleDbDataReader objReader;
        OleDbCommand objCmd;

        public AccessCmd()
        {
            objConn = new OleDbConnection(strConn);
            objConn.Open();
        }

        public OleDbDataReader ExecuteReader(String strSql)
        {
            try
            {
                objCmd = new OleDbCommand(strSql, objConn);
                objReader = objCmd.ExecuteReader();
            }
            catch (Exception ep)
            {
                ErrorLog.log(ep);
                this.Close();
            }
            finally
            {
                return objReader;
            }
        }

        public void ExecuteNonQuery(String strSql)
        {
            try
            {
                objCmd = new OleDbCommand(strSql, objConn);
                objCmd.ExecuteNonQuery();
            }
            catch (Exception ep)
            {
                ErrorLog.log(ep);
                this.Close();
            }
        }

        public void Close()
        {
            if (objConn.State == System.Data.ConnectionState.Open)
                objConn.Close();
        }
    }
}
