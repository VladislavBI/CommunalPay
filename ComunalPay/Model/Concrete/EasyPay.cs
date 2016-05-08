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
    public class EasyPay : InotifyImplement, IPayer, ICommentator
    {

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

        public decimal PaySum
        {
            get
            {
                return PaySum;
            }

            set
            {
                if (PaySum != value)
                    PaySum = value;
                OnPropertyChanged("PaySum");
            }
        }

        public float? Readings
        {
            get
            {
                return Readings;
            }

            set
            {
                if (Readings != value)
                    Readings = value;
                OnPropertyChanged("Readings");
            }
        }

        public string PayType
        {
            get
            {
                return PayType;
            }

            set
            {
                if (PayType != value)
                    PayType = value;
                OnPropertyChanged("PayType");
            }
        }

        public string Commentary
        {
            get
            {
                return Commentary;
            }

            set
            {
                if (Commentary != value)
                    Commentary = value;
                OnPropertyChanged("Commentary");
            }
        }

        public EasyPay()
        {}
    }
}
