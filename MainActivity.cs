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

namespace CunninghamTannerAssn3
{
    [Activity(Label = "Startup", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.startup);

            Button btnTask1Query = (Button)FindViewById(Resource.Id.btnTask1Query);
            btnTask1Query.Click += btnTask1Query_Click;
            Button btnTask2Query = (Button)FindViewById(Resource.Id.btnTask2Query);
            btnTask2Query.Click += btnTask2Query_Click;

            // Create your application here
        }
        protected void btnTask1Query_Click(object sender, EventArgs args)
        {
            Intent networkInfo = new Intent(this, typeof(LocationCatagories));
            StartActivity(networkInfo);
        }

        protected void btnTask2Query_Click(object sender, EventArgs args)
        {
            Intent asyncActivity = new Intent(this, typeof(JsonData));
            StartActivity(asyncActivity);
        }
    }
}