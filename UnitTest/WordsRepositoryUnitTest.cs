using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPlay;

namespace UnitTest
{
    [TestClass]
    public class WordsRepositoryUnitTest
    {
        [TestMethod]
        public void TestLoadDefaultFile()
        {
            Console.WriteLine(System.Environment.CurrentDirectory);
            Assert.IsNotNull(new WordsRepository());
        }

        [TestMethod]
        public void TestIsWord()
        {
            var wordRepository = new WordsRepository();
            Assert.IsNotNull(wordRepository);
            var word = "warday";
            Assert.IsTrue(wordRepository.IsWord(ref word));
        }

        [TestMethod]
        public void TestGetAnagramWords()
        {
            var wordRepository = new WordsRepository();
            Assert.IsNotNull(wordRepository);
            var word = "igdedgide";
            var lists = wordRepository.GetSimilarWords(ref word);
            Console.WriteLine(lists.Count);
            foreach(var item in lists)
            {
                Console.WriteLine(item);
            }
            Assert.IsTrue(lists.Count > 1);
        }

        [TestMethod]
        public void TestRepositoryManager()
        {
            Assert.IsNotNull(WordsRepositoryManager.Instance);
        }
    }
}
