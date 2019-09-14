using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;

namespace _1_LoginAdvanceAndCheckIt
{

    class Program
    {
        static void Main(string[] args)
        {
            var arrayEng_NumSymbol = new HashSet<char>()
            {
                'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                'а', 'б', 'в'
            };
            var arrayEng_NumSymbol2 = new HashSet<char>()
            {
                'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            };
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 5.1");
            WriteLine("Кузнецов");
            WriteLine(
                "1. Создать программу, которая будет проверять корректность ввода логина. Корректным логином будет строка от 2 до 10 символов, содержащая только буквы латинского алфавита или цифры, при этом цифра не может быть первой:" + Environment.NewLine +
                "   а) без использования регулярных выражений;" + Environment.NewLine +
                "   б) с использованием регулярных выражений.");

            WriteMassiveInFaile(@"login.txt", "root" + " " + "-169741140");

            var ac = new Account(@"login.txt");
            WriteLine("Ваш пароль и логин для раскрытия потаенных замыслов программиста C#:");
            var login = "";
            var password = "";
            var step = 1;
            do
            {
                //а) без использования регулярных выражений;
                //while (true)
                //{
                //    login = q.Question<string>("Введите логин? (Логин должен состоять от 2 до 10 символов (a-z, A-Z, 0-9). Цифры не могут быть первой)", arrayEng_NumSymbol, true);
                //    if (2 <= login.Length && 
                //        login.Length <= 10 && 
                //        !char.IsNumber(login[0]) && 
                //        login.Where(x => !arrayEng_NumSymbol2.Contains(x)).Count() == 0)
                //        break;
                //    else
                //        WriteLine("Логин не соответствует необходимым критериям. Введите логин еще раз.");
                //}

                //б) с использованием регулярных выражений.
                var regex = new Regex(@"\b[^ \t\n\r\f\v0-9А-яЁё][a-vA-Z0-9]{1,9}\b");
                while (true)
                {
                    login = q.Question<string>("Введите логин? (Логин должен состоять от 2 до 10 символов (a-z, A-Z, 0-9). Цифры не могут быть первой)", arrayEng_NumSymbol, true);
                    if (regex.IsMatch(login)) break;
                    else WriteLine("Логин не соответствует необходимым критериям. Введите логин еще раз.");
                }
                password = q.Question<string>("Введите пароль?", arrayEng_NumSymbol2, false);

                if (login == ac.Login && password.GetHashCode() == ac.Password)
                {
                    WriteLine("Потаенные замыслы программиста...");
                    ex.Print("НА ЭТОМ МЕСТЕ МОЖЕТ БЫТЬ ВАША РЕКЛАМА!", PositionForRow.Center, WindowHeight / 2);
                    ex.Print("Обращайтесь на сайте https://geekbrains.ru/", PositionForRow.Center, WindowHeight / 2 + 1);
                    ex.Print("Торопитесь! Предложение огрониченно и возможно из-за ваших конкурентов.", PositionForRow.Center, WindowHeight / 2 + 2);
                    break;
                }

                WriteLine("Неверный логин или пароль.");
                step++;
                if (step > 2)
                {
                    WriteLine("В доступе отказано.");
                    break;
                }
            } while (true);

            ex.Pause();
        }

        public static void WriteMassiveInFaile(string filename, string text)
        {
            StreamWriter sr = new StreamWriter(filename);
            try
            {
                sr.Write(text);
                sr.Close();
            }
            catch (Exception e)
            {
                sr.Close();
            }
        }
    }

    /// <summary>
    /// Загрузчик аккаунтов
    /// </summary>
    struct Account
    {
        public string Login;
        public int Password;

        /// <summary>
        /// Загрузить параметры пользователя
        /// </summary>
        /// <param name="file"></param>
        public Account(string file)
        {
            Login = "";
            Password = 0;
            if (File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);
                try
                {
                    var array = sr.ReadLine().Split(' ');
                    sr.Close();

                    if (array.Length > 1)
                    {
                        Login = array[0];
                        Password = int.Parse(array[1]);
                    }
                }
                catch (Exception e)
                {
                    sr.Close();
                }
            }
        }
    }
}
