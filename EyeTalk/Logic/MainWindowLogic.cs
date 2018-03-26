using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTalk.Objects;

namespace EyeTalk
{
    public class MainWindowLogic
    {
        public int CategoryIndex { get; set; }
        public int PageIndex { get; set; }
        public int SentenceIndex { get; set; }
        public int AmountOfWordsInSentence { get; set; }
        public List<Picture> Category { get; set; }
        public OrderedDictionary Sentence { get; set; }
        public SortedList<String, List<List<Picture>>> categories;
        public List<List<Picture>> customCategory;
        public SortedDictionary<String, Picture> mostUsed;
        SaveFileSerialiser save;

        public MainWindowLogic()
        {
            save = new SaveFileSerialiser();
            Sentence = new OrderedDictionary();

            categories = save.LoadCategories();
            customCategory = save.LoadCustomCategory();
            mostUsed = save.LoadMostUsed();
        

            CategoryIndex = 0;
            PageIndex = 0;
            SentenceIndex = 0;
            AmountOfWordsInSentence = 0;


        }

        public void UpdateMostUsedCategory()
        {
            categories.Remove("Most Used");

            if (mostUsed != null)
            {
                mostUsed = save.LoadMostUsed();

                List<List<Picture>> mostUsedCategory = new List<List<Picture>>();
                List<Picture> mostUsedPage1 = new List<Picture>();
                List<Picture> mostUsedPage2 = new List<Picture>();

                var topFour = mostUsed.OrderBy(entry => entry.Value.Count).Take(4).ToDictionary(pair => pair.Key, pair => pair.Value);

                var nextFour = mostUsed.OrderBy(entry => entry.Value.Count).Skip(4).Take(4).ToDictionary(pair => pair.Key, pair => pair.Value);

                for (int i = 0; i < topFour.Count(); i++)
                {
                    topFour.ElementAt(i).Value.Name = "" + topFour.ElementAt(i).Value.Count;
                    mostUsedPage1.Add(topFour.ElementAt(i).Value);
                    mostUsedPage1.ElementAt(i).Selected = false;
                }

                for (int i = 0; i < nextFour.Count(); i++)
                {
                    nextFour.ElementAt(i).Value.Name = "" + nextFour.ElementAt(i).Value.Count;
                    mostUsedPage2.Add(nextFour.ElementAt(i).Value);
                    mostUsedPage2.ElementAt(i).Selected = false;
                }

                mostUsedCategory.Add(mostUsedPage1);
                mostUsedCategory.Add(mostUsedPage2);
                categories.Add("Most Used", mostUsedCategory);

            }
        }

        public void UpdateCustomCategory()
        {
            categories.Remove("Custom");
            categories.Add("Custom", customCategory);
        }

        public void ResetSentence()
        {
            CategoryIndex = 0;
            PageIndex = 0;
            AmountOfWordsInSentence = 0;
            SentenceIndex = 0;
            Sentence.Clear();
           
        }

    }
}
