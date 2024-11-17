using System;
using System.IO.Pipes;
using System.Text;

namespace WinFormsApp1;

internal static class Program
{
    static Mutex mutex = new Mutex(true, "f7f257d8-da2f-4562-8874-84ecc9008f4f");
    const int bufSize = 20;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Form1? mainForm;
        if (mutex.WaitOne(TimeSpan.FromMilliseconds(10), true))
        {
            mainForm = new Form1();
            StartServer(mainForm);
            Application.Run(mainForm);
            //server.Dispose();
            mutex.ReleaseMutex();
        }
        else
        {
            var files = Helpers.GetLaunchFiles();
            if (files.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, files));
                //var fileStr = string.Join("\0\0\0\0\0", files);
                //var stream = new NamedPipeClientStream("pipe-f7f257d8-da2f-4562-8874-84ecc9008f4f");
                //await stream.ConnectAsync();

                //var message = Encoding.UTF8.GetBytes(fileStr).Concat(Enumerable.Range(0, 10).Select(_ => (byte)0)).ToArray();
                //await stream.WriteAsync(message, 0, message.Length);
            }
        }
    }

    private static void StartServer(Form1 mainForm)
    {
        // One client = one server. So we need to create a new server-pipe when the former is connected
        var server = new NamedPipeServerStream("pipe-f7f257d8-da2f-4562-8874-84ecc9008f4f", PipeDirection.InOut, 100, PipeTransmissionMode.Message, PipeOptions.CurrentUserOnly | PipeOptions.Asynchronous);
        server.BeginWaitForConnection((result) =>
        {
            server.EndWaitForConnection(result);
            var serverState = (NamedPipeServerStream)result.AsyncState!;

            StartServer(mainForm);

            var buffer = new byte[bufSize];
            var completeBuffer = new byte[0];
            ReadData(serverState, buffer, completeBuffer, mainForm);

        }, server);
    }

    private static void ReadData(NamedPipeServerStream server, byte[] readbuffer, byte[] completeBuffer, Form1 mainform)
    {
        server.BeginRead(readbuffer, 0, readbuffer.Length, (result) =>
        {
            var numberOfBytes = server.EndRead(result);
            var bufferState = (byte[])result.AsyncState!;

            //text += Encoding.UTF8.GetString(bufferState, 0, numberOfBytes);
            completeBuffer = completeBuffer.Concat(bufferState).ToArray();

            if (completeBuffer.TakeLast(10).All(c => c == 0))
            {
                var text = Encoding.UTF8.GetString(completeBuffer, 0, completeBuffer.Length - 10);

                var files = text.Split("\0\0\0\0\0", StringSplitOptions.RemoveEmptyEntries);
                mainform.lstFiles.Items.AddRange(files);

                completeBuffer = new byte[0];
            }
            ReadData(server, bufferState, completeBuffer, mainform);

        }, readbuffer);
    }
}