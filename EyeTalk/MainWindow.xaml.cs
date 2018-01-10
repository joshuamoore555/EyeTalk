using EyeTalk.Objects;
using System;
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

namespace EyeTalk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static int pageNumber = 0;
        public static int amountSelected = 0;
        public static int sentencePicked = 0;
        
        List<Picture> categoryData;
        List<string> savedSentences;
        SortedList<String, List<Picture>> categories;
        List<Picture> customPictures;

        List<Image> images;
        List<Button> buttons;
        List<TextBlock> textBlocks;

        SaveFileSerialiser initialiser = new SaveFileSerialiser();
        SpeechSynthesizer synth = new SpeechSynthesizer();
        OrderedDictionary sentence = new OrderedDictionary();
        
        public MainWindow()
        {
            InitializeComponent();
            synth.Volume = 100;
            synth.Rate = 0;
            categories = initialiser.LoadCategories();
            savedSentences = initialiser.LoadSentences();
            customPictures = initialiser.LoadCustomPictures();
        }

        /*
         Main Menu
         */

        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            categories = initialiser.LoadCategories();
            savedSentences = initialiser.LoadSentences();
            customPictures = initialiser.LoadCustomPictures();

            categories.Remove("Custom");
            categories.Add("Custom", customPictures);
            

            images = new List<Image> {Image1, Image2, Image3, Image4 , Image5, Image6, Image7, Image8, Image9, Image10, Image11, Image12, Image13, Image14, Image15 };
            buttons = new List<Button> {Image1_Button, Image2_Button, Image3_Button, Image4_Button ,Image5_Button , Image6_Button , Image7_Button , Image8_Button,Image9_Button , Image10_Button,Image11_Button,Image12_Button,Image13_Button,Image14_Button,Image15_Button };
            textBlocks = new List<TextBlock> {Text1, Text2, Text3, Text4, Text5, Text6, Text7, Text8, Text9, Text10, Text11, Text12, Text13, Text14, Text15};
       
            var category = categories.ElementAt(pageNumber);

            pageNumber = 0;
            amountSelected = 0;
            sentencePicked = 0;
            SentenceTextBox.Text = "";
            sentence.Clear();

            CreateCategory(category);

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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /*
        Begin Speaking
        */

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 0;
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = categories.Count;
            pageNumber++;

            if (pageNumber >= size)
            {
                pageNumber = 0;
                var category = categories.ElementAt(pageNumber);
                CreateCategory(category);

            }
            else
            {
                var category = categories.ElementAt(pageNumber);
                CreateCategory(category);

            }
        }

        private void Previous_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = categories.Count;
            pageNumber--;
            if (pageNumber < 0){
                pageNumber = size-1;
                var category = categories.ElementAt(pageNumber);
                CreateCategory(category);

            }
            else
            {
                var category = categories.ElementAt(pageNumber);
                CreateCategory(category);

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

        private void Image5_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 4;
            SelectImage(index);

        }

        private void Image6_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 5;
            SelectImage(index);

        }

        private void Image7_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 6;
            SelectImage(index);

        }

        private void Image8_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 7;
            SelectImage(index);

        }

        private void Image9_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 8;
            SelectImage(index);

        }

        private void Image10_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 9;
            SelectImage(index);

        }

        private void Image11_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 10;
            SelectImage(index);

        }

        private void Image12_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 11;
            SelectImage(index);

        }

        private void Image13_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 12;
            SelectImage(index);

        }

        private void Image14_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 13;
            SelectImage(index);

        }

        private void Image15_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 14;
            SelectImage(index);


        }

        private void CreateCategory(KeyValuePair<String, List<Picture>> category)
        {
            CategoryTitle.Text = category.Key;
            categoryData = category.Value;
            var numberOfPictures = category.Value.Count;
            var emptyPictures = numberOfPictures - 15;
            if (numberOfPictures > 0)
            {
                for (int i = 0; i < numberOfPictures; i++)
                {
                    UpdateImage(i);
                }

                for (int i = numberOfPictures; i < 15; i++)
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


        /*
        Saved Sentences
        */

        private void Sentences_Button_Click(object sender, RoutedEventArgs e)
        {
            var numberOfSentences = savedSentences.Count;
            if(numberOfSentences <= 0)
            {
                currentSentence.Text = "No sentences saved";
            }
            else
            {
                currentSentence.Text = savedSentences.First();
            }
            myTabControl.SelectedIndex = 4;
        }

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

            if (result == true && customPictures.Count <= 15)
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
            if (customPictures.Count < 15)
            {
                customPictures.Add(customPicture);
                Status.Text = "Added picture";
                initialiser.SaveCustomPictureToFile(customPictures);
            }
            else if (customPictures.Contains(customPicture))
            {
                Status.Text = "This image has already been added.";
            }
            else
            {
                Status.Text = "Cannot add more than 15 pictures";
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
            IReadOnlyCollection<InstalledVoice> InstalledVoices = synth.GetInstalledVoices();
            InstalledVoice InstalledVoice = InstalledVoices.ElementAt(1);
            synth.SelectVoice(InstalledVoice.VoiceInfo.Name);
            GenderStatus.Text = "Male";
            await Task.Run(() => Speak("This is an example of my voice."));
    

        }

        private async void Female_Button_Click(object sender, RoutedEventArgs e)
        {
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            GenderStatus.Text = "Female";
            await Task.Run(() => Speak("This is an example of my voice."));
         

        }

       
    }
}
