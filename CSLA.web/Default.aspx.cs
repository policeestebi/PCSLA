using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;
using COSEVI.lib.Security;
using COSEVI.CSLA.lib.accesoDatos.App_Constantes;

namespace CSLA.web
{
    public partial class Default : System.Web.UI.Page
    {
        #region Inicializacion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                //Si esta en esta página es que se salido
                //de la aplicación por lo que se ocultan
                //los controles de logeo
                // y la sesión se termina.
                this.TerminarSesion();
                
            }

        }
        #endregion

        #region Metodos

        /// <summary>
        /// Elimina la información de la sesión actual.
        /// </summary>
        public void TerminarSesion() 
        {
            try
            {
                if (this.Session["cls_usuario"] != null) 
                {
                    //Se agrega la información del logeo en la bitácora del sistema.
                    cls_interface.insertarTransacccionBitacora(cls_constantes.LOGOFF, cls_constantes.LOGOFF, ((COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes.cls_interface)this.Session[CSLA.web.App_Constantes.cls_constantes.INTERFACES]).vs_usuarioActual);
                }

                this.Session.Clear();
                this.Session["cls_usuario"] = null;

                (this.Master.FindControl("hpl_logOut") as HyperLink).Visible = false;
                (this.Master.FindControl("hpl_usuario") as HyperLink).Visible = false;
                (this.Master.FindControl("lbl_separador") as Label).Visible = false;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Valida que el usuario exista en la base de datos 
        /// y que los datos correspondan.
        /// </summary>
        /// <returns></returns>
        private bool ValidarUsuario()
        {
            bool resultado = true;

            string vs_pass = String.Empty;

            cls_usuario vo_usuario = null;

            COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes.cls_interface interfaces = null;

            CSLA.web.App_Variables.cls_variablesSistema variables = null;

            try
            {
                this.lbl_usuarioInvalido.Visible = false;
                this.lbl_usuarioInactivo.Visible = false;

                vs_pass = cls_MD5.GetPassword(this.txt_usuario.Text, this.txt_Constrasena.Text);

                vo_usuario = vo_usuario = cls_gestorUsuario.Login(this.txt_usuario.Text, vs_pass);

                if (vo_usuario == null)
                {
                    this.lbl_usuarioInvalido.Visible = true;
                }
                else
                {
                    if (vo_usuario.pActivo)
                    {
                        interfaces = new cls_interface();
                        variables = new App_Variables.cls_variablesSistema();

                        //Se almacenan en sesion las variables que utilizará el usuario
                        //en los diferentes mantenimientos.
                        this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES] = variables;
                        this.Session[CSLA.web.App_Constantes.cls_constantes.INTERFACES] = interfaces;

                        //Se inicia la sesion del usuario.
                        this.Session["cls_usuario"] = vo_usuario;
                        ((COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes.cls_interface)this.Session[CSLA.web.App_Constantes.cls_constantes.INTERFACES]).vs_usuarioActual = vo_usuario.pPK_usuario;
                        ((COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes.cls_interface)this.Session[CSLA.web.App_Constantes.cls_constantes.INTERFACES]).vs_direccionIP = Request.UserHostAddress;
                        ((COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes.cls_interface)this.Session[CSLA.web.App_Constantes.cls_constantes.INTERFACES]).vs_nombreHost = Dns.GetHostName();

                        //Se agrega la información del logeo en la bitácora del sistema.
                        cls_interface.insertarTransacccionBitacora(cls_constantes.LOGIN, cls_constantes.LOGIN, vo_usuario.pPK_usuario);

                        //Se envia a la página de principal.
                        Response.Redirect("frw_principal.aspx");
                    }
                    else
                    {
                        this.lbl_usuarioInactivo.Visible = true;
                    }
                }

            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta cuando se le da click al botón de logearse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_acceder_Click(object sender, EventArgs e)
        {
            this.ValidarUsuario();
        }

        #endregion
  
    }
}