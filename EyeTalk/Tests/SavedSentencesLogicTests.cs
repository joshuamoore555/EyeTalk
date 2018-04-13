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

        public SavedSentencesLogicTests()
        {
            logic = new SavedSentencesLogic();
            
        }

        [Test]
        public void SaveSentenceThatIsNull()
        {
            var same = logic.SaveSentenceIfNotPreviouslySaved("");
            Assert.AreEqual("Please create a sentence before saving", same);

        }

        [Test]
        public void SaveSentenceDuplicate()
        {
            logic.SaveSentenceIfNotPreviouslySaved("Hello World");
            var same = logic.SaveSentenceIfNotPreviouslySaved("Hello World");
            Assert.AreEqual("Sentence has already been saved", same);

        }

        [Test]
        public void SaveSentenceUnique()
        {
            var same = logic.SaveSentenceIfNotPreviouslySaved("Test");
            Assert.AreEqual("Sentence Saved", same);

        }

        [Test]
        public void RetrieveFirstSentenceThatExists()
        {
            logic.SavedSentences.Add("Hello World");
            var same = logic.RetrieveFirstSavedSentenceIfExists();
            Assert.AreEqual("Hello World", same);
          
        }

        [Test]
        public void RetrieveFirstSentenceIfNotExisting()
        {
            logic.SavedSentences.Clear();
            var same = logic.RetrieveFirstSavedSentenceIfExists();
            Assert.AreEqual("No sentences saved", same);

        }

        [Test]
        public void ChangeSentences()
        {
            logic.SavedSentences.Clear();
            logic.SaveSentenceIfNotPreviouslySaved("1");
            logic.SaveSentenceIfNotPreviouslySaved("2");
            var index = logic.SentenceIndex;

            Assert.AreEqual(index, 0);

            Assert.AreEqual("2", logic.NextSentence());
            
            Assert.AreEqual("1", logic.NextSentence());

            Assert.AreEqual("2", logic.PreviousSentence());


        }

        [Test]
        public void NoSentenceSavedWhenChangingSentences()
        {
            logic.SavedSentences.Clear();

            Assert.AreEqual("No Sentences Saved", logic.NextSentence());
            Assert.AreEqual("No Sentences Saved", logic.PreviousSentence());


        }

        [Test]
        public void DeleteSentenceWhenNoneSaved()
        {
            logic.SavedSentences.Clear();
            var same = logic.DeleteSavedSentence();
            Assert.AreEqual("No Sentences Saved", same);

        }

        [Test]
        public void DeleteSentence()
        {
            logic.SavedSentences.Clear();
            logic.SaveSentenceIfNotPreviouslySaved("1");
            logic.SaveSentenceIfNotPreviouslySaved("2");
            logic.SentenceIndex = 0;
            var index = logic.SentenceIndex;
            Assert.AreEqual(0, index);
            var firstDelete = logic.DeleteSavedSentence();
            Assert.AreEqual("2", firstDelete);
            var secondDelete = logic.DeleteSavedSentence();

            Assert.AreEqual("No Sentences Saved", secondDelete);

        }
    }
}
