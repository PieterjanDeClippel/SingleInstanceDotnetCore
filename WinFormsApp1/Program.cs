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
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        if (mutex.WaitOne(TimeSpan.FromMilliseconds(10), true))
        {
            var server = new NamedPipeServerStream("pipe-f7f257d8-da2f-4562-8874-84ecc9008f4f", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.CurrentUserOnly | PipeOptions.Asynchronous);
            AcceptConnection(server);

            Application.Run(new Form1());
            server.Dispose();
            mutex.ReleaseMutex();
        }
        else
        {

        }
    }

    private static void AcceptConnection(NamedPipeServerStream server)
    {
        server.BeginWaitForConnection((result) =>
        {
            server.EndWaitForConnection(result);
            var serverState = (NamedPipeServerStream)result.AsyncState!;

            AcceptConnection(serverState);

            var buffer = new byte[bufSize];
            ReadData(serverState, buffer);

        }, server);
    }

    static string text = string.Empty;
    private static void ReadData(NamedPipeServerStream server, byte[] buffer)
    {
        server.BeginRead(buffer, 0, buffer.Length, (result) =>
        {
            var numberOfBytes = server.EndRead(result);
            var bufferState = (byte[])result.AsyncState!;

            text += Encoding.UTF8.GetString(bufferState, 0, numberOfBytes);
            if (text.EndsWith('\0'))
            {
                MessageBox.Show(text);
                text = string.Empty;
            }
            ReadData(server, bufferState);

        }, buffer);
    }
}