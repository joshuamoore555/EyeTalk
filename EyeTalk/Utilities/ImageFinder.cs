using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Utilities
{
    class ImageFinder
    {
        public ImageFinder()
        {

        }

        public List<string> RetrieveAllImagesInFolder()
        {
            List<string> imageFiles = new List<string>();

            string picturePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            var files = Directory.EnumerateFiles(picturePath, "*.*", SearchOption.AllDirectories);
            


            foreach (string name in files) //C:\Users\User\Pictures
            {
                if (name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    imageFiles.Add(name);
                }
            }

            return imageFiles;
        }

        public static IEnumerable<string> GetFiles(string root, string searchPattern = "*")
        {
            var files = new List<string>();

            try
            {
                foreach (var dir in Directory.GetDirectories(root))
                {
                    try
                    {
                        var foundFIles = Directory.GetFiles(dir, searchPattern, SearchOption.TopDirectoryOnly).ToList();
                        files.AddRange(foundFIles);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("INFO: Cannot enumerate file in folder: {0}. Error: {1}", dir, ex.Message);
                        continue;
                    }

                    var recursiveFiles = GetFiles(dir, searchPattern);

                    if (recursiveFiles != null)
                    {
                        files = files.Concat(recursiveFiles).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("INFO: Cannot enumerate file in folder: {0}. Error: {1}", root, ex.Message);
            }

            return files;
        }
    }
}
