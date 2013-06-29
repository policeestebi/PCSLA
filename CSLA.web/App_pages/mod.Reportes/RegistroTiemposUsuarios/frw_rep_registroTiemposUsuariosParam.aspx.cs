using System;
using System.Text;
using System.Collections.Generic;
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

namespace CSLA.web.App_pages.mod.Reportes.RegistroTiemposUsuarios
{
    public partial class frw_rep_registroTiemposUsuariosParam : System.Web.UI.Page
    {
      

        #region Inicializacion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.validarSession();

            if (!Page.IsPostBack && this.Session["cls_usuario"] != null)
            {

                try
                {
                    this.obtenerPermisos();
                    this.validarAcceso();

                    this.llenarComboDeparamento();
                    this.llenarComboUsuario();

                    this.ddl_departamentos.SelectedValue = ((cls_usuario)Session["cls_usuario"]).pFK_departamento.ToString();
                    this.ddl_usuarios.SelectedValue = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                    this.ddl_departamentos.Enabled = this.pbTodosUsuarios;

                    this.txt_fechaInicio.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    this.txt_fechaFinal.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar el reporte de registro de tiempos usuarios.";
                    this.lanzarExcepcion(po_exception, vs_error_usuario);
                }

            }
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

        /// <summary>
        /// Evento que se ejecuta cuando se cambia de índice
        /// de departamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_departamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.llenarComboUsuario();
        }

        /// <summary>
        /// Evento que valida si la fecha inicial es mayor
        /// o igual que la fecha final
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cmv_fechas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //DateTime vd_fechaInicial = Convert.ToDateTime(this.txt_fechaInicio.Text);
            //DateTime vd_fechaFinal = Convert.ToDateTime(this.txt_fechaFinal.Text);

            //args.IsValid = vd_fechaInicial <= vd_fechaFinal;
        }


        #endregion

        #region Metodos

        /// <summary>
        /// Llena el combo de usuarios.
        /// </summary>
        public void llenarComboUsuario()
        {
            try
            {
                string vs_filtro = " FK_departamento= " + this.ddl_departamentos.SelectedValue ;
                this.ddl_usuarios.DataSource = cls_gestorUsuario.listarUsuarioFiltro(vs_filtro);
                this.ddl_usuarios.DataBind();

                this.ddl_usuarios.Items.Insert(0, new ListItem("<Seleccione>", ""));
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar los filtros de usuarios.", po_exception);
            }
        }

        /// <summary>
        /// Llena el combo de departamentos
        /// </summary>
        public void llenarComboDeparamento()
        {
            try
            {
                this.ddl_departamentos.DataSource = cls_gestorDepartamento.listarDepartamento();
                this.ddl_departamentos.DataBind();

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar los filtros de departamentos.", po_exception);
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
            string vs_usuario = this.ddl_usuarios.SelectedValue;

            Response.Redirect(this.contruirURl(vd_fechaInicio, vd_fechaFinal, vs_usuario));

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
                                    string ps_usuario)
        {
            
            StringBuilder vo_url = new StringBuilder();

            vo_url.AppendFormat("frw_rep_registroTiemposUsuarios.aspx?{0}={1}&{2}={3}&{4}={5}",
                                "fechaInicio",
                                po_fechaInicial.ToString("dd/MM/yyyy HH:mm:ss"),
                                "fechaFinal",
                                po_fechaFinal.ToString("dd/MM/yyyy HH:mm:ss"),
                                "usr",
                                ps_usuario
                                );

            return vo_url.ToString();
        }

        #endregion

        #region Atributos

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
        /// Valida el acceso del usuario en la página
        /// </summary>
        private bool pbTodosUsuarios
        {
            get
            {
                if (Session[cls_constantes.PAGINA] != null)
                {
                    return (Session[cls_constantes.PAGINA] as cls_pagina)[cls_constantes.TODOS_USUARIOS] != null;
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

       

    }
}