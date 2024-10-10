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
    /// Interaction logic for ComboBox.xaml
    /// </summary>
    public partial class PasswordBoxInput : UserControl, INotifyPropertyChanged
    {
        public PasswordBoxInput()
        {
            InitializeComponent();
        }

        private string textholder;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string TextHolder
        {
            get { return textholder; }
            set { textholder = value;
                PasswordGuide.Text = value;
                }
        }


        private void InputPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputPasswordBox.Password))
            {
                PasswordGuide.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordGuide.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            InputPasswordBox.Clear();
            InputPasswordBox.Focus();
        }
    }
}
