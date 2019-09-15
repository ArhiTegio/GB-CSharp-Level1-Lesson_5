using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LibraryFastDecisions;
using static System.Console;

namespace _5_BelieveUnbelievers
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayEng_NumSymbol2 = new HashSet<char>()
            {
                '1', '2',
            };
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 5.5");
            WriteLine("Кузнецов");
            WriteLine(
                "5. **Написать игру «Верю. Не верю». В файле хранятся вопрос и ответ, правда это или нет. " + Environment.NewLine +
                "Например: «Шариковую ручку изобрели в древнем Египте», «Да». Компьютер загружает эти данные, " + Environment.NewLine +
                "случайным образом выбирает 5 вопросов и задаёт их игроку. Игрок отвечает Да или Нет на каждый " + Environment.NewLine +
                "вопрос и набирает баллы за каждый правильный ответ. Список вопросов ищите во вложении или воспользуйтесь интернетом.");

            var bu = new BelieveUnbelievers();
            var allQuestion = 0;
            var score = 0;

            ex.Print("Играй \"Верю. не верю\"", PositionForRow.Center, CursorTop + 1);
            WriteLine();
            foreach (var v in bu.Question())
            {
                WriteLine(v.Item1);
                WriteLine("Нажмите 1 - для ответа да, 2 - для ответа нет.");
                var answer = ReadKey();
                var t = "";
                if (answer.Key == ConsoleKey.NumPad1 || answer.Key == ConsoleKey.D1)
                    t = "Да";
                else
                    t = "Нет";
                WriteLine();
                if (t == v.Item2)
                    score++;
                allQuestion++;
            }
            ex.Print($"Вы ответили правильно: {allQuestion}/{score}.", PositionForRow.Center, CursorTop + 1);
            ex.Pause();
        }
    }

    class BelieveUnbelievers
    {
        public Dictionary<string, string> answer = new Dictionary<string, string>();
        public BelieveUnbelievers()
        {
            if (File.Exists(@"Answer.txt"))
            {
                StreamReader sr = new StreamReader(@"Answer.txt");
                try
                {
                    while (true)
                    {
                        var t = sr.ReadLine();
                        if (t != null)
                        {
                            var t2 = t.Split('|');
                            if (!answer.ContainsKey(t2[0]))
                                answer.Add(t2[0], t2[1]);
                        }
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
        }
        /// <summary>
        /// Получить перерчислитель вопросов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tuple<string, string>> Question()
        {
            foreach(var n in answer)
            {
                yield return Tuple.Create(n.Key, n.Value);
            }
        }
    }
}
