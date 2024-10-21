using Microsoft.Win32;
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
using ZXing;
using System.IO;

namespace LensInfo1
{
    
    public partial class AddMovies : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        private byte[] _posterBytes;
        public AddMovies()
        {
            InitializeComponent();
        }

        private void ButtonAddMovieImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                _posterBytes = File.ReadAllBytes(filePath); // Store image as byte array

                // Optionally, display the image
                MovieImage.Source = new BitmapImage(new Uri(filePath));
            }
        }

        private void AddButtonMovies_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connection.Open();

                // Validate MovieName
                string movieName = InputTextMovieName.TextInput.Text;
                if (string.IsNullOrWhiteSpace(movieName))
                {
                    MessageBox.Show("Please enter a valid movie name.", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                // Validate AgeLimit
                if (!int.TryParse(InputTextAgeLimit.TextInput.Text, out int ageLimit) || ageLimit < 0)
                {
                    MessageBox.Show("Please enter a valid age limit (In Numbers Only).", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                // Validate Duration
                if (!TimeSpan.TryParse(InputTextDuration.TextInput.Text, out TimeSpan duration))
                {
                    MessageBox.Show("Please enter a valid duration Format(hh:mm:ss).", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                // Validate Genre
                string genre = InputTextMovieGenre.TextInput.Text;
                if (string.IsNullOrWhiteSpace(genre))
                {
                    MessageBox.Show("Please enter a valid genre.", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                // Format Duration
                string formattedDuration = duration.ToString("hh\\:mm\\:ss");

                // Insert query
                string mySqlQuery = "INSERT INTO Movies (MovieName, AgeLimit, Duration, Genre, Poster) VALUES (@MovieName, @AgeLimit, @Duration, @Genre, @Poster);";

                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@MovieName", movieName);
                    command.Parameters.AddWithValue("@AgeLimit", ageLimit);
                    command.Parameters.AddWithValue("@Duration", formattedDuration);
                    command.Parameters.AddWithValue("@Genre", genre);
                    command.Parameters.AddWithValue("@Poster", _posterBytes); // Add poster bytes
                    command.ExecuteNonQuery();
                }
                int lastInsertedId;
                using (var command = new MySqlCommand("SELECT IDMovie FROM Movies ORDER BY IDMovie DESC LIMIT 1;", Connection))
                {
                    lastInsertedId = (int)command.ExecuteScalar();
                }
                MovieData.Instance.Movies.Add(new Movie
                {
                    IDMovie = lastInsertedId,
                    MovieName = movieName,
                    AgeLimit = ageLimit,
                    Duration = duration,
                    Genre = genre,
                    DateAdded = DateTime.Now, // Optionally set the date added
                    PosterBytes = _posterBytes
                });
                // Add the new movie to the local collection
                MessageBox.Show("Movie added successfully!");

                // Clear inputs after successful addition
                ClearInputs();
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

        private void ClearInputs()
        {
            InputTextMovieName.TextInput.Text = string.Empty;
            InputTextAgeLimit.TextInput.Text = string.Empty;
            InputTextDuration.TextInput.Text = string.Empty;
            InputTextMovieGenre.TextInput.Text = string.Empty;
            MovieImage.Source = null; // Clear image
            _posterBytes = null; // Reset poster bytes
        }

        private void AddButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window on cancel
        }
    }
}
