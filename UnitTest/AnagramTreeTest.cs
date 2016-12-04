using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPlay;
namespace UnitTest
{
    [TestClass]
    public class AnagramTreeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var str = "silent";
            var anagramTree = new AnagramTree();
            anagramTree.Process(str);
            foreach(var item in anagramTree.Leaves)
            {
                foreach(var treeNode in item.Value)
                {
                    treeNode.Print(Console.Write, Console.WriteLine);
                }
            }
        }
    }
}
