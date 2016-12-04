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
    }
}
