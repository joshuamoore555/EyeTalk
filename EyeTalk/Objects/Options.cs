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
        public bool AdditionalEyeInformation { get; set; }



        public Options(double EyeFixationDuration, double EyeFixationValue, int VoiceSpeedSelection, int VoiceTypeSelection, bool AdditionalEyeInformation)
        {

            this.EyeFixationDuration = EyeFixationDuration;
            this.EyeFixationValue = EyeFixationValue;
            this.VoiceSpeedSelection = VoiceSpeedSelection;
            this.VoiceTypeSelection = VoiceTypeSelection;
            this.AdditionalEyeInformation = AdditionalEyeInformation;
        }


    }
}
