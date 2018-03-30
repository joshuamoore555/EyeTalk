﻿using EyeTalk.Utilities;
using System;
using System.Collections.Generic;
using System.IO;


namespace EyeTalk.Objects
{
    public class SaveFileSerialiser
    {
        public string dir = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        PictureInitialiser pictureInitialiser;
        public List<string> savedSentences = new List<string>();
        List<List<Picture>> mostUsed = new List<List<Picture>>();
        //SortedDictionary<String, Picture> mostUsed = new SortedDictionary<String, Picture>();
        Options options = new Options(0, 6, 0, 0, false);
        List<List<Picture>> customPages = new List<List<Picture>>();

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

            pictureInitialiser = new PictureInitialiser(); 
            CreateInitialFoldersAndFiles();

        }

        //load category from a file

        public void CreateInitialFoldersAndFiles()
        {
            Directory.CreateDirectory(saveDir);

            if (File.Exists(categoryPath))
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

            if (File.Exists(mostUsedPath))
            {
                CreateMostUsed();
            }
        }

        public void CreateCategoryFile()
        {
            using (Stream stream = File.Open(categoryPath, FileMode.Create))
            {
                bformatter.Serialize(stream, pictureInitialiser.categories);
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
                bformatter.Serialize(stream, pictureInitialiser.customPages);
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
                bformatter.Serialize(stream, pictureInitialiser.customPages);
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
    }
}
