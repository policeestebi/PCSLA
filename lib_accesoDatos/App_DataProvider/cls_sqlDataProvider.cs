using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.SqlClient;

namespace COSEVI.CSLA.lib.accesoDatos.App_DataProvider
{
    class cls_sqlDataProvider : itf_dataProvider
    {
        public IDbConnection connection(String ps_stringConnection)
        {
            return new SqlConnection(ps_stringConnection);
        }

        public IDbDataAdapter adapter()
        {
            return new SqlDataAdapter();
        }

        public IDbDataAdapter adapter(String ps_command, IDbConnection pu_connection)
        {
            return new SqlDataAdapter(ps_command, (SqlConnection)pu_connection);
        }

        public IDbCommand command(String ps_command)
        {
            return new SqlCommand(ps_command);
        }

        public IDataParameter parameter(cls_parameter parameter)
        {
            SqlParameter param = new SqlParameter(parameter.Nombre, parameter.Valor);

            if (parameter.Direccion == null)
                param.Direction = parameter.Direccion;

            return param;
        }
    }
}
