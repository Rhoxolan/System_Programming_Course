namespace _2022._08._31_PW
{
    public class Philosopher
    {
        public string Name { get; set; }

        public void Eating(Fork fork1, Fork fork2)
        {
            Console.WriteLine($"Философ {Name} кушает");
            //Thread.Sleep(5000);
            fork1.IsBusy = false;
            fork2.IsBusy = false;
        }

        public void Thinking()
        {
            Console.WriteLine($"Философ {Name} думает");
            //Thread.Sleep(5000);
        }
    }
}
