using System.Threading.Tasks;
using Tobii.EyeX.Framework;
using EyeXFramework;
using EyeTalk.Objects;


namespace EyeTalk
{
    public class EyeTracker
    {
        public XYCoordinate coordinates;
        public volatile bool eyeTracking;
        public EyeXHost eyeXHost;


        public EyeTracker()
        {
            coordinates = new XYCoordinate(0,0);
            eyeXHost = new EyeXHost();
            eyeXHost.Start();

            StartEyeTracking();
        }


        public void StartEyeTracking()
        {
            eyeTracking = true;
            
            
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
            var presence = CheckPresenceOfUser();

            if (presence == true)
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
            else
            {
                return "";
            }        
        }

        public string CheckY()
        {
            var presence = CheckPresenceOfUser();

            if (presence == true)
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
            else
            {
                return "";
            }
        }


        //Check Pages methods

        public string CheckXHomePage()
        {
            if (coordinates.X > 0 && coordinates.X < 960)
            {
                return "Left";

            }
            else if (coordinates.X > 960 && coordinates.X < 1920)
            {
                return "Right";
            }
            else
            {
                return "";
            }
        }

        public string CheckXSavedSentencesPage()
        {
            if (coordinates.X < 480)
            {
                return "Left";
            }
            else if (coordinates.X > 480 && coordinates.X < 1440 && coordinates.Y > 720)
            {
                return "Middle Right";

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
                return "";
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
            else if (coordinates.X > 1440 && coordinates.X < 1600)
            {
                return "Right";

            }
            else if (coordinates.X > 1600  && coordinates.Y > 720)
            {
                return "Right Alternate";

            }
            else if (coordinates.X > 1600 && coordinates.Y < 720)
            {
                return "Right";

            }
            else
            {
                return "";
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
                return "";
            }
        }

        public string CheckCurrentPositionKeyboard()
        {
            if(coordinates.Y <= 240)
            {
                return CheckTopRowOfKeyboard();
            }
            else if (coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return CheckMiddleRowOfKeyboard();
            }
            else if (coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return CheckBottomRowOfKeyboard();
            }

            else if (coordinates.Y >= 870)
            {
                return CheckFunctionRowOfKeyboard();
            }
            else
            {
                return "";
            }
        }

        public string CheckTopRowOfKeyboard()
        {

            if (coordinates.X <= 180 && coordinates.Y <= 240)
            {
                return "Q";
            }

            else if (coordinates.X > 180 && coordinates.X <= 360 && coordinates.Y <= 240)
            {
                return "W";

            }
            else if (coordinates.X > 360 && coordinates.X <= 540 && coordinates.Y <= 240)
            {
                return "E";

            }
            else if (coordinates.X > 540 && coordinates.X <= 720 && coordinates.Y <= 240)
            {
                return "R";

            }
            else if (coordinates.X > 720 && coordinates.X <= 900 && coordinates.Y <= 240)
            {
                return "T";

            }
            else if (coordinates.X > 900 && coordinates.X <= 1080 && coordinates.Y <= 240)
            {
                return "Y";

            }
            else if (coordinates.X > 1080 && coordinates.X <= 1260 && coordinates.Y <= 240)
            {
                return "U";

            }
            else if (coordinates.X > 1260 && coordinates.X <= 1440 && coordinates.Y <= 240)
            {
                return "U";

            }
            else if (coordinates.X > 1440 && coordinates.X <= 1620 && coordinates.Y <= 240)
            {
                return "I";

            }
            else if (coordinates.X > 1620 && coordinates.X <= 1800 && coordinates.Y <= 240)
            {
                return "O";

            }
            else if (coordinates.X > 1800 && coordinates.Y <= 240)
            {
                return "P";

            }
            else return "";
        }

        public string CheckMiddleRowOfKeyboard()
        {
            if (coordinates.X <= 180 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "A";
            }

            else if (coordinates.X > 180 && coordinates.X <= 360 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "S";

            }
            else if (coordinates.X > 360 && coordinates.X <= 540 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "D";

            }
            else if (coordinates.X > 540 && coordinates.X <= 720 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "F";

            }
            else if (coordinates.X > 720 && coordinates.X <= 900 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "G";

            }
            else if (coordinates.X > 900 && coordinates.X <= 1080 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "H";

            }
            else if (coordinates.X > 1080 && coordinates.X <= 1260 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "J";

            }
            else if (coordinates.X > 1260 && coordinates.X <= 1440 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "K";

            }
            else if (coordinates.X > 1440 && coordinates.X <= 1620 && coordinates.Y >= 300 && coordinates.Y < 580)
            {
                return "L";

            }

            else return "";
        }

        public string CheckBottomRowOfKeyboard()
        {
            if (coordinates.X <= 180 && coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return "Z";
            }

            else if (coordinates.X > 180 && coordinates.X <= 360 && coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return "X";

            }
            else if (coordinates.X > 360 && coordinates.X <= 540 && coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return "C";

            }
            else if (coordinates.X > 540 && coordinates.X <= 720 && coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return "V";

            }
            else if (coordinates.X > 720 && coordinates.X <= 900 && coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return "B";

            }
            else if (coordinates.X > 900 && coordinates.X <= 1080 && coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return "N";

            }
            else if (coordinates.X > 1080 && coordinates.X <= 1260 && coordinates.Y >= 580 && coordinates.Y < 870)
            {
                return "M";

            }

            else return "";
        }

        public string CheckFunctionRowOfKeyboard()
        {
            if (coordinates.X <= 360 && coordinates.Y >= 870)
            {
                return "Back";
            }

            else if (coordinates.X > 435 && coordinates.X <= 795 && coordinates.Y >= 870)
            {
                return "Space";

            }
            else if (coordinates.X > 870 && coordinates.X <= 1230 && coordinates.Y >= 870)
            {
                return "Delete";

            }
            else if (coordinates.X > 1305 && coordinates.X <= 1665 && coordinates.Y >= 870)
            {
                return "Enter";

            }
           
            else return "";
        }



        //Get methods

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

        public string GetCurrentPositionSavedSentence()
        {
            return CheckY() + " " + CheckXSavedSentencesPage();
        }

        public string GetCurrentPositionHomePage()
        {
            return CheckY() + " " + CheckXHomePage();
        }

        public string GetCurrentPositionKeyboard()
        {
            return CheckCurrentPositionKeyboard();
        }

        public bool CheckPresenceOfUser()
        {
            if (eyeXHost.UserPresence.ToString() == "Present")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetUserProfileName()
        {
            return eyeXHost.UserProfileName.Value.ToString();
        }

    }
}
