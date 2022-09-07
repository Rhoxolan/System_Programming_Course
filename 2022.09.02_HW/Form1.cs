using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace _2022._09._02_HW
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts;
        CancellationToken cancellationToken;
        OutputSettings outputSettings;

        public Form1()
        {
            InitializeComponent();
            outputSettings = new();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            cts = new();
            cancellationToken = cts.Token;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox2.Text = String.Empty;
            int sentenseAmount = 0;
            int characterAmount = 0;
            int wordsAmount = 0;
            int interrogativeSentensesAmount = 0;
            int imperativeSentenseAmount = 0;
            Task task1 = new(() => TaskRuner(ref sentenseAmount, textBox1.Text.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).Length, cancellationToken));
            Task task2 = new(() => TaskRuner(ref characterAmount, textBox1.Text.Length, cancellationToken));
            Task task3 = new(() => TaskRuner(ref wordsAmount, textBox1.Text.Split(" ").Length, cancellationToken));
            Task task4 = new(() => TaskRuner(ref interrogativeSentensesAmount, textBox1.Text.Split("?").Length, cancellationToken));
            Task task5 = new(() => TaskRuner(ref imperativeSentenseAmount, textBox1.Text.Split("!").Length, cancellationToken));
            Parallel.Invoke(task1.Start, task2.Start, task3.Start, task4.Start, task5.Start);
            Task.Run(() =>
            {
                Task.WaitAll(task1, task2, task3, task4, task5);
                if (cts.IsCancellationRequested)
                {
                    RefreshTextBox(textBox1, "Операция прервана");
                    return;
                }
                if (outputSettings.IsSentenseAmount)
                {
                    RefreshTextBox(textBox4, sentenseAmount.ToString());
                }
                if (outputSettings.IsCharacterAmount)
                {
                    RefreshTextBox(textBox3, characterAmount.ToString());
                }
                if (outputSettings.IsWordsAmount)
                {
                    RefreshTextBox(textBox2, wordsAmount.ToString());
                }
                if (outputSettings.IsInterrogativeSentensesAmount)
                {
                    RefreshTextBox(textBox5, interrogativeSentensesAmount.ToString());
                }
                if (outputSettings.IsImperativeSentenseAmount)
                {
                    RefreshTextBox(textBox6, imperativeSentenseAmount.ToString());
                }
            });
        }

        private void TaskRuner(ref int value, int amount, CancellationToken cts)
        {
            Thread.Sleep(3000);
            value = amount;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void RefreshTextBox(TextBox textBox, string Text)
        {
            if(textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(()=>RefreshTextBox(textBox, Text)));
            }
            else
            {
                textBox.Text = Text;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                outputSettings.IsSentenseAmount = true;
            }
            if (!checkBox1.Checked)
            {
                outputSettings.IsSentenseAmount = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                outputSettings.IsCharacterAmount = true;
            }
            if (!checkBox3.Checked)
            {
                outputSettings.IsCharacterAmount = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                outputSettings.IsWordsAmount = true;
            }
            if (!checkBox4.Checked)
            {
                outputSettings.IsWordsAmount = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                outputSettings.IsInterrogativeSentensesAmount = true;
            }
            if (!checkBox5.Checked)
            {
                outputSettings.IsInterrogativeSentensesAmount = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                outputSettings.IsImperativeSentenseAmount = true;
            }
            if (!checkBox2.Checked)
            {
                outputSettings.IsImperativeSentenseAmount = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder text = new();
            if (outputSettings.IsSentenseAmount)
            {
                text.AppendLine(textBox4.Text);
            }
            if (outputSettings.IsCharacterAmount)
            {
                text.AppendLine(textBox3.Text);
            }
            if (outputSettings.IsWordsAmount)
            {
                text.AppendLine(textBox2.Text);
            }
            if (outputSettings.IsInterrogativeSentensesAmount)
            {
                text.AppendLine(textBox5.Text);
            }
            if (outputSettings.IsImperativeSentenseAmount)
            {
                text.AppendLine(textBox6.Text);
            }
            SaveFileDialog sfd = new();
            sfd.Filter = "Текстовые документы(*.txt)|*.txt";
            sfd.FileName = $"Вывод.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new(sfd.FileName, FileMode.Create);
                using (StreamWriter sw = new(fs))
                {
                    sw.WriteLine(text.ToString());
                }
            }
        }
    }
}