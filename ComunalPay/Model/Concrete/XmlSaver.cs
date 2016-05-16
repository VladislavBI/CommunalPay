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
   public class XmlSaver<T> : ISaver<T> 
    {

        public void SaveInfo(T obj, string fName)
        {
            
            if (!fName.Contains(".xml"))
            {
                fName += ".xml";
            }

            Type t = obj.GetType();
            XmlSerializer formatter = new XmlSerializer(t);

            using (FileStream fs=new FileStream(fName, FileMode.Create))
            {
                formatter.Serialize(fs, obj);
            }
        }

        public dynamic GetInfo(T obj, string fName)
        {
            if (!fName.Contains(".xml"))
            {
                fName += ".xml";
            }

            XmlSerializer formatter = new XmlSerializer(obj.GetType());
            dynamic New;
            using (FileStream fs = new FileStream(fName, FileMode.Open))
            {
                //Convert.ChangeType(formatter.Deserialize(fs), t)

                //Если типы не сходяться


                try
                {
                    New = (T)formatter.Deserialize(fs);
                }
                catch (Exception)
                {
                    return null;
                }              
            }
                return New;
        }

        public bool IsEmpty(string fName)
        {
            if (!fName.Contains(".xml"))
            {
                fName += ".xml";
            }

            if (!new FileInfo(fName).Exists 
                || new FileInfo(fName).Length==0)
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
