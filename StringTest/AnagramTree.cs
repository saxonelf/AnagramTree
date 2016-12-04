using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPlay
{
    public class AnagramTree
    {
        public delegate void PrintFunc(string x);
        public delegate void PrintLineFunc(string x);
        //current tree word
        public string CurrWord { get; set; }
        public bool IsWord { get; set; }
        public int MinLenStr { get; set; }
        //use dictionary to category leaves by the word length of leaves
        public Dictionary<int, HashSet<AnagramTree>> Leaves { get; set; }

        public void Process(string str)
        {
            //var orderedCharArray = str.ToCharArray();
            //Array.Sort<char>(orderedCharArray);
            MinLenStr = 2;
            CurrWord = str;
            var numCompositions = str.Length.GetIntComposition(MinLenStr);
            //For Test
            var a = new List<int>();
            a.Add(2);a.Add(2);a.Add(2);
            numCompositions.Add(a);
            a = new List<int>();
            a.Add(2); a.Add(4);
            numCompositions.Add(a);
            a = new List<int>();
            a.Add(3); a.Add(3);
            numCompositions.Add(a);
            a = new List<int>();
            a.Add(6);
            numCompositions.Add(a);
            foreach (var numComp in numCompositions)
            {
                ProcessTree(str, numComp);
            }
        }

        private void ProcessTree(string str, IList<int> numList)
        {
            if (numList.Count > 0)
            {
                var num = numList.ElementAt(0);
                numList.RemoveAt(0);

                var posibilities = str.GetCombinationStringIngoreOrder(num);
                foreach (var posItem in posibilities)
                {
                    var tmpStr = posItem;
                    if (WordsRepositoryManager.Instance.Repository.HasSimilarWords(ref tmpStr))
                    {
                        if (Leaves == null)
                        {
                            Leaves = new Dictionary<int, HashSet<AnagramTree>>();
                        }

                        if (!Leaves.ContainsKey(num))
                        {
                            Leaves.Add(num, new HashSet<AnagramTree>());
                        }
                        var similarWords = WordsRepositoryManager.Instance.Repository.GetSimilarWords(ref tmpStr);
                        if (similarWords != null)
                        foreach (var word in similarWords)
                        {
                            var newLefe = new AnagramTree()
                            {
                                CurrWord = word,
                                IsWord = true
                            };
                            Leaves[num].Add(newLefe);
                            newLefe.ProcessTree(str.Replace(posItem, ""), numList);
                        }
                    }
                }
            }
        }

        public void Print(PrintFunc printFunc, PrintLineFunc printLineFunc)
        {
            if (Leaves == null)
            {
                printLineFunc(CurrWord);
            }
            else
            {
                printFunc(CurrWord + " ");
            }
        }
    }
}
