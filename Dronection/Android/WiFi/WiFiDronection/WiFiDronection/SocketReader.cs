﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.IO;
using System.IO;
using Android.Util;

namespace WiFiDronection
{
    public class SocketReader : Thread
    {
        private DataInputStream mDataInputStream;
        
        public SocketReader(Stream inputStream)
        {
            mDataInputStream = new DataInputStream(inputStream);
        }

        public override void Run()
        {
            int bytes;
            byte[] buffer = new byte[256];
            while (true)
            {
                try
                {
                    bytes = mDataInputStream.Read(buffer);
                    string msg = new Java.Lang.String(buffer, 0, bytes).ToString();
                }
                catch(Java.IO.IOException ex)
                {
                    Log.Debug("SocketReader", "Fehler beim Lesen");
                }
            }
        }

        public void Close()
        {
            mDataInputStream.Close();
        }
    }
}