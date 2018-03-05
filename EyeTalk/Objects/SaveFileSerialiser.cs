using System;
using System.Collections.Generic;
using System.IO;


namespace EyeTalk.Objects
{
    public class SaveFileSerialiser
    {
        public static string dir = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        static Picture pizza = new Picture("Pizza", false, "pack://application:,,,/Images/pizza.png");
        static Picture hotdog = new Picture("Hotdog", false, "pack://application:,,,/Images/hotdog.png");
        static Picture apple = new Picture("Apple", false, "pack://application:,,,/Images/apple.png");
        static Picture banana = new Picture("Banana", false, "pack://application:,,,/Images/banana.png");

        static Picture sad = new Picture("Sad", false, "pack://application:,,,/Images/sad.png");
        static Picture happy = new Picture("Happy", false, "pack://application:,,,/Images/happy.png");
        static Picture ecstatic = new Picture("Ecstatic", false, "pack://application:,,,/Images/ecstatic.png");
        static Picture awkward = new Picture("Awkward", false, "pack://application:,,,/Images/awkward.png");
        static Picture angry = new Picture("Angry", false, "pack://application:,,,/Images/angry.png");
        static Picture funny = new Picture("Funny", false, "pack://application:,,,/Images/funny.png");
        static Picture hilarious = new Picture("Hilarious", false, "pack://application:,,,/Images/hilarious.png");
        static Picture love = new Picture("Love", false, "pack://application:,,,/Images/love.png");
        static Picture smug = new Picture("Smug", false, "pack://application:,,,/Images/smug.png");
        static Picture wow = new Picture("Wow", false, "pack://application:,,,/Images/wow.png");





        static List<Picture> food1 = new List<Picture>() { pizza, hotdog, apple, banana };
        static List<Picture> food2 = new List<Picture>() {  };

        static List<Picture> emotions1 = new List<Picture>() { happy, sad, angry, love };
        static List<Picture> emotions2 = new List<Picture>() { ecstatic, awkward, funny, hilarious };

        static List<Picture> custom1 = new List<Picture>() { };


        static List<List<Picture>> foodPages = new List<List<Picture>>() {food1 };
        static List<List<Picture>> emotionPages = new List<List<Picture>>() { emotions1, emotions2,  };
        static List<List<Picture>> customPages = new List<List<Picture>>() { custom1 };



        static SortedList<String, List<List<Picture>>> categories = new SortedList<string, List<List<Picture>>>(){
            {"Custom", customPages },
            {"Foods", foodPages },
            {"Emotions", emotionPages },
            };



        static List<string> savedSentences = new List<string>();

        public static string saveDir = "Saves/";
        public static string picturesDir = "Pictures/";
        string categoryPath = "";
        string savedSentencesPath = "";
        string customPicturesPath = "";




        public SaveFileSerialiser()
        {
            categoryPath = Path.Combine(dir, saveDir, "Categories.bin");
            savedSentencesPath = Path.Combine(dir,saveDir, "SavedSentences.bin");
            customPicturesPath = Path.Combine(dir, saveDir,"CustomPictures.bin");

            CreateInitialFolders();
            CreateCategoryFile();
            CreateSavedSentencesFile();
            CreateCustomPictureFile();
        }

        //load category from a file

        public void CreateInitialFolders()
        {
            Directory.CreateDirectory(saveDir);
            Directory.CreateDirectory(picturesDir);

        }

        public void CreateCategoryFile()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, categories);
            }
        }

        public SortedList<String, List<List<Picture>>> LoadCategories()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return (SortedList<String, List<List<Picture>>>)bformatter.Deserialize(stream);
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

                bformatter.Serialize(stream, customPages);
            }
        }

        public List<List<Picture>> LoadCustomCategory()
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return (List<List<Picture>>)bformatter.Deserialize(stream);
            }
        }

        public void SaveCustomCategory(List<List<Picture>> customPages)
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, customPages);
            }
        }
    }
}
