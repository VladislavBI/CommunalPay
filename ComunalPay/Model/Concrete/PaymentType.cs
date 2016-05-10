using ComunalPay.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Model.Concrete
{
    public class PaymentType: InotifyImplement
    {
        string payName;
        public string PayName
        {
            get
            {
                return payName;
            }

            set
            {
                if (payName != value)
                    payName = value;
                OnPropertyChanged("PayName");
            }
        }
        public bool haveDate=false;
        public bool haveSum = false;
        public bool haveName = false;
        public bool haveReadings = false;
        public bool haveBill = false;
        public bool haveComment = false;


        
    }
}
