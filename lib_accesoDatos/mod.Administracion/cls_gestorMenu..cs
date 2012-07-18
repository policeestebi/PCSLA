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
// cls_gestorMenu.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// GENERADOR			 	16    05    2011    Se crea la clase
// Cristian Arce            22    05    2011    Se modifica los gestores producidos por el generador
// Cristian Arce Jiménez  	27    11    2011	Se agrega el manejo de excepciones personalizadas
// Esteban Ramírez          10    01    2012    Se agrega la inserción en la bitácora.
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.accesoDatos.mod.Administracion
{
    public class cls_gestorMenu
    {
        /// <summary>
        /// Método que permite insertar 
        /// un nuevo registro en la tabla menu
        /// </summary>
        /// <param name="poMenu">Menu a insertar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int insertMenu(cls_menu poMenu)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_menuInsert";

                cls_sqlDatabase.beginTransaction();

                if (poMenu.pMenuPadre == 0)
                {
                    cls_parameter[] vu_parametros = 
                    {
                        new cls_parameter("@paramPK_menu", poMenu.pPK_menu),
                        new cls_parameter("@paramFK_menuPadre",  DBNull.Value),
                        new cls_parameter("@paramimagen", poMenu.pImagen),
                        new cls_parameter("@paramtitulo", poMenu.pTitulo),
                        new cls_parameter("@paramdescripcion", poMenu.pDescripcion)
                    };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }
                else
                {
                    cls_parameter[] vu_parametros = 
                    {
                        new cls_parameter("@paramPK_menu", poMenu.pPK_menu),
                        new cls_parameter("@paramFK_menuPadre",  poMenu.pMenuPadre),
                        new cls_parameter("@paramimagen", poMenu.pImagen),
                        new cls_parameter("@paramtitulo", poMenu.pTitulo),
                        new cls_parameter("@paramdescripcion", poMenu.pDescripcion)
                    };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                }

                poMenu.pPK_menu = Convert.ToInt32(cls_gestorUtil.selectMax(cls_constantes.MENU, "PK_menu")); 

                cls_interface.insertarTransacccionBitacora(cls_constantes.INSERTAR, cls_constantes.MENU, poMenu.pPK_menu.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al insertar el menú.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite actualizar 
        /// un registro en la tabla menu
        /// </summary>
        /// <param name="poMenu">Menu a actualizar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int updateMenu(cls_menu poMenu)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_menuUpdate";

                cls_sqlDatabase.beginTransaction();

                if (poMenu.pMenuPadre == 0)
                {
                    cls_parameter[] vu_parametros = 
                    {
                        new cls_parameter("@paramPK_menu", poMenu.pPK_menu),
                        new cls_parameter("@paramFK_menuPadre", DBNull.Value),
                        new cls_parameter("@paramimagen", poMenu.pImagen),
                        new cls_parameter("@paramtitulo", poMenu.pTitulo),
                        new cls_parameter("@paramdescripcion", poMenu.pDescripcion)
                    };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }
                else
                {
                    cls_parameter[] vu_parametros = 
                    {
                        new cls_parameter("@paramPK_menu", poMenu.pPK_menu),
                        new cls_parameter("@paramFK_menuPadre", poMenu.pMenuPadre),
                        new cls_parameter("@paramimagen", poMenu.pImagen),
                        new cls_parameter("@paramtitulo", poMenu.pTitulo),
                        new cls_parameter("@paramdescripcion", poMenu.pDescripcion)
                    };

                    vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);
                }

                cls_interface.insertarTransacccionBitacora(cls_constantes.MODIFICAR, cls_constantes.MENU, poMenu.pPK_menu.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al modificar el menú.", po_exception);
            }

        }

        /// <summary>
        /// Método que permite eliminar 
        /// un registro en la tabla menu
        /// </summary>
        /// <param name="poMenu">Menu a eliminar</param>
        /// <returns>Int valor del resultado de la ejecución de la sentencia</returns>
        public static int deleteMenu(cls_menu poMenu)
        {
            int vi_resultado;

            try
            {
                String vs_comando = "PA_admi_menuDelete";

                cls_parameter[] vu_parametros = 
                {
                 		new cls_parameter("@paramPK_menu", poMenu.pPK_menu)  
                };

                cls_sqlDatabase.beginTransaction();

                vi_resultado = cls_sqlDatabase.executeNonQuery(vs_comando, true, vu_parametros);

                cls_interface.insertarTransacccionBitacora(cls_constantes.ELIMINAR, cls_constantes.MENU, poMenu.pPK_menu.ToString());

                cls_sqlDatabase.commitTransaction();

                return vi_resultado;

            }
            catch (Exception po_exception)
            {
                cls_sqlDatabase.rollbackTransaction();
                throw new Exception("Ocurrió un error al eliminar el menú.", po_exception);
            }

        }


        /// <summary>
        /// Método que permite listar 
        /// todos los registros en la tabla menu
        /// </summary>
        /// <returns> List<cls_menu>  valor del resultado de la ejecución de la sentencia</returns>
        public static List<cls_menu> listarMenu()
        {
            List<cls_menu> vo_lista = null;
            cls_menu poMenu = null;
            int vi_fkMenu;

            try
            {
                String vs_comando = "PA_admi_menuSelect";
                cls_parameter[] vu_parametros = { };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                vo_lista = new List<cls_menu>();
                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    poMenu = new cls_menu();

                    poMenu.pPK_menu = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_menu"]);

                    if (Int32.TryParse(vu_dataSet.Tables[0].Rows[i]["FK_menuPadre"].ToString(), out vi_fkMenu))
                    {
                        poMenu.pFK_menu = vi_fkMenu;
                    }

                    poMenu.pImagen = "";

                    poMenu.pTitulo = vu_dataSet.Tables[0].Rows[i]["titulo"].ToString();

                    poMenu.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                    vo_lista.Add(poMenu);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los menús.", po_exception);
            }
        }

        /// <summary>
        /// Método que permite seleccionar  
        /// un único registro en la tabla menu
        /// </summary>
        /// <returns>poMenu valor del resultado de la ejecución de la sentencia</returns>
        public static cls_menu seleccionarMenu(cls_menu poMenu)
        {
            int vi_fkPadre;

            try
            {
                String vs_comando = "PA_admi_menuSelectOne";
                cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramPK_menu", poMenu.pPK_menu) 
                                               };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                poMenu = new cls_menu();

                poMenu.pPK_menu = Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["PK_menu"]);

                if (Int32.TryParse(vu_dataSet.Tables[0].Rows[0]["FK_menuPadre"].ToString(), out vi_fkPadre))
                {
                    poMenu.pFK_menu = vi_fkPadre;
                }

                //Convert.ToInt32(vu_dataSet.Tables[0].Rows[0]["FK_menuPadre"]);

                poMenu.pImagen = vu_dataSet.Tables[0].Rows[0]["imagen"].ToString();

                poMenu.pTitulo = vu_dataSet.Tables[0].Rows[0]["titulo"].ToString();

                poMenu.pDescripcion = vu_dataSet.Tables[0].Rows[0]["descripcion"].ToString();

                return poMenu;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el menú específico.", po_exception);
            }
        }


        /// <summary>
        /// Devuelve un dataset con la lista
        /// de las opciones de menu del sistema
        /// </summary>
        /// <returns>DataSet con los datos</returns>
        public static DataSet seleccionarPaginaMenu()
        {
            try
            {
                String vs_comando = "PA_admi_paginaMenuSelect";
                cls_parameter[] vu_parametros = { 
                                               };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                return vu_dataSet;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al seleccionar la página del menú.", po_exception);
            }
        }

        /// <summary>
        /// Devuelve un dataset con la lista
        /// de las opciones de menu del sistema según rol
        /// </summary>
        /// <returns>DataSet con los datos</returns>
        public static DataTable seleccionarPaginaMenuRol(int piRol)
        {
            try
            {
                String vs_comando = "PA_admi_paginaMenuSelectRol";
                cls_parameter[] vu_parametros = { 
                                                   new cls_parameter("@paramrol", piRol)  
                                               };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                return BuildMenu(vu_dataSet.Tables[0]);


            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al seleccionar el menú de un usuario.", po_exception);
            }
        }

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="po_row"></param>
        /// <param name="po_data"></param>
        /// <returns></returns>
        private static void AddRow(DataRow po_row, DataTable po_data, ref bool vb_add)
        {

            EnumerableRowCollection vo_query = null;
            int vi_menu = Convert.ToInt32(po_row["menu"].ToString());

            if (!TieneHijos(Convert.ToInt32(po_row["menu"].ToString()), po_data))
            {
                if (Convert.ToInt32(po_row["permiso"].ToString()) != -1)
                    vb_add = vb_add || true;
                else
                    vb_add = vb_add || false;
            }
            else
            {
                //Se obtienen los hijos y se recorre
                vo_query = from vrow in po_data.AsEnumerable()
                           where vrow.Field<int>("padre") == vi_menu
                           select vrow;

                foreach (DataRow v_row in vo_query)
                {
                    AddRow(v_row, po_data, ref vb_add);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="po_data"></param>
        /// <returns></returns>
        private static DataTable BuildMenu(DataTable po_data)
        {
            DataTable vo_data;
            int vi_menu = 0;
            bool vb_agregarFila = false;
            bool vb_add = false;
            try
            {
                vo_data = po_data.Clone();

                foreach (DataRow vo_row in po_data.Rows)
                {
                    vb_add = false;

                    if (vi_menu != Convert.ToInt32(vo_row["menu"]))
                    {
                        vb_agregarFila = false;
                    }

                    AddRow(vo_row, po_data,ref vb_add);

                    if (!vb_agregarFila && vb_add)
                    {
                        vb_agregarFila = true;
                        vo_data.ImportRow(vo_row);
                    }

                    vi_menu = Convert.ToInt32(vo_row["menu"]);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return vo_data;
        }

        /// <summary>
        /// Verifica si un opción de menú tiene
        /// hijos dentro de un set de datos.
        /// </summary>
        /// <param name="psPadre">Código de la opción Padre</param>
        /// <param name="poData">Coleccion de datos</param>
        /// <returns></returns>
        private static bool TieneHijos(int psPadre, DataTable poData)
        {

            bool tieneHijos = false;
            int count;
            try
            {
                //Se busca si la opción tiene como padre
                count = (from row in poData.AsEnumerable()
                         where row.Field<int>("padre") == psPadre
                         select row).Count();

                if (count > 0)
                {
                    tieneHijos = true;
                }
            }
            catch (Exception)
            {

            }

            return tieneHijos;
        }

        /// <summary>
        /// Determina si la opción sólo
        /// tiene un privilegio, por lo que sólo
        /// sale una única vez en todo el set
        /// de datos por lo cuál para determinar
        /// si debe ser incluido se debe tomar sólo el
        /// valor actual de su privilegio.
        /// </summary>
        /// <param name="ps_menu">Código del menu.</param>
        /// <param name="poData">Datos.</param>
        /// <returns>True|False</returns>
        private static bool OpcionUnica(int ps_menu, DataTable poData)
        {
            bool vb_unica = false;
            int vi_count = 0;

            try
            {
                vi_count = (from vrow in poData.AsEnumerable()
                            where vrow.Field<int>("menu") == ps_menu
                            select vrow).Count();
                vb_unica = vi_count == 1;
            }
            catch (Exception)
            {
            }

            return vb_unica;
        }

        /// <summary>
        /// Devuelve el menú completo del 
        /// sistema.
        /// </summary>
        /// <returns>DataSet con los datos</returns>
        public static DataSet seleccionarMenuSistema()
        {
            try
            {
                String vs_comando = "PA_admi_menuSistemaSelect";
                cls_parameter[] vu_parametros = { 
                                               };

                DataSet vu_dataSet = cls_sqlDatabase.executeDataset(vs_comando, true, vu_parametros);

                return vu_dataSet;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el menú del sistema.", po_exception);
            }
        }

        /// <summary>
        /// Hace un lista de menú con un filtrado específico.
        /// </summary>
        /// <param name="psFiltro">String filtro.</param>
        /// <returns></returns>
        public static List<cls_menu> listarMenuFiltro(string psFiltro)
        {
            List<cls_menu> vo_lista = null;
            cls_menu voMenu = null;
            try
            {
                DataSet vu_dataSet = cls_gestorUtil.selectFilter(cls_constantes.MENU, string.Empty, psFiltro);

                vo_lista = new List<cls_menu>();

                for (int i = 0; i < vu_dataSet.Tables[0].Rows.Count; i++)
                {
                    voMenu = new cls_menu();

                    voMenu.pPK_menu = Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["PK_menu"]);

                    voMenu.pFK_menu = Convert.IsDBNull(vu_dataSet.Tables[0].Rows[i]["FK_menuPadre"]) ? 1 : Convert.ToInt32(vu_dataSet.Tables[0].Rows[i]["FK_menuPadre"]);

                    voMenu.pTitulo = vu_dataSet.Tables[0].Rows[i]["titulo"].ToString();

                    voMenu.pDescripcion = vu_dataSet.Tables[0].Rows[i]["descripcion"].ToString();

                    vo_lista.Add(voMenu);
                }

                return vo_lista;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener el listado de los menús de manera filtrada.", po_exception);
            }
        }
    }
}
