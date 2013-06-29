using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using COSEVI.CSLA.lib.accesoDatos.App_InterfaceComunes;
using CONS = COSEVI.CSLA.lib.accesoDatos.App_Constantes;
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
// PERSONA 			        MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	03 – 06  - 2011	 	Se crea la clase
// Esteban Ramírez Gónzalez  	26 – 08  - 2011	 	Se crea la clase
// Esteban Ramírez Gónzalez  	05 – 05 - 2012	    Se agrega manejo se seguridad
//								…………………………………………………………
//								…………………………………………………………
// 
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.Administracion
{
    public partial class frw_bitacora : System.Web.UI.Page
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

                    //this.llenarGridView();
                    
                    this.llenarCombosUsuario();
                    this.llenarComboTabla();
                    this.llenarComboAccion();

                    this.txt_fechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    this.txt_fechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                    this.filtrarBitacora();

                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Error al inicializar la consulta de bitácora.";
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
                //this.btn_agregar = (Button)acp_listadoDatos.FindControl("btn_agregar");
                //this.ucSearchBitacora.SearchClick += new COSEVI.web.controls.ucSearch.searchClick(this.ucSearchBitacora_searchClick);
                this.grd_listaBitacora = (GridView)acp_listadoDatos.FindControl("grd_listaBitacora");
                this.ddl_desde = (DropDownList)acp_listadoDatos.FindControl("ddl_desde");
                this.ddl_hasta = (DropDownList)acp_listadoDatos.FindControl("ddl_hasta");
                //Se agregan los filtros.
                //this.agregarItemListFiltro();

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

            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Bitacora", "PK_bitacora"));
            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Departamento", "FK_departamento"));
            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Usuario", "FK_usuario"));
            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Accion", "accion"));
            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Fecha", "fecha_accion"));
            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Numero", "numero_registro"));
            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Tabla", "tabla"));
            //this.ucSearchBitacora.LstCollecction.Add(new ListItem("Maquina", "maquina"));
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Valida los datos de los filtros.
        /// </summary>
        /// <returns></returns>
        public bool validarDatos()
        {
            bool resultado = true;

            if (Convert.ToDateTime(this.txt_fechaInicio.Text) > Convert.ToDateTime(this.txt_fechaFinal.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje", "MostrarMensaje();", true);

                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// Método que filtra la bitácora
        /// </summary>
        private void filtrarBitacora()
        {
            if (this.validarDatos())
            {

                DateTime vd_fechaInicio = Convert.ToDateTime(this.txt_fechaInicio.Text);
                DateTime vd_fechaFinal = Convert.ToDateTime(this.txt_fechaFinal.Text);
                string usuarioDesde = this.ddl_desde.SelectedIndex == 0 ? "A" : this.ddl_desde.SelectedValue;
                string usuarioHasta = this.ddl_hasta.SelectedIndex == 0 ? "Z" : this.ddl_hasta.SelectedValue;
                string tabla = "%" + this.ddl_table.SelectedValue + "%";
                string accion = "%" + this.ddl_accion.SelectedValue + "%";
                string registro = "%" + this.txt_registro.Text + "%";

                this.grd_listaBitacora.DataSource = cls_gestorBitacora.listarBitacoraFiltro(vd_fechaInicio,
                                                                                            vd_fechaFinal,
                                                                                            usuarioDesde,
                                                                                            usuarioHasta,
                                                                                            accion,
                                                                                            tabla, registro);
                this.grd_listaBitacora.DataBind();


                if (this.grd_listaBitacora.Rows.Count == 0)
                {
                    this.mostrarNoDatos();
                }
            }

        }

        /// <summary>
        /// Método que se encarga de
        /// llenar la información 
        /// del filtro de acciones.
        /// </summary>
        private void llenarComboAccion()
        {
            try
            {
                this.ddl_accion.Items.Clear();

                foreach (int value in Enum.GetValues(typeof(CONS.Accion)))
                {
                    this.ddl_accion.Items.Add(new ListItem(Enum.GetName(typeof(CONS.Accion), value), Enum.GetName(typeof(CONS.Accion), value)));
                }
                this.ddl_accion.Items.Insert(0, new ListItem("<Seleccione>", ""));
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar el filtro de acciones.", po_exception);
            }
        }

        /// <summary>
        /// Método que se encarga de
        /// llenar la información 
        /// del filtro de tablas.
        /// </summary>
        private void llenarComboTabla()
        {
            try
            {
                this.ddl_table.Items.Clear();

                foreach (int value in Enum.GetValues(typeof(CONS.Tablas)))
                {
                    this.ddl_table.Items.Add(new ListItem(Enum.GetName(typeof(CONS.Tablas), value), Enum.GetName(typeof(CONS.Tablas), value)));
                }
                this.ddl_table.Items.Insert(0, new ListItem("<Seleccione>", ""));

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar el filtro de tablas.", po_exception);
            }
        }

        /// <summary>
        /// Método que se encarga de 
        /// llenar la información
        /// del filtro de empleados.
        /// </summary>
        private void llenarCombosUsuario()
        {
            try
            {
                this.ddl_desde.DataSource = cls_gestorUsuario.listarUsuarios();
                this.ddl_desde.DataBind();
                this.ddl_hasta.DataSource = cls_gestorUsuario.listarUsuarios();
                this.ddl_hasta.DataBind();

                this.ddl_desde.Items.Insert(0,new ListItem("<Seleccione>",""));
                this.ddl_hasta.Items.Insert(0, new ListItem("<Seleccione>", ""));

            }
            catch(Exception po_exception)
            {
                throw new Exception("Ocurrió un error al llenar los filtros de usuarios.", po_exception);
            }
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
                this.grd_listaBitacora.Columns[0].Visible = true;
                this.grd_listaBitacora.DataSource = cls_gestorBitacora.listarBitacora();
                this.grd_listaBitacora.DataBind();
                this.grd_listaBitacora.Columns[0].Visible = false;

                if (this.grd_listaBitacora.Rows.Count == 0)
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
        public void  mostrarNoDatos()
        {
            List<object> vo_lista = new List<object>();
            vo_lista.Add(new cls_bitacora());
            this.grd_listaBitacora.DataSource = vo_lista;
            this.grd_listaBitacora.DataBind();

            for (int i = 0; i < this.grd_listaBitacora.Columns.Count; i++)
            {
                this.grd_listaBitacora.Rows[0].Cells[i].Text = String.Empty;
            }

            this.grd_listaBitacora.Rows[0].Cells[1].ColumnSpan = this.grd_listaBitacora.Columns.Count;
            this.grd_listaBitacora.Rows[0].Cells[1].Text = "No se encontraron datos.";

        }

        /// <summary>
        /// Hace un buscar de la lista de permisos.
        /// </summary>
        /// <param name="psFilter">String filtro.</param>
        private void llenarGridViewFilter(String psFilter)
        {
            try
            {
                this.grd_listaBitacora.Columns[0].Visible = true;
                this.grd_listaBitacora.DataSource = cls_gestorBitacora.listarBitacoraFiltro(psFilter);
                this.grd_listaBitacora.DataBind();
                this.grd_listaBitacora.Columns[0].Visible = false;

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error llenando la tabla con filtro.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
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
        /// Evento que se ejecuta
        /// cuando se da click al botón de buscar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.filtrarBitacora();
            }
            catch (Exception po_exception)
            {
                this.lanzarExcepcion(po_exception, "Error al filtrar la información de la bitácora.");
            }

        }

        /// <summary>
        /// Busca un rol según el filtro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <param name="seletecItem"></param>
        protected void ucSearchBitacora_searchClick(object sender, EventArgs e, string value, ListItem seletecItem)
        {
            //this.llenarGridViewFilter(this.ucSearchBitacora.Filter); 

        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da
        /// en el botón de cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.habilitarControles(true);

            //    this.limpiarCampos();

            //    this.upd_Principal.Update();

            //    this.ard_principal.SelectedIndex = 0;
            //}
            //catch (Exception po_exception)
            //{
            //    String vs_error_usuario = "Ocurrió un error al cancelar la operación.";
            //    this.lanzarExcepcion(po_exception, vs_error_usuario);
            //} 
        }

        /// <summary>
        /// Cambiar de índice de página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grd_listaBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {          
                this.grd_listaBitacora.PageIndex = e.NewPageIndex;
                this.filtrarBitacora();
                this.upd_Principal.Update();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al realizar el listado de la bitácora.";
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
                throw new Exception("Ocurrió un error al obtener los permisos en la página actual..", po_exception);
            }
        }

        /// <summary>
        /// Carga los permisos según la página.
        /// </summary>
        private void cargarPermisos()
        {
           
        }

        #endregion

       

    }
}