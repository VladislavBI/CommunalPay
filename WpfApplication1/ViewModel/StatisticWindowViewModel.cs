using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Concrete;
using ComunalPay.Model.Concrete;
using ComunalPay.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.UI.ViewModel
{
    class StatisticWindowViewModel: InotifyImplement
    {
        /// <summary>
        /// текущая недвижимость
        /// </summary>
        Property curProp;
        /// <summary>
        /// название файла недвижимости
        /// </summary>
        string propFileName;

        public ObservableCollection<string> AllPaymentsT { get; set; }

        public PaymentTypes PaymentT { get; set; }
        public string curFullAddress
        {
            get
            {
                if (curProp != null)
                    return curProp.Address + " " + curProp.Street;
                else
                {
                    return null;
                }
            }
            set { curFullAddress = value; }
        }
        public StatisticWindowViewModel()
        {}
        public StatisticWindowViewModel(string selAddress, string selStreet)
        {
            //получить текущую недвижимость
            propFileName = selAddress + "_" + selStreet;
            DI.FilesManager.EditFileName(ref propFileName);
            curProp = DI.Saver.GetInfo(new Property(), propFileName);

            AllPaymentsT = new ObservableCollection<string>();
            CreatePaymentsList();
        }

        private void CreatePaymentsList()
        {
            if (!DI.Saver.IsEmpty("PaymantsTypes"))
                PaymentT = DI.Saver.GetInfo(new PaymentTypes(), "PaymantsTypes");

            foreach (var item in PaymentT)
            {
                var payments = curProp.payList.Where(x => x.PayType == item.PayName);
                if (item.haveBill)
                {
                    decimal plus = payments.Where(x => x.IncomingBill == false).Select(x => x.PaySum).Sum();
                    decimal minus = payments.Where(x => x.IncomingBill == true).Select(x => x.PaySum).Sum();

                    AllPaymentsT.Add(item.PayName + ": " + (plus - minus).ToString());
                }
                else
                {
                    decimal sum = payments.Select(x => x.PaySum).Sum();
                    AllPaymentsT.Add(item.PayName + ": " + sum.ToString());
                }
            }
        }
    }
}
