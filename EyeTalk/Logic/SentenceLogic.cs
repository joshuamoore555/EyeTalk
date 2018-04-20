using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                {"Personal Feeling"},
                {"Colours"},
                {"Animals"},
                {"Times"},
                {"Carers"},
                {"Kitchen"},
                {"Bathroom"},
                {"Entertainment"},
                {"Family"},
                {"Custom"},
                {"Most Used"},
                {"Connecting Words"},
                {"Getting Dressed"},
                {"Personal Care"},
            };
        }

        //Generators

        public void GenerateSentencePage()
        {
            //updates the values used within the sentence page
            UpdateCustomCategory();
            UpdateMostUsedCategory();

            CategoryName = GetCurrentCategoryName();
            CategoryPage = GetCurrentCategoryPage();
        }

        public void GenerateSentencePageAndGoToFirstPage()
        {
            //brings the user to the first page of the current category
            PageIndex = 0;
            GenerateSentencePage();
        }


        //Category methods

        public void CheckIfBackToLastCategory()
        {
            //reduces category index by 1. Checks if category is before the first category. If so, sets index to the last category.

            CategoryIndex--;
            PageIndex = 0;

            if (CategoryIndex < 0)
            {
                CategoryIndex = categories.Count - 1;
            }

        }

        public void CheckIfBackToFirstCategory()
        {
            //increases category index by 1. Checks if category is back to the first category. If so, sets index to that.

            CategoryIndex++;
            PageIndex = 0;
            if (CategoryIndex >= categories.Count)
            {
                CategoryIndex = 0;

            }
        }

        public void ChangeCategory(int newCategoryIndex)
        {
            CategoryIndex = newCategoryIndex;
        }

        public string GetCurrentCategoryName()
        {
            return categoryNames.ElementAt(CategoryIndex);
        }

        public List<Picture> GetCurrentCategoryPage()
        {
            var categoryPages = categories.ElementAt(CategoryIndex);
            return categoryPages.ElementAt(PageIndex); 
        }

        public int GetNumberOfPicturesInCurrentCategory()
        {
            return categories.ElementAt(CategoryIndex).ElementAt(PageIndex).Count;
        }

        public string GetPreviousCategoryName()
        {
            //returns the name of the category before the current one

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
            //returns the name of the category after the current one

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
            //changes the category back to the first one and first page.

            CategoryIndex = 0;
            PageIndex = 0;
        }


        //Custom Picture Category methods

        public void UpdateCustomCategory()
        {
            //if the custom category is empty, add an empty page.
            //then updates the custom category with the latest one loaded

            if (customCategory.Count == 0)
            {
                List<Picture> page = new List<Picture>();
                customCategory.Add(page);
            }            
                categories[15] = customCategory;            
        }

        public string ResetCustomPictureCategoryIfNotEmpty()
        {
            //if custom category is not empty, reset and return an update on status.
            //if custom category is empty, return status saying it is empty

            if (customCategory.Count > 0 && customCategory.ElementAt(0).Count > 0)
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
            //increases the page index. If beyond the amount of pages, go back to first page. 
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

        public string GetPageNumber()
        {
            //returns the current page index and the total number of pages in the current category
            var categoryPages = categories.ElementAt(CategoryIndex);
            var numberOfPages = categoryPages.Count;
            return "Page " + (PageIndex + 1) + "/" + numberOfPages;

        }


        //Most Used methods

        public string ResetMostUsedIfNotEmpty()
        {
            //if empty, returns status update stating so
            //if not empty, clears the most used list and most used category and saves a new category with a blank page.

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
            //updates the most used picture list. If it is null, creates a new one. 

            if (mostUsed == null)
            {
                mostUsed = new List<List<Picture>>();
            }

            if (mostUsedList == null)
            {
                mostUsedList = new List<Picture>();
            }

            //increases the times clicked variable by one
        
            CategoryPage.ElementAt(i).AmountOfTimesClicked++;
  
            //if the list contains the word, removes the previous save of the word with the old count and adds a new one with the updated count.
            if (CheckMostUsedContainsWord(word))
            {
                mostUsedList.RemoveAll(u=> u.Name.StartsWith(word));
                mostUsedList.Add(CategoryPage.ElementAt(i));
            }
            else
            {
                //if not contained, add to the list
                mostUsedList.Add(CategoryPage.ElementAt(i));

            }


        }

        public bool CheckMostUsedContainsWord(string word)
        {
            //check each picture in list to see if it contains word

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
            //check if most used list and most used category are null
            if (mostUsed != null && mostUsedList != null)
            {
                List<List<Picture>> mostUsedCategory = new List<List<Picture>>();
                List<Picture> mostUsedPage1 = new List<Picture>();
                List<Picture> mostUsedPage2 = new List<Picture>();
                List<Picture> mostUsedPage3 = new List<Picture>();


                //if not, get the top 4 and next 4 most used pictures and put them into two pages.
                var orderedMostUsed = OrderMostUsedByCount();
                var pictures1To4 = orderedMostUsed.Take(4);
                var pictures5To8 = orderedMostUsed.Skip(4).Take(4);
                var pictures9To12= orderedMostUsed.Skip(8).Take(4);

                //page 1
                for (int i = 0; i < pictures1To4.Count(); i++)
                {
                    mostUsedPage1.Add(pictures1To4.ElementAt(i));
                    mostUsedPage1.ElementAt(i).Selected = false;
                }

                //page 2
                for (int i = 0; i < pictures5To8.Count(); i++)
                {
                    mostUsedPage2.Add(pictures5To8.ElementAt(i));
                    mostUsedPage2.ElementAt(i).Selected = false;
                }

                //page 3
                for (int i = 0; i < pictures9To12.Count(); i++)
                {
                    mostUsedPage3.Add(pictures9To12.ElementAt(i));
                    mostUsedPage3.ElementAt(i).Selected = false;
                }

                //add the pages to the most used category and add it to the categories list.
                mostUsedCategory.Add(mostUsedPage1);
                mostUsedCategory.Add(mostUsedPage2);
                mostUsedCategory.Add(mostUsedPage3);

                //adds the updated most used category to the categories list
                categories[16] = mostUsedCategory;


            }
        }

        public List<Picture> OrderMostUsedByCount()
        {
            //orders the pictures in the list by the amount of times they have been clicked. more clicks = top of list
            return mostUsedList.OrderByDescending(entry => entry.AmountOfTimesClicked).ToList();
        }


        //Sentence methods

        public string AddWordToSentence(string word, int i)
        {
            //takes the word and adds spaces
            var name = CategoryPage.ElementAt(i).Name;

            string sentence = GetSentence();

            //if sentence does not contain the word
            if (!CheckThatWordIsMatched(sentence, word))
            {
                //add the word to ordered dictionary, which remembers the order it was added.
                Sentence.Add(word, word);

                //string builder then recreates the entire sentence with the new word added, and returns the string.
                string newSentence = GetSentence();

                //increments amount of words in sentence value and makes the selected variable of the word true.
                AmountOfWordsInSentence++;
                CategoryPage.ElementAt(i).Selected = true;

                return newSentence;
            }
            else
            {
                //if already used, return nothing.
                return "";
            }
        }

        private string GetSentence()
        {
            StringBuilder sentence = new StringBuilder();
            foreach (DictionaryEntry s in Sentence)
            {
                sentence.Append(s.Value + " ");
            }

            return sentence.ToString();
        }

        public bool CheckThatWordIsMatched(string sentence, string word)
        {
            if (Regex.IsMatch(sentence, string.Format(@"\b{0}\b", Regex.Escape(word))))
            {
                return true;
            }

            else
            {
                return false;
            }
                
        }

        public string RemoveWordFromSentence(string word, int i)
        {
            //removes the word from the sentence, recreates the sentence without it, and returns the string. 
            Sentence.Remove(word);
            string newSentence = GetSentence();

            //decrements amount of words in sentence value and makes the selected variable of the word false.
            AmountOfWordsInSentence--;
            CategoryPage.ElementAt(i).Selected = false;

            return newSentence;

        }

        public void RemoveAllWordsFromSentence()
        {
            //clears the sentence and makes every selected picture = false
            ResetSentence();
         
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


        //Image methods

        public BitmapImage GenerateBitmap(string filepath)
        {
            //gets a file path and attempts to create a bitmap with reduced resolution, to preserve memory and speed
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
                //if file path has been changed or lost, then use a placeholder image instead.
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri("pack://application:,,,/Images/filenotfound.png");
                bmp.DecodePixelWidth = 400;
                bmp.EndInit();
            }

            //return the bitmap image.
            return bmp;
        }

    }
}
