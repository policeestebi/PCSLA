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
using System.Text;

namespace CSLA.web.App_pages.mod.ControlSeguimiento
{
    public partial class frm_registroTiempos : System.Web.UI.Page
    {
        #region Inicilizacion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.validarSession();

                if (!this.IsPostBack)
                {

                    this.obtenerPermisos();

                    this.cargarObjetoSegunUrl();
                    this.cargarValores();
                }
            }
            catch (Exception po_exception)
            {
                this.lanzarExcepcion(po_exception, "Error al cargar la página");
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Carga los valores enviados
        /// por la url.
        /// </summary>
        private void cargarObjetoSegunUrl()
        {
            try
            {
                //String p = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];

                string vs_actividad = Request.QueryString["act"].ToString(); //this.obtenerParam("act").ToString();
                string vs_proyecto = Request.QueryString["pro"].ToString(); //this.obtenerParam("pro").ToString();
                string vs_registro = Request.QueryString["preg"].ToString(); //this.obtenerParam("preg").ToString();//
                string vs_fecha = Request.QueryString["fech"].ToString(); //this.obtenerParam("fech").ToString();//

                string vs_paquete = Request.QueryString["paq"] == null ? "" : Request.QueryString["paq"].ToString();
                string vs_componente = Request.QueryString["comp"] == null ? "" : Request.QueryString["comp"].ToString();
                string vs_entregable = Request.QueryString["ent"] == null ? "" : Request.QueryString["ent"].ToString();

                //string vs_paquete = this.obtenerParam("paq") == null ? "" : this.obtenerParam("paq").ToString();
                //string vs_componente = this.obtenerParam("comp") == null ? "" : this.obtenerParam("comp").ToString();
                //string vs_entregable = this.obtenerParam("ent") == null ? "" : this.obtenerParam("ent").ToString();

                //Se cargan los datos
                if (vs_proyecto == COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_IMPREVISTO.ToString() ||
                    vs_proyecto == COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_OPERACION.ToString() ||
                    vs_paquete == COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_INVALIDO.ToString())
                {
                    this.cargarOperacion(vs_actividad, vs_proyecto, vs_registro, vs_fecha);
                }
                else
                {
                    this.cargarActividad(vs_actividad, vs_paquete, vs_componente, vs_entregable, vs_proyecto, vs_registro, vs_fecha);
                }

            }
            catch (Exception po_exception)
            {
                this.lanzarExcepcion(po_exception, "Error al cargar el registro de tiempos");
            }
        }

        private object obtenerParam(string ps_nombre)
        {
            object vo_valor = null;
            string[] vo_params = HttpContext.Current.Request.ServerVariables["QUERY_STRING"].Split('&');

            for (int i = 0; i < vo_params.Length; i++)
            {
                if (vo_params[i].Split('=')[0].Equals(ps_nombre))
                {
                    vo_valor = vo_params[i].Split('=')[1];
                    i = vo_params.Length;
                }
            }

            return vo_valor;
        }

        /// <summary>
        /// Carga un registro de tipo operación.
        /// </summary>
        /// <param name="ps_actividad">String código</param>
        /// <param name="ps_proyecto">String proyecto.</param>
        /// <param name="ps_registro">String registro</param>
        /// <param name="ps_fecha">String fecha.</param>
        private void cargarOperacion(string ps_actividad, string ps_proyecto, string ps_registro, string ps_fecha)
        {
            cls_registroOperacion vo_registro;
            cls_operacion vo_operacion;
            cls_asignacionOperacion vo_asignacion;

            try
            {
                vo_registro = new cls_registroOperacion();


                vo_operacion = new cls_operacion();
                vo_operacion.pPK_Codigo = ps_actividad;
                vo_operacion = cls_gestorOperacion.seleccionarOperacion(vo_operacion);
                vo_operacion.pFK_Proyecto = Convert.ToInt32(ps_proyecto);

                vo_asignacion = new cls_asignacionOperacion();
                vo_asignacion.pFK_Operacion = vo_operacion;
                vo_asignacion.pFK_Usuario = cls_interface.vs_usuarioActual;

                vo_registro = new cls_registroOperacion();
                vo_registro.pFK_Asignacion = vo_asignacion;
                vo_registro.pFecha = Convert.ToDateTime(ps_fecha);

                if (String.IsNullOrEmpty(ps_registro))
                {
                    vo_registro.pHoras = 0;
                    vo_registro.pComentario = String.Empty;
                }
                else
                {
                    vo_registro.pPK_registro = Convert.ToDecimal(ps_registro);
                    cls_gestorRegistroOperacion.seleccionarRegistroOperacion(vo_registro);
                }



                cls_variablesSistema.obj = vo_registro;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carga la instancia del
        /// registro de la asignación
        /// </summary>
        /// <param name="ps_actividad">String código actividad.</param>
        /// <param name="ps_paquete">String código paquete.</param>
        /// <param name="ps_componente">String código componente.</param>
        /// <param name="ps_entregable">String código entregable.</param>
        /// <param name="ps_proyecto">String código proyecto.</param>
        /// <param name="ps_registro">String código de registro.</param>
        /// <param name="ps_fecha">DateTime Fecha.</param>
        private void cargarActividad(string ps_actividad,
                                     string ps_paquete,
                                     string ps_componente,
                                     string ps_entregable,
                                     string ps_proyecto,
                                     string ps_registro,
                                     string ps_fecha)
        {
            cls_registroActividad vo_registro;
            cls_actividad vo_actividad;
            cls_asignacionActividad vo_asignacion;
            cls_paqueteActividad vo_paquete = null;
            try
            {
                vo_registro = new cls_registroActividad();
              

                vo_actividad = new cls_actividad();
                vo_actividad.pPK_Actividad = Convert.ToInt32(ps_actividad);
                vo_actividad = cls_gestorActividad.seleccionarActividad(vo_actividad);

                vo_paquete = new cls_paqueteActividad();
                vo_paquete.pPK_Actividad = vo_actividad.pPK_Actividad;
                vo_paquete.pPK_Componente = Convert.ToInt32(ps_componente);
                vo_paquete.pPK_Entregable = Convert.ToInt32(ps_entregable);
                vo_paquete.pPK_Paquete = Convert.ToInt32(ps_paquete);
                vo_paquete.pPK_Proyecto = Convert.ToInt32(ps_proyecto);

                vo_asignacion = new cls_asignacionActividad();
                vo_asignacion = cls_gestorAsignacionActividad.seleccionarAsignacionActividad(vo_paquete);
                vo_asignacion.pActividad = vo_actividad;
                vo_asignacion.pPK_Componente = Convert.ToInt32(ps_componente);
                vo_asignacion.pPK_Entregable = Convert.ToInt32(ps_entregable);
                vo_asignacion.pPK_Paquete = Convert.ToInt32(ps_paquete);
                vo_asignacion.pPK_Actividad = vo_actividad.pPK_Actividad;
                vo_asignacion.pPK_Proyecto = Convert.ToInt32(ps_proyecto);
                vo_asignacion.pPK_Usuario = cls_interface.vs_usuarioActual;

                vo_registro = new cls_registroActividad();
                vo_registro.pAsignacion = vo_asignacion;
                vo_registro.pFecha = Convert.ToDateTime(ps_fecha);

                if (String.IsNullOrEmpty(ps_registro))
                {
                    vo_registro.pHoras = 0;
                    vo_registro.pComentario = String.Empty;
                }
                else
                {
                    vo_registro.pRegistro = Convert.ToDecimal(ps_registro);
                    cls_gestorRegistroActividad.seleccionarRegistroActividad(vo_registro);
                }

                cls_variablesSistema.obj = vo_registro;


            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Levanta mensaje de advertencia cuando la actividad se 
        /// encuentra atrasada o esta superando el tiempo estimado.
        /// </summary>
        /// <param name="poAsignacion"></param>
        private void colocarActividadAtrasada(cls_asignacionActividad poAsignacion)
        {
            bool vbAtrasada = false;
            bool vbSupera = false;
            String vstitle = "Advertencia";
            String vsMsg1 = "Este mensaje es para avisarle que esta tarea se encuentra atrasada, verifique la fecha final asignada a esta actividad.";
            String vsMsg2 = "Este mensaje es para avisarle que esta tarea supera el tiempo estimado designado para realizarla.";
            String vsMsg3 = "Este mensaje es para avisarle que esta tarea se encuentra atrasada y además ha superado el tiempo estimado designado";
            String vsMensaje = String.Empty;
            try
            {
                cls_gestorRegistroActividad.verificarActividadAtrasada(poAsignacion, out vbAtrasada, out vbSupera);

                if (vbAtrasada && vbSupera)
                {
                    vsMensaje = vsMsg3;
                }
                else
                {
                    if (vbAtrasada)
                    {
                        vsMensaje = vsMsg1;
                    }
                    else
                    {
                        vsMensaje = vsMsg2;
                    }
                }

                if (vbAtrasada || vbSupera)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Advertencia", "DesplegarAdvertencia('" + vstitle + "', '" + vsMensaje + "');", true);
                }

            }
            catch (Exception po_exception)
            {
                this.lanzarExcepcion(po_exception, "Al colocar actividad atrasada.");
            }
        }

        /// <summary>
        /// Se cargan los
        /// valores en los campos.
        /// </summary>
        private void cargarValores()
        {
            cls_registroOperacion vo_registro = null;

            try
            {
                if (cls_variablesSistema.obj != null)
                {

                    if (cls_variablesSistema.obj.GetType().Name == "cls_registroOperacion")
                    {
                        vo_registro = (cls_registroOperacion)cls_variablesSistema.obj;

                        cls_proyecto vo_proyecto = new cls_proyecto();
                        vo_proyecto.pPK_proyecto = vo_registro.pFK_Asignacion.pFK_Operacion.pFK_Proyecto;

                        if (vo_proyecto.pPK_proyecto == COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_IMPREVISTO)
                        {
                            this.lblProyectoValor.Text = COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.NOMBRE_IMPREVISTO;
                        }
                        else if (vo_proyecto.pPK_proyecto == COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_OPERACION)
                        {
                            this.lblProyectoValor.Text = COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.NOMBRE_OPERACION;
                        }
                        else
                        {
                            vo_proyecto = cls_gestorProyecto.seleccionarProyectos(vo_proyecto);
                            this.lblProyectoValor.Text = vo_proyecto.pNombre;

                        }


                        this.lblDiaValor.Text = vo_registro.pFecha.ToString("dddd, dd MMMM yyyy");
                        this.lblActividadValor.Text = vo_registro.pFK_Asignacion.pFK_Operacion.pDescripcion;
                        this.txtHoras.Text = vo_registro.pHoras.ToString();
                        this.txtComentarios.Text = vo_registro.pComentario;
                    }
                    else
                    {
                        this.cargaValoresActividad();
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carga los valores de la actividad
        /// en los controles de la ventana.
        /// </summary>
        private void cargaValoresActividad()
        {
            cls_registroActividad vo_registro = null;

            vo_registro = cls_variablesSistema.obj as cls_registroActividad;

            cls_proyecto vo_proyecto = new cls_proyecto();
            vo_proyecto.pPK_proyecto = vo_registro.pAsignacion.pPK_Proyecto;

            vo_proyecto = cls_gestorProyecto.seleccionarProyectos(vo_proyecto);
            this.lblProyectoValor.Text = vo_proyecto.pNombre;

            this.lblDiaValor.Text = vo_registro.pFecha.ToString("dddd, dd MMMM yyyy");
            this.lblActividadValor.Text = vo_registro.pAsignacion.pActividad.pDescripcion;
            this.txtHoras.Text = vo_registro.pHoras.ToString();
            this.txtComentarios.Text = vo_registro.pComentario;


            this.lblFechaFinal.Visible = true;
            this.lblFechaFinalValor.Visible = true;
            this.lblFechaInicio.Visible = true;
            this.lblFechaInicioValor.Visible = true;
            this.lblHoraAsignadas.Visible = true;
            this.lblHorasAsignadasValor.Visible = true;

            this.lblFechaInicioValor.Text = vo_registro.pAsignacion.pFechaInicio.ToString("dd/MM/yyyy");
            this.lblFechaFinalValor.Text = vo_registro.pAsignacion.pFechaFin.ToString("dd/MM/yyyy");

            this.lblHorasAsignadasValor.Text = vo_registro.pAsignacion.pHorasAsignadas.ToString();

            this.colocarActividadAtrasada(vo_registro.pAsignacion);
        }

        /// <summary>
        /// Se cargan los valores de los controles
        /// en la instancia.
        /// </summary>
        private void cargarValoresInstancia()
        {
            try
            {
                if (cls_variablesSistema.obj != null && cls_variablesSistema.obj.GetType().Name == "cls_registroOperacion")
                {
                    cls_registroOperacion vo_registro = (cls_registroOperacion)cls_variablesSistema.obj;

                    vo_registro.pHoras = Convert.ToDecimal(this.txtHoras.Text);
                    vo_registro.pComentario = this.txtComentarios.Text;
                }
                else
                {
                    cls_registroActividad vo_registro = cls_variablesSistema.obj as cls_registroActividad;

                    vo_registro.pHoras = Convert.ToDecimal(this.txtHoras.Text);
                    vo_registro.pComentario = this.txtComentarios.Text;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta cuando se graba el registro
        /// de tiempo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.cargarValoresInstancia();

                if (cls_variablesSistema.obj != null && cls_variablesSistema.obj.GetType().Name == "cls_registroOperacion")
                {
                    cls_registroOperacion vo_registro = (cls_registroOperacion)cls_variablesSistema.obj;

                    //Si es nulo o vacío se debe insertar
                    if (vo_registro.pPK_registro == 0)
                    {
                        cls_gestorRegistroOperacion.insertRegistroOperacion(vo_registro);
                    }
                    else
                    {
                        cls_gestorRegistroOperacion.updateRegistroOperacion(vo_registro);
                    }

                }
                else
                {
                    cls_registroActividad vo_registro = (cls_registroActividad)cls_variablesSistema.obj;

                    //Si es nulo o vacío se debe insertar
                    if (vo_registro.pRegistro == 0)
                    {
                        cls_gestorRegistroActividad.insertRegistroActividad(vo_registro);
                    }
                    else
                    {
                        cls_gestorRegistroActividad.updateRegistroActividad(vo_registro);
                    }

                    this.colocarActividadAtrasada(vo_registro.pAsignacion);
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", "MostrarMensaje();", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", "alert(\"Se ha grabado con éxito el registro de tiempos\");", true);
            }
            catch (Exception po_excepciones)
            {
                this.lanzarExcepcion(po_excepciones, "Error al guardar la información");
            }
        }

        #endregion

        #region Propiedades

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
        /// Valida el permiso de agregar del usuario en página.
        /// </summary>
        private bool pbAgregar
        {
            get
            {
                if (Session[cls_constantes.PAGINA] != null)
                {
                    return (Session[cls_constantes.PAGINA] as cls_pagina)[cls_constantes.AGREGAR] != null;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Valida el permiso de modificar del usuario en página.
        /// </summary>
        private bool pbModificar
        {
            get
            {
                if (Session[cls_constantes.PAGINA] != null)
                {
                    return (Session[cls_constantes.PAGINA] as cls_pagina)[cls_constantes.MODIFICAR] != null;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Valida el permiso de eliminar del usuario en la página.
        /// </summary>
        private bool pbEliminar
        {
            get
            {
                if (Session[cls_constantes.PAGINA] != null)
                {
                    return (Session[cls_constantes.PAGINA] as cls_pagina)[cls_constantes.ELIMINAR] != null;
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

                if (this.Session["cls_usuario"] == null)
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