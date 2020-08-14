using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gridbeyond
{
    public class DataCommunication
    {
        SqlConnection con;
        string sqlconn;

        private void connection()
        {
            sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlComunication"].ConnectionString;
            con = new SqlConnection(sqlconn);

        }
        public bool InsertCSVRecords(DataTable csvdt)
        {
            bool success = true;

            connection();
            //creating object of SqlBulkCopy    
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            //assigning Destination table name    
            objbulk.DestinationTableName = "Market_Price_EX";
            //Mapping Table column    
            objbulk.ColumnMappings.Add("Date", "Date");
            objbulk.ColumnMappings.Add("Time", "Time");
            objbulk.ColumnMappings.Add("Price", "Price");

            //inserting Datatable Records to DataBase    
            con.Open();
            try
            {
                objbulk.WriteToServer(csvdt);
            }
            catch
            {
                success = false;
            }
            con.Close();

            return success;
        }

    }
}