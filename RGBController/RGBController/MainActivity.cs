using Android.App;
using Android.Widget;
using Android.OS;

namespace RGBController
{
    [Activity(Label = "RGBController", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button mainbtn1 = FindViewById<Button>(Resource.Id.mainbtn1);
            mainbtn1.Click += (o, e) => StartActivity(typeof(ChangeColour));

            Button mainbtn2 = FindViewById<Button>(Resource.Id.mainbtn2);
            mainbtn2.Click += (o, e) => StartActivity(typeof(LogIn));

            Button mainbtn3 = FindViewById<Button>(Resource.Id.mainbtn3);
            mainbtn3.Click += (o, e) => StartActivity(typeof(About));
        }
    }
}

