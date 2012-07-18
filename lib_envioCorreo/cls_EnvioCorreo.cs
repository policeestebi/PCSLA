using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Resources;
using System.Configuration;
using System.Reflection;

using ExceptionManagement;
using ExceptionManagement.SectionHandler;
using ExceptionManagement.Exceptions;
using ExceptionManagement.SectionHandler.Enums;

namespace lib_envioCorreo
{
    public class cls_EnvioCorreo
    {
        private String cs_clienteSmtp;
        private String cs_nombreUsuario;
        private String cs_contrasennaUsuario;

        public cls_EnvioCorreo()
        {
            ExceptionManagementSettings vu_conf = (ExceptionManagementSettings)ConfigurationManager.GetSection("exceptionManagement");
            foreach(PublisherSettings vu_publisher in vu_conf.Publishers)
            {
                if (vu_publisher.Mode == PublisherMode.On)
                {
                    if (vu_publisher.OtherAttributes["userName"] != null && vu_publisher.OtherAttributes["userName"] != "")
                    {
                        cs_clienteSmtp = vu_publisher.OtherAttributes["smtpClient"];
                        cs_nombreUsuario = vu_publisher.OtherAttributes["userName"];
                        cs_contrasennaUsuario = vu_publisher.OtherAttributes["password"];
                    }
                }
            }
        }

        public void enviarCorreo(cls_correo pu_correo)
        {
            try
            {
                MailAddress vu_correoDe = new MailAddress(pu_correo.DireccionCorreoEnviante, pu_correo.NombreCorreoEnviante);
                MailAddress vu_correoPara = new MailAddress(pu_correo.DireccionCorreoDestinatario, pu_correo.NombreCorreoDestinatario);

                MailMessage vu_mensaje = new MailMessage();
                vu_mensaje.Subject = pu_correo.Asunto;
                vu_mensaje.Body = pu_correo.Cuerpo;
                vu_mensaje.From = vu_correoDe;
                vu_mensaje.To.Add(vu_correoPara);

                NetworkCredential vu_networkCredential = new NetworkCredential(cs_nombreUsuario, cs_contrasennaUsuario);

                SmtpClient vu_smtpClient = new SmtpClient(cs_clienteSmtp);
                vu_smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                vu_smtpClient.Credentials = vu_networkCredential;
                vu_smtpClient.EnableSsl = true;
                vu_smtpClient.Send(vu_mensaje);
                vu_mensaje.Dispose();

            }
            catch (Exception vu_exception)
            {
                //throw vu_exception;
            }
        }
    }
}
