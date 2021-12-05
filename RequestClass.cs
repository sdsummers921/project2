using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.IO;


namespace SGIncReports
{

    public class DataModel
    {
        public string Report_ID { get; set; }
        public string Date_Time { get; set; }

        // Parameter names need to match the json key names for them to be deserialized correctly.
        public DataModel(string ReportTimeStamp, int ReportID)
        {
            Report_ID = Convert.ToString(ReportID);
            Date_Time = ReportTimeStamp;
        }

    }

    public class DataModel2
    {
        public string Edit_Date { get; set; }
        public string Report { get; set; }
        public string Report_ID2 { get; set; }

        public DataModel2(string EditTimestamp, string report, int ReportID)
        {
            Edit_Date = EditTimestamp;
            Report = report;
            Report_ID2 = Convert.ToString(ReportID);

        }


    }
    
    public class DataController
    {
        private static readonly HttpClient postRekt = new HttpClient();
        //Auth info for API connection to database.  Passwords pre-hashed.
        // Info in getDBAsync() to auth API and get database data.
        private static string getDataUser = "DataRetriever";
        private static string getDataPass = "$2y$10$GA8WUuUBIiadzC6rQlz47uzWoU6aUqju2xj4MEixAjJvLPTA6AEl6";

        public static async Task<List<DataModel>> getDBdataAsync()
        {
            // Prevent caching of request
            postRekt.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            Dictionary<string, string> authInfo = new Dictionary<string, string>
            {
                { "User", getDataUser },
                { "Password", getDataPass }
            };
            // Encode as form for posts
            FormUrlEncodedContent authPackage = new FormUrlEncodedContent(authInfo);
            // Error handling before sending
            HttpResponseMessage response;
            string responseString = "";

            // Will try to retrieve data from API 3 times before either returning valid data or null for
            // the "MainPage_Load" event function to handle. As soon a data is retrieved in the loop, it will
            // break the loop.
            int waitCount = 0;
            while (waitCount < 3)
            {
                try
                {

                    response = await postRekt.PostAsync("https://projectapi.thegrindhville.com/", authPackage);
                    responseString = await response.Content.ReadAsStringAsync();
                    if (responseString != null && responseString.Length > 2)
                    {
                        break;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    //Delay > 1 second between attempts.  Length of timeslice varies per Windows version and processor type, 
                    //generally ranging between 15 - 30 milliseconds.
                    Thread.Sleep(1000);
                    waitCount++;
                }
            }
            //Parse data retrieved as string if possible.
            //List<DataModel> parsedData = JsonConvert.DeserializeObject<List<DataModel>>(responseString);
            try
            {
                List<DataModel> parsedData = JsonConvert.DeserializeObject<List<DataModel>>(responseString);
                parsedData.Sort((x, y) => DateTime.Compare(DateTime.Parse(y.Date_Time), DateTime.Parse(x.Date_Time)));
                return parsedData;
            }catch(Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
                List<DataModel> data = new List<DataModel>();
                data.Add(new DataModel("00-00-0000",0));
                return data;
            }
        }


    }

    public class DataController2
    {
        private static readonly HttpClient postRekt2 = new HttpClient();
        //Auth info for API connection to database.  Passwords pre-hashed.
        // Info in getDBAsync() to auth API and get database data.
        private static string getDataUser = "DataRetriever";
        private static string getDataPass = "$2y$10$GA8WUuUBIiadzC6rQlz47uzWoU6aUqju2xj4MEixAjJvLPTA6AEl6";

        public static async Task<List<DataModel2>> getDBdataAsync()
        {
            // Prevent caching of request
            postRekt2.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            Dictionary<string, string> authInfo = new Dictionary<string, string>
            {
                { "User", getDataUser },
                { "Password", getDataPass }
            };
            // Encode as form for posts
            FormUrlEncodedContent authPackage = new FormUrlEncodedContent(authInfo);
            // Error handling before sending
            HttpResponseMessage response;
            string responseString = "";

            // Will try to retrieve data from API 3 times before either returning valid data or null for
            // the "MainPage_Load" event function to handle. As soon a data is retrieved in the loop, it will
            // break the loop.
            int waitCount = 0;
            while (waitCount < 3)
            {
                try
                {
                    response = await postRekt2.PostAsync("https://projectapi.thegrindhville.com/", authPackage);
                    responseString = await response.Content.ReadAsStringAsync();
                    if (responseString != null && responseString.Length > 2)
                    {
                        break;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    //Delay > 1 second between attempts.  Length of timeslice varies per Windows version and processor type, 
                    //generally ranging between 15 - 30 milliseconds.
                    Thread.Sleep(1000);
                    waitCount++;
                }
            }
            //Parse data retrieved as string if possible.
            List<DataModel2> parsedData2 = JsonConvert.DeserializeObject<List<DataModel2>>(responseString);
            return parsedData2;
        }
    }
    
}
