using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WordPlay;

namespace UnitTest
{
    [TestClass]
    public class PlayerUnitTest
    {
        [TestMethod]
        public void Test1()
        {
            var str = "anna";
            Assert.IsTrue(Player.Play(ref str));
        }

        [TestMethod]
        public void Test2()
        {
            var str = "aaaaaaaa";
            Assert.IsTrue(Player.Play(ref str));
        }

        [TestMethod]
        public void Test3()
        {
            var str = "aaaaaaaab";
            Assert.IsTrue(Player.Play(ref str));
        }

        [TestMethod]
        public void Test4()
        {
            var str = "caaaaaaaab";
            Assert.IsFalse(Player.Play(ref str));
        }

        [TestMethod]
        public void Test5()
        {
            var str = "donotbobtonod";
            Assert.IsTrue(Player.Play(ref str));
        }

        [TestMethod]
        public void Test6()
        {
            var str = "owefhijpfwai";
            Assert.IsFalse(Player.Play(ref str));
        }

        [TestMethod]
        public void Test7()
        {
            var str = "igdedgide";
            Assert.IsTrue(Player.Play(ref str));
        }
    }
}
