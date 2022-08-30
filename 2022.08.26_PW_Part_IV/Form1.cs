using System.Collections.Generic;
using System.ComponentModel;

namespace _2022._08._26_PW_Part_IV
{
    public partial class Form1 : Form
    {
        Mutex mutexObj;
        BindingList<int> list1;
        BindingList<int> list2;

        //Напоминание: попробовать сделать задание 7 через Семафор

        public Form1()
        {
            InitializeComponent();
            mutexObj = new();
            list1 = new();
            list2 = new();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = list1;
            listBox2.DataSource = list2;
        }

        private void Print20Symbols()
        {
            mutexObj.WaitOne();
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
                mutexObj.ReleaseMutex();
            }
        }

        private void Print10Symbols()
        {
            mutexObj.WaitOne();
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
                mutexObj.ReleaseMutex();
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
    }
}