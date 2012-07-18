using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExceptionManagement.Interfaces
{    
    /// <summary>
    /// Interface to publish exception information.  All exception information is passed as XML.
    /// </summary>
    public interface IExceptionXmlPublisher
    {
        /// <summary>
        /// Method used to publish exception information and any additional information in XML.
        /// </summary>
        /// <param name="exceptionInfo">An XML Document containing the all exception information.</param>
        /// <param name="configSettings">A collection of name/value attributes specified in the config settings.</param>
        void Publish(XmlDocument exceptionInfo, NameValueCollection configSettings);
    }
}
