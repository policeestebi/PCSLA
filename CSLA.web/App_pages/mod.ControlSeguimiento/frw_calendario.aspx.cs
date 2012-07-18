using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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


using COSEVI.lib.Security;

// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_usuarios.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			            MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	 29 –  04 - 2012	 	Se crea la clase
// Esteban Ramírez Gónzalez  	 01 –  05 - 2012	    Se agrega manejo se seguridad
//								
//								
//
// =========================================================================


namespace CSLA.web.App_pages.mod.ControlSeguimiento
{
    public partial class frm_calendario : System.Web.UI.Page
    {
        #region Inicializacion

        /// <summary>
        /// Inicialización de la página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.validarSession();

                this.InicializarCalendario();

                if (!this.IsPostBack)
                {
                   
                    this.obtenerPermisos();
                    this.validarAcceso();
                    
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Error al inicializar al iniciar el calendario.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Inicializa los datos del calendario.
        /// </summary>
        private void InicializarCalendario()
        {
            try
            {
                this.calendario.poDatosProyecto = this.ObtenerDatosProyectos();

                if (calendario.SelectValueProyecto == String.Empty)
                {
                    this.calendario.poDatosRegistro = this.ObtenerListaRegistro(this.ObtenerLunes(this.calendario.FechaLunes),
                                                        ((DataSet)this.calendario.poDatosProyecto).Tables[0].Rows[0][0].ToString()
                                                        );
                    this.calendario.poDatosActividades = this.ObtenerListaActividades(((DataSet)this.calendario.poDatosProyecto).Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    this.calendario.poDatosRegistro = this.ObtenerListaRegistro(this.ObtenerLunes(this.calendario.FechaLunes),
                                                        calendario.SelectValueProyecto
                                                        );
                    this.calendario.poDatosActividades = this.ObtenerListaActividades(calendario.SelectValueProyecto);
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Error al inicializar al iniciar el calendario.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Obtiene la lista de proyectos.
        /// </summary>
        /// <returns></returns>
        private Object ObtenerDatosProyectos()
        {
            Object vo_datos = null;
            try
            {
                vo_datos = cls_gestorProyecto.listarProyectosUsuarioDataSet();
            }
            catch (Exception)
            {
                vo_datos = null;
            }

            return vo_datos;
        }

        /// <summary>
        /// Inicializa los datos
        /// para cargar las actividades
        /// </summary>
        /// <returns></returns>
        public List<List<String>> ObtenerDatos()
        {
            List<List<String>> datos = null;

            int contador = 0;

            datos = new List<List<string>>();

            while (contador < 20)
            {
                datos.Add(new List<string>() { "Actividad" + contador, "0", "0", "0", "0", "0", "0", "0" });
                contador++;
            }
            return datos;
        }

        /// <summary>
        /// Obtiene los datos del
        /// registro de tiempos de las actividades
        /// </summary>
        /// <param name="pd_fecha"></param>
        /// <param name="ps_proyecto"></param>
        /// <returns></returns>
        private Object ObtenerListaRegistro(DateTime pd_fecha, string ps_proyecto)
        {
            Object datos = null;
            string vs_tipo = string.Empty;
            try
            {
                if (ps_proyecto.Equals(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_IMPREVISTO.ToString())
                    || ps_proyecto.Equals(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_OPERACION.ToString()))
                {

                    vs_tipo = ps_proyecto == COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_IMPREVISTO.ToString() ? "I" : "O";

                    datos = cls_gestorRegistroOperacion.listarOperacionesUsuario(cls_interface.vs_usuarioActual, 
                                                                                    vs_tipo, this.ConvertirFechaInicioDia(pd_fecha), 
                                                                                    this.ConvertirFechaInicioDia(pd_fecha.AddDays(6)));
                }
                else
                {
                    datos = cls_gestorRegistroActividad.listarRegistroActividadesUsuario(cls_interface.vs_usuarioActual,
                                                                                            ps_proyecto,
                                                                                            this.ConvertirFechaInicioDia(pd_fecha),
                                                                                            this.ConvertirFechaInicioDia(pd_fecha.AddDays(6)));
                }

            }
            catch (Exception po_exception)
            {
                datos = null;

                String vs_error_usuario = "Error al obtener la lista de registro de tiempos.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }

            return datos;
        }

        /// <summary>
        /// Convierte una fecha a una
        /// fecha a inicio de día.
        /// </summary>
        /// <param name="pd_fecha">DateTime fecha.</param>
        /// <returns>DateTime</returns>
        private DateTime ConvertirFechaInicioDia(DateTime pd_fecha)
        {
            return new DateTime(pd_fecha.Year, pd_fecha.Month, pd_fecha.Day);
        }

        /// <summary>
        /// Obtiene los datos del
        /// registro de tiempos de las actividades
        /// </summary>
        /// <param name="pd_fecha"></param>
        /// <param name="ps_proyecto"></param>
        /// <returns></returns>
        private Object ObtenerListaActividades(string ps_proyecto)
        {
            Object datos = null;
            string vs_tipo = string.Empty;
            try
            {
                if (ps_proyecto.Equals(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_IMPREVISTO.ToString())
                || ps_proyecto.Equals(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_OPERACION.ToString()))
                {
                    vs_tipo = ps_proyecto == COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.CODIGO_IMPREVISTO.ToString() ? "I" : "O";

                    datos = cls_gestorOperacion.listarOperacionesUsuario(cls_interface.vs_usuarioActual, vs_tipo);
                }
                else 
                {
                    datos = cls_gestorActividad.listarActividadesUsuario(cls_interface.vs_usuarioActual, ps_proyecto);
                }
            }
            catch (Exception po_exception) 
            {
                datos = null;

                String vs_error_usuario = "Error al obtener la lista de actividades.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
            return datos;
        }

        /// <summary>
        /// Obtiene el día lunes
        /// de la semana, según una fecha dada.
        /// </summary>
        /// <param name="pd_fecha">Datetime fecha.</param>
        /// <returns>Datetime fecha lunes de esa semana.</returns>
        public DateTime ObtenerLunes(DateTime pd_fecha)
        {
            DateTime vd_fecha = pd_fecha;

            if (pd_fecha.DayOfWeek == DayOfWeek.Monday)
                return pd_fecha;
            else
            {
                while (vd_fecha.DayOfWeek != DayOfWeek.Monday)
                {
                    vd_fecha = vd_fecha.AddDays(-1);
                }
            }

            return vd_fecha;
        }

        #endregion

        #region Eventos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pd_fechaLunes"></param>
        /// <param name="proyecto"></param>
        protected void Unnamed1_voCambioFecha(object sender, DateTime pd_fechaLunes, string proyecto)
        {

            this.calendario.poDatosRegistro = this.ObtenerListaRegistro(this.ObtenerLunes(pd_fechaLunes),//this.calendario.FechaLunes
                                               proyecto
                                               );
            this.calendario.poDatosActividades = this.ObtenerListaActividades(proyecto);
        }

        #endregion

        #region Atributos

        #endregion

        #region Propiedades

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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida",  cls_constantes.SCRIPTLOGOUT, true);
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
        /// Valida el acceso del usuario a la página
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
    }
}