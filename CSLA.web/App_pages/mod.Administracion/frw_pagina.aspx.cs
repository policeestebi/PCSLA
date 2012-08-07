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


// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_p.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			        MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	03 – 06  - 2011	 	Se crea la clase
// Cristian Arce Jimenez      	27 – 08  - 2011	 	Se crea la clase
// Cristian Arce Jiménez  	    27 – 11  - 2011	 	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez		01 - 22  - 2012		Modificación en la búsqueda através de filtros
// Esteban Ramírez Gónzalez  	01 – 05 - 2012	    Se agrega manejo se seguridad
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.Administracion
{
    public partial class frw_pagina : System.Web.UI.Page
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
                    this.llenarComboMenu();
                    this.llenarTreeViewPermisos();
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar el mantenimiento de páginas.";
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
                this.btn_permanente = (Button)pan_mensajeeliminar.FindControl("btn_permanente");

                this.ucSearchPagina.SearchClick += new COSEVI.web.controls.ucSearch.searchClick(this.ucSearchPagina_searchClick);

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

            this.ucSearchPagina.LstCollecction.Add(new ListItem("Nombre", "nombre"));
            this.ucSearchPagina.LstCollecction.Add(new ListItem("Url", "url"));
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
                this.grd_listaPaginas.Columns[0].Visible = true;
                this.grd_listaPaginas.DataSource = cls_gestorPagina.listarPagina();
                this.grd_listaPaginas.DataBind();
                this.grd_listaPaginas.Columns[0].Visible = false;

                if (this.grd_listaPaginas.Rows.Count == 0)
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
            vo_lista.Add(new cls_pagina());
            this.grd_listaPaginas.DataSource = vo_lista;
            this.grd_listaPaginas.DataBind();

            for (int i = 0; i < this.grd_listaPaginas.Columns.Count; i++)
            {
                this.grd_listaPaginas.Rows[0].Cells[i].Text = String.Empty;
            }

            this.grd_listaPaginas.Rows[0].Cells[1].ColumnSpan = this.grd_listaPaginas.Columns.Count;
            this.grd_listaPaginas.Rows[0].Cells[1].Text = "No se encontraron datos.";

        }

        /// <summary>
        /// Hace un buscar de la lista de pa´ginas.
        /// </summary>
        /// <param name="psFilter">String filtro.</param>
        private void llenarGridViewFilter(String psFilter)
        {
            try
            {
                this.grd_listaPaginas.Columns[0].Visible = true;
                this.grd_listaPaginas.DataSource = cls_gestorPagina.listarPaginaFiltro(psFilter);
                this.grd_listaPaginas.DataBind();
                this.grd_listaPaginas.Columns[0].Visible = false;

                if (this.grd_listaPaginas.Rows.Count == 0)
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
        /// Método que llena el tree de permismos.
        /// </summary>
        private void llenarTreeViewPermisos()
        {
            try
            {
                this.trv_permisos.ShowCheckBoxes = TreeNodeTypes.Leaf;
                TreeNode vo_node = new TreeNode("Permisos", "-1");
                this.trv_permisos.Nodes.Add(vo_node);
                List<cls_permiso> vo_permisos = cls_gestorPermiso.listarPermiso();

                foreach (cls_permiso vo_permiso in vo_permisos)
                {
                    vo_node.ChildNodes.Add(new TreeNode(vo_permiso.pNombre, vo_permiso.pPK_permiso.ToString()));
                }

                this.trv_permisos.ExpandAll();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando la el combo tree view de permisos.", po_exception);
            }
        }

        /// <summary>
        /// Me´todo que se encarga
        /// de llenar la información
        /// del combo de menú.
        /// </summary>
        private void llenarComboMenu()
        {
            try
            {
                this.ddl_menu.DataSource = cls_gestorMenu.listarMenu();
                this.ddl_menu.DataBind();
                this.ddl_menu.Items.Insert(0, new ListItem("Sin padre", "0"));
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando la el combo del menú.", po_exception);
            }
        }

        /// <summary>
        /// Crea un objeto de tipo
        /// cls_pagina con la informacón
        /// que se encuentra en el formulario
        /// web
        /// </summary>
        /// <returns>cls_pagina</returns>
        private cls_pagina crearObjeto()
        {
            cls_pagina vo_pagina = new cls_pagina();
            if (cls_variablesSistema.tipoEstado != cls_constantes.AGREGAR)
            {
                vo_pagina = (cls_pagina)cls_variablesSistema.obj;
            }
            try
            {
                vo_pagina.pNombre = txt_nombre.Text;
                vo_pagina.pUrl = txt_url.Text;
                vo_pagina.FK_menu = this.ddl_menu.SelectedIndex;
                vo_pagina.pHeight = this.txt_largo.Text;

                vo_pagina.Permisos = new List<cls_permiso>();
                cls_permiso vo_permiso = null;

                foreach (TreeNode vo_nodo in this.trv_permisos.CheckedNodes) 
                {
                    vo_permiso = new cls_permiso();
                    vo_permiso.pPK_permiso = Convert.ToInt32(vo_nodo.Value);
                    vo_permiso.pNombre = vo_nodo.Text;

                    vo_pagina.Permisos.Add(vo_permiso);
                }

                return vo_pagina;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el objeto para guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga la información
        /// de un pagina.
        /// </summary>
        private void cargarObjeto()
        {
            cls_pagina vo_pagina = null;

            try
            {
                vo_pagina = (cls_pagina)cls_variablesSistema.obj;
                this.txt_nombre.Text = vo_pagina.pNombre;
                this.txt_url.Text = vo_pagina.pUrl;
                this.ddl_menu.SelectedIndex = vo_pagina.FK_menu;
                this.txt_largo.Text = vo_pagina.pHeight;

                

                foreach (cls_permiso vo_permiso in vo_pagina.Permisos) 
                {
                    this.marcarNodo(vo_permiso);
                }

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
        /// Método que se encarga
        /// de marcar los permisos
        /// asociados en la página.
        /// </summary>
        /// <param name="po_permiso"></param>
        private void marcarNodo(cls_permiso po_permiso) 
        {
            foreach (TreeNode vo_node in this.trv_permisos.Nodes[0].ChildNodes) 
            {
                if(vo_node.Value.Equals(po_permiso.pPK_permiso.ToString()))
                {
                    vo_node.Checked = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Limpia los valores marcado del treeview
        /// </summary>
        private void limpiarTreeView()
        {
            //Lista de values
            List<string> vo_values = new List<string>();

            //Los nodos marcados se desmarcan.
            foreach (TreeNode nodo in trv_permisos.CheckedNodes)
            {
                vo_values.Add(nodo.Value);
            }

            foreach (string value in vo_values)
            {
                this.marcarDesmarcarNodo(value, this.trv_permisos.Nodes, false);
            }
        }

        /// <summary>
        /// Busca y marca el nodo según su value.
        /// </summary>
        /// <param name="ps_value">String value a buscar.</param>
        /// <returns>true|false</returns>
        private void marcarDesmarcarNodo(string ps_value, TreeNodeCollection po_nodos, bool pb_marcado)
        {
            foreach (TreeNode node in po_nodos)
            {
                if (node.Value == ps_value)
                {
                    node.Checked = pb_marcado;

                    return;
                }
                else
                {
                    marcarDesmarcarNodo(ps_value, node.ChildNodes, pb_marcado);

                }

            }
        }

        /// <summary>
        /// Método que elimina un pagina
        /// </summary>
        /// <param name="po_pagina">Permiso a eliminar</param>
        private void eliminarDatos(cls_pagina po_pagina)
        {
            try
            {
                cls_gestorPagina.deletePagina(po_pagina);

                this.llenarGridView();

                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error eliminando la página. Es posible que exista un registro asociado a este página.", po_exception);
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
            cls_pagina vo_pagina = this.crearObjeto();
            try
            {
                switch (cls_variablesSistema.tipoEstado)
                {
                    case cls_constantes.AGREGAR:
                        vi_resultado = cls_gestorPagina.insertPagina(vo_pagina);
                        break;
                    case cls_constantes.EDITAR:
                        vi_resultado = cls_gestorPagina.updatePagina(vo_pagina);
                        break;
                    default:
                        break;
                }
                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al guardar el registro, verifique que los permisos de la página no se encuentre asociados a un rol.", po_exception);
            }
        }

        /// <summary>
        /// Método que limpia la información
        /// existente en los diferentes
        /// controles del formulario web
        /// </summary>
        private void limpiarCampos()
        {
            this.txt_nombre.Text = String.Empty;
            this.txt_url.Text = String.Empty;
            this.ddl_menu.SelectedIndex = 0;
            this.txt_largo.Text = String.Empty;

            //Se limpia el treeview
            this.limpiarTreeView();
        }

        /// <summary>
        /// Método que habilita o 
        /// deshabilita los controles del
        /// formulario web.
        /// </summary>
        /// <param name="pb_habilitados"></param>
        private void habilitarControles(bool pb_habilitados)
        {
            this.txt_nombre.Enabled = pb_habilitados;
            this.txt_url.Enabled = pb_habilitados;
            this.btn_guardar.Visible = pb_habilitados && (this.pbAgregar || this.pbModificar);
            this.ddl_menu.Enabled = pb_habilitados;

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
        /// Busca un rol según el filtro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <param name="seletecItem"></param>
        protected void ucSearchPagina_searchClick(object sender, EventArgs e, string value, ListItem seletecItem)
        {

            this.llenarGridViewFilter(this.ucSearchPagina.Filter);

        }

        /// <summary>
        /// Agrega un nuevo pagina.
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

                //this.llenarGridView();
                this.llenarGridViewFilter(this.ucSearchPagina.Filter);

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

                this.ucSearchPagina_searchClick(null, null, null, null);
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
        protected void grd_listaPaginas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                this.grd_listaPaginas.PageIndex = e.NewPageIndex;
                //this.llenarGridView();
                this.llenarGridViewFilter(this.ucSearchPagina.Filter);
                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al realizar el listado de páginas.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Cuando se seleccionada un botón del grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grd_listaPaginas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int vi_indice = Convert.ToInt32(e.CommandArgument);

                GridViewRow vu_fila = this.grd_listaPaginas.Rows[vi_indice];

                cls_pagina vo_pagina = new cls_pagina();

                vo_pagina.pPK_pagina = Convert.ToInt32(vu_fila.Cells[0].Text.ToString());
                vo_pagina.pNombre = vu_fila.Cells[0].Text.ToString();
                vo_pagina.pUrl = vu_fila.Cells[0].Text.ToString();

                switch (e.CommandName.ToString())
                {
                    case cls_constantes.VER:
                        vo_pagina = cls_gestorPagina.seleccionarPagina(vo_pagina);

                        cls_variablesSistema.obj = vo_pagina;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.EDITAR:
                        vo_pagina = cls_gestorPagina.seleccionarPagina(vo_pagina);

                        cls_variablesSistema.obj = vo_pagina;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.ELIMINAR:
                        if (!cls_interface.verificarRegistrosPermanentes(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.PAGINA, vo_pagina.pPK_pagina.ToString()))
                        {
                            this.eliminarDatos(vo_pagina);
                        }
                        else
                        {
                            //Se levanta PopUp indicando que no se puede eliminar el registro
                            this.mpe_RegistroPermante.Show();
                            inicializarControles();
                        }
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
        /// Evento que se ejecuta cuando se le da
        /// en el botón permanente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_permanente_Click(object sender, EventArgs e)
        {
            try
            {
                this.mpe_RegistroPermante.Hide();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al cancelar la operación.";
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
                throw new Exception("Ocurrió un error al obtener los permisos del rol en la página actual.", po_exception);
            }
        }

        /// <summary>
        /// Carga los permisos según la página.
        /// </summary>
        private void cargarPermisos()
        {
            this.btn_agregar.Visible = this.pbAgregar;
            this.btn_guardar.Visible = this.pbModificar || this.pbAgregar;
            this.grd_listaPaginas.Columns[3].Visible = this.pbAcceso;
            this.grd_listaPaginas.Columns[4].Visible = this.pbModificar;
            this.grd_listaPaginas.Columns[5].Visible = this.pbEliminar;
        }

        #endregion
    }
}