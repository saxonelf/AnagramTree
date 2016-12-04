using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordPlay
{
    public static class AppConfig
    {
        public readonly static string DefaultRepository = "WordPlay.WordsRepository";
    }

    public interface IWordsRepository
    {
        bool IsWord(ref string word);
        ICollection<string> GetSimilarWords(ref string str);
        bool HasSimilarWords(ref string str);
    }

    public class WordsRepositoryManager
    {
        private static WordsRepositoryManager _instance;
        public static WordsRepositoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    //use default repository
                    var repositoryType = Type.GetType(AppConfig.DefaultRepository, true);
                    var defaultRepository = (IWordsRepository)repositoryType.InvokeMember(AppConfig.DefaultRepository,
                        System.Reflection.BindingFlags.CreateInstance,
                        Type.DefaultBinder,
                        null,
                        null);
                    _instance = new WordsRepositoryManager(defaultRepository);
                }
                return _instance;
            }
        }
        public IWordsRepository Repository { get; set; }
        protected WordsRepositoryManager(IWordsRepository repository)
        {
            Repository = repository;
        }
    }

    public class WordsRepository : IWordsRepository
    {
        private const string DefaultWordsFile = "words.txt";
        private const string WordsLoadWarnMessage = "Can't load the words list file {0}, will load default words list.";
        private const string WordsLoadErrorMessage = "Can't find any words list file.";
        protected Dictionary<int, HashSet<string>> WordsList;
        protected Dictionary<int, Dictionary<string, HashSet<string>>> WordsIndex;

        public WordsRepository() : this(DefaultWordsFile, true)
        {
        }
        public WordsRepository(string wordsFile = DefaultWordsFile, bool useIndex = true)
        {
            WordsList = new Dictionary<int, HashSet<string>>();
            if (useIndex)
            {
                WordsIndex = new Dictionary<int, Dictionary<string, HashSet<string>>>();
            }

            bool isFileExist = false;
            if (!(isFileExist = File.Exists(wordsFile)))
            {
                if (wordsFile != DefaultWordsFile)
                {
                    wordsFile = DefaultWordsFile;
                    Console.WriteLine(string.Format(WordsLoadWarnMessage, wordsFile));
                    isFileExist = File.Exists(wordsFile);
                }

                if (!isFileExist)
                {
                    throw new FileNotFoundException(WordsLoadErrorMessage);
                }
            }

            var streamReader = new StreamReader(wordsFile, Encoding.UTF8);
            string tmpStr;
            while (!streamReader.EndOfStream)
            {
                tmpStr = streamReader.ReadLine();
                if (!WordsList.ContainsKey(tmpStr.Length))
                {
                    WordsList.Add(tmpStr.Length, new HashSet<string>());
                }
                WordsList[tmpStr.Length].Add(tmpStr);

                if (useIndex)
                {
                    if (!WordsIndex.ContainsKey(tmpStr.Length))
                    {
                        WordsIndex.Add(tmpStr.Length, new Dictionary<string, HashSet<string>>());
                    }
                    var orderedChrArray = tmpStr.ToCharArray();
                    Array.Sort<char>(orderedChrArray);
                    var orderedStr = new String(orderedChrArray);
                    if (!WordsIndex[tmpStr.Length].ContainsKey(orderedStr))
                    {
                        WordsIndex[tmpStr.Length].Add(orderedStr, new HashSet<string>());
                    }
                    WordsIndex[tmpStr.Length][orderedStr].Add(tmpStr);
                }
            }
        }

        public bool IsWord(ref string str)
        {
            return WordsList.ContainsKey(str.Length) && WordsList[str.Length].Contains(str);
        }

        public bool HasSimilarWords(ref string str)
        {
            bool result = false;
            var orderedChrArray = str.ToCharArray();
            Array.Sort<char>(orderedChrArray);
            var orderedStr = new String(orderedChrArray);

            if ((WordsIndex != null && WordsIndex.ContainsKey(str.Length) && WordsIndex[str.Length].ContainsKey(orderedStr))
                || (WordsList != null && WordsList.ContainsKey(str.Length) && WordsList[str.Length].Any(c => c.HasSameChars(ref orderedStr))))
            {
                result = true;
            }
            return result;
        }
        public ICollection<string> GetSimilarWords(ref string str)
        {
            ICollection<string> result = null;
            var orderedChrArray = str.ToCharArray();
            Array.Sort<char>(orderedChrArray);
            var orderedStr = new String(orderedChrArray);

            if (WordsIndex != null)
            {
                if (WordsIndex.ContainsKey(str.Length) && WordsIndex[str.Length].ContainsKey(orderedStr))
                {
                    result = WordsIndex[str.Length][orderedStr].ToList();
                }
            }
            else
            {
                if (WordsList != null && WordsList.ContainsKey(str.Length))
                {
                    result = WordsList[str.Length].Where(c => c.HasSameChars(ref orderedStr)).ToList();
                }
            }
            return result;
        }
    }
}
