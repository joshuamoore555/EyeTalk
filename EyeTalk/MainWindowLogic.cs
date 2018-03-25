using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk
{
    public class MainWindowLogic
    {
        public int CategoryIndex { get; set; }
        public int PageIndex { get; set; }
        public int SentenceIndex { get; set; }
        public int AmountOfWordsInSentence { get; set; }


        public MainWindowLogic()
        {
            CategoryIndex = 0;
            PageIndex = 0;
            SentenceIndex = 0;
            AmountOfWordsInSentence = 0;
        }

    }
}
