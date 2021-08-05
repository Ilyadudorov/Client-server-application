using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleServer
{
    public class ClientObject
    {

        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }




        public void Process()
        {
            NetworkStream stream = null;


            stream = client.GetStream();
            byte[] data = new byte[64]; // буфер для получаемых данных
            while (true)
            {
                // получаем сообщение
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                string message = builder.ToString();


                


                XElement xdoc = XElement.Parse(message);
                message = "";

                message += "From: " + xdoc.Element("Message").Attribute("from").Value + "#";

                message += xdoc.Element("Message").Element("msg").Element("text").Value + "#";

                message += xdoc.Element("Message").Element("msg").Element("text").Attribute("color").Value + "#";

                message += xdoc.Element("Message").Element("msg").Element("image").Value;





                //foreach (XElement rootElement in xdoc.Element("root").Elements("Message"))
                //{
                //    XAttribute namefromAttribute = rootElement.Attribute("from");
                //    XElement textElement = rootElement.Element("text");

                //    if (namefromAttribute != null && textElement != null)
                //    {
                //        message += namefromAttribute.Value + "\n";
                //        message += textElement.Value + "\n";
                //    }

                //}


                // метод обработки сообщения 

                data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }


            if (stream != null)
            {
                stream.Close();
            }
                

            if (client != null)
            {
                client.Close();

            }
                

        }



    }
}
