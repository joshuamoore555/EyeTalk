using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Utilities
{
    class Keyboard
    {
        public Keyboard()
        {

        }

        public string AddLetter(string sentence, char letter)
        {
            return sentence = sentence + letter;           
        }

        public string DeleteLastLetter(string sentence)
        {
            if(sentence.Length > 0)
            {
                return sentence.Remove(sentence.Length - 1);
            }
            else
            {
                return "";
            }
            
        }
    }
}
