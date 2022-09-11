using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace _2022._09._05_PW
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> list = new();
            OpenFileDialog ofd = new();
            ofd.Filter = "Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string line in File.ReadLines(ofd.FileName))
                {
                    list.Add(Convert.ToInt32(line));
                }
            }
            var uniquelist = (from i in list.AsParallel()
                              where IsUnique(list, i)
                              select i).ToList();
            textBox1.Text = uniquelist.Count.ToString();
        }

        private bool IsUnique(List<int> list, int i)
        {
            Thread.Sleep(1000); //Исскуственное замедление для отладки - если метод на запрос не параллельный, мы будем последователньо ждать выполнения Thread.Sleep
            int counter = 0;
            foreach (int item in list)
            {
                if(item == i)
                {
                    counter++;
                }
            }
            if(counter == 1)
            {
                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> list = new();
            OpenFileDialog ofd = new();
            ofd.Filter = "Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string line in File.ReadLines(ofd.FileName))
                {
                    list.Add(Convert.ToInt32(line));
                }
            }
            var uniquelist = (from i in list.AsParallel().AsOrdered() select i).ToList();
            List<int> counts = new() { 1 };
            for (int i = 1; i < uniquelist.Count; i++)
            {
                if (uniquelist[i] > uniquelist[i-1] && uniquelist[i - 1] > 0)
                {
                    counts[^1]++;
                }
                else
                {
                    counts.Add(1);
                }
            }
            textBox2.Text = counts.Max().ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<int> list = new();
            OpenFileDialog ofd = new();
            ofd.Filter = "Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string line in File.ReadLines(ofd.FileName))
                {
                    list.Add(Convert.ToInt32(line));
                }
            }
            var uniquelist = list.AsParallel().AsOrdered().Select(i => i).ToList(); //Method syntax
            List<int> counts = new() { 0 };
            for (int i = 0; i < uniquelist.Count; i++)
            {
                if (uniquelist[i] > 0)
                {
                    counts[^1]++;
                }
                else
                {
                    counts.Add(0);
                }
            }
            textBox3.Text = counts.Max().ToString();
        }
    }
}