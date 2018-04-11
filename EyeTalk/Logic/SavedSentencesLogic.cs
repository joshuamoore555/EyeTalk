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
        public int SentenceIndex { get; set; }
        public SaveFileSerialiser save;


        public SavedSentencesLogic()
        {
            save = new SaveFileSerialiser();
            savedSentences = save.LoadSentences();


        }

        public void SaveSentences()
        {
            save.SaveSentencesToFile(savedSentences);

        }

        public string RetrieveFirstSavedSentenceIfExists()
        {

            if (savedSentences.Count <= 0)
            {
                return "No sentences saved";
            }
            else
            {
                return savedSentences.First();
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
            else if (savedSentences.Count > 0 && savedSentences.Contains(sentence))
            {
               return "Sentence has already been saved";
            }
            else
            {
                savedSentences.Add(sentence);
                save.SaveSentencesToFile(savedSentences);
                return "Sentence Saved";
            }
        }

        public string NextSentence()
        {
            SentenceIndex++;

            if(savedSentences.Count <= 0)
            {
                return "No Sentences Saved";
            }
            else if(SentenceIndex <= savedSentences.Count - 1)
            {
                return savedSentences.ElementAt(SentenceIndex);
            }
            else
            {
                SentenceIndex = 0;
                return savedSentences.ElementAt(SentenceIndex);
            }
        }

        public string PreviousSentence()
        {
            SentenceIndex--;
            if (savedSentences.Count <= 0)
            {
                return "No Sentences Saved";
            }
            else if (SentenceIndex >= 0)
            {
                return savedSentences.ElementAt(SentenceIndex);
            }
            else
            {
                SentenceIndex = savedSentences.Count - 1;
                return savedSentences.ElementAt(SentenceIndex);
            }
        }

        public string DeleteSavedSentence()
        {
            if (savedSentences.Count <= 0)
            {
                return "No Sentences Saved";
            }
            else
            {
                savedSentences.RemoveAt(SentenceIndex);
                SentenceIndex = 0;
                if (savedSentences.Count <= 0)
                {
                    return "No Sentences Saved";
                }
                else
                {
                    SaveSentences();
                    return savedSentences.ElementAt(SentenceIndex);
                }

            }
        }
    }
}
