namespace _2022._08._31_HW
{
    internal class Program
    {
        static AutoResetEvent autoResetEvent = new(true);

        static List<Bus> buses = new()
        {
            new(25) {Name = "Маршрутка 230"},
            new(50) {Name = "Троллейбус 8"},
            new(55) {Name = "Автобус C"}
        };

        static void Main()
        {
            Task.Run(ExitWait);
            int People = 0;
            Random random = new();
            while (true)
            {
                People += random.Next(150);
                Console.WriteLine($"На остановке {People} человек");
                Thread.Sleep(2000);
                List<Task> busStationTasks = new();
                foreach (Bus bus in buses)
                {
                    busStationTasks.Add(Task.Run(()=>TakePeople(bus, ref People)));
                }
                Task.WaitAll(busStationTasks.ToArray());
            }
        }

        static void TakePeople(Bus bus, ref int People)
        {
            autoResetEvent.WaitOne();
            Console.WriteLine($"Приезжает {bus.Name}, забирает {bus.TakePeople(ref People)} людей");
            Thread.Sleep(1000);
            autoResetEvent.Set();
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