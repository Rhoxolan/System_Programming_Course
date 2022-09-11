using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Windows.Forms;

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
                var uniquelist = (from i in list.AsParallel()
                                  where IsUnique(list, i)
                                  select i).ToList();
                textBox1.Text = uniquelist.Count.ToString();
            }
        }

        private bool IsUnique(List<int> list, int i)
        {
            Thread.Sleep(1000); //Исскуственное замедление для отладки - если метод на запрос не параллельный, мы будем последователньо ждать выполнения Thread.Sleep
            int counter = 0;
            foreach (int item in list)
            {
                if (item == i)
                {
                    counter++;
                }
            }
            if (counter == 1)
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
                var uniquelist = (from i in list.AsParallel().AsOrdered() select i).ToList();
                List<int> counts = new() { 1 };
                for (int i = 1; i < uniquelist.Count; i++)
                {
                    if (uniquelist[i] > uniquelist[i - 1] && uniquelist[i - 1] > 0)
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

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Multiselect = true;
            ofd.Filter = "Text Files(*.txt)|*.txt";
            List<string> pathes = new();
            List<Candidate> candidates = new();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pathes = ofd.FileNames.AsParallel().Select(i => i).ToList();
                foreach (string path in pathes)
                {
                    List<string> lines = File.ReadLines(path).AsParallel().ToList();
                    candidates.Add(new() { Name = lines[0], City = lines[1], Age = Convert.ToInt32(lines[2]), Years = Convert.ToInt32(lines[3]), Salary = Convert.ToInt32(lines[4]) });
                }
                textBox4.Text = GetCandidateWithMaxExperience(candidates);
                textBox5.Text = GetCandidateWithMinExperience(candidates);
                listBox1.DataSource = null;
                listBox1.DataSource = GetCandidatesFromOneCity(candidates, "Кривой Рог");
                textBox7.Text = GetCandidateWithMinExpectedSalary(candidates);
                textBox6.Text = GetCandidateWithMaxExpectedSalary(candidates);
            }
        }

        private List<string> GetCandidatesFromOneCity(List<Candidate> candidates, string city)
        {
            return candidates.AsParallel().Where(c => c.City == city).Select(c => c.Name).ToList();

        }

        private string GetCandidateWithMaxExperience(List<Candidate> candidates)
        {
            int maxExperience = candidates.AsParallel().Select(c => c.Years).Max();
            foreach (Candidate candidate in candidates)
            {
                if(candidate.Years == maxExperience)
                {
                    return candidate.Name;
                }
            }
            return String.Empty;
        }

        private string GetCandidateWithMinExperience(List<Candidate> candidates)
        {
            int MinExperience = candidates.AsParallel().Select(c => c.Years).Min();
            foreach (Candidate candidate in candidates)
            {
                if (candidate.Years == MinExperience)
                {
                    return candidate.Name;
                }
            }
            return String.Empty;
        }

        private string GetCandidateWithMaxExpectedSalary(List<Candidate> candidates)
        {
            int maxExpected = candidates.AsParallel().Select(c => c.Salary).Max();
            foreach (Candidate candidate in candidates)
            {
                if (candidate.Salary == maxExpected)
                {
                    return candidate.Name;
                }
            }
            return String.Empty;
        }

        private string GetCandidateWithMinExpectedSalary(List<Candidate> candidates)
        {
            int minExpected = candidates.AsParallel().Select(c => c.Salary).Min();
            foreach (Candidate candidate in candidates)
            {
                if (candidate.Salary == minExpected)
                {
                    return candidate.Name;
                }
            }
            return String.Empty;
        }
    }
}