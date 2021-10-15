using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9001);
        private byte[] bytesSend;
        private Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void Form1_Load(object sender, EventArgs e)
        {
            server.Connect(endPoint);
        }

     

        public void send(string msg)
        {
            bytesSend = Encoding.ASCII.GetBytes(msg);
            server.Send(bytesSend);
        }

        private void button_opencdrom_Click(object sender, EventArgs e)
        {
            send("opencdrom-");
        }
    }
}
