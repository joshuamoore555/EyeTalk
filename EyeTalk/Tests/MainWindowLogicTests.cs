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
        MainWindowLogic logic;

        public MainWindowLogicTests()
        {
            logic = new MainWindowLogic();
        }

        [Test]
        public void BeginMainWindow()
        {

            logic.Begin();
            Assert.AreEqual(0, logic.CategoryIndex);
            Assert.AreEqual(0, logic.PageIndex);
            Assert.AreEqual(0, logic.AmountOfWordsInSentence);
            Assert.IsTrue(logic.customCategory != null);
            Assert.IsTrue(logic.mostUsed != null);
        }

        [Test]
        public void OrderByMostUsedByCount()
        {
            //tests update most used category, and custom cateofyr,
            logic.Begin();
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
            //tests update most used category, and custom cateofyr,
            logic.Begin();

            logic.CategoryIndex = 1;
            logic.CheckIfBackToLastCategory();

            Assert.IsTrue(0 == logic.CategoryIndex);
            Assert.IsTrue(0 == logic.PageIndex);


        }


        [Test]
        public void CheckIfBackToFirstCategory()
        {
            //tests update most used category, and custom cateofyr,
            logic.Begin();

            logic.CategoryIndex = 1;
            logic.CheckIfBackToFirstCategory();

            Assert.IsTrue(2 == logic.CategoryIndex);
            Assert.IsTrue(0 == logic.PageIndex);


        }

        [Test]
        public void UpdateCategoryAndGoToFirstPage()
        {
            logic.Begin();
            AddTwoPictures();

            logic.UpdateCategoryAndGoToFirstPage();

            var categoryPages = logic.categories.ElementAt(logic.CategoryIndex);
            var CategoryPage = categoryPages.Value.ElementAt(0);
            var CategoryName = logic.categories.ElementAt(logic.CategoryIndex).Key;

            Assert.IsTrue(CategoryPage == logic.CategoryPage);
            Assert.IsTrue(CategoryName == logic.CategoryName);

        }

        private void AddTwoPictures()
        {
            var s = new Picture("a", false, "dd", 1);
            var ss = new Picture("b", false, "ddd", 1);

            List<Picture> g = new List<Picture>();
            g.Add(s);
            List<Picture> gg = new List<Picture>();

            gg.Add(ss);

            List<List<Picture>> x = new List<List<Picture>>() { g, gg };
            logic.categories.Clear();
            logic.categories.Add("hello", x);
        }

        [Test]
        public void GoToNextPage()
        {
            logic.Begin();
            AddTwoPictures();

            logic.GoToNextPage();
            Assert.AreEqual("b", logic.categories.ElementAt(logic.CategoryIndex).Value.ElementAt(1).ElementAt(0).Name);

        }

        [Test]
        public void UpdatePageNumber()
        {
            logic.Begin();
            var test = logic.CategoryName + " \nPage " + (logic.PageIndex + 1) + "/" + logic.categories.ElementAt(logic.CategoryIndex).Value.Count;
            Assert.AreEqual(test, logic.UpdatePageNumber());


        }

        [Test]
        public void SaveMostUsedIfNotNull()
        {
            logic.Begin();
            logic.SaveMostUsedIfNotNull();
            Assert.NotNull(logic.mostUsed);

        }

        [Test]
        public void ResetMostUsedIfNotEmpty()
        {
            logic.Begin();
            logic.mostUsed.Clear();
            List<Picture> f = new List<Picture>();
            f.Add(new Picture("dd", false, "s", 1));
            logic.mostUsed.Add(f);

            Assert.AreEqual("Most Used category has been reset.", logic.ResetMostUsedIfNotEmpty());
            Assert.AreEqual("Most Used category is already empty.", logic.ResetMostUsedIfNotEmpty());


        }

        [Test]
        public void UpdateMostUsedPicture()
        {
            logic.Begin();
            for(int i = 0; i < 1000000; i++)
            {
                logic.UpdateMostUsedPicture(0, "my");

            }
            Assert.AreEqual("my", logic.mostUsedList.ElementAt(0).Name);


        }

        [Test]
        public void CheckMostUsedContainsWord()
        {
            logic.Begin();
            logic.mostUsedList = new List<Picture>();
            Assert.False(logic.CheckMostUsedContainsWord("not in the list"));
            logic.UpdateMostUsedPicture(0, "my");
            Assert.True(logic.CheckMostUsedContainsWord("my"));



        }

        [Test]
        public void AddWordToSentence()
        {
            logic.Begin();
            logic.AddWordToSentence("hello", 0);
            Assert.AreEqual(true, logic.Sentence.Contains("hello"));

        }

        [Test]
        public void RemoveWordFromSentence()
        {
            logic.Begin();
            logic.Sentence.Clear();
            logic.AddWordToSentence("hello", 0);
            logic.AddWordToSentence("world", 1);
            logic.RemoveWordFromSentence("world", 1);
            Assert.AreEqual(false, logic.Sentence.Contains("world"));

        }
    }
 }
