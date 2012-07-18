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
// cls_gestorComponente.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	11    01    2012	Se agrega la modifica la inserción en la bitácora.
// Cristian Arce Jiménez  	23    01    2012	Se agrega el manejo de filtros
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento
{
    public class cls_gestorComponente
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla componente
        /// </summary>
        /// <param name="poComponente">Componente a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertComponente(cls_componente poComponente)
         {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_componenteInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramcodigo", poComponente.pCodigo),
                    new cls_parameter("@paramnombre", poComponente.pNombre),
                    new cls_parameter("@paramdescripcion", poComponente.pDescripcion)                   
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                // Se obtiene el número del registro insertado.
                poComponente.pPK_componente = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.COMPONENTE, "PK_componente"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.COMPONENTE, poComponente.pPK_componente.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el componente.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite actualizar 
       /// un registro en la tabla componente
       /// </summary>
       /// <param name="poComponente">Componente a actualizar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updateComponente(cls_componente poComponente)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_componenteUpdate";
                cls_parameter[] vu_parametros = 
                {              
                    new cls_parameter("@paramPK_componente", poComponente.pPK_componente),
                    new cls_parameter("@paramcodigo", poComponente.pCodigo),
                    new cls_parameter("@paramnombre", poComponente.pNombre),
                    new cls_parameter("@paramdescripcion", poComponente.pDescripcion)  
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.COMPONENTE, poComponente.pPK_componente.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar el componente.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla componente
       /// </summary>
       /// <param name="poComponente">Componente a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deleteComponente(cls_componente poComponente)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_cont_componenteDelete";
                cls_parameter[] vu_parametros = 
                {               
                 		new cls_parameter("@paramPK_componente", poComponente.pPK_componente)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.COMPONENTE, poComponente.pPK_componente.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el componente.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla componente
       /// </summary>
       /// <returns> List<cls_componente>  valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_componente> listarComponente()
       {
           List<cls_componente> vo_lista = null;
           cls_componente poComponente = null;
           try
           {
               String vs_comando = "PA_cont_componenteSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_componente>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poComponente = new cls_componente();

                   poComponente.pPK_componente = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_componente"]);

                   poComponente.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                   poComponente.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   poComponente.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(poComponente);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los componentes.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla componente
       /// </summary>
       /// <returns>poComponente valor del resultado de la ejecución de la sentencia</returns>
       public static cls_componente seleccionarComponente(cls_componente poComponente)
       {
           try
           {
               String vs_comando = "PA_cont_componenteSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_componente", poComponente.pPK_componente) };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poComponente = new cls_componente();

               poComponente.pPK_componente = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_componente"]);

               poComponente.pCodigo = vu_dataSet.Tables[0].Rows[0]["codigo"].ToString();

               poComponente.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

               poComponente.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

               return poComponente;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el componente específico.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de componente con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_componente> listarComponenteFiltro(string psFiltro)
       {
           List<cls_componente> vo_lista = null;
           cls_componente voComponente = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.COMPONENTE, string.Empty, psFiltro);

               vo_lista = new List<cls_componente>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voComponente = new cls_componente();

                   voComponente.pPK_componente = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_componente"]);

                   voComponente.pCodigo = vu_dataSet.Tables[0].Rows[i]["codigo"].ToString();

                   voComponente.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   voComponente.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                   vo_lista.Add(voComponente);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los componentes de manera filtrada.", po_exception);
           }
       }

    }
}
