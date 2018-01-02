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
        static Picture Pizza = new Picture("Pizza", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\pizza.png");
        static Picture Apple = new Picture("Apple", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\apple.png");
        static Picture Banana = new Picture("Banana", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\banana.png");

        static Picture Happy = new Picture("Happy", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\happy.png");
        static Picture Sad = new Picture("Sad", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\sad.png");
        static Picture Angry = new Picture("Angry", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\angry.png");

        static Picture Shower = new Picture("Shower", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\shower.png");
        static Picture Toilet = new Picture("Toilet", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\toilet.png");
        static Picture WashHands = new Picture("Wash Hands", false, "C:\\Users\\User\\source\\repos\\CategoryAndPicture\\CategoryAndPicture\\Pictures\\washhands.png");


        static List<Picture> food = new List<Picture>() { Pizza, Apple, Banana };
        static List<Picture> emotions = new List<Picture>() { Happy, Sad, Angry };
        static List<Picture> bathroom = new List<Picture>() { Shower, Toilet, WashHands };

        static SortedList<String, List<Picture>> categories = new SortedList<string, List<Picture>>(){
            {"Foods", food },
            {"Bathroom", bathroom },
            {"Emotions", emotions }
            };


        public static string dir = @"C:\Users\User\Desktop\SaveFolder";
        string serializationPath = Path.Combine(dir, "Categories.bin");

        public SaveFileSerialiser()
        {
            using (Stream stream = File.Open(serializationPath, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, categories);
            }
        }

        //load category from a file
        public SortedList<String, List<Picture>> Load()
        {
            using (Stream stream = File.Open(serializationPath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return (SortedList<String, List<Picture>>)bformatter.Deserialize(stream);
            }
        }
    }

}
