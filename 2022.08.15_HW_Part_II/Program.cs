namespace _2022._08._15_HW_Part_II
{
    internal class Program
    {
        static void Main(string[] args)
        {
            switch (args[1])
            {
                case "+":
                    Console.WriteLine(Convert.ToInt32(args[0]) + Convert.ToInt32(args[2]));
                    break;
                case "-":
                    Console.WriteLine(Convert.ToInt32(args[0]) - Convert.ToInt32(args[2]));
                    break;
                case "*":
                    Console.WriteLine(Convert.ToInt32(args[0]) * Convert.ToInt32(args[2]));
                    break;
                case "/":
                    Console.WriteLine(Convert.ToInt32(args[0]) * Convert.ToInt32(args[2]));
                    break;
            }

            Console.ReadKey();
        }
    }
}