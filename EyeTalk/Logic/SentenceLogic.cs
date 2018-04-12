using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EyeTalk.Objects;

namespace EyeTalk
{
    public class SentenceLogic
    {
        public int CategoryIndex { get; set; }
        public int PageIndex { get; set; }
        public int AmountOfWordsInSentence { get; set; }
        public OrderedDictionary Sentence { get; set; }
        public List<List<List<Picture>>> categories;
        public List<List<Picture>> customCategory;

        public List<List<Picture>> mostUsed;
        public List<Picture> mostUsedList;

        SaveFileSerialiser save;

        public string CategoryName { get; set; }
        public List<Picture> CategoryPage { get; set; }
        public List<String> categoryNames;

        public SentenceLogic()
        {
            save = new SaveFileSerialiser();
            Sentence = new OrderedDictionary();

            categories = save.LoadCategories();
            customCategory = save.LoadCustomCategory();
            mostUsed = save.LoadMostUsed();
            mostUsedList = save.LoadMostUsedList();

            CategoryIndex = 0;
            PageIndex = 0;
            AmountOfWordsInSentence = 0;

            categoryNames = new List<String>()
            {
                {"Actions"},
                {"Replies"},
                {"Foods"},
                {"Drinks"},
                {"Greetings"},
                {"Feelings"},
                {"Emotions"},
                {"Colours"},
                {"Animals"},
                {"Times"},
                {"Carers"},
                {"Kitchen"},
                {"Personal Care"},
                {"Entertainment"},
                {"Family"},
                {"Custom"},
                {"Most Used"},

            };
        }

        public void GenerateSentencePage()
        {           
            UpdateCustomCategory();
            UpdateMostUsedCategory();

            var categoryPages = categories.ElementAt(CategoryIndex);
            CategoryName = categoryNames.ElementAt(CategoryIndex);
            CategoryPage = categoryPages.ElementAt(PageIndex);

        }

        public BitmapImage GenerateBitmap(string filepath)
        {
            BitmapImage bmp = new BitmapImage();
            try
            {
                bmp.BeginInit();
                bmp.UriSource = new Uri(filepath);
                bmp.DecodePixelWidth = 400;
                bmp.EndInit();
            }
            catch
            {
                //if file path has been changed, give a file not found image
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri("pack://application:,,,/Images/filenotfound.png");
                bmp.DecodePixelWidth = 400;
                bmp.EndInit();
            }
            return bmp;
        }


        //Category methods

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
            PageIndex = 0;
            CategoryPage = categoryPages.ElementAt(PageIndex);
            CategoryName = GetCategoryName();
        }

        public void ChangeCategory(int newCategoryIndex)
        {
            CategoryIndex = newCategoryIndex;
        }

        public string GetCategoryName()
        {
            return categoryNames.ElementAt(CategoryIndex);
        }

        public int GetNumberOfPicturesInCurrentCategory()
        {
            return categories.ElementAt(CategoryIndex).ElementAt(0).Count;
        }

        public string GetPreviousCategoryName()
        {
            if (CategoryIndex - 1 < 0)
            {
                return categoryNames.ElementAt(categories.Count - 1);
            }
            else if (CategoryIndex - 2 < 0)
            {
                return categoryNames.ElementAt(categories.Count - 2);

            }
            else
            {
                return categoryNames.ElementAt(CategoryIndex - 1);

            }

        }

        public string GetNextCategoryName()
        {
            if (CategoryIndex + 1 > categories.Count)
            {
                return categoryNames.ElementAt(0);
            }
            else if (CategoryIndex + 2 > categories.Count)
            {
                return categoryNames.ElementAt(1);
            }
            else
            {
                return categoryNames.ElementAt(CategoryIndex + 1);

            }
        }

        public void ResetCategoryChoice()
        {
            CategoryIndex = 0;
            PageIndex = 0;

        }


        //Custom Picture Category methods

        public void UpdateCustomCategory()
        {
            if (customCategory.Count == 0)
            {
                List<Picture> page = new List<Picture>();
                customCategory.Add(page);
            }            
                categories[15] = customCategory;            
        }

        public string ResetCustomPictureCategoryIfNotEmpty()
        {
            if (customCategory.Count > 0)
            {
                customCategory.Clear();

                List<Picture> emptyPage = new List<Picture>();
                customCategory.Add(emptyPage);

                save.SaveCustomCategory(customCategory);
                return "Custom category has been reset.";
            }
            else
            {
                return "Custom category is already empty.";
            }

        }


        //Page methods

        public void GoToNextPage()
        {
            var categoryPages = categories.ElementAt(CategoryIndex);
            var numberOfPages = categoryPages.Count;
            PageIndex++;

            if (PageIndex >= numberOfPages)
            {
                PageIndex = 0;
                CategoryPage = categoryPages.ElementAt(PageIndex);

            }
            else
            {
                CategoryPage = categoryPages.ElementAt(PageIndex);
            }

            CategoryPage = categoryPages.ElementAt(PageIndex);
        }

        public string UpdatePageNumber()
        {
            var categoryPages = categories.ElementAt(CategoryIndex);
            var numberOfPages = categoryPages.Count;
            return "Page " + (PageIndex + 1) + "/" + numberOfPages;

        }


        //Most Used methods

        public string ResetMostUsedIfNotEmpty()
        {
            if (mostUsed.Count > 0 && mostUsedList.Count > 0)
            {
                mostUsed.Clear();
                mostUsedList.Clear();

                List<Picture> emptyPage = new List<Picture>();
                mostUsed.Add(emptyPage);

                save.SaveMostUsed(mostUsed);
                save.SaveMostUsedList(mostUsedList);
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

        public void UpdateMostUsedCategory()
        {

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

                categories[16] = mostUsedCategory;


            }
        }

        public List<Picture> OrderMostUsedByCount()
        {
            return mostUsedList.OrderByDescending(entry => entry.Count).ToList();
        }


        //Sentence methods

        public string AddWordToSentence(string word, int i)
        {
            var name = " " + CategoryPage.ElementAt(i).Name + " ";
            if (!Sentence.Contains(name))
            {
                Sentence.Add(word, word);
                StringBuilder sb = new StringBuilder();
                sb.Append(" ");
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
            sb.Append(" ");
            foreach (DictionaryEntry s in Sentence)
            {
                sb.Append(s.Value + " ");
            }
            AmountOfWordsInSentence--;

            CategoryPage.ElementAt(i).Selected = false;

            return sb.ToString();

        }

        public void RemoveAllWordsFromSentence()
        {
            AmountOfWordsInSentence = 0;
            Sentence.Clear();
         
            foreach (var category in categories)
            {
                foreach (List<Picture> page in category)
                {
                    foreach (Picture picture in page)
                    {
                        picture.Selected = false;
                    }
                }
            }
        }

        public void ResetSentence()
        {
            AmountOfWordsInSentence = 0;
            Sentence.Clear();
        }




    }
}
