using EyeTalk.Objects;
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
using EyeTalk.Utils;

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

        private double x;
        private double y;
        private string previousPosition;
        private string currentPosition;
        
        
        List<Picture> categoryData;
        List<string> savedSentences;
        SortedList<String, List<List<Picture>>> categories;
        List<List<Picture>> customCategory;

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
            t.Interval = 1000;
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
            savedSentences.Add(SentenceTextBox.Text);
            initialiser.SaveSentencesToFile(savedSentences);
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

        private void Highlight(Button ButtonImage)
        {
            ButtonImage.BorderBrush = new SolidColorBrush(Colors.Yellow);
            ButtonImage.BorderThickness = new Thickness(3, 3, 3, 3);
        }

        private void Unhighlight(Button ButtonImage)
        {
            ButtonImage.BorderBrush = new SolidColorBrush(Colors.Black);
            ButtonImage.BorderThickness = new Thickness(1, 1, 1, 1);

        }

        private void UpdatePageNumber(string categoryName)
        {
            var categoryPages = categories.ElementAt(categoryNumber);
            var numberOfPages = categoryPages.Value.Count;
            Page.Content = categoryName + " \nPage " + (pageNumber+1) + "/" + numberOfPages;
        }

        private void Page_Click(object sender, RoutedEventArgs e)
        {
            var categoryPages = categories.ElementAt(categoryNumber);
            string categoryName = categories.ElementAt(categoryNumber).Key;
            var numberOfPages = categoryPages.Value.Count;
            pageNumber++;
            
            
            if(pageNumber >= numberOfPages)
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

            if (result == true && customCategory.Count <= 15)
            {
                CustomFilePath.Text = dialog.FileName;
                CustomName.Text = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                var uri = new Uri(dialog.FileName);
                var bitmap = new BitmapImage(uri);
                CustomImage.Source = bitmap;
             }
        }

        private void SaveCustomPicture()
        {
            Picture customPicture = new Picture(CustomName.Text, false, CustomFilePath.Text);
            var numberOfPages = customCategory.Count-1;
            var currentPage = customCategory.ElementAt(numberOfPages);
            

            if(currentPage.Count < 4)
            {
                Boolean pictureAlreadyAdded = false;
                foreach (var picture in currentPage)
                {
                    if (picture.Name == customPicture.Name)
                    {
                        Status.Text = "This image has already been added.";
                        pictureAlreadyAdded = true;
                    }
                }

                if (pictureAlreadyAdded == false)
                {
                    currentPage.Add(customPicture);
                    Status.Text = "Added picture. Number of pictures is now: " + currentPage.Count;
                    initialiser.SaveCustomCategory(customCategory);
                }
                
            }
            else
            { //if greater than 4 images in category, make a new one
                List<Picture> newCustomCategory = new List<Picture>() {  };
                customCategory.Add(newCustomCategory);
                Status.Text = "Created new category. Number of categories is now: " + customCategory.Count;



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


        private async void Fast_Button_Click(object sender, RoutedEventArgs e)
        {
            synth.Rate = 3;
            SpeedStatus.Text = "Fast";
            await Task.Run(() => Speak("This is an example of my voice."));

        }

        private async void Slow_Button_Click(object sender, RoutedEventArgs e)
        {
            synth.Rate = -3;
            SpeedStatus.Text = "Slow";
            await Task.Run(() => Speak("This is an example of my voice."));

        }

        private async void Normal_Button_Click(object sender, RoutedEventArgs e)
        {
            synth.Rate = 0;
            SpeedStatus.Text = "Normal";
            await Task.Run(() => Speak("This is an example of my voice."));
        }

        private async void Male_Button_Click(object sender, RoutedEventArgs e)
        {
            //IReadOnlyCollection<InstalledVoice> InstalledVoices = synth.GetInstalledVoices();
            //InstalledVoice InstalledVoice = InstalledVoices.ElementAt(1);
            //synth.SelectVoice("MSMike");
            GenderStatus.Text = synth.GetInstalledVoices().ElementAt(1).VoiceInfo.Name;
            await Task.Run(() => Speak("This is an example of my voice."));
    

        }

        private async void Female_Button_Click(object sender, RoutedEventArgs e)
        {
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            GenderStatus.Text = synth.GetInstalledVoices().ElementAt(0).VoiceInfo.Name;
            await Task.Run(() => Speak("This is an example of my voice."));
         

        }


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
                    await Task.Delay(1000);
                }
            });
        }

        private void SetXY(double X, double Y)
        {
            x = X;
            y = Y;
        }

        public void Check(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                currentPosition = CheckY() + " " + CheckX();
                ShowPosition.Content = currentPosition;

                if (currentPosition == previousPosition)
                {
                    Interlocked.Increment(ref eyeFixationDuration);

                    if (eyeFixationDuration > 4 && myTabControl.SelectedIndex == 0)
                    {
                        CheckHomePage(currentPosition);
                    }
                    else if (eyeFixationDuration > 4 && myTabControl.SelectedIndex == 1)
                    {
                        SaveSentence.Content = currentPosition;

                    }
                    else if (eyeFixationDuration > 4 && myTabControl.SelectedIndex == 2)
                    {

                    }
                    else if (eyeFixationDuration > 4 && myTabControl.SelectedIndex == 3)
                    {

                    }
                    else if (eyeFixationDuration > 4 && myTabControl.SelectedIndex == 4)
                    {

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

        private void CheckHomePage(string position)
        {
            if (position == "Top Left")
            {
                BeginSpeaking.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            }
            else if (position == "Top Middle Left")
            {
                BeginSpeaking.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (position == "Top Middle Right")
            {
                AddPicture.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (position == "Top Right")
            {
                AddPicture.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
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
                Options.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (position == "Bottom Middle Left")
            {
                Options.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (position == "Bottom Middle Right")
            {
                Exit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (position == "Bottom Right")
            {
                Exit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

    }
}
