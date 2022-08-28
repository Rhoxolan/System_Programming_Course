namespace _2022._08._22_PW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add(ThreadPriority.AboveNormal);
            comboBox1.Items.Add(ThreadPriority.BelowNormal);
            comboBox1.Items.Add(ThreadPriority.Highest);
            comboBox1.Items.Add(ThreadPriority.Lowest);
            comboBox1.Items.Add(ThreadPriority.Normal);
            comboBox1.SelectedValue = ThreadPriority.Normal;
            comboBox1.SelectedIndex = 4;

            comboBox2.Items.Add(ThreadPriority.AboveNormal);
            comboBox2.Items.Add(ThreadPriority.BelowNormal);
            comboBox2.Items.Add(ThreadPriority.Highest);
            comboBox2.Items.Add(ThreadPriority.Lowest);
            comboBox2.Items.Add(ThreadPriority.Normal);
            comboBox2.SelectedValue = ThreadPriority.Normal;
            comboBox2.SelectedIndex = 4;

            comboBox3.Items.Add(ThreadPriority.AboveNormal);
            comboBox3.Items.Add(ThreadPriority.BelowNormal);
            comboBox3.Items.Add(ThreadPriority.Highest);
            comboBox3.Items.Add(ThreadPriority.Lowest);
            comboBox3.Items.Add(ThreadPriority.Normal);
            comboBox3.SelectedValue = ThreadPriority.Normal;
            comboBox3.SelectedIndex = 4;
        }

        //Ты тут. 1)Нужно написать инвокреквайред для текстбоксов, 2)создать потоки для отображения текста на текстбоксе.
    }
}