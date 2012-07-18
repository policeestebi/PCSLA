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
// cls_gestorDepartamentoProyecto.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            19    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	11    01    2012	Se agrega la modifica la inserción en la bitácora.
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorDepartamentoProyecto
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla departamentoProyecto
        /// </summary>
        /// <param name="poDepartamentoProyecto">Estado a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertDepartamentoProyecto(cls_departamentoProyecto poDepartamentoProyecto)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_departamento_proyectoInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_departamento", poDepartamentoProyecto.pPK_departamento),
                    new cls_parameter("@paramPK_proyecto", poDepartamentoProyecto.pPK_proyecto)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.DEPARTAMENTO_PROYECTO, poDepartamentoProyecto.pPK_departamento + "/" + poDepartamentoProyecto.pPK_proyecto);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el departamento del proyecto.", po_exception);
            }

        }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla departamentoProyecto
       /// </summary>
       /// <param name="poDepartamentoProyecto">departamentoProyecto a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteDepartamentoProyecto(cls_departamentoProyecto poDepartamentoProyecto)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_departamento_proyectoDelete";
                cls_parameter[] vu_parametros = 
                {
                    
                 	new cls_parameter("@paramPK_departamento", poDepartamentoProyecto.pPK_departamento),
                    new cls_parameter("@paramPK_proyecto", poDepartamentoProyecto.pPK_proyecto)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.DEPARTAMENTO_PROYECTO, poDepartamentoProyecto.pPK_departamento + "/" + poDepartamentoProyecto.pPK_proyecto);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el registro.", po_exception);
            }

        }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla departamentoProyecto
       /// </summary>
       /// <param name="poDepartamentoProyecto">departamentoProyecto a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteDepartamentoProyectoMasivo(cls_departamentoProyecto poDepartamentoProyecto)
       {
           int vi_resultado;

           try
           {
               String vs_comando = "PA_cont_departamento_proyectoDeleteMasivo";
               cls_parameter[] vu_parametros = 
                {
                    
                 	new cls_parameter("@paramPK_proyecto", poDepartamentoProyecto.pPK_proyecto)
                };

               cls_sqlDatabase.beginTransaction();

               vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

               cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.DEPARTAMENTO_PROYECTO, poDepartamentoProyecto.pPK_departamento + "/Masivo");

               cls_sqlDatabase.commitTransaction();

               return vi_resultado;

           }
           catch (Exception po_exception)
           {
               cls_sqlDatabase.rollbackTransaction();
               throw new Exception("Ocurrió un error al eliminar el registro.", po_exception);
           }

       }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       public static DataSet listarDepartamento()
       {
           try
           {
               String vs_comando = "PA_admi_departamentoSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               return vu_dataSet;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los departamentos.", po_exception);
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="poProyecto"></param>
       /// <returns></returns>
       public static DataSet selectDepartamentoProyecto(cls_proyecto poProyecto)
       {
           try
           {
               String vs_comando = "PA_cont_departamentoProyectoSelect";
               cls_parameter[] vu_parametros = { new cls_parameter("@paramPK_proyecto", poProyecto.pPK_proyecto) };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               return vu_dataSet;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los departamentos.", po_exception);
           }
       }

    }
}

