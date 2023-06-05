using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace lab03_bai5
{
    public partial class Client_Send_File : Form
    {
        string path;
        public Client_Send_File()
        { 
            InitializeComponent();
        }

        private void sendfile(string fn)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("192.168.83.198"), 18000); // dia chi server;
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string fileName = fn;
            byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
            byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
            byte[] fileData = File.ReadAllBytes(fileName);
            byte[] clientData = new byte[4 + fileData.Length + fileNameByte.Length];

            fileNameLen.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);
            client.Connect(ipe);
            client.Send(clientData);
            client.Close();
        }
        private void button1_Click(object sender, EventArgs e) //button browser
        {
            //textBox1.Text = filename;
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //string[] files = (string[])openFileDialog.FileNames;
            //if (files != null && files.Length != 0 ) 
            //{
            //    Console.WriteLine(files[0]);
            //    sendfile(files[0]);
            //}
            //filename = openFileDialog.FileName;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            path = openFileDialog.FileName;
            textBox1.Text = path;
        }

        private void button2_Click(object sender, EventArgs e) //button send (file)
        {
            string[] files = path.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            if (files != null && files.Length != 0)
            {
                //Console.WriteLine(files[0]);
                sendfile(files[0]);
                MessageBox.Show("Da gui file thanh cong");
            }
        }
    }
}
