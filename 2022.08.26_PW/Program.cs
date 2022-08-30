namespace _2022._08._26_PW
{
    internal class Program
    {
        static Mutex mutexObj = new();
        static Mutex mutexObj2 = new();

        static void Main()
        {
            //Задание 1
            {
                Thread thread1 = new(Print20Symbols);
                Thread thread2 = new(Print10Symbols);
                thread1.Start();
                Thread.Sleep(1);
                thread2.Start();
                thread1.Join();
                thread2.Join();
                Console.WriteLine("\n\n");
            }

            //Задание 2,3
            {
                int[] arr = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Task task1 = Task.Run(() => AddRandomValuesToArray(arr));
                Task<(int min, int max)> task2 = Task.Run(() => ReturnMinMaxFromArray(arr));
                mutexObj2.WaitOne();
                try
                {
                    //Ты тут. Разобраться
                    foreach (var item in arr)
                    {
                        Console.Write(item.ToString() + " ");
                    }
                    Console.WriteLine();
                }
                finally
                {
                    mutexObj2.ReleaseMutex();
                }
                Console.WriteLine($"Min - {task2.Result.min}, Max - {task2.Result.max}");
                Task.WaitAll(new Task[2] { task1, task2 });
                Console.WriteLine("\n\n");
            }
        }

        static void Print20Symbols()
        {
            mutexObj.WaitOne();
            try
            {
                for (int i = 0; i < 21; i++)
                {
                    Console.WriteLine($"Поток {Environment.CurrentManagedThreadId} отображает {i}");
                }
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
        }

        static void Print10Symbols()
        {
            mutexObj.WaitOne();
            try
            {
                for (int i = 11 - 1; i >= 0; i--)
                {
                    Console.WriteLine($"Поток {Environment.CurrentManagedThreadId} отображает {i}");
                }
            }
            finally
            {
                mutexObj.ReleaseMutex();
            }
        }

        static void AddRandomValuesToArray(object obj)
        {
            mutexObj2.WaitOne();
            //Thread.Sleep(1000);
            try
            {
                int[] arr = (int[])obj;
                Random rand = new Random();
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] += rand.Next(0, 101);
                }
            }
            finally
            {
                mutexObj2.ReleaseMutex();
            }
        }

        static (int, int) ReturnMinMaxFromArray(object obj)
        {
            mutexObj2.WaitOne();
            try
            {
                return ((obj as int[]).Min(), (obj as int[]).Max());
            }
            finally
            {
                mutexObj2.ReleaseMutex();
            }
        }
    }
}