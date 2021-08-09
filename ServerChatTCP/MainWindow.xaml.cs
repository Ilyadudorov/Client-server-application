using System;
using System.Collections.Generic;
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

namespace ServerChatTCP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ServerChat SC;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        

        private void ButtonConnection_Click(object sender, RoutedEventArgs e)
        {
            //SC = new ServerChat(IPstring.Text.Trim(), Convert.ToInt32(Portstring.Text.Trim()));
            SC = new ServerChat();
            SC.SetupConnection();
        }

        private void Okbtn_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Clear();

            foreach (var item in SC.ShowList())
            {
                txtEditor.AppendText(item + Environment.NewLine); 
                
            }

            

        }
    }
}
