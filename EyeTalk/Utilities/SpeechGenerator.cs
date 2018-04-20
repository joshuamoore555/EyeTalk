using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;


namespace EyeTalk.Utilities
{
    public class SpeechGenerator
    {
        SpeechSynthesizer synthesizer;
        List<InstalledVoice> voices;
        public List<string> voiceGendersAvailable = new List<string>(); //stores what voices are installed
        public Dictionary<string, int> voiceElement;

        public SpeechGenerator()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;
            synthesizer.Rate = 0;

            voices = new List<InstalledVoice>();

            GetInstalledVoices();

        }

        private void GetInstalledVoices()
        {        
            //This method finds what voices are installed on the user's computer, and in what order

            int numberOfVoices = 0; //stores the number of installed voices

            voiceElement = new Dictionary<string, int>(); //stores the element that the voice type is stored at

            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                voices.Add(voice);
                var voiceGender = voice.VoiceInfo.Gender.ToString();
                voiceGendersAvailable.Add(voiceGender);

                if (!voiceElement.ContainsKey(voiceGender))
                {
                    voiceElement.Add(voiceGender, numberOfVoices); //adds what element the gender's voice is installed - this is required as they can be installed in different orders
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
            //If female gender voice is installed, retrieve it and use it
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
            //If male gender voice is installed, retrieve it and use it
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
