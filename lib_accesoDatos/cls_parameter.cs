using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

namespace COSEVI.CSLA.lib.accesoDatos
{
    public class cls_parameter
    {
        private String cs_nombre;

        public String Nombre
        {
            get { return cs_nombre; }
            set { cs_nombre = value; }
        }

        private Object co_valor;

        public Object Valor
        {
            get { return co_valor; }
            set { value = co_valor; }
        }

        public cls_parameter(String ps_nombre, Object po_valor)
        {
            this.cs_nombre = ps_nombre;
            this.co_valor = po_valor;
        }

        public cls_parameter(String ps_nombre, Object po_valor, ParameterDirection po_direccion)
        {
            this.cs_nombre = ps_nombre;
            this.co_valor = po_valor;
            this.direccion = po_direccion;
        }

        private ParameterDirection direccion;

        public ParameterDirection Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        
    }
}
