using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace ClientChatTCP
{
    class ClientChat
    {


        public TcpClient client;

        


        public ClientChat()
        {
            client = new TcpClient();
        }


        public void SetupConnection(string ip, int port)
        {
            client.Connect(ip, port);

        }

        public void CloseConnection()
        {
            client.Close();
        }

        public void SendMsg(string message)
        {
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(message);

            stream.Write(data, 0, data.Length);

            stream.Close();

        }



        public void SendFile(XmlDocument path)
        {
            NetworkStream stream = client.GetStream();

            string doc = path.OuterXml.ToString();

            byte[] data = Encoding.UTF8.GetBytes(doc);

            stream.Write(data, 0, data.Length);

            stream.Close();

        }



        public bool Test()
        {

            if (client.Client == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            

        }









    }
}
