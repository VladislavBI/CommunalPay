using ComunalPay.Infrastructure.Concrete;
using ComunalPay.UI;
using ComunalPay.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void OnStartup(object sender, StartupEventArgs e)
        {

            MainWindow view = new MainWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel(new XmlSaver(), new List<EasyPay>() {
                new EasyPay() { City="Холодная Гора 190/6" ,Street="34В", PaymentType="Свет", PayDate=DateTime.Now},
                new EasyPay() { City="Холодная Гора 190/6" ,Street="35В", PaymentType="Коммунальные услуги", PayDate=DateTime.Now},
                new EasyPay() { City="Дубового 10" ,Street="10", PaymentType="Коммунальные услуги", PayDate=DateTime.Now}
            });
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
