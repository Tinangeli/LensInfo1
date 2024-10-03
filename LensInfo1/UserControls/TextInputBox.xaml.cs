using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace LensInfo1.UserControls
{

    public partial class TextInputBox : UserControl, INotifyPropertyChanged
    {
        public TextInputBox()
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
            if (string.IsNullOrEmpty(TextInput.Text))
            {
                TextGuide.Visibility = Visibility.Visible;
            }
            else
            {
                TextGuide.Visibility = Visibility.Hidden;
            }
        }


    }
}
