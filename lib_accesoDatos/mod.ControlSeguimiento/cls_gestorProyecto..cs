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
// cls_gestorProyecto.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            17    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	11    01    2012	Se agrega la modifica la inserción en la bitácora.
// Cristian Arce Jiménez  	     23 – 01  - 2012	 	Se agrega el manejo de filtros
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorProyecto
    {
       /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla Proyecto
        /// </summary>
        /// <param name="poProyecto">Proyecto a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertProyecto(cls_proyecto poProyecto)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_proyectoInsert";
                cls_parameter[] vu_parametros = 
                {                    
                        new cls_parameter("@paramFK_estado", poProyecto.pFK_estado),
                        new cls_parameter("@paramnombre", poProyecto.pNombre),
                        new cls_parameter("@paramdescripcion", poProyecto.pDescripcion),
                        new cls_parameter("@paramobjetivo", poProyecto.pObjetivo),
                        new cls_parameter("@parammeta", poProyecto.pMeta),
                        new cls_parameter("@paramfechaInicio", poProyecto.pFechaInicio),
                        new cls_parameter("@paramfechaFin", poProyecto.pFechaFin),
                        new cls_parameter("@paramhorasAsignadas", poProyecto.pHorasAsignadas)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                // Se obtiene el número del registro insertado.
                poProyecto.pPK_proyecto = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.PROYECTO, "PK_proyecto"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.PROYECTO, poProyecto.pPK_proyecto.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el proyecto.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite actualizar 
       /// un registro en la tabla Proyecto 
       /// </summary>
       /// <param name="poProyecto">Proyecto a actualizar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateProyecto(cls_proyecto poProyecto)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_cont_proyectoUpdate";
                    cls_parameter[] vu_parametros = 
                    {                  
                 		    new cls_parameter("@paramPK_proyecto", poProyecto.pPK_proyecto),
                            new cls_parameter("@paramFK_estado", poProyecto.pFK_estado),
                            new cls_parameter("@paramnombre", poProyecto.pNombre),
                            new cls_parameter("@paramdescripcion", poProyecto.pDescripcion),
                            new cls_parameter("@paramobjetivo", poProyecto.pObjetivo),
                            new cls_parameter("@parammeta", poProyecto.pMeta),
                            new cls_parameter("@paramfechaInicio", poProyecto.pFechaInicio),
                            new cls_parameter("@paramfechaFin", poProyecto.pFechaFin),
                            new cls_parameter("@paramhorasAsignadas", poProyecto.pHorasAsignadas)           
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.PROYECTO, poProyecto.pPK_proyecto.ToString());

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al modificar el proyecto.", po_exception);
                }

        }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla Proyecto 
       /// </summary>
       /// <param name="poProyecto">Proyecto a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteProyecto(cls_proyecto poProyecto)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_cont_proyectoDelete";
                    cls_parameter[] vu_parametros = 
                    {     
                 		    new cls_parameter("@paramPK_proyecto", poProyecto.pPK_proyecto)
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.PROYECTO, poProyecto.pPK_proyecto.ToString());

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al eliminar el proyecto.", po_exception);
                }

        }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla proyecto
       /// </summary>
       /// <returns>List<cls_proyecto> valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_proyecto> listarProyectos()
       {
           List<cls_proyecto> vo_lista = null;
           cls_proyecto poProyecto = null;
           try
           {
               String vs_comando = "PA_cont_proyectoSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_proyecto>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poProyecto = new cls_proyecto();

                   poProyecto.pPK_proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_proyecto"]);

                   poProyecto.pFK_estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_estado"]);

                   poProyecto.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   poProyecto.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   poProyecto.pObjetivo = vu_dataSet.Tables[0].Rows[i]["objetivo"].ToString();

                   poProyecto.pMeta = vu_dataSet.Tables[0].Rows[i]["meta"].ToString();

                   poProyecto.pFechaInicio = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaInicio"]);

                   poProyecto.pFechaFin = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaFin"]);

                   poProyecto.pHorasAsignadas = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasAsignadas"]);

                   poProyecto.pHorasReales = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasReales"]);
                  
                   poProyecto.pNombreEstado = vu_dataSet.Tables[0].Rows[i]["nombreEstado"].ToString();

                   vo_lista.Add(poProyecto);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los proyectos.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla proyecto
       /// </summary>
       /// <returns>poProyecto valor del resultado de la ejecución de la sentencia</returns>
       public static cls_proyecto seleccionarProyectos(cls_proyecto poProyecto)
       {
           try
           {
               String vs_comando = "PA_cont_proyectoSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_proyecto", poProyecto.pPK_proyecto) 
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poProyecto = new cls_proyecto();

                poProyecto.pPK_proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_proyecto"]);

                poProyecto.pFK_estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_estado"]);

                poProyecto.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

                poProyecto.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

                poProyecto.pObjetivo = vu_dataSet.Tables[0].Rows[0]["objetivo"].ToString();

                poProyecto.pMeta = vu_dataSet.Tables[0].Rows[0]["meta"].ToString();

                poProyecto.pFechaInicio = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[0]["fechaInicio"]);

                poProyecto.pFechaFin = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[0]["fechaFin"]);

                poProyecto.pHorasAsignadas = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[0]["horasAsignadas"]);

                poProyecto.pHorasReales = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[0]["horasReales"]);

               return poProyecto;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el proyecto específico.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla estado
       /// </summary>
       /// <returns> DataSet valor del resultado de la ejecución de la sentencia</returns>
       public static DataSet listarEstado()
       {
           try
           {
               String vs_comando = "PA_cont_estadoSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               return vu_dataSet;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de el estado del proyecto.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de proyectos con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_proyecto> listarProyectoFiltro(string psFiltro)
       {
           List<cls_proyecto> vo_lista = null;
           cls_proyecto voProyecto = null;
           try
           {
               string columnas = "tcp.PK_proyecto, tcp.FK_estado, tcp.nombre, " +
                                 "tcp.descripcion, tcp.objetivo, tcp.meta, tcp.fechaInicio, " +
                                 "tcp.fechaFin, tcp.horasAsignadas, ISNULL((SELECT SUM(horas) FROM t_cont_registro_actividad tcra WHERE tcra.PK_proyecto = tcp.PK_proyecto),0) horasReales, " +
                                 "tce.descripcion nombreEstado";

               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.PROYECTO + " tcp INNER JOIN " + cls_constantes.ESTADO + " tce ON tcp.FK_estado = tce.PK_estado ", columnas, psFiltro);

               vo_lista = new List<cls_proyecto>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voProyecto = new cls_proyecto();

                   voProyecto.pPK_proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_proyecto"]);

                   voProyecto.pFK_estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_estado"]);

                   voProyecto.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   voProyecto.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   voProyecto.pObjetivo = vu_dataSet.Tables[0].Rows[i]["objetivo"].ToString();

                   voProyecto.pMeta = vu_dataSet.Tables[0].Rows[i]["meta"].ToString();

                   voProyecto.pFechaInicio = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaInicio"]);

                   voProyecto.pFechaFin = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaFin"]);

                   voProyecto.pHorasAsignadas = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasAsignadas"]);

                   voProyecto.pHorasReales = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasReales"]);

                   voProyecto.pNombreEstado = vu_dataSet.Tables[0].Rows[i]["nombreEstado"].ToString();

                   vo_lista.Add(voProyecto);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los proyectos de manera filtrada.", po_exception);
           }
       }


       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla proyecto
       /// </summary>
       /// <returns>List valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_proyecto> listarProyectosUsuario()
       {
           List<cls_proyecto> vo_lista = null;
           cls_proyecto poProyecto = null;
           try
           {
               String vs_comando = "PA_cont_proyectoSelectUsuario";
               cls_parameter[] vu_parametros = {
                                                   new cls_parameter("@paramUsuario", cls_interface.vs_usuarioActual) 
                                                    
                                                };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_proyecto>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poProyecto = new cls_proyecto();

                   poProyecto.pPK_proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_proyecto"]);

                   poProyecto.pFK_estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_estado"]);

                   poProyecto.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   poProyecto.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   poProyecto.pObjetivo = vu_dataSet.Tables[0].Rows[i]["objetivo"].ToString();

                   poProyecto.pMeta = vu_dataSet.Tables[0].Rows[i]["meta"].ToString();

                   poProyecto.pFechaInicio = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaInicio"]);

                   poProyecto.pFechaFin = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaFin"]);

                   poProyecto.pHorasAsignadas = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasAsignadas"]);

                   poProyecto.pHorasReales = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasReales"]);

                   vo_lista.Add(poProyecto);
               }

               poProyecto = new cls_proyecto();
               poProyecto.pPK_proyecto = cls_constantes.CODIGO_IMPREVISTO;
               poProyecto.pFK_estado = 1;
               poProyecto.pNombre = cls_constantes.NOMBRE_IMPREVISTO;
               poProyecto.pDescripcion = cls_constantes.NOMBRE_IMPREVISTO;
               poProyecto.pObjetivo = cls_constantes.NOMBRE_IMPREVISTO;
               poProyecto.pMeta = cls_constantes.NOMBRE_IMPREVISTO;
               poProyecto.pFechaInicio = DateTime.Now;
               poProyecto.pFechaFin = DateTime.Now;
               poProyecto.pHorasAsignadas = 0;
               poProyecto.pHorasReales = 0;

               vo_lista.Insert(0, poProyecto);

               poProyecto = new cls_proyecto();
               poProyecto.pPK_proyecto = cls_constantes.CODIGO_OPERACION;
               poProyecto.pFK_estado = 1;
               poProyecto.pNombre = cls_constantes.NOMBRE_OPERACION;
               poProyecto.pDescripcion = cls_constantes.NOMBRE_OPERACION;
               poProyecto.pObjetivo = cls_constantes.NOMBRE_OPERACION;
               poProyecto.pMeta = cls_constantes.NOMBRE_OPERACION;
               poProyecto.pFechaInicio = DateTime.Now;
               poProyecto.pFechaFin = DateTime.Now;
               poProyecto.pHorasAsignadas = 0;
               poProyecto.pHorasReales = 0;
               vo_lista.Insert(0, poProyecto);

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los proyectos.", po_exception);
           }
       }

        /// <summary>
        /// Método que permite listar 
        /// todos los registros en la tabla proyecto
        /// </summary>
        /// <returns></returns>
       public static DataSet listarProyectosUsuarioDataSet()
       {
           DataSet vu_dataSet = null;
           try
           {
               String vs_comando = "PA_cont_proyectoSelectUsuario";
               cls_parameter[] vu_parametros = {
                                                   new cls_parameter("@paramUsuario", cls_interface.vs_usuarioActual) 
                                                    
                                                };

               vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               if(vu_dataSet != null)
               {

                   DataRow row = vu_dataSet.Tables[0].NewRow();
                   row.ItemArray = new Object[] {cls_constantes.CODIGO_OPERACION,
                                                   1,
                                                   cls_constantes.NOMBRE_OPERACION,
                                                   cls_constantes.NOMBRE_OPERACION,
                                                   cls_constantes.NOMBRE_OPERACION,
                                                   cls_constantes.NOMBRE_OPERACION,
                                                   DateTime.Now,
                                                   DateTime.Now,
                                                   0,
                                                   0
                                                   };

                   vu_dataSet.Tables[0].Rows.InsertAt(row,0);

                   row = vu_dataSet.Tables[0].NewRow();
                    row.ItemArray = new Object[] {cls_constantes.CODIGO_IMPREVISTO,
                                                1,
                                                cls_constantes.NOMBRE_IMPREVISTO,
                                                cls_constantes.NOMBRE_IMPREVISTO,
                                                cls_constantes.NOMBRE_IMPREVISTO,
                                                cls_constantes.NOMBRE_IMPREVISTO,
                                                DateTime.Now,
                                                DateTime.Now,
                                                0,
                                                0
                                                };

                   vu_dataSet.Tables[0].Rows.InsertAt(row,0);

               }

             
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los proyectos.", po_exception);
           }

           return vu_dataSet;
       }

       /// <summary>
       /// Método que permite insertar 
       /// un nuevo registro en la tabla Proyecto tomando como base uno ya existente
       /// </summary>
       /// <param name="poProyecto">Proyecto a insertar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int insertProyectoCopia(int pi_proyectoNuevo, int pi_proyectoOriginal)
       {
           int vi_resultado;

           try
           {
               String vs_comando = "PA_cont_proyectoCopiaInsert";
               cls_parameter[] vu_parametros = 
                {                    
                        new cls_parameter("@paramPK_proyectoNuevo", pi_proyectoNuevo),
                        new cls_parameter("@paramPK_proyectoOriginal", pi_proyectoOriginal)
                };

               cls_sqlDatabase.beginTransaction();

               vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

               cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.PROYECTO_COPIA, pi_proyectoNuevo.ToString());

               cls_sqlDatabase.commitTransaction();

               return vi_resultado;

           }
           catch (Exception po_exception)
           {
               cls_sqlDatabase.rollbackTransaction();
               throw new Exception("Ocurrió un error al insertar en las tablas correspondientes para la copia del proyecto.", po_exception);
           }

       }

    }
}
