using ComunalPay.Infrastructure.Concrete;
using ComunalPay.UI;
using ComunalPay.UI.Infrastructure;
using ComunalPay.UI.View;
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

            DI.CreateBindings();
            DI.FilesManager.FilesName = "Property.txt";
            
            

            ChooseWindow view = new ChooseWindow();
            /*ChooseWindowViewModel viewModel = new ChooseWindowViewModel(new List<Property>() {
                new Property() { Street="Холодная Гора 190/6" ,Address="34В", payList=new List<EasyPay>()},
                new Property() { Street="Холодная Гора 190/6" ,Address="35В", payList=new List<EasyPay>()},
                new Property() { Street="Дубового 10" ,Address="10", payList=new List<EasyPay>()}
            });*/

            ChooseWindowViewModel viewModel = new ChooseWindowViewModel();
            view.DataContext = viewModel;
            view.Show();

           
        }
    }
}
