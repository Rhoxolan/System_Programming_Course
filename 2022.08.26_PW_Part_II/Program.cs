namespace _2022._08._26_PW_Part_II
{
    internal class Program
    {

        static void Main()
        {
            _ = new Mutex(true, "Opener", out bool createdNew);
            if (!createdNew)
            {
                Console.WriteLine("Приложение уже открыто! Нажмите любую клавишу для выхода.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }
    }
}