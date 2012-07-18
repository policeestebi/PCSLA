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
// frw_usuarios.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			            MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	 03 –  06 - 2011	 	Se crea la clase
// Cristian Arce Jiménez    	 24 -  10 - 2011		Se realizan modificaciones en el listar y editar
// Cristian Arce Jiménez  	     27 – 11  - 2011	 	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	     23 – 01  - 2012	 	Se agrega el manejo de filtros
// Esteban Ramírez Gónzalez  	05 – 05 - 2012	    Se agrega manejo se seguridad
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.Administracion
{
    public partial class frw_usuario : System.Web.UI.Page
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
                    String vs_error_usuario = "Error al inicializar el mantenimiento de usuarios.";
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

                this.ucSearchUsuario.SearchClick +=new COSEVI.web.controls.ucSearch.searchClick(this.ucSearchUsuario_searchClick);
                this.btn_guardar.Click += this.btn_guardar_Click; 

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
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Usuario", "PK_usuario"));
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Rol", "FK_rol"));
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Nombre", "nombre"));
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Apellido1", "apellido1"));
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Apellido2", "apellido2"));
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Activo", "activo"));
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Email", "email"));
            this.ucSearchUsuario.LstCollecction.Add(new ListItem("Puesto", "puesto"));
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
                this.grd_listaUsuarios.Columns[5].Visible = true;
                this.grd_listaUsuarios.Columns[10].Visible = true;
                this.grd_listaUsuarios.Columns[11].Visible = true;
                this.grd_listaUsuarios.DataSource = cls_gestorUsuario.listarUsuarios();
                cargarDataSetRoles();
                cargarDataSetDepartamentos();
                this.grd_listaUsuarios.DataBind();
                this.ddl_rol.DataBind();
                this.ddl_departamento.DataBind();
                this.grd_listaUsuarios.Columns[5].Visible = false;
                this.grd_listaUsuarios.Columns[10].Visible = false;
                this.grd_listaUsuarios.Columns[11].Visible = false;

                if (this.grd_listaUsuarios.Rows.Count == 0)
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
            vo_lista.Add(new cls_usuario());
            this.grd_listaUsuarios.DataSource = vo_lista;
            this.grd_listaUsuarios.DataBind();

            for (int i = 0; i < this.grd_listaUsuarios.Columns.Count; i++)
            {
                this.grd_listaUsuarios.Rows[0].Cells[i].Text = String.Empty;
            }

            this.grd_listaUsuarios.Rows[0].Cells[0].ColumnSpan = this.grd_listaUsuarios.Columns.Count;
            this.grd_listaUsuarios.Rows[0].Cells[0].Text = "No se encontraron datos.";

        }

        /// <summary>
        /// Hace un buscar de la lista de usuarios.
        /// </summary>
        /// <param name="psFilter">String filtro.</param>
        private void llenarGridViewFilter(String psFilter)
        {
            try
            {
                this.grd_listaUsuarios.Columns[5].Visible = true;
                this.grd_listaUsuarios.Columns[10].Visible = true;
                this.grd_listaUsuarios.Columns[11].Visible = true;
                this.grd_listaUsuarios.DataSource = cls_gestorUsuario.listarUsuarioFiltro(psFilter);
                this.grd_listaUsuarios.DataBind();
                this.grd_listaUsuarios.Columns[5].Visible = false;
                this.grd_listaUsuarios.Columns[10].Visible = false;
                this.grd_listaUsuarios.Columns[11].Visible = false;


                if (this.grd_listaUsuarios.Rows.Count == 0)
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
        /// cls_usuario con la informacón
        /// que se encuentra en el formulario
        /// web
        /// </summary>
        /// <returns>cls_usuario</returns>
        private cls_usuario crearObjeto()
        {
            cls_usuario vo_usuario = new cls_usuario();
            if (cls_variablesSistema.tipoEstado != cls_constantes.AGREGAR)
            {
                vo_usuario = (cls_usuario)cls_variablesSistema.obj;
            }
            try
            {
                vo_usuario.pPK_usuario = txt_usuario.Text;
                vo_usuario.pNombre = txt_nombre.Text;
                vo_usuario.pContrasena = cls_MD5.GetPassword(txt_usuario.Text, txt_contrasena.Text);
                vo_usuario.pApellido1 = txt_apellido1.Text;
                vo_usuario.pApellido2 = txt_apellido2.Text;
                vo_usuario.pFK_rol = Convert.ToInt32(ddl_rol.SelectedValue);
                vo_usuario.pPuesto = txt_puesto.Text;
                vo_usuario.pActivo = chk_activo.Checked;
                vo_usuario.pEmail = txt_email.Text;
                vo_usuario.pFK_departamento = Convert.ToInt32(ddl_departamento.SelectedValue);

                return vo_usuario;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el objeto para guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga la información
        /// de un usuarios.
        /// </summary>
        private void cargarObjeto()
        {
            cls_usuario vo_usuario = null;

            try
            {
                vo_usuario = (cls_usuario)cls_variablesSistema.obj;
                this.txt_usuario.Text = vo_usuario.pPK_usuario;
                this.txt_nombre.Text = vo_usuario.pNombre;
                this.txt_contrasena.Text = vo_usuario.pContrasena;
                this.txt_confirmarContrasena.Text = vo_usuario.pContrasena; 
                this.txt_apellido1.Text = vo_usuario.pApellido1;
                this.txt_apellido2.Text = vo_usuario.pApellido2;
                this.ddl_rol.SelectedValue = vo_usuario.pFK_rol.ToString();
                this.txt_puesto.Text = vo_usuario.pPuesto;
                this.chk_activo.Checked = vo_usuario.pActivo;
                this.txt_email.Text = vo_usuario.pEmail;
                this.ddl_departamento.SelectedValue = vo_usuario.pFK_departamento.ToString();

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
        /// Método que elimina un usuarios
        /// </summary>
        /// <param name="po_usuario">usuarios a eliminar</param>
        private void eliminarDatos(cls_usuario po_usuario)
        {
            try
            {
                cls_gestorUsuario.deleteUsuario(po_usuario);

                this.llenarGridView();

                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error eliminando el usuario. Es posible que exista un registro asociado a este usuario.", po_exception);
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
            cls_usuario vo_usuario = this.crearObjeto();
            try
            {
                switch (cls_variablesSistema.tipoEstado)
                {
                    case cls_constantes.AGREGAR:
                        vi_resultado = cls_gestorUsuario.insertUsuario(vo_usuario);
                        break;
                    case cls_constantes.EDITAR:
                        vi_resultado = cls_gestorUsuario.updateUsuario(vo_usuario);
                        break;
                    default:
                        break;
                }
                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al guardar el registro. El posible que el usuario ingresado ya exista en el sistema.", po_exception);
            } 
        }

        /// <summary>
        /// Guarda la información se que se encuentra
        /// en el formulario Web para actualizar la contraseña a un usuario
        /// </summary>
        /// <returns>Int valor devuelvo por el motor de bases de datos</returns>
        private int cambiarContrasena()
        {
            int vi_resultado = 1;
            cls_usuario vo_usuario = new cls_usuario();
            vo_usuario.pPK_usuario = txt_usuariocambio.Text.ToString();
            vo_usuario.pContrasena = cls_MD5.GetPassword(txt_usuariocambio.Text, txt_nuevopassword.Text);
            try
            {
                vi_resultado = cls_gestorUsuario.updateContrasenaUsuario(vo_usuario);

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al modificar la contraseña.", po_exception);
            }
        }

        /// <summary>
        /// Método que limpia la información
        /// existente en los diferentes
        /// controles del formulario web
        /// </summary>
        private void limpiarCampos()
        {
            this.txt_usuario.Text = String.Empty;
            this.txt_nombre.Text = String.Empty;
            this.txt_contrasena.Text = String.Empty;
            this.txt_apellido1.Text = String.Empty;
            this.txt_apellido2.Text = String.Empty;
            this.ddl_rol.SelectedIndex = -1;
            this.txt_puesto.Text = String.Empty;
            this.chk_activo.Checked = false;
            this.txt_email.Text = String.Empty;
            this.ddl_departamento.SelectedIndex = -1;
        }

        /// <summary>
        /// Método que habilita o 
        /// deshabilita los controles del
        /// formulario web.
        /// </summary>
        /// <param name="pb_habilitados"></param>
        private void habilitarControles(bool pb_habilitados)
        {
            if (cls_variablesSistema.tipoEstado == cls_constantes.AGREGAR)
            { this.txt_usuario.Enabled = true; }
            else
            { this.txt_usuario.Enabled = false; }
            this.txt_nombre.Enabled = pb_habilitados;
            this.txt_contrasena.Enabled = pb_habilitados;
            this.txt_repetirpassword.Enabled = pb_habilitados;
            this.txt_apellido1.Enabled = pb_habilitados;
            this.txt_apellido2.Enabled = pb_habilitados;
            this.ddl_rol.Enabled = pb_habilitados;
            this.txt_puesto.Enabled = pb_habilitados;
            this.chk_activo.Enabled = pb_habilitados;
            this.txt_email.Enabled = pb_habilitados;
            this.btn_guardar.Visible = pb_habilitados  && (this.pbAgregar || this.pbModificar);
            this.ddl_departamento.Enabled = pb_habilitados;
        }

        /// <summary>
        /// Metodo que carga el dataSet de los roles a los que se puede asociar un usuario
        /// </summary>
        private void cargarDataSetRoles()
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                vo_dataSet = cls_gestorUsuario.listarRolesUsuario();
                this.ddl_rol.DataSource = vo_dataSet;
                this.ddl_rol.DataTextField = vo_dataSet.Tables[0].Columns["nombre"].ColumnName.ToString();
                this.ddl_rol.DataValueField = vo_dataSet.Tables[0].Columns["PK_rol"].ColumnName.ToString();   
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los roles del usuario.", po_exception);
            }

        }

        /// <summary>
        /// Metodo que carga el dataSet de los departamentos a los que se puede asociar un usuario
        /// </summary>
        private void cargarDataSetDepartamentos()
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                vo_dataSet = cls_gestorUsuario.listarDepartamentosUsuario();
                this.ddl_departamento.DataSource = vo_dataSet;
                this.ddl_departamento.DataTextField = vo_dataSet.Tables[0].Columns["nombre"].ColumnName.ToString();
                this.ddl_departamento.DataValueField = vo_dataSet.Tables[0].Columns["PK_departamento"].ColumnName.ToString();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los departamentos para el usuario.", po_exception);
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
        /// Método que habilita o 
        /// deshabilita el campo para contraseña
        /// </summary>
        /// <param name="pb_habilitados"></param>
        private void habilitarContrasena(bool pb_habilitados)
        {
            this.lbl_contrasena.Visible = pb_habilitados;

            this.txt_contrasena.Visible = pb_habilitados;

            this.rfv_contrasena.Visible = pb_habilitados;

            this.rfv_contrasena.Enabled = pb_habilitados;

            this.lbl_confirmarContrasena.Visible = pb_habilitados;

            this.txt_confirmarContrasena.Visible = pb_habilitados;

            this.rfv_confirmarContrasena.Visible = pb_habilitados;

            this.rfv_confirmarContrasena.Enabled = pb_habilitados;

            this.cpv_contrasena.Visible = pb_habilitados;

            this.cpv_contrasena.Enabled = pb_habilitados;
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
        protected void ucSearchUsuario_searchClick(object sender, EventArgs e, string value, ListItem seletecItem)
        {
            this.llenarGridViewFilter(this.ucSearchUsuario.Filter); 
        }

        /// <summary>
        /// Agrega un nuevo usuario.
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

                this.habilitarContrasena(true);

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
                //!String.IsNullOrEmpty(this.txt_contrasena.Text) && !String.IsNullOrEmpty(this.txt_confirmarContrasena.Text) && 
                if (this.txt_contrasena.Text == this.txt_confirmarContrasena.Text)
                {
                    this.guardarDatos();

                    this.llenarGridView();

                    this.limpiarCampos();

                    this.upd_Principal.Update();

                    this.ard_principal.SelectedIndex = 0;

                    this.habilitarContrasena(false);
                }
                else
                { 
                    
                }

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

                this.ucSearchUsuario_searchClick(null, null, null, null);
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
        protected void grd_listaUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {   
                this.grd_listaUsuarios.PageIndex = e.NewPageIndex;
                this.llenarGridView();
                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al realizar el listado de usuarios.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Cuando se seleccionada un botón del grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grd_listaUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int vi_indice = Convert.ToInt32(e.CommandArgument);

                //this.grd_listaUsuarios.Columns[5].Visible = true;
                GridViewRow vu_fila = this.grd_listaUsuarios.Rows[vi_indice];

                cls_usuario vo_usuario = new cls_usuario();

                vo_usuario.pPK_usuario = vu_fila.Cells[0].Text.ToString();
                vo_usuario.pNombre = vu_fila.Cells[1].Text.ToString();
                vo_usuario.pContrasena = vu_fila.Cells[2].Text.ToString();
                vo_usuario.pApellido1 = vu_fila.Cells[3].Text.ToString();
                vo_usuario.pApellido2 = vu_fila.Cells[4].Text.ToString();
                vo_usuario.pFK_rol = Convert.ToInt32(vu_fila.Cells[5].Text.ToString());
                vo_usuario.pNombreRol = vu_fila.Cells[6].Text.ToString();
                vo_usuario.pPuesto = vu_fila.Cells[7].Text.ToString();
                CheckBox chkBox = new CheckBox();
                chkBox = (CheckBox)(grd_listaUsuarios.Rows[vi_indice].Cells[8].Controls[0]);
                vo_usuario.pActivo = chkBox.Checked;
                vo_usuario.pEmail = vu_fila.Cells[9].Text.ToString();
                vo_usuario.pFK_departamento = Convert.ToInt32(vu_fila.Cells[10].Text.ToString());

                switch (e.CommandName.ToString())
                {
                    case cls_constantes.VER:
                        vo_usuario = cls_gestorUsuario.seleccionarUsuario(vo_usuario);

                        cls_variablesSistema.obj = vo_usuario;

                        cls_variablesSistema.tipoEstado = e.CommandName;


                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.EDITAR:
                        vo_usuario = cls_gestorUsuario.seleccionarUsuario(vo_usuario);

                        cls_variablesSistema.obj = vo_usuario;

                        cls_variablesSistema.tipoEstado = e.CommandName;

                        this.cargarObjeto();

                        this.ard_principal.SelectedIndex = 1;
                        break;

                    case cls_constantes.ELIMINAR:
                        if (!cls_interface.verificarRegistrosPermanentes(COSEVI.CSLA.lib.accesoDatos.App_Constantes.cls_constantes.USUARIO, vo_usuario.pPK_usuario.ToString()))
                        {
                            this.eliminarDatos(vo_usuario);
                        }
                        else
                        {
                            //Se levanta PopUp indicando que no se puede eliminar el registro
                            this.mpe_RegistroPermante.Show();
                        }
                        break;

                    case cls_constantes.CAMBIAR:
                        // Muestra el modalpopupextender
                        this.txt_usuariocambio.Text = vo_usuario.pPK_usuario;
                        this.mpe_CambioPassword.Show();

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
        /// Evento q asigna el nuevo valor del dropdown list de departamentos cuando se modifica el usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddl_departamento.Text = ((DropDownList)sender).SelectedValue;
        }

        /// <summary>
        /// Evento q asigna el nuevo valor del dropdown list de roles cuando se modifica el usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddl_rol.Text = ((DropDownList)sender).SelectedValue;
        }


        /// <summary>
        /// Cambia el password a un usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_password_Click(object sender, EventArgs e)
        {

            try
            {
                //Si los passwords son iguales, y no están vacíos
                if (!String.IsNullOrEmpty(this.txt_nuevopassword.Text) && !String.IsNullOrEmpty(this.txt_repetirpassword.Text) && (this.txt_nuevopassword.Text == this.txt_repetirpassword.Text))
                {
                    this.cambiarContrasena();

                    this.upd_Principal.Update();

                    this.ard_principal.SelectedIndex = 0;

                    this.habilitarContrasena(false);
                }
                else
                {

                }

                //Luego de realizar el proceso, se esconde el popUp
                mpe_CambioPassword.Hide();

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar cambiar la contraseña del usuario.";
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
            this.grd_listaUsuarios.Columns[10].Visible = this.pbAcceso;
            this.grd_listaUsuarios.Columns[11].Visible = this.pbEliminar;
            this.grd_listaUsuarios.Columns[12].Visible = this.pbModificar;
            this.grd_listaUsuarios.Columns[13].Visible = this.pbModificar;
        }

        #endregion

    }
}