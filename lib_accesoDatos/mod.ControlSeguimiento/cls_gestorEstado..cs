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
// cls_gestorEstado.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            17    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	11    01    2012	Se agrega la modifica la inserción en la bitácora.
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorEstado
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla estado
        /// </summary>
        /// <param name="poEstado">Estado a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertEstado(cls_estado poEstado)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_estadoInsert";
                cls_parameter[] vu_parametros = 
                { 
                        new cls_parameter("@paramdescripcion", poEstado.pDescripcion)  
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                // Se obtiene el número del registro insertado.
                poEstado.pPK_estado = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.ESTADO, "PK_estado"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ESTADO, poEstado.pPK_estado.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el estado.", po_exception);
            }

    }

        /// <summary>
        /// Método que permite actualizar 
        /// un registro en la tabla estado
        /// </summary>
       /// <param name="poEstado">Estado a actualizar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateEstado(cls_estado poEstado)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_cont_estadoUpdate";
                    cls_parameter[] vu_parametros = 
                    {
                 		    new cls_parameter("@paramPK_estado", poEstado.pPK_estado),
                            new cls_parameter("@paramdescripcion", poEstado.pDescripcion)
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.ESTADO, poEstado.pPK_estado.ToString());

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al modificar el estado.", po_exception);
                }

        }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla estado
       /// </summary>
       /// <param name="poEstado">Estado a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteEstado(cls_estado poEstado)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_cont_estadoDelete";
                    cls_parameter[] vu_parametros = 
                    {
                 		    new cls_parameter("@paramPK_estado", poEstado.pPK_estado)
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.ESTADO, poEstado.pPK_estado.ToString());

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al eliminar el estado.", po_exception);
                }

        }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla estado
       /// </summary>
       /// <returns> List<cls_estado>  valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_estado> listarEstado()
       {
           List<cls_estado> vo_lista = null;
           cls_estado poEstado = null;
           try
           {
               String vs_comando = "PA_cont_estadoSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_estado>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poEstado = new cls_estado();

                   poEstado.pPK_estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_estado"]);

                   poEstado.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(poEstado);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los estados.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla estado
       /// </summary>
       /// <returns>poEstado valor del resultado de la ejecución de la sentencia</returns>
       public static cls_estado seleccionarEstado(cls_estado poEstado)
       {
           try
           {
               String vs_comando = "PA_cont_estadoSelectOne";
               cls_parameter[] vu_parametros = { new cls_parameter("@paramPK_estado", poEstado.pPK_estado) };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poEstado = new cls_estado();

               poEstado.pPK_estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_estado"]);

               poEstado.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

               return poEstado;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el estado específico.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de estados con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_estado> listarEstadoFiltro(string psFiltro)
       {
           List<cls_estado> vo_lista = null;
           cls_estado voEstado = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.ESTADO, string.Empty, psFiltro);

               vo_lista = new List<cls_estado>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voEstado = new cls_estado();

                   voEstado.pPK_estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_estado"]);

                   voEstado.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();


                   vo_lista.Add(voEstado);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los estados de manera filtrada.", po_exception);
           }
       }
	
        }
    }
