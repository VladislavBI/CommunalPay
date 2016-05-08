using CommunalPay.Logic.Abstract;
using CommunalPay.Logic.Concrete;
using ComunalPay.Infrastructure.Abstract;
using ComunalPay.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunalPay.UI.Infrastructure
{
    public static class DI
    {
        public static ISaver<dynamic> Saver { get; set; }
        public static IFilesManager FilesManager { get; set; }

        public static void CreateBindings()
        {
            Saver = new XmlSaver<dynamic>();
            FilesManager = new TXTFileManager();
        }
    }
}
