using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib_envioCorreo
{
    public class cls_correo
    {
        #region Atributos

        private String cs_direccionCorreoEnviante;
        private String cs_nombreCorreoEnviante;

        private String cs_direccionCorreoDestinatario;
        private String cs_nombreCorreoDestinatario;

        private String cs_asunto;
        private String cs_cuerpo;

        #endregion

        #region Propiedades

        public String DireccionCorreoEnviante
        {
            get { return cs_direccionCorreoEnviante; }
            set { cs_direccionCorreoEnviante = value; }
        }

        public String NombreCorreoEnviante
        {
            get { return cs_nombreCorreoEnviante; }
            set { cs_nombreCorreoEnviante = value; }
        }

        public String DireccionCorreoDestinatario
        {
            get { return cs_direccionCorreoDestinatario; }
            set { cs_direccionCorreoDestinatario = value; }
        }

        public String NombreCorreoDestinatario
        {
            get { return cs_nombreCorreoDestinatario; }
            set { cs_nombreCorreoDestinatario = value; }
        }

        public String Asunto
        {
            get { return cs_asunto; }
            set { cs_asunto = value; }
        }

        public String Cuerpo
        {
            get { return cs_cuerpo; }
            set { cs_cuerpo = value; }
        }

        #endregion

        #region Constructor

        public cls_correo()
        {
        }

        #endregion
    }
}
