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
        System.Timers.Timer timer;
        MainWindowLogic mainWindowLogic;
        OptionsLogic optionsLogic;
        AddPictureLogic addPictureLogic;
        SavedSentencesLogic savedSentencesLogic;
        SpeechGenerator speech;

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
              
            BeginTimer();
            UpdateOptions();
        }



        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            LoadSaveFiles();
            mainWindowLogic.ResetSentence();
            SentenceTextBox.Text = "";
            SentenceUpdate.Text = "";
            mainWindowLogic.UpdateCustomCategory();
            mainWindowLogic.UpdateMostUsedCategory();

            images = new List<Image> { Image1, Image2, Image3, Image4 };
            buttons = new List<Button> { Image1_Button, Image2_Button, Image3_Button, Image4_Button };
            textBlocks = new List<TextBlock> { Text1, Text2, Text3, Text4 };

            var categoryPages = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex);
            string categoryName = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex).Key;
            var page = categoryPages.Value.ElementAt(mainWindowLogic.PageIndex);

            CreatePage(page);
            UpdatePageNumber(categoryName);
            myTabControl.SelectedIndex = 1;


        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 2;
        }

        private void Add_PictureCategory_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 3;
        }

        private void Sentences_Button_Click(object sender, RoutedEventArgs e)
        {
            var numberOfSentences = savedSentencesLogic.savedSentences.Count;
            if (numberOfSentences <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else
            {
                currentSentence.Text = savedSentencesLogic.savedSentences.First();
            }
            myTabControl.SelectedIndex = 4;

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowLogic.mostUsed != null)
            {
                saveInitialiser.SaveMostUsed(mainWindowLogic.mostUsed);
            }

            saveInitialiser.SaveOptions(optionsLogic.Options);
            myTabControl.SelectedIndex = 0;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            eyeTracker.eyeTracking = false;
            timer.Stop();
            Application.Current.Dispatcher.BeginInvokeShutdown(System.Windows.Threading.DispatcherPriority.Send);
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = mainWindowLogic.categories.Count;
            mainWindowLogic.CategoryIndex++;
            mainWindowLogic.PageIndex = 0;

            if (mainWindowLogic.CategoryIndex >= size)
            {
                mainWindowLogic.CategoryIndex = 0;
                var categoryPages = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex);
                string categoryName = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex).Key;
                var page = categoryPages.Value.ElementAt(mainWindowLogic.PageIndex);
                CreatePage(page);
                UpdatePageNumber(categoryName);

            }
            else
            {
                var categoryPages = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex);
                string categoryName = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex).Key;
                var page = categoryPages.Value.ElementAt(mainWindowLogic.PageIndex);
                CreatePage(page);
                UpdatePageNumber(categoryName);

            }
        }

        private void Previous_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = mainWindowLogic.categories.Count;
            mainWindowLogic.CategoryIndex--;
            mainWindowLogic.PageIndex = 0;

            if (mainWindowLogic.CategoryIndex < 0)
            {
                mainWindowLogic.CategoryIndex = size - 1;

                var categoryPages = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex);
                string categoryName = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex).Key;
                var page = categoryPages.Value.ElementAt(mainWindowLogic.PageIndex);
                CreatePage(page);
                UpdatePageNumber(categoryName);


            }
            else
            {
                var categoryPages = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex);
                string categoryName = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex).Key;
                var page = categoryPages.Value.ElementAt(mainWindowLogic.PageIndex);
                CreatePage(page);
                UpdatePageNumber(categoryName);

            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SentenceTextBox.Text))
            {
                SentenceUpdate.Text = "Please create a sentence before saving";

            }
            else if (savedSentencesLogic.savedSentences.Count > 0 && savedSentencesLogic.savedSentences.Contains(SentenceTextBox.Text))
            {
                SentenceUpdate.Text = "Sentence has already been saved";
            }
            else
            {
                savedSentencesLogic.savedSentences.Add(SentenceTextBox.Text);
                saveInitialiser.SaveSentencesToFile(savedSentencesLogic.savedSentences);
                SentenceUpdate.Text = "Sentence Saved";
            }

        }

        private async void PlaySound_Button_Click(object sender, RoutedEventArgs e)
        {
            var text = SentenceTextBox.Text.ToString();

            await Task.Run(() => speech.Speak(text));
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

        private async void Play_Saved_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var text = currentSentence.Text.ToString();

            await Task.Run(() => speech.Speak(text));
        }

        private void Next_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var numberOfSentences = savedSentencesLogic.savedSentences.Count;
            mainWindowLogic.SentenceIndex++;
            if (numberOfSentences <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else if (mainWindowLogic.SentenceIndex <= numberOfSentences - 1)
            {
                currentSentence.Text = savedSentencesLogic.savedSentences.ElementAt(mainWindowLogic.SentenceIndex);
            }
            else
            {
                mainWindowLogic.SentenceIndex = 0;
                currentSentence.Text = savedSentencesLogic.savedSentences.ElementAt(mainWindowLogic.SentenceIndex);
            }
        }

        private void Previous_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var numberOfSentences = savedSentencesLogic.savedSentences.Count;
            mainWindowLogic.SentenceIndex--;
            if (numberOfSentences <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else if (mainWindowLogic.SentenceIndex >= 0)
            {
                currentSentence.Text = savedSentencesLogic.savedSentences.ElementAt(mainWindowLogic.SentenceIndex);
            }
            else
            {
                mainWindowLogic.SentenceIndex = numberOfSentences - 1;
                currentSentence.Text = savedSentencesLogic.savedSentences.ElementAt(mainWindowLogic.SentenceIndex);
            }
        }

        private void Delete_Saved_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            if (savedSentencesLogic.savedSentences.Count <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else
            {
                savedSentencesLogic.savedSentences.RemoveAt(mainWindowLogic.SentenceIndex);
                mainWindowLogic.SentenceIndex = 0;

                if (savedSentencesLogic.savedSentences.Count <= 0)
                {
                    currentSentence.Text = "No sentences saved";
                }
                else
                {
                    currentSentence.Text = savedSentencesLogic.savedSentences.ElementAt(mainWindowLogic.SentenceIndex);
                }
            }
            saveInitialiser.SaveSentencesToFile(savedSentencesLogic.savedSentences);
        }

        private void Load_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = addPictureLogic.LoadCustomPicture();
            CustomFilePath.Text = picturePath;
            CustomName.Text = System.IO.Path.GetFileNameWithoutExtension(picturePath);
            var uri = new Uri(picturePath);
            var bitmap = new BitmapImage(uri);
            CustomPicture.Source = bitmap;
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
                    }

                    else if(spaceInCustomCategory == false)
                    {
                        mainWindowLogic.customCategory.Add(addPictureLogic.CreateNewPageAndAddCustomPicture(customPicture));
                        Status.Text = "Created new category. \nNumber of categories is now: " + mainWindowLogic.customCategory.Count;

                    }

                    saveInitialiser.SaveCustomCategory(mainWindowLogic.customCategory);
                }
            }
        }




        private void VoiceType_Click(object sender, RoutedEventArgs e)
        {
            optionsLogic.ChangeVoiceType();

            if (optionsLogic.Options.VoiceTypeSelection == 0)
            {
                VoiceType.Content = "Female";
                speech.SelectFemaleVoice();
                
            }
            else if (optionsLogic.Options.VoiceTypeSelection == 1)
            {
                VoiceType.Content = "Male";
                speech.SelectMaleVoice();

            }
        }

        private void Right_Delay_Click(object sender, RoutedEventArgs e)
        {
            optionsLogic.IncreaseSelectionDelay();
            var seconds = optionsLogic.Options.EyeFixationValue / 4;
            EyeSelectionSpeedStatus.Text = seconds + " Seconds";
        }

        private void Left_Delay_Click(object sender, RoutedEventArgs e)
        {
            optionsLogic.DecreaseSelectionDelay();
            var seconds = optionsLogic.Options.EyeFixationValue / 4;
            EyeSelectionSpeedStatus.Text = seconds + " Seconds";
        }

        private void Right_Speed_Click(object sender, RoutedEventArgs e)
        {
            optionsLogic.IncreaseVoiceSpeed();

            SpeedStatus.Text = optionsLogic.Options.VoiceSpeeds.ElementAt(optionsLogic.Options.VoiceSpeedSelection);

            if (SpeedStatus.Text == "Fast")
            {
                speech.SelectFastVoice();
            }
            else if (SpeedStatus.Text == "Normal")
            {
                speech.SelectNormalVoice();
            }
            else if (SpeedStatus.Text == "Slow")
            {
                speech.SelectSlowVoice();
            }
        }

        private void Left_Speed_Click(object sender, RoutedEventArgs e)
        {
            optionsLogic.DecreaseVoiceSpeed();

            SpeedStatus.Text = optionsLogic.Options.VoiceSpeeds.ElementAt(optionsLogic.Options.VoiceSpeedSelection);
            if (SpeedStatus.Text == "Fast")
            {
                speech.SelectFastVoice();
            }
            else if (SpeedStatus.Text == "Normal")
            {
                speech.SelectNormalVoice();
            }
            else if (SpeedStatus.Text == "Slow")
            {
                speech.SelectSlowVoice();
            }
        }

        private async void TestVoice_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => speech.Speak("Test Voice"));
        }

        private void UpdateOptions()
        {
            VoiceType.Content = optionsLogic.Options.VoiceTypes.ElementAt(optionsLogic.Options.VoiceTypeSelection);
            SpeedStatus.Text = optionsLogic.Options.VoiceSpeeds.ElementAt(optionsLogic.Options.VoiceSpeedSelection);
            var seconds = optionsLogic.Options.EyeFixationValue / 4;
            EyeSelectionSpeedStatus.Text = seconds + " Seconds";
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowLogic.mostUsed != null)
            {
                saveInitialiser.CreateMostUsed();
                mainWindowLogic.mostUsed.Clear();
                Reset.Content = "Most Used Pictures category has been reset";
                mainWindowLogic.categories.Remove("Most Used");
            }
            else
            {
                Reset.Content = "Most Used Pictures category is already empty";
            }

        }



        private void Page_Click(object sender, RoutedEventArgs e)
        {
            var categoryPages = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex);
            string categoryName = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex).Key;
            var numberOfPages = categoryPages.Value.Count;
            mainWindowLogic.PageIndex++;

            if (mainWindowLogic.PageIndex >= numberOfPages)
            {
                mainWindowLogic.PageIndex = 0;
                UpdatePageNumber(categoryName);
                var page = categoryPages.Value.ElementAt(mainWindowLogic.PageIndex);
                CreatePage(page);
            }
            else
            {
                UpdatePageNumber(categoryName);
                var page = categoryPages.Value.ElementAt(mainWindowLogic.PageIndex);
                CreatePage(page);
            }

        }

        private void CreatePage(List<Picture> page)
        {
            mainWindowLogic.Category = page;
            var numberOfPictures = page.Count;

            if (numberOfPictures > 0)
            {
                for (int i = 0; i < numberOfPictures; i++)
                {
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

        private void UpdatePageNumber(string categoryName)
        {
            var categoryPages = mainWindowLogic.categories.ElementAt(mainWindowLogic.CategoryIndex);
            var numberOfPages = categoryPages.Value.Count;
            Page.Content = categoryName + " \nPage " + (mainWindowLogic.PageIndex + 1) + "/" + numberOfPages;
        }

        private void SelectPicture(int i)
        {
            var word = textBlocks.ElementAt(i).Text;
            var selected = mainWindowLogic.Category.ElementAt(i).Selected;
            if(mainWindowLogic.mostUsed == null)
            {
                mainWindowLogic.mostUsed = new SortedDictionary<String, Picture>();
            }
            UpdateMostUsedPicture(i, word);

            if (mainWindowLogic.AmountOfWordsInSentence < 3 && selected == false)
            {
                AddWordToSentence(word, i);
                HighlightPicture(buttons.ElementAt(i));
            }
            else if (selected == true)
            {
                RemoveWordFromSentence(word, i);
                UnhighlightPicture(buttons.ElementAt(i));

            }
        }

        private void UpdateMostUsedPicture(int i, string word)
        {
            mainWindowLogic.Category.ElementAt(i).Count++;
            if (mainWindowLogic.mostUsed.ContainsKey(word))
            {
                mainWindowLogic.mostUsed.Remove(word);
                mainWindowLogic.mostUsed.Add(word, mainWindowLogic.Category.ElementAt(i));
            }
            else
            {
                mainWindowLogic.mostUsed.Add(word, mainWindowLogic.Category.ElementAt(i));
            }
        }

        private void HidePicture(int i)
        {
            buttons.ElementAt(i).Visibility = Visibility.Hidden;
            textBlocks.ElementAt(i).Text = "";
            UnhighlightPicture(buttons.ElementAt(i));
        }

        private void UpdatePicture(int i)
        {
            var filepath = mainWindowLogic.Category.ElementAt(i).FilePath;
            var pictureName = mainWindowLogic.Category.ElementAt(i).Name;
            buttons.ElementAt(i).Visibility = Visibility.Visible;
            images.ElementAt(i).Source = new BitmapImage(new Uri(filepath));
            textBlocks.ElementAt(i).Text = pictureName;

            if (mainWindowLogic.Sentence.Contains(mainWindowLogic.Category.ElementAt(i).Name))
            {
                HighlightPicture(buttons.ElementAt(i));
            }
            else if (mainWindowLogic.Category.ElementAt(i).Selected == false)
            {
                UnhighlightPicture(buttons.ElementAt(i));
            }
            
            else
            {
                HighlightPicture(buttons.ElementAt(i));
            }
        }

        private void HighlightPicture(Button ButtonImage)
        {
            ButtonImage.BorderBrush = new SolidColorBrush(Colors.Yellow);
            ButtonImage.BorderThickness = new Thickness(8, 8, 8, 8);
        }

        private void UnhighlightPicture(Button ButtonImage)
        {
            ButtonImage.BorderBrush = new SolidColorBrush(Colors.Black);
            ButtonImage.BorderThickness = new Thickness(1, 1, 1, 1);

        }

        private void AddWordToSentence(string word, int i)
        {
            if (!mainWindowLogic.Sentence.Contains(mainWindowLogic.Category.ElementAt(i).Name))
            {
                mainWindowLogic.Sentence.Add(word, word);
                StringBuilder sb = new StringBuilder();
                foreach (DictionaryEntry s in mainWindowLogic.Sentence)
                {
                    sb.Append(s.Value + " ");
                }
                SentenceTextBox.Text = sb.ToString();

                mainWindowLogic.AmountOfWordsInSentence++;

                mainWindowLogic.Category.ElementAt(i).Selected = true;
            }

        }

        private void RemoveWordFromSentence(string word, int i)
        {
            mainWindowLogic.Sentence.Remove(word);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry s in mainWindowLogic.Sentence)
            {
                sb.Append(s.Value + " ");
            }
            SentenceTextBox.Text = sb.ToString();

            mainWindowLogic.AmountOfWordsInSentence--;

            mainWindowLogic.Category.ElementAt(i).Selected = false;
        }






        public void BeginTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 125;
            timer.Elapsed += Check;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void Check(object sender, EventArgs e)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                currentPosition = CheckY() + " " + CheckX();

                if (eyeTracker.coordinates.X == 0 && eyeTracker.coordinates.X == 0)
                {
                    currentPosition = "";
                }

                if (currentPosition == previousPosition)
                {
                    optionsLogic.Options.EyeFixationDuration++;

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
                    optionsLogic.Options.EyeFixationDuration = 0;
                }

                previousPosition = currentPosition;

            }));

        }

        private string CheckX()
        {
            if (eyeTracker.coordinates.X < 480)
            {
                return "Left";
            }
            else if (eyeTracker.coordinates.X > 480 && eyeTracker.coordinates.X < 960)
            {
                return "Middle Left";

            }
            else if (eyeTracker.coordinates.X > 960 && eyeTracker.coordinates.X < 1440)
            {
                return "Middle Right";

            }
            else if (eyeTracker.coordinates.X > 1440 && eyeTracker.coordinates.X < 1920)
            {
                return "Right";

            }
            else
            {
                return eyeTracker.coordinates.X.ToString();
            }
        }

        private string CheckY()
        {
            if (eyeTracker.coordinates.Y < 360)
            {
                return "Top";
            }
            else if (eyeTracker.coordinates.Y > 360 && eyeTracker.coordinates.Y < 720)
            {
                return "Middle";
            }
            else if (eyeTracker.coordinates.Y > 720 && eyeTracker.coordinates.Y < 1080)
            {
                return "Bottom";
            }
            else
            {
                return eyeTracker.coordinates.Y.ToString();
            }
        }

        private void CheckHomePage(string position)
        {
            ResetHomePage();

            if (position == "Top Left")
            {
                HoverOverButton(BeginSpeaking);

            }
            else if (position == "Top Middle Left")
            {
                HoverOverButton(BeginSpeaking);

            }
            else if (position == "Top Middle Right")
            {
                HoverOverButton(AddPicture);

            }
            else if (position == "Top Right")
            {
                HoverOverButton(AddPicture);

            }
            else if (position == "Middle Left")
            {

            }
            else if (position == "Middle Middle Left")
            {

            }
            else if (position == "Middle Middle Right")
            {

            }
            else if (position == "Middle Right")
            {

            }
            else if (position == "Bottom Left")
            {
                HoverOverButton(Options);
            }

            else if (position == "Bottom Middle Left")
            {
                HoverOverButton(Options);

            }
            else if (position == "Bottom Middle Right")
            {
                HoverOverButton(Exit);

            }
            else if (position == "Bottom Right")
            {
                HoverOverButton(Exit);
            }
        }

        private void CheckSpeakingPage(string position)
        {
            ResetSentencePage();

            if (position == "Top Left")
            {
                HoverOverButton(SaveSentence);
            }
            else if (position == "Top Middle Left")
            {
                HoverOverButton(Page);
            }
            else if (position == "Top Middle Right")
            {
                HoverOverButton(Page);
            }
            else if (position == "Top Right")
            {
                HoverOverButton(PlaySound);
            }
            else if (position == "Middle Left")
            {
                HoverOverButton(Previous);
            }
            else if (position == "Middle Middle Left")
            {
                HoverOverButton(Image1_Button);
            }
            else if (position == "Middle Middle Right")
            {
                HoverOverButton(Image2_Button);
            }
            else if (position == "Middle Right")
            {
                HoverOverButton(Next);
            }
            else if (position == "Bottom Left")
            {
                HoverOverButton(Home);
            }
            else if (position == "Bottom Middle Left")
            {
                HoverOverButton(Image3_Button);
            }
            else if (position == "Bottom Middle Right")
            {
                HoverOverButton(Image4_Button);
            }
            else if (position == "Bottom Right")
            {
                HoverOverButton(SentenceList);
            }
        }

        private void CheckAddPicturePage(string position)
        {
            ResetAddPicturePage();

            if (position == "Top Left")
            {

            }
            else if (position == "Top Middle Left")
            {
            }
            else if (position == "Top Middle Right")
            {
                CustomName.Background = Brushes.LightBlue;
                CustomName.Focus();
                ResetPosition();


            }
            else if (position == "Top Right")
            {
                CustomName.Focus();
                CustomName.Background = Brushes.LightBlue;
                ResetPosition();
            }
            else if (position == "Middle Left")
            {

            }
            else if (position == "Middle Middle Left")
            {

            }
            else if (position == "Middle Middle Right")
            {

            }
            else if (position == "Middle Right")
            {

            }
            else if (position == "Bottom Left")
            {
                HoverOverButton(BackHome);

            }
            else if (position == "Bottom Middle Left")
            {
            }
            else if (position == "Bottom Middle Right")
            {
                position = "";
                HoverOverButton(Select_Picture);

            }
            else if (position == "Bottom Right")
            {
                HoverOverButton(Save_Custom_Button);

            }
        }

        private void CheckSavedSentencePage(string position)
        {
            ResetSaveSentencePage();

            if (position == "Top Left")
            {

            }
            else if (position == "Top Middle Left")
            {
            }
            else if (position == "Top Middle Right")
            {
            }
            else if (position == "Top Right")
            {
            }
            else if (position == "Middle Left")
            {
                HoverOverButton(PreviousSentence);

            }
            else if (position == "Middle Middle Left")
            {

            }
            else if (position == "Middle Middle Right")
            {

            }
            else if (position == "Middle Right")
            {
                HoverOverButton(NextSentence);

            }
            else if (position == "Bottom Left")
            {
                HoverOverButton(BackToSpeaking);
            }
            else if (position == "Bottom Middle Left")
            {
                HoverOverButton(SpeakSentence);
            }
            else if (position == "Bottom Middle Right")
            {
                HoverOverButton(SpeakSentence);
            }
            else if (position == "Bottom Right")
            {
                HoverOverButton(DeleteSentence);
            }
        }

        private void CheckOptionsPage(string position)
        {
            ResetOptionsPage();

            if (position == "Top Left")
            {
                HoverOverButton(Left_Speed);
            }
            else if (position == "Top Middle Left")
            {
                HoverOverButton(Right_Speed);
            }
            else if (position == "Top Middle Right")
            {
                HoverOverButton(VoiceType);
            }
            else if (position == "Top Right")
            {
                HoverOverButton(VoiceType);
            }
            else if (position == "Middle Left")
            {
                HoverOverButton(Left_Delay);
            }
            else if (position == "Middle Middle Left")
            {
                HoverOverButton(Right_Delay);

            }
            else if (position == "Middle Middle Right")
            {
                HoverOverButton(Reset);

            }
            else if (position == "Middle Right")
            {
                HoverOverButton(Reset);

            }
            else if (position == "Bottom Left")
            {
                HoverOverButton(Back);
            }
            else if (position == "Bottom Middle Left")
            {
            }
            else if (position == "Bottom Middle Right")
            {
                HoverOverButton(TestVoice);

            }
            else if (position == "Bottom Right")
            {
            }
        }

        private void HoverOverButton(Button button)
        {
            button.Background = Brushes.LightBlue;

            if (optionsLogic.Options.EyeFixationDuration > optionsLogic.Options.EyeFixationValue)
            {
                ResetPosition();
                button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void ResetPosition()
        {
            currentPosition = " ";
            previousPosition = "";
        }

        private void ResetHomePage()
        {
            BeginSpeaking.Background = Brushes.Red;
            AddPicture.Background = Brushes.Yellow;
            Options.Background = Brushes.ForestGreen;
            Exit.Background = Brushes.Purple;
        }

        private void ResetSentencePage()
        {
            SaveSentence.Background = Brushes.RoyalBlue;
            Page.Background = Brushes.LightGray;
            SentenceList.Background = Brushes.ForestGreen;
            Previous.Background = Brushes.Yellow;
            Next.Background = Brushes.Yellow;
            Home.Background = Brushes.Purple;
            PlaySound.Background = Brushes.Red;

            Image1_Button.Background = Brushes.Transparent;
            Image2_Button.Background = Brushes.Transparent;
            Image3_Button.Background = Brushes.Transparent;
            Image4_Button.Background = Brushes.Transparent;
        }

        private void ResetAddPicturePage()
        {
            CustomName.Background = Brushes.White;
            Select_Picture.Background = Brushes.Red;
            BackHome.Background = Brushes.Purple;
            Save_Custom_Button.Background = Brushes.Yellow;
        }

        private void ResetSaveSentencePage()
        {
            SpeakSentence.Background = Brushes.Red;
            NextSentence.Background = Brushes.Yellow;
            PreviousSentence.Background = Brushes.Yellow;
            BackToSpeaking.Background = Brushes.Purple;
            DeleteSentence.Background = Brushes.ForestGreen;
        }

        private void ResetOptionsPage()
        {

            Left_Speed.Background = Brushes.Yellow;
            Right_Speed.Background = Brushes.Yellow;
            Left_Delay.Background = Brushes.Red;
            Right_Delay.Background = Brushes.Red;
            Reset.Background = Brushes.RoyalBlue;
            VoiceType.Background = Brushes.ForestGreen;
            Back.Background = Brushes.Purple;
        }

        private void LoadSaveFiles()
        {
            mainWindowLogic.categories = saveInitialiser.LoadCategories();
            savedSentencesLogic.savedSentences = saveInitialiser.LoadSentences();
            mainWindowLogic.customCategory = saveInitialiser.LoadCustomCategory();
            optionsLogic.Options = saveInitialiser.LoadOptions();
        }





       
    }
}
