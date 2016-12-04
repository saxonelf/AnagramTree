using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPlay
{
    public static class StringExtension
    {
        public delegate bool IsEqualIntArray(int[] x, int[] y);
        public static bool IsPalindrome(this string str)
        {
            bool result = true;

            //Analyze string that whether it is a palindrome.
            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i] == str[str.Length - i - 1])
                {
                    continue;
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool IsAnagramOfPalindrome(this string str)
        {
            bool result = true;
            short singleTimes = 0;
            var tmpStr = str;
            char singleChar = '\0';
            int coupleIndex = 0;

            while (tmpStr.Length > 0 && singleTimes < 2)
            {
                singleChar = tmpStr[0];
                tmpStr = tmpStr.Remove(0, 1);

                coupleIndex = tmpStr.IndexOf(singleChar);
                if (coupleIndex >= 0)
                {
                    tmpStr = tmpStr.Remove(coupleIndex, 1);
                }
                else
                {
                    if (++singleTimes >= 2)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool HasSameChars(this string str, ref string compareStr)
        {
            bool result = true;
            if (str.Length == compareStr.Length)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (compareStr.IndexOf(str[i]) < 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static HashSet<string> GetCombinationStringIngoreOrder(this string str, int wordLen)
        {
            if (str.Length < wordLen || wordLen <= 0)
            {
                throw new ArgumentOutOfRangeException("The wordLen of combination string should be less or equal, and above zero to the string used to do combination.");
            }

            var result = new HashSet<string>();
            int[] wordCounter = new int[wordLen];
            int[] lastWordCounter = new int[wordLen];
            int tmpValue = 0;
            //initiate the counter
            for (int i = 0; i < wordLen; i++)
            {
                wordCounter[i] = tmpValue;
                lastWordCounter[i] = str.Length - wordLen + tmpValue;
                tmpValue++;
            }
            var tmpStr = new StringBuilder(4);
            IsEqualIntArray isEqual = (x, y) =>
            {
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i])
                    {
                        return false;
                    }
                }
                return true;
            };
            while (!isEqual(wordCounter, lastWordCounter))
            {
                tmpStr.Clear();

                //generate combination
                for (int i = 0; i < wordLen; i++)
                {
                    tmpStr.Append(str[wordCounter[i]]);
                }
                result.Add(tmpStr.ToString());

                //increase the word counter
                for (int i = wordLen - 1; i >= 0; i--)
                {
                    if ((wordCounter[i] + 1) > lastWordCounter[i])
                    {
                        continue;
                    }
                    else
                    {
                        for (int j = i; j < wordLen; j++)
                        {
                            if (j == i)
                            {
                                wordCounter[j] = wordCounter[j] + 1;
                            }
                            else
                            {
                                wordCounter[j] = wordCounter[j - 1] + 1;
                            }
                        }
                        break;
                    }
                }
            }

            //add the last combination
            tmpStr.Clear();
            for (int i = 0; i < wordLen; i++)
            {
                tmpStr.Append(str[wordCounter[i]]);
            }
            result.Add(tmpStr.ToString());

            return result;
        }

        public static List<List<int>> GetIntComposition(this int x, int minNum)
        {
            var result = new List<List<int>>();
            return result;
        }
    }
}
