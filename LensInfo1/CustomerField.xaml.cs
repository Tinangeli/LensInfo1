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
using System.Windows.Threading;

namespace LensInfo1
{
    /// <summary>
    /// Interaction logic for CustomerField.xaml
    /// </summary>
    public partial class CustomerField : Window
    {
        public ObservableCollection<Movie> Movies { get; set; }
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        private int customerID;
        public DispatcherTimer timer;
        public CustomerField(int customerID, string username, string lastName)
        {
            InitializeComponent();
            Movies = MovieData.Instance.Movies;
            DataGridMovies.ItemsSource = Movies;
            UserName.Text = username;
            LastName.Text = lastName;
            this.customerID = customerID;
            LoadMovies(); 
            StartTimer();
        }
        
        public void LoadMovies()
        {
            using (var connection = new MySqlConnection(SqlConnection))
            {
                try
                {
                    connection.Open();
                    var command = new MySqlCommand("SELECT IDMovie, MovieName, AgeLimit, Duration, Genre, Poster AS PosterBytes, Price FROM Movies", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        MovieData.Instance.Movies.Clear(); // Clear existing data

                        while (reader.Read())
                        {
                            var movie = new Movie
                            {
                                IDMovie = reader.GetInt32("IDMovie"), // Add this line
                                MovieName = reader.GetString("MovieName"),
                                AgeLimit = reader.GetInt32("AgeLimit"),
                                Duration = reader.GetTimeSpan("Duration"),
                                Genre = reader.GetString("Genre"),
                                PosterBytes = reader["PosterBytes"] is DBNull ? null : (byte[])reader["PosterBytes"],
                                Price = reader.GetFloat("Price"), // Get the price from the database
                                
                            };

                            MovieData.Instance.Movies.Add(movie);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        private void ButtonPurchase_Click(object sender, RoutedEventArgs e)
        {
            var selectedMovie = DataGridMovies.SelectedItem as Movie; // Get the selected movie
            if (selectedMovie != null)
            {
                // Assuming you have already fetched the username and last name during login
                string fetchedUsername = UserName.Text;
                string fetchedLastName = LastName.Text;

                var cartWindow = new CartWindow
                {
                    MovieID = selectedMovie.IDMovie,
                    MovieName = selectedMovie.MovieName,
                    Price = selectedMovie.Price,
                    Username = fetchedUsername, // Use the fetched username here
                    LastName = fetchedLastName, // Use the fetched last name here
                    CustomerID = customerID,
                };

                cartWindow.ShowDialog(); // Show the cart window as a dialog
            }
            else
            {
                MessageBox.Show("Please select a movie.");
            }
        }

        private void ButtonClearCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridMovies.SelectedItem is Movie selectedMovie)
            {
                CustomerBuyMovieName.customtextguide = selectedMovie.MovieName;
                CustomerBuyPrice.customtextguide = selectedMovie.Price.ToString();
                CustomerBuyGenre.customtextguide = selectedMovie.Genre; // Format as needed
            }
        }

        private void ButtonCart_Click(object sender, RoutedEventArgs e)
        {

        }
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Set the timer to tick every second
            timer.Tick += Timer_Tick; // Subscribe to the Tick event
            timer.Start(); // Start the timer
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Update the TextBlock with the current date and time
                DateTimeRunning.Text = DateTime.Now.ToString("g"); // Update TextBlock directly
            }
            catch (Exception ex)
            {
                // Show an error message or log it
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the login window
            var loginWindow = new Login(); // Adjust the name to match your login window's class

            // Show the login window
            loginWindow.Show();

            // Close the current window (or hide it if you want to keep it in memory)
            this.Close(); // Or you can use this.Visibility = Visibility.Hidden; if you want to keep it in memory
        }
    }
}
