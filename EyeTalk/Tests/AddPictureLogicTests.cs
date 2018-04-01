using EyeTalk.Logic;
using EyeTalk.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Tests
{
    class AddPictureLogicTests
    {
        AddPictureLogic logic;
        List<List<Picture>> customCategory;
        List<Picture> currentPage;
        Picture customPicture;
        Picture customPicture2;


        public AddPictureLogicTests()
        {
            logic = new AddPictureLogic();
        }


        [Test]
        public void CheckCustomPictureIsNotDuplicate()
        {
            customPicture = new Picture("Test", false, "fakepath", 0);
            currentPage = new List<Picture>();
            currentPage.Add(customPicture);
            customCategory.Add(currentPage);

            customPicture2 = new Picture("Test2", false, "fakepath2", 0);
            Assert.AreEqual(true, logic.CheckCustomPictureIsNotDuplicate(customPicture,customCategory));
            Assert.AreEqual(false, logic.CheckCustomPictureIsNotDuplicate(customPicture2, customCategory));

        }

        [Test]
        public void CreateNewPageAndAddCustomPicture()
        {

        }

        [Test]
        public void AddCustomPicture()
        {

        }

        [Test]
        public void CheckNumberOfCustomImagesInPage()
        {

        }
    }
}
