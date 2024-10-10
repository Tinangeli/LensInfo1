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
            string FirstName = TextBoxFirstName.TextInput.Text;
            string LastName = TextBoxLastName.TextInput.Text;
            string PhoneNumber = TextBoxPhoneNum.TextInput.Text;
            string Position = TextBoxPosition.TextInput.Text;
            string Username = TextBoxUsername.TextInput.Text;
            string Password = TextBoxPassword.InputPasswordBox.Password;

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 200,
                    Height = 200
                }
            };

            string FolderPath = @"C:\Users\Chester\source\repos\Tinangeli\LensInfo1\QRCodes\EmployeeQR";
            string QRFileName = $"{Username}.png";
            string CombinedCredentials = $"{Username};{Password}";

            Bitmap qrCodeBitMap = writer.Write(CombinedCredentials);

            byte[] imagebytes;
            using (var ms = new MemoryStream())
            {
                qrCodeBitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imagebytes = ms.ToArray();

            }


            string FileNamePath = Path.Combine(FolderPath, QRFileName);
            qrCodeBitMap.Save(FileNamePath, System.Drawing.Imaging.ImageFormat.Png);

            var bitmapImage = new BitmapImage();
            using (var stream = new System.IO.MemoryStream())
            {
                qrCodeBitMap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
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
                
                string MySqlQuery = "INSERT INTO employeesint (FirstName, LastName, PhoneNum, Position, Username, Password, QRLogin) VALUES (@FirstName, @LastName, @PhoneNumber, @Position, @Username, @Password, @QRLogin)";

                using (var command = new MySqlCommand(MySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.Parameters.AddWithValue("@Position", Position);
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@QRLogin", imagebytes);
                    command.ExecuteNonQuery();
                }
                int lastInsertedId;
                using (var command = new MySqlCommand("select idnum from employeesint order by IDnum desc limit 1;", Connection))
                {
                    lastInsertedId = (int)command.ExecuteScalar(); 
                }

                
                EmployeeData.Instance.Employees.Add(new Employee
                {
                    IDNum = (int)lastInsertedId,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    Position = Position,
                    Username = Username,
                    Password = Password,
                    QRLogin = imagebytes
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
