using System;
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
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;
using COSEVI.CSLA.lib.entidades.mod.Administracion;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorOperacion.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Esteban Ramírez G.	    04    11    2012    Se crea la clase
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorOperacion
    {
        /// <summary>
        /// Inserta una operación.
        /// </summary>
        /// <param name="poOperacion">cls_asignacionOperacion a insertar</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int insertOperacion(cls_asignacionOperacion poOperacion)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_operacionInsert";

                if (poOperacion.pFK_Operacion.pFK_Proyecto == -1)
                {

                    cls_parameter[] vu_parametros = 
                    {                   
                        new cls_parameter("@paramTipo", poOperacion.pFK_Operacion.pTipo),
                        new cls_parameter("@paramDescripcion", poOperacion.pFK_Operacion.pDescripcion),
                        new cls_parameter("@paramUsuario", poOperacion.pFK_Usuario),
                        new cls_parameter("@paramProyecto", DBNull.Value),
                        new cls_parameter("@paramActivo", poOperacion.pFK_Operacion.pActivo ? 1 : 0),
                        new cls_parameter("@param_PK_codigo", poOperacion.pFK_Operacion.pPK_Codigo,ParameterDirection.Output)
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }
                else
                {
                    cls_parameter[] vu_parametros = 
                    {                   
                        new cls_parameter("@paramTipo", poOperacion.pFK_Operacion.pTipo),
                        new cls_parameter("@paramDescripcion", poOperacion.pFK_Operacion.pDescripcion),
                        new cls_parameter("@paramUsuario", poOperacion.pFK_Usuario),
                        new cls_parameter("@paramProyecto", poOperacion.pFK_Operacion.pFK_Proyecto),
                        new cls_parameter("@paramActivo", poOperacion.pIsActivo),
                        new cls_parameter("@param_PK_codigo", poOperacion.pFK_Operacion.pPK_Codigo)
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }

                poOperacion.pFK_Operacion.pPK_Codigo = obtenerUltimaOperacion();

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.OPERACION, poOperacion.pFK_Operacion.pPK_Codigo.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar la operación.", po_exception);
            }

        }

        /// <summary>
        /// Método que se utiliza para
        /// actualizar una operación.
        /// </summary>
        /// <param name="poOperacion">cls_operacion operación.</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int updateOperacion(cls_operacion poOperacion)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_operacionUpdate";
                cls_parameter[] vu_parametros = 
                    {                   
                 		    new cls_parameter("@paramPK_operacion", poOperacion.pPK_Codigo),
                            new cls_parameter("@paramDescripcion", poOperacion.pDescripcion), 
                            new cls_parameter("@paramActivo", poOperacion.pActivo),
                            new cls_parameter("@paramUsuario", cls_interface.vs_usuarioActual)	
                    };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.OPERACION, poOperacion.pPK_Codigo);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar la operación.", po_exception);
            }

        }

        /// <summary>
        /// Método que se utiliza 
        /// para eliminar una operación.
        /// </summary>
        /// <param name="poOperacion">cls_asignacionOperacion operación.</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int deleteOperacion(cls_asignacionOperacion poOperacion)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_operacionDelete";
                cls_parameter[] vu_parametros = 
                    {
                        new cls_parameter("@paramPK_operacion", poOperacion.pFK_Operacion.pPK_Codigo),
                        new cls_parameter("@paramUsuario", poOperacion.pFK_Usuario)
                    };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.OPERACION, poOperacion.pFK_Operacion.pPK_Codigo);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar la operación.", po_exception);
            }
        }

        /// <summary>
        /// Método que permite listar 
        /// todos los registros en la tabla operacion
        /// </summary>
        /// <returns>List<cls_operacion> valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_operacion> listarOperaciones(string psUsuario)
        {
            List<cls_operacion> vo_lista = null;
            cls_operacion poOperacion = null;
            try
            {
                String vs_comando = "PA_cont_operacionSelectAll";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramUsuario", psUsuario)
                };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_operacion>();
                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    poOperacion = new cls_operacion();

                    poOperacion.pPK_Codigo = vu_dataSet.Tables[0].Rows[i]["PK_codigo"].ToString();

                    poOperacion.pTipo = vu_dataSet.Tables[0].Rows[i]["tipo"].ToString();

                    poOperacion.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                    poOperacion.pActivo = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["activo"]) == 1 ? true : false;

                    vo_lista.Add(poOperacion);
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
        /// Método que obtiene la información de una 
        /// operación.
        /// </summary>
        /// <param name="poOperacion"></param>
        /// <returns></returns>
        public static cls_operacion seleccionarOperacion(cls_operacion poOperacion)
        {
            try
            {
                String vs_comando = "PA_cont_operacionSelectOne";
                cls_parameter[] vu_parametros = { 
                                                       new cls_parameter("@paramPK_codigo", poOperacion.pPK_Codigo), 
                                                       new cls_parameter("@paramPK_usuario", cls_interface.vs_usuarioActual) 
                                                   };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                poOperacion = new cls_operacion();

                poOperacion.pPK_Codigo = vu_dataSet.Tables[0].Rows[0]["PK_codigo"].ToString();

                poOperacion.pTipo = vu_dataSet.Tables[0].Rows[0]["tipo"].ToString();

                poOperacion.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

                poOperacion.pFK_Proyecto = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_proyecto"].ToString());

                poOperacion.pActivo = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["activo"]) == 1 ? true : false;

                return poOperacion;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al obtener una operación específica.", po_exception);
            }
        }

        /// <summary>
        /// Lista las asignaciones
        /// existentes de una operación
        /// </summary>
        /// <param name="poOperacion">cls_operacion operacion</param>
        /// <returns>List con las asignaciones existentes de una operación</returns>
        public static List<cls_asignacionOperacion> listarAsignacionesOperacion(cls_operacion poOperacion)
        {
            List<cls_asignacionOperacion> vo_lista = null;
            cls_asignacionOperacion voAsignacion = null;
            cls_usuario voUsuario = null;

            try
            {
                String vs_comando = "PA_cont_asignacionOperacion";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramOperacion", poOperacion.pPK_Codigo)
                };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_asignacionOperacion>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    voAsignacion = new cls_asignacionOperacion();

                    voAsignacion.pFK_Operacion = poOperacion;

                    voAsignacion.pFK_Usuario = vu_dataSet.Tables[0].Rows[i]["PK_usuario"].ToString();

                    voUsuario = new cls_usuario();

                    voUsuario.pPK_usuario = voAsignacion.pFK_Usuario;

                    voAsignacion.pUsuario = cls_gestorUsuario.seleccionarUsuario(voUsuario);

                    vo_lista.Add(voAsignacion);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los paquetes de manera filtrada.", po_exception);
            }
        }

        /// <summary>
        /// Método que lista
        /// las operaciónes según un filtro.
        /// </summary>
        /// <param name="psFiltro"></param>
        /// <returns>List con la lista de operaciones</returns>
        public static List<cls_operacion> listarPaqueteFiltro(string psFiltro)
        {
            List<cls_operacion> vo_lista = null;
            cls_operacion voOperacion = null;
            try
            {
                DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.OPERACION + " o " + "," + cls_constantes.OPERACION_ASIGNACION + " ao ",
                                                                " o.PK_codigo,o.tipo,o.descripcion,o.activo ",
                                                                psFiltro + " AND o.PK_codigo = ao.PK_codigo AND ao.PK_usuario = '" + COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes.cls_interface.vs_usuarioActual + "'");

                vo_lista = new List<cls_operacion>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    voOperacion = new cls_operacion();

                    voOperacion.pPK_Codigo = vu_dataSet.Tables[0].Rows[i]["PK_codigo"].ToString();

                    voOperacion.pTipo = vu_dataSet.Tables[0].Rows[i]["tipo"].ToString();

                    voOperacion.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                    voOperacion.pActivo = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["activo"]) == 1 ? true : false;

                    vo_lista.Add(voOperacion);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los paquetes de manera filtrada.", po_exception);
            }
        }

        /// <summary>
        /// Optiene el último registro insertado en la tabla
        /// de operación.
        /// </summary>
        /// <returns></returns>
        public static string obtenerUltimaOperacion()
        {
            string vs_codigo = string.Empty;
            try
            {
                String vs_comando = "PA_cont_obtenerUltimaOperacion";
                cls_parameter[] vu_parametros = { };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                if (vu_dataSet != null && vu_dataSet.Tables[0].Rows.Count > 0)
                {
                    vs_codigo = vu_dataSet.Tables[0].Rows[0][0].ToString();

                }

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al obtener el último código de la operación.", po_exception);
            }

            return vs_codigo;
        }

        /// <summary>
        /// Lista todas las operaciones
        /// que no esten asociadas a un proyecto.
        /// </summary>
        /// <param name="psUsuario">código del usuario.</param>
        /// <param name="pstipo">tipo de operacion.</param>
        /// <returns>DataSet</returns>
        public static DataSet listarOperacionesUsuario(string psUsuario, string psTipo)
        {
            DataSet vu_dataSet = null;
            try
            {
                String vs_comando = "PA_cont_operacionSelectUsuario";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramUsuario", psUsuario),
                    new cls_parameter("@paramTipo", psTipo)
                };

                vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);
            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al obtener el listado de los paquetes.", po_exception);
            }

            return vu_dataSet;
        }

        /// <summary>
        /// Inserta una asignación 
        /// de una operación.
        /// Se utiliza  para la asignación
        /// masiva de operaciones
        /// </summary>
        /// <param name="poOperacion">cls_asignacionOperacion asignación a insertar.</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int insertAsignacionOperacion(cls_asignacionOperacion poOperacion)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_asignacionOperacionInsert";

                cls_parameter[] vu_parametros = 
                    {                   
                        new cls_parameter("@paramOperacion", poOperacion.pFK_Operacion.pPK_Codigo),
                        new cls_parameter("@paramUsuario", poOperacion.pFK_Usuario),
                        new cls_parameter("@paramActivo", poOperacion.pIsActivo)
                    };

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.OPERACION_ASIGNACION, poOperacion.pFK_Operacion.pPK_Codigo + "/" + poOperacion.pFK_Usuario);

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al insertar la asignación de la operación.", po_exception);
            }

        }

        /// <summary>
        /// Elimina una asiganción de una operación.
        /// </summary>
        /// <param name="poOperacion">cls_asignacionOperacion asignación a eliminar.</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int deleteAsignacionOperacion(cls_asignacionOperacion poOperacion)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_asignacionOperacionDelete";

                cls_parameter[] vu_parametros = 
                    {                   
                        new cls_parameter("@paramOperacion", poOperacion.pFK_Operacion.pPK_Codigo),
                        new cls_parameter("@paramUsuario", poOperacion.pFK_Usuario)
                    };

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.OPERACION_ASIGNACION, poOperacion.pFK_Operacion.pPK_Codigo + "/" + poOperacion.pFK_Usuario);

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al insertar la asignación de la operación.", po_exception);
            }

        }

        /// <summary>
        /// Método que realiza la 
        /// asignación masiva de operaciones,
        /// se pasan dos listas con las asignaciones
        /// a insertar y las asignaciones a eliminar.
        /// </summary>
        /// <param name="poAsignacionesAgregar">cls_asignacionOperacion lista de asignaciones a agregar.</param>
        /// <param name="poAsignacionesEliminar">cls_asignacionOperacion lista de asignaciones a eliminar.</param>
        public static void crearAsignacionMasiva(List<cls_asignacionOperacion> poAsignacionesAgregar, List<cls_asignacionOperacion> poAsignacionesEliminar)
        {
            try
            {
                cls_sqlDatabase.beginTransaction();

                foreach (cls_asignacionOperacion poAsignacion in poAsignacionesAgregar)
                {
                    cls_gestorOperacion.insertAsignacionOperacion(poAsignacion);
                }

                foreach (cls_asignacionOperacion poAsignacion in poAsignacionesEliminar)
                {
                    cls_gestorOperacion.deleteAsignacionOperacion(poAsignacion);
                }

                cls_sqlDatabase.commitTransaction() ;
            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al realizar la asignación masiva de operaciones.", po_exception);
            }
        }


    }
}
