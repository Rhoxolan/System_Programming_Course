namespace _2022._08._26_PW
{
    internal class Program
    {
        static Mutex mutexObj = new();

        static void Main()
        {
            //Задание 1
            {
                Thread thread1 = new(Print20Symbols);
                Thread thread2 = new(Print10Symbols);
                thread1.Start();
                Thread.Sleep(1);
                thread2.Start();
                Console.WriteLine("\n\n"); //Посмотреть как решал это
            }

            //Задание 2
            {

            }
        }

        static void Print20Symbols()
        {
            mutexObj.WaitOne();
            for (int i = 0; i < 21; i++)
            {
                Console.WriteLine($"Поток {Environment.CurrentManagedThreadId} отображает {i}");
            }
            mutexObj.ReleaseMutex();
        }

        static void Print10Symbols()
        {
            mutexObj.WaitOne();
            for (int i = 11 - 1; i >= 0; i--)
            {
                Console.WriteLine($"Поток {Environment.CurrentManagedThreadId} отображает {i}");
            }
            mutexObj.ReleaseMutex();
        }
    }
}