namespace _2022._08._22_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add(ThreadPriority.Lowest);
            comboBox1.Items.Add(ThreadPriority.BelowNormal);
            comboBox1.Items.Add(ThreadPriority.Normal);
            comboBox1.Items.Add(ThreadPriority.AboveNormal);
            comboBox1.Items.Add(ThreadPriority.Highest);
            comboBox1.SelectedIndex = 2;

            comboBox2.Items.Add(ThreadPriority.Lowest);
            comboBox2.Items.Add(ThreadPriority.BelowNormal);
            comboBox2.Items.Add(ThreadPriority.Normal);
            comboBox2.Items.Add(ThreadPriority.AboveNormal);
            comboBox2.Items.Add(ThreadPriority.Highest);
            comboBox2.SelectedIndex = 2;

            comboBox3.Items.Add(ThreadPriority.Lowest);
            comboBox3.Items.Add(ThreadPriority.BelowNormal);
            comboBox3.Items.Add(ThreadPriority.Normal);
            comboBox3.Items.Add(ThreadPriority.AboveNormal);
            comboBox3.Items.Add(ThreadPriority.Highest);
            comboBox3.SelectedIndex = 2;
        }
        private void AddSymbolToTextBox(TextBox textBox, string symbol)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(() => AddSymbolToTextBox(textBox, symbol)));
            }
            else
            {
                textBox.Text += symbol;
            }
        }
        private void GenerateRandomSymbolString(object obj)
        {
            int choice = (int)obj;
            Random random = new();
            if (choice == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    string symbol = ((char)random.Next(48, 58)).ToString();
                    AddSymbolToTextBox(textBox1, symbol);
                    Thread.Sleep(100);
                }
            }
            if (choice == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    string symbol = ((char)random.Next(97, 123)).ToString();
                    AddSymbolToTextBox(textBox2, symbol);
                    Thread.Sleep(100);
                }
            }
            if (choice == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    string symbol = ((char)random.Next(33, 48)).ToString();
                    AddSymbolToTextBox(textBox3, symbol);
                    Thread.Sleep(100);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            Thread thread = new(GenerateRandomSymbolString);
            thread.Name = "Тред-генератор рандомных чисел";
            thread.Priority = (ThreadPriority)comboBox1.SelectedIndex;
            thread.Start(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            Thread thread = new(GenerateRandomSymbolString);
            thread.Name = "Тред-генератор рандомных букв";
            thread.Priority = (ThreadPriority)comboBox2.SelectedIndex;
            thread.Start(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = String.Empty;
            Thread thread = new(GenerateRandomSymbolString);
            thread.Name = "Тред-генератор рандомных символов";
            thread.Priority = (ThreadPriority)comboBox3.SelectedIndex;
            thread.Start(2);
        }
    }
}