using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryFastDecisions;
using static System.Console;

namespace _2_Message
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
            WriteLine("С# - Уровень 1. Задание 5.2");
            WriteLine("Кузнецов");
            WriteLine(
                "2. Разработать класс Message, содержащий следующие статические методы для обработки текста:" + Environment.NewLine +
                "   а) Вывести только те слова сообщения, которые содержат не более n букв." + Environment.NewLine +
                "   б) Удалить из сообщения все слова, которые заканчиваются на заданный символ." + Environment.NewLine +
                "   в) Найти самое длинное слово сообщения." + Environment.NewLine +
                "   г) Сформировать строку с помощью StringBuilder из самых длинных слов сообщения." + Environment.NewLine +
                "      Продемонстрируйте работу программы на текстовом файле с вашей программой.");

            var massage = 
@"Как ни гнетет рука судьбины,
Как ни томит людей обман,
Как ни браздят чело морщины
И сердце как ни полно ран;

Каким бы строгим испытаньям
Вы ни были подчинены,-
Что устоит перед дыханьем
И первой встречею весны!

Весна… она о вас не знает,
О вас, о горе и о зле;
Бессмертьем взор ее сияет,
И ни морщины на челе.
Своим законам лишь послушна,
В условный час слетает к вам,
Светла, блаженно - равнодушна,
Как подобает божествам.

Цветами сыплет над землею,
Свежа, как первая весна;
Была ль другая перед нею —
О том не ведает она:
По небу много облак бродит,
Но эти облака — ея;
Она ни следу не находит
Отцветших весен бытия.

Не о былом вздыхают розы
И соловей в ночи поет;
Благоухающие слезы
Не о былом Аврора льет,-
И страх кончины неизбежной
Не свеет с древа ни листа:
Их жизнь, как океан безбрежный,
Вся в настоящем разлита.

Игра и жертва жизни частной!
Приди ж, отвергни чувств обман
И ринься, бодрый, самовластный,
В сей животворный океан!
Приди, струей его эфирной
Омой страдальческую грудь —
И жизни божеско - всемирной
Хотя на миг причастен будь!";
            ex.Print(massage, PositionForRow.LeftEdge, CursorTop + 1);
            WriteLine();
            ex.Print("Записываем в файл massage.txt", PositionForRow.Center, CursorTop + 2);
            Massage.WriteMassageInFile(massage, @"massage.txt");
            ex.Print("Загружаем из файл massage.txt", PositionForRow.Center, CursorTop + 2);
            var massage2 = Massage.LoadMessageFromFile(@"massage.txt");
            ex.Print(massage2, PositionForRow.LeftEdge, CursorTop + 1);
            WriteLine();
            ex.Print("Показать выборку слов менее 5 из послания:", PositionForRow.Center, CursorTop + 2);
            WriteLine();
            WriteLine(Massage.NoMoreThanNLetters(massage2, 5));
            ex.Print("Удалим слова оканчивающиеся на 'а' из послания:", PositionForRow.Center, CursorTop + 2);
            WriteLine();
            WriteLine(Massage.DeleteEndingWithLetter(massage2, 'а'));
            ex.Print("Показать самое длинное слово из сообщения:", PositionForRow.Center, CursorTop + 2);
            WriteLine();
            WriteLine(Massage.LongestWord(massage2));
            ex.Print("Показать самое короткое слово из сообщения:", PositionForRow.Center, CursorTop + 2);
            WriteLine();
            WriteLine(Massage.ShortestWord(massage2));
            ex.Print("Составить сообщение из слов длиной 5 и более:", PositionForRow.Center, CursorTop + 2);
            WriteLine();
            WriteLine(Massage.MoreThanNLetters(massage2, 5));

            ex.Pause();
        }
    }

    /// <summary>
    /// Обработчик сообщений
    /// </summary>
    public class Massage
    {
        /// <summary>
        /// Загрузка сообщения из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public string LoadMessageFromFile (string filename)
        {
            var s = new List<string>();
            //@"data.txt"
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);
                try
                {
                    while (true)
                    {
                        var t = sr.ReadLine();
                        if (t != null)
                            s.Add(t);
                        else
                            break;
                    }
                    
                    sr.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ошибка при загрузки файла");
                    sr.Close();
                }
            }

            return s.Aggregate((x, y) => x + Environment.NewLine + y);
        }

        /// <summary>
        /// Загрузка сообщения в файл
        /// </summary>
        /// <param name="massage"></param>
        /// <param name="filename"></param>
        static public void WriteMassageInFile(string massage, string filename)
        {
            StreamWriter sr = new StreamWriter(filename);
            try
            {
                sr.Write(massage);
                sr.Close();
            }
            catch (Exception e)
            {
                sr.Close();
            }
        }

        /// <summary>
        /// Получить текст со словами менее N символов
        /// </summary>
        /// <param name="massage"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        static public string NoMoreThanNLetters (string massage, int N) => 
            massage.Split(' ', Environment.NewLine[0], '.', ',', '!', '?', ';', ':', '-').Where(x => x.Length < N && x.Length != 0).Aggregate((x, y) => x + " " + y);

        /// <summary>
        /// Удалить из сообщения слова заканчивающиеся на определенную букву
        /// </summary>
        /// <param name="massage"></param>
        /// <param name="letter"></param>
        /// <returns></returns>
        static public string DeleteEndingWithLetter(string massage, char letter) =>
            massage.Split(' ', Environment.NewLine[0], '.', ',', '!', '?', ';', ':', '-').Where(x => x.Length != 0 && x[x.Length - 1] != letter).Aggregate((x, y) => x + " " + y);
        
        /// <summary>
        /// Получить самое длинное слово из сообщения
        /// </summary>
        /// <param name="massege"></param>
        /// <returns></returns>
        static public string LongestWord(string massege)
        {
            var t = massege.Split(' ', Environment.NewLine[0], '.', ',', '!', '?', ';', ':', '-');
            var word = "";
            foreach (var t1 in t)            
                if (word.Length <= t1.Length)
                    word = t1;
            return word;
        }

        /// <summary>
        /// Получить самое короткое слово из сообщение
        /// </summary>
        /// <param name="massege"></param>
        /// <returns></returns>
        static public string ShortestWord(string massege)
        {
            var t = massege.Split(' ', Environment.NewLine[0], '.', ',', '!', '?', ';', ':', '-');
            var word = "                                                                                       ";
            foreach (var t1 in t)
                if (word.Length >= t1.Length)
                    word = t1;
            return word;
        }

        /// <summary>
        /// Получить сообщение из слов с числом символов от N и более
        /// </summary>
        /// <param name="massage"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        static public string MoreThanNLetters(string massage, int N)
        {
            var array = massage.Split(' ', Environment.NewLine[0], '.', ',', '!', '?', ';', ':', '-');

            var sb = new StringBuilder();
            foreach(var word in array)
            {
                if(word.Length >= N)
                {
                    sb.Append(word);
                    sb.Append(" ");
                }
            }
            
            return sb.ToString();
        }
    }
}
