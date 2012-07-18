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
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorBitacora.cs
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

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{
    public class cls_gestorBitacora
    {
       /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla bitacora
        /// </summary>
        /// <param name="poBitacora">Bitacora a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertBitacora(cls_bitacora poBitacora)
        {
            int vi_resultado;

            try
            {
                poBitacora.pPK_bitacora = (Convert.ToDecimal(cls_gestorUtil.selectMax(cls_constantes.BITACORA, "PK_bitacora")) + 1);

                String vs_comando = "PA_admi_bitacoraInsert";
                cls_parameter[] vu_parametros = 
                {
                    
                    new cls_parameter("@paramPK_bitacora",  poBitacora.pPK_bitacora),
                    new cls_parameter("@paramFK_departamento", DBNull.Value),
                    new cls_parameter("@paramFK_usuario", poBitacora.pFK_usuario),
                    new cls_parameter("@paramaccion", poBitacora.pAccion),
                    new cls_parameter("@paramfecha_accion", poBitacora.pFechaAccion),
                    new cls_parameter("@paramnumero_registro", poBitacora.pNumeroRegistro),
                    new cls_parameter("@paramtabla", poBitacora.pTabla),
                    new cls_parameter("@parammaquina", poBitacora.pMaquina)
                };

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al insertar el registro en la bitácora.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla bitacora
       /// </summary>
       /// <returns> List<cls_bitacora>  valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_bitacora> listarBitacora()
       {
           List<cls_bitacora> vo_lista = null;
           cls_bitacora poBitacora = null;
           try
           {
               String vs_comando = "PA_admi_bitacoraSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_bitacora>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poBitacora = new cls_bitacora();

                   poBitacora.pPK_bitacora = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_bitacora"]);

                   poBitacora.pFK_departamento = Convert.IsDBNull(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]) ? 1 : Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]);

                   poBitacora.pFK_usuario = vu_dataSet.Tables[0].Rows[i]["FK_usuario"].ToString();

                   poBitacora.pAccion = vu_dataSet.Tables[0].Rows[i]["accion"].ToString();

                   poBitacora.pFechaAccion = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fecha_accion"]);

                   poBitacora.pNumeroRegistro = vu_dataSet.Tables[0].Rows[i]["numero_registro"].ToString();

                   poBitacora.pTabla = vu_dataSet.Tables[0].Rows[i]["tabla"].ToString();

                   poBitacora.pMaquina = vu_dataSet.Tables[0].Rows[i]["maquina"].ToString();

                   vo_lista.Add(poBitacora);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de la bitácora.", po_exception);
           }
       }

    /// <summary>
    /// Método que obtiene la lista
    /// de la bitácora de manera filtrada.
    /// </summary>
    /// <param name="pd_fechaInicial"></param>
    /// <param name="pd_fechaFinal"></param>
    /// <param name="ps_usuarioDesde"></param>
    /// <param name="ps_usuarioHasta"></param>
    /// <param name="ps_accion"></param>
    /// <param name="ps_tabla"></param>
    /// <param name="ps_registro"></param>
    /// <returns></returns>
       public static List<cls_bitacora> listarBitacoraFiltro
           (
                DateTime pd_fechaInicial,
               DateTime pd_fechaFinal,
               string ps_usuarioDesde,
               string ps_usuarioHasta,
               string ps_accion,
               string ps_tabla,
               string ps_registro
           )
       {
           List<cls_bitacora> vo_lista = null;
           cls_bitacora poBitacora = null;
           try
           {
               String vs_comando = "PA_admi_bitacoraSelectFiltro";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramfechaInicio",  pd_fechaInicial),
                                                   new cls_parameter("@paramfechaFinal",  pd_fechaFinal),
                                                   new cls_parameter("@paramUsuarioDesde ",  ps_usuarioDesde),
                                                   new cls_parameter("@paramUsuarioHasta ",  ps_usuarioHasta),
                                                   new cls_parameter("@paramTabla",  ps_tabla),
                                                   new cls_parameter("@paramAccion",  ps_accion),
                                                   new cls_parameter("@paramRegistro",  ps_registro)
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_bitacora>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poBitacora = new cls_bitacora();

                   poBitacora.pPK_bitacora = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_bitacora"]);

                   poBitacora.pFK_departamento = Convert.IsDBNull(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]) ? 1 : Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]);

                   poBitacora.pFK_usuario = vu_dataSet.Tables[0].Rows[i]["FK_usuario"].ToString();

                   poBitacora.pAccion = vu_dataSet.Tables[0].Rows[i]["accion"].ToString();

                   poBitacora.pFechaAccion = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fecha_accion"]);

                   poBitacora.pNumeroRegistro = vu_dataSet.Tables[0].Rows[i]["numero_registro"].ToString();

                   poBitacora.pTabla = vu_dataSet.Tables[0].Rows[i]["tabla"].ToString();

                   poBitacora.pMaquina = vu_dataSet.Tables[0].Rows[i]["maquina"].ToString();

                   vo_lista.Add(poBitacora);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de la bitácora.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla bitacora
       /// </summary>
       /// <returns>poBitacora valor del resultado de la ejecución de la sentencia</returns>
       public static cls_bitacora seleccionarBitacora(cls_bitacora poBitacora)
       {
           try
           {
               String vs_comando = "PA_admi_bitacoraSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_bitacora", poBitacora.pPK_bitacora) 
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poBitacora = new cls_bitacora();

               poBitacora.pPK_bitacora = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_bitacora"]);

               poBitacora.pFK_departamento = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_departamento"]);

               poBitacora.pFK_usuario = vu_dataSet.Tables[0].Rows[0]["FK_usuario"].ToString();

               poBitacora.pAccion = vu_dataSet.Tables[0].Rows[0]["accion"].ToString();

               poBitacora.pFechaAccion = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[0]["fecha_accion"]);

               poBitacora.pNumeroRegistro = vu_dataSet.Tables[0].Rows[0]["numero_registro"].ToString();

               poBitacora.pTabla = vu_dataSet.Tables[0].Rows[0]["tabla"].ToString();

               poBitacora.pMaquina = vu_dataSet.Tables[0].Rows[0]["maquina"].ToString();

               return poBitacora;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el registro específico de la bitácora.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de bitácora con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_bitacora> listarBitacoraFiltro(string psFiltro)
       {
           List<cls_bitacora> vo_lista = null;
           cls_bitacora voBitacora = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.BITACORA, string.Empty, psFiltro);

               vo_lista = new List<cls_bitacora>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voBitacora = new cls_bitacora();

                   voBitacora.pPK_bitacora = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_bitacora"]);

                   voBitacora.pFK_departamento = Convert.IsDBNull(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]) ? 1 : Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_departamento"]);

                   voBitacora.pFK_usuario = vu_dataSet.Tables[0].Rows[i]["Fk_usuario"].ToString();

                   voBitacora.pAccion = vu_dataSet.Tables[0].Rows[i]["accion"].ToString();

                   voBitacora.pFechaAccion = Convert.ToDateTime(vu_dataSet.Tables[0].Rows[i]["fecha_accion"]);

                   voBitacora.pNumeroRegistro = vu_dataSet.Tables[0].Rows[i]["numero_registro"].ToString();

                   voBitacora.pTabla = vu_dataSet.Tables[0].Rows[i]["tabla"].ToString();

                   voBitacora.pMaquina = vu_dataSet.Tables[0].Rows[i]["maquina"].ToString();

                   vo_lista.Add(voBitacora);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de bitácora de manera filtrada.", po_exception);
           }
       }

    }
}
