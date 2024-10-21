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
    /// Interaction logic for EditEmployees.xaml
    /// </summary>
    public partial class EditEmployees : Window
    {
        public event Action EmployeeUpdated;
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public EditEmployees()
        {
            InitializeComponent();
        }
        private void ClearTextBoxes()
        {
            TextBoxInputID.TextInput.Text = string.Empty;
            TextBoxInputFirstName.TextInput.Text = string.Empty;
            TextBoxInputLastName.TextInput.Text = string.Empty;
            TextBoxInputPhoneNum.TextInput.Text = string.Empty;
            TextBoxInputPosition.TextInput.Text = string.Empty;
            TextBoxInputUserName.TextInput.Text = string.Empty;
            TextBoxInputPassword.TextInput.Text = string.Empty;

            TextBlockCurrentFirstName.customtextguide = "No Data";
            TextBlockCurrentLastName.customtextguide = "No Data";
            TextBlockCurrentPhoneNum.customtextguide = "No Data";
            TextBlockCurrentPosition.customtextguide = "No Data";
            TextBlockCurrentUserName.customtextguide = "No Data";
            TextBlockCurrentPassword.customtextguide = "No Data";
        }

        private void PopulateEmployeeDetails(MySqlDataReader reader)
        {
            TextBoxInputID.TextInput.Text = reader["IDNum"].ToString();
            TextBoxInputFirstName.TextInput.Text = reader["FirstName"].ToString();
            TextBoxInputLastName.TextInput.Text = reader["LastName"].ToString();
            TextBoxInputPhoneNum.TextInput.Text = reader["PhoneNum"].ToString();
            TextBoxInputPosition.TextInput.Text = reader["Position"].ToString();
            TextBoxInputUserName.TextInput.Text = reader["Username"].ToString();
            TextBoxInputPassword.TextInput.Text = reader["Password"].ToString();
           
            TextBlockCurrentFirstName.customtextguide = reader["FirstName"].ToString();
            TextBlockCurrentLastName.customtextguide = reader["LastName"].ToString();
            TextBlockCurrentPhoneNum.customtextguide = reader["PhoneNum"].ToString();
            TextBlockCurrentPosition.customtextguide = reader["Position"].ToString();
            TextBlockCurrentUserName.customtextguide = reader["Username"].ToString();
            TextBlockCurrentPassword.customtextguide = reader["Password"].ToString();
        }

        private void TextInputCustomerIdSelector_TextChanged(object sender, TextChangedEventArgs e)
        {
            var inputText = TextInputCustomerIdSelector.TextInput.Text;
            if (string.IsNullOrWhiteSpace(inputText) || !int.TryParse(inputText, out int id))
            {
                ClearTextBoxes();
                return;
            }

            try
            {
                Connection.Open();

                string mySqlQuery = "SELECT * FROM Employeesint WHERE IDnum = @IDNum;";
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@IDNum", inputText);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PopulateEmployeeDetails(reader);
                        }
                        else
                        {
                            ClearTextBoxes();
                            // Optionally, show a message if desired:
                            // MessageBox.Show("No employee found with that username.", "Not Found", MessageBoxButton.OK);
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

        // Method to fetch employee details from the database
        private void PopulateEmployeeDetails(int employeeId)
        {
            using (var command = new MySqlCommand("SELECT * FROM Employeesint WHERE IDNum = @IDNum", Connection))
            {
                command.Parameters.AddWithValue("@IDNum", employeeId);

                try
                {
                    Connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PopulateEmployeeDetails(reader);
                        }
                        else
                        {
                            // Optionally handle case where no employee was found
                            MessageBox.Show("Employee not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        private void ValidateInputs(List<string> errorMessages)
        {
            // Validate Original IDNum
            if (string.IsNullOrWhiteSpace(TextInputCustomerIdSelector.TextInput.Text) ||
                !int.TryParse(TextInputCustomerIdSelector.TextInput.Text, out _))
            {
                errorMessages.Add("Original ID Number is required and must be a valid integer.");
            }

            // Validate New IDNum
            if (string.IsNullOrWhiteSpace(TextBoxInputID.TextInput.Text) ||
                !int.TryParse(TextBoxInputID.TextInput.Text, out _))
            {
                errorMessages.Add("New ID Number is required and must be a valid integer.");
            }

            // Validate other fields as before
            if (string.IsNullOrWhiteSpace(TextBoxInputFirstName.TextInput.Text))
                errorMessages.Add("First Name is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputLastName.TextInput.Text))
                errorMessages.Add("Last Name is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputPhoneNum.TextInput.Text))
                errorMessages.Add("Phone Number is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputPosition.TextInput.Text))
                errorMessages.Add("Position is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputUserName.TextInput.Text))
                errorMessages.Add("Username is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputPassword.TextInput.Text))
                errorMessages.Add("Password is required.");
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            // Clear any previous error messages
            var errorMessages = new List<string>();

            // Get the original and new IDNum
            var originalIdText = TextInputCustomerIdSelector.TextInput.Text;
            var newIdText = TextBoxInputID.TextInput.Text;

            // Validate the original and new IDs
            if (string.IsNullOrWhiteSpace(originalIdText) || !int.TryParse(originalIdText, out int originalId) ||
                string.IsNullOrWhiteSpace(newIdText) || !int.TryParse(newIdText, out int newId))
            {
                ClearTextBoxes();
                return;
            }

            
            ValidateInputs(errorMessages);

            // If there are validation errors, show them and exit the method
            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join("\n", errorMessages), "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Connection.Open();

                string query = "UPDATE Employeesint SET FirstName = @FirstName, LastName = @LastName, PhoneNum = @PhoneNum, " +
                               "Position = @Position, Username = @Username, Password = @Password, IDNum = @NewIDNum WHERE IDNum = @OriginalIDNum";

                using (var command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@FirstName", TextBoxInputFirstName.TextInput.Text);
                    command.Parameters.AddWithValue("@LastName", TextBoxInputLastName.TextInput.Text);
                    command.Parameters.AddWithValue("@PhoneNum", TextBoxInputPhoneNum.TextInput.Text);
                    command.Parameters.AddWithValue("@Position", TextBoxInputPosition.TextInput.Text);
                    command.Parameters.AddWithValue("@Username", TextBoxInputUserName.TextInput.Text);
                    command.Parameters.AddWithValue("@Password", TextBoxInputPassword.TextInput.Text);
                    command.Parameters.AddWithValue("@NewIDNum", newId); // New ID to update to
                    command.Parameters.AddWithValue("@OriginalIDNum", originalId); // Use original ID for the WHERE clause

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        
                        var employeeToUpdate = EmployeeData.Instance.Employees.FirstOrDefault(e => e.IDNum == originalId);
                        if (employeeToUpdate != null)
                        {
                            employeeToUpdate.FirstName = TextBoxInputFirstName.TextInput.Text;
                            employeeToUpdate.LastName = TextBoxInputLastName.TextInput.Text;
                            employeeToUpdate.PhoneNumber = TextBoxInputPhoneNum.TextInput.Text;
                            employeeToUpdate.Position = TextBoxInputPosition.TextInput.Text;
                            employeeToUpdate.Username = TextBoxInputUserName.TextInput.Text;
                            employeeToUpdate.Password = TextBoxInputPassword.TextInput.Text;
                            employeeToUpdate.IDNum = newId; // Update IDNum in the in-memory data
                        }

                        
                        EmployeeUpdated?.Invoke();

                        
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("No employee found with the provided original ID.", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
