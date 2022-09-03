﻿namespace _2022._08._31_PW
{
    internal class Program
    {
        static Semaphore semaphore = new(2, 2);
        static List<Fork> forks = new() { new(), new(), new(), new(), new() };
        static List<Philosopher> philosophers = new()
        {
            new() { Name = "Фалес" },
            new() { Name = "Анаксимандр" },
            new() { Name = "Пифагор" },
            new() { Name = "Эпикур" },
            new() { Name = "Гераклит Эфесский" }
        };

        static void Main()
        {
            Task.Run(ExitWait);
            Task.WaitAll(Task.Run(Lunch), Task.Run(Think));

        }

        static void Think()
        {
            while (true)
            {
                foreach(Philosopher philosopher in philosophers)
                {
                    if(!philosopher.IsEating)
                    {
                        Task.Run(() => Thinking(philosopher));
                    }
                }
            }
        }

        static void Lunch()
        {
            while (true)
            {
                foreach (Philosopher philosopher in philosophers)
                {
                    Fork fork1 = null;
                    Fork fork2 = null;
                    for (int i = 0; i < forks.Count; i++)
                    {
                        if (!forks[i].IsBusy)
                        {
                            fork1 = forks[i];
                            break;
                        }
                    }
                    for (int i = 0; i < forks.Count; i++)
                    {
                        if (!forks[i].IsBusy)
                        {
                            fork2 = forks[i];
                            break;
                        }
                    }
                    Task.Run(() => Eating(philosopher, fork1, fork2));
                }
            }
        }

        static void Eating(Philosopher philosopher, Fork fork1, Fork fork2)
        {
            semaphore.WaitOne();
            fork1.IsBusy = true;
            fork2.IsBusy = true;
            Console.WriteLine($"Философ {philosopher.Name} кушает");
            philosopher.Eating();
            fork1.IsBusy = false;
            fork2.IsBusy = false;
            semaphore.Release();
        }

        static void Thinking(Philosopher philosopher)
        {
            Console.WriteLine($"Философ {philosopher.Name} думает");
            philosopher.Thinking();
        }

        static void ExitWait()
        {
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}

//Версия без использования примитивов синхронизации
//namespace _2022._08._31_PW
//{
//    public class Philosopher
//    {
//        public string Name { get; set; }

//        public void Eating()
//        {
//            Thread.Sleep(5000);
//        }

//        public void Thinking()
//        {
//            Thread.Sleep(5000);
//        }
//    }
//}

//namespace _2022._08._31_PW
//{
//    internal class Program
//    {
//        static List<Fork> forks = new() { new(), new(), new(), new(), new() };
//        static List<Philosopher> philosophers = new()
//        {
//            new() { Name = "Фалес" },
//            new() { Name = "Анаксимандр" },
//            new() { Name = "Пифагор" },
//            new() { Name = "Эпикур" },
//            new() { Name = "Гераклит Эфесский" }
//        };

//        static void Main()
//        {
//            Task.Run(ExitWait);
//            while (true)
//            {
//                foreach (Philosopher philosopher in philosophers)
//                {
//                    Fork fork1 = null;
//                    Fork fork2 = null;
//                    for (int i = 0; i < forks.Count; i++)
//                    {
//                        if (!forks[i].IsBusy)
//                        {
//                            fork1 = forks[i];
//                            break;
//                        }
//                    }
//                    for (int i = 0; i < forks.Count; i++)
//                    {
//                        if (!forks[i].IsBusy)
//                        {
//                            fork2 = forks[i];
//                            break;
//                        }
//                    }
//                    Task.Run(() => Cycle(philosopher, fork1, fork2));
//                }
//            }
//        }

//        static void Cycle(Philosopher philosopher, Fork fork1, Fork fork2)
//        {
//            Console.WriteLine($"Философ {philosopher.Name} думает");
//            philosopher.Thinking();
//            fork1.IsBusy = true;
//            fork2.IsBusy = true;
//            Console.WriteLine($"Философ {philosopher.Name} кушает");
//            philosopher.Eating();
//            fork1.IsBusy = false;
//            fork2.IsBusy = false;
//            Console.WriteLine($"Философ {philosopher.Name} думает");
//            philosopher.Thinking();
//        }

//        static void ExitWait()
//        {
//            while (true)
//            {
//                if (Console.ReadKey().Key == ConsoleKey.Q)
//                {
//                    Environment.Exit(0);
//                }
//            }
//        }
//    }
//}