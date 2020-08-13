using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gridbeyond
{
    public partial class _Default : Page
    {
        SqlConnection con;
        string sqlconn;
    
        private void connection()
        {
            sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlComunication"].ConnectionString;
            con = new SqlConnection(sqlconn);

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click_FileUpload(object sender, EventArgs e)
        {
            //Creating object of datatable  
            DataTable tblcsv = new DataTable();
            //creating columns  
            tblcsv.Columns.Add("Date");
            tblcsv.Columns.Add("Time");
            tblcsv.Columns.Add("Price");

            //getting full file path of Uploaded file  
            string CSVFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
            //Reading All text  
            string ReadCSV = File.ReadAllText(CSVFilePath);
            //spliting row after new line  

            int DataRowCount = 0;
            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {                                  
                    int count = 0;

                    if (DataRowCount != 0)
                    {
                        //Adding each row into datatable  
                        tblcsv.Rows.Add();

                        foreach (string FileRec in csvRow.Split(','))
                        {

                            if (count == 0)
                            {
                                foreach (string FileRecDate in FileRec.Split(' '))
                                {
                                    tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRecDate;                                   
                                    count++;
                                }  
                                
                                if (count == 1)
                                {
                                    tblcsv.Rows[tblcsv.Rows.Count - 1][count] = "00:00";
                                    count++;
                                }
                            }

                            count = 2;
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;

                            count++;                           
                        }
                    }
                    DataRowCount++;
                }
            }
            //Calling insert Functions  
            InsertCSVRecords(tblcsv);
        }
        //Function to Insert Records  
        private void InsertCSVRecords(DataTable csvdt)
        {
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
            objbulk.WriteToServer(csvdt);
            con.Close();
        }
    }
}