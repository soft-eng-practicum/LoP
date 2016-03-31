using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Completed;

namespace UnitTestforLOP
{
    [TestClass]
    public class GameTester
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void enemyTester()
        {
            Wall w = new Wall();

            w.DamageWall(3);
            Assert.AreEqual(Wall.Equals);
        }
    }
}
