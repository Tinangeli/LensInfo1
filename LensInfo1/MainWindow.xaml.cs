using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
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
        public DispatcherTimer timer;


        MySql.Data.MySqlClient.MySqlConnection MySqlConnection;
        public static string SqlConnection = "server=127.0.0.1;uid=root;pwd=SushiiTr@sh1225;database=tindb";
        public static MySqlConnection Connection = new MySqlConnection(SqlConnection);

        public ObservableCollection<Ticket> Tickets { get; set; } = new ObservableCollection<Ticket>();

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
            StartTimer();
            NameOfUser.Text = "No Data";
            

            var editmovies = new EditMovies();
            editmovies.MovieUpdated += OnMovieUpdated;           
            var deletemovies = new DeleteMovies();           
            var delcustomer = new DeleteCustomer();
            var delEmployee = new DeleteEmployees();           
            var editEmployee = new EditEmployees();
            editEmployee.EmployeeUpdated += OnEmployeeUpdated;
            
            DataGridTickets.ItemsSource = Tickets;
            LoadTickets();

            var editCustomer = new EditCustomer();
            editCustomer.CustomerUpdated += OnCustomerUpdated;
               
           
            //BINDING FOR EMPLOYEE DATA GRID
            Employees = EmployeeData.Instance.Employees;
            DataGridEmployee.ItemsSource = Employees;

            //BINDING FOR MOVIES DATA GRID
            Movies = MovieData.Instance.Movies;
            DataGridMovies.ItemsSource = Movies;

            Customers = CustomerData.Instance.Customers;
            DataGridCustomer.ItemsSource = Customers;

            DataGridEmployee.Visibility = Visibility.Hidden;
            DataGridMovies.Visibility = Visibility.Hidden;
            DataGridCustomer.Visibility = Visibility.Hidden;
            SelectEmployees();
            SelectMovies();
            SelectCustomer();


            
        }

       

        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindow(string userName) : this() // Calls the default constructor
        {
            NameOfUser.Text = ("Welcome! " + userName); // Set the username after initialization
        }

        private void LoadTickets()
        {
            using (var connection = new MySqlConnection(SqlConnection))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT TicketID, MovieID, MovieName, Username, LastName, Price, PurchaseDate FROM Tickets", connection);

                using (var reader = command.ExecuteReader())
                {
                    Tickets.Clear();
                    while (reader.Read())
                    {
                        var ticket = new Ticket
                        {
                            TicketID = reader.GetInt32("TicketID"),
                            MovieID = reader.GetInt32("MovieID"),
                            MovieName = reader.GetString("MovieName"),
                            Username = reader.GetString("Username"),
                            LastName = reader.GetString("LastName"),
                            Price = reader.GetFloat("Price"),
                            PurchaseDate = reader.GetDateTime("PurchaseDate")
                        };
                        Tickets.Add(ticket);
                    }
                }
            }
        }

        private void UpdateTotalPrice()
        {
            float totalPrice = GetTotalPrice();
            TotalPriceSales.customtextguide = totalPrice.ToString("C2"); // Format as currency
        }
        private float GetTotalPrice()
        {
            float totalPrice = 0;

            using (var connection = new MySqlConnection(SqlConnection))
            {
                try
                {
                    connection.Open();
                    var command = new MySqlCommand("SELECT SUM(Price) AS TotalPrice FROM Tickets", connection);

                    // Execute the command and read the result
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalPrice = Convert.ToSingle(result);
                    }
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

            return totalPrice;
        }

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
                        EmployeeData.Instance.Employees.Add(new Employee
                        {
                            IDNum = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Position = reader.GetString(4),
                            Username = reader.GetString(5),
                            Password = reader.GetString(6),
                            QRLogin = reader.IsDBNull(7) ? null : reader["QRLogin"] as byte[] 
                            
                        });


                        /*if (employee.QRLogin != null)
                        {
                            using (var ms = new MemoryStream(employee.QRLogin))
                            {
                                employee.QRCodeImage = new Bitmap(ms); 
                            }
                        }

                        */
                        



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
                var command = new MySqlCommand("SELECT IDMovie, MovieName, AgeLimit, Duration, Genre, Date_Added, Price FROM Movies", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeSpan duration = reader.GetTimeSpan(3);
                        DateTime dateAdded = reader.GetDateTime(5);
                        float price = reader.GetFloat(6); // Assuming price is stored as a float in the database

                        MovieData.Instance.Movies.Add(new Movie
                        {
                            IDMovie = reader.GetInt32(0),
                            MovieName = reader.GetString(1),
                            AgeLimit = reader.GetInt32(2),
                            Duration = duration,
                            Genre = reader.GetString(4),
                            DateAdded = dateAdded,
                            Price = price // Add price here
                        });
                    }
                }
            }
        }

        public void SelectCustomers()
        {
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
                            Password = reader.GetString(5),
                            //QRLogin = reader.IsDBNull(7) ? null : reader["QRLogin"] as byte[]

                        });


                        /*if (employee.QRLogin != null)
                        {
                            using (var ms = new MemoryStream(employee.QRLogin))
                            {
                                employee.QRCodeImage = new Bitmap(ms); 
                            }
                        }

                        */




                    }
                }
            }

        }
    

        private void OnMovieUpdated()
        {
            // Refresh the DataGridMovies after a movie update
            RefreshMovies();
        }

        private void RefreshMovies()
        {
            // Clear the existing collection if needed
            MovieData.Instance.Movies.Clear();

            // Re-select movies from the database to refresh the DataGrid
            SelectMovies();
        }


        private void OnEmployeeUpdated()
        {
            RefreshEmployee();
            SelectEmployees();
        }
        private void RefreshEmployee()
        {
            EmployeeData.Instance.Employees.Clear();
        }

        private void OnCustomerUpdated()
        {
            RefreshCustomer();
            SelectCustomers();
        }

        private void RefreshCustomer()
        {
            CustomerData.Instance.Customers.Clear();
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
            DataGridCustomer.Visibility = Visibility.Hidden;
            DataGridTickets.Visibility = Visibility.Hidden;
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
            DataGridCustomer.Visibility = Visibility.Hidden;
            DataGridTickets.Visibility = Visibility.Hidden;
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
            DataGridCustomer.Visibility = Visibility.Visible;
            DataGridEmployee.Visibility = Visibility.Hidden;
            DataGridMovies.Visibility = Visibility.Hidden;
            DataGridTickets.Visibility = Visibility.Hidden;
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
            else if (MoviesSubButtonGridVisibleStatus == true)
            {

                AddMovies addMovies = new AddMovies();
                addMovies.Visibility = Visibility.Visible;

            }
            else if (CustomerSubButtonGridVisibleStatus == true)
            {
                Register customerAddDialog = new Register();
                customerAddDialog.Visibility = Visibility.Visible;
            }
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeSubButtonGridVisibleStatus == true)
            {
                EditEmployees editEmployees = new EditEmployees();
                editEmployees.Visibility = Visibility.Visible;
            }
            else if (MoviesSubButtonGridVisibleStatus == true)
            { 
                EditMovies editMovies = new EditMovies();
                editMovies.Visibility = Visibility.Visible;
            }

            else if (CustomerSubButtonGridVisibleStatus == true)
            {
                EditCustomer editCustomer = new EditCustomer();
                editCustomer.Visibility = Visibility.Visible;
            }
        }
         private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeSubButtonGridVisibleStatus == true)
            {
                DeleteEmployees deleteEmployees = new DeleteEmployees();
                deleteEmployees.Visibility = Visibility.Visible;
            }
            else if (MoviesSubButtonGridVisibleStatus == true)
            { 
                DeleteMovies deleteMovies = new DeleteMovies();
                deleteMovies.Visibility = Visibility.Visible;
            }

            else if (CustomerSubButtonGridVisibleStatus == true)
            {
                DeleteCustomer deleteCustomer = new DeleteCustomer();
                deleteCustomer.Visibility = Visibility.Visible;
            }
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
                TimeAndDataRunning.Text = DateTime.Now.ToString("g"); // Update TextBlock directly
            }
            catch (Exception ex)
            {
                // Show an error message or log it
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void CustomerSales_Click(object sender, RoutedEventArgs e)
        {
            DataGridCustomer.Visibility = Visibility.Hidden;
            DataGridEmployee.Visibility = Visibility.Hidden;
            DataGridMovies.Visibility = Visibility.Hidden;
            DataGridTickets.Visibility = Visibility.Visible;
            UpdateTotalPrice();

        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the login window
            var loginWindow = new Login(); // Adjust the name to match your login window's class

            // Show the login window
            loginWindow.Show();

           
            this.Close();
        }
    }
}