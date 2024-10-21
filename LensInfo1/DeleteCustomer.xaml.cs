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
    /// Interaction logic for DeleteCustomer.xaml
    /// </summary>
    
    public partial class DeleteCustomer : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public DeleteCustomer()
        {
            InitializeComponent();
        }

        private void PopulateMovieDetails(MySqlDataReader reader)
        {

            TextBoxCustomerFirstName.customtextguide = reader["FirstName"].ToString();
            TextBoxCustomerLastName.customtextguide = reader["LastName"].ToString();
            TextBoxCustomerAge.customtextguide = reader["Age"].ToString();
            TextBoxCustomerUsername.customtextguide = reader["Username"].ToString();
            TextBoxCustomerPassword.customtextguide = reader["Password"].ToString();
        }

        private void ClearTextBlocks()
        {
            TextBoxCustomerFirstName.customtextguide = string.Empty;
            TextBoxCustomerLastName.customtextguide = string.Empty;
            TextBoxCustomerAge.customtextguide = string.Empty;
            TextBoxCustomerUsername.customtextguide = string.Empty;
            TextBoxCustomerPassword.customtextguide= string.Empty;

        }

        private void InputTextBoxCustomerIdSelector_TextChanged(object sender, TextChangedEventArgs e)
        {
            var inputText = InputTextBoxIdSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                ClearTextBlocks();
                return;
            }

            if (!int.TryParse(inputText, out int IdCustomerSelector))
            {
                ClearTextBlocks();
                return;
            }

            try
            {
                Connection.Open();

                string mySqlQuery = "SELECT * FROM Customer WHERE CusID = @IdCustomerSelector;"; // Correct parameter name
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@IdCustomerSelector", IdCustomerSelector); // Correct parameter name

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PopulateMovieDetails(reader);
                        }
                        else
                        {
                            ClearTextBlocks();
                            // Optionally, show a message if desired:
                            // MessageBox.Show("No customer found with that ID.", "Not Found", MessageBoxButton.OK);
                        }
                    }
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show($"Error {ex.Number}: {ex.Message}", "Error", MessageBoxButton.OK);
            }
            finally
            {
                Connection.Close();
            }
        }

        private void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var inputText = InputTextBoxIdSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(inputText) || !int.TryParse(inputText, out int id))
            {
                MessageBox.Show("Please enter a valid Customer ID.", "Input Error", MessageBoxButton.OK);
                ClearTextBlocks();
                return;
            }

            try
            {
                Connection.Open();

                string mySqlQuery = "DELETE FROM Customer WHERE CusID = @CustomerIdSelector;"; // Correct parameter name
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@CustomerIdSelector", id);
                    command.ExecuteNonQuery();
                }

                // Remove from the ObservableCollection
                var CustomerToRemove = CustomerData.Instance.Customers.FirstOrDefault(m => m.IDCustomer == id);
                if (CustomerToRemove != null)
                {
                    CustomerData.Instance.Customers.Remove(CustomerToRemove);
                    MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("No customer found to delete.", "Not Found", MessageBoxButton.OK);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show($"Error {ex.Number}: {ex.Message}", "Error", MessageBoxButton.OK);
            }
            finally
            {
                Connection.Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
