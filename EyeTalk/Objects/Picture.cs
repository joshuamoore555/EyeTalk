using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Objects
{
    [Serializable]
    class Picture
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
        public string FilePath { get; set; }
    

        public Picture(string Name, bool Selected, String Filepath)
        {
            this.Name = Name;
            this.Selected = Selected;
            this.FilePath = Filepath;
        }
    }
}
