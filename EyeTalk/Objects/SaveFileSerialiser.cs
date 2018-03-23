using System;
using System.Collections.Generic;
using System.IO;


namespace EyeTalk.Objects
{
    public class SaveFileSerialiser
    {
        public static string dir = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        //pictures
        static Picture pizza = new Picture("Pizza", false, "pack://application:,,,/Images/pizza.png",0);
        static Picture hotdog = new Picture("Hotdog", false, "pack://application:,,,/Images/hotdog.png", 0);
        static Picture apple = new Picture("Apple", false, "pack://application:,,,/Images/apple.png", 0);
        static Picture banana = new Picture("Banana", false, "pack://application:,,,/Images/banana.png", 0);
        static Picture sad = new Picture("Sad", false, "pack://application:,,,/Images/sad.png", 0);
        static Picture happy = new Picture("Happy", false, "pack://application:,,,/Images/happy.png", 0);
        static Picture ecstatic = new Picture("Ecstatic", false, "pack://application:,,,/Images/ecstatic.png", 0);
        static Picture awkward = new Picture("Awkward", false, "pack://application:,,,/Images/awkward.png", 0);
        static Picture angry = new Picture("Angry", false, "pack://application:,,,/Images/angry.png", 0);
        static Picture funny = new Picture("Funny", false, "pack://application:,,,/Images/funny.png", 0);
        static Picture hilarious = new Picture("Hilarious", false, "pack://application:,,,/Images/hilarious.png", 0);
        static Picture love = new Picture("Love", false, "pack://application:,,,/Images/love.png", 0);
        static Picture smug = new Picture("Smug", false, "pack://application:,,,/Images/smug.png", 0);
        static Picture wow = new Picture("Wow", false, "pack://application:,,,/Images/wow.png", 0);
        static Picture washhands = new Picture("Wash Hands", false, "pack://application:,,,/Images/washhands.png", 0);
        static Picture toilet = new Picture("Toilet", false, "pack://application:,,,/Images/toilet.png", 0);
        static Picture shower = new Picture("Shower", false, "pack://application:,,,/Images/shower.png", 0);
        static Picture iwant = new Picture("I want", false, "pack://application:,,,/Images/want.png", 0);
        static Picture idontknow = new Picture("I don't know", false, "pack://application:,,,/Images/idontknow.png", 0);

        //page
        static List<Picture> food1 = new List<Picture>() { pizza, hotdog, apple, banana };
        static List<Picture> food2 = new List<Picture>() {  };
        static List<Picture> emotions1 = new List<Picture>() { happy, sad, angry, love };
        static List<Picture> emotions2 = new List<Picture>() { ecstatic, awkward, funny, hilarious };
        static List<Picture> emotions3 = new List<Picture>() { wow, smug };
        static List<Picture> bathroom1 = new List<Picture>() { washhands, toilet, shower };
        static List<Picture> verb1 = new List<Picture>() { iwant };
        static List<Picture> reply1 = new List<Picture>() { idontknow };
        static List<Picture> custom1 = new List<Picture>() { };
        static List<Picture> mostused = new List<Picture>() { };

        //pages
        static List<List<Picture>> foodPages = new List<List<Picture>>() {food1 };
        static List<List<Picture>> emotionPages = new List<List<Picture>>() { emotions1, emotions2, emotions3 };
        static List<List<Picture>> bathroomPages = new List<List<Picture>>() { bathroom1 };
        static List<List<Picture>> verbPages = new List<List<Picture>>() { verb1 };
        static List<List<Picture>> replyPages = new List<List<Picture>>() { reply1 };
        static List<List<Picture>> customPages = new List<List<Picture>>() { custom1 };
        static List<List<Picture>> mostUsedPages = new List<List<Picture>>() { mostused };


        static SortedList<String, List<List<Picture>>> categories = new SortedList<string, List<List<Picture>>>(){
            {"Most Used", mostUsedPages },
            {"Foods", foodPages },
            {"Emotions", emotionPages },
            {"Bathroom", bathroomPages },
            {"Verbs", verbPages },
            {"Replies", replyPages },
            {"Custom", customPages },

            };

        static List<string> savedSentences = new List<string>();

        SortedDictionary<String, Picture> mostUsed = new SortedDictionary<String, Picture>();

        Options options = new Options(0, 6, 0, 0, false);

        public static string saveDir = "Saves/";
        public static string picturesDir = "Pictures/";
        string categoryPath = "";
        string savedSentencesPath = "";
        string customPicturesPath = "";
        string optionsPath = "";
        string mostUsedPath = "";

        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();


        public SaveFileSerialiser()
        {
            categoryPath = Path.Combine(dir, saveDir, "Categories.bin");
            savedSentencesPath = Path.Combine(dir,saveDir, "SavedSentences.bin");
            customPicturesPath = Path.Combine(dir, saveDir,"CustomPictures.bin");
            optionsPath = Path.Combine(dir, saveDir, "Options.bin");
            mostUsedPath = Path.Combine(dir, saveDir, "MostUsed.bin");


            //CreateInitialFolders();
            //CreateCategoryFile();
            //CreateSavedSentencesFile();
            //CreateCustomPictureFile();
            //CreateOptions();
            //CreateMostUsed();

        }

        //load category from a file

        public void CreateInitialFolders()
        {
            Directory.CreateDirectory(saveDir);
        }

        public void CreateCategoryFile()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Create))
            {

                bformatter.Serialize(stream, categories);
            }
        }

        public SortedList<String, List<List<Picture>>> LoadCategories()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Open))
            {
                return (SortedList<String, List<List<Picture>>>)bformatter.Deserialize(stream);
            }
        }

        public void CreateSavedSentencesFile()
        {
            using (Stream stream = File.Open(savedSentencesPath, FileMode.Create))
            {
                bformatter.Serialize(stream, savedSentences);
            }
        }

        public List<string> LoadSentences()
        {
            using (Stream stream = File.Open(savedSentencesPath, FileMode.Open))
            {
                return (List<string>)bformatter.Deserialize(stream);
            }
        }

        public void SaveSentencesToFile(List<string> sentences)
        {
            using (Stream stream = File.Open(savedSentencesPath, FileMode.Create))
            {
                bformatter.Serialize(stream, sentences);
            }
        }

        public void CreateCustomPictureFile()
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Create))
            {
                bformatter.Serialize(stream, customPages);
            }
        }

        public List<List<Picture>> LoadCustomCategory()
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Open))
            {
                return (List<List<Picture>>)bformatter.Deserialize(stream);
            }
        }

        public void SaveCustomCategory(List<List<Picture>> customPages)
        {
            using (Stream stream = File.Open(customPicturesPath, FileMode.Create))
            {
                bformatter.Serialize(stream, customPages);
            }
        }

        public void CreateOptions()
        {
            using (Stream stream = File.Open(optionsPath, FileMode.Create))
            {
                bformatter.Serialize(stream, options);
            }
        }


        public Options LoadOptions()
        {
            using (Stream stream = File.Open(optionsPath, FileMode.Open))
            {
                return (Options)bformatter.Deserialize(stream);
            }
        }

        public void SaveOptions(Options options)
        {
            using (Stream stream = File.Open(optionsPath, FileMode.Create))
            {
                bformatter.Serialize(stream, options);
            }
        }

        public void CreateMostUsed()
        {
            using (Stream stream = File.Open(mostUsedPath, FileMode.Create))
            {
                bformatter.Serialize(stream, mostUsed);
            }
        }


        public SortedDictionary<String, Picture> LoadMostUsed()
        {
            using (Stream stream = File.Open(mostUsedPath, FileMode.Open))
            {
                return (SortedDictionary<String, Picture>)bformatter.Deserialize(stream);
            }
        }

        public void SaveMostUsed(SortedDictionary<String, Picture> mostUsed)
        {
            using (Stream stream = File.Open(mostUsedPath, FileMode.Create))
            {
                bformatter.Serialize(stream, mostUsed);
            }
        }
    }
}
