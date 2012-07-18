using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace COSEVI.CSLA.lib.accesoDatos
{
    interface itf_dataProvider
    {
        IDbConnection connection(String ps_stringConnection);
        IDbDataAdapter adapter();
        IDbDataAdapter adapter(String ps_command, IDbConnection pu_connection);
        IDbCommand command(String sp_command);
        IDataParameter parameter(cls_parameter pu_parameter);     
    }
}
