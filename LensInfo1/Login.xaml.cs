using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace LensInfo1
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public Login()
        {
            InitializeComponent();
            
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            var register = new Register();
            register.Visibility = Visibility.Visible;    
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string Username;
            using (var connection = new MySqlConnection(SqlConnection)) {
                connection.Open();
                var command = new MySqlCommand("Select * from employeesint");
            
                }

        }
    }
}
