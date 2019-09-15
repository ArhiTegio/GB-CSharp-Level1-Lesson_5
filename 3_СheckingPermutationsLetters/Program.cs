using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;

namespace _3_СheckingPermutationsLetters
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayEng_NumSymbol2 = new HashSet<char>()
            {
                'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            };
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 5.3");
            WriteLine("Кузнецов");
            WriteLine(
                "3. *Для двух строк написать метод, определяющий, является ли одна строка перестановкой другой. Регистр можно не учитывать:" + Environment.NewLine +
                "   а) с использованием методов C#;" + Environment.NewLine +
                "   б) *разработав собственный алгоритм." + Environment.NewLine +
                "   Например: " + Environment.NewLine +
                "   badc являются перестановкой abcd.");

            var text1 = "abcd";
            var text2 = "badc";
            var text3 = "aant";

            WriteLine("Вариант А:");
            ex.Print($"Проверим строку \"{text1}\" и строку \"{text2}\":", PositionForRow.Center, CursorTop + 1);
            WriteLine(СheckingPermutationsLetters.CheckWordsForPermutation(text1, text2));
            ex.Print($"Проверим строку \"{text2}\" и строку \"{text3}\":", PositionForRow.Center, CursorTop + 1);
            WriteLine(СheckingPermutationsLetters.CheckWordsForPermutation(text2, text3));
            WriteLine("Вариант Б:");
            ex.Print($"Проверим строку \"{text1}\" и строку \"{text2}\":", PositionForRow.Center, CursorTop + 1);
            WriteLine(СheckingPermutationsLetters.CheckWordsForPermutation2(text1, text2));
            ex.Print($"Проверим строку \"{text2}\" и строку \"{text3}\":", PositionForRow.Center, CursorTop + 1);
            WriteLine(СheckingPermutationsLetters.CheckWordsForPermutation2(text2, text3));

            ex.Pause();
        }
    }

    public class СheckingPermutationsLetters
    {
        /// <summary>
        /// Проверка строки определяющий, является ли одна строка перестановкой другой (вариант а)
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        static public bool CheckWordsForPermutation(string text1, string text2)
        {
            if (text1.Length == text2.Length)            
                return text1.Select(Char.ToUpper)
                    .OrderBy(x => x)
                    .SequenceEqual(text2.Select(Char.ToUpper)
                                   .OrderBy(x => x));
            else
                return false;
        }

        /// <summary>
        /// Проверка строки определяющий, является ли одна строка перестановкой другой (вариант б)
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        static public bool CheckWordsForPermutation2(string text1, string text2)
        {
            if (text1.Length == text2.Length)
            {
                var n = new HashSet<char>(text1);
                foreach (var c in text2)
                    if (!n.Contains(c))
                        return false;
                return true;
            }
            else
                return false;
        }
    }
}
