using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClientChatTCP
{
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


        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            if(CC != null)
            {
                if (CC.Test())
                {
                    MessageBox.Show("Status: connected");
                }
                else
                {
                    MessageBox.Show("Status: disconnected");
                }
            }
            else
            {
                MessageBox.Show("Status: disconnected");
            }

            
        }


        private ImageSource Image_Convert(string bgImage64)
        {

            byte[] binaryData = Convert.FromBase64String(bgImage64);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();

            Image img = new Image();
            img.Source = bi;

            return img.Source;
        }



        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {

            Color_text.Document.Blocks.Clear();



            string[] words = CC.SendMsg(txtEditor.Text.ToString()).Split('#');

            string from = words[0];
            string text = words[1];
            string colorfortext = words[2];
            string image = words[3];


            SolidColorBrush color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#" + colorfortext));


            txtEditor.Text = from;

            
            Color_text.Foreground = color;
            Color_text.AppendText(text);

            ImageBox.Source = Image_Convert(image);


        }

        private void StatusBtn_Click(object sender, RoutedEventArgs e)
        {

           

            if (Status.Text == "Status: connected")
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
            else
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
            }
            
            
        }

        
    }
}
