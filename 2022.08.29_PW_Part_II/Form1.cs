using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace _2022._08._29_PW_Part_II
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
    }
}