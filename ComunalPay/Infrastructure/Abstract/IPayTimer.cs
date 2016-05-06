using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Infrastructure.Abstract
{
    /// <summary>
    /// Время платежа
    /// </summary>
    public interface IPayTimer
    {
        /// <summary>
        /// Дата платежа
        /// </summary>
        DateTime PayDate{ get; set; }
    }
}
