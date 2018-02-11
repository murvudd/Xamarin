using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RGBController.LogIn;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AspNet.SignalR.Client;
using Android.Graphics;

namespace RGBController
{
    [Activity(Label = "ChangeColour")]
    public class ChangeColour : Activity
    {

        //protected HubConnection connection = new HubConnection("http://kolejkomatapp4.azurewebsites.net/signalr/hubs");
        protected HubConnection connection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChangeColour);
            string serverURL = Intent.GetStringExtra("MyData") ?? "http://kolejkomatapp4.azurewebsites.net/signalr/hubs";
            connection = new HubConnection(serverURL);

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

            var RedSeekBar = FindViewById<SeekBar>(Resource.Id.seekBarRed);
            var GreenSeekBar = FindViewById<SeekBar>(Resource.Id.seekBarGreen);
            var BlueSeekBar = FindViewById<SeekBar>(Resource.Id.seekBarBlue);
            var ColourButton = FindViewById<Button>(Resource.Id.ColourBtn);

            var tesst = int.Parse(Math.Floor(2.55m * RedSeekBar.Progress).ToString());
            int r, g, b;
            r = int.Parse(Math.Floor(2.55m * RedSeekBar.Progress).ToString());
            g = int.Parse(Math.Floor(2.55m * GreenSeekBar.Progress).ToString());
            b = int.Parse(Math.Floor(2.55m * BlueSeekBar.Progress).ToString());

            RedSeekBar.ProgressChanged += (s, e) =>
            {
                FindViewById<TextView>(Resource.Id.textViewR).Text = string.Format("R {0}", e.Progress);
                myHub.Invoke<string>("Hello", "Red " + e.Progress);

                ColourButton.SetBackgroundColor(new Color(
                    int.Parse(Math.Floor(2.55m * e.Progress).ToString()),
                    int.Parse(Math.Floor(2.55m * GreenSeekBar.Progress).ToString()),
                    int.Parse(Math.Floor(2.55m * BlueSeekBar.Progress).ToString())
                    ));
            };

            GreenSeekBar.ProgressChanged += (s, e) =>
            {
                FindViewById<TextView>(Resource.Id.textViewG).Text = string.Format("G {0}", e.Progress);
                myHub.Invoke<string>("Hello", "Green " + e.Progress);
                ColourButton.SetBackgroundColor(new Color(
                    int.Parse(Math.Floor(2.55m * RedSeekBar.Progress).ToString()),
                    int.Parse(Math.Floor(2.55m * e.Progress).ToString()),
                    int.Parse(Math.Floor(2.55m * BlueSeekBar.Progress).ToString())
                    ));
            };

            BlueSeekBar.ProgressChanged += (s, e) =>
            {
                FindViewById<TextView>(Resource.Id.textViewB).Text = string.Format("B {0}", e.Progress);
                myHub.Invoke<string>("Hello", "Blue " + e.Progress);
                ColourButton.SetBackgroundColor(new Color(
                    int.Parse(Math.Floor(2.55m * RedSeekBar.Progress).ToString()),
                    int.Parse(Math.Floor(2.55m * GreenSeekBar.Progress).ToString()),
                    int.Parse(Math.Floor(2.55m * e.Progress).ToString())
                    ));
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