//Решение заключается в ограничении количества человек, которые могут подойти к месту, где лежат вилки. Это решение уходит
//от максималньного быстродействия, но гарантирует, что все философы рано или поздно смогут поесть.

namespace _2022._08._31_PW
{
    internal class Program
    {
        static object locker = new();
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
            List<Task> tasksCycles = new();
            foreach(Philosopher philosopher in philosophers)
            {
                tasksCycles.Add(new(()=> Cycle(philosopher)));
            }
            foreach(Task task in tasksCycles)
            {
                task.Start();
            }
            Task.WaitAll(tasksCycles.ToArray());
        }

        static void Cycle(Philosopher philosopher)
        {
            while (true)
            {
                philosopher.Thinking();
                bool goEat = false;
                Fork leftFork = null;
                Fork rightFork = null;
                lock (locker)  //К выдаче вилок подходит может подойти только 1 человек!
                {
                    foreach (Fork fork1 in forks) //Ищем первую вилку
                    {
                        if (!fork1.IsBusy) //Если есть свободная вилка
                        {
                            fork1.IsBusy = true; //Берём её
                            foreach (Fork fork2 in forks) //Ищем вторую вилку
                            {
                                if (!fork2.IsBusy) //Если есть свободная вилка
                                {
                                    fork2.IsBusy = true; //Берём её
                                    goEat = true; //Идём кушать
                                    leftFork = fork1;
                                    rightFork = fork2;
                                    break;
                                }
                            }
                            fork1.IsBusy = false; //Если не была найдена вторая вилка - ложим вилку и останавливаем цикл
                            break;
                        }
                    }
                }
                if (goEat) //А вот кушать за общим столом могут все вместе!
                {
                    philosopher.Eating(leftFork, rightFork); //Вилки ложаться обратно внутри класса
                }
            }
        }

        static void ExitWait()
        {
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.End)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}