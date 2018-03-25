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
        public List<String> VoiceSpeeds { get; }
        public List<String> VoiceTypes { get; }


        public Options(double EyeFixationDuration, double EyeFixationValue, int VoiceSpeedSelection, int VoiceTypeSelection, bool AdditionalEyeInformation)
        {
            VoiceSpeeds = new List<String> { "Slow", "Normal", "Fast" };
            VoiceTypes = new List<String> { "Male", "Female" };
            this.EyeFixationDuration = EyeFixationDuration;
            this.EyeFixationValue = EyeFixationValue;
            this.VoiceSpeedSelection = VoiceSpeedSelection;
            this.VoiceTypeSelection = VoiceTypeSelection;
            this.AdditionalEyeInformation = AdditionalEyeInformation;
        }


    }
}
