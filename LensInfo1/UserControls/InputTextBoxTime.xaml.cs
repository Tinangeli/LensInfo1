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
    /// Interaction logic for InputTextBoxTime.xaml
    /// </summary>
    public partial class InputTextBoxTime : UserControl, INotifyPropertyChanged
    {
        public InputTextBoxTime()
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


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            TextInput.Clear();
            TextInput.Focus();
        }

        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var text = textBox.Text;


            // Check if input is empty to show/hide the guide
            if (string.IsNullOrEmpty(text))
            {
                TextGuide.Visibility = Visibility.Visible;
            }
            else
            {
                TextGuide.Visibility = Visibility.Hidden;
            }

            // Limit input length to 4
            if (text.Length > 5)
            {
                text = text.Substring(0, 5);
            }

            // Insert colon after the second character if applicable
            if (text.Length >= 2 && !text.Contains(":"))
            {
                text = text.Insert(2, ":");
            }

            // Update the TextBox and move the cursor to the end
            textBox.Text = text;
            textBox.SelectionStart = textBox.Text.Length; // Move cursor to the end
        }
    }
}
