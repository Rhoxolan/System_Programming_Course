using System.Windows.Forms;

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

        private async void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            await Task.Run(() => { Parallel.For(1, 1000, (i) => { if (IsSimple(i)) { AddToListBox(listBox1, i); } }); });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            int counter = 0;
            Task.WaitAll(Task.Run(() => { Parallel.For((int)numericUpDown1.Value, (int)numericUpDown2.Value, (i) => { if (IsSimple(i)) { AddToListBox(listBox2, i); counter++; } }); }));
            textBox4.Text = counter.ToString();
        }

        private void AddToListBox(ListBox listBox, object value)
        {
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(() => AddToListBox(listBox, value));
            }
            else
            {
                listBox.Items.Add(value);
            }
        }
    }
}