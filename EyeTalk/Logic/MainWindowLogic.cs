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
        public int AmountOfWordsInSentence { get; set; }
        public OrderedDictionary Sentence { get; set; }
        public SortedList<String, List<List<Picture>>> categories;
        public List<List<Picture>> customCategory;

        public List<List<Picture>> mostUsed;
        public List<Picture> mostUsedList;

        SaveFileSerialiser save;

        public string CategoryName { get; set; }
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
            AmountOfWordsInSentence = 0;


        }

        public void Begin()
        {
            ResetSentence();
            UpdateCustomCategory();
            UpdateMostUsedCategory();

            var categoryPages = categories.ElementAt(CategoryIndex);
            CategoryName = categories.ElementAt(CategoryIndex).Key;
            CategoryPage = categoryPages.Value.ElementAt(PageIndex);

        }

        public void ResetSentence()
        {
            CategoryIndex = 0;
            PageIndex = 0;
            AmountOfWordsInSentence = 0;
            Sentence.Clear();
        }

        public void UpdateCustomCategory()
        {
            categories.Remove("Custom");
            categories.Add("Custom", customCategory);
        }

        public void UpdateMostUsedCategory()
        {
            categories.Remove("Most Used");

            if (mostUsed != null && mostUsedList != null)
            {

                var orderedMostUsed = OrderMostUsedByCount();
                var topFour = orderedMostUsed.Take(4);
                var nextFour = orderedMostUsed.Skip(4).Take(4);


                List<List<Picture>> mostUsedCategory = new List<List<Picture>>();
                List<Picture> mostUsedPage1 = new List<Picture>();
                List<Picture> mostUsedPage2 = new List<Picture>();

                for (int i = 0; i < topFour.Count(); i++)
                {
                    mostUsedPage1.Add(topFour.ElementAt(i));
                    mostUsedPage1.ElementAt(i).Selected = false;
                }

                for (int i = 0; i < nextFour.Count(); i++)
                {
                    mostUsedPage2.Add(nextFour.ElementAt(i));
                    mostUsedPage2.ElementAt(i).Selected = false;
                }

                mostUsedCategory.Add(mostUsedPage1);
                mostUsedCategory.Add(mostUsedPage2);
                categories.Add("Most Used", mostUsedCategory);

            }
        }

        public List<Picture> OrderMostUsedByCount()
        {
            return mostUsedList.OrderByDescending(entry => entry.Count).ToList();
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

        public void CheckIfBackToFirstCategory()
        {
            CategoryIndex++;
            PageIndex = 0;
            if (CategoryIndex >= categories.Count)
            {
                CategoryIndex = 0;

            }
        }

        public void UpdateCategoryAndGoToFirstPage()
        {
            var categoryPages = categories.ElementAt(CategoryIndex);
            CategoryPage = categoryPages.Value.ElementAt(0);
            CategoryName = categories.ElementAt(CategoryIndex).Key;
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
            return CategoryName + " \nPage " + (PageIndex + 1) + "/" + numberOfPages;

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
           
            if (mostUsed.Count > 0)
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
                mostUsed = new List<List<Picture>>();
            }

            if (mostUsedList == null)
            {
                mostUsedList = new List<Picture>();
            }

            CategoryPage.ElementAt(i).Count++;
  

            if (CheckMostUsedContainsWord(word))
            {
                mostUsedList.RemoveAll(u=> u.Name.StartsWith(word));
                mostUsedList.Add(CategoryPage.ElementAt(i));
                //if contains word, remove it and then re-add
            }
            else
            {
                //if not contained, add
                mostUsedList.Add(CategoryPage.ElementAt(i));

            }


        }

        public bool CheckMostUsedContainsWord(string word)
        {
            foreach (Picture picture in mostUsedList)
            {
                if (picture.Name == word)
                {
                    return true;
                }
            }

            return false;
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
