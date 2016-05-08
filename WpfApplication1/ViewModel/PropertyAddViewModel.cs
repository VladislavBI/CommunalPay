using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Concrete;
using ComunalPay.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComunalPay.UI.ViewModel
{
    class PropertyAddViewModel : InotifyImplement
    {
        public string Street { get; set; }
        public string Address { get; set; }

        public ICommand AddProperty { get; set; }
        public PropertyAddViewModel()
        {
            AddProperty = new CommandClass(arg => AddPropertyCommand());
        }


        void AddPropertyCommand()
        {
            string fName = Street + "_" + Address;
            DI.FilesManager.AddFileName(ref fName);
            DI.Saver.SaveInfo(new Property() {Address=this.Address, Street=this.Street }, fName);
        }
    }
}
