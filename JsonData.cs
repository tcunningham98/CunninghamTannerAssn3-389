using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.IO;
using System.Json;
using Android.Locations;

namespace CunninghamTannerAssn3
{
    //
    // This activity shows how to make an HTTPRequest from a RESTful Web service.
    // and parse the resulting JSON code.
    //
    // NOTE: We should h ave implmeneted this as an AsyncTask. However, I have 
    // kept the example simple for brevity.
    [Activity(Label = "JsonData")]
    public class JsonData : Activity
    {
        private TextView tvOutputJson;

        // Wire the event handlers as usual.
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.jsondata);

            Button btnGetStationsJson = (Button)FindViewById(Resource.Id.btnGetStationsJson);
            btnGetStationsJson.Click += btnGetStationsJson_Click;
            Button NextButton = (Button)FindViewById(Resource.Id.NextButton);
            NextButton.Click += NextButton_Click;
            Button BackButton = (Button)FindViewById(Resource.Id.BackButton);
            BackButton.Click += BackButton_Click;
            tvOutputJson = (TextView)FindViewById(Resource.Id.tvOutputJson);

        }
        private int currentOffset = 0;
        private const int increment = 50;

        private void BackButton_Click(object sender, EventArgs args)
        {
            if (currentOffset == 0)
            {
                
                Context context = ApplicationContext;
                string text = "No queries available!";
                Toast toast = Toast.MakeText(context, text,
                Android.Widget.ToastLength.Long);
                toast.Show();
                return;
            }
            else
            {
                currentOffset -= increment;
                string baseURL = @"https://www.ncdc.noaa.gov/cdo-web/api/v2/stations?";
                string limitPart = "limit=50";
                string offsetPart = "offset=" + currentOffset.ToString();
                string myURL = baseURL + limitPart + "&" + offsetPart;

                HttpWebRequest requestStations = WebRequest.CreateHttp(myURL);
                //////
                requestStations.Method = "GET";
                requestStations.Headers.Add("token", "vczOaCmkCxBVQHkKGXeVcGoNcrzjdBab");

                HttpWebResponse httpResponse = (HttpWebResponse)requestStations.GetResponse();
                HttpStatusCode i = httpResponse.StatusCode;

                Stream s = httpResponse.GetResponseStream();
                StreamReader sr = new StreamReader(s);
                string resultString = sr.ReadToEnd();

                // The HTTP result is a string containing the unparsed JSON.
                // Call JsonValue.Parse to convert the string into a JSON object.
                JsonValue value = JsonValue.Parse(resultString);

                tvOutputJson.Text = "";
                int resultCount = (int)value["results"].Count;
                // foreach (var i1 in value["results"])
                for (int count = 0; count < resultCount; count++)
                {
                    var data = (value["results"][count]["id"] + " " +
                    value["results"][count]["name"] + " Lat: " + (value["results"][count]["latitude"]).ToString() + " Lon: " +
                    (value["results"][count]["longitude"]).ToString());
                    tvOutputJson.Text += data.ToString() + "\n";
                }

                sr.Close();
            }
        }
        private void NextButton_Click(object sender, EventArgs args)
        {
            currentOffset += increment;
            string baseURL = @"https://www.ncdc.noaa.gov/cdo-web/api/v2/stations?";
            string limitPart = "limit=50";
            string offsetPart = "offset=" + currentOffset.ToString();
            string myURL = baseURL + limitPart + "&" + offsetPart;

            HttpWebRequest requestStations = WebRequest.CreateHttp(myURL);
            //////
            requestStations.Method = "GET";
            requestStations.Headers.Add("token", "vczOaCmkCxBVQHkKGXeVcGoNcrzjdBab");

            HttpWebResponse httpResponse = (HttpWebResponse)requestStations.GetResponse();
            HttpStatusCode i = httpResponse.StatusCode;

            Stream s = httpResponse.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            string resultString = sr.ReadToEnd();

            // The HTTP result is a string containing the unparsed JSON.
            // Call JsonValue.Parse to convert the string into a JSON object.
            JsonValue value = JsonValue.Parse(resultString);

            tvOutputJson.Text = "";
            int resultCount = (int)value["results"].Count;
            // foreach (var i1 in value["results"])
            for (int count = 0; count < resultCount; count++)
            {
                var data = (value["results"][count]["id"] + " " +
                    value["results"][count]["name"] + " Lat: " + (value["results"][count]["latitude"]).ToString() + " Lon: " +
                    (value["results"][count]["longitude"]).ToString());
                tvOutputJson.Text += data.ToString() + "\n";
            }

            sr.Close();
        }

        // NOAA supports supports a number of different datasets. The list of DataSets
        // is a top-level object.
        // Here we have a similar query to get a list of weather stations.
        // 
        protected void btnGetStationsJson_Click(object sender, EventArgs args)
        {
            HttpWebRequest requestStations =
                WebRequest.CreateHttp("https://www.ncdc.noaa.gov/cdo-web/api/v2/stations?limit=50");
            requestStations.Method = "GET";
            requestStations.Headers.Add("token", "vczOaCmkCxBVQHkKGXeVcGoNcrzjdBab");

            HttpWebResponse httpResponse = (HttpWebResponse)requestStations.GetResponse();
            HttpStatusCode i = httpResponse.StatusCode;

            Stream s = httpResponse.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            string resultString = sr.ReadToEnd();

            // The HTTP result is a string containing the unparsed JSON.
            // Call JsonValue.Parse to convert the string into a JSON object.
            JsonValue value = JsonValue.Parse(resultString);

            tvOutputJson.Text = "";
            int resultCount = (int)value["results"].Count;
            // foreach (var i1 in value["results"])
            for (int count = 0; count < resultCount; count++)
            {
                var data = (value["results"][count]["id"] + " " +
                    value["results"][count]["name"] + " Lat: " + (value["results"][count]["latitude"]).ToString() + " Lon: " +
                    (value["results"][count]["longitude"]).ToString());
                tvOutputJson.Text += data.ToString() + "\n";
            }

            sr.Close();
        }

        // Here we get the data types supported by NWS. 

    }
}

// 110 120 130 140
//////MAKE BACK AND NEXT BUTTONS FOR THIS QUERY, INCLUDE LONGITUDE AND LATITUDE 
/////MAKE QUERY ON LOCATION QUERIES 