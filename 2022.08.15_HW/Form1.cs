using System.Diagnostics;

namespace _2022._08._15_HW
{
    public partial class Form1 : Form
    {
        Process process;
        public Form1()
        {
            InitializeComponent();
            process = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            process = Process.Start(new ProcessStartInfo(textBox1.Text, $"{numericUpDown1.Value} {comboBox1.Text} {numericUpDown2.Value}"));
        }
    }
}