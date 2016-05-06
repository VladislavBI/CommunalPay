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
    public interface ISaver
    {
        
        void SaveInfo(dynamic d);
        dynamic GetInfo(Type t);

        bool IsEmpty(); 
    }
}
