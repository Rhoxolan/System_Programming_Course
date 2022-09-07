using System.Diagnostics.Metrics;
using System.Linq;

namespace _2022._09._02_HW_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += (s, e) => Task.Run(Mover);
        }

        private void Mover()
        {
            foreach(string file in Directory.GetFiles(textBox1.Text))
            {
                if(!Directory.GetFiles(textBox2.Text).Contains(Path.GetFileName(file)))
                {
                    File.Move(file, textBox2.Text + $"/{Path.GetFileName(file)}");
                    textBox1.Text += $"Фаил {Path.GetFileName(file)} c директории {Path.GetDirectoryName(file)} успешно перемещено по новому адресу " +
                        $"{textBox2.Text}{Path.GetFileName(file)}"; //Проверить
                }
            }
        }
    }
}