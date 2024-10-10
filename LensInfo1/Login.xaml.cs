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
            string Username = TextBoxUsername.TextInput.Text;
            string Password = PasswordBoxPassword.InputPasswordBox.Password;
            using (var connection = new MySqlConnection(SqlConnection))
            {
                connection.Open();
                var command = new MySqlCommand("Select * from employeesint where Username = @Username and Password = @Password", connection);
                    command.Parameters.AddWithValue("@username", Username);
                    command.Parameters.AddWithValue("@Password", Password);
                    using (var reader = command.ExecuteReader())
                    {
                    
                        if (reader.Read())
                        {
                        var addmovies = new AddMovies();
                        addmovies.Show();
                    
                    
                    
                    
                    
                        }
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
