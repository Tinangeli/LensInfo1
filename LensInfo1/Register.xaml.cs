using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public Register()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connection.Open();
                string Firstname = TextBoxFirstName.TextInput.Text;
                string Lastname = TextBoxLastName.TextInput.Text;
                int Age = int.Parse(TextBoxAge.TextInput.Text);
                string Username = TextBoxUsername.TextInput.Text;
                //string Password = TextBoxPassword.TextInput.Text;

                string MySqlQuery = "Insert into Customer (Firstname, Lastname, Age, Username, Password) values (@Firstname, @Lastname, @Age, @Username, @Password)";
                using (var command = new MySqlCommand (MySqlQuery, Connection)) {
                     command.Parameters.AddWithValue("@Firstname", Firstname);
                     command.Parameters.AddWithValue("@Lastname", Lastname);
                     command.Parameters.AddWithValue("@Age", Age);
                     command.Parameters.AddWithValue("@Username", Username);
                    //command.Parameters.AddWithValue("@Password", Password);

                    command.ExecuteNonQuery();     
                }
                int lastInsertedId;
                using (var command = new MySqlCommand("select CusID from Customer order by CusID desc limit 1;", Connection))
                {
                    lastInsertedId = (int)command.ExecuteScalar();
                }

                
                CustomerData.Instance.Customers.Add(new Customer
                {
                    IDCustomer = (int)lastInsertedId,
                    FirstName = Firstname,
                    LastName = Lastname,
                    Age = (int)Age,                    
                    Username = Username,
                    //Password = Password,
                });
            }
            catch { }
        }

        private void InputPasswordBox_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
