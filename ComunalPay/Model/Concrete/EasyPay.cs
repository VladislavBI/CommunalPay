using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Abstract;
using ComunalPay.Model.Abstract;

namespace ComunalPay.Infrastructure.Concrete
{
    [Serializable]
    public class EasyPay : InotifyImplement, IPayer, ICommentator, IComparable, IInvestor
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        DateTime payDate;
        public DateTime PayDate
        {
            get
            {
                return payDate;
            }

            set
            {
                if (payDate != value)
                    payDate = value;
                OnPropertyChanged("PayDate");
            }
        }

        decimal paySum;
        public decimal PaySum
        {
            get
            {
                return paySum;
            }

            set
            {
                if (paySum != value)
                    paySum = value;
                OnPropertyChanged("PaySum");
            }
        }

        float? readings;
        public float? Readings
        {
            get
            {
                return readings;
            }

            set
            {
                if (readings != value)
                    readings = value;
                OnPropertyChanged("Readings");
            }
        }

        string payType;
        public string PayType
        {
            get
            {
                return payType;
            }

            set
            {
                if (payType != value)
                    payType = value;
                OnPropertyChanged("PayType");
            }
        }

        string commentary;
        public string Commentary
        {
            get
            {
                return commentary;
            }

            set
            {
                if (commentary != value)
                    commentary = value;
                OnPropertyChanged("Commentary");
            }
        }

        bool incomingBill;
        public bool IncomingBill
        {
            get
            {
                return incomingBill;
            }

            set
            {
                if (incomingBill != value)
                    incomingBill = value;
                OnPropertyChanged("IncomingBill");
            }
        }
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

        public EasyPay()
        {}
        
        public int CompareTo(object obj)
        {
            return payDate.CompareTo(obj);
        }

        
    }
}
