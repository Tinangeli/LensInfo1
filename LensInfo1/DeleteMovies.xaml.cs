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
    /// Interaction logic for DeleteMovies.xaml
    /// </summary>
    public partial class DeleteMovies : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public DeleteMovies()
        {
            InitializeComponent();
        }

        private void ButtonDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            var inputText = InputTextBoxDeleteIdSelector.TextInput.Text;
            var movieNameText = InputTextBoxDeleteNameSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(movieNameText) && string.IsNullOrWhiteSpace(inputText))
            {
                ClearTextBlocks();
                return;
            }

            int? idMovieSelector = null;

            try
            {
                Connection.Open();

                if (!string.IsNullOrWhiteSpace(inputText) && int.TryParse(inputText, out int id))
                {
                    // Delete by ID
                    idMovieSelector = id; // Save the ID for later use
                    string mySqlQuery = "DELETE FROM Movies WHERE IDMovie = @IdMovieSelector;";
                    using (var command = new MySqlCommand(mySqlQuery, Connection))
                    {
                        command.Parameters.AddWithValue("@IdMovieSelector", idMovieSelector);
                        command.ExecuteNonQuery();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(movieNameText))
                {
                    // Delete by Movie Name
                    string mySqlQuery = "DELETE FROM Movies WHERE MovieName = @MovieName;";
                    using (var command = new MySqlCommand(mySqlQuery, Connection))
                    {
                        command.Parameters.AddWithValue("@MovieName", movieNameText);
                        command.ExecuteNonQuery();
                    }
                }

                // Remove from the ObservableCollection
                if (idMovieSelector.HasValue)
                {
                    var movieToRemove = MovieData.Instance.Movies.FirstOrDefault(m => m.IDMovie == idMovieSelector.Value);
                    if (movieToRemove != null)
                    {
                        MovieData.Instance.Movies.Remove(movieToRemove);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(movieNameText))
                {
                    var movieToRemove = MovieData.Instance.Movies.FirstOrDefault(m => m.MovieName.Equals(movieNameText, StringComparison.OrdinalIgnoreCase));
                    if (movieToRemove != null)
                    {
                        MovieData.Instance.Movies.Remove(movieToRemove);
                    }
                }

                // Optionally notify the user of successful deletion
                MessageBox.Show("Movie deleted successfully.", "Success", MessageBoxButton.OK);
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
            
            TextBlockDeleteMovieName.customtextguide = reader["MovieName"].ToString();
            TextBlockDeleteAgeLimit.customtextguide = reader["AgeLimit"].ToString();
            TextBlockDeleteDuration.customtextguide = ((TimeSpan)reader["Duration"]).ToString(@"hh\:mm\:ss");
            TextBlockDeleteGenre.customtextguide = reader["Genre"].ToString();
        }

        private void ClearTextBlocks()
        {
            TextBlockDeleteMovieName.customtextguide = string.Empty;
            TextBlockDeleteAgeLimit.customtextguide = string.Empty;
            TextBlockDeleteDuration.customtextguide = string.Empty;
            TextBlockDeleteGenre.customtextguide = string.Empty;
            
        }

        private void InputTextBoxDeleteIdSelector_TextChanged(object sender, TextChangedEventArgs e)
        {
            var inputText = InputTextBoxDeleteIdSelector.TextInput.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                ClearTextBlocks();
                return;
            }

            if (!int.TryParse(inputText, out int idMovieSelector))
            {
                ClearTextBlocks();
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
                            ClearTextBlocks();
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

        private void InputTextBoxDeleteNameSelector_TextChanged(object sender, TextChangedEventArgs e)
        {
            var inputText = InputTextBoxDeleteNameSelector.TextInput.Text;
            if (string.IsNullOrWhiteSpace(inputText))
            {
                ClearTextBlocks();
                return;
            }

            

            try
            {
                Connection.Open();

                string mySqlQuery = "SELECT IDMovie, MovieName, AgeLimit, Duration, Genre FROM Movies WHERE MovieName = @MovieName;";
                using (var command = new MySqlCommand(mySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@MovieName", inputText);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PopulateMovieDetails(reader);
                        }
                        else
                        {
                            ClearTextBlocks();
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
    }
}
