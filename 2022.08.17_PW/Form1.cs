using System.Diagnostics;
using System.Management;

namespace _2022._08._17_PW
{
    public partial class Form1 : Form
    {
        //Key = PID, Value = ParentPID
        Dictionary<int, int> processesDictionary;

        public Form1()
        {
            InitializeComponent();
            processesDictionary = new();
        }

        private int GetParentProcessId(int childProcessId)
        {
            int parentId = 0;
            using (ManagementObject obj = new($"win32_process.handle={childProcessId}"))
            {
                try
                {
                    obj.Get();
                    parentId = Convert.ToInt32(obj["ParentProcessId"]);
                }
                catch { }
            }
            return parentId;
        }

        private Dictionary<int, int> GetAllProcesses()
        {
            Dictionary<int, int> keyValuePairs = new();
            foreach (var process in Process.GetProcesses())
            {
                keyValuePairs.Add(process.Id, GetParentProcessId(process.Id));
            }
            return keyValuePairs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            processesDictionary = GetAllProcesses();
            foreach (var pair in processesDictionary)
            {
                try
                {
                    if (pair.Key == pair.Value || !processesDictionary.ContainsKey(pair.Value))
                    {
                        Process process = Process.GetProcessById(pair.Key);
                        TreeNode node = new($"{process.ProcessName} [{process.Id}], parent - {pair.Value}]");
                        foreach (var pair2 in processesDictionary)
                        {
                            Process childProcess = Process.GetProcessById(pair2.Key);
                            if (pair2.Value == pair.Key)
                            {
                                node.Nodes.Add($"{childProcess.ProcessName} [{childProcess.Id}], parent - {process.ProcessName} [{process.Id}]");
                            }
                        }
                        treeView1.Nodes.Add(node);
                    }
                }
                catch { }
            }
        }
    }
}