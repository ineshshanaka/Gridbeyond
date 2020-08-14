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
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlComunication"].ConnectionString;
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                string query = "SELECT DISTINCT [Date] FROM [Market_Price_EX] ORDER BY [Date]";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DateTime datelabel = DateTime.Parse(rdr["Date"].ToString());

                    ddlDate.Items.Add(datelabel.ToString("yyyy/MM/dd"));
                }

                con.Close();
                con.Open();
            }
    
            GetChartData();
            SetMaxMIN();
            GetMAxWindow();
            
        }

        private void GetChartData()
        {
            string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlComunication"].ConnectionString;
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                string query = "select cast(M.Date as date) as M_Date, avg(M.Price) as Avg_Price from Market_Price_EX M group by cast(M.Date as date) order by cast(M.Date as date) asc;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                Series series = Chart1.Series["Series1"];

                while (rdr.Read())
                {
                    DateTime datelabel = DateTime.Parse(rdr["M_Date"].ToString());
                    series.Points.AddXY(datelabel.ToShortDateString(), rdr["Avg_Price"]);
                }

                con.Close();
            }
        }

        private void SetMaxMIN()
        {
            string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlComunication"].ConnectionString;
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                string query = "SELECT MAX([Price]) AS MAXVAL, MIN([Price]) AS MINVAL from Market_Price_EX";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Read(); // get the first row

                    TextMAX.Text = rdr.GetDouble(0).ToString();
                    TextBoxMIN.Text = rdr.GetDouble(1).ToString();

                }
                con.Close();
            }
        }

        private void GetMAxWindow()
        {
            double MaxValue = 0;   
            string Maxdate = "";
            double Previous_value = 0;
            string Previuos_Time = "";

            string TimeRanegeUpper = "";
            string TimeRanegeLover = "";


            string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlComunication"].ConnectionString;
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                string query = "SELECT [Date],convert(varchar,[Time], 8) AS Time,[Price] FROM [dbo].[Market_Price_EX] order by [Date], [Time]";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (Previous_value == 0)
                    {
                        Maxdate = rdr["Date"].ToString();
                        TimeRanegeUpper = rdr["Time"].ToString();
                        Previous_value = rdr.GetDouble(2);
                        Previuos_Time = rdr["Time"].ToString();


                    }

                    else if (MaxValue == 0 && Previous_value != 0)
                    {
                        TimeRanegeLover = rdr["Time"].ToString();
                        MaxValue = Previous_value + rdr.GetDouble(2);
                        Previous_value = rdr.GetDouble(2);
                        Previuos_Time = rdr["Time"].ToString();

                    }

                    else if (MaxValue != 0)
                    {
                        double tempSUM = Previous_value + rdr.GetDouble(2);

                        if (tempSUM > MaxValue)
                        {
                            Maxdate = rdr["Date"].ToString();
                            TimeRanegeUpper = Previuos_Time;
                            TimeRanegeLover = rdr["Time"].ToString();
                            MaxValue = tempSUM;
                            Previuos_Time = rdr["Time"].ToString();
                        }

                        else
                        {
                            Previuos_Time = rdr["Time"].ToString();
                            Previous_value = rdr.GetDouble(2);
                        }
                    }            
                }

                TextBoxMaxvaluDate.Text = DateTime.Parse(Maxdate).ToShortDateString();
                TextBoxMaxValuTime.Text = TimeRanegeUpper + " To " + TimeRanegeLover + " : " + MaxValue;

                con.Close();
            }  
        }

        private void GetChartData2()
        {

            String Selected_Dateval = ddlDate.SelectedValue;
            Chart2.Titles.Add(Selected_Dateval+" Price Variation");

            string sqlconn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlComunication"].ConnectionString;
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                string query = "SELECT convert(varchar,[Time], 8) AS Time, [Price] from Market_Price_EX where [Date] = @Date  order by [Time]";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Date", Selected_Dateval);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                Series series2 = Chart2.Series["Series1"];

                while (rdr.Read())
                {
                    series2.Points.AddXY(rdr["Time"].ToString(), rdr["Price"]);
                }
                this.Chart2.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line");
                con.Close();

            }
        }

        protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData2();
        }
    }
}