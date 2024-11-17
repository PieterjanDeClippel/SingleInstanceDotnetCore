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
            var variables = Environment.GetEnvironmentVariables();
            foreach (var variable in variables)
            {
                MessageBox.Show(variable.ToString());
            }
        }
    }
}
