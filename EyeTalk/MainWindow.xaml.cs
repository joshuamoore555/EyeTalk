using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections;
using EyeTalk.Objects;
using EyeTalk.Logic;
using EyeTalk.Utilities;
using EyeTalk.Constants;

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

        SaveFileSerialiser saveInitialiser;
        EyeTracker eyeTracker;
        SpeechGenerator speech;
        CoordinateTimer timer;
    
        MainWindowLogic mainWindowLogic;
        OptionsLogic optionsLogic;
        AddPictureLogic addPictureLogic;
        SavedSentencesLogic savedSentencesLogic;
        

        public MainWindow()
        {
            InitializeComponent();

            mainWindowLogic = new MainWindowLogic();
            optionsLogic = new OptionsLogic();
            savedSentencesLogic = new SavedSentencesLogic();
            addPictureLogic = new AddPictureLogic();

            speech = new SpeechGenerator();
            eyeTracker = new EyeTracker();
            saveInitialiser = new SaveFileSerialiser();
            timer = new CoordinateTimer();

            timer.coordinateTimer.Elapsed += CheckCoordinates;
            LoadSaveFiles();
            UpdateOptions();
        }

        //Home Page Buttons

        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            LoadSaveFiles();

            images = new List<Image> { Image1, Image2, Image3, Image4 };
            buttons = new List<Button> { Image1_Button, Image2_Button, Image3_Button, Image4_Button };
            textBlocks = new List<TextBlock> { Text1, Text2, Text3, Text4 };

            FormatTextBoxes();

            mainWindowLogic.Begin();
            CreatePage();
            PageNumber.Text = mainWindowLogic.UpdatePageNumber();

            myTabControl.SelectedIndex = 1;

        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            UpdateOptions();
            
            myTabControl.SelectedIndex = 2;
        }

        private void Add_PictureCategory_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 3;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            eyeTracker.eyeTracking = false;
            timer.coordinateTimer.Stop();
            Application.Current.Dispatcher.BeginInvokeShutdown(System.Windows.Threading.DispatcherPriority.Send);
        }



        //Begin Speaking Buttons

        private void Sentences_Button_Click(object sender, RoutedEventArgs e)
        {
            currentSentence.Text = savedSentencesLogic.RetrieveFirstSavedSentenceIfExists();
            myTabControl.SelectedIndex = 4;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveAllFiles();
            myTabControl.SelectedIndex = 0;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SentenceUpdate.Text = savedSentencesLogic.SaveSentenceIfNotPreviouslySaved(SentenceTextBox.Text);
        }

        private async void PlaySound_Button_Click(object sender, RoutedEventArgs e)
        {
            var sentence = SentenceTextBox.Text;
            await Task.Run(() => speech.Speak(sentence));
        }

        private void Image1_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            SelectPicture(index);
        }

        private void Image2_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            SelectPicture(index);
        }

        private void Image3_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 2;
            SelectPicture(index);
        }

        private void Image4_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 3;
            SelectPicture(index);
        }

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            mainWindowLogic.GoToNextPage();
            CreatePage();
            
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindowLogic.CheckIfBackToFirstCategory();
            mainWindowLogic.UpdateCategoryAndGoToFirstPage();
            CreatePage();
            

        }

        private void Previous_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindowLogic.CheckIfBackToLastCategory();
            mainWindowLogic.UpdateCategoryAndGoToFirstPage();
            CreatePage();
            
        }


        //Begin Speaking Methods

        private void CreatePage()
        {
            var numberOfPictures = mainWindowLogic.CategoryPage.Count;
            SentenceUpdate.Text = " ";
            PageNumber.Text = mainWindowLogic.UpdatePageNumber();
            CategoryName.Text = mainWindowLogic.GetCategoryName();
            NextCategoryText.Text = mainWindowLogic.GetNextCategoryName();
            PreviousCategoryText.Text = mainWindowLogic.GetPreviousCategoryName();


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
            textBlocks.ElementAt(i).Text = mainWindowLogic.CategoryPage.ElementAt(i).Name;
            buttons.ElementAt(i).Visibility = Visibility.Visible;
            var filepath = mainWindowLogic.CategoryPage.ElementAt(i).FilePath;
            images.ElementAt(i).Source = mainWindowLogic.GenerateBitmap(filepath);
        }

        private void UpdatePicture(int i)
        {
            var name = mainWindowLogic.CategoryPage.ElementAt(i).Name;

            if (!SentenceTextBox.Text.Contains(name))
            {
                UnhighlightPicture(buttons, i);
            }
            else if(SentenceTextBox.Text.Contains(name))
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
            var selected = mainWindowLogic.CategoryPage.ElementAt(i).Selected;
            var name = mainWindowLogic.CategoryPage.ElementAt(i).Name;

            if (SentenceTextBox.Text.Contains(name) && selected == false)
            {
                SentenceUpdate.Text = "This word has already been added.";
            }
            else
            {
                if (mainWindowLogic.AmountOfWordsInSentence < 6 && selected == false)
                {
                    SentenceTextBox.Text = mainWindowLogic.AddWordToSentence(word, i);
                    mainWindowLogic.UpdateMostUsedPicture(i, word);
                    HighlightPicture(buttons, i);
                }
                else if (selected == true)
                {
                    SentenceTextBox.Text = mainWindowLogic.RemoveWordFromSentence(word, i);
                    UnhighlightPicture(buttons, i);

                }
            }
        
    }



        //Saved Sentences Buttons

        private async void Play_Saved_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var sentence = currentSentence.Text;
            await Task.Run(() => speech.Speak(sentence));
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



        //Options Buttons

        private void ResetMostUsed_Click(object sender, RoutedEventArgs e)
        {
           ResetMostUsedText.Text = mainWindowLogic.ResetMostUsedIfNotEmpty();
        }

        private void ResetCustomPicture_Click(object sender, RoutedEventArgs e)
        {
            ResetCustomText.Text = mainWindowLogic.ResetCustomPictureCategoryIfNotEmpty();
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
            await Task.Run(() => speech.Speak("Test Voice"));
        }

        //Options Methods

        private void UpdateOptions()
        {
            VoiceType.Content = "Voice Type: " + optionsLogic.VoiceTypes.ElementAt(optionsLogic.Options.VoiceTypeSelection);
            SpeedStatus.Text = "Voice Speed: " + optionsLogic.VoiceSpeeds.ElementAt(optionsLogic.Options.VoiceSpeedSelection);
            EyeSelectionSpeedStatus.Text = "Eye Fixation Value: " + optionsLogic.Options.EyeFixationValue / 4 + " Seconds";
        }



        //Add Custom Picture Buttons

        private void Load_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = addPictureLogic.LoadCustomPicture();
            CustomFilePath.Text = picturePath;
            CustomName.Text = System.IO.Path.GetFileNameWithoutExtension(picturePath);
            if (picturePath != null && picturePath != "")
            {
                var uri = new Uri(picturePath);
                var bitmap = new BitmapImage(uri);
                      
                CustomPicture.Source = bitmap;
            }
        }

        private void Save_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CustomFilePath.Text) && String.IsNullOrEmpty(CustomName.Text))
            {
                Status.Text = "Please select a picture and name before saving";

            }
            else
            {
                Picture customPicture = new Picture(CustomName.Text, false, CustomFilePath.Text, 0);
                var currentPage = mainWindowLogic.customCategory.ElementAt(mainWindowLogic.customCategory.Count - 1);
                var pictureAlreadyAdded = addPictureLogic.CheckCustomPictureIsNotDuplicate(customPicture, mainWindowLogic.customCategory);

                if (pictureAlreadyAdded)
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
                        saveInitialiser.SaveCustomCategory(mainWindowLogic.customCategory);

                    }

                    else if (spaceInCustomCategory == false)
                    {
                        mainWindowLogic.customCategory.Add(addPictureLogic.CreateNewPageAndAddCustomPicture(customPicture));
                        Status.Text = "Created new category. \nNumber of categories is now: " + mainWindowLogic.customCategory.Count;
                        saveInitialiser.SaveCustomCategory(mainWindowLogic.customCategory);

                    }

                }
            }
        }


        //Coordinate Checking Methods

        public void CheckCoordinates(object sender, EventArgs e)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                if(myTabControl.SelectedIndex == 1)
                {
                    currentPosition = eyeTracker.GetCurrentPositionBeginSpeaking();
                }
                else
                {
                    currentPosition = eyeTracker.GetCurrentPosition();
                }

                if (eyeTracker.coordinates.X == 0 && eyeTracker.coordinates.X == 0)
                {
                    currentPosition = "";
                }

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
                }
                else
                {
                    optionsLogic.ResetEyeFixationDuration();
                }

                previousPosition = currentPosition;

            }));

        }

        private void CheckHomePage(string position)
        {
            ResetHomePage();
            switch (position) {

                case Positions.TopLeft:
                    HoverOverButton(BeginSpeaking);
                    break;

                case Positions.TopMiddleLeft:
                    HoverOverButton(BeginSpeaking);
                    break;

                case Positions.TopMiddleRight:
                    HoverOverButton(AddPicture);
                    break;

                case Positions.TopRight:
                    HoverOverButton(AddPicture);
                    break;

                case Positions.MiddleLeft:
                    break;

                case Positions.MiddleMiddleLeft:
                    break;

                case Positions.MiddleMiddleRight:
                    break;

                case Positions.MiddleRight:
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(Options);
                    break;

                case Positions.BottomMiddleLeft:
                    HoverOverButton(Options);
                    break;

                case Positions.BottomMiddleRight:
                    HoverOverButton(Exit);
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
                    HoverOverButton(Page);
                    break;

                case Positions.TopLeft:
                    HoverOverButton(PlaySound);
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

                case Positions.MiddleMiddleLeft:
                    break;

                case Positions.MiddleMiddleRight:
                    break;

                case Positions.MiddleRight:
                    HoverOverButton(Next);
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(Home);
                    break;

                case Positions.BottomMiddleLeft:
                    HoverOverButton(Image3_Button);
                    break;

                case Positions.BottomMiddleRight:
                    HoverOverButton(Image4_Button);
                    break;

                case Positions.BottomRight:
                    HoverOverButton(SaveSentence);
                    break;

                case Positions.BottomRightAlternate:
                    HoverOverButton(SentenceList);
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
                    break;

                case Positions.TopMiddleLeft:
                    break;

                case Positions.TopMiddleRight:
                    CustomName.Background = Brushes.LightBlue;
                    CustomName.Focus();
                    ResetPosition();
                    break;

                case Positions.TopRight:
                    CustomName.Focus();
                    CustomName.Background = Brushes.LightBlue;
                    ResetPosition();
                    break;

                case Positions.MiddleLeft:
                    break;

                case Positions.MiddleMiddleLeft:
                    break;

                case Positions.MiddleMiddleRight:
                    break;

                case Positions.MiddleRight:
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(BackHome);
                    break;

                case Positions.BottomMiddleLeft:
                    break;

                case Positions.BottomMiddleRight:
                    position = "";
                    HoverOverButton(Select_Picture);
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
                    HoverOverButton(ResetCustomPictures);
                    break;

                case Positions.BottomLeft:
                    HoverOverButton(Back);
                    break;

                case Positions.BottomMiddleLeft:
                    HoverOverButton(VoiceType);
                    break;

                case Positions.BottomMiddleRight:
                    HoverOverButton(VoiceType);

                    break;

                case Positions.BottomRight:
                    HoverOverButton(TestVoice);
                    break;

                case "":
                    break;

            }
        }

        private void HoverOverButton(Button button)
        {
            button.Background = Brushes.LightBlue;

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
            BeginSpeaking.Background = Brushes.Red;
            AddPicture.Background = Brushes.Yellow;
            Options.Background = Brushes.GreenYellow;
            Exit.Background = Brushes.BlueViolet;
        }

        private void ResetSentencePage()
        {
            SaveSentence.Background = Brushes.LightSeaGreen;
            Page.Background = Brushes.DeepPink;
            SentenceList.Background = Brushes.GreenYellow;
            Previous.Background = Brushes.Yellow;
            Next.Background = Brushes.Yellow;
            Home.Background = Brushes.BlueViolet;
            PlaySound.Background = Brushes.Red;
            RemoveAll.Background = Brushes.RoyalBlue;

            Image1_Button.Background = Brushes.Transparent;
            Image2_Button.Background = Brushes.Transparent;
            Image3_Button.Background = Brushes.Transparent;
            Image4_Button.Background = Brushes.Transparent;
        }

        private void ResetAddPicturePage()
        {
            CustomName.Background = Brushes.White;
            Select_Picture.Background = Brushes.Red;
            BackHome.Background = Brushes.BlueViolet;
            Save_Custom_Button.Background = Brushes.Yellow;
        }

        private void ResetSaveSentencePage()
        {
            SpeakSentence.Background = Brushes.Red;
            NextSentence.Background = Brushes.Yellow;
            PreviousSentence.Background = Brushes.Yellow;
            BackToSpeaking.Background = Brushes.Purple;
            DeleteSentence.Background = Brushes.YellowGreen;
        }

        private void ResetOptionsPage()
        {

            Left_Speed.Background = Brushes.Yellow;
            Right_Speed.Background = Brushes.Yellow;
            Left_Delay.Background = Brushes.Red;
            Right_Delay.Background = Brushes.Red;
            ResetMostUsed.Background = Brushes.RoyalBlue;
            ResetCustomPictures.Background = Brushes.Magenta;
            TestVoice.Background = Brushes.Crimson;
            VoiceType.Background = Brushes.GreenYellow;
            Back.Background = Brushes.BlueViolet;
        }

        private void FormatTextBoxes()
        {
            SentenceTextBox.Text = "";
            SentenceUpdate.Text = "";
        }


        //Save Methods

        private void LoadSaveFiles()
        {
            mainWindowLogic.categories = saveInitialiser.LoadCategories();
            savedSentencesLogic.savedSentences = saveInitialiser.LoadSentences();
            optionsLogic.Options = saveInitialiser.LoadOptions();
            mainWindowLogic.customCategory = saveInitialiser.LoadCustomCategory();
        }

        private void SaveAllFiles()
        {
            saveInitialiser.SaveSentencesToFile(savedSentencesLogic.savedSentences);
            saveInitialiser.SaveCustomCategory(mainWindowLogic.customCategory);
            saveInitialiser.SaveOptions(optionsLogic.Options);
        }

        private void Remove_All_Click(object sender, RoutedEventArgs e)
        {
            SentenceTextBox.Clear();
            mainWindowLogic.RemoveAllWordsFromSentence();
            CreatePage();

        }

        private void Choose_Category_Button_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 5;
        }
    }
}
