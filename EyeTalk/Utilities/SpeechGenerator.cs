using EyeTalk.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Utilities
{
    public class SpeechGenerator
    {
        SpeechSynthesizer synthesizer;
        List<InstalledVoice> voices;

        public SpeechGenerator()
        {
            OptionsLogic optionsLogic = new OptionsLogic();
            synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;
            synthesizer.Rate = 0;

            voices = new List<InstalledVoice>();

            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                voices.Add(voice);
            }

            ChooseVoice(optionsLogic.VoiceTypes.ElementAt(optionsLogic.Options.VoiceTypeSelection));
            ChooseSpeedOfVoice(optionsLogic.VoiceSpeeds.ElementAt(optionsLogic.Options.VoiceSpeedSelection));

        }

        public void ChooseSpeedOfVoice(string speed)
        {
            if (speed == "Fast")
            {
               SelectFastVoice();
            }
            else if (speed == "Normal")
            {
                SelectNormalVoice();
            }
            else if (speed == "Slow")
            {
                SelectSlowVoice();
            }
        }

        public void ChooseVoice(string type)
        {
            if (type == "Female")
            {
                SelectFemaleVoice();
            }
            else if (type == "Male")
            {
                SelectMaleVoice();
            }
        }

        public void SelectFemaleVoice()
        {
            synthesizer.SelectVoice(voices.ElementAt(0).VoiceInfo.Name);
        }

        public void SelectMaleVoice()
        {
            synthesizer.SelectVoice(voices.ElementAt(1).VoiceInfo.Name);
        }

        public void SelectFastVoice()
        {
            synthesizer.Rate = 3;
        }

        public void SelectNormalVoice()
        {
            synthesizer.Rate = 0;

        }

        public void SelectSlowVoice()
        {
            synthesizer.Rate = -3;
        }

        public void Speak(string text)
        {
            synthesizer.Speak(text);
        }
    }
}
