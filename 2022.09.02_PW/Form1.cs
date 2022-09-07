using System.Linq;

namespace _2022._09._02_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task task1 = new(() => { textBox1.Text = DateTime.Now.ToString(); });
            Parallel.Invoke(
                task1.Start,
                () => Task.Factory.StartNew(() => { textBox2.Text = DateTime.Now.ToString(); }),
                () => Task.Run(() => { textBox3.Text = DateTime.Now.ToString(); }));
        }

        private bool IsSimple(int num)
        {
            if (num < 2)
            {
                return false;
            }
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            List<int> ints = new();
            Task.Run(() => { Parallel.For(1, 1000, (i) => { if (IsSimple(i)) { ints.Add(i); } }); }).Wait();
            listBox1.DataSource = ints;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.DataSource = null;
            listBox2.Items.Clear();
            int counter = 0;
            List<int> ints = new();
            Task.Run(() =>
            {
                Parallel.For((int)numericUpDown1.Value, (int)numericUpDown2.Value, (i) =>
                {
                    if (IsSimple(i))
                    {
                        ints.Add(i); counter++;
                    }
                });
            }).Wait();
            listBox2.DataSource = ints;
            textBox4.Text = counter.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(0, 10001);
                listBox3.Items.Add(arr[i]);
            }
            Task<double>[] tasks = new Task<double>[4]
            {
                new Task<double>(() => { return arr.Min(); }),
                new Task<double>(() => { return arr.Max(); }),
                new Task<double>(() => { return arr.Average(); }),
                new Task<double>(() => { return arr.Sum(); })
            };
            Parallel.Invoke(tasks[0].Start, tasks[1].Start, tasks[2].Start, tasks[3].Start);
            Task.WaitAll(tasks);
            textBox5.Text = tasks[0].Result.ToString();
            textBox6.Text = tasks[1].Result.ToString();
            textBox7.Text = tasks[2].Result.ToString();
            textBox8.Text = tasks[3].Result.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox4.DataSource = null;
            Random random = new Random();
            int[] arr = new int[10];
            string text = null;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(0, 11);
            }
            Task task1 = new(() =>
            {
                arr = arr.Distinct().ToArray();
            });
            Task task2 = task1.ContinueWith((t) => { Array.Sort(arr); });
            Task task3 = task2.ContinueWith((t) =>
            {
                int index = Array.BinarySearch(arr, (int)numericUpDown3.Value);
                if (index >= 0)
                {
                    text = $"Элемент найден на позиции №{index}.";
                }
                else
                {
                    text = $"Элемент не найден.";
                }
            });
            task1.Start();
            task1.Wait();
            listBox4.DataSource = arr;
            task3.Wait();
            textBox9.Text = text;
        }
    }
}