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

namespace lab03_bai5
{
    public partial class Client_Send_File : Form
    {
        public Client_Send_File()
        {
            InitializeComponent();
        }

        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("192.168.83.198"), 18000); // dia chi server;
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        string filename;
        string filepath;

        private void button1_Click(object sender, EventArgs e) //button brownser
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            filename = openFileDialog.FileName;
            textBox1.Text = filename;
        }

        private void button2_Click(object sender, EventArgs e) //button send
        {

        }
    }
}
