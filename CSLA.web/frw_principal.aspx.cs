using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;

namespace CSLA.web
{
    public partial class frw_principal : System.Web.UI.Page
    {


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.CargarMenu();
            this.CargarRegistrosPermanentes();

            //COSEVI.web.controls.option opcion = new COSEVI.web.controls.option();
            //opcion.Text = "Control y Seguimiento";
            //opcion.CssClass = "parent";
            //opcion.Url = "#";
            //this.menu.addOption(opcion);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //COSEVI.web.controls.option opcion = new COSEVI.web.controls.option();
            //opcion.Text = "Control y Seguimiento";
            //opcion.CssClass = "parent";
            //opcion.Url = "#";
            //this.menu.addOption(opcion);
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            //COSEVI.web.controls.option opcion = new COSEVI.web.controls.option();
            //opcion.Text = "Control y Seguimiento";
            //opcion.CssClass = "parent";
            //opcion.Url = "#";
            //this.menu.addOption(opcion);
        }

        #region Inicializacion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                this.HabilitarControlesLogeo();
            }

        }

        #endregion

        #region Metodos

        public void HabilitarControlesLogeo()
        {
            try 
            {
                (this.Master.FindControl("hpl_logOut") as HyperLink).Visible = true;
                (this.Master.FindControl("hpl_usuario") as HyperLink).Visible = true;
                (this.Master.FindControl("lbl_separador") as Label).Visible = true;

                //Si la sesión no esta nula se coloca la información del usuario
                if (this.Session["cls_usuario"] != null) 
                {
                    (this.Master.FindControl("hpl_usuario") as HyperLink).Text = (this.Session["cls_usuario"] as cls_usuario).pNombre;
                }

            }
            catch(Exception e)
            { 
                throw e;
            }
        }

        /// <summary>
        /// Carga el menú de la aplicacion.
        /// </summary>
        private void CargarMenu()
        {
            DataTable vo_menu = null;
            EnumerableRowCollection vo_query = null;
            COSEVI.web.controls.option vo_opcion;
            try
            {
                vo_menu = cls_gestorMenu.seleccionarPaginaMenuRol((this.Session["cls_usuario"] as cls_usuario).pFK_rol);//seleccionarPaginaMenu().Tables[0];

                if (vo_menu != null && vo_menu.Rows.Count > 0)
                {
                    //Se seleccionan las primeras opciones.
                    vo_query = from row in vo_menu.AsEnumerable()
                               where row.Field<int>("padre") == -1
                               orderby row.Field<int>("menu") ascending
                               select row;

                    //Por cada padre se agrega al menu una opcion
                    foreach (DataRow row in vo_query)
                    {
                        vo_opcion = this.agregarOpcion(row["titulo"].ToString(), "parent", row["url"].ToString(), row["height"].ToString(), null);

                        this.agregarOpcionesHijas(Convert.ToInt32(row["menu"]), vo_menu, vo_opcion);
                        
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Carga los registros que no se pueden eliminar de la aplicación.
        /// </summary>
        private void CargarRegistrosPermanentes()
        {
            try
            {
                cls_interface.cargarRegistrosPermanentes();
            }
            catch (Exception)
            {
            }
        }


        private void agregarOpcionesHijas(int pi_padre, 
                                          DataTable po_menu, 
                                          COSEVI.web.controls.option po_opcion)
        {
            try
            {
                EnumerableRowCollection vo_query = null;
                COSEVI.web.controls.option vo_opcion = null;

                if (po_menu != null && po_menu.Rows.Count > 0)
                {
                    //Se seleccionan las primeras opciones.
                    vo_query = from row in po_menu.AsEnumerable()
                               where row.Field<int>("padre") == pi_padre
                               orderby row.Field<int>("menu") ascending
                               select row;

                    //Por cada padre se agrega al menu una opcion
                    foreach (DataRow row in vo_query)
                    {
                        vo_opcion = this.agregarOpcion(row["titulo"].ToString(), String.Empty, row["url"].ToString(), row["height"].ToString(), po_opcion);

                        this.agregarOpcionesHijas(Convert.ToInt32(row["menu"]), po_menu, vo_opcion);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Agrega una nueva opcion de menu en el Menu principal de la aplicacion
        /// </summary>
        private COSEVI.web.controls.option agregarOpcion(string ps_titulo,
                                   string ps_cssClass,
                                   string ps_url,
                                   string height,
                                   COSEVI.web.controls.option po_padre)
        {
            COSEVI.web.controls.option vo_opcion = new COSEVI.web.controls.option();
            vo_opcion.Text = ps_titulo;
            vo_opcion.Url = ps_url;
            vo_opcion.CssClass = ps_cssClass;
            vo_opcion.Padre = po_padre;
            vo_opcion.Height = height;

            if (po_padre != null)
            {
                po_padre.addOption(vo_opcion);
            }
            else
            {
                this.menu.addOption(vo_opcion);
            }

            return vo_opcion;
            
        }

        #endregion

        #region Eventos

        #endregion

        #region Propiedades

        #endregion

        #region Atributos

        #endregion

    }
}
