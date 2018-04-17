using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;


namespace EyeTalk.Utilities
{
    public class SpeechGenerator
    {
        SpeechSynthesizer synthesizer;
        List<InstalledVoice> voices;
        public List<string> voiceGendersAvailable = new List<string>();
        public Dictionary<string, int> voiceElement;

        public SpeechGenerator()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;
            synthesizer.Rate = 0;

            voices = new List<InstalledVoice>();
            int numberOfVoices = 0;
            voiceElement = new Dictionary<string, int>();

            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                voices.Add(voice);
                voiceGendersAvailable.Add(voice.VoiceInfo.Gender.ToString());

                if (!voiceElement.ContainsKey(voice.VoiceInfo.Gender.ToString()))
                {
                    voiceElement.Add(voice.VoiceInfo.Gender.ToString(), numberOfVoices);
                }

                numberOfVoices++;
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

        public string ChooseVoice(string type)
        {
            if (type == "Female")
            {
                return SelectFemaleVoice();
            }
            else if (type == "Male")
            {
                return SelectMaleVoice();
            }
            else return "No Voices Installed";
        }

        public string SelectFemaleVoice()
        {

            foreach (var gender in voiceGendersAvailable)
            {
                if (gender == "Female")
                {
                    var element = voiceElement["Female"];
                    synthesizer.SelectVoice(voices.ElementAt(element).VoiceInfo.Name);
                    return "Voice Type: Female";
                }
            }

            return "No Female Voice Installed";
        }

        public string SelectMaleVoice()
        {
            foreach (var gender in voiceGendersAvailable)
            {
                if (gender == "Male")
                {
                    var element = voiceElement["Male"];
                    synthesizer.SelectVoice(voices.ElementAt(element).VoiceInfo.Name);
                    return "Voice Type: Male";
                }
            }

            return "No Male Voice Installed";


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
