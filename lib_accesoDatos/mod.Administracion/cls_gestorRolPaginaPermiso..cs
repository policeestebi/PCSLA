using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using COSEVI.CSLA.lib.accesoDatos;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.App_Database;
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorRolPaginaPermiso.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Esteban Ramírez          10    01    2012    Se agrega la inserción en la bitácora.
//								
// 
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{
  
    public class cls_gestorRolPaginaPermiso
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla rolPaginaPermiso
        /// </summary>
        /// <param name="poRolPaginaPermiso">RolPaginaPermiso a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	    public static int insertRolPaginaPermiso(cls_rolPaginaPermiso poRolPaginaPermiso)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_rol_pagina_permisoInsert";

                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_rol", poRolPaginaPermiso.pPK_rol),
                    new cls_parameter("@paramPK_pagina", poRolPaginaPermiso.pPK_pagina),
                    new cls_parameter("@paramPK_permiso", poRolPaginaPermiso.pPK_permiso)
                };

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ROL_PAGINA_PERMISO, poRolPaginaPermiso.pPK_rol + "/" + poRolPaginaPermiso.pPK_pagina + "/" + poRolPaginaPermiso.pPK_permiso);
                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al insertar el rol,página,permiso.", po_exception);
            }

    }

        /// <summary>
        /// Método que permite eliminar 
        /// un registro en la tabla rolPaginaPermiso
        /// </summary>
        /// <param name="poRolPaginaPermiso">RolPaginaPermiso a eliminar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int deleteRolPaginaPermiso(cls_rolPaginaPermiso poRolPaginaPermiso)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_rol_pagina_permisoDelete";

                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_rol", poRolPaginaPermiso.pPK_rol),
                    new cls_parameter("@paramPK_pagina", poRolPaginaPermiso.pPK_pagina),
                    new cls_parameter("@paramPK_permiso", poRolPaginaPermiso.pPK_permiso)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.ROL_PAGINA_PERMISO, poRolPaginaPermiso.pPK_rol + "/" + poRolPaginaPermiso.pPK_pagina + "/" + poRolPaginaPermiso.pPK_permiso);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el rol,página,permiso.", po_exception);
            }

    }

        /// <summary>
        /// Método que permite eliminar 
        /// un registro en la tabla rolPaginaPermiso
        /// </summary>
        /// <param name="poRolPaginaPermiso">RolPaginaPermiso a eliminar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int deleteRolPaginaPermisoAll(cls_rol poRol)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_rol_pagina_permisoDeleteAll";

                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_rol", poRol.pPK_rol)
                };

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.ROL_PAGINA_PERMISO, poRol.pPK_rol.ToString());

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al eliminar los roles,páginas,permisos.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite listar 
        /// todos los permisos de un rol
        /// </summary>
        /// <returns> DataTable con los datos</returns>
        public static DataTable listarPermisosUsuario(cls_rol poRol)
        {
            try
            {
                String vs_comando = "PA_admi_rol_pagina_permisoSelectRol";
                cls_parameter[] vu_parametros = { 
                                                    new cls_parameter("@paramPK_rol", poRol.pPK_rol)
                                                };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                return vu_dataSet.Tables[0];
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los permisos de usuario en la página.", po_exception);
            }
        }
	
    }
}
