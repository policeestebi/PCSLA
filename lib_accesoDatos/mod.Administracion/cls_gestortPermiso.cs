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
// cls_gestort_admi_permiso.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	Se crea la clase
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Esteban Ramírez          10    01    2012    Se agrega la inserción en la bitácora.
// 
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{
    public class cls_gestorPermiso
    {
       /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla permiso
        /// </summary>
        /// <param name="poPermiso">Permiso a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertPermiso(cls_permiso poPermiso)
        {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_admi_permisoInsert";
                    cls_parameter[] vu_parametros = 
                    {
                    	new cls_parameter("@paramnombre", poPermiso.pNombre)  
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    poPermiso.pPK_permiso = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.PERMISO, "PK_permiso"));

                    cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.PERMISO, poPermiso.pPK_permiso.ToString());

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al insertar el permiso.", po_exception);
                }
        }

       /// <summary>
        /// Método que permite actualizar 
        /// un registro en la tabla permiso
        /// </summary>
        /// <param name="poPermiso">Permiso a actualizar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updatePermiso(cls_permiso poPermiso)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_admi_permisoUpdate";
                    cls_parameter[] vu_parametros = 
                    {
                        new cls_parameter("@paramPK_permiso", poPermiso.pPK_permiso),
                 		new cls_parameter("@paramnombre", poPermiso.pNombre )
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.PERMISO, poPermiso.pPK_permiso.ToString());

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al modificar el permiso.", po_exception);
                }
        }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla permiso
       /// </summary>
       /// <param name="poPermiso">Permiso a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deletePermiso(cls_permiso poPermiso)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_admi_permisoDelete";
                    cls_parameter[] vu_parametros = 
                    {
                 	    new cls_parameter("@paramPK_permiso", poPermiso.pPK_permiso)
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.PERMISO, poPermiso.pPK_permiso.ToString());

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al eliminar el permiso.", po_exception);
                }
        }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla permiso
       /// </summary>
       /// <returns> List<cls_permiso>  valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_permiso> listarPermiso()
       {
           List<cls_permiso> vo_lista = null;
           cls_permiso voPermiso = null;
           try
           {
               String vs_comando = "PA_admi_permisoSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_permiso>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voPermiso = new cls_permiso();

                   voPermiso.pPK_permiso = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_permiso"]);

                   voPermiso.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   vo_lista.Add(voPermiso);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los permisos.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite seleccionar 
       /// un permiso específico
       /// </summary>
       /// <param name="poPermiso"></param>
       /// <returns></returns>
       public static cls_permiso seleccionarPermiso(cls_permiso poPermiso)
       {
           try
           {
               String vs_comando = "PA_admi_permisoSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_permiso", poPermiso.pPK_permiso) 
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poPermiso = new cls_permiso();

               poPermiso.pPK_permiso = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_permiso"]);

               poPermiso.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

               return poPermiso;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el permiso específico.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de permisos con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_permiso> listarPermisoFiltro(string psFiltro)
       {
           List<cls_permiso> vo_lista = null;
           cls_permiso voPermiso = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.PERMISO, string.Empty, psFiltro);

               vo_lista = new List<cls_permiso>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voPermiso = new cls_permiso();

                   voPermiso.pPK_permiso = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_permiso"]);

                   voPermiso.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   vo_lista.Add(voPermiso);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los permisos de manera filtrada.", po_exception);
           }
       }
	
    }
}

