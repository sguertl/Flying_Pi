﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WiFiDronection
{
   public class CurrentVisualizationData
    {
        // List od data points
        private Dictionary<string, List<DataPoint>> mPoints;

        public Dictionary<string, List<DataPoint>> Points
        {
            get { return mPoints; }
            set { mPoints = value; }
        }

        // Altitude control points
        private List<float> mAltControlTime;

        public List<float> AltControlTime
        {
            get { return mAltControlTime; }
            set { mAltControlTime = value; }
        }

        /// <summary>
        /// Singleton pattern
        /// </summary>
        private static CurrentVisualizationData instance = null;
        private static readonly object padlock = new object();

        /// <summary>
        /// Private constructor
        /// </summary>
        private CurrentVisualizationData()
        {
            this.mPoints = new Dictionary<string, List<DataPoint>>();
            this.AltControlTime = new List<float>();
        }

        public static CurrentVisualizationData Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new CurrentVisualizationData();
                    }
                    return instance;
                }
            }
        }

    }
}
