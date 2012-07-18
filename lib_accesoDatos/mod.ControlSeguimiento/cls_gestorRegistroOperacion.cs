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
    public class cls_gestorRegistroOperacion
    {

        /// <summary>
        /// Lista todas las operaciones
        /// que no esten asociadas a un proyecto.
        /// </summary>
        /// <param name="psUsuario">código del usuario.</param>
        /// <param name="pstipo">tipo de operacion.</param>
        /// <returns>DataSet</returns>
        public static DataSet listarOperacionesUsuario(string psUsuario, string psTipo, DateTime pdFechaInicio, DateTime pdFechaFinal)
        {
            DataSet vu_dataSet = null;
            try
            {
                String vs_comando = "PA_cont_registroOperacionSelectUsuario";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramUsuario", psUsuario),
                    new cls_parameter("@paramTipo", psTipo),
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
        /// Inserta una operación.
        /// </summary>
        /// <param name="poOperacion">cls_asignacionOperacion a insertar</param>
        /// <returns>valor del resultado de la ejecución de la sentencia</returns>
        public static int insertRegistroOperacion(cls_registroOperacion poRegistro)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_operacionRegistroInsert";


                cls_parameter[] vu_parametros = 
                    {                   
                        new cls_parameter("@paramPK_codigo", poRegistro.pFK_Asignacion.pFK_Operacion.pPK_Codigo),
                        new cls_parameter("@paramComentario", poRegistro.pComentario),
                        new cls_parameter("@paramUsuario", poRegistro.pFK_Asignacion.pFK_Usuario),
                        new cls_parameter("@paramFecha", poRegistro.pFecha),
                        new cls_parameter("@paramHoras", poRegistro.pHoras)
                    };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                poRegistro.pPK_registro = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.OPERACION_REGISTRO, "PK_registro"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.OPERACION_REGISTRO, poRegistro.pPK_registro + "/" + poRegistro.pFK_Asignacion.pFK_Operacion.pPK_Codigo + "/" + poRegistro.pFK_Asignacion.pFK_Usuario);

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
        /// Método que obtiene la información de un
        /// registro de operación
        /// </summary>
        /// <param name="poOperacion">cls_registroOperacion operación</param>
        /// <returns></returns>
        public static cls_registroOperacion seleccionarRegistroOperacion(cls_registroOperacion poRegistro)
        {
            try
            {
                String vs_comando = "PA_cont_operacionRegistroSelectOne";
                cls_parameter[] vu_parametros = { 
                                                       new cls_parameter("@paramPK_registro", poRegistro.pPK_registro),
                                                       new cls_parameter("@paramPK_codigo", poRegistro.pFK_Asignacion.pFK_Operacion.pPK_Codigo),
                                                       new cls_parameter("@paramUsuario", poRegistro.pFK_Asignacion.pFK_Usuario)
                                                   };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                poRegistro.pHoras = Convert.ToDecimal( vu_dataSet.Tables[0].Rows[0]["horas"].ToString());

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
        /// Método que actualiza un 
        /// registro de la tabla t_cont_operacion_registro
        /// </summary>
        /// <param name="poOperacion"></param>
        /// <returns></returns>
        public static int updateRegistroOperacion(cls_registroOperacion poRegistro)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_operacionRegistroUpdate";
                cls_parameter[] vu_parametros = 
                    {                   
                 		     new cls_parameter("@paramPK_registro", poRegistro.pPK_registro),
                             new cls_parameter("@paramPK_codigo", poRegistro.pFK_Asignacion.pFK_Operacion.pPK_Codigo),
                             new cls_parameter("@paramUsuario", poRegistro.pFK_Asignacion.pFK_Usuario),
                             new cls_parameter("@paramHoras", poRegistro.pHoras),
                             new cls_parameter("@comentario", poRegistro.pComentario)
                    };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.OPERACION_REGISTRO, poRegistro.pPK_registro + "/" + poRegistro.pFK_Asignacion.pFK_Operacion.pPK_Codigo + "/" + poRegistro.pFK_Asignacion.pFK_Usuario);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar la operación.", po_exception);
            }

        }

    }
}
