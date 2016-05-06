using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Abstract;

namespace ComunalPay.Infrastructure.Concrete
{
    [Serializable]
    public class EasyPay : InotifyImplement, IAdress,
        IPayCalculator, IPaymentType, IPayTimer
    {
        private string city;
        public string City
        {
            get
            {
                return city;
            }

            set
            {
                if (city != value)
                    city = value;
                OnPropertyChanged("City");
            }
        }

        private string street;
        public string Street
        {
            get
            {
                return street;
            }

            set
            {
                if (street != value)
                    street = value;
                OnPropertyChanged("Street");
            }
        }

        private string paymentType;
        public string PaymentType
        {
            get
            {
                return paymentType;
            }

            set
            {
                if (paymentType != value)
                    paymentType = value;
                OnPropertyChanged("PaymentType");
            }
        }
        private float readings;
        public float Readings
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
        
        
        public decimal Difference
        {
            get
            {
                return GetPaymant-PutOnAccount;
            }
        }
        private decimal getPaymant;
        public decimal GetPaymant
        {
            get
            {
                return getPaymant;
            }

            set
            {
                if (getPaymant != value)
                    getPaymant = value;
                OnPropertyChanged("GetPaymant");
            }
        }
        private decimal putOnAccount;

        public decimal PutOnAccount
        {
            get
            {
                return putOnAccount;
            }

            set
            {
                if (putOnAccount != value)
                    putOnAccount = value;
                OnPropertyChanged("PutOnAccount");
            }
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

        public EasyPay()
        {}
    }
}
