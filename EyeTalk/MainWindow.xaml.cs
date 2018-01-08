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

        SaveFileSerialiser initialiser = new SaveFileSerialiser();
        List<Picture> categoryData;
        SortedList<String, List<Picture>> categories;
        List<string> savedSentences;

        SpeechSynthesizer synth = new SpeechSynthesizer();
        OrderedDictionary sentence = new OrderedDictionary();
        
        public MainWindow()
        {
            InitializeComponent();
            synth.Volume = 100;
            synth.Rate = -2;
        }

        /*
         Main Menu
         */

        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            categories = initialiser.LoadCategories();
            savedSentences = initialiser.LoadSentences();

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

        private void CreateCategory(KeyValuePair<String, List<Picture>> category)
        {
            CategoryTitle.Text = category.Key;
            categoryData = category.Value;

            var filepath1 = categoryData.ElementAt(0).FilePath;
            var filepath2 = categoryData.ElementAt(1).FilePath;
            var filepath3 = categoryData.ElementAt(2).FilePath;

            var pictureName1 = categoryData.ElementAt(0).Name;
            var pictureName2 = categoryData.ElementAt(1).Name;
            var pictureName3 = categoryData.ElementAt(2).Name;

            Image1.Source = new BitmapImage(new Uri(filepath1));
            Image2.Source = new BitmapImage(new Uri(filepath2));
            Image3.Source = new BitmapImage(new Uri(filepath3));

            Text1.Text = pictureName1;
            Text2.Text = pictureName2;
            Text3.Text = pictureName3;

            if (categoryData.ElementAt(0).Selected == false)
            {
                Unhighlight(ButtonImage1);
            }
            else
            {
                Highlight(ButtonImage1);
            }

            if (categoryData.ElementAt(1).Selected == false)
            {
                Unhighlight(ButtonImage2);

            }
            else
            {
                Highlight(ButtonImage2);
            }

            if (categoryData.ElementAt(2).Selected == false)
            {
                Unhighlight(ButtonImage3);
            
            }
            else
            {
                Highlight(ButtonImage3);
            }


        }

        private void ButtonImage1_Click(object sender, RoutedEventArgs e)
        {
            var word = Text1.Text;
            int index = 0;
            var selected = categoryData.ElementAt(index).Selected;
           
            if (amountSelected < 3 && selected == false)
            {
                AddWordToSentence(word, index);
                Highlight(ButtonImage1);
            }
            else if(selected == true)
            {
                RemoveWordFromSentence(word, index);
                Unhighlight(ButtonImage1);
            }

        }

        private void ButtonImage2_Click(object sender, RoutedEventArgs e)
        {
            var word = Text2.Text;
            int index = 1;
            var selected = categoryData.ElementAt(index).Selected;

            if (amountSelected < 3 && selected == false)
            {
                AddWordToSentence(word, index);
                Highlight(ButtonImage2);
            }
            else if (selected == true)
            {
                RemoveWordFromSentence(word, index);
                Unhighlight(ButtonImage2);
            }
        }

        private void ButtonImage3_Click(object sender, RoutedEventArgs e)
        {
            var word = Text3.Text;
            int index = 2;
            var selected = categoryData.ElementAt(index).Selected;
          
            if (amountSelected < 3 && selected == false)
            {
                AddWordToSentence(word, index);
                Highlight(ButtonImage3);
            }
            else if (selected == true)
            {
                RemoveWordFromSentence(word, index);
                Unhighlight(ButtonImage3);
                
            }
        }

        private async void PlaySound_Button_Click(object sender, RoutedEventArgs e)
        {
            var text = SentenceTextBox.Text.ToString();

            await Task.Run(() => Speak(text));
        }

        private void AddWordToSentence(string word, int index)
        {
            sentence.Add(word, word);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry s in sentence)
            {
                sb.Append(s.Value + " ");
            }
            SentenceTextBox.Text = sb.ToString();

            amountSelected++;

            categoryData.ElementAt(index).Selected = true;
           
        }

        private void RemoveWordFromSentence(string word, int index)
        {
            sentence.Remove(word);
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry s in sentence)
            {
                sb.Append(s.Value + " ");
            }
            SentenceTextBox.Text = sb.ToString();

            amountSelected--;

            categoryData.ElementAt(index).Selected = false;
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
            ButtonImage.BorderThickness = new Thickness(3, 3, 3, 3);

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            savedSentences.Add(SentenceTextBox.Text);
            initialiser.SaveSentencesToFile(savedSentences);
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
                Filter = "JPG Files (*.jpg)|*.jpeg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            Nullable<bool> result = dialog.ShowDialog();

            List<BitmapImage> list = new List<BitmapImage>();

            if (result == true)
            {
                string filePath = dialog.FileName;
                string name = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                CustomFilePath.Text = name;
                var uri = new Uri(filePath);
                var bitmap = new BitmapImage(uri);
                CustomImage.Source = bitmap;
             }
        }

        private void Add_Custom_Picture_Button_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomPicture();
        }
    }
}
