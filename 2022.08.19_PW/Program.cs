namespace _2022._08._19_PW
{
    internal class Program
    {
        static void Main()
        {
            int t = 0;
            do
            {
                Console.WriteLine("Пожалуйста, выберите задание (1-5), 0 - выход: ");
                t = NumberInput(0, 5);
                Console.Clear();
                if (t > 4)
                {

                }
                else if (t > 3)
                {

                }
                else if (t > 2)
                {

                }
                else if (t > 1)
                {
                    Task2();
                    AnyKey();
                }
                else if (t > 0)
                {
                    Task1();
                    AnyKey();
                }
            } while (t != 0);

        }

        //Задание 1
        static void Task1()
        {
            ThreadStart threadStart = () =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine($"Задание 1, поток {Thread.CurrentThread.ManagedThreadId}, число {i}");
                }
            };

            Thread thread = new(threadStart);
            thread.Start();
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Задание 1, поток {Thread.CurrentThread.ManagedThreadId}, число {i}");
            }
        }

        //Задание 2
        static void Task2()
        {
            ParameterizedThreadStart parameterizedThreadStart = (object obj) =>
            {
                (int, int) tuple = ((int, int))obj;
                for (int i = tuple.Item1; i < tuple.Item2; i++)
                {
                    Console.WriteLine($"Задание 2, поток {Thread.CurrentThread.ManagedThreadId}, имя потока {Thread.CurrentThread.Name} число {i}");
                }
            };

            Thread thread = new(parameterizedThreadStart);
            thread.Name = "Поток со второго задания";
            thread.Start((10, 90));
            for (int i = 10; i < 80; i++)
            {
                Console.WriteLine($"Задание 2, поток {Thread.CurrentThread.ManagedThreadId}, число {i}");
            }
        }

        static void AnyKey()
        {
            Thread.Sleep(100);
            Console.WriteLine("\nPress any key");
            Console.ReadKey();
            Console.Clear();
        }

        public static int NumberInput(int min, int max)
        {
            while (true)
            {
                try
                {
                    string s_number = Console.ReadLine();
                    int number = 0;
                    checked
                    {
                        number = Convert.ToInt32(s_number);
                        if (number < min || number > max)
                        {
                            throw new MyExceptionToString("\nВведено неверное значение!\n");
                        }
                        return number;
                    }
                }
                catch (MyExceptionToString ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch
                {
                    Console.WriteLine("\nНепредвиденная ошибка!\n");
                }
            }
        }
    }

    public class MyExceptionToString : ApplicationException
    {
        public MyExceptionToString(string message) : base(message) { }
    }
}