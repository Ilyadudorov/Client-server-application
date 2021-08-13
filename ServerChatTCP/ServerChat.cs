
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;


namespace ServerChatTCP
{
    class ServerChat
    {


        public List<TcpClient> clientlist;

        public List<string> clientconn;

        static TcpListener listener;

        public TcpClient client;

        public ServerChat()
        {


           
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);

            clientlist = new List<TcpClient>();

            clientconn = new List<string>();

        }



        public async void SetupConnection()
        {
            await Task.Run(() =>
            {
                listener.Start();
                int i = 1;
                while (true)
                {
                    client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);

                    clientlist.Add(client);

                    i++;
                    
                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    
                    clientThread.Start();

                    
                    
                }


            });
        }

        public List<string> ShowList()
        {
            foreach (var i in clientlist)
            {
                if (!i.Connected)
                {
                    clientconn.Remove(i.Client.RemoteEndPoint.ToString());
                }
            }
            

            foreach (var i in clientlist)
            {
                
                if (i.Connected)
                {
                    if (!clientconn.Contains(i.Client.RemoteEndPoint.ToString()))
                    {
                        clientconn.Add(i.Client.RemoteEndPoint.ToString());
                    }
                    
                }

            }

            clientconn.Distinct().ToArray();

            return clientconn;
        }

        public void CloseConnection()
        {
            if (listener != null)
                listener.Stop();

        }
    }
}
