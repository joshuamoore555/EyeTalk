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
                    await Task.Delay(200);
                }

            });
        }

        public void SetXY(double X, double Y)
        {
            coordinates.X = X;
            coordinates.Y = Y;
        }

        public string CheckX()
        {
            if (coordinates.X < 480)
            {
                return "Left";
            }
            else if (coordinates.X > 480 && coordinates.X < 960)
            {
                return "Middle Left";

            }
            else if (coordinates.X > 960 && coordinates.X < 1440)
            {
                return "Middle Right";

            }
            else if (coordinates.X > 1440 && coordinates.X < 1920)
            {
                return "Right";

            }
            else
            {
                return coordinates.X.ToString();
            }
        }

        public string CheckXOfBeginSpeakingPage()
        {
            if (coordinates.X < 240 && coordinates.Y < 360 || coordinates.X < 240 && coordinates.Y > 720)
            {
                return "Left Alternate";
            }

            else if (coordinates.X < 240 && coordinates.Y > 360 && coordinates.X < 720)
            {
                return "Left";

            }
            else if (coordinates.X > 240 && coordinates.X < 480)
            {
                return "Left";

            }
            else if (coordinates.X > 480 && coordinates.X < 960)
            {
                return "Middle Left";

            }
            else if (coordinates.X > 960 && coordinates.X < 1440)
            {
                return "Middle Right";

            }
            else if (coordinates.X > 1440 && coordinates.X < 1680)
            {
                return "Right";

            }
            else if (coordinates.X > 1680 && coordinates.X < 1920 && coordinates.Y > 720)
            {
                return "Right Alternate";

            }
            else if (coordinates.X > 1680 && coordinates.X < 1920 && coordinates.Y < 720)
            {
                return "Right";

            }
            else
            {
                return coordinates.X.ToString();
            }
        }

        public string CheckXOfChooseCategoryPage()
        {
            if (coordinates.X < 240)
            {
                return "Left Alternate";
            }

            else if (coordinates.X > 240 && coordinates.X < 480)
            {
                return "Left";

            }
            else if (coordinates.X > 480 && coordinates.X < 720)
            {
                return "Middle Left Alternate";

            }
            else if (coordinates.X > 720 && coordinates.X < 960)
            {
                return "Middle Left";

            }
            else if (coordinates.X > 960 && coordinates.X < 1200)
            {
                return "Middle Right Alternate";

            }
            else if (coordinates.X > 1200 && coordinates.X < 1440)
            {
                return "Middle Right";

            }
            else if (coordinates.X > 1440 && coordinates.X < 1680)
            {
                return "Right";

            }
            else if (coordinates.X > 1680 && coordinates.X < 1920)
            {
                return "Right Alternate";

            }
            else
            {
                return coordinates.X.ToString();
            }
        }

        public string CheckY()
        {
            if (coordinates.Y < 360)
            {
                return "Top";
            }
            else if (coordinates.Y > 360 && coordinates.Y < 720)
            {
                return "Middle";
            }
            else if (coordinates.Y > 720 && coordinates.Y < 1080)
            {
                return "Bottom";
            }
            else
            {
                return coordinates.Y.ToString();
            }
        }

        public string GetCurrentPosition()
        {
            return CheckY() + " " + CheckX();
        }

        public string GetCurrentPositionBeginSpeaking()
        {
            return CheckY() + " " + CheckXOfBeginSpeakingPage();
        }

        public string GetCurrentPositionChooseCategory()
        {
            return CheckY() + " " + CheckXOfChooseCategoryPage();
        }
    }
}
