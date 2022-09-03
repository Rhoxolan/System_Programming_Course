namespace _2022._08._31_PW
{
    public class Philosopher
    {
        public string Name { get; set; }
        private bool isEating;

        public bool IsEating
        {
            get
            {
                return isEating;
            }
        }

        public void Eating()
        {
            isEating = true;
            Thread.Sleep(5000);
        }

        public void Thinking()
        {
            Thread.Sleep(5000);
        }
    }
}
