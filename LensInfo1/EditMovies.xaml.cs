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
    /// Interaction logic for EditMovies.xaml
    /// </summary>
    public partial class EditMovies : Window
    {
        public event Action MovieUpdated;
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public EditMovies()
        {
            InitializeComponent();
        }


        

        private void EditMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connection.Open();

                // Parse input values
                if (!int.TryParse(TextInputIdMovieSelector.TextInput.Text, out int IdMovieSelector))
                {
                    MessageBox.Show("Please enter a valid ID for movie selection.", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                // Validate and parse IDMovie
                if (!int.TryParse(TextInputEditIdMovies.TextInput.Text, out int IDMovie))
                {
                    MessageBox.Show("Please enter a valid ID for the movie.", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                // Get the MovieName
                string MovieName = TextInputEditMovieName.TextInput.Text;

                // Validate and parse AgeLimit
                if (!int.TryParse(TextInputEditMovieAge.TextInput.Text, out int AgeLimit))
                {
                    MessageBox.Show("Please enter a valid age limit.", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                // Validate and parse Duration
                if (!TimeSpan.TryParse(TextInputEditDuration.TextInput.Text, out TimeSpan Duration))
                {
                    MessageBox.Show("Please enter a valid duration (hh:mm:ss).", "Invalid Input", MessageBoxButton.OK);
                    return; // Exit if invalid
                }

                string Genre = TextInputEditGenre.TextInput.Text;

                string duration = Duration.ToString("hh\\:mm\\:ss");

                // Update query
                string MySqlQuery = "UPDATE Movies SET IDMovie = @IDmovie, MovieName = @MovieName, AgeLimit = @AgeLimit, Duration = @Duration, Genre = @Genre WHERE IDMovie = @IdMovieSelector;";

                using (var command = new MySqlCommand(MySqlQuery, Connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@IDMovieSelector", IdMovieSelector);
                    command.Parameters.AddWithValue("@MovieName", MovieName);
                    command.Parameters.AddWithValue("@AgeLimit", AgeLimit);
                    command.Parameters.AddWithValue("@Duration", duration);
                    command.Parameters.AddWithValue("@Genre", Genre);
                    command.Parameters.AddWithValue("@IDMovie", IDMovie); // Add IDMovie for the WHERE clause

                    // Execute the update command
                    command.ExecuteNonQuery();

                    
                }
                var movieToUpdate = MovieData.Instance.Movies.FirstOrDefault(m => m.IDMovie == IdMovieSelector);
                if (movieToUpdate != null)
                {
                    movieToUpdate.IDMovie = IDMovie; // Update ID
                    movieToUpdate.MovieName = MovieName;
                    movieToUpdate.AgeLimit = AgeLimit;
                    movieToUpdate.Duration = Duration;
                    movieToUpdate.Genre = Genre;
                    
                }
                MovieUpdated?.Invoke();

                
                int lastInsertedId;
                DateTime lastDateUpdated;
                using (var command = new MySqlCommand("SELECT IDMovie FROM Movies ORDER BY IDMovie DESC LIMIT 1;", Connection))
                {
                    lastInsertedId = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand("SELECT Date_Added FROM Movies ORDER BY Date_Added DESC LIMIT 1;", Connection))
                {
                    lastDateUpdated = (DateTime)command.ExecuteScalar();
                }

                
                MessageBox.Show("Movie updated successfully!", "Success", MessageBoxButton.OK);
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
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
        
        private void ExitButtonEditMovies_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void TextInputIdMovieSelector_TextChanged(object sender, TextChangedEventArgs e)
        {
            var inputText = TextInputIdMovieSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                ClearTextBoxes();
                return;
            }

            if (!int.TryParse(inputText, out int idMovieSelector))
            {
                ClearTextBoxes();
                return;
            }

            try
            {
                Connection.Open();

                string mySqlQuery = "SELECT IDMovie, MovieName, AgeLimit, Duration, Genre FROM Movies WHERE IDMovie = @IdMovieSelector;";
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@IdMovieSelector", idMovieSelector);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PopulateMovieDetails(reader);
                        }
                        else
                        {
                            ClearTextBoxes();
                            // Optionally, show a message if desired:
                            // MessageBox.Show("No movie found with that ID.", "Not Found", MessageBoxButton.OK);
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

        private void ClearTextBoxes()
        {
            TextInputEditIdMovies.TextInput.Text = string.Empty;
            TextInputEditMovieName.TextInput.Text = string.Empty;
            TextInputEditMovieAge.TextInput.Text = string.Empty;
            TextInputEditDuration.TextInput.Text = string.Empty;
            TextInputEditGenre.TextInput.Text = string.Empty;

            TextBlockMovieName.customtextguide = "No Data";           
            TextBlockMovieAge.customtextguide = "No Data";
            TextBlockMovieDuration.customtextguide = "No Data";
            TextBlockMovieGenre.customtextguide = "No Data";
        }

        private void TextInputIdMovieSelector_LostFocus(object sender, RoutedEventArgs e)
        {
            var movieIdText = TextInputIdMovieSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(movieIdText))
            {
                ClearTextBoxes();
                return;
            }

            if (!int.TryParse(movieIdText, out int idMovieSelector))
            {
                ClearTextBoxes();
                return;
            }

            try
            {
                Connection.Open();

                string mySqlQuery = "SELECT IDMovie, MovieName, AgeLimit, Duration, Genre FROM Movies WHERE IDMovie = @IdMovieSelector;";
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@IdMovieSelector", idMovieSelector);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PopulateMovieDetails(reader);
                        }
                        else
                        {
                            ClearTextBoxes();
                            MessageBox.Show("No movie found with that ID.", "Not Found", MessageBoxButton.OK);
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

        private void PopulateMovieDetails(MySqlDataReader reader)
        {
            TextInputEditIdMovies.TextInput.Text = reader["IDMovie"].ToString();
            TextInputEditMovieName.TextInput.Text = reader["MovieName"].ToString();
            TextInputEditMovieAge.TextInput.Text = reader["AgeLimit"].ToString();
            TextInputEditDuration.TextInput.Text = ((TimeSpan)reader["Duration"]).ToString(@"hh\:mm\:ss");
            TextInputEditGenre.TextInput.Text = reader["Genre"].ToString();

            TextBlockMovieName.customtextguide = reader["MovieName"].ToString();
            TextBlockMovieAge.customtextguide = reader["AgeLimit"].ToString();
            TextBlockMovieDuration.customtextguide = ((TimeSpan)reader["Duration"]).ToString();
            TextBlockMovieGenre.customtextguide = reader["Genre"].ToString();
        }
    }
}

