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
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;

//=======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_gestorPagina.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Esteban Ramírez          10    01    2012    Se agrega la inserción en la bitácora.
// Cristian Arce Jiménez	01    22    2012	Modificación en la búsqueda através de filtros
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{
  
    public class cls_gestorPagina
    {
       /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla pagina
        /// </summary>
        /// <param name="poPagina">Pagina a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
	   public static int insertPagina(cls_pagina poPagina)
        {
            int vi_resultado;
           cls_paginaPermiso vo_paginaPermiso = null;
            try
            {
                String vs_comando = "PA_admi_paginaInsert";

                cls_sqlDatabase.beginTransaction();
                
                if (poPagina.FK_menu != 0)
                {
                    cls_parameter[] vu_parametros = 
                        {
                            new cls_parameter("@paramFK_menu", poPagina.FK_menu),
                            new cls_parameter("@paramnombre", poPagina.pNombre),
                            new cls_parameter("@paramurl", poPagina.pUrl),
                            new cls_parameter("@paramheight", poPagina.pHeight)
                        };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }
                else
                {
                    cls_parameter[] vu_parametros = 
                        {
                            new cls_parameter("@paramFK_menu", DBNull.Value),
                            new cls_parameter("@paramnombre", poPagina.pNombre),
                            new cls_parameter("@paramurl", poPagina.pUrl),
                            new cls_parameter("@paramheight", poPagina.pHeight)
                        };
                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }


                poPagina.pPK_pagina =Convert.ToInt32( cls_gestorUtil.selectMax(cls_constantes.PAGINA, "PK_pagina"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.PAGINA, poPagina.pPK_pagina.ToString(),poPagina.pUsuarioTransaccion);

                foreach(cls_permiso vo_permiso in poPagina.Permisos)
                {
                    vo_paginaPermiso = new cls_paginaPermiso();
                    vo_paginaPermiso.pPK_pagina = poPagina.pPK_pagina;
                    vo_paginaPermiso.pPK_permiso = vo_permiso.pPK_permiso;

                    cls_gestorPaginaPermiso.insertPaginaPermiso(vo_paginaPermiso);
                }

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar la página.", po_exception);
            }

    }
       
        /// <summary>
        /// Método que permite actualizar 
        /// un registro en la tabla pagina
        /// </summary>
        /// <param name="poPagina">Pagina a actualizar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int updatePagina(cls_pagina poPagina)
       {
            int vi_resultado;
            cls_paginaPermiso vo_paginaPermiso = null;
            cls_pagina vo_pagina = null;
            try
            {
                String vs_comando = "PA_admi_paginaUpdate";

                cls_sqlDatabase.beginTransaction();

                if (poPagina.FK_menu != 0)
                {

                    cls_parameter[] vu_parametros = 
                    {
                 	    new cls_parameter("@paramPK_pagina", poPagina.pPK_pagina),
                        new cls_parameter("@paramFK_menu", poPagina.FK_menu),
                        new cls_parameter("@paramnombre", poPagina.pNombre),
                        new cls_parameter("@paramurl", poPagina.pUrl),
                        new cls_parameter("@paramheight", poPagina.pHeight)
                    };
                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }
                else
                {
                    cls_parameter[] vu_parametros = 
                    {
                 	    new cls_parameter("@paramPK_pagina", DBNull.Value),
                        new cls_parameter("@paramFK_menu", poPagina.FK_menu),
                        new cls_parameter("@paramnombre", poPagina.pNombre),
                        new cls_parameter("@paramurl", poPagina.pUrl),
                        new cls_parameter("@paramheight", poPagina.pHeight)
                    };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.PAGINA, poPagina.pPK_pagina.ToString(), poPagina.pUsuarioTransaccion);

                vo_pagina = new cls_pagina();
                vo_pagina.pPK_pagina = poPagina.pPK_pagina;

                vo_pagina = cls_gestorPagina.seleccionarPagina(vo_pagina);


                foreach (cls_permiso vo_permiso in poPagina.Permisos)
                {
                    if (!vo_pagina.Permisos.Exists(c => c.pPK_permiso == vo_permiso.pPK_permiso)) 
                    {
                        vo_paginaPermiso = new cls_paginaPermiso();
                        vo_paginaPermiso.pPK_pagina = poPagina.pPK_pagina;
                        vo_paginaPermiso.pPK_permiso = vo_permiso.pPK_permiso;

                        cls_gestorPaginaPermiso.insertPaginaPermiso(vo_paginaPermiso);
                    }
                }

                foreach (cls_permiso vo_permiso in vo_pagina.Permisos)
                {
                    if (!poPagina.Permisos.Exists(c => c.pPK_permiso == vo_permiso.pPK_permiso))
                    {
                        vo_paginaPermiso = new cls_paginaPermiso();
                        vo_paginaPermiso.pPK_pagina = poPagina.pPK_pagina;
                        vo_paginaPermiso.pPK_permiso = vo_permiso.pPK_permiso;

                        cls_gestorPaginaPermiso.deletePaginaPermiso(vo_paginaPermiso);
                    }
                }

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar la página.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite eliminar 
       /// un registro en la tabla pagina
       /// </summary>
       /// <param name="poPagina">Pagina a eliminar</param>
       /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
       public static int deletePagina(cls_pagina poPagina)
       {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_paginaDelete";

                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramPK_pagina", poPagina.pPK_pagina)  
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.PAGINA, poPagina.pPK_pagina.ToString(), poPagina.pUsuarioTransaccion);

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar la página.", po_exception);
            }

    }

       /// <summary>
       /// Método que permite listar 
       /// todos los registros en la tabla pagina
       /// </summary>
       /// <returns> List<cls_pagina>  valor del resultado de la ejecución de la sentencia</returns>
       public static List<cls_pagina> listarPagina()
       {
           List<cls_pagina> vo_lista = null;
           cls_pagina poPagina = null;
           try
           {
               String vs_comando = "PA_admi_paginaSelect";
               cls_parameter[] vu_parametros = { };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               vo_lista = new List<cls_pagina>();
               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   poPagina = new cls_pagina();

                   poPagina.pPK_pagina = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_pagina"]);

                   poPagina.FK_menu = vu_dataSet.Tables[0].Rows[i]["FK_menu"] != DBNull.Value ? Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_menu"]) : -1;

                   poPagina.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();
                   
                   poPagina.pUrl = vu_dataSet.Tables[0].Rows[i]["url"].ToString();

                   poPagina.pHeight = vu_dataSet.Tables[0].Rows[i]["height"].ToString();

                   vo_lista.Add(poPagina);
               }


               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de las páginas.", po_exception);
           }
       }

       /// <summary>
       /// Método que permite seleccionar  
       /// un único registro en la tabla pagina
       /// </summary>
       /// <returns>poPagina valor del resultado de la ejecución de la sentencia</returns>
       public static cls_pagina seleccionarPagina(cls_pagina poPagina)
       {
           try
           {
               String vs_comando = "PA_admi_paginaSelectOne";
               cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_pagina", poPagina.pPK_pagina) 
                                               };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               poPagina = new cls_pagina();

               poPagina.pPK_pagina = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_pagina"]);

               poPagina.FK_menu = vu_dataSet.Tables[0].Rows[0]["FK_menu"] != DBNull.Value ? Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_menu"]) : -1;

               poPagina.pNombre = vu_dataSet.Tables[0].Rows[0]["nombre"].ToString();

               poPagina.pUrl = vu_dataSet.Tables[0].Rows[0]["url"].ToString();

               poPagina.pHeight = vu_dataSet.Tables[0].Rows[0]["height"].ToString();

               poPagina.Permisos = cls_gestorPaginaPermiso.listarPaginaPermiso(poPagina);

               return poPagina;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al seleccionar la página específica.", po_exception);
           }
       }

       /// <summary>
       /// Hace un lista de páginas con un filtrado específico.
       /// </summary>
       /// <param name="psFiltro">String filtro.</param>
       /// <returns></returns>
       public static List<cls_pagina> listarPaginaFiltro(string psFiltro)
       {
           List<cls_pagina> vo_lista = null;
           cls_pagina voPagina = null;
           try
           {
               DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.PAGINA, string.Empty, psFiltro);

               vo_lista = new List<cls_pagina>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   voPagina = new cls_pagina();

                   voPagina.pPK_pagina = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_pagina"]);

                   voPagina.pNombre = string.IsNullOrEmpty(vu_dataSet.Tables[0].Rows[i]["nombre"].ToString()) ? string.Empty : vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   voPagina.pUrl = string.IsNullOrEmpty(vu_dataSet.Tables[0].Rows[i]["url"].ToString()) ? string.Empty : vu_dataSet.Tables[0].Rows[i]["url"].ToString();

                   vo_lista.Add(voPagina);
               }

               return vo_lista;
           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener el listado de los páginas de manera filtrada.", po_exception);
           }
       }

       /// <summary>
       /// Obtiene los permisos
       /// asociados a una página
       /// esto para determinar si
       /// el usuario posee privilegios
       /// para realizar las diferentes opciones
       /// en el sistema.
       /// </summary>
       /// <param name="psUrl">String url de la página.</param>
       /// <param name="piRol">Int código del rol.</param>
       /// <returns>cls_pagina con los datos necesarios para el manejo de los permisos asociados.</returns>
       public static cls_pagina obtenerPermisoPaginaRol(string psUrl, int piRol)
       {
           List<cls_permiso> loPermisos = null;
           cls_permiso loPermiso = null;
           cls_pagina loPagina = null;
           try
           {
               String vs_comando = "PA_admi_selectPaginaRolPermisos";
               cls_parameter[] vu_parametros = {
                                                    new cls_parameter("@paramPagina", psUrl) ,
                                                    new cls_parameter("@paramRol", piRol) ,
                                                };

               DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

               loPermisos = new List<cls_permiso>();

               for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
               {
                   loPermiso = new cls_permiso();

                   loPermiso.pPK_permiso = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_permiso"]);

                   loPermiso.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                   loPermisos.Add(loPermiso);
               }

               loPagina = new cls_pagina();
               loPagina.pUrl = psUrl;
               loPagina.Permisos = loPermisos;

           }
           catch (Exception po_exception)
           {
               throw new Exception("Ocurrió un error al obtener los permisos de una página.", po_exception);
           }

           return loPagina;
       }

    }
}
