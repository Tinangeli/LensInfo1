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
    /// Interaction logic for EditCustomer.xaml
    /// </summary>
    public partial class EditCustomer : Window
    {
        public event Action CustomerUpdated;
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public EditCustomer()
        {
            InitializeComponent();
        }
        private void ClearTextBoxes()
        {
            TextBoxInputID.TextInput.Text = string.Empty;
            TextBoxInputFirstName.TextInput.Text = string.Empty;
            TextBoxInputLastName.TextInput.Text = string.Empty;
            TextBoxInputAge.TextInput.Text = string.Empty;

            TextBoxInputUserName.TextInput.Text = string.Empty;
            TextBoxInputPassword.TextInput.Text = string.Empty;

            // Resetting text guides to "No Data"
            TextBlockCurrentFirstName.customtextguide = "No Data";
            TextBlockCurrentLastName.customtextguide = "No Data";
            TextBlockCurrentAge.customtextguide = "No Data";
            TextBlockCurrentUserName.customtextguide = "No Data";
            TextBlockCurrentPassword.customtextguide = "No Data";
        }

        private void PopulateCustomerDetails(MySqlDataReader reader)
        {
            TextBoxInputID.TextInput.Text = reader["CusId"].ToString(); // Make sure column name matches database
            TextBoxInputFirstName.TextInput.Text = reader["FirstName"].ToString();
            TextBoxInputLastName.TextInput.Text = reader["LastName"].ToString();

            TextBoxInputAge.TextInput.Text = reader["Age"].ToString(); // Update to read Age instead of PhoneNum
            TextBoxInputUserName.TextInput.Text = reader["Username"].ToString();
            TextBoxInputPassword.TextInput.Text = reader["Password"].ToString();

            // Update current text guides
            TextBlockCurrentFirstName.customtextguide = reader["FirstName"].ToString();
            TextBlockCurrentLastName.customtextguide = reader["LastName"].ToString();
            TextBlockCurrentAge.customtextguide = reader["Age"].ToString(); // Update to Age
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
                string mySqlQuery = "SELECT * FROM Customer WHERE CusId = @CusId;";
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@CusId", id); // Use parsed ID

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PopulateCustomerDetails(reader);
                        }
                        else
                        {
                            ClearTextBoxes();
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

        // Validate inputs including Age as integer
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

            // Validate other fields
            if (string.IsNullOrWhiteSpace(TextBoxInputFirstName.TextInput.Text))
                errorMessages.Add("First Name is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputLastName.TextInput.Text))
                errorMessages.Add("Last Name is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputAge.TextInput.Text) ||
                !int.TryParse(TextBoxInputAge.TextInput.Text.Trim(), out _))
                errorMessages.Add("Age is required and must be a valid number.");

            if (string.IsNullOrWhiteSpace(TextBoxInputUserName.TextInput.Text))
                errorMessages.Add("Username is required.");
            if (string.IsNullOrWhiteSpace(TextBoxInputPassword.TextInput.Text))
                errorMessages.Add("Password is required.");
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var errorMessages = new List<string>();

            var originalIdText = TextInputCustomerIdSelector.TextInput.Text;
            var newIdText = TextBoxInputID.TextInput.Text;

            if (string.IsNullOrWhiteSpace(originalIdText) || !int.TryParse(originalIdText, out int originalId) ||
                string.IsNullOrWhiteSpace(newIdText) || !int.TryParse(newIdText, out int newId))
            {
                ClearTextBoxes();
                return;
            }

            ValidateInputs(errorMessages);

            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join("\n", errorMessages), "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Convert Age to int
            if (!int.TryParse(TextBoxInputAge.TextInput.Text.Trim(), out int age))
            {
                MessageBox.Show("Age must be a valid integer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Connection.Open();
                string query = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Age = @Age, " +
                               "Username = @Username, Password = @Password, CusId = @NewIDNum WHERE CusId = @OriginalIDNum";

                using (var command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@FirstName", TextBoxInputFirstName.TextInput.Text);
                    command.Parameters.AddWithValue("@LastName", TextBoxInputLastName.TextInput.Text);
                    command.Parameters.AddWithValue("@Age", age); // Use the parsed Age
                    command.Parameters.AddWithValue("@Username", TextBoxInputUserName.TextInput.Text);
                    command.Parameters.AddWithValue("@Password", TextBoxInputPassword.TextInput.Text);
                    command.Parameters.AddWithValue("@NewIDNum", newId);
                    command.Parameters.AddWithValue("@OriginalIDNum", originalId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Update in-memory data
                        var CustomerToUpdate = CustomerData.Instance.Customers.FirstOrDefault(e => e.IDCustomer == originalId);
                        if (CustomerToUpdate != null)
                        {
                            CustomerToUpdate.FirstName = TextBoxInputFirstName.TextInput.Text;
                            CustomerToUpdate.LastName = TextBoxInputLastName.TextInput.Text;
                            CustomerToUpdate.Age = age; // Update Age in the in-memory data
                            CustomerToUpdate.Username = TextBoxInputUserName.TextInput.Text;
                            CustomerToUpdate.Password = TextBoxInputPassword.TextInput.Text;
                            CustomerToUpdate.IDCustomer = newId; // Update IDCustomer
                        }

                        CustomerUpdated?.Invoke();
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("No customer found with the provided original ID.", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
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
}