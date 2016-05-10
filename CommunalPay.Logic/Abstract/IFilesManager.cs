using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunalPay.Logic.Abstract
{
    public interface IFilesManager
    {
        string FilesName { get; set; }
        bool IsEmpty();
        void AddFileName(ref string Name);
        IEnumerable<string> GetFilesName();
        /// <summary>
        /// Проверки имени файла на недопустимые знаки и их изменение
        /// </summary>
        /// <param name="Name">Исходное имя файла</param>
        void EditFileName(ref string Name);
    }
}
