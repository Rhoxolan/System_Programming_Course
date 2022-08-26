namespace _2022._08._19_HW
{
    public partial class Form1 : Form
    {
        List<int> fibonacciNumbers;

        public Form1()
        {
            InitializeComponent();
            fibonacciNumbers = new();
            numericUpDown1.Maximum = Decimal.MaxValue;
            numericUpDown2.Maximum = Decimal.MaxValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread showFibonacciNumbersThread = new(ShowFibonacciNumbers);
            (int, int) dizpazone = new((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            fibonacciNumbers.Clear();
            listBox1.Items.Clear();
            showFibonacciNumbersThread.IsBackground = true;
            showFibonacciNumbersThread.Start(dizpazone);
        }

        private void ShowFibonacciNumbers(object obj)
        {
            (int min, int max) = ((int min, int max))obj;

            for (int i = min; i < max; i++)
            {
                if (IsFibonacci(i))
                {
                    fibonacciNumbers.Add(i);
                }
            }
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(new Action(() => RefreshListBox()));
            }
            else
            {
                foreach (int i in fibonacciNumbers)
                {
                    listBox1.Items.Add(i.ToString());
                }
            }
        }

        static bool IsFibonacci(int num)
        {
            List<int> al = new List<int>();
            al.Add(0);
            al.Add(1);
            for (int i = 0; i < num; i++)
            {
                al.Add(al[al.Count - 1] + al[al.Count - 2]);
            }
            if (al.Contains(num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
