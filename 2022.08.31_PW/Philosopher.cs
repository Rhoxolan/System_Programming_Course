namespace _2022._08._31_PW
{
    public class Philosopher
    {
        public string Name { get; set; }

        public void Eating()
        {
            Thread.Sleep(5000);
        }

        public void Thinking()
        {
            Thread.Sleep(5000);
        }
    }
}
