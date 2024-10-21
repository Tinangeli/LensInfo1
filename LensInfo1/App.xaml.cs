﻿using System.Windows;

namespace LensInfo1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and show the Login window
            var loginWindow = new Login();
            loginWindow.Show();
            
        }
    }

}
