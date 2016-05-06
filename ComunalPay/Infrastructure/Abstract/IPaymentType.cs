using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Infrastructure.Abstract
{
    /// <summary>
    /// За какую услугу оплачено
    /// </summary>
    public interface IPaymentType
    {
        /// <summary>
        /// Тип платежа
        /// </summary>
        string PaymentType { get; set; }
        /// <summary>
        /// Показания
        /// </summary>
        float Readings { get; set; }
    }
}
