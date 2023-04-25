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
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 18000);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientlist = new List<Socket>();

        private void Server_Load(object sender, EventArgs e)
        {
            listView1.Items.Add("Server is ready...");
            connect();
        }

        void connect() // hàm dùng để kết nối với các client
        {
            server.Bind(ipe);
            Thread listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        server.Listen(100);
                        Socket client = server.Accept();
                        //listView1.Items.Add("Client is connected");
                        clientlist.Add(client);
                        Thread recieve_thr = new Thread(recieve);
                        recieve_thr.Start(client);
                    }
                }
                catch
                {
                    IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 18000);
                    Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }

            });
            listen.Start();
        }
        void recieve(object obj) // hàm nhận message cùng với đó là gửi message đó cho các client còn lại.
        {
            // Socket cli = obj as Socket;
            Socket cli = (Socket)obj;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 200];
                    cli.Receive(data);
                    string mess = Encoding.UTF8.GetString(data);
                    listView1.Items.Add(mess);
                    foreach(Socket item in clientlist)
                    {
                        if (item != null && item != cli) item.Send(data);
                    }
                }
            }
            catch
            {
                clientlist.Remove(cli);
                cli.Close();
            }
        }

        
    }
}
