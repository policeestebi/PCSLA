using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

using ExceptionManagement.Interfaces;

namespace ExceptionManagement.Exceptions
{
    public class ExceptionPublisher : IExceptionPublisher
    {
        private String m_LogName = @"C:\ErrorLog.txt";

        private String m_MailAddressFrom = "";
        private String m_MailNameFrom = "";

        private String m_MailAddressTo = "";
        private String m_MailNameTo = "";        

        private String m_UserName = "";
        private String m_Password = "";

        private String m_SmtpClient = "";        

        public ExceptionPublisher()
        {
        }

        // Provide implementation of the IPublishException interface
        // This contains the single Publish method.
        void IExceptionPublisher.Publish(Exception exception, NameValueCollection AdditionalInfo, NameValueCollection ConfigSettings)
        {
            // Load Config values if they are provided.
            if (ConfigSettings != null)
            {
                if (ConfigSettings["fileName"] != null && ConfigSettings["fileName"].Length > 0)
                {
                    m_LogName = ConfigSettings["fileName"];
                }
                if (ConfigSettings["mailAddressFrom"] != null && ConfigSettings["mailAddressFrom"].Length > 0)
                {
                    m_MailAddressFrom = ConfigSettings["mailAddressFrom"];
                }
                if (ConfigSettings["mailNameFrom"] != null && ConfigSettings["mailNameFrom"].Length > 0)
                {
                    m_MailNameFrom = ConfigSettings["mailNameFrom"];
                }
                if (ConfigSettings["mailAddressTo"] != null && ConfigSettings["mailAddressTo"].Length > 0)
                {
                    m_MailAddressTo = ConfigSettings["mailAddressTo"];
                }
                if (ConfigSettings["mailNameTo"] != null && ConfigSettings["mailNameTo"].Length > 0)
                {
                    m_MailNameTo = ConfigSettings["mailNameTo"];
                }
                if (ConfigSettings["userName"] != null && ConfigSettings["userName"].Length > 0)
                {
                    m_UserName = ConfigSettings["userName"];
                }
                if (ConfigSettings["password"] != null && ConfigSettings["password"].Length > 0)
                {
                    m_Password = ConfigSettings["password"];
                }
                if (ConfigSettings["smtpClient"] != null && ConfigSettings["smtpClient"].Length > 0)
                {
                    m_SmtpClient = ConfigSettings["smtpClient"];
                }                
            }

            // Create StringBuilder to maintain publishing information.
            StringBuilder strInfo = new StringBuilder();

            // Record the contents of the AdditionalInfo collection.
            if (AdditionalInfo != null)
            {
                // Record General information.
                strInfo.AppendFormat("{0}General Information{0}", Environment.NewLine);
                strInfo.AppendFormat("{0}Additonal Info:", Environment.NewLine);
                foreach (String i in AdditionalInfo)
                {
                    strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, AdditionalInfo.Get(i));
                }
            }

            // Append the exception text
            strInfo.AppendFormat("{0}{0}Exception Information{0}{1}", Environment.NewLine, exception.ToString());

            // Write the entry to the log file.   
/*            using (FileStream fs = File.Open(m_LogName, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(strInfo.ToString());
                }
            }
 */

            // send notification email if operatorMail attribute was provided
            if (m_MailAddressFrom.Length > 0 && m_MailNameFrom.Length > 0 &&
                m_MailAddressTo.Length > 0 && m_MailNameTo.Length > 0 &&
                m_UserName.Length > 0 && m_Password.Length > 0 &&
                m_SmtpClient.Length > 0)
            {
                MailAddress mailAddressFrom = new MailAddress(m_MailAddressFrom, m_MailNameFrom);
                MailAddress mailAddressTo = new MailAddress(m_MailAddressTo, m_MailNameTo);

                MailMessage mailMessage = new MailMessage();
                mailMessage.Subject = "Exception Notification";
                mailMessage.Body = strInfo.ToString();                                
                mailMessage.From = mailAddressFrom;
                mailMessage.To.Add(mailAddressTo);

                NetworkCredential networkCredential = new NetworkCredential(m_UserName, m_Password);

                SmtpClient smtpClient = new SmtpClient(m_SmtpClient);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;                
                smtpClient.Credentials = networkCredential;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                mailMessage.Dispose();
            }
        }
    }
}
