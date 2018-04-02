using EyeTalk.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EyeTalk.Logic
{
    class AddPictureLogic
    {
        public AddPictureLogic()
        {
            
        }

        public string LoadCustomPicture()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg"
                
            };
            
            Nullable<bool> result = dialog.ShowDialog();

            List<BitmapImage> list = new List<BitmapImage>();

            if (result == true)
            {
                return dialog.FileName;

            }
            else
            {
                return "";
            }

        }

        public bool CheckCustomPictureIsNotDuplicate(Picture customPicture,  List<List<Picture>> customCategory)
        {
            foreach (var page in customCategory)
            {
                foreach (var picture in page)
                {
                    if (picture.Name == customPicture.Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool CheckNumberOfCustomImagesInPage(List<Picture> currentPage)
        {
            if(currentPage.Count < 4)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void AddCustomPicture(Picture customPicture, List<Picture> currentPage)
        {
            currentPage.Add(customPicture);
        }

        public List<Picture> CreateNewPageAndAddCustomPicture(Picture customPicture)
        {
            List<Picture> newCustomCategory = new List<Picture>() { };
            newCustomCategory.Add(customPicture);
            return newCustomCategory;
        }
    }
}
