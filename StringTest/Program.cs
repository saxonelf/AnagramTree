using System;

namespace WordPlay
{
    public static class Player {
        public static bool Play(ref string words)
        {
            var regex = new System.Text.RegularExpressions.Regex("^[a-z]+$");
            //Check string that only contain lower alphabet characters.
            if (!regex.IsMatch(words))
            {
                throw new FormatException("The string can only accept lower alphabet characters from a to z.");
            }

            if (words.IsPalindrome())
            {
                Console.WriteLine("A palindrome.");
                return true;
            }
            else if (words.IsAnagramOfPalindrome())
            {
                Console.WriteLine("An anagram of a palindrome.");
                return true;
            }
            else
            {
                Console.WriteLine("Neither.");
                return false;
            }
        }
    }

    class Program
    {
        static readonly string FormatErrorMessage = "The string {0} is not valid. Try to input another one. Error: {1}";
        static void Main(string[] args)
        {
            string word = "";
            Console.WriteLine("An application uses to check whehter a word is palindrome, or an anagram of palindrome.");
            while(true)
            {
                Console.WriteLine("Input a word, or Press Ctrl+C to quit:");
                word = Console.ReadLine();
                try
                {
                    Console.WriteLine(Player.Play(ref word));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(string.Format(FormatErrorMessage, word, ex.Message));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown exception. " + ex.Message);
                }
            }
        }
    }
}
