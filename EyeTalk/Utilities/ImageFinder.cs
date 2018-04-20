using System;
using System.Collections.Generic;
using System.IO;

namespace EyeTalk.Utilities
{
    class ImageFinder
    {
        public ImageFinder()
        {

        }

        private List<string> GetImageFilepaths(IEnumerable<string> files)
        {
            List<string> imageFiles = new List<string>();

            //checks if files in the picture folder are a picture format, then adds them to the list if they are

            foreach (string filepath in files)
            {
                if (filepath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || filepath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || filepath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    imageFiles.Add(filepath);
                }
            }

            return imageFiles;
        }

        public List<string> RetrieveAllImagesInPictureDirectory()
        {
            //gets all files from the picture folder
            IEnumerable<string> files = GetFilepathsFromPictureDirectory();

            return GetImageFilepaths(files);
        }

        private IEnumerable<string> GetFilepathsFromPictureDirectory()
        {
            //returns files from picture directory and sub directories
            string picturePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            var files = Directory.EnumerateFiles(picturePath, "*.*", SearchOption.AllDirectories);
            return files;
        }
    }
}
