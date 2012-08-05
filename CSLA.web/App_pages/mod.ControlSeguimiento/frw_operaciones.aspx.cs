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
    public partial class frw_operaciones : System.Web.UI.Page
    {
        #region Inicializacion

        /// <summary>
        /// Función que se ejecuta
        /// cuando se carga la página.
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
                    this.llenarComboProyectos();
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar el mantenimiento de operaciones.";
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

            base.OnInit(e);
            if (!this.DesignMode)
            {
                this.inicializarControles();
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
                this.btn_agregar = (Button)acp_listadoDatos.FindControl("btn_agregar");
                this.btn_cancelar = (Button)acp_edicionDatos.FindControl("btn_cancelar");
                this.btn_guardar = (Button)acp_edicionDatos.FindControl("btn_guardar");

                this.ucSearchPaquete.SearchClick += new COSEVI.web.controls.ucSearch.searchClick(this.ucSearchPaquete_searchClick);

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

            this.ucSearchPaquete.LstCollecction.Add(new ListItem("Código", "PK_codigo"));
            this.ucSearchPaquete.LstCollecction.Add(new ListItem("Tipo", "tipo"));
            this.ucSearchPaquete.LstCollecction.Add(new ListItem("Descripcion", "descripcion"));
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Método que se encarga de 
        /// llenar la información del
        /// grid view
        /// </summary>
        private void llenarGridView()
        {
            try
            {
                //this.grd_listaOperaciones.Columns[0].Visible = true;
                this.grd_listaOperaciones.DataSource = cls_gestorOperacion.listarOperaciones(((cls_usuario)(Session["cls_usuario"])).pPK_usuario);
                this.grd_listaOperaciones.DataBind();

                if (this.grd_listaOperaciones.Rows.Count == 0)
                {
                    this.mostrarNoDatos();
                }


            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando la tabla.", po_exception);
            }
        }

        /// <summary>
        /// Método que despliega el grid vacío.
        /// </summary>
        public void mostrarNoDatos()
        {
            List<object> vo_lista = new List<object>();
            vo_lista.Add(new cls_operacion());
            this.grd_listaOperaciones.DataSource = vo_lista;
            this.grd_listaOperaciones.DataBind();

            for (int i = 0; i < this.grd_listaOperaciones.Columns.Count; i++)
            {
                this.grd_listaOperaciones.Rows[0].Cells[i].Text = String.Empty;
            }

            this.grd_listaOperaciones.Rows[0].Cells[0].ColumnSpan = this.grd_listaOperaciones.Columns.Count;
            this.grd_listaOperaciones.Rows[0].Cells[0].Text = "No se encontraron datos.";

        }

        /// <summary>
        /// Método que se encarga
        /// de llanar la información
        /// del combo de proyectos.
        /// </summary>
        private void llenarComboProyectos()
        {
            try
            {
                this.ddl_Proyectos.DataSource = cls_gestorProyecto.listarProyectosUsuario();
                this.ddl_Proyectos.DataBind();

                this.ddl_Proyectos.Items.RemoveAt(0);
                this.ddl_Proyectos.Items.RemoveAt(0);

                this.ddl_Proyectos.Items.Insert(0,new ListItem("Seleccione..","-1"));

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando el combo de operaciones.", po_exception);
            }
        }

        /// <summary>
        /// Hace un buscar de la lista de operaciones.
        /// </summary>
        /// <param name="psFilter">String filtro.</param>
        private void llenarGridViewFilter(String psFilter)
        {
            try
            {
                psFilter = "o." + psFilter;
                //this.grd_listaOperaciones.Columns[0].Visible = true;
                this.grd_listaOperaciones.DataSource = cls_gestorOperacion.listarPaqueteFiltro(psFilter);
                this.grd_listaOperaciones.DataBind();
                //this.grd_listaOperaciones.Columns[0].Visible = false;

                if (this.grd_listaOperaciones.Rows.Count == 0)
                {
                    this.mostrarNoDatos();
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error llenando la tabla con filtro.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Crea un objeto de tipo
        /// cls_operacion con la informacón
        /// que se encuentra en el formulario
        /// web
        /// </summary>
        /// <returns>cls_operacion</returns>
        private cls_operacion crearObjeto()
        {
            cls_operacion vo_operacion = new cls_operacion();
            if (cls_variablesSistema.tipoEstado != cls_constantes.AGREGAR)
            {
                vo_operacion = (cls_operacion)cls_variablesSistema.obj;
            }
            try
            {
                vo_operacion.pPK_Codigo = txt_codigo.Text;
                vo_operacion.pTipo = this.ddl_Tipos.SelectedValue.ToString();
                vo_operacion.pDescripcion = txt_descripcion.Text;
                vo_operacion.pFK_Proyecto = Convert.ToInt32(ddl_Proyectos.SelectedValue.ToString());
                vo_operacion.pActivo = this.chk_activo.Checked;
                return vo_operacion;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el objeto para guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga la información
        /// de un entregable.
        /// </summary>
        private void cargarObjeto()
        {
            cls_operacion vo_operacion = null;

            try
            {
                vo_operacion = (cls_operacion)cls_variablesSistema.obj;
                this.txt_codigo.Text = vo_operacion.pPK_Codigo;
                this.ddl_Tipos.SelectedValue = vo_operacion.pTipo;
                this.txt_descripcion.Text = vo_operacion.pDescripcion;
                this.ddl_Proyectos.SelectedValue = vo_operacion.pFK_Proyecto.ToString();
                this.chk_activo.Checked = vo_operacion.pActivo;

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
        /// Método que elimina un entregable
        /// </summary>
        /// <param name="po_paquete">Permiso a eliminar</param>
        private void eliminarDatos(cls_operacion po_operacion)
        {
            try
            {
                cls_asignacionOperacion vo_asignacion = new cls_asignacionOperacion();

                vo_asignacion.pFK_Operacion = po_operacion;

                vo_asignacion.pFK_Usuario = ((cls_usuario)(Session["cls_usuario"])).pPK_usuario;

                cls_gestorOperacion.deleteOperacion(vo_asignacion);

                this.llenarGridView();

                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error eliminando la operación.", po_exception);
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
            cls_operacion vo_operacion = this.crearObjeto();
            cls_asignacionOperacion vo_asignacion = null;
            try
            {
                vo_asignacion = new cls_asignacionOperacion();

                vo_asignacion.pFK_Operacion = vo_operacion;
                vo_asignacion.pFK_Usuario = ((cls_usuario)(Session["cls_usuario"])).pPK_usuario;

                switch (cls_variablesSistema.tipoEstado)
                {
                    case cls_constantes.AGREGAR:
                        vi_resultado = cls_gestorOperacion.insertOperacion(vo_asignacion);
                        break;
                    case cls_constantes.EDITAR:
                        vi_resultado = cls_gestorOperacion.updateOperacion(vo_operacion);
                        break;
                    default:
                        break;
                }
                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que limpia la información
        /// existente en los diferentes
        /// controles del formulario web
        /// </summary>
        private void limpiarCampos()
        {
            this.txt_codigo.Text = String.Empty;
            this.ddl_Tipos.SelectedIndex = 0;
            this.ddl_Proyectos.SelectedIndex = 0;
            this.txt_descripcion.Text = String.Empty;
            this.chk_activo.Checked = true;
        }

        /// <summary>
        /// Método que habilita o 
        /// deshabilita los controles del
        /// formulario web.
        /// </summary>
        /// <param name="pb_habilitados"></param>
        private void habilitarControles(bool pb_habilitados)
        {
            if (cls_variablesSistema.tipoEstado == cls_constantes.EDITAR)
            {
                this.ddl_Tipos.Enabled = false;
                this.ddl_Proyectos.Enabled = false;
            }
            else
            {
                this.ddl_Tipos.Enabled = pb_habilitados;
                this.ddl_Proyectos.Enabled = pb_habilitados;
            }
            this.txt_descripcion.Enabled = pb_habilitados;
            this.chk_activo.Enabled = pb_habilitados;
            this.btn_guardar.Visible = pb_habilitados && (this.pbAgregar || this.pbModificar); ;



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
        /// Busca una operación según el filtro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <param name="seletecItem"></param>
        protected void ucSearchPaquete_searchClick(object sender, EventArgs e, string value, ListItem seletecItem)
        {

            this.llenarGridViewFilter(this.ucSearchPaquete.Filter);

        }

        /// <summary>
        /// Agrega un nuevo entregable.
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
        /// guarda un nuevo rol.
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
                this.grd_listaOperaciones.PageIndex = e.NewPageIndex;
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

                GridViewRow vu_fila = this.grd_listaOperaciones.Rows[vi_indice];

                cls_operacion vo_operacion = new cls_operacion();

                vo_operacion.pPK_Codigo = vu_fila.Cells[0].Text.ToString();
                vo_operacion.pTipo = vu_fila.Cells[1].Text.ToString() == "Operación" ? "O" : "I";
                vo_operacion.pDescripcion = vu_fila.Cells[2].Text.ToString();

                switch (e.CommandName.ToString())
                {
                    case cls_constantes.VER:
                        vo_operacion = cls_gestorOperacion.seleccionarOperacion(vo_operacion);

                        cls_variablesSistema.obj = vo_operacion;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.EDITAR:
                        vo_operacion = cls_gestorOperacion.seleccionarOperacion(vo_operacion);

                        cls_variablesSistema.obj = vo_operacion;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.ELIMINAR:
                        this.eliminarDatos(vo_operacion);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar mostrar la ventana de edición para los registros.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se cambia 
        /// el tipo de tipo de operación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_Tipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddl_Tipos.SelectedValue == "O")
            {
                this.ddl_Proyectos.Enabled = false;
                this.ddl_Tipos.SelectedIndex = 0;
            }
            else
            {
                this.ddl_Proyectos.Enabled = true;
            }

            this.upd_Principal.Update();
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
                throw new Exception("Ocurrió un error al obtener los permisos en la página actual.", po_exception);
            }
        }

        /// <summary>
        /// Carga los permisos según la página.
        /// </summary>
        private void cargarPermisos()
        {
            this.btn_agregar.Visible = this.pbAgregar;
            this.btn_guardar.Visible = this.pbModificar || this.pbAgregar;
            this.grd_listaOperaciones.Columns[3].Visible = this.pbAcceso;
            this.grd_listaOperaciones.Columns[4].Visible = this.pbModificar;
            this.grd_listaOperaciones.Columns[5].Visible = this.pbEliminar;
        }

        #endregion
       
    }
}