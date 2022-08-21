using System.ComponentModel;
using System.Diagnostics;

namespace _2022._08._15_PW_2
{

    public partial class Form1 : Form
    {

        BindingList<MyProcess> processes;

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
            //processes.Add(Process.Start("notepad.exe"));

            MyProcess process = new MyProcess();
            process.StartInfo = new ProcessStartInfo("notepad.exe");
            process.Start();
            processes.Add(process);
            MessageBox.Show(process.ToString());
        }

        private void buttonRunCalculator_Click(object sender, EventArgs e)
        {
            //processes.Add(Process.Start("calc.exe"));
        }

        private void buttonRunPaint_Click(object sender, EventArgs e)
        {
            //processes.Add(Process.Start("mspaint.exe"));
        }

        private void buttonRunSCSCalc_Click(object sender, EventArgs e)
        {
            //processes.Add(Process.Start("SCS-Calc.exe"));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                processes[listBox1.SelectedIndex].CloseMainWindow();
                processes[listBox1.SelectedIndex].Close();
                processes.RemoveAt(listBox1.SelectedIndex);
            }
        }
    }

    public class MyProcess : Process
    {
        public override string ToString()
        {
            return this.ProcessName;
        }
    }
}