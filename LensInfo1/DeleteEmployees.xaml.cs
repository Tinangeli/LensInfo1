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
    /// Interaction logic for DeleteEmployees.xaml
    /// </summary>
    public partial class DeleteEmployees : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public DeleteEmployees()
        {
            InitializeComponent();
        }
        private void PopulateMovieDetails(MySqlDataReader reader)
        {

            TextBoxEmployeeFirstName.customtextguide = reader["FirstName"].ToString();
            TextBoxEmployeeLastName.customtextguide = reader["LastName"].ToString();
            TextBoxEmployeeNumber.customtextguide = reader["PhoneNum"].ToString();
            TextBoxEmployeePosition.customtextguide = reader["Position"].ToString();
            TextBoxEmployeeUsername.customtextguide = reader["Username"].ToString();
            TextBoxEmployeePassword.customtextguide = reader["Password"].ToString();
        }

        private void ClearTextBlocks()
        {
            TextBoxEmployeeFirstName.customtextguide = string.Empty;
            TextBoxEmployeeLastName.customtextguide = string.Empty;
            TextBoxEmployeeNumber.customtextguide= string.Empty;
            TextBoxEmployeePosition.customtextguide = string.Empty;
            TextBoxEmployeeUsername.customtextguide = string.Empty;
            TextBoxEmployeePassword.customtextguide = string.Empty;

        }

        private void ButtonDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var inputText = InputTextBoxIdSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(inputText) || !int.TryParse(inputText, out int id))
            {
                MessageBox.Show("Please enter a valid Employee ID.", "Input Error", MessageBoxButton.OK);
                ClearTextBlocks();
                return;
            }

            try
            {
                Connection.Open();

                string mySqlQuery = "DELETE FROM employeesint WHERE IDnum = @EmployeeIdSelector;"; // Correct parameter name
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@EmployeeIdSelector", id);
                    command.ExecuteNonQuery();
                }

                // Remove from the ObservableCollection
                var EmployeeToRemove = EmployeeData.Instance.Employees.FirstOrDefault(m => m.IDNum == id);
                if (EmployeeToRemove != null)
                {
                    EmployeeData.Instance.Employees.Remove(EmployeeToRemove);
                    MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("No Employee found to delete.", "Not Found", MessageBoxButton.OK);
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

      

        private void InputTextBoxIdSelector_TextChanged(object sender, TextChangedEventArgs e)
        {
            var inputText = InputTextBoxIdSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                ClearTextBlocks();
                return;
            }

            if (!int.TryParse(inputText, out int EmployeeIdSelector))
            {
                ClearTextBlocks();
                return;
            }

            try
            {
                Connection.Open();

                string mySqlQuery = "SELECT * FROM employeesint WHERE IDnum = @EmployeeIdSelector;"; // Correct parameter name
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@EmployeeIdSelector", EmployeeIdSelector); // Correct parameter name

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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
