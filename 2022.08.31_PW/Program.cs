namespace _2022._08._31_PW
{
    internal class Program
    {
        static Semaphore semaphore = new(2,2);
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
            while (true)
            {
                List<Task> tasks = new();
                semaphore.WaitOne();
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
                    if (fork1 != null && fork2 != null)
                    {
                        tasks.Add(Task.Run(() => Cycle(philosopher, fork1, fork2)));
                    }
                }
                Task.WaitAll(tasks.ToArray());
                semaphore.Release();
            }
        }

        static void Cycle(Philosopher philosopher, Fork fork1, Fork fork2)
        {
            Console.WriteLine($"Философ {philosopher.Name} думает");
            philosopher.Thinking();
            fork1.IsBusy = true;
            fork2.IsBusy = true;
            Console.WriteLine($"Философ {philosopher.Name} кушает");
            philosopher.Eating();
            fork1.IsBusy = false;
            fork2.IsBusy = false;
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