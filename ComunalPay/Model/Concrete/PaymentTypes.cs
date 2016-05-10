using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Model.Concrete
{
    [Serializable]
    public class PaymentTypes:Collection<PaymentType>
    {

        public IEnumerable<string> GetAllNames
        {
            get {
                    List<string> temp = new List<string>();
                    foreach (var item in Items)
                    {
                    temp.Add(item.PayName);
                    }
                    return temp;
                }
            set
            {
                if(GetAllNames!=value)
                GetAllNames = value;
            }
        }
    }
}
