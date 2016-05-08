using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Infrastructure.Abstract
{
    /// <summary>
    /// Адрес платежа
    /// </summary>
    public interface IAdress
    {
        /// <summary>
        /// Город
        /// </summary>
        string Street { get; set; }
        /// <summary>
        /// Полный адрес в городе
        /// </summary>
        string Address { get; set; }
    }
}
