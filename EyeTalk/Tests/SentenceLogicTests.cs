using EyeTalk.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyeTalk.Tests
{
    class SentenceLogicTests
    {
        SentenceLogic logic;

        public SentenceLogicTests()
        {
            
        }

        [Test]
        public void GenerateSentencePageTest()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            Assert.AreEqual(0, logic.CategoryIndex);
            Assert.AreEqual(0, logic.PageIndex);
            Assert.AreEqual(0, logic.AmountOfWordsInSentence);
            Assert.IsTrue(logic.customCategory != null);
            Assert.IsTrue(logic.mostUsed != null);
        }

        [Test]
        public void GenerateSentencePageAndGoToFirstPageTest()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePageAndGoToFirstPage();
            Assert.AreEqual(0, logic.CategoryIndex);
            Assert.AreEqual(0, logic.PageIndex);
            Assert.AreEqual(0, logic.AmountOfWordsInSentence);
            Assert.IsTrue(logic.customCategory != null);
            Assert.IsTrue(logic.mostUsed != null);
        }

        [Test]
        public void CheckIfBackToLastCategory()
        {
            logic = new SentenceLogic();
            //tests update most used category, and custom cateofyr,
            logic.GenerateSentencePage();

            logic.CategoryIndex = 1;
            logic.CheckIfBackToLastCategory();

            Assert.IsTrue(0 == logic.CategoryIndex);
            Assert.IsTrue(0 == logic.PageIndex);


        }

        [Test]
        public void CheckIfBackToFirstCategory()
        {
            logic = new SentenceLogic();
            //tests update most used category, and custom cateofyr,
            logic.GenerateSentencePage();

            logic.CategoryIndex = 1;
            logic.CheckIfBackToFirstCategory();

            Assert.IsTrue(2 == logic.CategoryIndex);
            Assert.IsTrue(0 == logic.PageIndex);


        }

        [Test]
        public void ChangeCategoryTest()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();

            logic.CategoryIndex = 0;
            
            logic.ChangeCategory(1);

            Assert.IsTrue(1 == logic.CategoryIndex);


        }

        [Test]
        public void GetCurrentCategoryNameTest()
        {
            logic = new SentenceLogic();
            Assert.IsTrue("Actions" == logic.GetCurrentCategoryName());

        }

        [Test]
        public void GetCurrentCategoryPageTest()
        {
            logic = new SentenceLogic();
            Assert.IsTrue(3 == logic.GetCurrentCategoryPage().Count);

        }

        [Test]
        public void GetNumberOfPicturesInCurrentCategoryPageTest()
        {
            logic = new SentenceLogic();
            Assert.IsTrue(3 == logic.GetNumberOfPicturesInCurrentCategory());

        }

        [Test]
        public void GetPreviousCategoryNameTest()
        {
            logic = new SentenceLogic();
            Assert.AreEqual("Punctuation", logic.GetPreviousCategoryName());

        }

        [Test]
        public void GetNextCategoryNameTest()
        {
            logic = new SentenceLogic();
            logic.ResetCategoryChoice();
            Assert.IsTrue(0 == logic.PageIndex);
            Assert.IsTrue(0 == logic.CategoryIndex);

        }


        [Test]
        public void UpdateCustomCategory()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            logic.customCategory.Clear();
            List<Picture> page = new List<Picture>();
            page.Add(new Picture("test", false, "s", 1));

            logic.customCategory.Add(page);
            logic.UpdateCustomCategory();

            Assert.AreEqual("test", logic.categories[15].ElementAt(0).ElementAt(0).Name);


        }
        [Test]
        public void ResetCategoryChoiceTest()
        {
            logic = new SentenceLogic();
            Assert.IsTrue(3 == logic.GetNumberOfPicturesInCurrentCategory());

        }

        [Test]
        public void ResetCustomPictureCategoryIfNotEmpty()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            logic.customCategory.Clear();
            List<Picture> page = new List<Picture>();
            page.Add(new Picture("dd", false, "s", 1));

            logic.customCategory.Add(page);

            Assert.AreEqual("Custom category has been reset.", logic.ResetCustomPictureCategoryIfNotEmpty());
            Assert.AreEqual("Custom category is already empty.", logic.ResetCustomPictureCategoryIfNotEmpty());


        }

        [Test]
        public void GoToNextPage()
        {
            logic = new SentenceLogic();
            logic.CategoryIndex = 3;
            logic.GenerateSentencePageAndGoToFirstPage();
            logic.GoToNextPage();

            Assert.AreEqual(1, logic.PageIndex);

        }

        [Test]
        public void UpdatePageNumber()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            var test = "Page " + (logic.PageIndex + 1) + "/" + logic.categories.ElementAt(logic.CategoryIndex).Count;
            Assert.AreEqual(test, logic.GetPageNumber());


        }

        [Test]
        public void ResetMostUsed()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();

            logic.UpdateMostUsedPicture(0, "hello world");
            List<Picture> f = new List<Picture>();
            f.Add(new Picture("dd", false, "s", 1));
            logic.mostUsed.Add(f);


            Assert.AreEqual("Most Used category has been reset.", logic.ResetMostUsedIfNotEmpty());
            Assert.AreEqual("Most Used category is already empty.", logic.ResetMostUsedIfNotEmpty());


        }

        [Test]
        public void UpdateMostUsedPicture()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();

            for (int i = 0; i < 1000; i++)
            {
                logic.UpdateMostUsedPicture(0, "I want");

            }

            var ordered = logic.OrderMostUsedByCount();

            Assert.AreEqual("I want", ordered.ElementAt(0).Name);


        }

        [Test]
        public void CheckMostUsedContainsWord()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();


            Assert.False(logic.CheckMostUsedContainsWord("not in the list"));

            for (int i = 0; i < 1000; i++)
            {
                logic.UpdateMostUsedPicture(0, "I want");

            }

            var ordered = logic.OrderMostUsedByCount();
            Assert.True(logic.CheckMostUsedContainsWord("I want"));



        }

        [Test]
        public void UpdateMostUsedCategoryTest()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();

            for (int i = 0; i < 1000; i++)
            {
                logic.UpdateMostUsedPicture(0, "I want");

            }

            logic.UpdateMostUsedCategory();
            


            var ordered = logic.OrderMostUsedByCount();
            Assert.AreEqual("I want", logic.categories[16].ElementAt(0).ElementAt(0).Name) ;



        }

        [Test]
        public void OrderByMostUsedByCount()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();

            for (int i = 0; i < 1000; i++)
            {
                logic.UpdateMostUsedPicture(0, "I want");

            }
            for (int i = 0; i < 500; i++)
            {
                logic.UpdateMostUsedPicture(1, "I don't want");

            }

            logic.OrderMostUsedByCount();


            Assert.IsTrue(logic.mostUsedList.ElementAt(0).AmountOfTimesClicked > logic.mostUsedList.ElementAt(1).AmountOfTimesClicked);


        }

        [Test]
        public void UpdateCategoryAndGoToFirstPage()
        {
            logic = new SentenceLogic();

            logic.GenerateSentencePageAndGoToFirstPage();

            var categoryPages = logic.categories.ElementAt(logic.CategoryIndex);
            var CategoryPage = categoryPages.ElementAt(0);
            var CategoryName = logic.categoryNames.ElementAt(logic.CategoryIndex);

            Assert.IsTrue(CategoryPage == logic.CategoryPage);
            Assert.IsTrue(CategoryName == logic.CategoryName);

        }

        private void AddTwoPictures()
        {
            logic = new SentenceLogic();
            var s = new Picture("a", false, "dd", 1);
            var ss = new Picture("b", false, "ddd", 1);

            List<Picture> g = new List<Picture>();
            g.Add(s);
            List<Picture> gg = new List<Picture>();

            gg.Add(ss);

            List<List<Picture>> x = new List<List<Picture>>() { g, gg };
            logic.categories.Clear();
            logic.categories.Add(x);
        }






        [Test]
        public void AddWordToSentence()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            logic.AddWordToSentence("hello", 0);
            Assert.AreEqual(true, logic.Sentence.Contains("hello"));

        }

        [Test]
        public void RemoveWordFromSentence()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            logic.Sentence.Clear();
            logic.CategoryIndex = 0;
            logic.PageIndex = 0;
            logic.AddWordToSentence("hello", 0);
            logic.RemoveWordFromSentence("hello", 0);
            Assert.AreEqual(false, logic.Sentence.Contains("hello"));

        }


        [Test]
        public void RemoveAllWordsFromSentence()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            logic.Sentence.Clear();
            logic.CategoryIndex = 0;
            logic.PageIndex = 0;
            logic.AddWordToSentence("hello", 0);
            logic.AddWordToSentence("world", 1);

            logic.RemoveAllWordsFromSentence();
            Assert.AreEqual(false, logic.Sentence.Contains(""));

        }

        [Test]
        public void ResetSentence()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            logic.Sentence.Clear();

            logic.AddWordToSentence("hello", 0);
            logic.AddWordToSentence("world", 1);

            logic.ResetSentence();

            Assert.AreEqual(false, logic.Sentence.Contains(""));
            Assert.AreEqual(0, logic.AmountOfWordsInSentence);


        }

        [Test]
        public void RegexTest()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            logic.Sentence.Clear();

            logic.AddWordToSentence("hello", 0);
            

            string sentence = logic.AddWordToSentence("world", 1);

            Assert.AreEqual(true, logic.CheckThatWordIsMatched(sentence, "world"));


        }
    }
 }
