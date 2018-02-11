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

namespace RGBController
{
    [Activity(Label = "LogIn")]
    public class LogIn : Activity
    {
        public string serverURL;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.LogIn);

            Button savebtn= FindViewById<Button>(Resource.Id.loginBtnSave);
            savebtn.Click += (o, e) => {
                serverURL = FindViewById<EditText>(Resource.Id.LogIn_EditText1).Text;
                var activity = new Intent(this, typeof(ChangeColour));
                activity.PutExtra("MyData", serverURL);
                StartActivity(activity);
            };
        }
    }
}