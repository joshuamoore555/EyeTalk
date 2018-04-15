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

        public List<string> RetrieveAllImagesInPictureDirectory()
        {
            IEnumerable<string> files = GetFilepathsFromPictureDirectory();

            return GetImageFilepaths(files);
        }

        private List<string> GetImageFilepaths(IEnumerable<string> files)
        {
            //checks if filepath is an image format, and adds to list of images

            List<string> imageFiles = new List<string>();

            foreach (string filepath in files)
            {
                if (filepath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || filepath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || filepath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    imageFiles.Add(filepath);
                }
            }

            return imageFiles;
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
