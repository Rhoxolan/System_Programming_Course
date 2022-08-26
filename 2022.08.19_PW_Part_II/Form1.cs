namespace _2022._08._19_PW_Part_II
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = (int)numericUpDown1.Value;
            List<Thread> threads = new();
            for (int i = 0; i < count; i++)
            {
                threads.Add(new Thread(() => { MessageBox.Show($"Сообщение из потока {Thread.CurrentThread.ManagedThreadId}"); }));
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int minVal = 0;
            int maxVal = 0;
            double avgVal = 0;
            List<int> numbers = new();
            Random random = new();
            for (int i = 0; i < 100000; i++)
            {
                numbers.Add(random.Next(0, 100000));
            }

            Thread serachMinThread = new((object obj) => {
                minVal = (obj as List<int>).Min();
            });
            serachMinThread.Name = "Поток поиска минимума";
            Thread serachMaxThread = new((object obj) => {
                maxVal = (obj as List<int>).Max();
            });
            serachMaxThread.Name = "Поток поиска масксимума";
            Thread serachAvgThread = new((object obj) => {
                avgVal = (obj as List<int>).Average();
            });
            serachAvgThread.Name = "Поток поиска среднего числа";

            serachMinThread.Start(numbers);
            serachMaxThread.Start(numbers);
            serachAvgThread.Start(numbers);

            Thread.Sleep(500);
            File.WriteAllText("Text.txt", $"Мин {minVal}, макс {maxVal}, среднее {avgVal}");
        }
    }
}