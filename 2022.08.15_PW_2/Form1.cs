using System.ComponentModel;
using System.Diagnostics;

namespace _2022._08._15_PW_2
{
    public partial class Form1 : Form
    {
        BindingList<Process> processes;

        public Form1()
        {
            InitializeComponent();
            processes = new();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = processes;
        }

        private void buttonRunNotepad_Click(object sender, EventArgs e)
        {
            
        }

    }
}