using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace lab03_bai5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //button Server
        {
            Server server = new Server();
            server.Show();
        }

        private void button2_Click(object sender, EventArgs e) //button Client
        {
            Client client = new Client();
            client.Show();
        }
    }
}
