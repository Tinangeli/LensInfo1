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
using System.Windows.Media.Imaging;
using System.Drawing;
using ZXing;
using ZXing.QrCode.Internal;
using ZXing.Windows.Compatibility;
using System.IO;

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
            // Clear previous error messages
            var errorMessages = new List<string>();

            // Validate input fields
            ValidateInputs(errorMessages);

            // If there are validation errors, show them and exit the method
            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join("\n", errorMessages), "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Generate QR code
            if (!GenerateQRCode(out Bitmap qrCodeBitMap))
            {
                MessageBox.Show("Failed to generate QR Code.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Save QR code to file and update UI
            if (!SaveQRCode(qrCodeBitMap))
            {
                MessageBox.Show("Failed to save QR Code.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Insert customer data into the database
            try
            {
                Connection.Open();
                InsertCustomerData();
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

        private void ValidateInputs(List<string> errorMessages)
        {
            if (string.IsNullOrWhiteSpace(TextBoxFirstName.TextInput.Text))
                errorMessages.Add("First Name is required.");

            if (string.IsNullOrWhiteSpace(TextBoxLastName.TextInput.Text))
                errorMessages.Add("Last Name is required.");

            if (!int.TryParse(TextBoxAge.TextInput.Text, out int age) || age <= 0)
                errorMessages.Add("Please enter a valid Age (greater than 0).");

            if (string.IsNullOrWhiteSpace(TextBoxUsername.TextInput.Text))
                errorMessages.Add("Username is required.");

            if (string.IsNullOrWhiteSpace(TextBoxPassword.InputPasswordBox.Password))
                errorMessages.Add("Password is required.");
        }

        private bool GenerateQRCode(out Bitmap qrCodeBitMap)
        {
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 200,
                        Height = 200
                    }
                };

                string combinedCredentials = $"{TextBoxUsername.TextInput.Text};{TextBoxPassword.InputPasswordBox.Password}";
                qrCodeBitMap = writer.Write(combinedCredentials);
                return true;
            }
            catch
            {
                qrCodeBitMap = null;
                return false;
            }
        }

        private bool SaveQRCode(Bitmap qrCodeBitMap)
        {
            string folderPath = @"C:\Users\Chester\source\repos\Tinangeli\LensInfo1\QRCodes\CustomerQR";
            string qrFileName = $"{TextBoxUsername.TextInput.Text}.png";

            try
            {
                // Save QR code image
                string fileNamePath = Path.Combine(folderPath, qrFileName);
                qrCodeBitMap.Save(fileNamePath, System.Drawing.Imaging.ImageFormat.Png);

                // Update QR code image source for display
                var bitmapImage = new BitmapImage();
                using (var stream = new MemoryStream())
                {
                    qrCodeBitMap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }
                QRCodeImage.Source = bitmapImage;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void InsertCustomerData()
        {
            string query = "INSERT INTO Customer (Firstname, Lastname, Age, Username, Password) VALUES (@Firstname, @Lastname, @Age, @Username, @Password)";

            using (var command = new MySqlCommand(query, Connection))
            {
                command.Parameters.AddWithValue("@Firstname", TextBoxFirstName.TextInput.Text);
                command.Parameters.AddWithValue("@Lastname", TextBoxLastName.TextInput.Text);
                command.Parameters.AddWithValue("@Age", int.Parse(TextBoxAge.TextInput.Text));
                command.Parameters.AddWithValue("@Username", TextBoxUsername.TextInput.Text);
                command.Parameters.AddWithValue("@Password", TextBoxPassword.InputPasswordBox.Password);

                command.ExecuteNonQuery();
            }

            // Retrieve the last inserted Customer ID
            int lastInsertedId;
            using (var command = new MySqlCommand("SELECT CusID FROM Customer ORDER BY CusID DESC LIMIT 1;", Connection))
            {
                lastInsertedId = Convert.ToInt32(command.ExecuteScalar());
            }

            // Add the new customer to the instance
            CustomerData.Instance.Customers.Add(new Customer
            {
                IDCustomer = lastInsertedId,
                FirstName = TextBoxFirstName.TextInput.Text,
                LastName = TextBoxLastName.TextInput.Text,
                Age = int.Parse(TextBoxAge.TextInput.Text),
                Username = TextBoxUsername.TextInput.Text,
                Password = TextBoxPassword.InputPasswordBox.Password
            });
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }

    }
}
