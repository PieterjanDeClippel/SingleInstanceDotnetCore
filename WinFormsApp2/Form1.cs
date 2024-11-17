using System.IO;
using System.IO.Pipes;
using System.Text;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var stream = new NamedPipeClientStream("pipe-f7f257d8-da2f-4562-8874-84ecc9008f4f");
            await stream.ConnectAsync();

            var messageText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            var message = Encoding.UTF8.GetBytes($"{messageText}\0");

            stream.BeginWrite(message, 0, message.Length, (asyncState) => MessageBox.Show("Message sent"), stream);
        }
    }
}
