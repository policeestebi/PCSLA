using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using ExceptionManagement.Exceptions;
using COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;

using CSLA.web.App_Variables;
using CSLA.web.App_Constantes;

namespace CSLA.web.App_pages.mod.ControlSeguimiento
{
    public partial class frw_operacionAsignacion : System.Web.UI.Page
    {
        #region Inicializacion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.cargarOperacion();
                    this.cargarListBox();
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.validarSession();
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Método que carga la información de la operación
        /// </summary>
        private void cargarOperacion()
        {
            try
            {
                string vs_codigo = Request.QueryString["ope"].ToString();

                cls_operacion vo_operacion = new cls_operacion();

                vo_operacion.pPK_Codigo = vs_codigo;

                vo_operacion = cls_gestorOperacion.seleccionarOperacion(vo_operacion, ((cls_usuario)Session["cls_usuario"]).pPK_usuario);

                this.lbl_codigoValor.Text = vs_codigo;

                this.lbl_descripcionValor.Text = vo_operacion.pDescripcion;

                vo_operacion.ListaAsignaciones = cls_gestorOperacion.listarAsignacionesOperacion(vo_operacion);

                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).obj = vo_operacion;

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error obteniendo la información de la operación.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Carga la información de los listboxs
        /// </summary>
        private void cargarListBox()
        {
            cls_operacion voOperacion = null;
            List<cls_usuario> listaUsuario = null;
            string vsFiltro = String.Empty;
            try
            {
                voOperacion = (cls_operacion)((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).obj;

                listaUsuario = new List<cls_usuario>();

                if (voOperacion.ListaAsignaciones.Count == 0)
                {
                    vsFiltro = "''";
                }

                foreach(cls_asignacionOperacion voAsignacion in voOperacion.ListaAsignaciones)
                {
                    listaUsuario.Add(voAsignacion.pUsuario);

                    vsFiltro += "'" + voAsignacion.pUsuario.pPK_usuario + "'" + ",";
                }

                vsFiltro = vsFiltro.Equals("''") ? vsFiltro : vsFiltro.Remove(vsFiltro.Length - 1, 1);

                this.ltb_usuarioAsignados.DataSource = listaUsuario;
                this.ltb_usuarioAsignados.DataBind();

                listaUsuario = cls_gestorUsuario.listarUsuarioFiltro("PK_usuario NOT IN (" + vsFiltro +") " );

                this.ltb_usuarioNoAsignados.DataSource = listaUsuario;
                this.ltb_usuarioNoAsignados.DataBind();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al cargar la información de los listboxs.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Generar las listas con asignaciones
        /// de eliminar y agregar en la asignacion masiva.
        /// </summary>
        /// <param name="poAsignacionesAgregar"></param>
        /// <param name="poAsignacionesEliminar"></param>
        private void generarDiferencias(out List<cls_asignacionOperacion> poAsignacionesAgregar, out List<cls_asignacionOperacion> poAsignacionesEliminar)
        {
            poAsignacionesAgregar = null;
            poAsignacionesEliminar = null;
            cls_operacion voOperacion = null;
            cls_asignacionOperacion voAsignacion = null;
            List<cls_asignacionOperacion> poAsignaciones = null;

            try
            {
                voOperacion = (cls_operacion)((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).obj;
                poAsignaciones = new List<cls_asignacionOperacion>();

                foreach (ListItem voItem in this.ltb_usuarioAsignados.Items)
                {
                    voAsignacion = new cls_asignacionOperacion();
                    voAsignacion.pFK_Operacion = voOperacion;
                    voAsignacion.pFK_Usuario = voItem.Value.ToString();
                    voAsignacion.pIsActivo = true;

                    voAsignacion.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                    poAsignaciones.Add(voAsignacion);
                }

                poAsignacionesAgregar = new List<cls_asignacionOperacion>();
                poAsignacionesEliminar = new List<cls_asignacionOperacion>();

                foreach (cls_asignacionOperacion pOperacion in poAsignaciones)
                {
                    if (!voOperacion.ListaAsignaciones.Exists(c => c.pFK_Usuario == pOperacion.pFK_Usuario))
                    {
                        pOperacion.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        poAsignacionesAgregar.Add(pOperacion);
                    }
                }

                foreach (cls_asignacionOperacion pOperacion in voOperacion.ListaAsignaciones)
                {
                    if (!poAsignaciones.Exists(c => c.pFK_Usuario == pOperacion.pFK_Usuario))
                    {
                        pOperacion.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                        poAsignacionesEliminar.Add(pOperacion);
                    }
                }

            }
            catch (Exception po_exception)
            {
                throw po_exception;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta 
        /// al agregar nuevos usuarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_agregar_Click1(object sender, EventArgs e)
        {

            List<ListItem> lista = new List<ListItem>();

            foreach (ListItem voItem in this.ltb_usuarioNoAsignados.Items)
            {
                if (voItem.Selected)
                {
                    this.ltb_usuarioAsignados.Items.Add(voItem);
                    lista.Add(voItem);
                }
            }

            foreach (ListItem voItem in lista)
            {

                this.ltb_usuarioNoAsignados.Items.Remove(voItem);
            }

        }

        /// <summary>
        /// Evento que se ejecuta al eliminar usuarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            List<ListItem> lista = new List<ListItem>();

            foreach (ListItem voItem in this.ltb_usuarioAsignados.Items)
            {
                if (voItem.Selected)
                {
                    this.ltb_usuarioNoAsignados.Items.Add(voItem);
                    lista.Add(voItem);
                }
            }

            foreach (ListItem voItem in lista)
            {

                this.ltb_usuarioAsignados.Items.Remove(voItem);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al guardar la asignación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            List<cls_asignacionOperacion> voAsignacionesAgregar = null;
            List<cls_asignacionOperacion> voAsignacionesEliminar = null;

            try
            {
                this.generarDiferencias(out voAsignacionesAgregar, out voAsignacionesEliminar);


                cls_gestorOperacion.crearAsignacionMasiva(voAsignacionesAgregar, voAsignacionesEliminar);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", "window.parent.$.fancybox.close();", true);
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al guardar la información.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }


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

        
      
        #region Propiedades

        #endregion

        #region Atributos

        #endregion

    }
}