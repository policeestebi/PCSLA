using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using COSEVI.CSLA.lib.accesoDatos;
using COSEVI.CSLA.lib.accesoDatos.App_Database;
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using COSEVI.CSLA.lib.entidades.mod.Administracion;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorPaginaPermiso.cs
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
  
    public class cls_gestorPaginaPermiso
    {
       /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla paginaPermiso
        /// </summary>
        /// <param name="poPaginaPermiso">PaginaPermiso a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertPaginaPermiso(cls_paginaPermiso poPaginaPermiso)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_pagina_permisoInsert";

                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_pagina", poPaginaPermiso.pPK_pagina),
                    new cls_parameter("@paramPK_permiso", poPaginaPermiso.pPK_permiso)
                };

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.PAGINA_PERMISO, poPaginaPermiso.pPK_pagina + "/" + poPaginaPermiso.pPK_permiso,poPaginaPermiso.pUsuarioTransaccion);

                return vi_resultado;

            }
            catch (Exception po_exception)
            {                
                throw new Exception("Ocurrió un error al insertar el permiso de la página.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla paginaPermiso
       /// </summary>
       /// <param name="poPaginaPermiso">PaginaPermiso a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deletePaginaPermiso(cls_paginaPermiso poPaginaPermiso)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_pagina_permisoDelete";

                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_pagina", poPaginaPermiso.pPK_pagina),
                    new cls_parameter("@paramPK_permiso", poPaginaPermiso.pPK_permiso)
                };

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.PAGINA_PERMISO, poPaginaPermiso.pPK_pagina + "/" + poPaginaPermiso.pPK_permiso, poPaginaPermiso.pUsuarioTransaccion);

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al eliminar el permiso de la página.", po_exception);
            }
        }

        /// <summary>
        /// Método que permite 
        /// obtener las asociación que exite
        /// entre las páginas y los permisos.
        /// </summary>
        /// <param name="poPagina">cls_permiso</param>
       /// <returns>List</returns>
       public static List<cls_permiso> listarPaginaPermiso(cls_pagina poPagina) 
       {
           List<cls_permiso> vo_lista = null;
           cls_permiso voPermiso = null;
           try
           {
               String vs_comando = "PA_admi_paginaPermisoSelect";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPagina", poPagina.pPK_pagina) 
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_permiso>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voPermiso = new cls_permiso();

                   voPermiso.pPK_permiso = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_permiso"]);

                   vo_lista.Add(voPermiso);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los páginas de manera filtrada.", po_exception);
           }
       }

    }
}
