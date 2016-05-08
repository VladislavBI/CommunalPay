using ComunalPay.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.Infrastructure.Abstract
{/// <summary>
 /// Запись и загрузка информации
 /// </summary>
    public interface ISaver<T>
    {
        
        void SaveInfo(T obj, string fName);
        dynamic GetInfo(T obj, string fName);

        bool IsEmpty(string fName); 
    }
}
