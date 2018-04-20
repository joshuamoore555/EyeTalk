using EyeTalk.Utilities;
using System;
using System.Collections.Generic;
using System.IO;


namespace EyeTalk.Objects
{
    public class SaveFileSerialiser
    {
        PictureInitialiser pictureInitialiser;
        public List<string> savedSentences = new List<string>();
        List<List<Picture>> mostUsed = new List<List<Picture>>();
        List<Picture> mostUsedList = new List<Picture>();
        Options options = new Options(0, 6, 0, 0, 0);
        List<List<Picture>> customPages = new List<List<Picture>>();
       
        public string saveFolder;

        public string categoryPath;
        public string savedSentencesPath;
        public string customPicturesPath;
        public string optionsPath;
        public string mostUsedPath;
        public string mostUsedListPath;      

        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();


        public SaveFileSerialiser()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); //folder used to store all the saves

            //creates the paths for the different files and folders
            saveFolder = Path.Combine(path, "Saves");
            categoryPath = Path.Combine(saveFolder, "Categories.bin");
            savedSentencesPath = Path.Combine(saveFolder,"SavedSentences.bin");
            customPicturesPath = Path.Combine(saveFolder, "CustomPictures.bin");
            optionsPath = Path.Combine(saveFolder, "Options.bin");
            mostUsedPath = Path.Combine(saveFolder, "MostUsed.bin");
            mostUsedListPath = Path.Combine(saveFolder, "mostUsedList.bin");
            
            pictureInitialiser = new PictureInitialiser(); 
            CreateInitialFoldersAndFiles();
            
        }

        public void CreateInitialFoldersAndFiles()
        {
            //this creates folders and files if they don't exist
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
           
            if (!File.Exists(categoryPath))
            {
                CreateCategoryFile();
            }

            if (!File.Exists(savedSentencesPath))
            {
                CreateSavedSentencesFile();
            }

            if (!File.Exists(customPicturesPath))
            {
                CreateCustomPictureFile();
            }

            if (!File.Exists(optionsPath))
            {
                CreateOptions();
            }

            if (!File.Exists(mostUsedPath))
            {
                CreateMostUsed();
            }

            if (!File.Exists(mostUsedListPath))
            {
                CreateMostUsedList();
            }
        }

        //All the below methods create, load, or save different types of .bin files

        public void CreateCategoryFile()
        {

            using (Stream stream = File.Open(categoryPath, FileMode.Create))
            {
                bformatter.Serialize(stream, pictureInitialiser.categories);
            }
        }

        public List<List<List<Picture>>> LoadCategories()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Open))
            {
                return (List<List<List<Picture>>>)bformatter.Deserialize(stream);
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

        public List<List<Picture>> LoadMostUsed()
        {
            using (Stream stream = File.Open(mostUsedPath, FileMode.Open))
            {
                return (List<List<Picture>>)bformatter.Deserialize(stream);
            }
        }

        public void SaveMostUsed(List<List<Picture>> mostUsed)
        {
            using (Stream stream = File.Open(mostUsedPath, FileMode.Create))
            {
                bformatter.Serialize(stream, mostUsed);
            }
        }

        public void CreateMostUsedList()
        {
            using (Stream stream = File.Open(mostUsedListPath, FileMode.Create))
            {
                bformatter.Serialize(stream, mostUsedList);
            }
        }

        public List<Picture> LoadMostUsedList()
        {
            using (Stream stream = File.Open(mostUsedListPath, FileMode.Open))
            {
                return (List<Picture>)bformatter.Deserialize(stream);
            }
        }

        public void SaveMostUsedList(List<Picture> mostUsedList)
        {
            using (Stream stream = File.Open(mostUsedListPath, FileMode.Create))
            {
                bformatter.Serialize(stream, mostUsedList);
            }
        }
    }
}
