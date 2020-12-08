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
    [Activity(Label = "LocationCatagories")]
    public class LocationCatagories : Activity
    {
        private TextView tvOutputJsonLocations;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.locations);

            Button btnGetDatasetsLocations = (Button)FindViewById(Resource.Id.btnGetDatasetsLocations);
            btnGetDatasetsLocations.Click += btnGetDatasetsLocations_Click;

            tvOutputJsonLocations = (TextView)FindViewById(Resource.Id.tvOutputJsonLocations);

            // Create your application here
        }
        protected void btnGetDatasetsLocations_Click(object sender, EventArgs args)
        {
            HttpWebRequest requestStations =
                WebRequest.CreateHttp("https://www.ncdc.noaa.gov/cdo-web/api/v2/locationcategories");
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

            tvOutputJsonLocations.Text = "";
            int resultCount = (int)value["results"].Count;
            // foreach (var i1 in value["results"])
            for (int count = 0; count < resultCount; count++)
            {
                tvOutputJsonLocations.Text += (value["results"][count]["id"] + " " +
                    value["results"][count]["name"]) + "\n";
            }

            sr.Close();
        }

    }
}