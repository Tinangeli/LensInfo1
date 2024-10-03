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
    
    public partial class AddMovies : Window
    {
        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);
        public AddMovies()

        {
            InitializeComponent();
        }
            protected override void OnClosing(CancelEventArgs e)
            {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
            }


        private void AddButtonMovies_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connection.Open();

                string MovieName = InputTextMovieName.TextInput.Text;
                int AgeLimit = Int32.Parse(InputTextAgeLimit.TextInput.Text);
                TimeSpan Duration = TimeSpan.Parse(InputTextDuration.TextInput.Text);
                string MovieDescription = InputTextMovieDescription.TextInput.Text;

                string duration = Duration.ToString("hh\\:mm\\:ss");

                string MySqlQuery = "Insert into Movies (MovieName, AgeLimit, Duration, MovieDescription) Values (@MovieName, @AgeLimit, @Duration, @" +
                    "MovieDescription);";

                using (var command = new MySqlCommand(MySqlQuery, Connection))
                {
                    command.Parameters.AddWithValue("@MovieName", MovieName);
                    command.Parameters.AddWithValue("@AgeLimit", AgeLimit);
                    command.Parameters.AddWithValue("@Duration", duration);
                    command.Parameters.AddWithValue("@MovieDescription", MovieDescription);
                    command.ExecuteNonQuery();


                }
                int lastInsertedId;
                DateTime lastDateUpdated;
                using (var command = new MySqlCommand("select IDMovie from Movies order by IDMovie desc limit 1;", Connection))
                {
                    lastInsertedId = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand("select Date_Added from Movies order by Date_Added desc limit 1;",Connection))
                {
                    lastDateUpdated = (DateTime)command.ExecuteScalar();
                }

                MovieData.Instance.Movies.Add(new Movie
                {
                    IDMovie = (int)lastInsertedId,
                    MovieName = MovieName,
                    AgeLimit = (int)AgeLimit,
                    Duration = (TimeSpan)Duration,
                    MovieDescription = MovieDescription,

                });

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message, "Error", MessageBoxButton.OK);



            }
            finally { Connection.Close(); }

        }
    }
}
