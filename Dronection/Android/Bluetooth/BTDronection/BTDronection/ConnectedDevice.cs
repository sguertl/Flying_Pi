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
using Android.Graphics.Drawables;

namespace BTDronection
{

    [Activity(Label = "ConnectedDevice" ,Theme = "@android:style/Theme.Light.NoTitleBar.Fullscreen",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ConnectedDevices : Activity
    {
        // Members
        private TextView m_ViewName; // TextView for displaying the device's name
        private TextView m_ViewAddress; // TextView for displaying the device's address
        private Button m_BtControl; // Button for controlling the device
        private Button m_BtDisconnect; // Button for disconnecting from the device
        private LinearLayout m_Linear;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ConnectedDevicesLayout);

            Init();
        }

        protected override void OnStop()
        {
            base.OnStop();
            ConnectedThread.Cancel();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ConnectedThread.Cancel();
        }
        /// <summary>
        /// Initializes and modifies the members
        /// </summary>
        public void Init()
        {
            // Initializing objects
            m_ViewName = FindViewById<TextView>(Resource.Id.DeviceName);
            m_ViewAddress = FindViewById<TextView>(Resource.Id.DeviceAdresse);
            m_BtControl = FindViewById<Button>(Resource.Id.btSteueren);
            m_BtDisconnect = FindViewById<Button>(Resource.Id.btDisconnect);
            m_Linear = FindViewById<LinearLayout>(Resource.Id.linear4);

            // Setting text color of buttons
            m_BtControl.SetBackgroundColor(Android.Graphics.Color.DeepSkyBlue);
            m_BtDisconnect.SetBackgroundColor(Android.Graphics.Color.DeepSkyBlue);
            m_BtControl.SetTextColor(Android.Graphics.Color.White);
            m_BtDisconnect.SetTextColor(Android.Graphics.Color.White);

            // Setting activity background
            m_Linear.SetBackgroundColor(Android.Graphics.Color.White);

            // Receiving data from other activities
            IList<String> text = Intent.GetStringArrayListExtra("MyData");
            m_ViewName.Text = text.ElementAt(0);
            m_ViewAddress.Text = text.ElementAt(1);

            // Setting text color of textviews
            m_ViewName.SetTextColor(Android.Graphics.Color.Black);
            m_ViewAddress.SetTextColor(Android.Graphics.Color.Black);

            // Handling button contact
           m_BtControl.Click += delegate
           {
               if (ConnectedThread.m_Socket.IsConnected)
               { 
                   StartActivity(typeof(ControllerActivity));
               }
           };

            m_BtDisconnect.Click += delegate
            {
                ConnectedThread.Cancel();
                StartActivity(typeof(MainActivity));
            };
        }
    }
}