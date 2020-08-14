using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Gridbeyond
{
    public class ReadFile
    {
        public DataTable ReadFileCSV(string ReadCSV)
        {
            //Creating object of datatable  
            DataTable tblcsv = new DataTable();
            //creating columns  
            tblcsv.Columns.Add("Date");
            tblcsv.Columns.Add("Time");
            tblcsv.Columns.Add("Price");

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
                                //Spllit Data from Time and Add sperately to datatable 
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

                            // Add market price to datatabel
                            count = 2;
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;

                            count++;
                        }
                    }
                    DataRowCount++;
                }
            }
            return tblcsv;
        }
    }
}