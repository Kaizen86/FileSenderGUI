using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSenderGUI
{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
            //get settings
            settings_serverAddressInput.Text = Properties.Settings.Default.ServerAddress;
            settings_serverPortInput.Value = Properties.Settings.Default.ServerPort;
            settings_listenPortInput.Value = Properties.Settings.Default.ListenPort;
            //define threads
            send_connectthread = new Thread(send);
        }

        //send tab
        string send_filename;
        FileInfo send_fileinfo;
        string send_hash;
        TcpClient send_tcpclient;
        Thread send_connectthread;
        Thread send_checksum;
        void send_updateStatus(string status)
        {
            //append the string to a newline and automatically scroll down to the bottom.
            this.InvokeEx(f => send_Status.Text += "\r\n" + status);
            this.InvokeEx(f => send_Status.SelectionStart = send_Status.Text.Length);
            this.InvokeEx(f => send_Status.ScrollToCaret());
        }
        private void send_fileSelectButton_Click(object sender, EventArgs e)
        {
            //select file
            send_fileSelect.ShowDialog();
            send_filename = send_fileSelect.FileName;

            //if a file was selected, generate checksum of file and get fileinfo.
            if (send_filename != "")
            {
                send_fileinfo = new FileInfo(send_filename);
                send_updateStatus("Computing file checksum...");
                send_checksum = new Thread(send_computehash);
                send_checksum.Start();
                send_checksum.Join();
                send_updateStatus("File selected.");
            }
        }
        void send_computehash()
        {
            send_hash = methods.checkMD5(send_filename);
        }
        private void send_sendButton_Click(object sender, EventArgs e)
        {
            //check that a file has been selected
            if (send_filename == null) { send_updateStatus("Error: No file selected"); }
            else
            {
                //check if the send thread is already running. if so, abort the thread. if not, start the thread.
                if (send_connectthread.IsAlive)
                {
                    //stop
                    send_connectthread.Abort();
                    send_sendButton.Text = "Send";
                }
                else
                {
                    //start
                    try
                    {
                        //check if thread died; if so dispose the old, useless one and create a new one.
                        if (send_connectthread.ThreadState == ThreadState.Stopped || send_connectthread.ThreadState == ThreadState.Aborted)
                        {
                            send_connectthread = new Thread(send);
                        }
                        send_connectthread.Start();
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
        void send()
        {
            send_updateStatus("Connecting...");
            try
            {
                clientdisconnect();
                //create new client object
                send_tcpclient = new TcpClient();

                //connect to the server 
                send_tcpclient.Connect(Properties.Settings.Default.ServerAddress, Properties.Settings.Default.ServerPort);
                send_updateStatus("Connected. Waiting for remote approval...");

                //communication protocol handler 
                send_tcpclient.Client.Send(Encoding.ASCII.GetBytes(send_fileinfo.Length + "," + send_hash + "," + send_fileinfo.Name + "\n"));
                //read the response from the client to determine whether to send the file or not
                string data;
                //while socket is connected:
                try
                {
                    for (; ; )
                    {
                        var data_bytes = methods.ReceiveAll(send_tcpclient.Client);
                        data = Encoding.UTF8.GetString(data_bytes, 0, data_bytes.Length);
                        if (data.Length != 0)
                        {
                            //remove pesky return at the end
                            data = data.Remove(data.Length - 1, 1);
                            //we now have the response; exit the loop.
                            break;
                        }
                        if (!methods.IsConnected(send_tcpclient.Client))
                        {
                            send_updateStatus("Server disconnected.");
                            this.InvokeEx(f => send_sendButton.Text = "Send");
                            send_tcpclient.Dispose();
                            return;
                        }
                    }
                }
                catch
                {
                    send_updateStatus("Error receiving response from server.");
                    try { send_tcpclient.Close(); } catch { }
                    send_tcpclient.Dispose();
                    return;
                }

                //have they accepted the file?
                if (data == "ACCEPT_FILE")
                {
                    send_updateStatus("File accepted. Sending...");

                    //configure progress bar
                    int total = 0;
                    this.InvokeEx(f => send_progressBar.Maximum = (int)send_fileinfo.Length);

                    var readed = -1;
                    var buffer = new Byte[4096];
                    using (var networkStream = new BufferedStream(new NetworkStream(send_tcpclient.Client, false)))
                    using (var fileStream = send_fileinfo.OpenRead())
                    {
                        while (readed != 0)
                        {
                            readed = fileStream.Read(buffer, 0, buffer.Length);
                            networkStream.Write(buffer, 0, readed);

                            //update progress bar
                            total = total + readed;
                            this.InvokeEx(f => send_progressBar.Value = total);
                        }
                        networkStream.FlushAsync();
                    }
                    send_updateStatus("File send complete.");
                }
                else { send_updateStatus("File rejected. Aborting send..."); }

                //at this point we should disconnect.
                clientdisconnect();
                this.InvokeEx(f => send_sendButton.Text = "Send");
                send_updateStatus("Disconnected.");
            }
            catch (Exception err)
            {
                send_updateStatus("Error connecting: " + err.Message);
                this.InvokeEx(f => send_sendButton.Text = "Send");
            }
        }
        void clientdisconnect()
        {
            //attempt to disconnect the connection
            try
            {
                send_tcpclient.Client.Disconnect(true);
                send_tcpclient.Close();
                send_tcpclient.Dispose();
            }
            catch { }
        }

        //receive tab
        string receive_foldername;
        string receive_filename;
        string receive_filehash;
        Int64 receive_filesize;
        TcpListener receive_listener;
        Socket receive_socket;
        Thread receivethread;
        bool receive_isserverrunning = false;
        void receive_disconnect()
        {
            try { receive_socket.Disconnect(false); }
            catch { }
            try { receive_socket.Dispose(); }
            catch { }
            try { receive_listener.Stop(); }
            catch { }
        }
        void receieve_updateStatus(string status)
        {
            //append the string to a newline and automatically scroll down to the bottom.
            this.InvokeEx(f => receive_Status.Text += "\r\n" + status);
            this.InvokeEx(f => receive_Status.SelectionStart = receive_Status.Text.Length);
            this.InvokeEx(f => receive_Status.ScrollToCaret());
        }
        private void receive_selectFolderButton_Click(object sender, EventArgs e)
        {
            receive_selectFolder.ShowDialog();
            if (receive_selectFolder.SelectedPath != "")
            {
                receive_foldername = receive_selectFolder.SelectedPath;
                receive_startServerButton.Enabled = true;
                receieve_updateStatus("Set folder path to " + receive_foldername);
            }
        }
        private void receive_startServerButton_Click(object sender, EventArgs e)
        {
            if (receive_isserverrunning)
            {
                receive_disconnect();
                receivethread.Abort();
                receive_startServerButton.Text = "Start server";
                receieve_updateStatus("Stopped server.");
                receive_isserverrunning = false;
            }
            else
            {
                receive_startServerButton.Text = "Stop server";
                receieve_updateStatus("Started server.");
                receive_isserverrunning = true;
                receivethread = new Thread(recv);
                receivethread.Start();
            }
        }
        public void recv()
        {
            try
            {
                receive_listener = new TcpListener(IPAddress.Any, Properties.Settings.Default.ListenPort);
                receive_listener.Start();
                receieve_updateStatus("Listening for file transfer requests on port " + Convert.ToString(Properties.Settings.Default.ListenPort));
                receive_socket = receive_listener.AcceptSocket();

                IPEndPoint remoteIpEndPoint = receive_socket.RemoteEndPoint as IPEndPoint; //create way of getting addresses

                receieve_updateStatus("Connection established from " + remoteIpEndPoint.Address + "\n");
                //socket is now connected. 
                for (; ; )
                {
                    //while socket is connected:
                    try
                    {
                        var data_bytes = methods.ReceiveAll(receive_socket);
                        string data = Encoding.UTF8.GetString(data_bytes, 0, data_bytes.Length);
                        if (data.Length != 0)
                        {
                            //remove pesky return at the end
                            data = data.Remove(data.Length - 1, 1); //data structure is as follows: length in bytes, file hash, filename
                            //split data by commas. 
                            string[] res = data.Split(Convert.ToChar(","));
                            //put each part of the array into variables
                            receive_filesize = Convert.ToInt32(res[0]);
                            receive_filehash = res[1];
                            receive_filename = res[2];
                            MessageBox.Show("Filesize: " + methods.filesizePerpective(receive_filesize) + "\nFile hash: " + receive_filehash + "\nFilename: " + receive_filename);

                        }
                        if (!methods.IsConnected(receive_socket))
                        {
                            receieve_updateStatus("Sender disconnected.");
                            receive_startServerButton.Text = "Start server";
                            receive_isserverrunning = false;
                            break;
                        }
                    }
                    catch (Exception err)
                    {
                        if (methods.IsConnected(receive_socket))
                        {
                            if (!err.Message.Contains("disposed object"))
                            {
                                receieve_updateStatus("Error receiving:\n" + err.Message);
                                try { receive_disconnect(); }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                if (err.Message == "Thread was being aborted.") { }
                else { receieve_updateStatus("Error receiving:\n" + err.Message); }
            }
        }

        async Task ReceiveFile(Socket socket, FileInfo file)
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

        //settings tab
        private void settings_saveSettingsButton_Click(object sender, EventArgs e)
        {
            //save settings to appdata
            Properties.Settings.Default.ServerAddress = settings_serverAddressInput.Text;
            Properties.Settings.Default.ServerPort = Convert.ToInt32(settings_serverPortInput.Value);
            Properties.Settings.Default.ListenPort = Convert.ToInt32(settings_listenPortInput.Value);
            Properties.Settings.Default.Save();
        }
    }
    static class methods
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
        public static string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        public static string filesizePerpective(Int64 filesize)
        {
            int kilobyte_size = 1024;
            int megabyte_size = 1024000;
            int gigabyte_size = 1024000000;
            //Int64 is needed due to the numbers being way too big for 32 bit .-.
            Int64 terabyte_size = Convert.ToInt64(1024000000000);
            string size;

            try
            {
                //Calculate file size. Uses kilobytes, megabytes, gigabytes and even terabytes.
                if (filesize > terabyte_size)
                {
                    //Terabytes
                    float formatted_size = filesize / terabyte_size;
                    size = (formatted_size.ToString() + " TB (" + filesize + " bytes)");
                }
                else if (filesize > gigabyte_size)
                {
                    //Gigabytes
                    float formatted_size = filesize / gigabyte_size;
                    size = (formatted_size.ToString() + " GB (" + filesize + " bytes)");
                }
                else if (filesize > megabyte_size)
                {
                    //Megabytes
                    float formatted_size = filesize / megabyte_size;
                    size = (formatted_size.ToString() + " MB (" + filesize + " bytes)");
                }
                else if (filesize > kilobyte_size)
                {
                    //Kilobytes
                    float formatted_size = filesize / kilobyte_size;
                    size = (formatted_size.ToString() + " KB (" + filesize + " bytes)");
                }
                else
                {
                    //Bytes
                    size = (filesize + " bytes");
                }
            }
            catch (DivideByZeroException) { size = "0 bytes"; }
            return size;
        }
    }
    static class ISynchronizeInvokeExtensions
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