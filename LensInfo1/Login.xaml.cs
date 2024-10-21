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
using ZXing;
using Emgu;
using ZXing.QrCode.Internal;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Threading;


namespace LensInfo1
{
    
    public partial class Login : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public Login()
        {
            InitializeComponent();           
            try
            {
                QRImage.Source = new BitmapImage(new Uri("QRCode.png", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}");
            }
        }

        

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            var register = new Register();
            register.Visibility = Visibility.Visible;    
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TextBoxUsername.TextInput.Text;
            string password = PasswordBoxPassword.InputPasswordBox.Password;

            using (var connection = new MySqlConnection(SqlConnection))
            {
                try
                {
                    connection.Open();

                    // Check if the user is an employee
                    using (var employeeCommand = new MySqlCommand("SELECT FirstName, LastName FROM employeesint WHERE Username = @Username AND Password = @Password", connection))
                    {
                        employeeCommand.Parameters.AddWithValue("@Username", username);
                        employeeCommand.Parameters.AddWithValue("@Password", password);

                        using (var reader = employeeCommand.ExecuteReader())
                        {
                            if (reader.Read()) // Employee found
                            {
                                string firstName = reader["FirstName"] as string;
                                string lastName = reader["LastName"] as string;

                                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                                {
                                    var mainWindow = new MainWindow($"{firstName} {lastName}");
                                    mainWindow.Show();
                                    this.Close(); // Close the login window
                                    return; // Exit the method
                                }
                            }
                        }
                    }

                    // Check if the user is a customer
                    using (var customerCommand = new MySqlCommand("SELECT CusID, Username, LastName FROM customer WHERE Username = @Username AND Password = @Password", connection))
                    {
                        customerCommand.Parameters.AddWithValue("@Username", username);
                        customerCommand.Parameters.AddWithValue("@Password", password);

                        using (var reader = customerCommand.ExecuteReader())
                        {
                            if (reader.Read()) // Customer found
                            {
                                int fetchedCustomerID = reader.GetInt32("CusId");
                                string fetchedUsername = reader["Username"] as string;
                                string fetchedLastName = reader["LastName"] as string;

                                if (!string.IsNullOrEmpty(fetchedUsername) && !string.IsNullOrEmpty(fetchedLastName))
                                {
                                    // Create the CustomerField window with both username and lastName
                                    var customerWindow = new CustomerField(fetchedCustomerID, fetchedUsername, fetchedLastName);
                                    customerWindow.Show();
                                    this.Close();
                                    return; // Exit the method
                                }
                            }
                        }
                    }

                    // If we reach this point, neither an employee nor a customer was found
                    MessageBox.Show("Invalid username or password.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }





        private Login _currentLogin; // Store the current login instance

        private void ButtonQR_Click(object sender, RoutedEventArgs e)
        {
            // Store the current instance
            _currentLogin = this;

            QRCamera qrCameraWindow = new QRCamera();
            qrCameraWindow.QRCodeDecoded += OnQRCodeDecoded;
            qrCameraWindow.Show(); // Show the camera window
        }

        private void OnQRCodeDecoded(string qrData)
        {
            // Use the existing login instance
            _currentLogin.AuthenticateUser(qrData);
        }

        public void AuthenticateUser(string qrData)
        {
            Console.WriteLine($"Authenticating user with QR data: {qrData}");
            var userData = qrData.Split(';');

            if (userData.Length == 2) // Check for valid data
            {
                string username = userData[0];
                string password = userData[1];

                // Update UI elements on the UI thread
                Application.Current.Dispatcher.Invoke(() =>
                {
                    TextBoxUsername.TextInput.Text = username; // Update username
                    PasswordBoxPassword.InputPasswordBox.Password = password; // Update password
                });

                Console.WriteLine($"Username set: {username}, Password set: {password}");
            }
            else
            {
                MessageBox.Show("Invalid QR code format. Expected 'username;password'.");
            }
        }
    }
}
