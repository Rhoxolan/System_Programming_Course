using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _2022._08._29_PW_Part_II
{
    public partial class Form1 : Form
    {
        int[] arr;

        public Form1()
        {
            InitializeComponent();
            arr = new int[10];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "Text.txt";
            Task.Run(() => OpenFile(path)); //Подумать, как можно исправить момент с запуском сразу другого потока
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
        }

        private void EditFile(string path)
        {
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
        }

        private void RefreshTextBox(TextBox tB, string text)
        {
            if (tB.InvokeRequired)
            {
                tB.Invoke(()=>RefreshTextBox(tB, text));
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
            textBox4.Text = String.Empty;
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(0, 101);
            }
            Task.Run(()=> SortArray(arr));
            RefreshListBox();
        }

        private void SortArray(int[] arr)
        {
            Array.Sort(arr);
        }

        //Прочитать за монитор и сделать задание

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var i in arr)
            {
                if(i == (int)numericUpDown1.Value)
                {
                    textBox4.Text = "Да";
                    return;
                }
            }
            textBox4.Text = "Нет";
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