using EyeTalk.Logic;
using EyeTalk.Objects;
using EyeTalk.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace EyeTalk.Tests
{
    public class AddPictureLogicTests
    {
        AddPictureLogic logic;
        Keyboard keyboard;
        List<List<List<Picture>>> categories;
        List<List<Picture>> customCategory;
        List<Picture> currentPage;
        Picture customPicture;
        Picture customPicture2;
        Picture customPicture3;
        Picture customPicture4;
        Picture customPicture5;



        public AddPictureLogicTests()
        {
            logic = new AddPictureLogic();
        }

        private void Setup()
        {
            customPicture = new Picture("Test", false, "fakepath", 0);
            currentPage = new List<Picture>();
            categories = new List<List<List<Picture>>>();
            customCategory = new List<List<Picture>>();
            currentPage.Add(customPicture);
            customCategory.Add(currentPage);
            categories.Add(customCategory);
            customPicture2 = new Picture("Test2", false, "fakepath2", 0);
            customPicture3 = new Picture("Test3", false, "fakepath3", 0);
            customPicture4 = new Picture("Test4", false, "fakepath4", 0);
            customPicture5 = new Picture("Test5", false, "fakepath5", 0);


        }

        [Test]
        public void CheckCustomPictureIsNotDuplicate()
        {
            Setup();

            Assert.AreEqual(true, logic.CheckCustomPictureIsNotDuplicate(customPicture, categories));
            Assert.AreEqual(false, logic.CheckCustomPictureIsNotDuplicate(customPicture2, categories));

        }

        [Test]
        public void CreateNewPageAndAddCustomPicture()
        {
            Setup();
            List<Picture> newCustomCategory = new List<Picture>() { };
            newCustomCategory.Add(customPicture);
            Assert.AreEqual(newCustomCategory, logic.CreateNewPageAndAddCustomPicture(customPicture));

        }

        [Test]
        public void AddCustomPicture()
        {
            Setup();
            logic.AddCustomPicture(customPicture2, currentPage);
            Assert.IsTrue(currentPage.Contains(customPicture2));
        }

        [Test]
        public void CheckNumberOfCustomImagesInPage()
        {
            Setup();
            currentPage.Add(customPicture2);
            currentPage.Add(customPicture3);
            
            Assert.AreEqual(true, logic.CheckNumberOfCustomImagesInPage(currentPage));
            currentPage.Add(customPicture5);
            Assert.AreEqual(false, logic.CheckNumberOfCustomImagesInPage(currentPage));

        }

        [Test]
        public void TestKeyboardFunctions()
        {
            keyboard = new Keyboard();
            string test = "hello worl";
            
            Assert.AreEqual(keyboard.AddLetter(test, 'd'), "hello world");
            Assert.AreEqual(keyboard.AddLetter("123456789123456789123456789123", 'd'), "123456789123456789123456789123");

            Assert.AreEqual(keyboard.DeleteLastLetter(test), "hello wor");
            Assert.AreEqual(keyboard.DeleteLastLetter(""), "");


        }

        [Test]
        public void TestChangingPictures()
        {
            logic = new AddPictureLogic();
            
            Assert.True(logic.GetFirstImageFromPicturesFolder()!=null);
            Assert.True(logic.GetNextPictureFromPicturesFolder() != null);
            Assert.True(logic.GetPreviousPictureFromPicturesFolder() != null);
      
        }
    }
}
