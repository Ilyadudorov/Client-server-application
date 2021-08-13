using System;
using System.Text;
using System.Net.Sockets;

namespace ClientChatTCP
{
    class ClientChat
    {


        public TcpClient client;

        public NetworkStream stream;

      


       






        public ClientChat()
        {
            
        }


        public void SetupConnection(string ip, int port)
        {

            try
            {
                client = new TcpClient(ip, port);
                stream = client.GetStream();
            }
            catch (Exception e)
            {

                
            }

            
        }

        public void CloseConnection()
        {
            client.Close();
            client = null;
        }

        public string SendMsg(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);

            stream.Write(data, 0, data.Length);

 
            data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            return builder.ToString();

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
            if (client == null)
            {
                return false;
            }

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
