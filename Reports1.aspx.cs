using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Windows;
using System.Data;
using System.Text;

namespace SGIncReports
{
    public partial class Reports1 : System.Web.UI.Page

    {
        // Put this here to be accessible throughout class
        private List<DataModel> Info;
        private List<DataModel2> Info2;


        protected async void Page_Load(object sender, EventArgs e)
        {
            this.Info = await DataController.getDBdataAsync();
            if(this.Info[0].Report_ID == "0")
            {
                string message = "Unable to retrieve data at this time.  Please check connections and retry or contact your Administrator.";
                MessageBox.Show(Page, message);
            }
            else
            {
                if (!IsPostBack)
                {
                    Button1_Click(new object(), new EventArgs());
                }
            }


        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            this.Info = await DataController.getDBdataAsync();
            BindGrid(Info);




        }

        private void BindGrid(List<DataModel> info)
        {
            gvDataModel.DataSource = info;
            gvDataModel.DataBind();


        }

        protected async void gvDataModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Info2 = await DataController2.getDBdataAsync();
            // Need to use selected row to extract column
            // information.
            int sel = gvDataModel.SelectedIndex;
            GridViewRow selectedRow = gvDataModel.Rows[sel];
            string repID = selectedRow.Cells[1].Text;
            DataModel2 reportToFind = new DataModel2(null, "", 0);
            // Then find the selected report ID in the list
            for (int i = 0; i < Info.Count; i++)
            {
                if (Info2[i].Report_ID2 == repID)
                {
                    reportToFind = Info2[i];
                }
            }
            TextBox1.Text = $"Report ID: {reportToFind.Report_ID2}\nLast Edit Date: {reportToFind.Edit_Date}\n\n{reportToFind.Report}";

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // Getting users selected date and trimming off time.
            // Have to format it to match format its stored in.
            string selectedDate = Calendar1.SelectedDate.ToString();
            string justDate = selectedDate.Substring(0, selectedDate.IndexOf(" "));
            string fomattedDate = justDate.Replace('/', '-');
            // List will be with data models that have similar date to date
            // selected by user.
            List<DataModel> foundDates = new List<DataModel>();
            for (int i = 0; i < Info.Count; i++)
            {
                string tempDate = Info[i].Date_Time.Substring(0, Info[i].Date_Time.IndexOf(" "));
                int dateComp = DateTime.Compare(DateTime.Parse(tempDate), DateTime.Parse(fomattedDate));
                if (dateComp == 0)
                {
                    foundDates.Add(Info[i]);
                }
            }
            BindGrid(foundDates);
        }

        protected void gvDataModel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDataModel.PageIndex = e.NewPageIndex;
            Button1_Click(new object(), new EventArgs());
        }
    }

    public static class MessageBox
    {
        public static void Show(this Page Page, String Message)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }
    }

}