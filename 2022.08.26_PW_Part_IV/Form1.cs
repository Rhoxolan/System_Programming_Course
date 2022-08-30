using System.Collections.Generic;
using System.ComponentModel;

namespace _2022._08._26_PW_Part_IV
{
    public partial class Form1 : Form
    {
        Mutex mutexObj1;
        Mutex mutexObj2;
        BindingList<int> list1;
        BindingList<int> list2;
        BindingList<int> list3;

        public Form1()
        {
            InitializeComponent();
            mutexObj1 = new();
            mutexObj2 = new();
            list1 = new();
            list2 = new();
            list3 = new();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = list1;
            listBox2.DataSource = list2;
            listBox3.DataSource = list3;
        }

        private void Print20Symbols()
        {
            mutexObj1.WaitOne();
            try
            {
                for (int i = 0; i < 21; i++)
                {
                    list1.Add(i);
                    Thread.Sleep(10);
                }
            }
            finally
            {
                mutexObj1.ReleaseMutex();
            }
        }

        private void Print10Symbols()
        {
            mutexObj1.WaitOne();
            try
            {
                for (int i = 11 - 1; i >= 0; i--)
                {
                    list2.Add(i);
                    Thread.Sleep(10);
                }
            }
            finally
            {
                mutexObj1.ReleaseMutex();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list1.Clear();
            list2.Clear();
            Thread thread1 = new(Print20Symbols);
            Thread thread2 = new(Print10Symbols);
            thread1.Start();
            Thread.Sleep(1);
            thread2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list3.Clear();
            for (int i = 1; i < 11; i++)
            {
                list3.Add(i);
                Thread.Sleep(1000);
            }
            Task task1 = Task.Run(() => AddRandomValuesToArray(list3));
            Task<(int min, int max)> task2 = Task.Run(() => ReturnMinMaxFromArray(list3));
            textBox1.Text = task2.Result.min.ToString();
            textBox2.Text = task2.Result.max.ToString();
        }

        private void AddRandomValuesToArray(object obj)
        {
            mutexObj2.WaitOne();
            try
            {
                BindingList<int> list = (BindingList<int>)obj;
                Random rand = new();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] += rand.Next(0, 101);
                    Thread.Sleep(100);
                }
            }
            finally
            {
                mutexObj2.ReleaseMutex();
            }
        }

        private (int, int) ReturnMinMaxFromArray(object obj)
        {
            mutexObj2.WaitOne();
            try
            {
                return ((obj as BindingList<int>).Min(), (obj as BindingList<int>).Max());
            }
            finally
            {
                mutexObj2.ReleaseMutex();
            }
        }
    }
}