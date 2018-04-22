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
        public void OpenAppAndChooseCategoryWithCategoryPage()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            beginButton.Click();

            var ChooseCategory = window.Get<Button>("ChooseCategory");
            ChooseCategory.Click();

            var Actions = window.Get<Button>("Actions");
            Actions.Click();
            ChooseCategory.Click();

            var BackToSpeak = window.Get<Button>("BackToSpeak");
            BackToSpeak.Click();
            ChooseCategory.Click();

            var Food = window.Get<Button>("Food");
            Food.Click();
            ChooseCategory.Click();

            var Drink = window.Get<Button>("Drink");
            Drink.Click();
            ChooseCategory.Click();

            var Greetings = window.Get<Button>("Greetings");
            Greetings.Click();
            ChooseCategory.Click();

            var Feelings = window.Get<Button>("Feelings");
            Feelings.Click();
            ChooseCategory.Click();

            var Emotions = window.Get<Button>("Emotions");
            Emotions.Click();
            ChooseCategory.Click();

            var Colours = window.Get<Button>("Colours");
            Colours.Click();
            ChooseCategory.Click();

            var Animals = window.Get<Button>("Animals");
            Animals.Click();
            ChooseCategory.Click();

            var Times = window.Get<Button>("Times");
            Times.Click();
            ChooseCategory.Click();

            var Carers = window.Get<Button>("Carers");
            Carers.Click();
            ChooseCategory.Click();

            var Kitchen = window.Get<Button>("Kitchen");
            Kitchen.Click();
            ChooseCategory.Click();

            var Bathroom = window.Get<Button>("Bathroom");
            Bathroom.Click();
            ChooseCategory.Click();

            var Entertainment = window.Get<Button>("Entertainment");
            Entertainment.Click();
            ChooseCategory.Click();

            var Family = window.Get<Button>("Family");
            Family.Click();
            ChooseCategory.Click();

            var Custom = window.Get<Button>("Custom");
            Custom.Click();
            ChooseCategory.Click();

            var MostUsed = window.Get<Button>("MostUsed");
            MostUsed.Click();
            ChooseCategory.Click();

            var ConnectingWords = window.Get<Button>("ConnectingWords");
            ConnectingWords.Click();
            ChooseCategory.Click();

            var Replies = window.Get<Button>("Replies");
            Replies.Click();
            ChooseCategory.Click();

            var GettingDressed = window.Get<Button>("GettingDressed");
            GettingDressed.Click();
            ChooseCategory.Click();

            var PersonalCare = window.Get<Button>("PersonalCare");
            PersonalCare.Click();
            ChooseCategory.Click();

            app.Close();
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
        public void OpenAppAndTestKeyboard()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
            
            var add = window.Get<Button>("AddPicture");
            add.Click();

            var NextPicture = window.Get<Button>("NextPicture");
            NextPicture.Click();

            var PreviousPicture = window.Get<Button>("PreviousPicture");
            PreviousPicture.Click();

            var KeyboardPage = window.Get<Button>("KeyboardPage");
            KeyboardPage.Click();

            var A = window.Get<Button>("A");
            A.Click();

            var B = window.Get<Button>("B");
            B.Click();

            var C = window.Get<Button>("C");
            C.Click();

            var D = window.Get<Button>("D");
            D.Click();

            var E = window.Get<Button>("E");
            E.Click();

            var F = window.Get<Button>("F");
            F.Click();

            var G = window.Get<Button>("G");
            G.Click();

            var H = window.Get<Button>("H");
            H.Click();

            var I = window.Get<Button>("I");
            I.Click();

            var J = window.Get<Button>("J");
            J.Click();

            var K = window.Get<Button>("K");
            K.Click();

            var L = window.Get<Button>("L");
            L.Click();

            var M = window.Get<Button>("M");
            M.Click();

            var N = window.Get<Button>("N");
            N.Click();

            var O = window.Get<Button>("O");
            O.Click();

            var P = window.Get<Button>("P");
            P.Click();

            var Q = window.Get<Button>("Q");
            Q.Click();

            var R = window.Get<Button>("R");
            R.Click();

            var S = window.Get<Button>("S");
            S.Click();

            var T = window.Get<Button>("T");
            T.Click();

            var U = window.Get<Button>("U");
            U.Click();

            var V = window.Get<Button>("V");
            V.Click();

            var W = window.Get<Button>("W");
            W.Click();

            var X = window.Get<Button>("X");
            X.Click();

            var Y = window.Get<Button>("Y");
            Y.Click();

            var Z = window.Get<Button>("Z");
            Z.Click();

            var Delete = window.Get<Button>("Delete");
            Delete.Click();

            var Space = window.Get<Button>("Space");
            Space.Click();

            var Enter = window.Get<Button>("Enter");
            Enter.Click();

            KeyboardPage.Click();

            var BackSave = window.Get<Button>("BackSave");
            BackSave.Click();

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

            var resetMostUsed = window.Get<Button>("ResetMostUsed");
            resetMostUsed.Click();

            var resetCustomPictures = window.Get<Button>("ResetCustomPictures");
            resetCustomPictures.Click();

            var colour = window.Get<Button>("ColourType");
            colour.Click();

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
            var picture4 = window.Get<Button>("Image4_Button");
            picture1.Click();
            picture2.Click();
            picture3.Click();
            picture4.Click();
            var playsound = window.Get<Button>("PlaySound");
            playsound.Click();
            var sentence = window.Get<TextBox>("SentenceTextBox");
            Assert.IsTrue(sentence.Text == "I want I don't want I like I don't like ");
            var removeall = window.Get<Button>("RemoveAll");
            removeall.Click();
            playsound.Click();

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

        [TestMethod]
        public void OpenAppAndChooseACategoryFromCategoryPage()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);

            //Test Home Buttons
            var beginButton = window.Get<Button>("BeginSpeaking");
            var addPictureButton = window.Get<Button>("AddPicture");
            var optionsButton = window.Get<Button>("Options");
            var exitButton = window.Get<Button>("Exit");
            beginButton.Click();

            var chooseCategory = window.Get<Button>("ChooseCategory");
            chooseCategory.Click();

            var food = window.Get<Button>("Food");
            food.Click();

            var Image1_Button = window.Get<Button>("Image1_Button");
            Image1_Button.Click();

            var playsound = window.Get<Button>("PlaySound");


            playsound.Click();

            app.Close();
        }


    }
}
