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
        public List<String> SavedSentences { get; set; }
        public int SentenceIndex { get; set; }
        public SaveFileSerialiser save;

        public SavedSentencesLogic()
        {
            save = new SaveFileSerialiser();
            SavedSentences = save.LoadSentences();
        }

        public void SaveSentences()
        {
            save.SaveSentencesToFile(SavedSentences);

        }

        public string RetrieveFirstSavedSentenceIfExists()
        {

            if (SavedSentences.Count <= 0)
            {
                return "No sentences saved";
            }
            else
            {
                return SavedSentences.First();
            }
        }

        public string SaveSentenceIfNotPreviouslySaved(string sentence)
        {
            if (String.IsNullOrEmpty(sentence))
            {
                return "Please create a sentence before saving";

            }
            else if (sentence == " ") 
            {
                return "Please create a sentence before saving";
            }
            else if (SavedSentences.Count > 0 && SavedSentences.Contains(sentence))
            {
               return "Sentence has already been saved";
            }
            else
            {
                SavedSentences.Add(sentence);
                save.SaveSentencesToFile(SavedSentences);
                return "Sentence Saved";
            }
        }

        public string NextSentence()
        {
            SentenceIndex++;

            if(SavedSentences.Count <= 0)
            {
                return "No Sentences Saved";
            }
            else if(SentenceIndex <= SavedSentences.Count - 1)
            {
                return SavedSentences.ElementAt(SentenceIndex);
            }
            else
            {
                SentenceIndex = 0;
                return SavedSentences.ElementAt(SentenceIndex);
            }
        }

        public string PreviousSentence()
        {
            SentenceIndex--;
            if (SavedSentences.Count <= 0)
            {
                return "No Sentences Saved";
            }
            else if (SentenceIndex >= 0)
            {
                return SavedSentences.ElementAt(SentenceIndex);
            }
            else
            {
                SentenceIndex = SavedSentences.Count - 1;
                return SavedSentences.ElementAt(SentenceIndex);
            }
        }

        public string DeleteSavedSentence()
        {
            if (SavedSentences.Count <= 0)
            {
                return "No Sentences Saved";
            }
            else
            {
                SavedSentences.RemoveAt(SentenceIndex);
                SentenceIndex = 0;
                if (SavedSentences.Count <= 0)
                {
                    return "No Sentences Saved";
                }
                else
                {
                    SaveSentences();
                    return SavedSentences.ElementAt(SentenceIndex);
                }

            }
        }
    }
}
