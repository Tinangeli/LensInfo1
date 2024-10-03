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

namespace LensInfo1.UserControls
{
    /// <summary>
    /// Interaction logic for TopButton.xaml
    /// </summary>
    public partial class TopButton : UserControl
    {
        public TopButton()
        {
            InitializeComponent();
        }
        private string contentholder;

        public string ContentHolder
        {
            get { return contentholder; }
            
            set { contentholder = value;
                    topbutton.Content = contentholder;
                }
        }

        private object topbtnhandler;

        public object TopBtnHandler
        {
            get { return topbtnhandler; }
            set { topbtnhandler = value;
                    
                    
                }
        }

        public void Button(object contentholder)
        {

        }


        
    }
}
