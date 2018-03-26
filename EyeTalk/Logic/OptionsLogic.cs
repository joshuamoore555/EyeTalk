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
        public List<String> VoiceTypes = new List<String> { "Male", "Female" };

        public OptionsLogic()
        {
            save= new SaveFileSerialiser();
            Options = save.LoadOptions();
            /*
            Options = new Options(0,0,0,0,false);
            save.SaveOptions(Options);
            */
        }

        public void IncreaseEyeFixationDuration()
        {
            Options.EyeFixationDuration++;
        }

        public void ResetEyeFixationDuration()
        {
            Options.EyeFixationDuration++;
        }

        public bool HasDurationBeenReached()
        {
            if (Options.EyeFixationDuration > Options.EyeFixationValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        public string IncreaseSelectionDelay()
        {
            if (Options.EyeFixationValue < 21)
            {
                Options.EyeFixationValue++;
            }
            return Options.EyeFixationValue / 4 + " Seconds";
        }

        public string DecreaseSelectionDelay()
        {
            if (Options.EyeFixationValue > 1)
            {
                Options.EyeFixationValue--;
            }
            return Options.EyeFixationValue / 4 + " Seconds";
        }

        public void SaveOptionsIfNotNull()
        {
            if(Options != null)
            {
                save.SaveOptions(Options);
            }
        }
    }
}
