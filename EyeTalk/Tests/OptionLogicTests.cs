using EyeTalk.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EyeTalk.Tests
{
    class OptionLogicTests
    {
        OptionsLogic logic;

        public OptionLogicTests()
        {
            logic = new OptionsLogic();
        }

        [Test]
        public void GetCurrentColourTest()
        {
            logic.Options.ApplicationColour = 0;
            Assert.AreEqual("#FF1493", logic.GetCurrentColour());
        }

        [Test]
        public void ChangeColourTest()
        {
            logic.Options.ApplicationColour = 0;
            Assert.AreEqual("#FF1493", logic.GetCurrentColour());
            logic.ChangeColour();
            Assert.AreEqual("#ff6a6a", logic.GetCurrentColour());
        }

        [Test]
        public void IncreaseEyeFixationDuration()
        {
            logic.ResetEyeFixationDuration();
            logic.IncreaseEyeFixationDuration();
            Assert.AreEqual(1, logic.Options.EyeFixationDuration);
        }

        [Test]
        public void ResetEyeFixationDuration()
        {
            logic.IncreaseEyeFixationDuration();
            logic.ResetEyeFixationDuration();
            Assert.AreEqual(0, logic.Options.EyeFixationDuration);

        }

        [Test]
        public void HasDurationBeenReachedTest()
        {          
            logic.ResetEyeFixationDuration();
            Assert.AreEqual(false, logic.HasDurationBeenReached());
            logic.Options.EyeFixationValue = 1;
            logic.IncreaseEyeFixationDuration();
            Assert.AreEqual(true, logic.HasDurationBeenReached());

        }

        [Test]
        public void ChangeVoiceType()
        {
            logic.Options.VoiceTypeSelection = 0;
            Assert.AreEqual("Male", logic.ChangeVoiceType());
            Assert.AreEqual("Female", logic.ChangeVoiceType());
        }

        [Test]
        public void IncreaseVoiceSpeed()
        {
            logic.Options.VoiceSpeedSelection = 0;
            Assert.AreEqual("Normal", logic.IncreaseVoiceSpeed());
            Assert.AreEqual("Fast", logic.IncreaseVoiceSpeed());
            Assert.AreEqual("Slow", logic.IncreaseVoiceSpeed());
        }

        [Test]
        public void DecreaseVoiceSpeed()
        {
            logic.Options.VoiceSpeedSelection = 0;
            Assert.AreEqual("Fast", logic.DecreaseVoiceSpeed());
            Assert.AreEqual("Normal", logic.DecreaseVoiceSpeed());
            Assert.AreEqual("Slow", logic.DecreaseVoiceSpeed());

        }

        [Test]
        public void IncreaseSelectionDelay()
        {
            logic.Options.EyeFixationValue = 0;
            logic.IncreaseSelectionDelay();
            Assert.AreEqual("0.4 Seconds", logic.IncreaseSelectionDelay());
        }

        [Test]
        public void DecreaseSelectionDelay()
        {
            logic.Options.EyeFixationValue = 2;

            Assert.AreEqual("0.4 Seconds", logic.DecreaseSelectionDelay());
        }

    }
}
