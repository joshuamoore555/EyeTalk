using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTalk.Logic;
using NUnit.Framework;
namespace EyeTalk.Tests
{
    class SavedSentencesLogicTests
    {
        SavedSentencesLogic logic;
        public List<String> savedSentences;

        public SavedSentencesLogicTests()
        {
            savedSentences = new List<string>();
            savedSentences.Add("Hello World");
        }

        [Test]
        public void TestMethod1()
        {
            var root = Path.GetPathRoot(Environment.CurrentDirectory);
            var temp = Path.Combine(root, "Save");

        }
    }
}
