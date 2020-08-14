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
     
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Buttonupload_Click_FileUpload(object sender, EventArgs e)
        {
            ////Creating object of result datatable  
            DataTable tblcsvResult = new DataTable();
      
            //getting full file path of Uploaded file  
            string CSVFilePath = Path.GetFullPath(FileUploadCSV.PostedFile.FileName);
            //Reading All text  
            string ReadCSV = File.ReadAllText(CSVFilePath);
            //spliting row after new line  

            ReadFile RF = new ReadFile();
            tblcsvResult = RF.ReadFileCSV(ReadCSV);


            InsertDatabase IR = new InsertDatabase();
            bool success  = IR.InsertCSVRecords(tblcsvResult);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"$('#MessageModal').modal('show');");
            sb.Append(@"</script>");

            if (success)
                lblMessage.Text = "Your Data Successfully Uploaded to the Database.";
            else
                lblMessage.Text = "Error in uploading.";

            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
        }
    }
}