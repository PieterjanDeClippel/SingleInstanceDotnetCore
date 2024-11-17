using System.Collections;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object? sender, EventArgs e)
        {
            var files = Helpers.GetLaunchFiles().ToArray();
            if (files.Any()) lstFiles.Items.AddRange(files);
        }
    }
}
