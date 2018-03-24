using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.Factory;

namespace EyeTalk.Tests
{
    [TestClass]
   public class HomeTests
    {
        [TestMethod]
        public void TestHome()
        {
            var app = Application.Launch("EyeTalk");
            var window = app.GetWindow("MainWindow",InitializeOption.NoCache);
            var begin = window.Get<Button>("BeginSpeaking");
            app.Close();
        }
    }
}
