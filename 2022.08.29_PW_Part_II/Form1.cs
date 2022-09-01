using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _2022._08._29_PW_Part_II
{
    public partial class Form1 : Form
    {
        AutoResetEvent waitHandler1; //Дополнительно создаём waitHandler для контроля очередности запуска блоков кода в потоках. Подробнее - см. комментарий в Form1.cs 2022.08.29_PW
        AutoResetEvent waitHandler2; //Также см. https://www.cyberforum.ru/csharp-net/thread3017660.html

        int[] arr;

        public Form1()
        {
            InitializeComponent();
            arr = new int[10];
            waitHandler1 = new(false);
            waitHandler2 = new(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            waitHandler1.Reset();
            string path = "Text.txt";
            Task.Run(() => OpenFile(path));
            Task.Run(() => EditFile(path));
        }

        private void OpenFile(string path)
        {
            lock (path)
            {
                string text = File.ReadAllText(path);
                RefreshTextBox(textBox1, text);
                int sentenceCount = text.Split(new string[] { ".", "!", "?", "..." }, StringSplitOptions.RemoveEmptyEntries).Length;
                RefreshTextBox(textBox2, sentenceCount.ToString());
            }
            waitHandler1.Set();
        }

        private void EditFile(string path)
        {
            waitHandler1.WaitOne();
            lock (path)
            {
                StringBuilder sb = new(File.ReadAllText(path));
                for (int i = 0; i < sb.Length; i++)
                {
                    if (sb[i] == '!')
                    {
                        sb[i] = '#';
                    }
                }
                File.WriteAllText(path, sb.ToString());
                string text = File.ReadAllText(path);
                RefreshTextBox(textBox3, text);
            }
            waitHandler1.Set();
        }

        private void RefreshTextBox(TextBox tB, string text)
        {
            if (tB.InvokeRequired)
            {
                tB.Invoke(() => RefreshTextBox(tB, text));
            }
            else
            {
                tB.Text = String.Empty;
                tB.Text = text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText("Text.txt", "Я - ученик Компьютерной Академии Шаг!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            waitHandler2.Reset();
            textBox4.Text = String.Empty;
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(0, 101);
            }
            Task.Run(() => SortArray(arr));
            RefreshListBox();
        }

        private void SortArray(int[] array)
        {
            Monitor.Enter(array);
            try
            {
                Array.Sort(array);
            }
            finally
            {
                Monitor.Exit(array);
                waitHandler2.Set();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Task.Run(() => FindTheNumber(arr));
        }

        private void FindTheNumber(int[] array)
        {
            waitHandler2.WaitOne();
            Monitor.Enter(array);
            try
            {
                foreach (var i in array)
                {
                    if (i == (int)numericUpDown1.Value)
                    {
                        RefreshTextBox(textBox4, "Да");
                        return;
                    }
                }
                RefreshTextBox(textBox4, "Нет");
            }
            finally
            {
                Monitor.Exit(array);
                waitHandler2.Set();
            }
        }

        private void RefreshListBox()
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(RefreshTextBox);
            }
            else
            {
                listBox1.DataSource = null;
                listBox1.DataSource = arr;
            }
        }
    }
}