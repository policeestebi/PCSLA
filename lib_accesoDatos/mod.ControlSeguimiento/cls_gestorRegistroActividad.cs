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

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorRegistroActividad.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Esteban Ramírez G.	    04    09    2012    Se crea la clase
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorRegistroActividad
    {

        /// <summary>
        /// Método que obtiene la lista
        /// del registro de actividades
        /// que cuenta un usuario.
        /// </summary>
        /// <param name="psUsuario">String código el usuario.</param>
        /// <param name="psProyecto">String código del proyecto.</param>
        /// <param name="pdFechaInicio">String fecha inicio.</param>
        /// <param name="pdFechaFinal">String fecha final.</param>
        /// <returns></returns>
        public static DataSet listarRegistroActividadesUsuario(string psUsuario, string psProyecto, DateTime pdFechaInicio, DateTime pdFechaFinal)
        {
            DataSet vu_dataSet = null;
            try
            {
                String vs_comando = "PA_cont_registroActividadSelectUsuario";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramProyecto", psProyecto),
                    new cls_parameter("@paramUsuario", psUsuario),
                    new cls_parameter("@paramFechaInicio", pdFechaInicio),
                    new cls_parameter("@paramFechaFin", pdFechaFinal)
                };

                vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);
            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al obtener el listado del registro de actividades.", po_exception);
            }

            return vu_dataSet;
        }


        /// <summary>
        /// Inserta un registro nuevo
        /// en la tabla de t_cont_registro_actividad.
        /// </summary>
        /// <param name="poRegistro">cls_registroActividad con la información.</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int insertRegistroActividad(cls_registroActividad poRegistro)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_actividadRegistroInsert";


                cls_parameter[] vu_parametros = 
                    {                   
                        new cls_parameter("@paramActividad", poRegistro.pAsignacion.pPK_Actividad ),
                        new cls_parameter("@paramPaquete", poRegistro.pAsignacion.pPK_Paquete),
                        new cls_parameter("@paramComponente", poRegistro.pAsignacion.pPK_Componente),
                        new cls_parameter("@paramEntregable", poRegistro.pAsignacion.pPK_Entregable),
                        new cls_parameter("@paramProyecto", poRegistro.pAsignacion.pPK_Proyecto),
                        new cls_parameter("@paramComentario", poRegistro.pComentario),
                        new cls_parameter("@paramUsuario", poRegistro.pAsignacion.pPK_Usuario),
                        new cls_parameter("@paramFecha", poRegistro.pFecha),
                        new cls_parameter("@paramHoras", poRegistro.pHoras)
                    };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                poRegistro.pRegistro = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.ACTIVIDAD_REGISTRO, "PK_registro"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ACTIVIDAD_REGISTRO, poRegistro.pRegistro + 
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Actividad +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Paquete +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Componente +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Proyecto +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Usuario,poRegistro.pUsuarioTransaccion);

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
        /// Inserta un nuevo registro de tiempos
        /// de una actividad.
        /// </summary>
        /// <param name="poOperacion">cls_registroActividad a insertar</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int updateRegistroActividad(cls_registroActividad poRegistro)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_actividadRegistroUpdate";


                cls_parameter[] vu_parametros = 
                    {   
                        new cls_parameter("@paramRegistro", poRegistro.pRegistro ),
                        new cls_parameter("@paramActividad", poRegistro.pAsignacion.pPK_Actividad ),
                        new cls_parameter("@paramPaquete", poRegistro.pAsignacion.pPK_Paquete),
                        new cls_parameter("@paramComponente", poRegistro.pAsignacion.pPK_Componente),
                        new cls_parameter("@paramEntregable", poRegistro.pAsignacion.pPK_Entregable),
                        new cls_parameter("@paramProyecto", poRegistro.pAsignacion.pPK_Proyecto),
                        new cls_parameter("@paramComentario", poRegistro.pComentario),
                        new cls_parameter("@paramUsuario", poRegistro.pAsignacion.pPK_Usuario),
                        new cls_parameter("@paramHoras", poRegistro.pHoras)
                    };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.ACTIVIDAD_REGISTRO, poRegistro.pRegistro +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Actividad +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Paquete +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Componente +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Proyecto +
                                                                                                                        "/" + poRegistro.pAsignacion.pPK_Usuario, poRegistro.pUsuarioTransaccion);

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
        /// Método que selecciona
        /// un registro de actividad especifivo.
        /// </summary>
        /// <param name="poRegistro">cls_registroActividad registro.</param>
        /// <returns>cls_registroActividad</returns>
        public static cls_registroActividad seleccionarRegistroActividad(cls_registroActividad poRegistro)
        {
            try
            {
                String vs_comando = "PA_cont_actividadRegistroSelectOne";
                cls_parameter[] vu_parametros = { 
                                                       new cls_parameter("@paramRegistro", poRegistro.pRegistro),
                                                       new cls_parameter("@paramActividad", poRegistro.pAsignacion.pPK_Actividad),
                                                       new cls_parameter("@paramPaquete", poRegistro.pAsignacion.pPK_Paquete),
                                                       new cls_parameter("@paramComponente", poRegistro.pAsignacion.pPK_Componente),
                                                       new cls_parameter("@paramEntregable", poRegistro.pAsignacion.pPK_Entregable),
                                                       new cls_parameter("@paramProyecto", poRegistro.pAsignacion.pPK_Proyecto),
                                                       new cls_parameter("@paramUsuario", poRegistro.pAsignacion.pPK_Usuario),
                                                   };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                poRegistro.pHoras = Convert.ToDecimal(vu_dataSet.Tables[0].Rows[0]["horas"].ToString());

                poRegistro.pComentario = vu_dataSet.Tables[0].Rows[0]["comentario"].ToString();

                return poRegistro;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al obtener un registro de operación específico.", po_exception);
            }
        }

        /// <summary>
        /// Método que determina si una 
        /// actividad asociada a un usuario se encuentra atrasada o 
        /// el número de horas supera lo estimado
        /// </summary>
        /// <param name="poRegistro">Registro de tiempo</param>
        /// <param name="pbRetrasada">Determina si la actividad esta atrasada.</param>
        /// <param name="pbSuperaEstimado">Determina si la actividad supera el estimado.</param>
        public static void verificarActividadAtrasada(cls_asignacionActividad poAsignacion, out bool pbRetrasada, out bool pbSuperaEstimado)
        {
            pbRetrasada = false;
            pbSuperaEstimado = false;

            try
            {
                String vs_comando = "PA_cont_verificarActividadRetrasada";
                cls_parameter[] vu_parametros = { 
                                                       new cls_parameter("@paramProyecto", poAsignacion.pPK_Proyecto),
                                                       new cls_parameter("@paramActividad", poAsignacion.pPK_Actividad),
                                                       new cls_parameter("@paramPaquete", poAsignacion.pPK_Paquete),
                                                       new cls_parameter("@paramComponente", poAsignacion.pPK_Componente),
                                                       new cls_parameter("@paramEntregable", poAsignacion.pPK_Entregable),
                                                       new cls_parameter("@paramUsuario", poAsignacion.pPK_Usuario),
                                                   };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                if (vu_dataSet != null && vu_dataSet.Tables[0].Rows.Count == 1)
                {
                    pbRetrasada = vu_dataSet.Tables[0].Rows[0]["ATRASADA"].ToString().Equals("1") ? true : false;

                    pbSuperaEstimado = vu_dataSet.Tables[0].Rows[0]["SUPERA_ESTIMADO"].ToString().Equals("1") ? true : false;
                }
            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al determina si la actividad se encuentra atrasada.", po_exception);
            }
        }

    }
}
