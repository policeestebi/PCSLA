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
// cls_gestorRol.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Esteban Ramírez          12    10    2011    Se modifica el método de insert.
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Esteban Ramírez          10    01    2012    Se agrega la inserción en la bitácora.
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
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
            cls_rolPaginaPermiso vo_rolPaginaPermiso = null;

            try
            {
                String vs_comando = "PA_admi_RolInsert";
                cls_parameter[] vu_parametros = 
                {
                    new cls_parameter("@paramdescripcion", poRol.pDescripcion),
                    new cls_parameter("@paramnombre", poRol.pNombre),
                    new cls_parameter("@paramvisible", poRol.pVisible)
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                poRol.pPK_rol = Convert.ToInt16(cls_gestorUtil.selectMax("t_admi_rol", "PK_rol"));

                if (poRol.Paginas != null)
                {
                    foreach (cls_pagina pagina in poRol.Paginas)
                    {
                        vo_rolPaginaPermiso = new cls_rolPaginaPermiso();

                        vo_rolPaginaPermiso.pPagina = pagina;
                        vo_rolPaginaPermiso.pRol = poRol;

                        foreach (cls_permiso permiso in pagina.Permisos)
                        {
                            vo_rolPaginaPermiso.pPermiso = permiso;

                            cls_gestorRolPaginaPermiso.insertRolPaginaPermiso(vo_rolPaginaPermiso);
                        }
                    }
                }

                // Se obtiene el número del registro insertado.
                poRol.pPK_rol = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.ROL, "PK_rol"));

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ROL, poRol.pPK_rol.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el rol.", po_exception);
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
            cls_rolPaginaPermiso vo_rolPaginaPermiso = null;

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


                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                //Se elimina la asociacion de los permisos

                cls_gestorRolPaginaPermiso.deleteRolPaginaPermisoAll(poRol);

                //Se graban los nuevos permsos
                if (poRol.Paginas != null)
                {
                    foreach (cls_pagina pagina in poRol.Paginas)
                    {
                        vo_rolPaginaPermiso = new cls_rolPaginaPermiso();

                        vo_rolPaginaPermiso.pPagina = pagina;
                        vo_rolPaginaPermiso.pRol = poRol;

                        foreach (cls_permiso permiso in pagina.Permisos)
                        {
                            vo_rolPaginaPermiso.pPermiso = permiso;

                            cls_gestorRolPaginaPermiso.insertRolPaginaPermiso(vo_rolPaginaPermiso);
                        }
                    }
                }

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.ROL, poRol.pPK_rol.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar el rol.", po_exception);
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

                //Se elimina la relación con los permisos.
                cls_gestorRolPaginaPermiso.deleteRolPaginaPermisoAll(poRol);

                String vs_comando = "PA_admi_RolDelete";
                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramPK_rol", poRol.pPK_rol)  
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.ROL, poRol.pPK_rol.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el rol.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite listar 
        /// todos los registros en la tabla rol
        /// </summary>
        /// <returns> List<cls_rol>  valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_rol> listarRoles()
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
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de roles.",po_exception);
            }
        }

        /// <summary>
        /// Método que permite seleccionar  
        /// un único registro en la tabla rol
        /// </summary>
        /// <returns>poRol valor del resultado de la ejecución de la sentencia</returns>
        public static cls_rol seleccionarRol(cls_rol poRol)
        {
            DataTable vo_permisos = null;
            cls_pagina vo_pagina = null;
            cls_permiso vo_permiso = null;

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


                //Se cargan los permisos de un rol.
                vo_permisos = cls_gestorRolPaginaPermiso.listarPermisosUsuario(poRol);

                if (vo_permisos != null && vo_permisos.Rows.Count > 0)
                {
                    //Se seleccionan los las diferentes páginas
                    var paginas = (from pagina in vo_permisos.AsEnumerable()
                                   select new
                                   {
                                       PAGINA = pagina.Field<Int32>("PK_pagina")
                                   }).Distinct();

                    foreach (var pagina in paginas)
                    {

                        // Se crea la pagina
                        vo_pagina = new cls_pagina();
                        vo_pagina.pPK_pagina = pagina.PAGINA;

                        //Se obtienen los permisos
                        var permisos = from permiso in vo_permisos.AsEnumerable()
                                       where permiso.Field<Int32>("PK_pagina") == pagina.PAGINA
                                       select new
                                       {
                                           PERMISO = permiso.Field<Int32>("PK_permiso")
                                       };

                        foreach(var permiso in permisos)
                        {
                            vo_permiso = new cls_permiso();
                            vo_permiso.pPK_permiso = permiso.PERMISO;

                            vo_pagina.Permisos.Add(vo_permiso);
                        }

                        poRol.Paginas.Add(vo_pagina);

                    }

                }

                return poRol;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el rol especificado.", po_exception);
            }
        }

        /// <summary>
        /// Hace un lista de roles con un filtrado específico.
        /// </summary>
        /// <param name="psFiltro">String filtro.</param>
        /// <returns></returns>
        public static List<cls_rol> listarRolFiltro(string psFiltro)
        {
            List<cls_rol> vo_lista = null;
            cls_rol voRol = null;
            try
            {
                DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.ROL, string.Empty, psFiltro);

                vo_lista = new List<cls_rol>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    voRol = new cls_rol();

                    voRol.pPK_rol = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_rol"]);

                    voRol.pNombre = vu_dataSet.Tables[0].Rows[i]["nombre"].ToString();

                    voRol.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                    vo_lista.Add(voRol);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los roles de manera filtrada.", po_exception);
            }
        }

      

    }
}
