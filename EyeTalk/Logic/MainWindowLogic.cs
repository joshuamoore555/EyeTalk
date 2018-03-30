using System;
using System.Collections;
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
        //public int SentenceIndex { get; set; }
        public int AmountOfWordsInSentence { get; set; }
        public OrderedDictionary Sentence { get; set; }
        public SortedList<String, List<List<Picture>>> categories;
        public List<List<Picture>> customCategory;
        public SortedDictionary<String, Picture> mostUsed;
        SaveFileSerialiser save;

        public string categoryName { get; set; }
        public List<Picture> CategoryPage { get; set; }



        public MainWindowLogic()
        {
            save = new SaveFileSerialiser();
            Sentence = new OrderedDictionary();

            categories = save.LoadCategories();
            customCategory = save.LoadCustomCategory();
            mostUsed = save.LoadMostUsed();

            CategoryIndex = 0;
            PageIndex = 0;
            //SentenceIndex = 0;
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
            //SentenceIndex = 0;
            Sentence.Clear();
        }

        public void CheckIfBackToLastCategory()
        {
            CategoryIndex--;
            PageIndex = 0;

            if (CategoryIndex < 0)
            {
                CategoryIndex = categories.Count - 1;
            }

        }

        public void UpdateCategoryAndGoToFirstPage()
        {
            var categoryPages = categories.ElementAt(CategoryIndex);
            CategoryPage = categoryPages.Value.ElementAt(0);
            categoryName = categories.ElementAt(CategoryIndex).Key;
        }

        public void GoToNextPage()
        {
            var categoryPages = categories.ElementAt(CategoryIndex);
            var numberOfPages = categoryPages.Value.Count;
            PageIndex++;

            if (PageIndex >= numberOfPages)
            {
                PageIndex = 0;
                CategoryPage = categoryPages.Value.ElementAt(PageIndex);

            }
            else
            {
                CategoryPage = categoryPages.Value.ElementAt(PageIndex);
            }

            CategoryPage = categoryPages.Value.ElementAt(PageIndex);
        }

        public string UpdatePageNumber()
        {
            var categoryPages = categories.ElementAt(CategoryIndex);
            var numberOfPages = categoryPages.Value.Count;
            return categoryName + " \nPage " + (PageIndex + 1) + "/" + numberOfPages;

        }

        public void CheckIfBackToFirstCategory()
        {
            CategoryIndex++;
            PageIndex = 0;
            if (CategoryIndex >= categories.Count)
            {
                CategoryIndex = 0;

            }
        }

        public void Begin()
        {
            ResetSentence();
            UpdateCustomCategory();
            UpdateMostUsedCategory();

            var categoryPages = categories.ElementAt(CategoryIndex);
            categoryName = categories.ElementAt(CategoryIndex).Key;
            CategoryPage = categoryPages.Value.ElementAt(PageIndex);

        }
        public void SaveMostUsedIfNotNull()
        {
            if (mostUsed != null)
            {
                save.SaveMostUsed(mostUsed);
            }
        }

        public string ResetMostUsedIfNotEmpty()
        {
           
            if (mostUsed.Count > 1)
            {
                mostUsed.Clear();
                return "Most Used category has been reset.";
            }
            else
            {
                return "Most Used category is already empty.";
            }

        }

        public void UpdateMostUsedPicture(int i, string word)
        {
            if (mostUsed == null)
            {
                mostUsed = new SortedDictionary<String, Picture>();
            }

            CategoryPage.ElementAt(i).Count++;

            if (mostUsed.ContainsKey(word))
            {
                mostUsed.Remove(word);
                mostUsed.Add(word, CategoryPage.ElementAt(i));
            }
            else
            {
                mostUsed.Add(word, CategoryPage.ElementAt(i));
            }
        }


        public string AddWordToSentence(string word, int i)
        {
            if (!Sentence.Contains(CategoryPage.ElementAt(i).Name))
            {
                Sentence.Add(word, word);
                StringBuilder sb = new StringBuilder();
                foreach (DictionaryEntry s in Sentence)
                {
                    sb.Append(s.Value + " ");
                }
                AmountOfWordsInSentence++;
                CategoryPage.ElementAt(i).Selected = true;
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        public string RemoveWordFromSentence(string word, int i)
        {
            Sentence.Remove(word);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry s in Sentence)
            {
                sb.Append(s.Value + " ");
            }
            AmountOfWordsInSentence--;

            CategoryPage.ElementAt(i).Selected = false;

            return sb.ToString();
         
        }
    }
}
