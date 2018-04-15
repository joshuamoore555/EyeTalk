
namespace EyeTalk.Utilities
{
    class Keyboard
    {
        public Keyboard()
        {

        }

        public string AddLetter(string sentence, char letter)
        {
            if(sentence.Length < 20)
            {
                return sentence = sentence + letter;
            }
            else
            {
                return sentence;
            }
        }

        public string DeleteLastLetter(string sentence)
        {
            if(sentence.Length > 0)
            {
                return sentence.Remove(sentence.Length - 1);
            }
            else
            {
                return sentence;
            }
            
        }
    }
}
