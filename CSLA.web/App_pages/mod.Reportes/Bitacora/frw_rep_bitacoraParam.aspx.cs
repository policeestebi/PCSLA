using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using CONS = COSEVI.CSLA.lib.accesoDatos.App_Constantes;
using ExceptionManagement.Exceptions;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;


using CSLA.web.App_Variables;
using CSLA.web.App_Constantes;


// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_permiso.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	21 – 06  - 2011	 	Se crea la clase
//								…………………………………………………………
//								…………………………………………………………
// 
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.Reportes.Bitacora
{
    public partial class frw_rep_bitacoraParam : System.Web.UI.Page
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

                    this.llenarCombosUsuario();
                    this.llenarComboTabla();
                    this.llenarComboAccion();

                    this.txt_fechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    this.txt_fechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar la consulta de bitácora.";
                    this.lanzarExcepcion(po_exception, vs_error_usuario);
                }

            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Valida los datos de los filtros.
        /// </summary>
        /// <returns></returns>
        public bool validarDatos()
        {
            bool resultado = true;

            if (Convert.ToDateTime(this.txt_fechaInicio.Text) > Convert.ToDateTime(this.txt_fechaFinal.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje", "MostrarMensaje();", true);

                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// Método que filtra la bitácora
        /// </summary>
        private void filtrarBitacora()
        {
            if (this.validarDatos())
            {

                DateTime vd_fechaInicio = Convert.ToDateTime(this.txt_fechaInicio.Text);
                DateTime vd_fechaFinal = Convert.ToDateTime(this.txt_fechaFinal.Text);
                string usuarioDesde = this.ddl_desde.SelectedIndex == 0 ? "A" : this.ddl_desde.SelectedValue;
                string usuarioHasta = this.ddl_hasta.SelectedIndex == 0 ? "Z" : this.ddl_hasta.SelectedValue;
                string tabla = "%" + this.ddl_table.SelectedValue + "%";
                string accion = "%" + this.ddl_accion.SelectedValue + "%";
                string registro = "%" + this.txt_registro.Text + "%";
            }

        }

        /// <summary>
        /// Método que se encarga de
        /// llenar la información 
        /// del filtro de acciones.
        /// </summary>
        private void llenarComboAccion()
        {
            try
            {
                this.ddl_accion.Items.Clear();

                foreach (int value in Enum.GetValues(typeof(CONS.Accion)))
                {
                    this.ddl_accion.Items.Add(new ListItem(Enum.GetName(typeof(CONS.Accion), value), Enum.GetName(typeof(CONS.Accion), value)));
                }
                this.ddl_accion.Items.Insert(0, new ListItem("<Seleccione>", ""));
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar el filtro de acciones.", po_exception);
            }
        }

        /// <summary>
        /// Método que se encarga de
        /// llenar la información 
        /// del filtro de tablas.
        /// </summary>
        private void llenarComboTabla()
        {
            try
            {
                this.ddl_table.Items.Clear();

                foreach (int value in Enum.GetValues(typeof(CONS.Tablas)))
                {
                    this.ddl_table.Items.Add(new ListItem(Enum.GetName(typeof(CONS.Tablas), value), Enum.GetName(typeof(CONS.Tablas), value)));
                }
                this.ddl_table.Items.Insert(0, new ListItem("<Seleccione>", ""));

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar el filtro de tablas.", po_exception);
            }
        }

        /// <summary>
        /// Método que se encarga de 
        /// llenar la información
        /// del filtro de empleados.
        /// </summary>
        private void llenarCombosUsuario()
        {
            try
            {
                this.ddl_desde.DataSource = cls_gestorUsuario.listarUsuarios();
                this.ddl_desde.DataBind();
                this.ddl_hasta.DataSource = cls_gestorUsuario.listarUsuarios();
                this.ddl_hasta.DataBind();

                this.ddl_desde.Items.Insert(0, new ListItem("<Seleccione>", ""));
                this.ddl_hasta.Items.Insert(0, new ListItem("<Seleccione>", ""));

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar los filtros de usuarios.", po_exception);
            }
        }

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

                String vs_script = "window.showModalDialog(\"../../frw_error.aspx?vs_error_usuario=" + vs_error_usuario + "&vs_error_tecnico=" + vs_error_tecnico + "\",\"Ventana\",\"dialogHeight:450px;dialogWidth:625px;center:yes;status:no;menubar:no;resizable:no;scrollbars:yes;toolbar:no;location:no;directories:no\");";
                ScriptManager.RegisterClientScriptBlock(this.upd_Principal, this.upd_Principal.GetType(), "jsKeyScript", vs_script, true);

                throw new GeneralException("GeneralException", po_exception);
            }
            catch (GeneralException po_general_exception)
            {
                ExceptionManagement.ExceptionManager.Publish(po_general_exception);
            }
        }

        /// <summary>
        /// Método que se encarga
        /// de llamar al reporte
        /// </summary>
        private void imprimirReporte()
        {
            DateTime vd_fechaInicio = Convert.ToDateTime(this.txt_fechaInicio.Text);
            DateTime vd_fechaFinal = Convert.ToDateTime(this.txt_fechaFinal.Text);
            string usuarioDesde = this.ddl_desde.SelectedIndex == 0 ? "A" : this.ddl_desde.SelectedValue;
            string usuarioHasta = this.ddl_hasta.SelectedIndex == 0 ? "Z" : this.ddl_hasta.SelectedValue;
            string tabla = "%" + this.ddl_table.SelectedValue + "%";
            string accion = "%" + this.ddl_accion.SelectedValue + "%";
            string registro = "%" + this.txt_registro.Text + "%";

            Response.Redirect(this.contruirURl(vd_fechaInicio, vd_fechaFinal,usuarioDesde,usuarioHasta,tabla,accion,registro));

        }

        /// <summary>
        /// Método que construye la url
        /// con los parámetros que serán 
        /// enviados al reporte.
        /// </summary>
        /// <param name="po_fechaInicial">Fecha Incial</param>
        /// <param name="po_fechaFinal">Fecha Final</param>
        private string contruirURl(DateTime po_fechaInicial, 
                                   DateTime po_fechaFinal,
                                    string ps_usuarioDesde, 
                                    string ps_usuariosHasta, 
                                    string ps_tabla, 
                                    string ps_accion, 
                                    string ps_registro)
        {
            //Parámetros del reporte FechaInicio FechaFinal
            StringBuilder vo_url = new StringBuilder();

            vo_url.AppendFormat("frw_rep_bitacora.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}",
                                "fechaInicio",
                                po_fechaInicial.ToString("dd/MM/yyyy HH:mm:ss"),
                                "fechaFinal",
                                po_fechaFinal.ToString("dd/MM/yyyy HH:mm:ss"),
                                "usD",
                                ps_usuarioDesde,
                                "usH",
                                ps_usuariosHasta,
                                "tab",
                                ps_tabla,
                                "acc",
                                ps_accion,
                                "preg",
                                ps_registro
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