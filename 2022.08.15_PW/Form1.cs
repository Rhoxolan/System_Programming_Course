using System.Diagnostics;

namespace _2022._08._15_PW
{
    public partial class Form1 : Form
    {
        List<ListViewItem> listItems;
        private static System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            listItems = new();
            timer = new();
            this.Load += Load_Data;
            numericUpDown1.Value = 10;
            timer.Interval = (int)numericUpDown1.Value * 1000;
            timer.Tick += Load_Data;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("PID", 100);
            listView1.Columns.Add("Process Name", 150);
            listView1.Columns.Add("Start Time", 100);
            listView1.Columns.Add("Threads Count", 100);
            listView1.Columns.Add("Total Processor Time", 150);
            listView1.Columns.Add("Processes Count", 150);
            listView1.View = View.Details;
            timer.Start();
        }

        private void Load_Data(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcesses())
            {
                ListViewItem listViewItem = new(process.Id.ToString());
                listViewItem.SubItems.Add(process.ProcessName);
                try
                {
                    listViewItem.SubItems.Add(process.StartTime.ToString());
                }
                catch
                {
                    listViewItem.SubItems.Add("Access Denied");
                }
                listViewItem.SubItems.Add(process.Threads.Count.ToString());
                try
                {
                    listViewItem.SubItems.Add(process.TotalProcessorTime.ToString());
                }
                catch
                {
                    listViewItem.SubItems.Add("Access Denied");
                }
                listViewItem.SubItems.Add(Process.GetProcessesByName(process.ProcessName).Length.ToString());
                listItems.Add(listViewItem);
            }
            listView1.Items.Clear();
            listView1.Items.AddRange(listItems.ToArray());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer.Interval = (int)numericUpDown1.Value * 1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.GetProcessById(Convert.ToInt32(listView1.SelectedItems[0].Text)).CloseMainWindow();
            Process.GetProcessById(Convert.ToInt32(listView1.SelectedItems[0].Text)).Close();
        }
    }
}