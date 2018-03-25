using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Speech.Synthesis;
using System.Collections.Specialized;
using System.Collections;
using Tobii.EyeX.Framework;
using EyeXFramework;
using EyeTalk.Objects;


namespace EyeTalk
{
    public class EyeTracker
    {
        public XYCoordinate coordinates;
        public volatile bool eyeTracking;


        public EyeTracker()
        {
            coordinates = new XYCoordinate(0,0);
            StartEyeTracking();
        }


        public void StartEyeTracking()
        {
            eyeTracking = true;
            var eyeXHost = new EyeXHost();
            eyeXHost.Start();
            var stream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);

            Task.Run(async () =>
            {
                while (eyeTracking)
                {
                    stream.Next += (s, t) => SetXY(t.X, t.Y);
                    await Task.Delay(125);
                }

            });
        }

        private void SetXY(double X, double Y)
        {
            coordinates.X = X;
            coordinates.Y = Y;
        }
    }
}
