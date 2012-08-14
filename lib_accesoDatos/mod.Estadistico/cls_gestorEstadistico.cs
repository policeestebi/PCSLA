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
using COSEVI.CSLA.lib.entidades.mod.Estadistico;

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

namespace COSEVI.CSLA.lib.accesoDatos.mod.Estadistico
{
    public class cls_gestorEstadistico
    {

        #region Consultas Comunes

        /// <summary>
        /// Método que permite seleccionar  
        /// la lista simple de proyectos del sistema
        /// </summary>
        /// <returns>poEstado valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_proyecto> listarProyectos()
        {
            List<cls_proyecto> vo_lista = null;
            cls_proyecto vo_proyecto = null;

            try
            {
                DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.PROYECTO, " PK_proyecto, nombre", " 1 = 1 ");

                vo_lista = new List<cls_proyecto>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    vo_proyecto = new cls_proyecto();

                    vo_proyecto.pPK_proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_proyecto"].ToString());

                    vo_proyecto.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                    vo_lista.Add(vo_proyecto);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener la lista de proyectos.", po_exception);
            }
        }

        /// <summary>
        /// Método que permite seleccionar  
        /// la lista de paquetes provenientes de 
        /// la asignación de actividades sobre un proyecto
        /// </summary>
        /// <returns>poEstado valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_paquete> listarPaquetesAsignacion(int pi_proyecto)
        {
            List<cls_paquete> vo_lista = null;
            cls_paquete vo_paquete = null;

            try
            {
                String vs_comando = "PA_estd_paquetesAsignacionActSelect";
                cls_parameter[] vu_parametros = { new cls_parameter("@paramProyecto", pi_proyecto)
                                                };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_paquete>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    vo_paquete = new cls_paquete();

                    vo_paquete.pPK_Paquete = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_paquete"].ToString());

                    vo_paquete.pNombre = vu_dataSet.Tables[0].Rows[i]["nombrePaquete"].ToString();

                    vo_lista.Add(vo_paquete);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener la lista de paquetes.", po_exception);
            }
        }

        #endregion Consultas Comunes


        #region Gráfico Totalidad de Labores por Proyecto

        /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla estado
       /// </summary>
       /// <returns>poEstado valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_totalidadLabores> TotalidadLaboresPorProyecto(cls_totalidadLabores po_totalidadLabores)
       {
           List<cls_totalidadLabores> vo_lista = null;
           cls_totalidadLabores vo_totalidadLabores = null;

           try
           {
               String vs_comando = "PA_estd_inversionTiempos";
               cls_parameter[] vu_parametros = { new cls_parameter("@paramProyecto", po_totalidadLabores.pPK_proyecto),
                                                  new cls_parameter("@paramFechaInicio", po_totalidadLabores.pFechaDesde),
                                                  new cls_parameter("@paramFechaFin", po_totalidadLabores.pFechaHasta),
                                                 new cls_parameter("@paramUsuario", po_totalidadLabores.pPK_usuario)};

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_totalidadLabores>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   vo_totalidadLabores = new cls_totalidadLabores();

                   vo_totalidadLabores.pTipoLabor = vu_dataSet.Tables[0].Rows[i]["tipo"].ToString();

                   vo_totalidadLabores.pCantidad = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["cantidad"].ToString());

                   vo_lista.Add(vo_totalidadLabores);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el gráfico de la totalidad de labores.", po_exception);
           }
       }

        #endregion Gráfico Totalidad de Labores por Proyecto


        #region Gráfico Top Actividades por Proyecto

        /// <summary>
        /// Método que permite seleccionar  
        /// un único registro en la tabla estado
        /// </summary>
        /// <returns>vo_lista valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_topActividades> TopActividadesPorProyecto(cls_topActividades po_topActividades)
        {
            List<cls_topActividades> vo_lista = null;
            cls_topActividades vo_topActividades = null;

            try
            {
                String vs_comando = "PA_estd_actividadesTopProyecto";
                cls_parameter[] vu_parametros = { new cls_parameter("@paramProyecto", po_topActividades.pPK_proyecto),
                                                  new cls_parameter("@paramFechaInicio", po_topActividades.pFechaDesde),
                                                  new cls_parameter("@paramFechaFin", po_topActividades.pFechaHasta),
                                                  new cls_parameter("@paramUsuario", po_topActividades.pPK_usuario)
                                                };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_topActividades>();
                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    vo_topActividades = new cls_topActividades();

                    vo_topActividades.pNombreActividad = vu_dataSet.Tables[0].Rows[i]["actividad"].ToString();

                    vo_topActividades.pCantidadHoras = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["cantidadHoras"].ToString());

                    vo_lista.Add(vo_topActividades);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el gráfico del top de actividades por proyecto.", po_exception);
            }
        }

        #endregion Gráfico Top Actividades por Proyecto


        #region Gráfico Comparación Horas Actividad

        /// <summary>
        /// Método que permite seleccionar  
        /// un único registro en la tabla estado
        /// </summary>
        /// <returns>vo_lista valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_compHorasActividades> CompHorasActividadesPorProyecto(cls_compHorasActividades po_compHorasActividades)
        {
            List<cls_compHorasActividades> vo_lista = null;
            cls_compHorasActividades vo_compHorasActividades = null;

            try
            {
                String vs_comando = "PA_estd_comparacionHorasActividad";
                cls_parameter[] vu_parametros = { new cls_parameter("@paramProyecto", po_compHorasActividades.pPK_proyecto),
                                                  new cls_parameter("@paramPaquete", po_compHorasActividades.pPK_paquete),
                                                  new cls_parameter("@paramFechaInicio", po_compHorasActividades.pFechaDesde),
                                                  new cls_parameter("@paramFechaFin", po_compHorasActividades.pFechaHasta),
                                                  new cls_parameter("@paramUsuario", po_compHorasActividades.pPK_usuario)
                                                };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_compHorasActividades>();
                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    vo_compHorasActividades = new cls_compHorasActividades();

                    vo_compHorasActividades.pNombreActividad = vu_dataSet.Tables[0].Rows[i]["nombreActividad"].ToString();

                    vo_compHorasActividades.pHorasAsignadas = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasAsignadas"].ToString());

                    vo_compHorasActividades.pHorasReales = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasReales"].ToString());

                    vo_lista.Add(vo_compHorasActividades);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el gráfico de comparación de horas de actividades por proyecto.", po_exception);
            }
        }

        #endregion Gráfico Comparación Horas Actividad

        #region Gráfico Consulta Actividades Retrasadas

        /// <summary>
        /// Método que permite consultar  
        /// las actividades que presenten retrasos o superen el estimado en los diferente paquetes de proyecto
        /// </summary>
        /// <returns>vo_lista valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_consActRetrasadas> ConsultaActRetrasadas(cls_consActRetrasadas po_consActRetrasadas)
        {
            List<cls_consActRetrasadas> vo_lista = null;
            cls_consActRetrasadas vo_consActRetrasadas = null;

            try
            {
                String vs_comando = "PA_estd_consultaActRetrasadas";
                cls_parameter[] vu_parametros = { new cls_parameter("@paramProyecto", po_consActRetrasadas.pPK_proyecto),
                                                  new cls_parameter("@paramPaquete", po_consActRetrasadas.pPK_paquete),                                                  
                                                  new cls_parameter("@paramFechaInicio", po_consActRetrasadas.pFechaDesde),
                                                  new cls_parameter("@paramFechaFin", po_consActRetrasadas.pFechaHasta),
                                                  new cls_parameter("@paramUsuario", po_consActRetrasadas.pPK_usuario)
                                                };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_consActRetrasadas>();
                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    vo_consActRetrasadas = new cls_consActRetrasadas();

                    vo_consActRetrasadas.pNombreActividad = vu_dataSet.Tables[0].Rows[i]["nombreActividad"].ToString();

                    vo_consActRetrasadas.pDiasRetraso = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["diasRetraso"].ToString());

                    vo_consActRetrasadas.pHorasRetraso = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[i]["horasRetraso"].ToString());

                    vo_lista.Add(vo_consActRetrasadas);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el gráfico de consulta de actividades retrasadas por proyecto.", po_exception);
            }
        }

        #endregion Gráfico Consulta Actividades Retrasadas

    }
}
