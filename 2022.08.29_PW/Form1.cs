using System.Threading;
using System.ComponentModel;

namespace _2022._08._29_PW
{
    public partial class Form1 : Form
    {
        List<int> list;
        AutoResetEvent waitHandler;

        public Form1()
        {
            InitializeComponent();
            list = new();
            waitHandler = new(false); // Инициализируем waitHandler в состоянии ожидания. В методе заполнения списка InitializeList() waitHandler будет переведен в сигнальный
                                      // режим, благодаря чему смогут запуститься методы поиска минимума, максимума и среднего значения, в которых прописано ожидание на сигнальный
                                      // режим waitHandler. При повторном срабатывании обработчика button1_Click waitHandler будет опять переведен в режим ожидания, из-за чего методы
                                      // поиска максимума, минимума и среднего значения опять не смогут запуститься раньше чем методе заполнения списка InitializeList().
                                      // Необходимо это для того, чтобы методы поиска минимума, максимума и среднего значения не смогли запуститься первее и перевести waitHandler в состояние
                                      // ожидания, в результате чего метод заполнения списка InitializeList() не начал бы заполнять список, а метод уже пытался бы вернуть
                                      // еще отсутствующее значение.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            waitHandler.Reset();
            Task.Run(InitializeList);
            textBox1.Text = Task.Run(GetMin).Result.ToString();
            textBox2.Text = Task.Run(GetMax).Result.ToString();
            textBox3.Text = Task.Run(GetAverage).Result.ToString();
        }

        private void InitializeList()
        {
            list.Clear();
            Random random = new();
            Thread.Sleep(1000);
            for (int i = 0; i < 1000; i++)
            {
                list.Add(random.Next(0, 5001));
            }
            waitHandler.Set();
            RefreshListBox();
        }

        private int GetMin()
        {
            waitHandler.WaitOne();
            try
            {
                return list.Min();
            }
            finally
            {
                waitHandler.Set();
            }
        }

        private int GetMax()
        {
            waitHandler.WaitOne();
            try
            {
                Thread.Sleep(2000);
                return list.Max();
            }
            finally
            {
                waitHandler.Set();
            }
        }

        private double GetAverage()
        {
            waitHandler.WaitOne();
            try
            {
                return list.Average();
            }
            finally
            {
                waitHandler.Set();
            }
        }

        private void RefreshListBox()
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(RefreshListBox);
            }
            else
            {
                listBox1.DataSource = null;
                listBox1.DataSource = list;
            }
        }

    }
}