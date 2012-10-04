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
// cls_gestorEntregable.cs
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
    public class cls_gestorEntregable
    {
       /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla entregable
        /// </summary>
        /// <param name="poEntregable">Estado a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertEntregable(cls_entregable poEntregable)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_entregableInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramcodigo", poEntregable.pCodigo),
                    new cls_parameter("@paramnombre", poEntregable.pNombre),
                    new cls_parameter("@paramdescripcion", poEntregable.pDescripcion)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                // Se obtiene el número del registro insertado.
                poEntregable.pPK_entregable = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.ENTREGABLE, "PK_entregable"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ENTREGABLE, poEntregable.pPK_entregable.ToString(),poEntregable.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el entregable.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite actualizar 
       /// un registro en la tabla entregable
       /// </summary>
       /// <param name="poEntregable">Estado a actualizar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateEntregable(cls_entregable poEntregable)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_entregableUpdate";
                cls_parameter[] vu_parametros = 
                {                 
                 	new cls_parameter("@paramPK_entregable", poEntregable.pPK_entregable),
                    new cls_parameter("@paramcodigo", poEntregable.pCodigo),
                    new cls_parameter("@paramnombre", poEntregable.pNombre),
                    new cls_parameter("@paramdescripcion", poEntregable.pDescripcion)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.ENTREGABLE, poEntregable.pPK_entregable.ToString(), poEntregable.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar el entregable.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla entregable
       /// </summary>
       /// <param name="poEntregable">Estado a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteEntregable(cls_entregable poEntregable)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_EntregableDelete";
                cls_parameter[] vu_parametros = 
                {                  
                 		new cls_parameter("@paramPK_entregable", poEntregable.pPK_entregable)  
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.ENTREGABLE, poEntregable.pPK_entregable.ToString(), poEntregable.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el entregable.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla entregable
       /// </summary>
       /// <returns> List<cls_entregable>  valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_entregable> listarEntregable()
       {
           List<cls_entregable> vo_lista = null;
           cls_entregable poEntregable = null;
           try
           {
               String vs_comando = "PA_cont_entregableSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_entregable>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poEntregable = new cls_entregable();

                   poEntregable.pPK_entregable = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_entregable"]);

                   poEntregable.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                   poEntregable.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   poEntregable.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(poEntregable);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los entregables.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla entregable
       /// </summary>
       /// <returns>poEntregable valor del resultado de la ejecución de la sentencia</returns>
       public static cls_entregable seleccionarEntregable(cls_entregable poEntregable)
       {
           try
           {
               String vs_comando = "PA_cont_entregableSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_entregable", poEntregable.pPK_entregable) 
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poEntregable = new cls_entregable();

               poEntregable.pPK_entregable = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_entregable"]);

               poEntregable.pCodigo = vu_dataSet.Tables[0].Rows[0]["codigo"].ToString();

               poEntregable.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

               poEntregable.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

               return poEntregable;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el entregable específico.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de entregables con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_entregable> listarEntregableFiltro(string psFiltro)
       {
           List<cls_entregable> vo_lista = null;
           cls_entregable voPermiso = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.ENTREGABLE, string.Empty, psFiltro);

               vo_lista = new List<cls_entregable>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voPermiso = new cls_entregable();

                   voPermiso.pPK_entregable = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_entregable"]);

                   voPermiso.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                   voPermiso.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   voPermiso.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(voPermiso);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los entregables de manera filtrada.", po_exception);
           }
       }

    }
}
