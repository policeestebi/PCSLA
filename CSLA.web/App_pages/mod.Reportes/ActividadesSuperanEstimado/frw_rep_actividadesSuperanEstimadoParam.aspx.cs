using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using ExceptionManagement.Exceptions;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.accesoDatos.mod.Reportes;

using CSLA.web.App_Variables;
using CSLA.web.App_Constantes;

namespace CSLA.web.App_pages.mod.Reportes.ActividadesSuperanEstimado
{
    public partial class frw_rep_actividadesSuperanEstimadoParam : System.Web.UI.Page
    {
        #region Inicialización

        protected void Page_Load(object sender, EventArgs e)
        {
            this.validarSession();

            if (!Page.IsPostBack && this.Session["cls_usuario"] != null)
            {
                try
                {
                    this.obtenerPermisos();
                    this.validarAcceso();
                    this.llenarComboProyectos();
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar el reporte de registro de tiempos.";
                    this.lanzarExcepcion(po_exception, vs_error_usuario);
                }

            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Método que se encarga
        /// de llanar la información
        /// del combo de proyectos.
        /// </summary>
        private void llenarComboProyectos()
        {
            try
            {
                this.ddl_Proyectos.DataSource = cls_gestorProyecto.listarProyectos();
                this.ddl_Proyectos.DataBind();

                this.ddl_Proyectos.Items.Insert(0, new ListItem("Seleccione..", "-1"));

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando el combo de proyectos.", po_exception);
            }
        }

        /// <summary>
        /// Método que se encarga
        /// de llamar al reporte
        /// </summary>
        private void imprimirReporte()
        {
            string vs_proyecto = this.ddl_Proyectos.SelectedValue;

            Response.Redirect(this.contruirURl(vs_proyecto));

        }

        /// <summary>
        /// Método que construye la url
        /// con los parámetros que serán 
        /// enviados al reporte.
        /// </summary>
        /// <param name="ps_proyecto"></param>
        private string contruirURl(string ps_proyecto)
        {
            //Parámetros del reporte FechaInicio FechaFinal
            StringBuilder vo_url = new StringBuilder();

            vo_url.AppendFormat("frw_rep_actividadesSuperanEstimado.aspx?{0}={1}",
                                "proyecto",
                                ps_proyecto
                              );

            return vo_url.ToString();
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta cuando se imprime un reporte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                this.imprimirReporte();
            }
            catch (Exception po_exception)
            {
                this.lanzarExcepcion(po_exception, "Error al imprimir el reporte.");
            }
        }

        #endregion

        #region Atributos

        #endregion

        #region Excepciones

        /// <summary>
        /// Método que lanza la excepción personalizada
        /// </summary>
        /// <param name="po_exception">Excepción a levantar</param>
        /// <param name="ps_mensajeUsuario">Mensaje a comunicar al usuario</param>
        private void lanzarExcepcion(Exception po_exception, String ps_mensajeUsuario)
        {
            try
            {
                String vs_error_usuario = ps_mensajeUsuario;
                vs_error_usuario = vs_error_usuario.Replace(" ", "_");
                vs_error_usuario = vs_error_usuario.Replace("'", "|");

                String vs_error_tecnico = po_exception.Message;
                vs_error_tecnico = vs_error_tecnico.Replace(" ", "_");
                vs_error_tecnico = vs_error_tecnico.Replace("'", "|");

                String vs_script = "window.showModalDialog(\"../../../frw_error.aspx?vs_error_usuario=" + vs_error_usuario + "&vs_error_tecnico=" + vs_error_tecnico + "\",\"Ventana\",\"dialogHeight:450px;dialogWidth:625px;center:yes;status:no;menubar:no;resizable:no;scrollbars:yes;toolbar:no;location:no;directories:no\");";
                ScriptManager.RegisterClientScriptBlock(this.upd_Principal, this.upd_Principal.GetType(), "jsKeyScript", vs_script, true);

                throw new GeneralException("GeneralException", po_exception);
            }
            catch (GeneralException po_general_exception)
            {
                ExceptionManagement.ExceptionManager.Publish(po_general_exception);
            }
        }


        #endregion

        #region Seguridad

        /// <summary>
        /// Valida si el usuario
        /// tiene acceso a la página de lo contrario
        /// destruye la sessión
        /// 
        /// </summary>
        private void validarAcceso()
        {
            if (!this.pbAcceso)
            {
                this.Session.Abandon();
                this.Session.Clear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", cls_constantes.SCRIPTLOGOUT, true);
            }
        }

        /// <summary>
        /// Determina si la sesión se encuentra
        /// activa, si no es así se envía a la página de inicio.
        /// </summary>
        private void validarSession()
        {
            if (this.Session["cls_usuario"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", cls_constantes.SCRIPTLOGOUT, true);
            }
        }

        /// <summary>
        /// Valida el acceso del usuario en la página
        /// </summary>
        private bool pbAcceso
        {
            get
            {
                if (Session[cls_constantes.PAGINA] != null)
                {
                    return (Session[cls_constantes.PAGINA] as cls_pagina)[cls_constantes.ACCESO] != null;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Obtiene los permisos
        /// para la página actual.
        /// </summary>
        private void obtenerPermisos()
        {
            string lsUrl = String.Empty;

            try
            {
                lsUrl = "#" + cls_util.ObtenerDireccion(HttpContext.Current.Request.Url.AbsolutePath.Remove(0, 1));

                if (this.Session["cls_usuario"] != null)
                {
                    Session[cls_constantes.PAGINA] = cls_gestorPagina.obtenerPermisoPaginaRol(lsUrl, ((cls_usuario)this.Session["cls_usuario"]).pFK_rol);
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener los permisos del rol en la página actual.", po_exception);
            }
        }


        #endregion

    }
}