namespace _2022._08._26_PW_Part_III
{
    internal class Program
    {
        static void Main()
        {
            List<Thread> threads = new();
            for (int i = 0; i < 10; i++)
            {
                threads.Add(new(ShowRandomNumbers) { Name = $"Lists thread {i}" });
            }
            Semaphore semaphore = new(3, 3);
            foreach (Thread thread in threads)
            {
                thread.Start(semaphore);
            }
        }

        static void ShowRandomNumbers(object? obj)
        {
            Semaphore? semaphore = obj as Semaphore;
            semaphore?.WaitOne();
            try
            {
                Random random = new();
                int[] nums = new int[3] { random.Next(0, 101), random.Next(0, 101), random.Next(0, 101) };
                Console.WriteLine($"Поток {Environment.CurrentManagedThreadId} выводит числа {nums[0]}, {nums[1]} и {nums[2]}");
                Thread.Sleep(500);
            }
            finally
            {
                semaphore?.Release();
            }
            
        }
    }
}