using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace ServerChatTCP
{
    class ServerChat
    {
        public string ip;

        public int port;

        TcpListener server;

        public ServerChat(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            server = new TcpListener(IPAddress.Parse(ip), port);

        }

        public ServerChat()
        {
           

            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);

        }



        public void SetupConnection()
        {
            server.Start();


            TcpClient client = server.AcceptTcpClient();



            //await Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        TcpClient client = server.AcceptTcpClient();

            //        NetworkStream stream = client.GetStream();

            //        string response = "Привет мир";
            //        byte[] data = Encoding.UTF8.GetBytes(response);

            //        stream.Write(data, 0, data.Length);

            //        stream.Close();

            //        client.Close();
            //    }
            //});

        }

        public void CloseConnection()
        {
            server.Stop();
        }








        
    }
}
