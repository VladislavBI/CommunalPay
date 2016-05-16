using ComunalPay.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Infrastructure.Concrete
{
    [Serializable]
    public class Property : InotifyImplement, IAdress, ICommentator
    {
        private int idProp;

        public int IdProp
        {
            get { return idProp; }
            set
            {
                idProp = value;
                OnPropertyChanged("IdProp");
            }
        }

        string address;
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                if (address != value)
                    address = value;
                OnPropertyChanged("Address");
            }
        }
        string street;
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

        public List<EasyPay> payList { get; set; }

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

       
    }
}
