using EyeTalk.Objects;

namespace EyeTalk.Logic
{
    class OptionsLogic
    {
       public Options Options{ get; set; }
        public SaveFileSerialiser save;
       public OptionsLogic()
        {
             save= new SaveFileSerialiser();
            Options = save.LoadOptions();
            /*
            Options = new Options(0,0,0,0,false);
            save.SaveOptions(Options);
            */
        }

       public void ChangeVoiceType()
        {
            Options.VoiceTypeSelection++;

            if (Options.VoiceTypeSelection > Options.VoiceTypes.Count-1)
            {
                Options.VoiceTypeSelection = 0;
            }
        }

        public void IncreaseVoiceSpeed()
        {
            Options.VoiceSpeedSelection++;

            if (Options.VoiceSpeedSelection > 2)
            {
                Options.VoiceSpeedSelection = 0;
            }
        }

        public void DecreaseVoiceSpeed()
        {
            Options.VoiceSpeedSelection--;
            if (Options.VoiceSpeedSelection < 0)
            {
                Options.VoiceSpeedSelection = 2;
            }
        }

        public void IncreaseSelectionDelay()
        {
            if (Options.EyeFixationValue < 21)
            {
                Options.EyeFixationValue++;
            }
        }

        public void DecreaseSelectionDelay()
        {
            if (Options.EyeFixationValue > 1)
            {
                Options.EyeFixationValue--;
            }
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
