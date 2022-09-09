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
        }
    }
}