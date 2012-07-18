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
// cls_gestorEntregableComponente.cs
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
    public class cls_gestorEntregableComponente
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla entregableComponente
        /// </summary>
        /// <param name="poEntregableComponente">EntregableComponente a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertEntregableComponente(cls_entregableComponente po_entregableComponente)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_entregable_componenteInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_componente", po_entregableComponente.pPK_Componente),
                    new cls_parameter("@paramPK_entregable", po_entregableComponente.pPK_Entregable),
                    new cls_parameter("@paramPK_proyecto", po_entregableComponente.pPK_Proyecto)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ENTREGABLE_COMPONENTE, po_entregableComponente.pPK_Proyecto + "/" + po_entregableComponente.pPK_Entregable + "/" + po_entregableComponente.pPK_Componente);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el componente del entregable.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla entregableComponente
       /// </summary>
       /// <param name="po_entregableComponente">EntregableComponente a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateEntregableComponente(cls_entregableComponente po_entregableComponente, int pi_accion)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_entregable_componenteUpdate";
                cls_parameter[] vu_parametros = 
                {                   
                 	new cls_parameter("@paramPK_componente", po_entregableComponente.pPK_Componente),
                    new cls_parameter("@paramPK_entregable", po_entregableComponente.pPK_Entregable),
                    new cls_parameter("@paramPK_proyecto", po_entregableComponente.pPK_Proyecto),
                    new cls_parameter("@paramAccion", pi_accion)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.ENTREGABLE_COMPONENTE, po_entregableComponente.pPK_Proyecto + "/" + po_entregableComponente.pPK_Entregable + "/" + po_entregableComponente.pPK_Componente);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el componente del entregable.", po_exception);
            }

    }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="poProyecto"></param>
       /// <returns></returns>
       public static DataSet selectEntregableComponente(cls_entregableComponente po_entregableComponente)
       {
           try
           {
               String vs_comando = "PA_cont_EntregableComponenteSelect";
               cls_parameter[] vu_parametros = { new cls_parameter("@paramPK_proyecto", po_entregableComponente.pProyecto.pPK_proyecto),
                                                    new cls_parameter("@paramPK_entregable", po_entregableComponente.pEntregable.pPK_entregable)};

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               return vu_dataSet;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los entregables.", po_exception);
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="poProyecto"></param>
       /// <returns></returns>
       public static DataSet selectEntregableComponente(cls_proyecto po_proyecto)
       {
           try
           {
               String vs_comando = "PA_cont_entregableComponenteSelectAll";
               cls_parameter[] vu_parametros = { new cls_parameter("@paramPK_proyecto", po_proyecto.pPK_proyecto)
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               return vu_dataSet;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los entregables.", po_exception);
           }
       }
    }
}
