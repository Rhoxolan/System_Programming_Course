using System.Threading;

namespace _2022._08._26_PW
{
    internal class Program
    {
        static Mutex mutexObj = new();
        static Mutex mutexObj2 = new();

        static AutoResetEvent waitHandler = new(false); //Дополнительно создаём waitHandler для контроля очередности запуска блоков кода в потоках. Подробнее - см. комментарий в Form1.cs 2022.08.29_PW
        static AutoResetEvent waitHandler2 = new(false);

        static void Main()
        {
            //Задание 1
            {
                waitHandler.Reset();
                Thread thread1 = new(Print20Symbols);
                Thread thread2 = new(Print10Symbols);
                thread1.Start();
                thread2.Start();
                thread1.Join();
                thread2.Join();
                Console.WriteLine("\n\n");
            }

            //Задание 2,3
            {
                waitHandler2.Reset();
                int[] arr = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Task task1 = Task.Run(() => AddRandomValuesToArray(arr));
                Task<(int min, int max)> task2 = Task.Run(() => ReturnMinMaxFromArray(arr));
                Console.WriteLine($"Min - {task2.Result.min}, Max - {task2.Result.max}");
                Task.WaitAll(new Task[2] { task1, task2 });
                foreach (var item in arr)
                {
                    Console.Write(item.ToString() + " ");
                }
                Console.WriteLine("\n\n");
            }
        }

        static void Print20Symbols()
        {
            //Thread.Sleep(1000);
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
                waitHandler.Set();
            }
        }

        static void Print10Symbols()
        {
            waitHandler.WaitOne();
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
                waitHandler.Set();
            }
        }

        static void AddRandomValuesToArray(object obj)
        {
            //Thread.Sleep(1000);
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
                waitHandler2.Set();
            }
        }

        static (int, int) ReturnMinMaxFromArray(object obj)
        {
            waitHandler2.WaitOne();
            mutexObj2.WaitOne();
            try
            {
                return ((obj as int[]).Min(), (obj as int[]).Max());
            }
            finally
            {
                mutexObj2.ReleaseMutex();
                waitHandler2.Set();
            }
        }
    }
}