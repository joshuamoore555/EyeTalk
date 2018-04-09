using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Objects
{
    [Serializable]
    public class Options
    {
        public double EyeFixationDuration { get; set; }
        public double EyeFixationValue { get; set; }
        public int VoiceSpeedSelection { get; set; }
        public int VoiceTypeSelection { get; set; }
        public int ApplicationColour { get; set; }



        public Options(double EyeFixationDuration, double EyeFixationValue, int VoiceSpeedSelection, int VoiceTypeSelection, int ApplicationColour)
        {

            this.EyeFixationDuration = EyeFixationDuration;
            this.EyeFixationValue = EyeFixationValue;
            this.VoiceSpeedSelection = VoiceSpeedSelection;
            this.VoiceTypeSelection = VoiceTypeSelection;
            this.ApplicationColour = ApplicationColour;
        }


    }
}
