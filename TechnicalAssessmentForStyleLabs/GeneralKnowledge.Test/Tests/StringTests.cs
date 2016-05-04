using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic string manipulation exercises
    /// </summary>http://localhost:62677/Properties/
    public class StringTests : ITest
    {


        public void Run()
        {
            // TODO:
            // Complete the methods below

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("String Test");
            Console.WriteLine();

            Console.WriteLine("Anagram Test");
            AnagramTest();

            Console.WriteLine("-----");
            Console.WriteLine("Get Unique Test");
            GetUniqueCharsAndCount();
        }


        private void AnagramTest()
        {
            var word = "stop";
            var possibleAnagrams = new string[] { "test", "tops", "spin", "post", "mist", "step" };
                
            foreach (var possibleAnagram in possibleAnagrams)
            {
                Console.WriteLine(string.Format("{0} > {1}: {2}", word, possibleAnagram, possibleAnagram.IsAnagram(word)));
            }

        }

        private void GetUniqueCharsAndCount()
        {
            var word = "xxzwxzyzzyxwxzyxyzyxzyxzyzyxzzz";

            // TODO:
            // Write an algoritm that gets the unique characters of the word below 
            // and counts the number of occurences for each character found

            var answer = new String(word.Distinct().ToArray());

            foreach (var answerChar in answer)
            {
                var counter = word.Count(x => x == answerChar);
                Console.WriteLine("Char: "+answerChar +" - occurrences: "+ counter);
            }

        }
    }

    internal static class StringExtensions
    {
        public static bool IsAnagram(this string a, string b)
        {
            // TODO: 
            // Write logic to determine whether a is an anagram of b

            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                return false;
            if (a.Length != b.Length)
                return false;

            foreach (char c in b)
            {
                int ix = a.IndexOf(c);
                if (ix >= 0)
                    a = a.Remove(ix, 1);
                else
                    return false;
            }

            return string.IsNullOrEmpty(a);


        }
    }
}
