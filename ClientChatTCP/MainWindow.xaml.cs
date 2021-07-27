using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;

namespace ClientChatTCP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path;


        public string ipserver = "127.0.0.1";

        public const int port = 8888;

        ClientChat CC;

        public MainWindow()
        {
            InitializeComponent();
            

        }






        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            path = openFileDialog.FileName;





        }

        private void ButtonConnection_Click(object sender, RoutedEventArgs e)
        {

            CC = new ClientChat();

            CC.SetupConnection(IPstring.Text.Trim(), Convert.ToInt32(Portstring.Text.Trim()));


            if (CC.Test())
            {
                Status.Text = "Status: connected";
            }
            else
            {
                Status.Text = "Status: disconnected";
            }

            //Waitfile();

        }

        async void Waitfile()
        {
            await Task.Run(() => 
            {
                TcpClient client = new TcpClient();
                client.Connect(ipserver, port);

                byte[] data = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable); // пока данные есть в потоке

                MessageBox.Show(response.ToString());

                // Закрываем потоки
                stream.Close();
                client.Close();


            });
        }



        private void Okbtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            

            if (CC.Test())
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Disconnected");
            }
                


        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {

            CC.CloseConnection();

            if (CC.Test())
            {
                Status.Text = "Status: connected";
            }
            else
            {
                Status.Text = "Status: disconnected";
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            CC.SendMsg(txtEditor.Text.ToString());
        }
    }
}
