using ComunalPay.Infrastructure.Concrete;
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
using System.Windows.Shapes;

namespace ComunalPay.UI.View
{
    /// <summary>
    /// Interaction logic for StatisticWindowView.xaml
    /// </summary>
    public partial class StatisticWindowView : Window
    {
        public StatisticWindowView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var se = dgt.SelectedValue;
        }
    }
}
