using EyeTalk.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Logic
{
    public class SavedSentencesLogic
    {
        public List<String> savedSentences { get; set; }

        public SavedSentencesLogic()
        {
            SaveFileSerialiser save = new SaveFileSerialiser();
            savedSentences = save.LoadSentences();


        }

    }
}
