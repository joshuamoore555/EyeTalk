using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EyeTalk.Objects;
using EyeTalk.Logic;
using EyeTalk.Utilities;
using EyeTalk.Constants;
using System.Media;


namespace EyeTalk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    { 
        private string previousPosition;
        private string currentPosition;

        List<Image> images;
        List<Button> buttons;
        List<TextBlock> textBlocks;

        EyeTracker eyeTracker;
        SaveFileSerialiser saveInitialiser;
        SpeechGenerator speech;
        CoordinateTimer timer;
        Keyboard keyboard;
    
        SentenceLogic sentenceLogic;
        OptionsLogic optionsLogic;
        AddPictureLogic addPictureLogic;
        SavedSentencesLogic savedSentencesLogic;
        Brush brush;
        bool isSpeaking = false;


        public MainWindow()
        {
            InitializeComponent();

            eyeTracker = new EyeTracker();
            speech = new SpeechGenerator();

            timer = new CoordinateTimer();
            sentenceLogic = new SentenceLogic();
            optionsLogic = new OptionsLogic();
            savedSentencesLogic = new SavedSentencesLogic();
            addPictureLogic = new AddPictureLogic();
            saveInitialiser = new SaveFileSerialiser();
            keyboard = new Keyboard();


            brush = GetBrush();
            timer.coordinateTimer.Elapsed += GetCurrentPosition;
            LoadSaveFiles();
            UpdateOptionsPage();
            UpdateGUI();
            
        }

        private void UpdateGUI()
        {
            ResetHomePage();
            ResetSentencePage();
            ResetOptionsPage();
            ResetSaveSentencePage();
            ResetChooseCategoryPage();
            ResetSaveSentencePage();
            ResetKeyboardPage();
        }

        //Home Page Buttons

        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            LoadSaveFiles();
            sentenceLogic.ResetCategoryChoice();
            sentenceLogic.ResetSentence();
            ResetSentencePageText();
            GoToSentencePage();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            UpdateOptionsPage();           
            myTabControl.SelectedIndex = 2;
        }

        private void Add_PictureCategory_Click(object sender, RoutedEventArgs e)
        {
            CustomFilePath.Text = addPictureLogic.GetFirstImageFromPicturesFolder();
            ConvertFilepathIntoImage(CustomFilePath.Text);
            myTabControl.SelectedIndex = 3;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            SaveAllFiles();
            eyeTracker.eyeTracking = false;
            timer.coordinateTimer.Stop();
            Application.Current.Dispatcher.BeginInvokeShutdown(System.Windows.Threading.DispatcherPriority.Send);
        }



        //Begin Speaking Button Clicks

        private void Sentences_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveAllFiles();
            currentSentence.Text = savedSentencesLogic.RetrieveFirstSavedSentenceIfExists();
            myTabControl.SelectedIndex = 4;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveAllFiles();
            brush = GetBrush();
            myTabControl.SelectedIndex = 0;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SentenceUpdate.Text = savedSentencesLogic.SaveSentenceIfNotPreviouslySaved(SentenceTextBox.Text);
        }

        private async void PlaySound_Button_Click(object sender, RoutedEventArgs e)
        {

            var sentence = SentenceTextBox.Text;
            if (isSpeaking == false)
            {
                isSpeaking = true;
                await Task.Run(() => speech.Speak(sentence));
                isSpeaking = false;
            }
     
        }

        private void Image1_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            if (sentenceLogic.GetNumberOfPicturesInCurrentCategory() >= 1)
            {
                SelectPicture(index);
            }
        }

        private void Image2_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            if (sentenceLogic.GetNumberOfPicturesInCurrentCategory() >= 2)
            {
                SelectPicture(index);
            }
        }

        private void Image3_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 2;
            if (sentenceLogic.GetNumberOfPicturesInCurrentCategory() >= 3)
            {
                SelectPicture(index);
            }
        }

        private void Image4_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 3;
            if(sentenceLogic.GetNumberOfPicturesInCurrentCategory() >= 4)
            {
                SelectPicture(index);
            }
        }

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.GoToNextPage();
            CreatePage();
            
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.CheckIfBackToFirstCategory();
            sentenceLogic.GenerateSentencePageAndGoToFirstPage();
            CreatePage();
            

        }

        private void Previous_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.CheckIfBackToLastCategory();
            sentenceLogic.GenerateSentencePageAndGoToFirstPage();
            CreatePage();
            
        }

        private void Remove_All_Click(object sender, RoutedEventArgs e)
        {
            SentenceTextBox.Clear();
            sentenceLogic.RemoveAllWordsFromSentence();
            ClickOff();
            CreatePage();

        }

        //Begin Speaking Methods

        private void CreatePage()
        {
            var numberOfPictures = sentenceLogic.CategoryPage.Count;
            SentenceUpdate.Text = " ";
            PageNumber.Text = sentenceLogic.GetPageNumber();
            CategoryName.Text = sentenceLogic.GetCurrentCategoryName();
            NextCategoryText.Text = sentenceLogic.GetNextCategoryName();
            PreviousCategoryText.Text = sentenceLogic.GetPreviousCategoryName();


            if (numberOfPictures > 0)
            {
                for (int i = 0; i < numberOfPictures; i++)
                {
                    CreatePicture(i);
                    UpdatePicture(i);
                }

                for (int i = numberOfPictures; i < 4; i++)
                {
                    HidePicture(i);
                }
            }
            else
            {
                for (int i = 0; i < images.Count; i++)
                {
                    HidePicture(i);
                }
            }
        }

        private void CreatePicture(int i)
        {
            textBlocks.ElementAt(i).Text = sentenceLogic.CategoryPage.ElementAt(i).Name;
            buttons.ElementAt(i).Visibility = Visibility.Visible;
            var filepath = sentenceLogic.CategoryPage.ElementAt(i).Filepath;
            images.ElementAt(i).Source = sentenceLogic.GenerateBitmap(filepath);
        }

        private void UpdatePicture(int i)
        {
            var name = sentenceLogic.CategoryPage.ElementAt(i).Name;
            
            if (!sentenceLogic.CheckThatWordIsMatched(SentenceTextBox.Text, name))
            {
                UnhighlightPicture(buttons, i);
            }
            else if(sentenceLogic.CheckThatWordIsMatched(SentenceTextBox.Text, name))
            {
                HighlightPicture(buttons, i);
            }
        
        }

        private void HidePicture(int i)
        {
            buttons.ElementAt(i).Visibility = Visibility.Hidden;
            textBlocks.ElementAt(i).Text = "";
            UnhighlightPicture(buttons, i);
        }

        private void HighlightPicture(List<Button> buttons, int i)
        {
            buttons.ElementAt(i).BorderBrush = new SolidColorBrush(Colors.Yellow);
            buttons.ElementAt(i).BorderThickness = new Thickness(12, 12, 12, 12);
        }

        private void UnhighlightPicture(List<Button> buttons, int i)
        {
            buttons.ElementAt(i).BorderBrush = new SolidColorBrush(Colors.Black);
            buttons.ElementAt(i).BorderThickness = new Thickness(1, 1, 1, 1);

        }

        private void SelectPicture(int i)
        {
            var word = textBlocks.ElementAt(i).Text;
            var selected = sentenceLogic.CategoryPage.ElementAt(i).Selected;
            var name = sentenceLogic.CategoryPage.ElementAt(i).Name;

            if (sentenceLogic.CheckThatWordIsMatched(SentenceTextBox.Text, name) && selected == false)
            {
                SentenceUpdate.Text = "This word has already been added.";
            }

            else
            {
                if (sentenceLogic.AmountOfWordsInSentence < 8 && selected == false)
                {
                    SentenceTextBox.Text = sentenceLogic.AddWordToSentence(word, i);
                    sentenceLogic.UpdateMostUsedPicture(i, word);
                    HighlightPicture(buttons, i);
                    ClickOn();
                }
                else if (selected == true)
                {
                    SentenceTextBox.Text = sentenceLogic.RemoveWordFromSentence(word, i);
                    UnhighlightPicture(buttons, i);
                    ClickOff();

                }
            }
        
         }

        private void ClickOn()
        {
            new SoundPlayer(Properties.Resources.ClickOn).Play();
        }

        private void ClickOff()
        {
            new SoundPlayer(Properties.Resources.ClickOff).Play();
        }



        //Saved Sentences Page Button Clicks

        private async void Play_Saved_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var sentence = currentSentence.Text;
            if (isSpeaking == false)
            {
                isSpeaking = true;
                await Task.Run(() => speech.Speak(sentence));
                isSpeaking = false;
            }
        }

        private void Next_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {

            currentSentence.Text = savedSentencesLogic.NextSentence();
            
        }

        private void Previous_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            currentSentence.Text = savedSentencesLogic.PreviousSentence();

        }

        private void Delete_Saved_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            currentSentence.Text = savedSentencesLogic.DeleteSavedSentence();  
        }



        //Options Page Button Clicks

        private void ResetMostUsed_Click(object sender, RoutedEventArgs e)
        {
           ResetMostUsedText.Text = sentenceLogic.ResetMostUsedIfNotEmpty();
        }

        private void ResetCustomPicture_Click(object sender, RoutedEventArgs e)
        {
            ResetCustomText.Text = sentenceLogic.ResetCustomPictureCategoryIfNotEmpty();
        }

        private void VoiceType_Click(object sender, RoutedEventArgs e)
        {
            var type = optionsLogic.ChangeVoiceType();
            VoiceType.Content = "Voice Type: " + type;

            speech.ChooseVoice(type);
        }

        private void Right_Delay_Click(object sender, RoutedEventArgs e)
        {
            var currentSelectionDelay = optionsLogic.IncreaseSelectionDelay();
            EyeSelectionSpeedStatus.Text = "Eye Fixation Value: " + currentSelectionDelay;
        }

        private void Left_Delay_Click(object sender, RoutedEventArgs e)
        {
            var currentSelectionDelay = optionsLogic.DecreaseSelectionDelay();
            EyeSelectionSpeedStatus.Text = "Eye Fixation Value: " + currentSelectionDelay;

        }

        private void Right_Speed_Click(object sender, RoutedEventArgs e)
        {
            var currentSpeed = optionsLogic.IncreaseVoiceSpeed();
            SpeedStatus.Text = "Voice Speed: " + currentSpeed;
            speech.ChooseSpeedOfVoice(currentSpeed);
        }

        private void Left_Speed_Click(object sender, RoutedEventArgs e)
        {
            var currentSpeed = optionsLogic.DecreaseVoiceSpeed();
            SpeedStatus.Text = "Voice Speed: " + currentSpeed;
            speech.ChooseSpeedOfVoice(currentSpeed);

        }

        private async void TestVoice_Click(object sender, RoutedEventArgs e)
        {
            if (isSpeaking == false)
            {
                isSpeaking = true;
                await Task.Run(() => speech.Speak("This is testing my voice"));
                isSpeaking = false;
            }
        }

        private void ColourType_Click(object sender, RoutedEventArgs e)
        {
            optionsLogic.ChangeColour();
            brush = GetBrush();
            UpdateGUI();

        }


        //Options Methods

        private void UpdateOptionsPage()
        {
            speech.ChooseVoice(optionsLogic.VoiceTypes.ElementAt(optionsLogic.Options.VoiceTypeSelection));
            speech.ChooseSpeedOfVoice(optionsLogic.VoiceSpeeds.ElementAt(optionsLogic.Options.VoiceSpeedSelection));

            VoiceType.Content = "Voice Type: " + optionsLogic.VoiceTypes.ElementAt(optionsLogic.Options.VoiceTypeSelection);
            SpeedStatus.Text = "Voice Speed: " + optionsLogic.VoiceSpeeds.ElementAt(optionsLogic.Options.VoiceSpeedSelection);
            EyeSelectionSpeedStatus.Text = "Eye Fixation Value: " + optionsLogic.Options.EyeFixationValue / 4 + " Seconds";
        }

        private Brush GetBrush()
        {
            var converter = new BrushConverter();
            string currentColour = optionsLogic.GetCurrentColour();
            var brush = (Brush)converter.ConvertFromString(currentColour);
            return brush;
        }


        //Add Custom Picture Buttons

        private void Load_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = addPictureLogic.LoadCustomPicture();
            CustomFilePath.Text = picturePath;
            ConvertFilepathIntoImage(picturePath);
        }

        private void ConvertFilepathIntoImage(string filepath)

        {       
            CustomName.Text = addPictureLogic.GeneratePictureName(filepath);
            BitmapImage bitmap = addPictureLogic.GeneratePicture(filepath);

            if(bitmap != null)
            {
                CustomPicture.Source = bitmap;
            }
        }

        private void Save_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CustomFilePath.Text) || String.IsNullOrEmpty(CustomName.Text))
            {
                Status.Text = "Please select a picture and name before saving";

            }
            else
            {
                Picture customPicture = new Picture(CustomName.Text, false, CustomFilePath.Text, 0);
                var currentPage = sentenceLogic.customCategory.ElementAt(sentenceLogic.customCategory.Count - 1);
                var pictureAlreadyAdded = addPictureLogic.CheckCustomPictureIsNotDuplicate(customPicture, sentenceLogic.categories);
                var pictureAlreadyAddedInCustom = addPictureLogic.CheckCustomPictureIsNotDuplicateInCustomCategory(customPicture, sentenceLogic.customCategory);



                if (pictureAlreadyAdded || pictureAlreadyAddedInCustom)
                {
                    Status.Text = "This image has already been added.";
                }
                else
                {
                    var spaceInCustomCategory = addPictureLogic.CheckNumberOfCustomImagesInPage(currentPage);

                    if (spaceInCustomCategory == true)
                    {
                        addPictureLogic.AddCustomPicture(customPicture, currentPage);
                        Status.Text = "Added picture. " + "Number of pictures in category: " + currentPage.Count;
                        saveInitialiser.SaveCustomCategory(sentenceLogic.customCategory);

                    }

                    else if (spaceInCustomCategory == false)
                    {
                        sentenceLogic.customCategory.Add(addPictureLogic.CreateNewPageAndAddCustomPicture(customPicture));
                        Status.Text = "Created new category. \nNumber of categories is now: " + sentenceLogic.customCategory.Count;
                        saveInitialiser.SaveCustomCategory(sentenceLogic.customCategory);

                    }

                }
            }
        }

        private void NextPicture_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomFilePath.Text = addPictureLogic.GetNextPictureFromPicturesFolder();
            ConvertFilepathIntoImage(CustomFilePath.Text);
        }

        private void PreviousPicture_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomFilePath.Text = addPictureLogic.GetPreviousPictureFromPicturesFolder();
            ConvertFilepathIntoImage(CustomFilePath.Text);

        }


        //Coordinate Checking Methods

        public void GetCurrentPosition(object sender, EventArgs e)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                if (myTabControl.SelectedIndex == 1)
                {
                    currentPosition = eyeTracker.GetCurrentPositionBeginSpeaking();
                }
                else if (myTabControl.SelectedIndex == 0)
                {
                    currentPosition = eyeTracker.GetCurrentPositionHomePage();
                }
                else if (myTabControl.SelectedIndex == 5)
                {
                    currentPosition = eyeTracker.GetCurrentPositionChooseCategory();
                }
                else if (myTabControl.SelectedIndex == 4)
                {
                    currentPosition = eyeTracker.GetCurrentPositionSavedSentence();
                }
                else if (myTabControl.SelectedIndex == 6)
                {
                    currentPosition = eyeTracker.GetCurrentPositionKeyboard();

                }
                else
                {
                    currentPosition = eyeTracker.GetCurrentPosition();
                }

                CheckPositionIsTheSame();

                previousPosition = currentPosition;

            }));

        }

        private void CheckPositionIsTheSame()
        {
            if (currentPosition == previousPosition)
            {
                optionsLogic.IncreaseEyeFixationDuration();

                if (myTabControl.SelectedIndex == 0)
                {
                    CheckHomePage(currentPosition);
                }
                else if (myTabControl.SelectedIndex == 1)
                {
                    CheckSpeakingPage(currentPosition);
                }
                else if (myTabControl.SelectedIndex == 2)
                {
                    CheckOptionsPage(currentPosition);
                }
                else if (myTabControl.SelectedIndex == 3)
                {
                    CheckAddPicturePage(currentPosition);
                }
                else if (myTabControl.SelectedIndex == 4)
                {
                    CheckSavedSentencePage(currentPosition);
                }
                else if (myTabControl.SelectedIndex == 5)
                {
                    CheckChooseCategoryPage(currentPosition);
                }
                else if (myTabControl.SelectedIndex == 6)
                {
                    CheckKeyboardPage(currentPosition);
                }
            }
            else
            {
                optionsLogic.ResetEyeFixationDuration();
            }
        }

        private void CheckHomePage(string position)
        {
            ResetHomePage();
            switch (position) {

                case Positions.TopLeft:
                    HoverOverButton(BeginSpeaking);
                    break;

                case Positions.TopRight:
                    HoverOverButton(AddPicture);
                    break;

                case Positions.MiddleLeft:
                    break;

                case Positions.MiddleRight:
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(Options);
                    break;

                case Positions.BottomRight:
                    HoverOverButton(Exit);
                    break;

            }
        
        }

        private void CheckSpeakingPage(string position)
        {
            ResetSentencePage();
            switch (position)
            {
                case Positions.TopLeftAlternate:
                    HoverOverButton(SaveSentence);
                    break;

                case Positions.TopLeft:
                    HoverOverButton(SentenceList);
                    break;

                case Positions.TopMiddleLeft:
                    HoverOverButton(Image1_Button);
                    break;

                case Positions.TopMiddleRight:
                    HoverOverButton(Image2_Button);
                    break;

                case Positions.TopRight:
                    HoverOverButton(RemoveAll);
                    break;

                case Positions.MiddleLeft:
                    HoverOverButton(Previous);
                    break;

                case Positions.MiddleLeftAlternate:
                    HoverOverButton(Previous);
                    break;

                case Positions.MiddleMiddleLeft:
                    break;

                case Positions.MiddleMiddleRight:
                    break;

                case Positions.MiddleRight:
                    HoverOverButton(Next);
                    break;

                case Positions.MiddleRightAlternate:
                    HoverOverButton(Next);
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(ChooseCategory);
                    break;

                case Positions.BottomLeftAlternate:
                    HoverOverButton(Home);
                    break;

                case Positions.BottomMiddleLeft:
                    HoverOverButton(Image3_Button);
                    break;

                case Positions.BottomMiddleRight:
                    HoverOverButton(Image4_Button);
                    break;

                case Positions.BottomMiddleRightAlternate:
                    HoverOverButton(Image4_Button);
                    break;

                case Positions.BottomRight:
                    HoverOverButton(Page);
                    break;

                case Positions.BottomRightAlternate:
                    HoverOverButton(PlaySound);
                    break;

                case "":
                    break;


            }
        }

        private void CheckAddPicturePage(string position)
        {
            ResetAddPicturePage();
            switch (position)
            {
                case Positions.TopLeft:
                    HoverOverButton(KeyboardPage);
                    break;

                case Positions.TopMiddleLeft:
                    HoverOverButton(KeyboardPage);
                    break;

                case Positions.TopMiddleRight:
                    
                    break;

                case Positions.TopRight:
                    position = "";
                    HoverOverButton(Select_Picture);
                    break;

                case Positions.MiddleLeft:
                   
                    HoverOverButton(PreviousPicture);
                    break;

                case Positions.MiddleMiddleLeft:

                    break;

                case Positions.MiddleMiddleRight:
                    break;

                case Positions.MiddleRight:
                    HoverOverButton(NextPicture);

                    break;

                case Positions.BottomLeft:
                    HoverOverButton(BackHome);
                    break;

                case Positions.BottomMiddleLeft:
                    break;

                case Positions.BottomMiddleRight:
                    
                    break;

                case Positions.BottomRight:
                    HoverOverButton(Save_Custom_Button);
                    break;

                case "":
                    break;

            }
        }

        private void CheckSavedSentencePage(string position)
        {
            ResetSaveSentencePage();
            switch (position)
            {
                case Positions.TopLeft:
                    break;

                case Positions.TopMiddleLeft:
                    break;

                case Positions.TopMiddleRight:
                    break;

                case Positions.TopRight:
                    break;

                case Positions.MiddleLeft:
                    HoverOverButton(PreviousSentence);
                    break;

                case Positions.MiddleMiddleLeft:
                    break;

                case Positions.MiddleMiddleRight:
                    break;

                case Positions.MiddleRight:
                    HoverOverButton(NextSentence);
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(BackToSpeaking);
                    break;

                case Positions.BottomMiddleLeft:
                    HoverOverButton(SpeakSentence);
                    break;

                case Positions.BottomMiddleRight:
                    HoverOverButton(SpeakSentence);

                    break;

                case Positions.BottomRight:
                    HoverOverButton(DeleteSentence);
                    break;

                case "":
                    break;

            }
        }

        private void CheckOptionsPage(string position)
        {
            ResetOptionsPage();

            switch (position)
            {
                case Positions.TopLeft:
                    HoverOverButton(Left_Speed);
                    break;

                case Positions.TopMiddleLeft:
                    HoverOverButton(Right_Speed);
                    break;

                case Positions.TopMiddleRight:
                    HoverOverButton(Left_Delay);
                    break;

                case Positions.TopRight:
                    HoverOverButton(Right_Delay);
                    break;

                case Positions.MiddleLeft:
                    HoverOverButton(ResetCustomPictures);
                    break;

                case Positions.MiddleMiddleLeft:
                    break;

                case Positions.MiddleMiddleRight:
                    break;

                case Positions.MiddleRight:
                    HoverOverButton(ResetMostUsed);
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(Back);
                    break;

                case Positions.BottomMiddleLeft:
                    HoverOverButton(VoiceType);
                    break;

                case Positions.BottomMiddleRight:
                    HoverOverButton(ColourType);

                    break;

                case Positions.BottomRight:
                    HoverOverButton(TestVoice);
                    break;

                case "":
                    break;

            }
        }

        private void CheckChooseCategoryPage(string position)
        {
            ResetChooseCategoryPage();

            switch (position)
            {
                case Positions.TopLeft:
                    HoverOverButton(Food);
                    break;

                case Positions.TopLeftAlternate:
                    HoverOverButton(Actions);
                    break;

                case Positions.TopMiddleLeft:
                    HoverOverButton(Greetings);
                    break;

                case Positions.TopMiddleLeftAlternate:
                    HoverOverButton(Drink);
                    break;

                case Positions.TopMiddleRight:
                    HoverOverButton(Emotions);
                    break;

                case Positions.TopMiddleRightAlternate:
                    HoverOverButton(Feelings);
                    break;

                case Positions.TopRight:
                    HoverOverButton(Colours);
                    break;

                case Positions.TopRightAlternate:
                    HoverOverButton(Animals);
                    break;

                case Positions.MiddleLeft:
                    HoverOverButton(Carers);
                    break;

                case Positions.MiddleLeftAlternate:
                    HoverOverButton(Times);
                    break;

                case Positions.MiddleMiddleLeft:

                    break;

                case Positions.MiddleMiddleLeftAlternate:
                    HoverOverButton(Kitchen);
                    break;

                case Positions.MiddleMiddleRight:
                    HoverOverButton(PersonalCare);
                    break;

                case Positions.MiddleMiddleRightAlternate:
                   
                    break;

                case Positions.MiddleRight:
                    HoverOverButton(Entertainment);
                    break;

                case Positions.MiddleRightAlternate:
                    HoverOverButton(Family);
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(BackToSpeak);
                    break;

                case Positions.BottomLeftAlternate:
                    HoverOverButton(BackToSpeak);
                    break;

                case Positions.BottomMiddleLeft:
                    HoverOverButton(MostUsed);
                    break;

                case Positions.BottomMiddleLeftAlternate:
                    HoverOverButton(Replies);
                    break;

                case Positions.BottomMiddleRight:
                    HoverOverButton(ConnectingWords);
                    break;

                case Positions.BottomMiddleRightAlternate:
                    HoverOverButton(Custom);
                    break;

                case Positions.BottomRight:
                    break;

                case Positions.BottomRightAlternate:
                    break;

                case "":
                    break;

            }
        }

        private void CheckKeyboardPage(string position)
        {
            ResetKeyboardPage();

            switch (position)
            {

                case "Q":
                    HoverOverButton(Q);
                    break;

                case "W":
                    HoverOverButton(W);
                    break;

                case "E":
                    HoverOverButton(E);
                    break;

                case "R":
                    HoverOverButton(R);
                    break;

                case "T":
                    HoverOverButton(T);
                    break;

                case "Y":
                    HoverOverButton(Y);
                    break;

                case "U":
                    HoverOverButton(U);
                    break;

                case "I":
                    HoverOverButton(I);
                    break;

                case "O":
                    HoverOverButton(O);
                    break;

                case "P":
                    HoverOverButton(P);
                    break;

                case "A":
                    HoverOverButton(A);
                    break;

                case "S":
                    HoverOverButton(S);
                    break;

                case "D":
                    HoverOverButton(D);
                    break;

                case "F":
                    HoverOverButton(F);
                    break;

                case "G":
                    HoverOverButton(G);
                    break;

                case "H":
                    HoverOverButton(H);
                    break;

                case "J":
                    HoverOverButton(J);
                    break;

                case "K":
                    HoverOverButton(K);
                    break;

                case "L":
                    HoverOverButton(L);
                    break;

                case "Z":
                    HoverOverButton(Z);
                    break;

                case "X":
                    HoverOverButton(X);
                    break;

                case "C":
                    HoverOverButton(C);
                    break;

                case "V":
                    HoverOverButton(V);
                    break;

                case "B":
                    HoverOverButton(B);
                    break;

                case "N":
                    HoverOverButton(N);
                    break;

                case "M":
                    HoverOverButton(M);
                    break;

                case "Space":
                    HoverOverButton(Space);
                    break;

                case "Enter":
                    HoverOverButton(Enter);
                    break;

                case "Back":
                    HoverOverButton(BackSave);
                    break;

                case "Delete":
                    HoverOverButton(Delete);
                    break;

                case "":
                    break;

            }
        }

        private void HoverOverButton(Button button)
        {
            button.Background = Brushes.Yellow;

            if (optionsLogic.HasDurationBeenReached())
            {
                ResetPosition();
                button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }


        //Reset Methods

        private void ResetPosition()
        {
            currentPosition = " ";
            previousPosition = "";
        }

        private void ResetHomePage()
        {
            BeginSpeaking.Background = brush;
            AddPicture.Background = brush;
            Options.Background = brush;
            Exit.Background = brush;
        }

        private void ResetSentencePage()
        {
            SaveSentence.Background = brush;
            SentenceList.Background = brush;

            Previous.Background = Brushes.Lavender;
            Next.Background = Brushes.Lavender;

            Home.Background = brush;
            ChooseCategory.Background = brush;

            PlaySound.Background = brush;
            Page.Background = brush;

            RemoveAll.Background = brush;

            Image1_Button.Background = Brushes.Transparent;
            Image2_Button.Background = Brushes.Transparent;
            Image3_Button.Background = Brushes.Transparent;
            Image4_Button.Background = Brushes.Transparent;
        }

        private void ResetAddPicturePage()
        {
            CustomName.Background = Brushes.White;
            Select_Picture.Background = brush;
            BackHome.Background = brush;
            Save_Custom_Button.Background = brush;
            KeyboardPage.Background = brush;
            NextPicture.Background = Brushes.Lavender;
            PreviousPicture.Background = Brushes.Lavender;
        }

        private void ResetSaveSentencePage()
        {
            SpeakSentence.Background = brush;
            NextSentence.Background = Brushes.Lavender;
            PreviousSentence.Background = Brushes.Lavender;
            BackToSpeaking.Background = brush;
            DeleteSentence.Background = brush;
        }

        private void ResetOptionsPage()
        {
            Left_Speed.Background = brush;
            Right_Speed.Background = brush;
            Left_Delay.Background = brush;
            Right_Delay.Background = brush;
            Back.Background = brush;
            TestVoice.Background = brush;
            KeyboardPage.Background = brush;

            ResetMostUsed.Background = Brushes.Lavender;
            ResetCustomPictures.Background = Brushes.Lavender;
            VoiceType.Background = Brushes.Lavender;
            ColourType.Background = Brushes.Lavender;

        }

        private void ResetChooseCategoryPage()
        {
            
            BackToSpeak.Background  = brush;

            Actions.Background = Brushes.Lavender;
            Food.Background = Brushes.Lavender;
            Drink.Background = Brushes.Lavender;
            Greetings.Background = Brushes.Lavender;
            Feelings.Background = Brushes.Lavender;
            Emotions.Background = Brushes.Lavender;
            Colours.Background = Brushes.Lavender;
            Times.Background = Brushes.Lavender;
            Carers.Background = Brushes.Lavender;
            Kitchen.Background = Brushes.Lavender;
            PersonalCare.Background = Brushes.Lavender;
            Entertainment.Background = Brushes.Lavender;
            Replies.Background = Brushes.Lavender;
            MostUsed.Background = Brushes.Lavender;
            Custom.Background = Brushes.Lavender;
            Animals.Background = Brushes.Lavender;
            Family.Background = Brushes.Lavender;

        }

        private void ResetKeyboardPage()
        {

            Enter.Background = brush;
            Delete.Background = brush;
            BackSave.Background = brush;
            Space.Background = Brushes.Lavender;

            Q.Background = Brushes.Lavender;
            W.Background = Brushes.Lavender;
            E.Background = Brushes.Lavender;
            R.Background = Brushes.Lavender;
            T.Background = Brushes.Lavender;
            Y.Background = Brushes.Lavender;
            U.Background = Brushes.Lavender;
            I.Background = Brushes.Lavender;
            O.Background = Brushes.Lavender;
            P.Background = Brushes.Lavender;
            A.Background = Brushes.Lavender;
            S.Background = Brushes.Lavender;
            D.Background = Brushes.Lavender;
            F.Background = Brushes.Lavender;
            G.Background = Brushes.Lavender;
            H.Background = Brushes.Lavender;
            J.Background = Brushes.Lavender;
            K.Background = Brushes.Lavender;
            L.Background = Brushes.Lavender;
            Z.Background = Brushes.Lavender;
            X.Background = Brushes.Lavender;
            C.Background = Brushes.Lavender;
            V.Background = Brushes.Lavender;
            B.Background = Brushes.Lavender;
            N.Background = Brushes.Lavender;
            M.Background = Brushes.Lavender;




        }

        private void ResetSentencePageText()
        {
            SentenceTextBox.Text = "";
            SentenceUpdate.Text = "";
        }


        //Save Methods

        private void LoadSaveFiles()
        {
            sentenceLogic.categories = saveInitialiser.LoadCategories();
            savedSentencesLogic.SavedSentences = saveInitialiser.LoadSentences();
            optionsLogic.Options = saveInitialiser.LoadOptions();
            sentenceLogic.customCategory = saveInitialiser.LoadCustomCategory();
            sentenceLogic.mostUsedList = saveInitialiser.LoadMostUsedList();
            sentenceLogic.mostUsed = saveInitialiser.LoadMostUsed();

        }

        private void SaveAllFiles()
        {
            saveInitialiser.SaveSentencesToFile(savedSentencesLogic.SavedSentences);
            saveInitialiser.SaveCustomCategory(sentenceLogic.customCategory);
            saveInitialiser.SaveOptions(optionsLogic.Options);
            saveInitialiser.SaveMostUsed(sentenceLogic.mostUsed);
            saveInitialiser.SaveMostUsedList(sentenceLogic.mostUsedList);
        }


        //Choose Category Page Methods

        private void Choose_Category_Button_Click(object sender, RoutedEventArgs e)
        {            
            SaveAllFiles();
            myTabControl.SelectedIndex = 5;
        }

        private void Actions_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(0);
            GoToSentencePage();
        }

        private void Replies_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(1);
            GoToSentencePage();
        }

        private void Food_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(2);
            GoToSentencePage();

        }

        private void Drinks_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(3);
            GoToSentencePage();
        }

        private void Greetings_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(4);
            GoToSentencePage();

        }

        private void Feelings_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(5);

            GoToSentencePage();

        }

        private void Emotions_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(6);

            GoToSentencePage();

        }

        private void Colours_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(7);

            GoToSentencePage();

        }

        private void Animals_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(8);

            GoToSentencePage();

        }

        private void Times_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(9);

            GoToSentencePage();

        }

        private void Carers_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(10);

            GoToSentencePage();

        }

        private void Kitchen_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(11);

            GoToSentencePage();

        }

        private void PersonalCare_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(12);

            GoToSentencePage();

        }

        private void Entertainment_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(13);

            GoToSentencePage();

        }

        private void Family_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(14);

            GoToSentencePage();

        }


        private void Custom_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(15);

            GoToSentencePage();

        }

        private void MostUsed_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(16);

            GoToSentencePage();

        }

        private void ConnectingWords_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            sentenceLogic.ChangeCategory(17);

            GoToSentencePage();
        }


        private void GoToSentencePage()
        {
            
            images = new List<Image> { Image1, Image2, Image3, Image4 };
            buttons = new List<Button> { Image1_Button, Image2_Button, Image3_Button, Image4_Button };
            textBlocks = new List<TextBlock> { Text1, Text2, Text3, Text4 };
            sentenceLogic.UpdateCustomCategory();
            sentenceLogic.UpdateMostUsedCategory();
            
            sentenceLogic.GenerateSentencePageAndGoToFirstPage();
            CreatePage();
            PageNumber.Text = sentenceLogic.GetPageNumber();

            myTabControl.SelectedIndex = 1;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            GoToSentencePage();
        }

        //Keyboard clicks

        private void Q_Button_Click(object sender, RoutedEventArgs e)
        {   
            Word.Text = keyboard.AddLetter(Word.Text, 'q');       
        }

        private void W_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'w');
        }

        private void E_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'e');
        }

        private void R_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'r');
        }

        private void T_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 't');
        }

        private void Y_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'y');
        }

        private void U_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'u');
        }

        private void I_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'i');
        }

        private void O_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'o');
        }

        private void P_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'p');
        }

        private void A_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'a');
        }

        private void S_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 's');
        }

        private void D_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'd');
        }

        private void F_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'f');
        }

        private void G_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'g');
        }

        private void H_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'h');
        }

        private void J_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'j');
        }

        private void K_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'k');
        }

        private void L_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'l');
        }

        private void Z_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'z');
        }

        private void X_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'x');
        }

        private void C_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'c');
        }

        private void V_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'v');
        }

        private void B_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'b');
        }

        private void N_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'n');
        }

        private void M_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, 'm');
        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomName.Text = Word.Text;
            myTabControl.SelectedIndex = 3;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.DeleteLastLetter(Word.Text);
        }

        private void BackToSave_Button_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 3;
        }

        private void Keyboard_Button_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 6;

        }

        private void Space_Button_Click(object sender, RoutedEventArgs e)
        {
            Word.Text = keyboard.AddLetter(Word.Text, ' ');

        }


    }
}
