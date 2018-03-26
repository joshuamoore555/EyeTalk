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
            synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;
            synthesizer.Rate = 0;

            voices = new List<InstalledVoice>();

            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                voices.Add(voice);
            }
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
