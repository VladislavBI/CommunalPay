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
        /// Редакттирование имени файла
        /// </summary>
        /// <param name="edit">Изменение = true, удаление = false </param>
        void EditFileName(bool edit);
    }
}
