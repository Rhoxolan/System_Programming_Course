using System.Diagnostics;

namespace _2022._08._15_PW
{
    public partial class Form1 : Form
    {
        List<ListViewItem> listItems;

        public Form1()
        {
            InitializeComponent();
            listItems = new();
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
            listView1.Items.AddRange(listItems.ToArray());
        }
    }
}