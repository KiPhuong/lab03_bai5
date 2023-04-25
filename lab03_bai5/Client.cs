﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab03_bai5
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18000); // dia chi server;
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        void send(string s) // gui tin nhan
        {
            if (s != textBox2.Text)
            {
                string ip = client.LocalEndPoint.ToString(); // lấy ip và port của client
                string mess = s + " " + ip; // send thong bao cho server 
                byte[] data = Encoding.UTF8.GetBytes(mess);
                client.Send(data);
            }
            else
            {
                //s = textBox2.Text; message cần gửi
                string mess = textBox1.Text + ": " + s; // format = name: mess
                byte[] data = Encoding.UTF8.GetBytes(mess);
                client.Send(data);
            }
        }

        void recieve() // nhan tin nhan
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 200];
                    client.Receive(data);
                    string mess = Encoding.UTF8.GetString(data);
                    listView1.Items.Add(mess);
                }
            }
            catch
            {
                Close();
            }
        }


        private void button1_Click(object sender, EventArgs e) // button send
        {
            if (textBox2.Text != "")
            {
                send(textBox2.Text);
                listView1.Items.Add(textBox1.Text + ": " + textBox2.Text);
                textBox2.Clear();
            }
            else MessageBox.Show("Vui lòng nhập tin nhắn!");
        }

        private void button2_Click(object sender, EventArgs e) // button connect
        {
            CheckForIllegalCrossThreadCalls = false;
            if (textBox1.Text != "")
            {
                try
                {
                    client.Connect(ipe);
                    send("New client connected from: ");
                }
                catch
                {
                    MessageBox.Show("Kết nối lỗi!");
                    return;
                }
                Thread listen = new Thread(recieve);
                listen.Start();
                button2.Text = "Connected";
                button2.Enabled = false;
            }
            else MessageBox.Show("Vui lòng nhập tên!");
        }

        private void button3_Click(object sender, EventArgs e) //button disconnect
        {
            string temp = textBox1.Text + " is disconnected";
            send(temp);
            Close();
        }
    }
}
