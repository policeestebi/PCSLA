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
// cls_gestorPaqueteActividad.cs
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
    public class cls_gestorPaqueteActividad
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla PaqueteActividad
        /// </summary>
        /// <param name="po_paqueteActividad">PaqueteActividad a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int insertPaqueteActividad(cls_paqueteActividad po_paqueteActividad)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_paquete_actividadInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_actividad", po_paqueteActividad.pActividad.pPK_Actividad),
                    new cls_parameter("@paramPK_paquete", po_paqueteActividad.pPaquete.pPK_Paquete),
                    new cls_parameter("@paramPK_componente", po_paqueteActividad.pComponente.pPK_componente),
                    new cls_parameter("@paramPK_entregable", po_paqueteActividad.pEntregable.pPK_entregable),
                    new cls_parameter("@paramPK_proyecto", po_paqueteActividad.pProyecto.pPK_proyecto)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.COMPONENTE_PAQUETE, po_paqueteActividad.pProyecto.pPK_proyecto + "/" + po_paqueteActividad.pEntregable.pPK_entregable + "/" + po_paqueteActividad.pComponente.pPK_componente + "/" + po_paqueteActividad.pPaquete.pPK_Paquete);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el componente del paquete.", po_exception);
            }

        }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla componentePaquete
       /// </summary>
       /// <param name="po_paqueteActividad">ComponentePaquete a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int updatePaqueteActividad(cls_paqueteActividad po_paqueteActividad, int pi_accion)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_paquete_actividadUpdate";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_actividad", po_paqueteActividad.pActividad.pPK_Actividad),
                    new cls_parameter("@paramPK_paquete", po_paqueteActividad.pPaquete.pPK_Paquete),
                    new cls_parameter("@paramPK_componente", po_paqueteActividad.pComponente.pPK_componente),
                    new cls_parameter("@paramPK_entregable", po_paqueteActividad.pEntregable.pPK_entregable),
                    new cls_parameter("@paramPK_proyecto", po_paqueteActividad.pProyecto.pPK_proyecto), 
                    new cls_parameter("@paramAccion", pi_accion) 
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.COMPONENTE_PAQUETE, po_paqueteActividad.pProyecto.pPK_proyecto + "/" + po_paqueteActividad.pEntregable.pPK_entregable + "/" + po_paqueteActividad.pComponente.pPK_componente + "/" + po_paqueteActividad.pPaquete.pPK_Paquete);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el componente del paquete.", po_exception);
            }

        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="poProyecto"></param>
       /// <returns></returns>
       public static DataSet selectPaqueteActividad(cls_paqueteActividad po_paqueteActividad)
       {
           try
           {
               String vs_comando = "PA_cont_paqueteActividadSelect";
               cls_parameter[] vu_parametros = { new cls_parameter("@paramPK_proyecto", po_paqueteActividad.pProyecto.pPK_proyecto),
                                                 new cls_parameter("@paramPK_paquete", po_paqueteActividad.pPaquete.pPK_Paquete) };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               return vu_dataSet;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las actividades asociadas a los paquetes.", po_exception);
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="poProyecto"></param>
       /// <returns></returns>
       public static DataSet selectPaqueteActividad(cls_proyecto po_proyecto)
       {
           try
           {
               String vs_comando = "PA_cont_paqueteActividadSelectAll";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_proyecto", po_proyecto.pPK_proyecto)
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               return vu_dataSet;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las actividades asociadas a los paquetes.", po_exception);
           }
       }

    }
}
