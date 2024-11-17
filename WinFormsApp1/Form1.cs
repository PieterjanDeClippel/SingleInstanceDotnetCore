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
            var variables = GetEnvironmentVariables();
            foreach (var variable in variables.Where(v => v.Key.StartsWith("ClickOnce_ActivationData_")))
            {
                MessageBox.Show($"{variable.Key} = {variable.Value}");
            }
        }

        private Dictionary<string, object> GetEnvironmentVariables()
        {
            var env = (Hashtable)Environment.GetEnvironmentVariables();
            var envKeys = new string[env.Count];
            env.Keys.CopyTo(envKeys, 0);
            return envKeys.ToDictionary(k => k, k => env[k]);
        }
    }
}
