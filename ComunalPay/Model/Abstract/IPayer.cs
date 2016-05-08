using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Infrastructure.Abstract
{
    interface IPayer
    {
        /// <summary>
        /// Дата платежа
        /// </summary>
        DateTime PayDate { get; set; }
        decimal PaySum { get; set; }
        float? Readings { get; set; }
        string PayType { get; set; }
    }
}
 