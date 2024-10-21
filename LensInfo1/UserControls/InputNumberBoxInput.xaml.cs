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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LensInfo1.UserControls
{
    /// <summary>
    /// Interaction logic for InputNumberBoxInput.xaml
    /// </summary>
    public partial class InputNumberBoxInput : UserControl, INotifyPropertyChanged
    {
        public InputNumberBoxInput()
        {
            DataContext = this;
            InitializeComponent();
        }
        private string TxtHolder;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string txtholder
        {
            get { return TxtHolder; }

            set
            {
                TxtHolder = value;
                TextGuide.Text = value;
            }
        }

        private string CheckBoxContentHolder;

        public string  checkboxcontentholder
        {
            get { return CheckBoxContentHolder; }
            set {   
                    CheckBoxContentHolder = value;
                    CheckBoxCustomID.Content = value;
                }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            TextInput.Clear();
            TextInput.Focus();
        }

        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextInput.Text))
            {
                TextGuide.Visibility = Visibility.Visible;
            }
            else
            {
                TextGuide.Visibility = Visibility.Hidden;
            }
        }

        private void CheckBoxCustomID_Checked(object sender, RoutedEventArgs e)
        {
            TextInput.IsEnabled = true;
            TextInput.Focus();
            TextInput.BorderBrush = Brushes.Wheat;
        }

        private void CheckBoxCustomID_Unchecked(object sender, RoutedEventArgs e)
        {
            TextInput.IsEnabled = false;
            TextInput.BorderBrush = Brushes.Gray;
        }
    }
}
