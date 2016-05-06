using ComunalPay.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSaver sav = new XmlSaver();
            EasyPay p = new EasyPay();
            p=sav.GetInfo(p.GetType());
        }
    }
}
