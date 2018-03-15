using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Speech.Synthesis;
using System.Collections.Specialized;
using System.Collections;
using Tobii.EyeX.Framework;
using EyeXFramework;
using EyeTalk.Objects;

namespace EyeTalk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static int categoryNumber = 0;
        public static int pageNumber = 0;
        
        public static int amountSelected = 0;
        public static int sentencePicked = 0;
        private int eyeFixationDuration = 0;
        private int eyeFixationValue = 3;
        private int voiceSpeedSelection = 0;
        private int voiceTypeSelection = 0;

        private double x;
        private double y;
        private string previousPosition;
        private string currentPosition;
            
        SortedList<String, List<List<Picture>>> categories;
        List<List<Picture>> customCategory;
        List<Picture> categoryData;
        List<String> savedSentences;

        List<String> voiceSpeeds = new List<String>{ "Slow", "Normal", "Fast"};
        List<String> voiceTypes = new List<String> { "Male", "Female" };
        Boolean additionalEyeInformation = false;

        List<Image> images;
        List<Button> buttons;
        List<TextBlock> textBlocks;

        SaveFileSerialiser initialiser = new SaveFileSerialiser();
        SpeechSynthesizer synth = new SpeechSynthesizer();
        OrderedDictionary sentence = new OrderedDictionary();

        public MainWindow()
        {
            InitializeComponent();

            StartEyeTracking();

            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 500;
            t.Elapsed += Check;
            t.AutoReset = true;
            t.Enabled = true;

            synth.Volume = 100;
            synth.Rate = 0;
            categories = initialiser.LoadCategories();
            savedSentences = initialiser.LoadSentences();
            customCategory = initialiser.LoadCustomCategory();
           
        }

        /*
         Main Menu
         */

        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            savedSentences = initialiser.LoadSentences();
            customCategory = initialiser.LoadCustomCategory();

            images = new List<Image> {Image1, Image2, Image3, Image4};
            buttons = new List<Button> {Image1_Button, Image2_Button, Image3_Button, Image4_Button};
            textBlocks = new List<TextBlock> {Text1, Text2, Text3, Text4};

            categories = initialiser.LoadCategories();
            categories.Remove("Custom");
            categories.Add("Custom", customCategory);

            var categoryPages = categories.ElementAt(categoryNumber);
            string categoryName = categories.ElementAt(categoryNumber).Key;
            var page = categoryPages.Value.ElementAt(pageNumber);

            categoryNumber = 0;
            pageNumber = 0;
            amountSelected = 0;
            sentencePicked = 0;
            SentenceTextBox.Text = "";
            sentence.Clear();

            CreatePage(page);
            UpdatePageNumber(categoryName);

            myTabControl.SelectedIndex = 1;
            eyeFixationDuration = 0;

        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 2;
            eyeFixationDuration = 0;

        }

        private void Add_PictureCategory_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 3;
            eyeFixationDuration = 0;

        }

        private void Sentences_Button_Click(object sender, RoutedEventArgs e)
        {
            var numberOfSentences = savedSentences.Count;
            if (numberOfSentences <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else
            {
                currentSentence.Text = savedSentences.First();
            }
            myTabControl.SelectedIndex = 4;
            eyeFixationDuration = 0;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 0;
            eyeFixationDuration = 0;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        /*
        Begin Speaking
        */


        /*
        Clicks
        */
        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = categories.Count;
            categoryNumber++;
            pageNumber = 0;

            if (categoryNumber >= size)
            {
                categoryNumber = 0;
                var categoryPages = categories.ElementAt(categoryNumber);
                string categoryName = categories.ElementAt(categoryNumber).Key;
                var page = categoryPages.Value.ElementAt(pageNumber);
                CreatePage(page);
                UpdatePageNumber(categoryName);

            }
            else
            {
                var categoryPages = categories.ElementAt(categoryNumber);
                string categoryName = categories.ElementAt(categoryNumber).Key;
                var page = categoryPages.Value.ElementAt(pageNumber);
                CreatePage(page);
                UpdatePageNumber(categoryName);

            }
        }

        private void Previous_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = categories.Count;
            categoryNumber--;
            pageNumber = 0;

            if (categoryNumber < 0){
                categoryNumber = size-1;

                var categoryPages = categories.ElementAt(categoryNumber);
                string categoryName = categories.ElementAt(categoryNumber).Key;
                var page = categoryPages.Value.ElementAt(pageNumber);
                CreatePage(page);
                UpdatePageNumber(categoryName);


            }
            else
            {
                var categoryPages = categories.ElementAt(categoryNumber);
                string categoryName = categories.ElementAt(categoryNumber).Key;
                var page = categoryPages.Value.ElementAt(pageNumber);
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
            else if (savedSentences.Count > 0 && savedSentences.Contains(SentenceTextBox.Text))
            {
                SentenceUpdate.Text = "Sentence has already been saved";
            }
            else
            {
                savedSentences.Add(SentenceTextBox.Text);
                initialiser.SaveSentencesToFile(savedSentences);
                SentenceUpdate.Text = "Sentence Saved";
            }
                
        }

        private async void PlaySound_Button_Click(object sender, RoutedEventArgs e)
        {
            var text = SentenceTextBox.Text.ToString();

            await Task.Run(() => Speak(text));
        }

        private void Image1_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            SelectImage(index);
        }

        private void Image2_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            SelectImage(index);
        }

        private void Image3_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 2;
            SelectImage(index);
        }

        private void Image4_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 3;
            SelectImage(index);
        }

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            var categoryPages = categories.ElementAt(categoryNumber);
            string categoryName = categories.ElementAt(categoryNumber).Key;
            var numberOfPages = categoryPages.Value.Count;
            pageNumber++;


            if (pageNumber >= numberOfPages)
            {
                pageNumber = 0;
                UpdatePageNumber(categoryName);
                var page = categoryPages.Value.ElementAt(pageNumber);
                CreatePage(page);
            }
            else
            {
                UpdatePageNumber(categoryName);
                var page = categoryPages.Value.ElementAt(pageNumber);
                CreatePage(page);
            }

        }


        /*
        Image Stuff
        */

        private void CreatePage(List<Picture> page)
        {
            categoryData = page;
            var numberOfPictures = page.Count;

            if (numberOfPictures > 0)
            {
                for (int i = 0; i < numberOfPictures; i++)
                {
                    UpdateImage(i);
                }

                for (int i = numberOfPictures; i < 4; i++)
                {
                    HideImage(i);
                }
            }
            else
            {
                for (int i = 0; i < images.Count; i++)
                {
                    HideImage(i);
                }
            }
        }

        private void UpdatePageNumber(string categoryName)
        {
            var categoryPages = categories.ElementAt(categoryNumber);
            var numberOfPages = categoryPages.Value.Count;
            Page.Content = categoryName + " \nPage " + (pageNumber + 1) + "/" + numberOfPages;
        }

        private void SelectImage(int i)
        {
            var word = textBlocks.ElementAt(i).Text;
            var selected = categoryData.ElementAt(i).Selected;

            if (amountSelected < 3 && selected == false)
            {
                AddWordToSentence(word, i);
                Highlight(buttons.ElementAt(i));
            }
            else if (selected == true)
            {
                RemoveWordFromSentence(word, i);
                Unhighlight(buttons.ElementAt(i));

            }
        }

        private void HideImage(int i)
        {
            buttons.ElementAt(i).Visibility = Visibility.Hidden;
            textBlocks.ElementAt(i).Text = "";
            Unhighlight(buttons.ElementAt(i));
        }

        private void UpdateImage(int i)
        {
            var filepath = categoryData.ElementAt(i).FilePath;
            var pictureName = categoryData.ElementAt(i).Name;
            buttons.ElementAt(i).Visibility = Visibility.Visible;
            images.ElementAt(i).Source = new BitmapImage(new Uri(filepath));
            textBlocks.ElementAt(i).Text = pictureName;

            if (categoryData.ElementAt(i).Selected == false)
            {
                Unhighlight(buttons.ElementAt(i));
            }
            else
            {
                Highlight(buttons.ElementAt(i));
            }
        }

        private void Highlight(Button ButtonImage)
        {
            ButtonImage.BorderBrush = new SolidColorBrush(Colors.Yellow);
            ButtonImage.BorderThickness = new Thickness(6, 6, 6, 6);
        }

        private void Unhighlight(Button ButtonImage)
        {
            ButtonImage.BorderBrush = new SolidColorBrush(Colors.Black);
            ButtonImage.BorderThickness = new Thickness(1, 1, 1, 1);

        }

        /*
        Sentence Stuff
        */

        private void AddWordToSentence(string word, int i)
        {
            sentence.Add(word, word);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry s in sentence)
            {
                sb.Append(s.Value + " ");
            }
            SentenceTextBox.Text = sb.ToString();

            amountSelected++;

            categoryData.ElementAt(i).Selected = true;
           
        }

        private void RemoveWordFromSentence(string word, int i)
        {
            sentence.Remove(word);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry s in sentence)
            {
                sb.Append(s.Value + " ");
            }
            SentenceTextBox.Text = sb.ToString();

            amountSelected--;

            categoryData.ElementAt(i).Selected = false;
        }

        private void Speak(string text)
        {
            synth.Speak(text);
        }



        /*
        Saved Sentences
        */

        private async void Play_Saved_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var text = currentSentence.Text.ToString();

            await Task.Run(() => Speak(text));
        }

        private void Next_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var numberOfSentences = savedSentences.Count;
            sentencePicked++;
            if (numberOfSentences <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else if(sentencePicked <= numberOfSentences-1)
            {
                currentSentence.Text = savedSentences.ElementAt(sentencePicked);
            }
            else
            {
                sentencePicked = 0;
                currentSentence.Text = savedSentences.ElementAt(sentencePicked);
            }
        }

        private void Previous_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            var numberOfSentences = savedSentences.Count;
            sentencePicked--;
            if (numberOfSentences <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else if (sentencePicked >= 0)
            {
                currentSentence.Text = savedSentences.ElementAt(sentencePicked);
            }
            else
            {
                sentencePicked = numberOfSentences-1;
                currentSentence.Text = savedSentences.ElementAt(sentencePicked);
            }
        }

        private void Delete_Saved_Sentence_Button_Click(object sender, RoutedEventArgs e)
        {
            if (savedSentences.Count <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else 
            {
                savedSentences.RemoveAt(sentencePicked);
                sentencePicked = 0;
                
                if (savedSentences.Count <= 0)
                {
                    currentSentence.Text = "No sentences saved";
                }
                else
                {
                    currentSentence.Text = savedSentences.ElementAt(sentencePicked);
                }
            }
            initialiser.SaveSentencesToFile(savedSentences);
        }
    

        /*
        Custom picture
        */
    
        private void LoadCustomPicture()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif"
            };

            Nullable<bool> result = dialog.ShowDialog();

            List<BitmapImage> list = new List<BitmapImage>();

            if (result == true)
            {
                CustomFilePath.Text = dialog.FileName;
                CustomName.Text = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                var uri = new Uri(dialog.FileName);
                var bitmap = new BitmapImage(uri);
                CustomPicture.Source = bitmap;
             }
        }

        private void SaveCustomPicture()
        {
            if (String.IsNullOrEmpty(CustomFilePath.Text) && String.IsNullOrEmpty(CustomName.Text))
            {
                Status.Text = "Please select a picture and name before saving";

            }
            else
            {
                Picture customPicture = new Picture(CustomName.Text, false, CustomFilePath.Text);
                var numberOfPages = customCategory.Count - 1;
                var currentPage = customCategory.ElementAt(numberOfPages);

                Boolean pictureAlreadyAdded = false;

                foreach (var page in customCategory)
                {
                    foreach (var picture in page)
                    {
                        if (picture.Name == customPicture.Name)
                        {
                            Status.Text = "This image has already been added.";
                            pictureAlreadyAdded = true;
                        }
                    }
                }



                if (currentPage.Count < 4)
                {
                    if (pictureAlreadyAdded == false)
                    {
                        currentPage.Add(customPicture);
                        Status.Text = "Added picture. Number of pictures is now: " + currentPage.Count;
                        initialiser.SaveCustomCategory(customCategory);
                    }

                }
                else
                {
                    if (pictureAlreadyAdded == false)
                    {
                        List<Picture> newCustomCategory = new List<Picture>() { };
                        newCustomCategory.Add(customPicture);
                        customCategory.Add(newCustomCategory);
                        Status.Text = "Created new category. Number of categories is now: " + customCategory.Count + "\nNumber of pictures is now: " + currentPage.Count;
                        initialiser.SaveCustomCategory(customCategory);
                    }
                }
            }
        }

        private void Add_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomPicture();
        }

        private void Save_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveCustomPicture();
        }



        /*
        Options
        */

        private void VoiceType_Click(object sender, RoutedEventArgs e)
        {
            voiceTypeSelection++;

            if (voiceTypeSelection > 1)
            {
                voiceTypeSelection = 0;
            }

            SetVoiceType();
        }

        private void ExtraEyeData_Click(object sender, RoutedEventArgs e)
        {
            if (additionalEyeInformation)
            {
                additionalEyeInformation = false;
                 SetExtraEyeData();
            }
            else if (!additionalEyeInformation)
            {
                additionalEyeInformation = true;
                SetExtraEyeData();
            }
        }

        private void Right_Delay_Click(object sender, RoutedEventArgs e)
        {
            if (eyeFixationValue < 31)
            {
                eyeFixationValue++;
            }
           SetSelectionDelay();
        }

        private void Left_Delay_Click(object sender, RoutedEventArgs e)
        {
            if(eyeFixationValue > 1)
            {
                eyeFixationValue--;
                SetSelectionDelay();
            }
        }

        private void Right_Speed_Click(object sender, RoutedEventArgs e)
        {
            voiceSpeedSelection++;
            if(voiceSpeedSelection > 2)
            {
                voiceSpeedSelection = 0;
            }

             SetSpeedOfVoice();

        }

        private void Left_Speed_Click(object sender, RoutedEventArgs e)
        {
            voiceSpeedSelection--;
            if (voiceSpeedSelection <= 0)
            {
                voiceSpeedSelection = 2;
            }
            SpeedStatus.Text = voiceSpeeds.ElementAt(voiceSpeedSelection);

             SetSpeedOfVoice();

        }

        private async void TestVoice_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => Speak("Test Voice"));
        }
        /*
        Setters for Options
        */


        private void SetVoiceType()
        {
            if (voiceTypeSelection == 0)
            {
                VoiceType.Content = "Female";
                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            }
            else if (voiceTypeSelection == 1)
            {
                List<InstalledVoice> voices = new List<InstalledVoice>();

                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {

                    voices.Add(voice);
                }
                VoiceType.Content = "Male";
                synth.SelectVoice(voices.ElementAt(1).VoiceInfo.Name);
            }
        }

        private void SetSelectionDelay()
        {
            var seconds = eyeFixationValue / 2;
            EyeSelectionSpeedStatus.Text = seconds + " Seconds";
        }

        private void SetExtraEyeData()
        {
            if (additionalEyeInformation)
            {
                ExtraEyeData.Content = "Additional Eye Data = On";
            }
            else if (!additionalEyeInformation)
            {
                ExtraEyeData.Content = "Additional Eye Data = Off";
            }
        }

        private void SetSpeedOfVoice()
        {
            SpeedStatus.Text = voiceSpeeds.ElementAt(voiceSpeedSelection);
            if (SpeedStatus.Text == "Fast")
            {
                synth.Rate = 3;

            }
            else if (SpeedStatus.Text == "Normal")
            {
                synth.Rate = 0;
            }
            else if (SpeedStatus.Text == "Slow")
            {
                synth.Rate = -3;

            }
        }




        /*
        Eye Tracking
        */

        public void StartEyeTracking()
        {
            var eyeXHost = new EyeXHost();
            eyeXHost.Start();
            var stream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);

            Task.Run(async () =>
            {
                while (true)
                {
                    stream.Next += (s, t) => SetXY(t.X, t.Y);
                    await Task.Delay(500);
                }
            });
        }

        private void SetXY(double X, double Y)
        {
            x = X;
            y = Y;
        }



        /*
        XY Checks
        */

        public void Check(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                currentPosition = CheckY() + " " + CheckX();

                if(x == 0 && y == 0)
                {
                    currentPosition = "";
                }

                if (currentPosition == previousPosition)
                {
                    Interlocked.Increment(ref eyeFixationDuration);

                    if (myTabControl.SelectedIndex == 0)
                    {
                        CheckHomePage(currentPosition, eyeFixationValue);
                    }
                    else if (myTabControl.SelectedIndex == 1)
                    {
                        CheckSpeakingPage(currentPosition, eyeFixationValue);

                    }
                    else if (myTabControl.SelectedIndex == 2)
                    {
                        CheckOptionsPage(currentPosition, eyeFixationValue);
                    }
                    else if (myTabControl.SelectedIndex == 3)
                    {
                        CheckAddPicturePage(currentPosition, eyeFixationValue);
                    }
                    else if (myTabControl.SelectedIndex == 4)
                    {
                        CheckSavedSentencePage(currentPosition, eyeFixationValue);
                    }
                }
                else
                {
                    eyeFixationDuration = 0;
                }

                previousPosition = currentPosition;

            }));

        }

        private string CheckX()
        {
            if (x < 480)
            {
                return "Left";
            }
            else if (x > 480 && x < 960)
            {
                return "Middle Left";

            }
            else if (x > 960 && x < 1440)
            {
                return "Middle Right";

            }
            else if (x > 1440 && x < 1920)
            {
                return "Right";

            }
            else
            {
                return x.ToString();
            }
        }

        private string CheckY()
        {
            if (y < 360)
            {
                return "Top";
            }
            else if (y > 360 && y < 720)
            {
                return "Middle";
            }
            else if (y > 720 && y < 1080)
            {
                return "Bottom";
            }
            else
            {
                return y.ToString();
            }
        }

        private void CheckHomePage(string position, int eyeFixationValue)
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

        private void CheckSpeakingPage(string position, int eyeFixationValue)
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

        private void CheckAddPicturePage(string position, int eyeFixationValue)
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
                //CustomName.Select(0, CustomName.Text.Length);
                ResetPosition();


            }
            else if (position == "Top Right")
            {
                CustomName.Focus();
                CustomName.Background = Brushes.LightBlue;


                //CustomName.Select(0, CustomName.Text.Length);
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
                HoverOverButton(SelectPicture);

            }
            else if (position == "Bottom Right")
            {
                HoverOverButton(Save_Custom_Button);

            }
        }

        private void CheckSavedSentencePage(string position, int eyeFixationValue)
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

        private void CheckOptionsPage(string position, int eyeFixationValue)
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
                HoverOverButton(ExtraEyeData);

            }
            else if (position == "Middle Right")
            {
                HoverOverButton(ExtraEyeData);

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
            }
            else if (position == "Bottom Right")
            {
            }
        }



        /*
        Button Selection 
        */

        private void HoverOverButton(Button button)
        {
            button.Background = Brushes.LightBlue;

            if (eyeFixationDuration > eyeFixationValue)
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
            SentenceUpdate.Text = "";
        }

        private void ResetAddPicturePage()
        {
            CustomName.Background = Brushes.White;
            SelectPicture.Background = Brushes.Red;
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
            SetSpeedOfVoice();
            SetVoiceType();
            SetSelectionDelay();
            SetExtraEyeData();

            Left_Speed.Background = Brushes.Yellow;
            Right_Speed.Background = Brushes.Yellow;
            Left_Delay.Background = Brushes.Red;
            Right_Delay.Background = Brushes.Red;
            ExtraEyeData.Background = Brushes.RoyalBlue;
            VoiceType.Background = Brushes.ForestGreen;
            Back.Background = Brushes.Purple;
        }


    }
}
