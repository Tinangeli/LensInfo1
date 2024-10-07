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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace LensInfo1.UserControls
{    
    public partial class InputPasswordBox : UserControl
    {
        public InputPasswordBox()
        {
            InitializeComponent();
        }
        private string TextHolder;

        public string textholder
        {
            get { return TextHolder; }
            set
            {
                TextHolder = value;
                TextBlockPasswordGuide.Text = value;
            }
        }







        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            PasswordBoxInput.Focus();
            PasswordBoxInput.Clear();

        }

        private void PasswordBoxInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBoxInput.Password))
            {
                TextBlockPasswordGuide.Visibility = Visibility.Visible;
            }
            else
            {
                TextBlockPasswordGuide.Visibility = Visibility.Hidden;
            }

        }
    }
}
