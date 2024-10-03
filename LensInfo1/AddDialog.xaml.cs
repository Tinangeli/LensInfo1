using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    
    public partial class AddDialog : Window
    {
        

        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public AddDialog()
        {
            InitializeComponent();
            //TextBoxFirstName
            
            
                
        }
                      
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }


        public void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connection.Open();
                string FirstName = TextBoxFirstName.TextInput.Text;
                string LastName = TextBoxLastName.TextInput.Text;
                string PhoneNumber = TextBoxPhoneNum.TextInput.Text;
                string Position = TextBoxPosition.TextInput.Text;

                string MySqlQuery = "INSERT INTO employeesint (FirstName, LastName, PhoneNum, Position) VALUES (@FirstName, @LastName, @PhoneNumber, @Position)";

                using (var command = new MySqlCommand(MySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.Parameters.AddWithValue("@Position", Position);
                    command.ExecuteNonQuery();
                }
                int lastInsertedId;
                using (var command = new MySqlCommand("select idnum from employeesint order by IDnum desc limit 1;", Connection))
                {
                    lastInsertedId = (int)command.ExecuteScalar(); 
                }

                // Add the new employee to the ObservableCollection
                EmployeeData.Instance.Employees.Add(new Employee
                {
                    IDNum = (int)lastInsertedId,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    Position = Position
                });


                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButton.OK);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message, "Error", MessageBoxButton.OK);
            }
            finally
            {
                Connection.Close();
            }

        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
            {
            this.Visibility = Visibility.Hidden;
            
            }

        

    }
}
