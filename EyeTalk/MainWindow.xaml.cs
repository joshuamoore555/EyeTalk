using EyeTalk.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;

namespace EyeTalk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int index = 0;
        public static int sentenceIndex = 0;
        public StringBuilder sb = new StringBuilder();

        SaveFileSerialiser initialiser = new SaveFileSerialiser();
        List<Picture> categoryData;
        SortedList<String, List<Picture>> categories;

        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
       
             
        
        public MainWindow()
        {
            InitializeComponent();
            synthesizer.Volume = 100;
            synthesizer.Rate = -2;
        }

        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            categories = initialiser.Load();

            var category = categories.ElementAt(index);

            CreateCategory(category);

            myTabControl.SelectedIndex = 1;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            myTabControl.SelectedIndex = 0;
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

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = categories.Count;
            index++;

            if (index >= size)
            {
                index = 0;
                var category = categories.ElementAt(index);
                CreateCategory(category);

            }
            else
            {
                var category = categories.ElementAt(index);
                CreateCategory(category);

            }
        }

        private void Previous_Button_Click(object sender, RoutedEventArgs e)
        {
            var size = categories.Count;
            index--;
            if ( index < 0){
                index = size-1;
                var category = categories.ElementAt(index);
                CreateCategory(category);

            }
            else
            {
                var category = categories.ElementAt(index);
                CreateCategory(category);

            }
        }

        private void CreateCategory(KeyValuePair<String, List<Picture>> category)
        {
            CategoryTitle.Text = category.Key + " " + sentenceIndex.ToString();
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
        }

        private void ButtonImage1_Click(object sender, RoutedEventArgs e)
        {
            var word = Text1.Text;
            if(sentenceIndex < 3) {
                sb.Append(word);
                sb.Append(" ");
                SentenceTextBox.Text = sb.ToString();
                sentenceIndex++;
            }
            
        }

        private void ButtonImage2_Click(object sender, RoutedEventArgs e)
        {
            var word = Text2.Text;
            if (sentenceIndex < 3)
            {
                sb.Append(word);
                sb.Append(" ");
                SentenceTextBox.Text = sb.ToString();
                sentenceIndex++;
            }
        }

        private void ButtonImage3_Click(object sender, RoutedEventArgs e)
        {
            var word = Text3.Text;
            if (sentenceIndex < 3)
            {
                sb.Append(word);
                sb.Append(" ");
                SentenceTextBox.Text = sb.ToString();
                sentenceIndex++;
            }
        }

        private void PlaySound_Button_Click(object sender, RoutedEventArgs e)
        {
            synthesizer.Speak(sb.ToString());
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
