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
    /// Interaction logic for TextBoxTextGuide.xaml
    /// </summary>
    public partial class TextBoxTextGuide : UserControl, INotifyPropertyChanged
    {
        public TextBoxTextGuide()
        {
            DataContext = this;
            InitializeComponent();
        }
        private string CustomTextGuide;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string customtextguide
        {
            get { return CustomTextGuide; }

            set
            {
                CustomTextGuide = value;
                TextGuide.Text = value;
            }
        }
    }
}
