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
    public partial class AddDialog : Window
    {
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);

        public AddDialog()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        public void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TextBoxFirstName.TextInput.Text;
            string lastName = TextBoxLastName.TextInput.Text;
            string phoneNumber = TextBoxPhoneNum.TextInput.Text;
            string position = TextBoxPosition.TextInput.Text;
            string username = TextBoxUsername.TextInput.Text;
            string password = TextBoxPassword.InputPasswordBox.Password;

            // Input validation
            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("First name is required.", "Invalid Input", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Last name is required.", "Invalid Input", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Phone number is required.", "Invalid Input", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                MessageBox.Show("Position is required.", "Invalid Input", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username is required.", "Invalid Input", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password is required.", "Invalid Input", MessageBoxButton.OK);
                return;
            }

            // Generate QR code
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 200,
                    Height = 200
                }
            };

            string folderPath = @"C:\Users\Chester\source\repos\Tinangeli\LensInfo1\QRCodes\EmployeeQR";
            string qrFileName = $"{username}.png";
            string combinedCredentials = $"{username};{password}";

            Bitmap qrCodeBitmap = writer.Write(combinedCredentials);
            byte[] imageBytes;

            using (var ms = new MemoryStream())
            {
                qrCodeBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageBytes = ms.ToArray();
            }

            string fileNamePath = Path.Combine(folderPath, qrFileName);
            qrCodeBitmap.Save(fileNamePath, System.Drawing.Imaging.ImageFormat.Png);

            var bitmapImage = new BitmapImage();
            using (var stream = new MemoryStream())
            {
                qrCodeBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            QRCodeImage.Source = bitmapImage;

            try
            {
                Connection.Open();
                string mySqlQuery = "INSERT INTO employeesint (FirstName, LastName, PhoneNum, Position, Username, Password, QRLogin) VALUES (@FirstName, @LastName, @PhoneNumber, @Position, @Username, @Password, @QRLogin)";

                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@QRLogin", imageBytes);
                    command.ExecuteNonQuery();
                }

                int lastInsertedId;
                using (var command = new MySqlCommand("SELECT IDnum FROM employeesint ORDER BY IDnum DESC LIMIT 1;", Connection))
                {
                    lastInsertedId = Convert.ToInt32(command.ExecuteScalar());
                }

                EmployeeData.Instance.Employees.Add(new Employee
                {
                    IDNum = lastInsertedId,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Position = position,
                    Username = username,
                    Password = password,
                    QRLogin = imageBytes
                });

                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButton.OK);
            }
            catch (MySqlException ex)
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
