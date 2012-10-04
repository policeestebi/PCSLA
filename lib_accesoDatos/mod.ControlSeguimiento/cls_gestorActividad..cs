using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using COSEVI.CSLA.lib.accesoDatos;
using COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.accesoDatos.App_Database;
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorActividad.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	11    01    2012	Se agrega la modifica la inserción en la bitácora.
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorActividad
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla estado
        /// </summary>
        /// <param name="poActividad">Actividad a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertActividad(cls_actividad poActividad)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_actividadInsert";
                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramcodigo", poActividad.pCodigo),
                        new cls_parameter("@paramnombre", poActividad.pNombre),
                        new cls_parameter("@paramdescripcion", poActividad.pDescripcion)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                // Se obtiene el número del registro insertado.
                poActividad.pPK_Actividad = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.ACTIVIDAD, "PK_actividad"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ACTIVIDAD, poActividad.pPK_Actividad.ToString(),poActividad.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar la actividad.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite actualizar 
       /// un registro en la tabla Actividad
       /// </summary>
       /// <param name="poActividad">Actividad a actualizar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateActividad(cls_actividad poActividad)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_actividadUpdate";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_actividad", poActividad.pPK_Actividad),
                    new cls_parameter("@paramFK_estado", poActividad.pCodigo),
                    new cls_parameter("@paramnombre", poActividad.pNombre),
                    new cls_parameter("@paramdescripcion", poActividad.pDescripcion)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.ACTIVIDAD, poActividad.pPK_Actividad.ToString(), poActividad.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar la actividad.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla Actividad
       /// </summary>
       /// <param name="poActividad">Actividad a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteActividad(cls_actividad poActividad)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_actividadDelete";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_actividad", poActividad.pPK_Actividad)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.ACTIVIDAD, poActividad.pPK_Actividad.ToString(), poActividad.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar la actividad.", po_exception);
            }

       }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla actividad
       /// </summary>
       /// <returns> List<cls_actividad>  valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_actividad> listarActividad()
       {
           List<cls_actividad> vo_lista = null;
           cls_actividad poActividad = null;
           try
           {
               String vs_comando = "PA_cont_actividadSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_actividad>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poActividad = new cls_actividad();

                   poActividad.pPK_Actividad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_actividad"]);

                   poActividad.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                   poActividad.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   poActividad.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(poActividad);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las actividades.", po_exception);
           }

       }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla estado
       /// </summary>
       /// <returns>poActividad valor del resultado de la ejecución de la sentencia</returns>
       public static cls_actividad seleccionarActividad(cls_actividad poActividad)
       {
           try
           {
               String vs_comando = "PA_cont_actividadSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_actividad", poActividad.pPK_Actividad)
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poActividad = new cls_actividad();

                   poActividad.pPK_Actividad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_actividad"]);

                   poActividad.pCodigo = vu_dataSet.Tables[0].Rows[0]["codigo"].ToString();

                   poActividad.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

                   poActividad.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

               return poActividad;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al seleccionar la actividad específica.", po_exception);
           }

       }

       /// <summary>
       /// Hace un lista de permisos con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_actividad> listarActividadFiltro(string psFiltro)
       {
           List<cls_actividad> vo_lista = null;
           cls_actividad voActividad = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.ACTIVIDAD, string.Empty, psFiltro);

               vo_lista = new List<cls_actividad>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voActividad = new cls_actividad();

                   voActividad.pPK_Actividad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_actividad"]);

                   voActividad.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                   voActividad.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   voActividad.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(voActividad);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las actividades de manera filtrada.", po_exception);
           }
       }

        /// <summary>
        /// Obtiene la lista de actividades
        /// a las que esta asociado un usuario.
        /// Sólo aquellas que se encuentren 
        /// activas.
        /// </summary>
        /// <param name="psUsuario">String código del usuario.</param>
        /// <param name="psProyecto">String código del usuario</param>
        /// <returns></returns>
       public static DataSet listarActividadesUsuario(string psUsuario, string psProyecto)
       {
           DataSet vu_dataSet = null;
           try
           {
               String vs_comando = "PA_cont_actividadSelectUsuario";
               cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramUsuario", psUsuario),
                    new cls_parameter("@paramProyecto", psProyecto)
                };

               vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);
           }
           catch (Exception po_exception)
           {
               cls_sqlDatabase.rollbackTransaction();
               throw new Exception("Ocurrió un error al obtener el listado de los actividades.", po_exception);
           }

           return vu_dataSet;
       }

	
    }
}
