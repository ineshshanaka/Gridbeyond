using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Gridbeyond
{
    public partial class DashBoard : System.Web.UI.Page
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
            //Check Data avalability
            bool cehck = CheckDataAvalabity();

            if (cehck)
            {
                // Load Avalable dates to Dropdown Box
                GetDropdownListData();
                // Load Data to Daily Average Bar Chart
                GetAverageChartData();
                // Get Max and Min Price values 
                SetMaxMIN();
                //Get Most expensive hour window for the data range provided
                GetMAxWindow();
            }
            else
            {
                LabelNoData.Text = "Data not Avalable !";
            }
        }

        private bool CheckDataAvalabity()
        {
            connection();
            //Check table for data
            string query = " select TOP 1[Date] from [Market_Price_EX]";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
       
            if (rdr.HasRows)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        private void GetDropdownListData()
        {
            connection();
            string query = "SELECT DISTINCT [Date] FROM [Market_Price_EX] ORDER BY [Date]";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {      
                // Adding Date values one by one to the drop down list
                DateTime datelabel = DateTime.Parse(rdr["Date"].ToString());
                ddlDate.Items.Add(datelabel.ToString("yyyy/MM/dd"));
            }

            con.Close();           
        }

        private void GetAverageChartData()
        {
            ChartAverageDaily.Titles.Add(" Daily Price Average");

            connection();
            //Query to get avreage daily values from the database
            string query = "select cast(M.Date as date) as M_Date, avg(M.Price) as Avg_Price from Market_Price_EX M group by cast(M.Date as date) order by cast(M.Date as date) asc;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            Series seriesAverage = ChartAverageDaily.Series["SeriesAverage"];

            while (rdr.Read())
            {
                DateTime datelabel = DateTime.Parse(rdr["M_Date"].ToString());
                // Adding X and Y Points to the Daily Average Bar Chart
                seriesAverage.Points.AddXY(datelabel.ToShortDateString(), rdr["Avg_Price"]);
            }

            con.Close();
            
        }

        private void SetMaxMIN()
        {
            connection();
            //Query to get maximum and minimum values from the data range
            string query = "SELECT MAX([Price]) AS MAXVAL, MIN([Price]) AS MINVAL from Market_Price_EX";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read(); // get the first row

                //Set the maximum value
                TextBoxMAX.Text = rdr.GetDouble(0).ToString();
                // Set the minimum value
                TextBoxMIN.Text = rdr.GetDouble(1).ToString();

            }
            con.Close();        
        }

        private void GetMAxWindow()
        {
            // Store Maximum Value Sum for 1 hour
            double MaxValue = 0;
            // Store date value that recorded the maximum value
            string Maxdate = "";
            // Store Uper Time limit Of the maximum number recorded time window
            string TimeRanegeUpper = "";
            // Store Lover Time limit Of the maximum number recorded time window
            string TimeRanegeLover = "";

            // Store Each loop price value to use in next round
            double Previous_value = 0;
            // Store Each loop Time Window value to use in next round
            string Previuos_Time = "";

            connection();

            // Select data Range fro the database
            string query = "SELECT [Date],convert(varchar,[Time], 8) AS Time,[Price] FROM [dbo].[Market_Price_EX] order by [Date], [Time]";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Store First Time loop Values initially
                if (Previous_value == 0)
                {
                    Maxdate = rdr["Date"].ToString();
                    TimeRanegeUpper = rdr["Time"].ToString();
                    Previous_value = rdr.GetDouble(2);
                    Previuos_Time = rdr["Time"].ToString();
                }

                // Store Secound Time loop values and set the max value
                else if (MaxValue == 0 && Previous_value != 0)
                {
                    TimeRanegeLover = rdr["Time"].ToString();
                    MaxValue = Previous_value + rdr.GetDouble(2);
                    Previous_value = rdr.GetDouble(2);
                    Previuos_Time = rdr["Time"].ToString();
                }

                //Handel 3rd and futher loop values
                else if (MaxValue != 0)
                {
                    double tempSUM = Previous_value + rdr.GetDouble(2);

                    //Check curren sum greater than the previous maximum value 
                    if (tempSUM > MaxValue)
                    {
                        // Set New max value
                        Maxdate = rdr["Date"].ToString();
                        // Set new uper limt realated to max value
                        TimeRanegeUpper = Previuos_Time;
                        // Set new lower limt realated to max value
                        TimeRanegeLover = rdr["Time"].ToString();
                        //Set New max value
                        MaxValue = tempSUM;
                        Previuos_Time = rdr["Time"].ToString();
                    }

                    // if the current sum value less than the maximum value
                    else
                    {
                        Previuos_Time = rdr["Time"].ToString();
                        Previous_value = rdr.GetDouble(2);
                    }
                }
            }

            //Assign calculated value to texboxes to view 
            TextBoxMaxvaluDate.Text = DateTime.Parse(Maxdate).ToShortDateString();
            TextBoxMaxValuTime.Text = TimeRanegeUpper + " To " + TimeRanegeLover + " : " + MaxValue;

            con.Close();
             
        }

        private void GetDailyChartData()
        {
            String Selected_Dateval = ddlDate.SelectedValue;
            ChartDailyView.Titles.Add(Selected_Dateval+" Price Variation");

            connection();

            // Query to get Price varition with the time filtered by date
            string query = "SELECT convert(varchar,[Time], 8) AS Time, [Price] from Market_Price_EX where [Date] = @Date  order by [Time]";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Date", Selected_Dateval);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            Series seriesDaily = ChartDailyView.Series["SeriesDaily"];

            while (rdr.Read())
            {
                // Add X and Y Points one by one to the bart char according to the date selected
                seriesDaily.Points.AddXY(rdr["Time"].ToString(), rdr["Price"]);
            }

            //Setting chart type to Line chart
            this.ChartDailyView.Series["SeriesDaily"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line");
            con.Close();
          
        }

        protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Genarating Daily Price variation chart according to selected date
            GetDailyChartData();
        }
    }
}