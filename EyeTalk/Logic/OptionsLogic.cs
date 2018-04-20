using EyeTalk.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeTalk.Logic
{
    class OptionsLogic
    {
       public Options Options{ get; set; }
       public SaveFileSerialiser save;
       public List<String> VoiceSpeeds = new List<String> { "Slow", "Normal", "Fast" };
       public List<String> VoiceTypes = new List<String> {  "Female","Male" };
       public List<String> Colours = new List<String> {"#FF1493", "#ff6a6a", "#8470ff", "#00fa9a","#ff4500", "#fff8dc", "#cae1ff", "#d8bfd8" };

        public OptionsLogic()
        {
            save = new SaveFileSerialiser();
            Options = save.LoadOptions();
        }

        //Colour methods

        public string GetCurrentColour()
        {
            //gets the colour based on the application colour index
            return Colours.ElementAt(Options.ApplicationColour);
        }

        public void ChangeColour()
        {
            //Increases the application's colour index
            Options.ApplicationColour++;

            if (Options.ApplicationColour > Colours.Count - 1)
            {
                Options.ApplicationColour = 0;
            }
        }

        //Eye Duration methods

        public void IncreaseEyeFixationDuration()
        {
            Options.EyeFixationDuration++;
        }

        public void ResetEyeFixationDuration()
        {
            Options.EyeFixationDuration = 0;
        }

        public bool HasDurationBeenReached()
        {
            //checks if the duration has been reached or not
            if (Options.EyeFixationDuration >= Options.EyeFixationValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Voice methods

        public string ChangeVoiceType()
        {
            Options.VoiceTypeSelection++;

            if (Options.VoiceTypeSelection > VoiceTypes.Count-1)
            {
                Options.VoiceTypeSelection = 0;
            }

            if(Options.VoiceTypeSelection == 0)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }

        }

        public string IncreaseVoiceSpeed()
        {
            Options.VoiceSpeedSelection++;

            if (Options.VoiceSpeedSelection > 2)
            {
                Options.VoiceSpeedSelection = 0;
            }

            return VoiceSpeeds.ElementAt(Options.VoiceSpeedSelection);
        }

        public string DecreaseVoiceSpeed()
        {
            Options.VoiceSpeedSelection--;
            if (Options.VoiceSpeedSelection < 0)
            {
                Options.VoiceSpeedSelection = 2;
            }
            return VoiceSpeeds.ElementAt(Options.VoiceSpeedSelection);

        }

        //Eye Fixation methods

        public string IncreaseSelectionDelay()
        {
            if (Options.EyeFixationValue < 40)
            {
                Options.EyeFixationValue++;
            }
            return Options.EyeFixationValue / 5 + " Seconds";
        }

        public string DecreaseSelectionDelay()
        {
            if (Options.EyeFixationValue > 2)
            {
                Options.EyeFixationValue--;
            }
            return Options.EyeFixationValue / 5 + " Seconds";
        }
    }
}
