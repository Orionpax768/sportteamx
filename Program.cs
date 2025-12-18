//*******************************************************************************************
//* Практическая работа №15                                                                 *
//* Выполнил: Абдуллаев Э.С., группа 2-ИСПд                                                 *
//* Задание: Написать программу, выполняющую следующие действия:                            *
//* - Ввод с клавиатуры данных в массив, состоящий из 5 элементов типа «Спортивная команда».*
//* - Вывод на экран информации о всех спортсменах, занимающихся указанным видом спорта     *
//*   (вид спорта вводится с клавиатуры).                                                   *
//* - Определить спортсмена с лучшим результатом (среди указанного вида спорта).            *
//*******************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeam
{
    class Program
    {
        struct SportsTeam
        {
            public string FullName;
            public string SportType;
            public double BestResult;

            public SportsTeam(string fullName, string sportType, double bestResult)
            {
                FullName = fullName;
                SportType = sportType;
                BestResult = bestResult;
            }

            static bool IsValidNameOrSport(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return false;
                foreach (char c in input)
                {
                    if (char.IsDigit(c))
                        return false;
                }
                return true;
            }

            static SportsTeam InputAthlete(int index)
            {
                while (true)
                {
                    Console.WriteLine($"\nВведите данные для спортсмена №{index + 1}:");
                    Console.Write("ФИО: ");
                    string fullName = Console.ReadLine();
                    if (!IsValidNameOrSport(fullName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: ФИО не может быть пустым, состоять из пробелов или содержать цифры.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        continue;
                    }
                    Console.Write("Вид спорта: ");
                    string sportType = Console.ReadLine();
                    if (!IsValidNameOrSport(sportType))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: Вид спорта не может быть пустым, состоять из пробелов или содержать цифры.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        continue;
                    }
                    Console.Write("Лучший результат (число): ");
                    if (!Double.TryParse(Console.ReadLine(), out double result) || result < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: введите корректное неотрицательное число.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        continue;
                    }
                    return new SportsTeam(fullName, sportType, result);
                }
            }
        }
            static void DisplayAthletesBySport(SportsTeam[] teams, string sport)
            {
                bool found = false;
                Console.WriteLine($"\nСпортсмены, занимающиеся {sport}:");
                foreach (SportsTeam athlete in teams)
                {
                    if (athlete.SportType == sport)
                    {
                        Console.WriteLine($"ФИО: {athlete.FullName}, Результат: {athlete.BestResult}");
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Спортсмены не найдены.");
                }
            }

            static bool TryGetBestAthlete(SportsTeam[] teams, string sport, out SportsTeam bestAthlete)
            {
                bestAthlete = new SportsTeam();
                bool found = false;
                double bestResult = -1;
                foreach (SportsTeam athlete in teams)
                {
                    if (athlete.SportType == sport)
                    {
                        if (!found || athlete.BestResult > bestResult)
                        {
                            bestAthlete = athlete;
                            bestResult = athlete.BestResult;
                            found = true;
                        }
                    }
                }
                return found;
            }
            static void Main()
            {
                Console.Clear();
                Console.Title = "Практическая работа №15";
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Здравствуйте!");
                while (true)
                {
                    try
                    {
                        const int count = 5;
                        SportsTeam[] teams = new SportsTeam[count];
                        for (int i = 0; i < count; i++)
                        {
                            teams[i] = InputAthlete(i);
                        }
                        string searchSport;
                        while (true)
                        {
                            Console.Write("\nВведите вид спорта для поиска: ");
                            searchSport = Console.ReadLine();
                            if (!IsValidNameOrSport(searchSport))
                            {
                                Console.WriteLine("Ошибка: вид спорта не может быть пустым, состоять из пробелов или содержать цифры.");
                                continue;
                            }
                            break;
                        }
                        DisplayAthletesBySport(teams, searchSport);
                        if (TryGetBestAthlete(teams, searchSport, out SportsTeam best))
                        {
                            Console.WriteLine($"\nЛучший спортсмен в {searchSport}:");
                            Console.WriteLine($"ФИО: {best.FullName}, Вид спорта: {best.SportType}, Результат: {best.BestResult}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nВыберите действие:");
                        Console.WriteLine("1 - Новый поиск (ввести новых спортсменов)");
                        Console.WriteLine("0 - Выйти из программы");
                        Console.Write("Ваш выбор: ");
                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Новый поиск...");
                                break;
                            case "0":
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Программа завершена.");
                                return;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new Exception("Неверный выбор!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Критическая ошибка: {ex.Message}");
                        Console.WriteLine("Нажмите любую клавишу для выхода...");
                        Console.ReadKey();
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
    }
}







using System;
using System.Linq;

namespace SportTeam
{
    class Program
    {
        struct SportsTeam
        {
            public string FullName;
            public string SportType;
            public double BestResult;

            public SportsTeam(string fullName, string sportType, double bestResult)
            {
                FullName = fullName;
                SportType = sportType;
                BestResult = bestResult;
            }

            public static SportsTeam Input(int index)
            {
                while (true)
                {
                    Console.WriteLine($"\nВведите данные для спортсмена №{index + 1}:");
                    Console.Write("ФИО: ");
                    string fullName = Console.ReadLine();
                    if (!IsValidNameOrSport(fullName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: ФИО не может быть пустым, состоять из пробелов или содержать цифры.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        continue;
                    }

                    Console.Write("Вид спорта: ");
                    string sportType = Console.ReadLine();
                    if (!IsValidNameOrSport(sportType))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: Вид спорта не может быть пустым, состоять из пробелов или содержать цифры.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        continue;
                    }

                    Console.Write("Лучший результат (число): ");
                    if (!double.TryParse(Console.ReadLine(), out double result) || result < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка: введите корректное неотрицательное число.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        continue;
                    }

                    return new SportsTeam(fullName, sportType, result);
                }
            }

            public static bool IsValidNameOrSport(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return false;
                foreach (char c in input)
                {
                    if (char.IsDigit(c))
                        return false;
                }
                return true;
            }

            public bool IsSameSport(string sport) => 
                !string.IsNullOrEmpty(sport) && SportType.Equals(sport, StringComparison.OrdinalIgnoreCase);
        }

        static void DisplayAthletesBySport(SportsTeam[] teams, string sport)
        {
            bool found = false;
            Console.WriteLine($"\nСпортсмены, занимающиеся {sport}:");
            foreach (SportsTeam athlete in teams)
            {
                if (athlete.IsSameSport(sport))
                {
                    Console.WriteLine($"ФИО: {athlete.FullName}, Результат: {athlete.BestResult}");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Спортсмены не найдены.");
            }
        }

        static bool TryGetBestAthlete(SportsTeam[] teams, string sport, out SportsTeam bestAthlete)
        {
            bestAthlete = new SportsTeam();
            bool found = false;
            double bestResult = -1;

            foreach (SportsTeam athlete in teams)
            {
                if (athlete.IsSameSport(sport))
                {
                    if (!found || athlete.BestResult > bestResult)
                    {
                        bestAthlete = athlete;
                        bestResult = athlete.BestResult;
                        found = true;
                    }
                }
            }
            return found;
        }

        static void Main()
        {
            Console.Clear();
            Console.Title = "Практическая работа №15";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Здравствуйте!");

            while (true)
            {
                try
                {
                    const int count = 5;
                    SportsTeam[] teams = new SportsTeam[count];

                    for (int i = 0; i < count; i++)
                    {
                        teams[i] = SportsTeam.Input(i);
                    }

                    string searchSport;
                    while (true)
                    {
                        Console.Write("\nВведите вид спорта для поиска: ");
                        searchSport = Console.ReadLine();

                        if (!SportsTeam.IsValidNameOrSport(searchSport))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ошибка: вид спорта не может быть пустым, состоять из пробелов или содержать цифры.");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            continue;
                        }
                        break;
                    }

                    DisplayAthletesBySport(teams, searchSport);

                    if (TryGetBestAthlete(teams, searchSport, out SportsTeam best))
                    {
                        Console.WriteLine($"\nЛучший спортсмен в {searchSport}:");
                        Console.WriteLine($"ФИО: {best.FullName}, Вид спорта: {best.SportType}, Результат: {best.BestResult}");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nВыберите действие:");
                    Console.WriteLine("1 - Новый поиск (ввести новых спортсменов)");
                    Console.WriteLine("0 - Выйти из программы");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Новый поиск...");
                            break;
                        case "0":
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Программа завершена.");
                            return;
                        default:
                            throw new Exception("Неверный выбор!");
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Критическая ошибка: {ex.Message}");
                    Console.WriteLine("Нажмите любую клавишу для выхода...");
                    Console.ReadKey();
                    break;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
