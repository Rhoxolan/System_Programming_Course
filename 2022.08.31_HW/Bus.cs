namespace _2022._08._31_HW
{
    public class Bus
    {
        private int capacity;

        public string Name { get; set; }

        public Bus(int capacity, string name)
        {
            this.capacity = capacity;
            Name = name;
        }

        public Bus(int capacity)
        {
            this.capacity = capacity;
        }

        public void TakePeople(int People)
        {
            if(capacity > People)
            {
                People = 0;
                return;
            }
            People -= capacity;
        }
    }
}
