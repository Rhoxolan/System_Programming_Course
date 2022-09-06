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
            task1.Start(); //Подумать, может выйдет все это запустить через паралел.
        }
    }
}