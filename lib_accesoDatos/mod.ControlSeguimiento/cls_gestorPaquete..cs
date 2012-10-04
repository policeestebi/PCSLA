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
// cls_gestorPaquete.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            17    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	11    01    2012	Se agrega la modifica la inserción en la bitácora.
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorPaquete
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla Paquete
        /// </summary>
        /// <param name="poPaquete">Proyecto a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertPaquete(cls_paquete poPaquete)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_paqueteInsert";
                cls_parameter[] vu_parametros = 
                {                   
                        new cls_parameter("@paramcodigo", poPaquete.pCodigo),
                        new cls_parameter("@paramnombre", poPaquete.pNombre),
                        new cls_parameter("@paramdescripcion", poPaquete.pDescripcion)  
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                // Se obtiene el número del registro insertado.
                poPaquete.pPK_Paquete = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.PAQUETE, "PK_paquete"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.PAQUETE, poPaquete.pPK_Paquete.ToString(), poPaquete.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el paquete.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite actualizar 
       /// un registro en la tabla Paquete 
       /// </summary>
       /// <param name="poPaquete">Paquete a actualizar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updatePaquete(cls_paquete poPaquete)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_cont_paqueteUpdate";
                    cls_parameter[] vu_parametros = 
                    {                   
                 		    new cls_parameter("@paramPK_paquete", poPaquete.pPK_Paquete),
                            new cls_parameter("@paramcodigo", poPaquete.pCodigo),
                            new cls_parameter("@paramnombre", poPaquete.pNombre),
                            new cls_parameter("@paramdescripcion", poPaquete.pDescripcion)                   
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.PAQUETE, poPaquete.pPK_Paquete.ToString(), poPaquete.pUsuarioTransaccion);

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al modificar el paquete.", po_exception);
                }

        }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla Paquete 
       /// </summary>
       /// <param name="poPaquete">Paquete a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deletePaquete(cls_paquete poPaquete)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_cont_paqueteDelete";
                    cls_parameter[] vu_parametros = 
                    {
                        new cls_parameter("@paramPK_paquete", poPaquete.pPK_Paquete)
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.PAQUETE, poPaquete.pPK_Paquete.ToString(), poPaquete.pUsuarioTransaccion);

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al eliminar el paquete.", po_exception);
                }

        }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla paquete
       /// </summary>
       /// <returns>List<cls_paquete> valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_paquete> listarPaquetes()
           {
               List<cls_paquete> vo_lista = null;
               cls_paquete poPaquete = null;
               try
               {
                   String vs_comando = "PA_cont_paqueteSelect";
                   cls_parameter[] vu_parametros = { };

                   DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                   vo_lista = new List<cls_paquete>();
                   for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                   {
                       poPaquete = new cls_paquete();

                       poPaquete.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_paquete"]);

                       poPaquete.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                       poPaquete.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                       poPaquete.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                       vo_lista.Add(poPaquete);
                   }

                   return vo_lista;
               }
               catch (Exception po_exception)
               {
                   cls_sqlDatabase.rollbackTransaction();
                   throw new Exception("Ocurrió un error al obtener el listado de los paquetes.", po_exception);
               }
           }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla paquete
       /// </summary>
       /// <returns>poPaquete valor del resultado de la ejecución de la sentencia</returns>
       public static cls_paquete seleccionarPaquetes(cls_paquete poPaquete)
           {
               try
               {
                   String vs_comando = "PA_cont_paqueteSelectOne";
                   cls_parameter[] vu_parametros = { 
                                                       new cls_parameter("@paramPK_paquete", poPaquete.pPK_Paquete) 
                                                   };

                   DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                   poPaquete = new cls_paquete();

                   poPaquete.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_paquete"]);

                   poPaquete.pCodigo = vu_dataSet.Tables[0].Rows[0]["codigo"].ToString();

                   poPaquete.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

                   poPaquete.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

                   return poPaquete;

               }
               catch (Exception po_exception)
               {
                   cls_sqlDatabase.rollbackTransaction();
                   throw new Exception("Ocurrió un error al obtener el paquete específico.", po_exception);
               }
           }

       /// <summary>
       /// Hace un lista de paquetes con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_paquete> listarPaqueteFiltro(string psFiltro)
       {
           List<cls_paquete> vo_lista = null;
           cls_paquete voPaquete = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.PAQUETE, string.Empty, psFiltro);

               vo_lista = new List<cls_paquete>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voPaquete = new cls_paquete();

                   voPaquete.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_paquete"]);

                   voPaquete.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                   voPaquete.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   voPaquete.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(voPaquete);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los paquetes de manera filtrada.", po_exception);
           }
       }
	
        }
    }
