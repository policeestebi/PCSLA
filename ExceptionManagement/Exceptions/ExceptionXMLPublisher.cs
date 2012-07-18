using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using ExceptionManagement.Interfaces;

namespace ExceptionManagement.Exceptions
{
    public class ExceptionXMLPublisher : IExceptionXmlPublisher
    {
        void IExceptionXmlPublisher.Publish(XmlDocument ExceptionInfo, NameValueCollection ConfigSettings)
        {
            String filename;
            if (ConfigSettings != null)
            {
                filename = ConfigSettings["fileName"];
            }
            else
            {
                filename = @"C:\ErrorLog.xml";
            }
            using (FileStream fs = File.Open(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(ExceptionInfo.OuterXml);
                }
            }
        }
    }
}
