using EyeTalk.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Tests
{
    class MainWindowLogicTests
    {
        SentenceLogic logic;

        public MainWindowLogicTests()
        {
            
        }

        [Test]
        public void BeginMainWindow()
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
        public void OrderByMostUsedByCount()
        {
            logic = new SentenceLogic();
            //tests update most used category, and custom cateofyr,
            logic.GenerateSentencePage();
            Picture hello = new Picture("test", false, "test", 99999);
            Picture small = new Picture("test2", false, "test", 0);
            logic.mostUsedList = new List<Picture>();

            logic.mostUsedList.Add(hello);
            logic.mostUsedList.Add(small);

            Assert.IsTrue(logic.mostUsedList.ElementAt(0).Count > logic.mostUsedList.ElementAt(1).Count);


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
        public void UpdateCategoryAndGoToFirstPage()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            AddTwoPictures();

            logic.UpdateCategoryAndGoToFirstPage();

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
        public void GoToNextPage()
        {
            logic = new SentenceLogic();
            logic.CategoryIndex = 3;
            logic.UpdateCategoryAndGoToFirstPage();           
            logic.GoToNextPage();

            Assert.AreEqual(1, logic.PageIndex);

        }

        [Test]
        public void UpdatePageNumber()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();
            var test = "Page " + (logic.PageIndex + 1) + "/" + logic.categories.ElementAt(logic.CategoryIndex).Count;
            Assert.AreEqual(test, logic.UpdatePageNumber());


        }

        [Test]
        public void ResetMostUsed()
        {
            logic = new SentenceLogic();
            logic.GenerateSentencePage();

            logic.UpdateMostUsedPicture(0,"hello world");
            List<Picture> f = new List<Picture>();
            f.Add(new Picture("dd", false, "s", 1));
            logic.mostUsed.Add(f);
            

            Assert.AreEqual("Most Used category has been reset.", logic.ResetMostUsedIfNotEmpty());
            Assert.AreEqual("Most Used category is already empty.", logic.ResetMostUsedIfNotEmpty());


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
    }
 }
