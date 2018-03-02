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
        public static string dir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        static Picture pizza = new Picture("Pizza", false, Path.Combine(Environment.CurrentDirectory, "Pictures", "Pizza.png"));
    
        static List<Picture> food = new List<Picture>() { pizza, pizza, pizza, pizza, pizza, pizza, pizza, pizza, pizza, pizza, pizza };

        static List<Picture> custom = new List<Picture>() { };


        static SortedList<String, List<Picture>> categories = new SortedList<string, List<Picture>>(){
            {"Foods", food },
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

            CreateCategoryFile();
            CreateSavedSentencesFile();
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
