﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace ECouponsPrinter
{
    class AccessCmd
    {
        String strConn = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + 
            System.Windows.Forms.Application.StartupPath + "ecoupons.mdb";
        OleDbConnection objConn;
        OleDbDataReader objReader;
        OleDbCommand objCmd;

        public AccessCmd()
        {
            objConn = new OleDbConnection(strConn);
        }

        public OleDbDataReader ExecuteReader(String strSql)
        {
            objConn.Open();
            objCmd = new OleDbCommand(strSql, objConn);
            objReader = objCmd.ExecuteReader();
            return objReader;
        }

        public void ExecuteNonQuery(String strSql)
        {
            objConn.Open();
            objCmd = new OleDbCommand(strSql, objConn);
            objCmd.ExecuteNonQuery();
        }

        public void Close()
        {
            if (objReader != null)
                objReader.Close();
            if (objConn.State == System.Data.ConnectionState.Open)
                objConn.Close();
        }
    }
}