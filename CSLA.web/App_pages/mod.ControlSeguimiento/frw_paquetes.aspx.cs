using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using ExceptionManagement.Exceptions;
using COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento;
using COSEVI.CSLA.lib.accesoDatos.mod.ControlSeguimiento;

using CSLA.web.App_Variables;
using CSLA.web.App_Constantes;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;


// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_permiso.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA     		           MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	03 – 06  - 2011	 	Se crea la clase
// Cristian Arce Jiménez      	08 – 29  - 2011	 	Se crea la clase
// Cristian Arce Jiménez  	    11 – 27  - 2011	 	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	    01 – 23  - 2012	 	Se agrega el manejo de filtros
// Cristian Arce Jiménez  	    05 – 04  - 2012	 	Se agrega el manejo de filtros
// 
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.ControlSeguimiento
{
    public partial class frw_paquetes : System.Web.UI.Page
    {

        #region Inicialización

        /// <summary>
        /// Función que se ejecuta al cargar
        /// la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.validarSession();

            if (!Page.IsPostBack)
            {
                try
                {
                    this.obtenerPermisos();
                    this.validarAcceso();
                    this.cargarPermisos();

                    this.llenarGridView();
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar el mantenimiento de paquetes.";
                    this.lanzarExcepcion(po_exception, vs_error_usuario);
                } 
            }
        }

        /// <summary>
        /// Función que se ejecuta al inicializar la página.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
			try
            {
				base.OnInit(e);
				if (!this.DesignMode)
				{
					this.inicializarControles();
				}
            }
            catch (Exception po_exception)
            {
				String vs_error_usuario = "Error al intentar inicializar los controles del mantenimiento de paquetes.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Método que inicializa los controles 
        ///que se encuentra dentro de los acordeones
        ///esto porque tienen a perderse cuando 
        ///la página se inicializa.
        /// </summary>
        private void inicializarControles()
        {
            try
            {
				//Botones de uso común
                this.btn_agregar = (Button)acp_listadoDatos.FindControl("btn_agregar");
                this.btn_cancelar = (Button)acp_edicionDatos.FindControl("btn_cancelar");
                this.btn_guardar = (Button)acp_edicionDatos.FindControl("btn_guardar");

                this.ucSearchPaquete.SearchClick +=new COSEVI.web.controls.ucSearch.searchClick(this.ucSearchPaquete_searchClick);

                //Se agregan los filtros.
                this.agregarItemListFiltro();

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error inicializando los controles del mantenimiento.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Agrega los filtros para el control de búsqueda.
        /// </summary>
        private void agregarItemListFiltro()
        {
			try
			{
				this.ucSearchPaquete.LstCollecction.Add(new ListItem("Paquete", "PK_paquete"));
				this.ucSearchPaquete.LstCollecction.Add(new ListItem("Codigo", "codigo"));
				this.ucSearchPaquete.LstCollecction.Add(new ListItem("Nombre", "nombre"));
				this.ucSearchPaquete.LstCollecction.Add(new ListItem("Descripcion", "descripcion"));
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al agregar los filtros para el mantenimiento.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que se encarga de 
        /// llenar la información del
        /// grid view
        /// </summary>
        private void llenarGridView()
        {
            try
            {
                this.grd_listaPaquete.Columns[0].Visible = true;
                this.grd_listaPaquete.DataSource = cls_gestorPaquete.listarPaquetes();
                this.grd_listaPaquete.DataBind();
                this.grd_listaPaquete.Columns[0].Visible = false;

                if (this.grd_listaPaquete.Rows.Count == 0)
                {
                    this.mostrarNoDatos();
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando la tabla con los paquetes.", po_exception);
            } 
        }

        /// <summary>
        /// Método que despliega el grid vacío.
        /// </summary>
        public void mostrarNoDatos()
        {
            List<object> vo_lista = new List<object>();
            vo_lista.Add(new cls_paquete());
            this.grd_listaPaquete.DataSource = vo_lista;
            this.grd_listaPaquete.DataBind();

            for (int i = 0; i < this.grd_listaPaquete.Columns.Count; i++)
            {
                this.grd_listaPaquete.Rows[0].Cells[i].Text = String.Empty;
            }

            this.grd_listaPaquete.Rows[0].Cells[1].ColumnSpan = this.grd_listaPaquete.Columns.Count;
            this.grd_listaPaquete.Rows[0].Cells[1].Text = "No se encontraron datos.";

        }

        /// <summary>
        /// Hace un buscar de la lista de paquetes.
        /// </summary>
        /// <param name="psFilter">String filtro.</param>
        private void llenarGridViewFilter(String psFilter)
        {
            try
            {
                this.grd_listaPaquete.Columns[0].Visible = true;
                this.grd_listaPaquete.DataSource = cls_gestorPaquete.listarPaqueteFiltro(psFilter);
                this.grd_listaPaquete.DataBind();
                this.grd_listaPaquete.Columns[0].Visible = false;

                if (this.grd_listaPaquete.Rows.Count == 0)
                {
                    this.mostrarNoDatos();
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando la tabla con el filtro de paquetes.", po_exception);
            } 
        }

        /// <summary>
        /// Crea un objeto de tipo
        /// cls_paquete con la informacón
        /// que se encuentra en el formulario
        /// web
        /// </summary>
        /// <returns>cls_paquete</returns>
        private cls_paquete crearObjeto()
        {
            cls_paquete vo_paquete = new cls_paquete();            
            try
            {
				if (cls_variablesSistema.tipoEstado != cls_constantes.AGREGAR)
				{
					vo_paquete = (cls_paquete)cls_variablesSistema.obj;
				}
                vo_paquete.pCodigo = txt_codigo.Text;
                vo_paquete.pNombre = txt_nombre.Text;
                vo_paquete.pDescripcion = txt_descripcion.Text;
                return vo_paquete;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el paquete para registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga la información
        /// de un paquete.
        /// </summary>
        private void cargarObjeto()
        {
            cls_paquete vo_paquete = null;

            try
            {
                vo_paquete = (cls_paquete)cls_variablesSistema.obj;
                this.txt_codigo.Text = vo_paquete.pCodigo;
                this.txt_nombre.Text = vo_paquete.pNombre;
                this.txt_descripcion.Text = vo_paquete.pDescripcion;
                if (cls_variablesSistema.tipoEstado == cls_constantes.VER)
                {
                    this.habilitarControles(false);
                }
                else
                {
                    this.habilitarControles(true);
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar el registro.", po_exception);
            } 

        }

        /// <summary>
        /// Método que elimina un paquete
        /// </summary>
        /// <param name="po_paquete">Paquete a eliminar</param>
        private void eliminarDatos(cls_paquete po_paquete)
        {
            try
            {
                cls_gestorPaquete.deletePaquete(po_paquete);

                this.llenarGridView();

                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error eliminando el paquete.", po_exception);
            }
        }

        /// <summary>
        /// Guarda la información se que se encuentra
        /// en el formulario Web, ya sea
        /// para ingresar o actualizarla
        /// </summary>
        /// <returns>Int valor devuelvo por el motor de bases de datos</returns>
        private int guardarDatos()
        {
            int vi_resultado = 1;
            cls_paquete vo_paquete = this.crearObjeto();
            try
            {
                switch (cls_variablesSistema.tipoEstado)
                {
                    case cls_constantes.AGREGAR:
                        vi_resultado = cls_gestorPaquete.insertPaquete(vo_paquete);
                        break;
                    case cls_constantes.EDITAR:
                        vi_resultado = cls_gestorPaquete.updatePaquete(vo_paquete);
                        break;
                    default:
                        break;
                }
                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar guardar el registro.", po_exception);
            } 
        }

        /// <summary>
        /// Método que limpia la información
        /// existente en los diferentes
        /// controles del formulario web
        /// </summary>
        private void limpiarCampos()
        {
			try
			{
				this.txt_codigo.Text = String.Empty;
				this.txt_nombre.Text = String.Empty;
				this.txt_descripcion.Text = String.Empty;
			}
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar limpiar los campos  de registro.", po_exception);
            } 
        }

        /// <summary>
        /// Método que habilita o 
        /// deshabilita los controles del
        /// formulario web.
        /// </summary>
        /// <param name="pb_habilitados"></param>
        private void habilitarControles(bool pb_habilitados)
        {
			try
			{
				this.txt_codigo.Enabled = pb_habilitados;
				this.txt_nombre.Enabled = pb_habilitados;
				this.txt_descripcion.Enabled = pb_habilitados;
				this.btn_guardar.Visible = pb_habilitados && (this.pbAgregar || this.pbModificar); 
			}
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar habilitar los campos  de registro.", po_exception);
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

        #endregion

        #region Eventos

        /// <summary>
        /// Busca un paquete según el filtro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <param name="seletecItem"></param>
        protected void ucSearchPaquete_searchClick(object sender, EventArgs e, string value, ListItem seletecItem)
        {
			try
			{
				this.llenarGridViewFilter(this.ucSearchPaquete.Filter); 
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar realizar el filtro para los registros.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Agrega un nuevo paquete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                cls_variablesSistema.tipoEstado = cls_constantes.AGREGAR;

                this.limpiarCampos();

                this.habilitarControles(true);

                this.upd_Principal.Update();

                this.ard_principal.SelectedIndex = 1;
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar mostrar la ventana de edición para los registros.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento que se ejecuta cuando se 
        /// guarda un nuevo paquete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.guardarDatos();

                this.llenarGridView();

                this.limpiarCampos();

                this.upd_Principal.Update();

                this.ard_principal.SelectedIndex = 0;
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error mientras se guardaba el registro.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da
        /// en el botón de cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.habilitarControles(true);

                this.limpiarCampos();

                this.upd_Principal.Update();

                this.ard_principal.SelectedIndex = 0;

                this.ucSearchPaquete_searchClick(null, null, null, null);
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al cancelar la operación.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Cambiar de índice de página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grd_listaPaquete_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {                
                this.grd_listaPaquete.PageIndex = e.NewPageIndex;
                this.llenarGridView();
                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al realizar el listado de paquetes.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Cuando se seleccionada un botón del grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grd_listaPaquete_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int vi_indice = Convert.ToInt32(e.CommandArgument);

                GridViewRow vu_fila = this.grd_listaPaquete.Rows[vi_indice];

                cls_paquete vo_paquete = new cls_paquete();

                vo_paquete.pPK_Paquete = Convert.ToInt32(vu_fila.Cells[0].Text.ToString());
                vo_paquete.pCodigo = vu_fila.Cells[1].Text.ToString();
                vo_paquete.pNombre = vu_fila.Cells[2].Text.ToString();
                vo_paquete.pDescripcion = vu_fila.Cells[3].Text.ToString();

                switch (e.CommandName.ToString())
                {
                    case cls_constantes.VER:
                        vo_paquete = cls_gestorPaquete.seleccionarPaquetes(vo_paquete);

                        cls_variablesSistema.obj = vo_paquete;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.EDITAR:
                        vo_paquete = cls_gestorPaquete.seleccionarPaquetes(vo_paquete);

                        cls_variablesSistema.obj = vo_paquete;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.ELIMINAR:
                        this.eliminarDatos(vo_paquete);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar acceder a la información del registro.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
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

                if (this.Session["cls_usuario"] != null)
                {
                    Session[cls_constantes.PAGINA] = cls_gestorPagina.obtenerPermisoPaginaRol(lsUrl, ((cls_usuario)this.Session["cls_usuario"]).pFK_rol);
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener los permisos del rol en la página actual..", po_exception);
            }
        }

        /// <summary>
        /// Carga los permisos según la página.
        /// </summary>
        private void cargarPermisos()
        {
            try
            {
                this.btn_agregar.Visible = this.pbAgregar;
                this.btn_guardar.Visible = this.pbModificar || this.pbAgregar;
                this.grd_listaPaquete.Columns[4].Visible = this.pbAcceso;
                this.grd_listaPaquete.Columns[5].Visible = this.pbModificar;
                this.grd_listaPaquete.Columns[6].Visible = this.pbEliminar;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar cargar los permisos para la página actual..", po_exception);
            }
        }

        #endregion Seguridad

    }
}