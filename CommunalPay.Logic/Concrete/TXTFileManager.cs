using CommunalPay.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunalPay.Logic.Concrete
{
   public class TXTFileManager : IFilesManager
    {
        string filesName;
        public string FilesName
        {
            get
            {
                return filesName;
            }

            set
            {
                filesName = value;
            }
        }

        public void AddFileName(ref string Name)
        {
            using (StreamWriter write = new StreamWriter(FilesName, true, System.Text.Encoding.Default))
            {
                EditFileName(ref Name);
                write.WriteLine(Name);
            }
        }

        public void EditFileName(ref string Name)
        {
            if (Name.Contains("/") || Name.Contains(@"\"))
            {
                Name = Name.Replace(@"\", "#");
                Name = Name.Replace("/", "#");
            }
        }

        public IEnumerable<string> GetFilesName()
        {
            List<string> temp = new List<string>();
            using (StreamReader reader=new StreamReader(FilesName, System.Text.Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    temp.Add(reader.ReadLine());
                }
            }

            return temp; 
        }

        public bool IsEmpty()
        {
            if (!new FileInfo(FilesName).Exists)
            {
                File.Create(FilesName);
                return true;
            }
            else
            {
                if (new FileInfo(FilesName).Length == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
