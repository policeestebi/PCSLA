using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
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
// frw_permiso.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			           MES – DIA - AÑO		DESCRIPCIÓN
// Cristian Arce Jiménez  	    27 – 08  - 2011	 	Se crea la clase
// Esteban Ramírez              12-  10  - 2011     Se modifica la clase, se agregan columnas.
// Esteban Ramírez              25-  10  - 2011     Se agrega la tree view del menú del sistema.
// Cristian Arce Jiménez  	    27 – 11  - 2011	 	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	    23 – 01  - 2012	 	Se agrega el manejo de filtros
// Esteban Ramírez Gónzalez  	01 – 05 - 2012	    Se agrega manejo se seguridad
// 
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.Administracion
{
    public partial class frw_rol : System.Web.UI.Page
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
                    this.llenarTreeView();
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar el mantenimiento de roles.";
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

                this.ucSearchRol.SearchClick += new COSEVI.web.controls.ucSearch.searchClick(this.ucSearchRol_searchClick);

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
            this.ucSearchRol.LstCollecction.Add(new ListItem("Nombre", "nombre"));
            this.ucSearchRol.LstCollecction.Add(new ListItem("Descripcion", "descripcion"));
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
                this.grd_listaRoles.Columns[0].Visible = true;
                this.grd_listaRoles.DataSource = cls_gestorRol.listarRoles();
                this.grd_listaRoles.DataBind();
                this.grd_listaRoles.Columns[0].Visible = false;

                if (this.grd_listaRoles.Rows.Count == 0)
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
            vo_lista.Add(new cls_rol());
            this.grd_listaRoles.DataSource = vo_lista;
            this.grd_listaRoles.DataBind();

            for (int i = 0; i < this.grd_listaRoles.Columns.Count; i++)
            {
                this.grd_listaRoles.Rows[0].Cells[i].Text = String.Empty;
            }

            this.grd_listaRoles.Rows[0].Cells[1].ColumnSpan = this.grd_listaRoles.Columns.Count;
            this.grd_listaRoles.Rows[0].Cells[1].Text = "No se encontraron datos.";

        }

        /// <summary>
        /// Hace un buscar de la lista de roles.
        /// </summary>
        /// <param name="psFilter">String filtro.</param>
        private void llenarGridViewFilter(String psFilter)
        {
            try
            {
                this.grd_listaRoles.Columns[0].Visible = true;
                this.grd_listaRoles.DataSource = cls_gestorRol.listarRolFiltro(psFilter);
                this.grd_listaRoles.DataBind();
                this.grd_listaRoles.Columns[0].Visible = false;

                if (this.grd_listaRoles.Rows.Count == 0)
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
        /// Método que se encarga de llenar el tree list del menú
        /// </summary>
        private void llenarTreeView()
        {
            DataTable vo_dt = null;
            IEnumerable<DataRow> vo_row = null;
            TreeNode vo_nodo = null;
            string vs_nombre = string.Empty;

            try
            {
                vo_dt = cls_gestorMenu.seleccionarMenuSistema().Tables[0];

                vo_row = (from menu in vo_dt.AsEnumerable()
                          where menu.Field<Int32>("FK_menuPadre") == 0
                          select menu);

                //Se agregan primeras acciones
                foreach (DataRow row in vo_row)
                {

                    if (row["titulo"].ToString() != vs_nombre)
                    {
                        vo_nodo = new TreeNode(row["titulo"].ToString(), row["PK_menu"].ToString());

                        this.trv_menu.Nodes.Add(vo_nodo);

                        agregarHijosMenu(row["PK_menu"].ToString(), vo_dt, vo_nodo);
                    }

                    vs_nombre = row["titulo"].ToString();

                    //Se agregan los permisos asociados a las páginas

                }

                this.trv_menu.ShowLines = true;
                this.trv_menu.CollapseAll();

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error llenando el árbol.", po_exception);
            }
        }

        /// <summary>
        /// Crea un objeto de tipo
        /// cls_rol con la informacón
        /// que se encuentra en el formulario
        /// web
        /// </summary>
        /// <returns>cls_rol</returns>
        private cls_rol crearObjeto()
        {
            cls_rol vo_rol = new cls_rol();
            if (cls_variablesSistema.tipoEstado != cls_constantes.AGREGAR)
            {
                vo_rol = (cls_rol)cls_variablesSistema.obj;
            }
            try
            {
                vo_rol.pNombre = txt_nombre.Text;
                vo_rol.pDescripcion = txt_descripcion.Text;
                vo_rol.pVisible = this.chk_activo.Checked;

                vo_rol.Paginas = ObtenerPermisos();

                return vo_rol;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el objeto para guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga la información
        /// de un rol.
        /// </summary>
        private void cargarObjeto()
        {
            cls_rol vo_rol = null;

            try
            {
                vo_rol = (cls_rol)cls_variablesSistema.obj;
                this.txt_nombre.Text = vo_rol.pNombre;
                this.txt_descripcion.Text = vo_rol.pDescripcion;
                this.chk_activo.Checked = vo_rol.pVisible;

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
        /// Método que elimina un rol
        /// </summary>
        /// <param name="po_rol">Permiso a eliminar</param>
        private void eliminarDatos(cls_rol po_rol)
        {
            try
            {
                cls_gestorRol.deleteRol(po_rol);

                this.llenarGridView();

                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error eliminando el rol. Es posible que exista un registro asociado a este rol.", po_exception);
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
            cls_rol vo_rol = this.crearObjeto();
            try
            {
                switch (cls_variablesSistema.tipoEstado)
                {
                    case cls_constantes.AGREGAR:
                        vi_resultado = cls_gestorRol.insertRol(vo_rol);
                        break;
                    case cls_constantes.EDITAR:
                        vi_resultado = cls_gestorRol.updateRol(vo_rol);
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
            this.txt_nombre.Text = String.Empty;
            this.txt_descripcion.Text = String.Empty;

            //Se limpia el tree_view
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
            this.txt_descripcion.Enabled = pb_habilitados;
            this.chk_activo.Enabled = pb_habilitados;
            this.btn_guardar.Visible = pb_habilitados && (this.pbAgregar || this.pbModificar); 
        }

        /// <summary>
        /// Agrega los hijos de una opcion de menú.
        /// </summary>
        /// <param name="ps_padre"></param>
        /// <param name="po_datos"></param>
        private void agregarHijosMenu(string ps_padre, DataTable po_datos, TreeNode po_nodo)
        {
            TreeNode vo_nodo = null;
            try
            {
                var vo_row = (from menu in po_datos.AsEnumerable()
                              where menu.Field<Int32>("FK_menuPadre").ToString().Equals(ps_padre) || (
                                    menu.Field<Int32>("PK_menu").ToString().Equals(ps_padre) &&
                                    menu.Field<Int32>("PK_pagina") != 0 &&
                                    menu.Field<Int32>("PK_permiso") != 0 &&
                                    menu.Field<Int32>("FK_menuPadre") == 0)
                              select new
                              {
                                  PKMENU = menu["PK_menu"].ToString(),
                                  MENUPADRE = Convert.ToString(menu["FK_menuPadre"]),
                                  TITULO = menu["titulo"].ToString()

                              }).Distinct();

                //Se agregan los hijos
                foreach (var row in vo_row)
                {

                    vo_nodo = new TreeNode(row.TITULO, row.PKMENU);

                    if (row.MENUPADRE != "0")
                    {
                        po_nodo.ChildNodes.Add(vo_nodo);

                        agregarHijosMenu(row.PKMENU, po_datos, vo_nodo);
                    }
                    else
                    {
                        vo_nodo = po_nodo;
                    }

                    agregarPermisos(row.PKMENU, po_datos, vo_nodo);
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al agregar los registros asociados al menú.", po_exception);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ps_menu"></param>
        /// <param name="po_datos"></param>
        /// <param name="po_nodo"></param>
        private void agregarPermisos(string ps_menu, DataTable po_datos, TreeNode po_nodo)
        {
            TreeNode vo_node = null;

            try
            {
                var vo_row = from menu in po_datos.AsEnumerable()
                             where menu.Field<Int32>("FK_menu").ToString().Equals(ps_menu)
                             select new
                             {
                                 PKPAGINA = Convert.ToString(menu["PK_pagina"]),
                                 NOMBREPERMISO = Convert.ToString(menu["nombre_permiso"]),
                                 PKPERMISO = Convert.ToString(menu["PK_permiso"])
                             };


                foreach (var row in vo_row)
                {
                    vo_node = new TreeNode(row.NOMBREPERMISO, ps_menu + "/" + row.PKPAGINA + "/" + row.PKPERMISO);
                    po_nodo.ChildNodes.Add(vo_node);

                    vo_node.ShowCheckBox = true;
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al agregar los permisos.", po_exception);
            }
        }

        /// <summary>
        /// Obtiene la lista de permisos para el rol
        /// según lo seleccionado en el treeview.
        /// </summary>
        /// <returns></returns>
        private List<cls_pagina> ObtenerPermisos()
        {
            List<cls_pagina> vo_permisos = null;
            cls_pagina vo_pagina = null;
            String[] vo_codigo = null;

            try
            {
                vo_permisos = new List<cls_pagina>();

                foreach (TreeNode nodo in this.trv_menu.CheckedNodes)
                {
                    vo_codigo = nodo.Value.ToString().Split('/');

                    if (vo_codigo != null && vo_codigo.Count() == 3)
                    {
                        vo_pagina = new cls_pagina();

                        vo_pagina.pPK_pagina = Convert.ToInt32(vo_codigo[1]);


                        if (!vo_permisos.Exists(c => c.pPK_pagina == Convert.ToInt32(vo_codigo[1])))
                        {
                            vo_permisos.Add(vo_pagina);
                        }
                        else
                        {
                            vo_pagina = vo_permisos.Find(c => c.pPK_pagina == Convert.ToInt32(vo_codigo[1]));
                        }

                        vo_pagina.Permisos.Add(new cls_permiso() { pPK_permiso = Convert.ToInt32(vo_codigo[2]) });
                    }
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener los permisos del rol.", po_exception);
            }

            return vo_permisos;
        }

        private void cargarPermisosRol(cls_rol poRol)
        {
            List<cls_permiso> vo_permisos = null;
            cls_pagina vo_pagina = null;
            try
            {
                if (poRol.Paginas != null)
                {
                    foreach (cls_pagina pagina in poRol.Paginas)
                    {
                        vo_permisos = pagina.Permisos;

                        vo_pagina = cls_gestorPagina.seleccionarPagina(pagina);

                        vo_pagina.Permisos = vo_permisos;

                        foreach (cls_permiso permiso in vo_pagina.Permisos)
                        {
                            this.marcarDesmarcarNodo(vo_pagina.FK_menu + "/" + vo_pagina.pPK_pagina + "/" + permiso.pPK_permiso, this.trv_menu.Nodes, true);
                        }
                    }
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error cargando los permisos para el rol.", po_exception);
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
        /// Limpia los valores marcado del treeview
        /// </summary>
        private void limpiarTreeView()
        {
            //Colapsa todo el árbol
            this.trv_menu.CollapseAll();
            //Lista de values
            List<string> vo_values = new List<string>();

            //Los nodos marcados se desmarcan.
            foreach (TreeNode nodo in trv_menu.CheckedNodes)
            {
                vo_values.Add(nodo.Value);
            }

            foreach (string value in vo_values)
            {
                this.marcarDesmarcarNodo(value, this.trv_menu.Nodes, false);
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
        /// Marcar o desmarca
        /// todos los nodos
        /// del tree view 
        /// del menú.
        /// </summary>
        /// <param name="pb_marcar"></param>
        private void marcarDesmarcar(bool pb_marcar, TreeNode po_padre)
        {
            TreeNodeCollection nodos = po_padre == null ? this.trv_menu.Nodes : po_padre.ChildNodes;

            foreach (TreeNode nodo in nodos)
            {
                if (nodo.ChildNodes != null &&
                   nodo.ChildNodes.Count > 0)
                {
                    this.marcarDesmarcar(pb_marcar, nodo);
                }
                else
                {
                    nodo.Checked = pb_marcar;
                }
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta
        /// cuando se presiona el botón 
        /// de marcar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_marcar_Click(object sender, EventArgs e)
        {
            this.marcarDesmarcar(true, null);
        }

        /// <summary>
        /// Evento que se ejecuta
        /// cuando se presiona el botón 
        /// de desmarcar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_desmarcar_Click(object sender, EventArgs e)
        {
            this.marcarDesmarcar(false, null);
        }


        /// <summary>
        /// Busca un rol según el filtro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <param name="seletecItem"></param>
        protected void ucSearchRol_searchClick(object sender, EventArgs e, string value, ListItem seletecItem)
        {

            this.llenarGridViewFilter(this.ucSearchRol.Filter);

        }

        /// <summary>
        /// Agrega un nuevo rol.
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

                this.ucSearchRol_searchClick(null, null, null, null);
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
        protected void grd_listaRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.grd_listaRoles.PageIndex = e.NewPageIndex;
                this.llenarGridView();
                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al realizar el listado de roles.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Cuando se seleccionada un botón del grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grd_listaRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int vi_indice = Convert.ToInt32(e.CommandArgument);

                GridViewRow vu_fila = this.grd_listaRoles.Rows[vi_indice];

                cls_rol vo_rol = new cls_rol();

                vo_rol.pPK_rol = Convert.ToInt32(vu_fila.Cells[0].Text.ToString());

                switch (e.CommandName.ToString())
                {
                    case cls_constantes.VER:
                        vo_rol = cls_gestorRol.seleccionarRol(vo_rol);

                        this.cargarPermisosRol(vo_rol);

                        cls_variablesSistema.obj = vo_rol;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.EDITAR:
                        vo_rol = cls_gestorRol.seleccionarRol(vo_rol);

                        this.cargarPermisosRol(vo_rol);

                        cls_variablesSistema.obj = vo_rol;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.ELIMINAR:
                        if (!cls_interface.verificarRegistrosPermanentes(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.ROL, vo_rol.pPK_rol.ToString()))
                        {
                            this.eliminarDatos(vo_rol);
                        }
                        else
                        {
                            //Se levanta PopUp indicando que no se puede eliminar el registro
                            this.mpe_RegistroPermante.Show();
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
            this.btn_agregar.Visible = this.pbAgregar;
            this.btn_guardar.Visible = this.pbModificar || this.pbAgregar;
            this.grd_listaRoles.Columns[3].Visible = this.pbAcceso;
            this.grd_listaRoles.Columns[4].Visible = this.pbModificar;
            this.grd_listaRoles.Columns[5].Visible = this.pbEliminar;
        }

        #endregion


    }
}