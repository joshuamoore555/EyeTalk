using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Objects
{
    [Serializable]
    public class Picture
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
        public string Filepath { get; set; }
        public ulong AmountOfTimesClicked { get; set; }


        public Picture(string Name, bool Selected, String Filepath, ulong AmountOfTimesClicked)
        {
            this.Name = Name;
            this.Selected = Selected;
            this.Filepath = Filepath;
            this.AmountOfTimesClicked = AmountOfTimesClicked;
        }
    }
}
