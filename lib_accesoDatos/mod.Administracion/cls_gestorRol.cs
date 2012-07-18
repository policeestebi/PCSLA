using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using COSEVI.CSLA.lib.accesoDatos;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.App_Database;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorRol.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
//								
//								
//
//======================================================================

namespace  COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{
  
    public class cls_gestorRol
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla rol
        /// </summary>
        /// <param name="poRol">Rol a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	     public static int insertRol(cls_rol poRol)
         {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_RolInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramdescripcion", poRol.pDescripcion),
                    new cls_parameter("@paramnombre", poRol.pNombre),
                    new cls_parameter("@paramvisible", poRol.pVisible)
                };


                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                return vi_resultado;

            }
            catch (Exception)
            {
                throw;
            }

    }

         /// <summary>
         /// Método que permite actualizar 
         /// un registro en la tabla rol
         /// </summary>
         /// <param name="poRol">Rol a actualizar</param>
         /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
           public static int updateRol(cls_rol poRol)
           {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_RolUpdate";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramPK_rol", poRol.pPK_rol),
                    new cls_parameter("@paramdescripcion", poRol.pDescripcion),
                    new cls_parameter("@paramnombre", poRol.pNombre),
                    new cls_parameter("@paramvisible", poRol.pVisible)
                };


                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                return vi_resultado;

            }
            catch (Exception)
            {
                throw;
            }

    }
           /// <summary>
           /// Método que permite eliminar 
           /// un registro en la tabla rol
           /// </summary>
           /// <param name="poRol">Rol a eliminar</param>
           /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
           public static int deleteRol(cls_rol poRol)
           {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_RolDelete";
                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramPK_rol", poRol.pPK_rol)  
                };


                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                return vi_resultado;

            }
            catch (Exception)
            {
                throw;
            }

    }

           /// <summary>
           /// Método que permite listar 
           /// todos los registros en la tabla rol
           /// </summary>
           /// <returns> List<cls_rol>  valor del resultado de la ejecución de la sentencia</returns>
           public static List<cls_rol> listarRol()
           {
               List<cls_rol> vo_lista = null;
               cls_rol poRol = null;
               try
               {
                   String vs_comando = "PA_admi_rolSelect";
                   cls_parameter[] vu_parametros = { };

                   DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                   vo_lista = new List<cls_rol>();
                   for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                   {
                       poRol = new cls_rol();

                       poRol.pPK_rol = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_rol"]);

                       poRol.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                       poRol.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                       poRol.pVisible = Convert.ToBoolean(vu_dataSet.Tables[0].Rows[i]["visible"]);


                       vo_lista.Add(poRol);
                   }

                   return vo_lista;
               }
               catch (Exception)
               {
                   throw;
               }
           }

           /// <summary>
           /// Método que permite seleccionar  
           /// un único registro en la tabla rol
           /// </summary>
           /// <returns>poRol valor del resultado de la ejecución de la sentencia</returns>
           public static cls_rol seleccionarRol(cls_rol poRol)
           {
               try
               {
                   String vs_comando = "PA_admi_rolSelectOne";
                   cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_rol", poRol.pPK_rol) 
                                               };

                   DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                   poRol = new cls_rol();

                   poRol.pPK_rol = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_rol"]);

                   poRol.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

                   poRol.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

                   poRol.pVisible = Convert.ToBoolean(vu_dataSet.Tables[0].Rows[0]["visible"]);

                   return poRol;

               }
               catch (Exception)
               {
                   throw;
               }
           }
    }
}
