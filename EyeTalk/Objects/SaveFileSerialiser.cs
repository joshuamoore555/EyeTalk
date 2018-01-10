using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Objects
{
    class SaveFileSerialiser
    {
        static Picture pizza = new Picture("Pizza", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\pizza.png");
        static Picture apple = new Picture("Apple", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\apple.png");
        static Picture banana = new Picture("Banana", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\banana.png");

        static Picture happy = new Picture("Happy", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\happy.png");
        static Picture sad = new Picture("Sad", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\sad.png");
        static Picture angry = new Picture("Angry", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\angry.png");

        static Picture shower = new Picture("Shower", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\shower.png");
        static Picture toilet = new Picture("Toilet", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\toilet.png");
        static Picture washHands = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");

        static Picture I = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture me = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture you = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture him = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture her = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture it = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture they = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture he = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture she = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");
        static Picture we = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");



        static List<Picture> food = new List<Picture>() { pizza, apple, banana, pizza, pizza, pizza, pizza, pizza, pizza, pizza, pizza };
        static List<Picture> emotions = new List<Picture>() { happy, sad, angry, happy, happy, happy, happy, happy, happy, happy, happy, happy };
        static List<Picture> bathroom = new List<Picture>() { shower, toilet, washHands, shower, shower, shower, shower, shower};
        static List<Picture> verbs = new List<Picture>() { shower, toilet, washHands, shower, shower, shower, shower, shower };
        static List<Picture> pronouns = new List<Picture>() { shower, toilet, washHands, shower, shower, shower, shower, shower };


        static List<Picture> custom = new List<Picture>() { };


        static SortedList<String, List<Picture>> categories = new SortedList<string, List<Picture>>(){
            {"Foods", food },
            {"Bathroom", bathroom },
            {"Emotions", emotions },
            {"Verbs", verbs},
            {"Pronouns", pronouns },
            {"Custom", custom }
            };

        static List<string> savedSentences = new List<string>();




        public static string dir = @"C:\Users\User\Desktop\SaveFolder";
        string categoryPath = Path.Combine(dir, "Categories.bin");
        string savedSentencesPath = Path.Combine(dir, "SavedSentences.bin");
        string customPicturesPath = Path.Combine(dir, "CustomPictures.bin");



        public SaveFileSerialiser()
        {
           // CreateCategoryFile();
            //CreateSavedSentencesFile();
            CreateCustomPictureFile();
        }

        //load category from a file

        public void CreateCategoryFile()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, categories);
            }
        }

        public SortedList<String, List<Picture>> LoadCategories()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return (SortedList<String, List<Picture>>)bformatter.Deserialize(stream);
            }
        }

        public void CreateSavedSentencesFile()
        {
            using (Stream stream = File.Open(savedSentencesPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, savedSentences);
            }
        }

        public List<string> LoadSentences()
        {
            using (Stream stream = File.Open(savedSentencesPath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return (List<string>)bformatter.Deserialize(stream);
            }
        }

        public void SaveSentencesToFile(List<string> sentences)
        {
            using (Stream stream = File.Open(savedSentencesPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, sentences);
            }
        }

        public void CreateCustomPictureFile()
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, custom);
            }
        }

        public List<Picture> LoadCustomPictures()
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return (List<Picture>)bformatter.Deserialize(stream);
            }
        }

        public void SaveCustomPictureToFile(List<Picture> custom)
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, custom);
            }
        }
    }
}
