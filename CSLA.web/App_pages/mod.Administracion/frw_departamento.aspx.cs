using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using ExceptionManagement.Exceptions;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;

using CSLA.web.App_Variables;
using CSLA.web.App_Constantes;
using System.Data;


// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_departamento.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			           MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	03 – 06  - 2011	 	Se crea la clase
// Esteban Ramírez Gónzalez  	26 – 08  - 2011	 	Se crea la clase
// Cristian Arce Jiménez		01 - 22  - 2012		Modificación en la búsqueda através de filtros
// Cristian Arce Jiménez		01 - 24  - 2012		Modificación en la búsqueda através de filtros y habilitación de campos
// Esteban Ramírez Gónzalez  	01 –  05 - 2012	    Se agrega manejo se seguridad
// 
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.Administracion
{
    public partial class frw_departamento : System.Web.UI.Page
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

            if (!Page.IsPostBack && this.Session["cls_usuario"] != null)
            {
                try
                {    
                    this.obtenerPermisos();
                    this.validarAcceso();
                    this.cargarPermisos();
                    this.llenarGridView();
                    this.cargarDataSetDepartamentos();


                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar el mantenimiento de departamentos.";
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
                String vs_error_usuario = "Error al tratar de inicializar los controles del departamento.";
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
                this.btn_permanente = (Button)pan_mensajeeliminar.FindControl("btn_permanente");

                this.ucSearchDepartamento.SearchClick += new COSEVI.web.controls.ucSearch.searchClick(this.ucSearchDepartamento_searchClick);

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
                this.ucSearchDepartamento.LstCollecction.Add(new ListItem("Nombre", "nombre"));
                this.ucSearchDepartamento.LstCollecction.Add(new ListItem("Ubicacion", "ubicacion"));
                this.ucSearchDepartamento.LstCollecction.Add(new ListItem("Administrador", "administrador"));
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar asignar los filtros para el mantenimiento.", po_exception);
            } 
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Muestra los acordeones dependiendo 
        /// en los valores enviados por parámetro
        /// </summary>
        /// <param name="pbListado"></param>
        /// <param name="pbEdicion"></param>
        private void mostrarAcordeones(bool pbListado, bool pbEdicion)
        {
            this.acp_edicionDatos.Visible = pbEdicion;
            this.acp_listadoDatos.Visible = pbListado;
        }

        /// <summary>
        /// Método que se encarga de 
        /// llenar la información del
        /// grid view
        /// </summary>
        private void llenarGridView()
        {
            try
            {
                this.grd_listaDepartamentos.Columns[0].Visible = true;
                this.grd_listaDepartamentos.DataSource = cls_gestorDepartamento.listarDepartamento();
                this.grd_listaDepartamentos.DataBind();
                this.grd_listaDepartamentos.Columns[0].Visible = false;

                if (this.grd_listaDepartamentos.Rows.Count == 0)
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
            vo_lista.Add(new cls_departamento());
            this.grd_listaDepartamentos.DataSource = vo_lista;
            this.grd_listaDepartamentos.DataBind();

            for (int i = 0; i < this.grd_listaDepartamentos.Columns.Count; i++)
            {
                this.grd_listaDepartamentos.Rows[0].Cells[i].Text = String.Empty;
            }

            this.grd_listaDepartamentos.Rows[0].Cells[2].ColumnSpan = this.grd_listaDepartamentos.Columns.Count;
            this.grd_listaDepartamentos.Rows[0].Cells[2].Text = "No se encontraron datos.";

        }

        /// <summary>
        /// Hace un buscar de la lista de departamentos.
        /// </summary>
        /// <param name="psFilter">String filtro.</param>
        private void llenarGridViewFilter(String psFilter)
        {
            try
            {
                this.grd_listaDepartamentos.Columns[0].Visible = true;
                this.grd_listaDepartamentos.DataSource = cls_gestorDepartamento.listarDepartamentoFiltro(psFilter);
                this.grd_listaDepartamentos.DataBind();
                this.grd_listaDepartamentos.Columns[0].Visible = false;

                if (this.grd_listaDepartamentos.Rows.Count == 0)
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
        /// cls_departamento con la informacón
        /// que se encuentra en el formulario
        /// web
        /// </summary>
        /// <returns>cls_departamento</returns>
        private cls_departamento crearObjeto()
        {
            cls_departamento vo_departamento = new cls_departamento();
            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado != cls_constantes.AGREGAR)
            {
                vo_departamento = (cls_departamento)((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).obj;
            }
            try
            {
                vo_departamento.pFK_departamento = Convert.ToInt32(ddl_departamentoPadre.SelectedValue);
                vo_departamento.pNombre = txt_nombre.Text;
                vo_departamento.pUbicacion = txt_ubicacion.Text;
                vo_departamento.pAdministrador = txt_administrador.Text;
                vo_departamento.pConsecutivo = txt_consecutivo.Text;
                return vo_departamento;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el objeto para guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga la información
        /// de un departamento.
        /// </summary>
        private void cargarObjeto()
        {
            cls_departamento vo_departamento = null;

            try
            {
                vo_departamento = (cls_departamento)((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).obj;
                this.ddl_departamentoPadre.SelectedValue = vo_departamento.pFK_departamento.ToString();
                this.txt_nombre.Text = vo_departamento.pNombre;
                this.txt_ubicacion.Text = vo_departamento.pUbicacion;
                this.txt_administrador.Text = vo_departamento.pAdministrador;
                this.txt_consecutivo.Text = vo_departamento.pConsecutivo;

                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado == cls_constantes.VER)
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
        /// Método que elimina un departamento
        /// </summary>
        /// <param name="po_departamento">Departamento a eliminar</param>
        private void eliminarDatos(cls_departamento po_departamento)
        {
            try
            {
                po_departamento.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                cls_gestorDepartamento.deleteDepartamento(po_departamento);

                this.llenarGridView();

                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error eliminando el departamento. Es posible que exista un registro asociado a este departamento.", po_exception);
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
            cls_departamento vo_departamento = this.crearObjeto();
            vo_departamento.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
            try
            {
                switch (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado)
                {
                    case cls_constantes.AGREGAR:
                        vi_resultado = cls_gestorDepartamento.insertDepartamento(vo_departamento);
                        break;
                    case cls_constantes.EDITAR:
                        vi_resultado = cls_gestorDepartamento.updateDepartamento(vo_departamento);
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
            try
            {
                this.ddl_departamentoPadre.SelectedIndex = -1;
                this.txt_nombre.Text = String.Empty;
                this.txt_ubicacion.Text = String.Empty;
                this.txt_administrador.Text = String.Empty;
                this.txt_consecutivo.Text = String.Empty;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al limpiar los campos del registro.", po_exception);
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
                this.txt_nombre.Enabled = pb_habilitados;
                this.txt_ubicacion.Enabled = pb_habilitados;
                this.txt_administrador.Enabled = pb_habilitados;
                this.txt_consecutivo.Enabled = pb_habilitados;
                this.ddl_departamentoPadre.Enabled = pb_habilitados;
                this.btn_guardar.Visible = pb_habilitados && (this.pbAgregar || this.pbModificar); 
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al habilitar los campos del registro.", po_exception);
            } 
        }

        /// <summary>
        /// Metodo que carga el dataSet de los departamentos a los que se puede asociar un usuario
        /// </summary>
        private void cargarDataSetDepartamentos()
        {
            List<cls_departamento> vo_dataSet = new List<cls_departamento>();

            try
            {
                this.ddl_departamentoPadre.DataSource = cls_gestorDepartamento.listarDepartamento();
                this.ddl_departamentoPadre.DataBind();

                //Se agrega un nuevo un valor por defecto en este caso selecionar 
                this.ddl_departamentoPadre.Items.Insert(0,new ListItem("Seleccione Departamento","0"));

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los los departamentos.", po_exception);
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
        /// Busca un rol según el filtro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <param name="seletecItem"></param>
        protected void ucSearchDepartamento_searchClick(object sender, EventArgs e, string value, ListItem seletecItem)
        {
            try
            {
                this.llenarGridViewFilter(this.ucSearchDepartamento.Filter); 
             }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar realizar la búsqueda filtrada de los registros.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Agrega un nuevo departamento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado = cls_constantes.AGREGAR;

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
        /// guarda un nuevo departamento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.guardarDatos();

                //this.llenarGridView();
                this.llenarGridViewFilter(this.ucSearchDepartamento.Filter); 

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

                this.ucSearchDepartamento_searchClick(null, null, null, null);

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
        protected void grd_listaDepartamentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {        
                this.grd_listaDepartamentos.PageIndex = e.NewPageIndex;
                //this.llenarGridView();
                this.llenarGridViewFilter(this.ucSearchDepartamento.Filter); 
                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al realizar el listado de departamentos.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Cuando se seleccionada un botón del grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grd_listaDepartamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int vi_fkDepartamento = 0;

            try
            {
                int vi_indice = Convert.ToInt32(e.CommandArgument);

                GridViewRow vu_fila = this.grd_listaDepartamentos.Rows[vi_indice];

                cls_departamento vo_departamento = new cls_departamento();

                vo_departamento.pPK_departamento = Convert.ToInt32(vu_fila.Cells[0].Text.ToString());

                if (Int32.TryParse(vu_fila.Cells[1].Text.ToString(), out vi_fkDepartamento))
                {
                    vo_departamento.pFK_departamento = vi_fkDepartamento;
                }
                else
                {
                    vo_departamento.pFK_departamento = 0;
                }

                vo_departamento.pNombre = vu_fila.Cells[2].Text.ToString();
                vo_departamento.pUbicacion = vu_fila.Cells[3].Text.ToString();
                vo_departamento.pAdministrador = vu_fila.Cells[4].Text.ToString();
                vo_departamento.pConsecutivo = vu_fila.Cells[5].Text.ToString();

                switch (e.CommandName.ToString())
                {
                    case cls_constantes.VER:
                        vo_departamento = cls_gestorDepartamento.seleccionarDepartamento(vo_departamento);

                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).obj = vo_departamento;

                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.EDITAR:
                        vo_departamento = cls_gestorDepartamento.seleccionarDepartamento(vo_departamento);

                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).obj = vo_departamento;

                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.ELIMINAR:
                        if (!cls_interface.verificarRegistrosPermanentes(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.DEPARTAMENTO, vo_departamento.pPK_departamento.ToString()))
                        {
                            this.eliminarDatos(vo_departamento);
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida",cls_constantes.SCRIPTLOGOUT, true);
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
                this.grd_listaDepartamentos.Columns[5].Visible = this.pbAcceso;
                this.grd_listaDepartamentos.Columns[6].Visible = this.pbModificar; 
                this.grd_listaDepartamentos.Columns[7].Visible = this.pbEliminar;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar cargar los permisos para la página actual..", po_exception);
            }
        }

        #endregion Seguridad

    }
}