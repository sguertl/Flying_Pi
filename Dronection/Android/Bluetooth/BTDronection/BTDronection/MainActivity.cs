﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Util;

namespace BTDronection
{
    [Activity(Label = "BTDronection", MainLauncher = true, Icon = "@drawable/icon", 
        Theme = "@android:style/Theme.Light.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        // Members
        private BluetoothAdapter m_BtAdapter;
        private Button m_BtPairedDevices;
        private Button m_BtSearchDevices;
        private LinearLayout m_Linear;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            //h
            Init();

            // Checking if bluetooth is supported
            if (m_BtAdapter == null)
            {
                Toast.MakeText(ApplicationContext, "Bluetooth is not supported", 0).Show();
                m_BtSearchDevices.Enabled = false;
                m_BtSearchDevices.SetTextColor(Android.Graphics.Color.LightGray);
                m_BtPairedDevices.Enabled = false;
                m_BtPairedDevices.SetTextColor(Android.Graphics.Color.LightGray);

                // Displaying an alert to inform the user that bluetooth is not supported
                AlertDialog alert = new AlertDialog.Builder(this).Create();
                alert.SetTitle("Bluetooth not supported");
                alert.SetMessage("Bluetooth is not supported!");
                alert.SetButton("Ok", (s, ev) => { Finish(); });
                alert.Show();
            }
            else
            {
                // Checking if bluetooth is enabled
                if (!m_BtAdapter.IsEnabled) { TurnBTOn(); }
            }
        }

        /// <summary>
        /// Initializing and modifies objects
        /// </summary>
        public void Init()
        {
            // Initializing objects
            m_BtAdapter = BluetoothAdapter.DefaultAdapter;
            m_BtPairedDevices = FindViewById<Button>(Resource.Id.btPairedDevices);
            m_BtSearchDevices = FindViewById<Button>(Resource.Id.btSearchDevices);
            m_Linear = FindViewById<LinearLayout>(Resource.Id.linear);

            // Setting text color of the buttons
            m_BtPairedDevices.SetBackgroundColor(Android.Graphics.Color.DeepSkyBlue);
            m_BtSearchDevices.SetBackgroundColor(Android.Graphics.Color.DeepSkyBlue);
            m_BtPairedDevices.SetTextColor(Android.Graphics.Color.White);
            m_BtSearchDevices.SetTextColor(Android.Graphics.Color.White);
            
            // Setting activity background
            m_Linear.SetBackgroundColor(Android.Graphics.Color.White);

            // Handling search devices button click
            m_BtSearchDevices.Click += delegate
            {
                if(m_BtAdapter.IsEnabled)
                {
                    //StartActivity(typeof(SearchDevices));
                    Toast.MakeText(this, "Pair with device", ToastLength.Short).Show();
                    Intent bluetoothSettings = new Intent();
                    bluetoothSettings.SetAction(Android.Provider.Settings.ActionBluetoothSettings);
                    StartActivity(bluetoothSettings);
                }
                else
                {
                    Toast.MakeText(this, "Bluetooth has to be turned on", ToastLength.Short).Show();
                }
            };

            // Handling paired devices button click
            m_BtPairedDevices.Click += delegate
            {
                if (m_BtAdapter.IsEnabled)
                {
                    StartActivity(typeof(PairedDevices));
                }
                else
                {
                    Toast.MakeText(this, "Bluetooth has to be turned on", ToastLength.Short).Show();
                }
            };
        }

        /// <summary>
        /// Enables bluetooth on the device
        /// </summary>
        public void TurnBTOn()
        {
            Intent intent = new Intent(BluetoothAdapter.ActionRequestEnable);
            StartActivityForResult(intent, 1);
        }
    }
}

