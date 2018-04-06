using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSenderGUI
{
    public partial class Window : Form
    {
        string filename;
        FileInfo fileinfo;
        public string checksum;
        TcpClient tcpclient;
        Thread connectthread;

        public Window()
        {
            InitializeComponent();
            //get settings
            settings_serverAddressInput.Text = Properties.Settings.Default.ServerAddress;
            settings_serverPortInput.Value = Properties.Settings.Default.ServerPort;
            settings_listenPortInput.Value = Properties.Settings.Default.ListenPort;
            //define threads
            connectthread = new Thread(send);
        }
        public void send_updateStatus(string status)
        {
            //append the string to a newline and automatically scroll down to the bottom.
            this.InvokeEx(f => send_Status.Text += "\r\n" + status);
            this.InvokeEx(f => send_Status.SelectionStart = send_Status.Text.Length);
            this.InvokeEx(f => send_Status.ScrollToCaret());
        }

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            //select file
            send_fileSelect.ShowDialog();
            filename = send_fileSelect.FileName;

            //if a file was selected, generate checksum of file and get fileinfo.
            if (filename != "")
            {
                fileinfo = new FileInfo(filename);
                send_updateStatus("Computing file checksum...");
                checksum = checkMD5(filename);
                send_updateStatus("File selected.");
            }
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            //check that a file has been selected
            if (filename == null) { send_updateStatus("Error: No file selected"); }
            else
            {
                //check if the send thread is already running. if so, abort the thread. if not, start the thread.
                if (connectthread.IsAlive)
                {
                    //stop
                    connectthread.Abort();
                    send_sendButton.Text = "Send";
                }
                else
                {
                    //start
                    try
                    {
                        //check if thread died; if so dispose the old, useless one and create a new one.
                        if (connectthread.ThreadState == ThreadState.Stopped || connectthread.ThreadState == ThreadState.Aborted)
                        {
                            connectthread = new Thread(send);
                        }

                        send_updateStatus("Starting thread...");
                        connectthread.Start();
                        send_sendButton.Text = "Cancel";
                    }
                    catch (Exception err)
                    {
                        send_updateStatus("Error starting send thread: " + err.Message);
                        send_sendButton.Text = "Send";
                    }
                }
            }
        }
        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            //save settings to appdata
            Properties.Settings.Default.ServerAddress = settings_serverAddressInput.Text;
            Properties.Settings.Default.ServerPort = Convert.ToInt32(settings_serverPortInput.Value);
            Properties.Settings.Default.ListenPort = Convert.ToInt32(settings_listenPortInput.Value);
            Properties.Settings.Default.Save();
        }

        public void send()
        {
            send_updateStatus("Connecting...");
            try
            {
                clientdisconnect();
                //create new client object
                tcpclient = new TcpClient();

                //connect to the server 
                tcpclient.Connect(Properties.Settings.Default.ServerAddress, Properties.Settings.Default.ServerPort);
                send_updateStatus("Connected. Waiting for remote approval...");

                //communication protocol handler 
                tcpclient.Client.Send(Encoding.ASCII.GetBytes("REQUEST_FILE_SEND: SIZE="+fileinfo.Length+" NAME="+fileinfo.Name+"\n"));
                //read the response from the client to determine whether to send the file or not
                string data;
                //while socket is connected:
                try
                {
                    for (; ; )
                    {
                        var data_bytes = methods.ReceiveAll(tcpclient.Client);
                        data = Encoding.UTF8.GetString(data_bytes, 0, data_bytes.Length);
                        if (data.Length != 0)
                        {
                            //remove pesky return at the end
                            data = data.Remove(data.Length - 1, 1);
                            //we now have the response; exit the loop.
                            break;
                        }
                        if (!methods.IsConnected(tcpclient.Client))
                        {
                            send_updateStatus("Server disconnected.");
                            tcpclient.Dispose();
                            return;
                        }
                    }
                }
                catch
                {
                    send_updateStatus("Error receiving response from server.");
                    tcpclient.Dispose();
                    return;
                }
                
                //have they accepted the file?
                if (data == "FILE_ACCEPT")
                {
                    var readed = -1;
                    var buffer = new Byte[4096];
                    using (var networkStream = new BufferedStream(new NetworkStream(tcpclient.Client, false)))
                    using (var fileStream = fileinfo.OpenRead())
                    {
                        while (readed != 0)
                        {
                            readed = fileStream.Read(buffer, 0, buffer.Length);
                            networkStream.Write(buffer, 0, readed);
                            send_updateStatus("Copied " + readed);
                        }
                        networkStream.FlushAsync();
                    }
                }

                //at this point we are disconnected.
                this.InvokeEx(f => send_sendButton.Text = "Send");
                send_updateStatus("Disconnected.");
            }
            catch (Exception err)
            {
                send_updateStatus("Error connecting: " + err.Message);
                this.InvokeEx(f => send_sendButton.Text = "Send");
            }
        }
        public void clientdisconnect()
        {
            //attempt to disconnect the connection
            try
            {
                tcpclient.Close();
                tcpclient.Dispose();
            }
            catch { }
        }

        public async Task ReceiveFile(Socket socket, FileInfo file)
        {
            var readed = -1;
            var buffer = new Byte[4096];
            using (var fileStream = file.OpenWrite())
            using (var networkStream = new NetworkStream(socket, false))
            {
                while (readed != 0)
                {
                    readed = networkStream.Read(buffer, 0, buffer.Length);
                    fileStream.Write(buffer, 0, readed);
                    Console.WriteLine("Copied " + readed);
                }
            }
        }

        public string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
    public static class methods
    {
        public static bool IsConnected(this Socket socket)
        {
            try { return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0); }
            catch (SocketException) { return false; }
        }
        public static byte[] ReceiveAll(this Socket socket)
        {
            var buffer = new List<byte>();
            while (socket.Available > 0)
            {
                var currByte = new Byte[1];
                var byteCounter = socket.Receive(currByte, currByte.Length, SocketFlags.None);

                if (byteCounter.Equals(1)) { buffer.Add(currByte[0]); }
            }
            return buffer.ToArray();
        }
    }
    public static class ISynchronizeInvokeExtensions
    {
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                action(@this);
            }
        }
    }
}
