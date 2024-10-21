using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public float Price { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public int CustomerID { get; set; }
        public ObservableCollection<Ticket> Tickets { get; set; } = new ObservableCollection<Ticket>();


        public CartWindow()
        {
            InitializeComponent();
            // Bind the data to your TextBoxes or other UI elements here
            DataContext = this; // Set DataContext to bind properties directly
        }

        private void ButtonPurchaseCartWindow_Click(object sender, RoutedEventArgs e)
        {

            InsertTicket();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InsertTicket()
        {
            using (var connection = new MySqlConnection(SqlConnection))
            {
                try
                {
                    connection.Open();
                    var command = new MySqlCommand("INSERT INTO Tickets (MovieID, MovieName, Username, LastName, Price, PurchaseDate, CustomerID) VALUES (@MovieID, @MovieName, @Username, @LastName, @Price, @PurchaseDate, @CusID); SELECT LAST_INSERT_ID();", connection);
                    command.Parameters.AddWithValue("@MovieID", MovieID);
                    command.Parameters.AddWithValue("@MovieName", MovieName);
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                    command.Parameters.AddWithValue("@CusID", CustomerID);

                    var ticketId = Convert.ToInt32(command.ExecuteScalar());
                    // Use ticketId as needed
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            // Populate the Tickets collection here if needed
            var ticket = new Ticket
            {
                
                MovieName = MovieName,
                Username = Username,
                LastName = LastName,
                Price = Price,
                PurchaseDate = DateTime.Now
            };
            Tickets.Add(ticket);
        }
    } 
}
