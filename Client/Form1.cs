using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(String command, StringBuilder buffer, Int32 bufferSize, IntPtr hwndCallback);
        private const int buffers = 0xfffffff;
        public Socket _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9001);
        private byte[] bytesRecv = new byte[buffers];
        private string stringRecv = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            __INITSERVER__();
            new Thread(new ThreadStart(new Action(() => {
                __STARTSERVER__();
            }))).Start();
        }

        private void __INITSERVER__()
        {

            _sock.Bind(endPoint);
            _sock.Listen(0xA);

        }
        private void __STARTSERVER__()
        {
            Socket _clientsock = _sock.Accept();
            while (true)
            {
                int BuffRecv = _clientsock.Receive(bytesRecv);
                stringRecv += Encoding.ASCII.GetString(bytesRecv, 0, BuffRecv);
                if (stringRecv.StartsWith("opencdrom-"))
                {
                    mciSendString("set CDAudio door open", null, 0, IntPtr.Zero);
                }
            }
        }
    }
}
