using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySql.Data.MySqlClient;



namespace LensInfo1
{
    
    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {    
        
        
        bool EmployeeSubButtonGridVisibleStatus;
        bool MoviesSubButtonGridVisibleStatus ;
        bool CustomerSubButtonGridVisibleStatus;
        bool AddButtonChangerEmployee;
        bool AddButtonChangerMovie;


        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var Register = new Register();
            Register.Show();

            var Login = new Login();
            Login.Show();

            //BINDING FOR EMPLOYEE DATA GRID
            Employees = EmployeeData.Instance.Employees;
            DataGridEmployee.ItemsSource = Employees;

            //BINDING FOR MOVIES DATA GRID
            Movies = MovieData.Instance.Movies;
            DataGridMovies.ItemsSource = Movies;


            DataGridEmployee.Visibility = Visibility.Hidden;
            DataGridMovies.Visibility = Visibility.Hidden;
            DataGridCustomer.Visibility = Visibility.Hidden;
            SelectEmployees();
            SelectMovies();
            SelectCustomer();
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
       

        private void TextInputBox_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
        public void SelectEmployees()
        {
            

            using (var connection = new MySqlConnection(SqlConnection)) {
                connection.Open();
                var command = new MySqlCommand("Select * from employeesint", connection);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        EmployeeData.Instance.Employees.Add(new Employee{
                                    IDNum = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    PhoneNumber = reader.GetString(3),
                                    Position = reader.GetString(4),
                                    Username = reader.GetString(5),
                                    Password = reader.GetString(6),

                        
                                
                         });





                    }
                } 
            }                       
            
        }
        //ANDITO KA NGAYON GETTING DATA FROM SQL AND DATA BINDING FOR DATAGRID MOVIES PAGTAPOS MO FUNC FOR ADDMOVIES.XAML
        public void SelectMovies()
        {
            using (var connection = new MySqlConnection(SqlConnection))
            {
                connection.Open();
                var command = new MySqlCommand("Select * from movies", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeSpan timespan = reader.GetTimeSpan(3);
                        //TimeSpan time = TimeOnly.FromTimeSpan(timespan);
                        DateTime datetime = reader.GetDateTime(5);
                        
                        MovieData.Instance.Movies.Add(new Movie
                        {
                            

                            IDMovie = reader.GetInt32(0),
                            MovieName = reader.GetString(1),
                            AgeLimit = reader.GetInt32(2),
                            Duration = timespan,
                            MovieDescription = reader.GetString(4),
                            DateAdded = datetime



                        }); 





                    }
                }
            }
        }

        public void SelectCustomer() {

            using (var connection = new MySqlConnection(SqlConnection))
            {
                connection.Open();
                var command = new MySqlCommand("Select * from customer", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        

                        CustomerData.Instance.Customers.Add(new Customer
                        {


                            IDCustomer = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Age = reader.GetInt32(3),
                            Username = reader.GetString(4),
                            Password = reader.GetString(5)



                        });





                    }
                }
            }

        }


        public void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataGridEmployee.Visibility = Visibility.Visible;
            DataGridMovies.Visibility = Visibility.Hidden;
            
            EmployeeSubButtonGrid.UpdateLayout();
            
                if (EmployeeSubButtonGridVisibleStatus == false)
                {
                    EmployeeSubButtonGridVisibleStatus = true;
                    MoviesSubButtonGridVisibleStatus = false;
                    CustomerSubButtonGridVisibleStatus = false;
                    EmployeeSubButtonGrid.Visibility = Visibility.Visible;
                    MoviesSubButtonGrid.Visibility = Visibility.Hidden;
                    CustomerSubButtonGrid.Visibility = Visibility.Hidden;
                    OnPropertyChanged();
                    
                }
                else { 
                        EmployeeSubButtonGridVisibleStatus = false;
                        EmployeeSubButtonGrid.Visibility = Visibility.Hidden;
                        OnPropertyChanged();
                }
                    

        }

        private void btnMovies_Click(object sender, RoutedEventArgs e)
        {            
            
            DataGridEmployee.Visibility = Visibility.Hidden;
            DataGridMovies.Visibility = Visibility.Visible;            

            if (MoviesSubButtonGridVisibleStatus == false)
            {
                MoviesSubButtonGridVisibleStatus = true;
                EmployeeSubButtonGridVisibleStatus = false;
                CustomerSubButtonGridVisibleStatus = false;
                MoviesSubButtonGrid.Visibility = Visibility.Visible;
                CustomerSubButtonGrid.Visibility = Visibility.Hidden;
                EmployeeSubButtonGrid.Visibility = Visibility.Hidden;
                OnPropertyChanged();

            }
            else
            {
                MoviesSubButtonGridVisibleStatus = false;               
                MoviesSubButtonGrid.Visibility = Visibility.Hidden;
                OnPropertyChanged();

            }



        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {

            
            if (CustomerSubButtonGridVisibleStatus == false)
            {
                CustomerSubButtonGridVisibleStatus = true;
                EmployeeSubButtonGridVisibleStatus = false;
                MoviesSubButtonGridVisibleStatus = false;
                CustomerSubButtonGrid.Visibility = Visibility.Visible;
                MoviesSubButtonGrid.Visibility = Visibility.Hidden;
                EmployeeSubButtonGrid.Visibility = Visibility.Hidden;
                OnPropertyChanged();
            }
            else
            {
                CustomerSubButtonGridVisibleStatus = false;
                CustomerSubButtonGrid.Visibility = Visibility.Hidden;
            }

        }

     

        public void OnPropertyChanged( [CallerMemberName]string propertyName = null)
            {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        private void TopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeSubButtonGridVisibleStatus == true)
            {
                AddDialog addDialog = new AddDialog();
                addDialog.Visibility = Visibility.Visible;
            }
            else if (MoviesSubButtonGridVisibleStatus == true) { 
                
                AddMovies addMovies = new AddMovies();
                addMovies.Visibility = Visibility.Visible;
            
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SelectEmployees();
            SelectMovies();
        }

        
    }
}