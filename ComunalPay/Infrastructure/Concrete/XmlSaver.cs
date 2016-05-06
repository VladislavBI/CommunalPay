using ComunalPay.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ComunalPay.Infrastructure.Concrete
{
   public class XmlSaver : ISaver 
    {

        public void SaveInfo(dynamic d)
        {
            Type t = d.GetType();
            XmlSerializer formatter = new XmlSerializer(t);

            using (FileStream fs=new FileStream("person.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, d);
            }
        }

        public dynamic GetInfo(Type t)
        {
            
            XmlSerializer formatter = new XmlSerializer(t);
            IEnumerable<EasyPay> New;
            using (FileStream fs = new FileStream("person.xml", FileMode.Open))
            {
                New = (IEnumerable<EasyPay>)formatter.Deserialize(fs);
                
            }
            return New;
        }

        public bool IsEmpty()
        {
            
                if (!new FileInfo("person.xml").Exists 
                || new FileInfo("person.xml").Length==0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
        }
    }
}
