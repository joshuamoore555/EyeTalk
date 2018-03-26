using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.Factory;

namespace EyeTalk.Tests
{
    [TestClass]
    public class UITests
    {

        [TestMethod]
        public void OpenAppAndExit()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
            var exitButton = window.Get<Button>("Exit");
            exitButton.Click();

        }

        [TestMethod]
        public void OpenAppAndTestAddPicture()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
            var exitButton = window.Get<Button>("Exit");
            var add = window.Get<Button>("AddPicture");
            add.Click();

            var save = window.Get<Button>("Save_Custom_Button");
            save.Click();

            var select = window.Get<Button>("Select_Picture");
            select.Click();
            app.Close();

        }

        [TestMethod]
        public void OpenAppAndTestSavePicture()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
            var exitButton = window.Get<Button>("Exit");
            var add = window.Get<Button>("AddPicture");
            add.Click();

            var save = window.Get<Button>("Save_Custom_Button");
            save.Click();
            app.Close();

        }

        [TestMethod]
        public void OpenAppAndTestOptions()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
            var exitButton = window.Get<Button>("Exit");

            var options = window.Get<Button>("Options");
            options.Click();

            var rightspeed = window.Get<Button>("Right_Speed");
            rightspeed.Click();

            var leftspeed = window.Get<Button>("Left_Speed");
            leftspeed.Click();

            var reset = window.Get<Button>("Reset");
            reset.Click();

            var leftdelay = window.Get<Button>("Left_Delay");
            leftdelay.Click();

            var rightdelay = window.Get<Button>("Right_Delay");
            rightdelay.Click();

            var voicetype = window.Get<Button>("VoiceType");
            voicetype.Click();

            var testvoice = window.Get<Button>("TestVoice");
            testvoice.Click();

            var back = window.Get<Button>("Back");
            back.Click();

            exitButton.Click();

        }

        [TestMethod]
        public void OpenAppAndBeginSpeaking()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var picture1 = window.Get<Button>("Image1_Button");
            var picture2 = window.Get<Button>("Image2_Button");
            var picture3 = window.Get<Button>("Image3_Button");

            var playsound = window.Get<Button>("PlaySound");

            picture1.Click();
            picture2.Click();
            picture3.Click();
            playsound.Click();
            var sentence = window.Get<TextBox>("SentenceTextBox");
            Assert.IsTrue(sentence.Text == "Wash Hands Toilet Shower ");

            app.Close();
        }


        [TestMethod]
        public void OpenAppAndSaveSentence()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var picture1 = window.Get<Button>("Image1_Button");
            var picture2 = window.Get<Button>("Image2_Button");
            var picture3 = window.Get<Button>("Image3_Button");

            var playsound = window.Get<Button>("PlaySound");
            var save = window.Get<Button>("SaveSentence");


            picture1.Click();
            picture2.Click();
            picture3.Click();
            save.Click();
            app.Close();
        }

        [TestMethod]
        public void OpenAppAndChooseCategories()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var picture1 = window.Get<Button>("Image1_Button");
            var picture2 = window.Get<Button>("Image2_Button");
            var picture3 = window.Get<Button>("Image3_Button");

            var playsound = window.Get<Button>("PlaySound");
            var next = window.Get<Button>("Next");
            var previous = window.Get<Button>("Previous");
            next.Click();
            next.Click();
            next.Click();

            previous.Click();
            previous.Click();
            previous.Click();

            app.Close();
        }


        [TestMethod]
        public void OpenAppAndChangePage()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var page = window.Get<Button>("Page");

            var next = window.Get<Button>("Next");
            next.Click();
            page.Click();


            app.Close();
        }

        [TestMethod]
        public void OpenAppAndSpeakASavedSentence()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var list = window.Get<Button>("SentenceList");

            list.Click();
            var speak = window.Get<Button>("SpeakSentence");
            speak.Click();


            app.Close();
        }


        [TestMethod]
        public void OpenAppAndDeleteASavedSentence()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var list = window.Get<Button>("SentenceList");
            list.Click();

            var delete = window.Get<Button>("DeleteSentence");
            delete.Click();


            app.Close();
        }

        [TestMethod]
        public void OpenAppAndChooseSavedSentences()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var list = window.Get<Button>("SentenceList");
            list.Click();

            var next = window.Get<Button>("NextSentence");
            var previous = window.Get<Button>("PreviousSentence");

            next.Click();
            next.Click();
            next.Click();
            previous.Click();
            previous.Click();
            previous.Click();




            app.Close();
        }
    }
}
