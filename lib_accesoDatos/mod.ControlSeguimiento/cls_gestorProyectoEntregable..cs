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
// cls_gestorProyectoEntregable.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historialnamespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento

// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	11    01    2012	Se agrega la modifica la inserción en la bitácora.
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorProyectoEntregable
    {
       /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla Proyecto_Entregable
        /// </summary>
        /// <param name="po_proyectoEntregable">ProyectoEntregable a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertProyectoEntregable(cls_proyectoEntregable po_proyectoEntregable)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_proyecto_entregableInsert";
                cls_parameter[] vu_parametros = 
                {                  
                 	new cls_parameter("@paramPK_proyecto", po_proyectoEntregable.pPK_Proyecto),  	
                    new cls_parameter("@paramPK_entregable", po_proyectoEntregable.pPK_Entregable)
                                         
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.PROYECTO_ENTREGABLE, po_proyectoEntregable.pPK_Proyecto + "/" + po_proyectoEntregable.pPK_Entregable);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar los entregables del proyecto.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite editar 
       /// un registro en la tabla Proyecto Entregable 
       /// </summary>
       /// <param name="po_proyectoEntregable">ProyectoEntregable a editar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateProyectEntregable(cls_proyectoEntregable po_proyectoEntregable, int pi_accion)
       {
                int vi_resultado;

                try
                {
                    String vs_comando = "PA_cont_proyecto_entregableUpdate";
                    cls_parameter[] vu_parametros = 
                    {                   
                 		    new cls_parameter("@paramPK_entregable", po_proyectoEntregable.pPK_Entregable),
                            new cls_parameter("@paramPK_proyecto", po_proyectoEntregable.pPK_Proyecto),
                            new cls_parameter("@paramAccion", pi_accion)                     
                    };

                    cls_sqlDatabase.beginTransaction();

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                    cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.PROYECTO_ENTREGABLE, po_proyectoEntregable.pPK_Proyecto + "/" + + po_proyectoEntregable.pPK_Entregable);

                    cls_sqlDatabase.commitTransaction();

                    return vi_resultado;

                }
                catch (Exception po_exception)
                {
                    cls_sqlDatabase.rollbackTransaction();
                    throw new Exception("Ocurrió un error al eliminar los entregables del proyecto.", po_exception);
                }

        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="po_proyecto"></param>
       /// <returns></returns>
       public static DataSet selectProyectoEntregable(cls_proyecto po_proyecto)
       {
           try
           {
               String vs_comando = "PA_cont_ProyectoEntregableSelect";
               cls_parameter[] vu_parametros = { new cls_parameter("@paramPK_proyecto", po_proyecto.pPK_proyecto)};

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
