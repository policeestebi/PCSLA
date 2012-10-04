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
using COSEVI.CSLA.lib.entidades.mod.Administracion;

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
    public class cls_gestorAsignacionActividad
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla estado
        /// </summary>
        /// <param name="po_Actividad">Actividad a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertAsignacionActividad(cls_asignacionActividad po_Actividad)
   {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_asignacionActividadInsert";
                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramPK_actividad", po_Actividad.pPK_Actividad),
                        new cls_parameter("@paramPK_paquete", po_Actividad.pPK_Paquete),
                        new cls_parameter("@paramPK_componente", po_Actividad.pPK_Componente),
                        new cls_parameter("@paramPK_entregable", po_Actividad.pPK_Entregable),
                        new cls_parameter("@paramPK_proyecto", po_Actividad.pPK_Proyecto),
                        new cls_parameter("@paramPK_usuario", po_Actividad.pUsuarioPivot),
                        new cls_parameter("@paramFK_estado", po_Actividad.pFK_Estado),
                        new cls_parameter("@paramdescripcion", po_Actividad.pDescripcion),
                        new cls_parameter("@paramfechaInicio", po_Actividad.pFechaInicio),
                        new cls_parameter("@paramfechaFin", po_Actividad.pFechaFin),
                        new cls_parameter("@paramhorasAsignadas", po_Actividad.pHorasAsignadas)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ACTIVIDAD_ASIGNACION, "Act: " + po_Actividad.pPK_Actividad.ToString() +
                                                                                                                         "Paq: " + po_Actividad.pPK_Paquete.ToString() +
                                                                                                                         "Comp: " + po_Actividad.pPK_Componente +
                                                                                                                         "Ent: " + po_Actividad.pPK_Entregable.ToString() + 
                                                                                                                         "Proy: " + po_Actividad.pPK_Proyecto.ToString() +
                                                                                                                         "Usuario: " + po_Actividad.pPK_Usuario.ToString(), po_Actividad.pUsuarioTransaccion);

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
       /// <param name="po_Actividad">Actividad a actualizar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateAsignacionActividad(cls_asignacionActividad po_Actividad, int ps_accion)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_asignacionActividadUpdate";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_actividad", po_Actividad.pPK_Actividad),
                    new cls_parameter("@paramPK_paquete", po_Actividad.pPK_Paquete),
                    new cls_parameter("@paramPK_componente", po_Actividad.pPK_Componente),
                    new cls_parameter("@paramPK_entregable", po_Actividad.pPK_Entregable),
                    new cls_parameter("@paramPK_proyecto", po_Actividad.pPK_Proyecto),
                    new cls_parameter("@paramPK_usuario", po_Actividad.pUsuarioPivot),
                    new cls_parameter("@paramFK_estado", po_Actividad.pFK_Estado),
                    new cls_parameter("@paramdescripcion", po_Actividad.pDescripcion),
                    new cls_parameter("@paramfechaInicio", po_Actividad.pFechaInicio),
                    new cls_parameter("@paramfechaFin", po_Actividad.pFechaFin),
                    new cls_parameter("@paramhorasAsignadas", po_Actividad.pHorasAsignadas),
                    new cls_parameter("@paramAccion", ps_accion)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.ACTIVIDAD_ASIGNACION, "Act: " + po_Actividad.pPK_Actividad.ToString() +
                                                                                                                          "Paq: " + po_Actividad.pPK_Paquete.ToString() +
                                                                                                                          "Comp: " + po_Actividad.pPK_Componente +
                                                                                                                          "Ent: " + po_Actividad.pPK_Entregable.ToString() +
                                                                                                                          "Proy: " + po_Actividad.pPK_Proyecto.ToString() +
                                                                                                                          "Usuario: " + po_Actividad.pUsuarioPivot.ToString(), po_Actividad.pUsuarioTransaccion);

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
       /// <param name="po_Actividad">Actividad a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteActividad(cls_asignacionActividad po_Actividad)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_asignacionActividadDelete";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_actividad", po_Actividad.pPK_Actividad),
                    new cls_parameter("@paramPK_paquete", po_Actividad.pPK_Paquete),
                    new cls_parameter("@paramPK_componente", po_Actividad.pPK_Componente),
                    new cls_parameter("@paramPK_entregable", po_Actividad.pPK_Entregable),
                    new cls_parameter("@paramPK_proyecto", po_Actividad.pPK_Proyecto),
                    new cls_parameter("@paramPK_usuario", po_Actividad.pPK_Usuario)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.ACTIVIDAD_ASIGNACION, "Act: " + po_Actividad.pPK_Actividad.ToString() +
                                                                                                                         "Paq: " + po_Actividad.pPK_Paquete.ToString() +
                                                                                                                         "Comp: " + po_Actividad.pPK_Componente +
                                                                                                                         "Ent: " + po_Actividad.pPK_Entregable.ToString() +
                                                                                                                         "Proy: " + po_Actividad.pPK_Proyecto.ToString() +
                                                                                                                         "Usuario: " + po_Actividad.pPK_Usuario.ToString(), po_Actividad.pUsuarioTransaccion);

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
       public static List<cls_asignacionActividad> listarActividades()
       {
           List<cls_asignacionActividad> vo_lista = null;
           cls_asignacionActividad vo_Actividad = null;
           try
           {
               String vs_comando = "PA_cont_actividadSelect";
               cls_parameter[] vu_parametros = {
                                                  
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_asignacionActividad>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   vo_Actividad = new cls_asignacionActividad();

                   vo_Actividad.pPK_Actividad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_actividad"]);

                   vo_Actividad.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_paquete"]);

                   vo_Actividad.pPK_Componente = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_componente"]);

                   vo_Actividad.pPK_Entregable = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_entregable"]);

                   vo_Actividad.pPK_Proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_proyecto"]);

                   vo_Actividad.pPK_Usuario = vu_dataSet.Tables[0].Rows[i]["PK_usuario"].ToString();

                   vo_Actividad.pFK_Estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_estado"]);

                   vo_Actividad.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_Actividad.pFechaInicio = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaInicio"]);

                   vo_Actividad.pFechaFin = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fechaFin"]);

                   vo_Actividad.pHorasAsignadas = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasAsignadas"]);
                   
                   vo_Actividad.pHorasReales = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasReales"]);
                   
                   vo_lista.Add(vo_Actividad);
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
       public static cls_asignacionActividad seleccionarAsignacionActividad(cls_paqueteActividad po_paqueteActividad)
       {
           cls_asignacionActividad vo_asignacionActividad = null;
           cls_usuario vo_usuario = null;
           List<cls_usuario> vo_listaUsuarios = null;

           try
           {
               String vs_comando = "PA_cont_actividadAsignadaSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_proyecto", po_paqueteActividad.pPK_Proyecto),
                                                   new cls_parameter("@paramPK_paquete", po_paqueteActividad.pPK_Paquete),
                                                   new cls_parameter("@paramPK_actividad", po_paqueteActividad.pPK_Actividad)
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_asignacionActividad = new cls_asignacionActividad();

               vo_listaUsuarios = new List<cls_usuario>();

               if (vu_dataSet.Tables[0].Rows.Count > 0)
               {
                    vo_asignacionActividad.pPK_Actividad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_actividad"]);

                    vo_asignacionActividad.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_paquete"]);

                    vo_asignacionActividad.pPK_Componente = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_componente"]);

                    vo_asignacionActividad.pPK_Entregable = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_entregable"]);

                    vo_asignacionActividad.pPK_Proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_proyecto"]);

                    vo_asignacionActividad.pPK_Usuario = vu_dataSet.Tables[0].Rows[0]["PK_usuario"].ToString();

                    vo_asignacionActividad.pFK_Estado = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_estado"]);

                    vo_asignacionActividad.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

                    vo_asignacionActividad.pFechaInicio = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[0]["fechaInicio"]);

                    vo_asignacionActividad.pFechaFin = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[0]["fechaFin"]);

                    vo_asignacionActividad.pHorasAsignadas = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[0]["horasAsignadas"]);

                    vo_asignacionActividad.pHorasReales = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[0]["horasReales"]);
                    
                    for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                    {
                        vo_usuario = new cls_usuario();

                        vo_usuario.pPK_usuario = vu_dataSet.Tables[0].Rows[i]["PK_usuario"].ToString();
                        
                        vo_usuario.pNombre = vu_dataSet.Tables[0].Rows[i]["nombreUsuario"].ToString();

                        vo_listaUsuarios.Add(vo_usuario);
                    }

                    vo_asignacionActividad.pUsuarioLista = vo_listaUsuarios;
               }

               return vo_asignacionActividad;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al seleccionar la actividad específica.", po_exception);
           }

       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="ps_proyecto"></param>
       /// <returns></returns>
       public static List<cls_paquete> listarPaquetesProyecto(int ps_proyecto)
       {
           List<cls_paquete> vo_lista = null;
           cls_paquete vo_paqueteProyecto = null;
           try
           {
               String vs_comando = "PA_cont_paquetesProyectoSelectAll";
               cls_parameter[] vu_parametros = {
                                                 new cls_parameter("@paramPK_proyecto", ps_proyecto.ToString())
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_paquete>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   vo_paqueteProyecto = new cls_paquete();

                   vo_paqueteProyecto.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_paquete"]);

                   vo_paqueteProyecto.pNombre = vu_dataSet.Tables[0].Rows[i]["nombrePaquete"].ToString();

                   vo_lista.Add(vo_paqueteProyecto);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las actividades.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de permisos con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_asignacionActividad> listarActividadesPorPaquete(int pi_proyecto, int pi_paquete)
       {
           List<cls_asignacionActividad> vo_lista = null;
           cls_asignacionActividad vo_asignacionActividad = null;
           try
           {
               String vs_comando = "PA_cont_actividadesPaqueteSelect";
               cls_parameter[] vu_parametros = {
                                                 new cls_parameter("@paramPK_proyecto", pi_proyecto),
                                                 new cls_parameter("@paramPK_paquete", pi_paquete)
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_asignacionActividad>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   vo_asignacionActividad = new cls_asignacionActividad();

                   vo_asignacionActividad.pPK_Actividad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_actividad"]);

                   vo_asignacionActividad.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_paquete"]);

                   vo_asignacionActividad.pPK_Componente = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_componente"]);

                   vo_asignacionActividad.pPK_Entregable = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_entregable"]);

                   vo_asignacionActividad.pPK_Proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_proyecto"]);

                   vo_asignacionActividad.pNombrePaquete = vu_dataSet.Tables[0].Rows[i]["nombrePaquete"].ToString();
                   
                   vo_asignacionActividad.pNombreActividad = vu_dataSet.Tables[0].Rows[i]["nombreActividad"].ToString();

                   vo_lista.Add(vo_asignacionActividad);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las actividades.", po_exception);
           }
       }

       /// <summary>
       /// Este va sobrecargado para traer también las actividades cuando NO hay cticidades aún asignadas
       /// </summary>
       /// <param name="pi_proyecto"></param>
       /// <param name="pi_paquete"></param>
       /// <returns></returns>
       public static cls_asignacionActividad listarActividadesPorPaquete(int pi_proyecto, int pi_paquete, int pi_actividad)
       {
           List<cls_asignacionActividad> vo_lista = null;
           cls_asignacionActividad vo_asignacionActividad = null;
           try
           {
               String vs_comando = "PA_cont_actividadesPaqueteSelectOne";
               cls_parameter[] vu_parametros = {
                                                 new cls_parameter("@paramPK_proyecto", pi_proyecto),
                                                 new cls_parameter("@paramPK_paquete", pi_paquete),
                                                 new cls_parameter("@paramPK_actividad", pi_actividad)
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_asignacionActividad>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   vo_asignacionActividad = new cls_asignacionActividad();

                   vo_asignacionActividad.pPK_Actividad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_actividad"]);

                   vo_asignacionActividad.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_paquete"]);

                   vo_asignacionActividad.pPK_Componente = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_componente"]);

                   vo_asignacionActividad.pPK_Entregable = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_entregable"]);

                   vo_asignacionActividad.pPK_Proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_proyecto"]);

                   vo_asignacionActividad.pNombrePaquete = vu_dataSet.Tables[0].Rows[i]["nombrePaquete"].ToString();

                   vo_asignacionActividad.pNombreActividad = vu_dataSet.Tables[0].Rows[i]["nombreActividad"].ToString();

                   vo_lista.Add(vo_asignacionActividad);
               }

               return vo_asignacionActividad;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las actividades.", po_exception);
           }
       }
	    
    }
}
