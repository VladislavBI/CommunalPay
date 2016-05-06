using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Infrastructure.Abstract
{
    /// <summary>
    /// Платеж человеком и на счет
    /// </summary>
    interface IPayCalculator
    {
        /// <summary>
        /// Получено денег
        /// </summary>
        decimal GetPaymant { get; set; }
        /// <summary>
        /// Положено на счет
        /// </summary>
        decimal PutOnAccount { get; set; }

        /// <summary>
        /// Разница
        /// </summary>
        decimal Difference { get;}
    }
}
