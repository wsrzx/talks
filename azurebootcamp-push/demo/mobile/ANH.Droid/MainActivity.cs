using Android.App;
using Android.Widget;
using Android.OS;
using Gcm;
using Android.Util;

namespace ANH.Droid
{
    [Activity(Label = "ANH", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        public static MainActivity Instance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            RegisterWithGCM();

        }

        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            GcmClient.Register(this, Constants.SenderID);

            // Register for push notifications
            Log.Info("MainActivity", "Registering...");
        }
    }
}

