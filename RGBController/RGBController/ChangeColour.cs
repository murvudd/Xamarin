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
using Microsoft.AspNet.SignalR.Client;

namespace RGBController
{
    [Activity(Label = "ChangeColour")]
    public class ChangeColour : Activity
    {

        protected HubConnection connection = new HubConnection("http://kolejkomatapp4.azurewebsites.net/signalr/hubs");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChangeColour);
            // Create your application here
            var myHub = connection.CreateHubProxy("mainHub");
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                  task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }
            }).Wait();
            //myHub.Invoke<string>("Hello", "witam z apki").Wait();

            FindViewById<SeekBar>(Resource.Id.seekBarRed).ProgressChanged += (s, e) =>
            {
                FindViewById<TextView>(Resource.Id.textViewR).Text = string.Format("R {0}", e.Progress);
                myHub.Invoke<string>("Hello", "Red " + e.Progress);
            };

            FindViewById<SeekBar>(Resource.Id.seekBarGreen).ProgressChanged += (s, e) =>
            {
                FindViewById<TextView>(Resource.Id.textViewG).Text = string.Format("G {0}", e.Progress);
                myHub.Invoke<string>("Hello", "Green " + e.Progress);
            };

            FindViewById<SeekBar>(Resource.Id.seekBarBlue).ProgressChanged += (s, e) =>
            {
                FindViewById<TextView>(Resource.Id.textViewB).Text = string.Format("B {0}", e.Progress);
                myHub.Invoke<string>("Hello", "Blue " + e.Progress);
            };

            //connection.Stop();

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            connection.Stop();
        }
    }
}