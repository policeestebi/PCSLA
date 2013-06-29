using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using ExceptionManagement.Exceptions;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;

using CSLA.web.App_Variables;
using CSLA.web.App_Constantes;
using System.Data;

using COSEVI.lib.Security;

// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_edicionUsuario .aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			            MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	 07 –  06 - 2012	 	Se crea la clase
//								
//
// =========================================================================
namespace CSLA.web.App_pages.mod.Administracion
{
    public partial class frw_edicionUsuario : System.Web.UI.Page
    {
        #region Inicializacion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.validarSession();

            if (!Page.IsPostBack && this.Session["cls_usuario"] != null)
            {
                //Se carga la información del usuario.
                this.cargarUsuario();
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Método que se encarga
        /// de cargar la información
        /// del usaurio.
        /// </summary>
        private void cargarUsuario()
        {
            cls_usuario vo_usuario = null;
            try
            {
                vo_usuario = ((cls_usuario)this.Session["cls_usuario"] );

                this.txt_usuario.Text = vo_usuario.pPK_usuario;
                this.txt_email.Text = vo_usuario.pEmail;
                this.txt_nombre.Text = vo_usuario.pNombre;
                this.txt_apellido1.Text = vo_usuario.pApellido1;
                this.txt_apellido2.Text = vo_usuario.pApellido2;
                this.txt_puesto.Text = vo_usuario.pPuesto;

                this.lbl_usuarioActual.Text = vo_usuario.pNombreCompleto;

            }
            catch (Exception po_exception)
            {
                this.lanzarExcepcion(po_exception, "Error al cargar la información del usuario");
            }
        }

        /// <summary>
        /// Método que se 
        /// encarga de guardar la información del
        /// usuario.
        /// </summary>
        private void guardarUsuario()
        {
            cls_usuario vo_usuario = null;
            try
            {
                vo_usuario = new cls_usuario();

                vo_usuario.pPK_usuario = this.txt_usuario.Text;

                //Se carga la información actual del usuario.
                vo_usuario = cls_gestorUsuario.seleccionarUsuario(vo_usuario);

                vo_usuario.pEmail = this.txt_email.Text;

                if (!String.IsNullOrEmpty(this.txt_contrasena.Text) &&
                   !String.IsNullOrEmpty(this.txt_confirmarContrasena.Text) &&
                   this.txt_contrasena.Text.Equals(this.txt_confirmarContrasena.Text))
                {
                    vo_usuario.pContrasena = cls_MD5.GetPassword( vo_usuario.pPK_usuario,this.txt_contrasena.Text);
                }

                vo_usuario.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                //Se actualiza la información del usuario
                cls_gestorUsuario.updateUsuario(vo_usuario);

                //Se actualiza la información del usuario
                //en la sesión.
                vo_usuario.pContrasena = string.Empty;

                this.Session["cls_usuario"] = vo_usuario;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", "MostrarMensaje();", true);
            }
            catch (Exception po_exception)
            {
                this.lanzarExcepcion(po_exception, "Error al guardar la información del usuario.");
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta cuando se 
        /// guarda un nuevo rol.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            this.guardarUsuario();
        }

        #endregion

        #region Atributos

        #endregion

        #region Seguridad

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

        #endregion

        #region Manejo de Excepciones

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

        #endregion

    }
}