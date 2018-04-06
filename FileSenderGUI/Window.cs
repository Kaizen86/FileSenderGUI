using System;
using System.IO;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSenderGUI
{
    public partial class Window : Form
    {
        string filename;
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
            connectthread = new Thread(connect);
        }
        public void send_updateStatus(string status)
        {
            //this.InvokeEx(f => send_Status.Text = status);
        }

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            //select file
            send_fileSelect.ShowDialog();
            filename = send_fileSelect.FileName;

            //generate checksum
            //send_updateStatus("Computing file checksum...");
            //idfk what goes here yet lol
            send_updateStatus("File selected.");
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                connectthread.Start();
            }
            catch { MessageBox.Show("Error: Cannot connect twice"); }
        }
        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            //save settings to appdata
            Properties.Settings.Default.ServerAddress = settings_serverAddressInput.Text;
            Properties.Settings.Default.ServerPort = Convert.ToInt32(settings_serverPortInput.Value);
            Properties.Settings.Default.ListenPort = Convert.ToInt32(settings_listenPortInput.Value);
            Properties.Settings.Default.Save();
        }

        public void connect()
        {
            MessageBox.Show("Connecting...");
            try
            {
                disconnect();
                //create new client object
                tcpclient = new TcpClient();

                //connect to the server 
                tcpclient.Connect(Properties.Settings.Default.ServerAddress, Properties.Settings.Default.ServerPort);
                MessageBox.Show("Connected :D");
            }
            catch (Exception err) { MessageBox.Show("Error connecting: " + err.Message); }
        }
        public void disconnect()
        {
            //attempt to disconnect the connection
            try
            {
                tcpclient.Close();
                tcpclient.Dispose();

            }
            catch { }
        }
        
        public async Task SendFile(FileInfo file, Socket socket)
        {
            var readed = -1;
            var buffer = new Byte[4096];
            using (var networkStream = new BufferedStream(new NetworkStream(socket, false)))
            using (var fileStream = file.OpenRead())
            {
                while (readed != 0)
                {
                    readed = fileStream.Read(buffer, 0, buffer.Length);
                    networkStream.Write(buffer, 0, readed);
                    Console.WriteLine("Copied " + readed);
                }
                await networkStream.FlushAsync();
            }
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
        
        /*
        bool IsConnected(this Socket socket)
        {
            try { return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0); }
            catch (SocketException) { return false; }
        }  
        
        public  byte[] ReceiveAll(this Socket socket)
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
        */


        public  string checkMD5(string filename)
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
}
