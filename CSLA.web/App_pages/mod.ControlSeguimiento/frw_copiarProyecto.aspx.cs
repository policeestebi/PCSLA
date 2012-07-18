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
using System.Data;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.accesoDatos.mod.Administracion;


// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_copiarProyecto.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA     		           MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	03 – 06  - 2011	 	Se crea la clase
// Cristian Arce Jiménez  	    11 – 27  - 2011	 	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	    01 – 23  - 2012	 	Se agrega el manejo de filtros
// Cristian Arce Jiménez  	    05 – 02  - 2012	 	Se agrega el manejo de filtros
// Cristian Arce Jiménez  	    05 – 04  - 2012	 	Se agrega cambio en las excepciones
// 
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.ControlSeguimiento
{
    public partial class frw_copiarProyecto : System.Web.UI.Page
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

                    //Se envía a cargar por debajo los datos del proyecto que se intenta copiar
                    cargarNombreProyecto();

                    //Se carga el dataset con los estados que puede adquirir un proyecto
                    cargarDataSetEstados();
                    this.ddl_estado.DataBind();

                    //Se cargan los departamentos
                    cargarDataSetDepartamentos();

                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Ocurrió un error al inicializar el copiado de proyectos.";
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
                String vs_error_usuario = "Ocurrió un error durante la inicialización de los controles en el copiado de proyectos.";
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
                //Inicialización de botones generales
                this.btn_cancelar = (Button)acp_edicionDatos.FindControl("btn_cancelar");
                this.btn_guardar = (Button)acp_edicionDatos.FindControl("btn_guardar");

                //Inicialización de botones de la asignación de Departamentos para el Proyecto
                this.btn_asignarDepto = (Button)acp_edicionDatos.FindControl("btn_asignarDepto");
                this.btn_removerDepto = (Button)acp_edicionDatos.FindControl("btn_removerDepto");

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al tratar de inicializar los controles del mantenimiento de proyectos.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Crea un objeto de tipo
        /// cls_proyecto con la informacón
        /// que se encuentra en el formulario web
        /// </summary>
        /// <returns>cls_proyecto</returns>
        private cls_proyecto crearObjetoProyecto()
        {
            cls_proyecto vo_proyecto = new cls_proyecto();
            try
            {
                vo_proyecto.pFK_estado = Convert.ToInt32(ddl_estado.SelectedValue);
                vo_proyecto.pNombre = txt_nombre.Text;
                vo_proyecto.pDescripcion = txt_descripcion.Text;
                vo_proyecto.pObjetivo = txt_objetivo.Text;
                vo_proyecto.pMeta = txt_meta.Text;
                vo_proyecto.pFechaInicio = Convert.ToDateTime(txt_fechaInicio.Text);
                vo_proyecto.pFechaFin = Convert.ToDateTime(txt_fechaFin.Text);
                vo_proyecto.pHorasAsignadas = Convert.ToDecimal(txt_horasAsignadas.Text);
                return vo_proyecto;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el objeto para guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que devuelve los departamentos que han sido recién asociados al proyecto
        /// </summary>
        /// <returns>cls_departamento</returns>
        private List<cls_departamento> departamentosAsociados()
        {
            List<cls_departamento> vo_lista = null;
            cls_departamento vo_departamento = null;

            vo_lista = new List<cls_departamento>();

            try
            {
                //Se recorre los elementos del listbox de departamentos asociados
                foreach (ListItem item in lbx_depasociados.Items)
                {
                    vo_departamento = new cls_departamento();

                    vo_departamento.pPK_departamento = Convert.ToInt32(item.Value);
                    vo_departamento.pNombre = item.Text;

                    vo_lista.Add(vo_departamento);
                }

                return vo_lista;

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al verificar los departamentos asociados al proyecto.", po_exception);
            }           
        }

        /// <summary>
        /// Método que carga la información
        /// de un proyecto.
        /// </summary>
        //private void cargarObjetoProyecto()
        //{
        //    cls_proyecto vo_proyecto = null;

        //    try
        //    {
        //        vo_proyecto = new cls_proyecto();
        //        vo_proyecto = (cls_proyecto)cls_variablesSistema.vs_proyecto;

        //        this.habilitarControles(true);               
        //    }
        //    catch (Exception po_exception)
        //    {
        //        throw new Exception("Ocurrió un error al cargar el registro de proyecto.", po_exception);
        //    }

        //}

        /// <summary>
        /// Guarda la información se que se encuentra
        /// en el formulario Web, ya sea
        /// para ingresar o actualizarla
        /// </summary>
        /// <returns>Int valor devuelvo por el motor de bases de datos</returns>
        private int guardarDatos()
        {
            int vi_resultado = 1;

            try
            {
                cls_proyecto vo_proyecto = this.crearObjetoProyecto();
                cls_departamento vo_dpto = new cls_departamento();
                cls_departamentoProyecto vo_dptoProyecto = new cls_departamentoProyecto();

                List<cls_departamento> vl_departamentoAsociado = departamentosAsociados();
                List<cls_departamentoProyecto> vl_departamentoProyecto = new List<cls_departamentoProyecto>();

                //El proyecto es el mismo para todos los departamentos que se vayan a insertar
                vo_dptoProyecto.pProyecto = vo_proyecto;

                //Se intenta realizar la inserción del proyecto en la tabla correspondiente
                vi_resultado = cls_gestorProyecto.insertProyecto(vo_proyecto);

                //Para cada departamento, se realiza la correspondiente inserción con el proyecto específico
                foreach (cls_departamento vo_departamento in vl_departamentoAsociado)
                {
                    vo_dptoProyecto.pDepartamento = vo_departamento;

                    vi_resultado = cls_gestorDepartamentoProyecto.insertDepartamentoProyecto(vo_dptoProyecto);
                }

                //Se realiza la inserción en las demás tablas, entregable/componente/paquete/actividad, con respecto al proyecto copiado
                vi_resultado = cls_gestorProyecto.insertProyectoCopia(vo_proyecto.pPK_proyecto, cls_variablesSistema.vs_proyecto.pPK_proyecto);

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al guardar el registro de copia del proyecto.", po_exception);
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
                this.ddl_estado.SelectedIndex = -1;
                this.txt_nombre.Text = String.Empty;
                this.txt_descripcion.Text = String.Empty;
                this.txt_objetivo.Text = String.Empty;
                this.txt_meta.Text = String.Empty;
                this.txt_fechaInicio.Text = String.Empty;
                this.txt_fechaFin.Text = String.Empty;
                this.txt_horasAsignadas.Text = String.Empty;
                this.txt_horasReales.Text = String.Empty;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al limpiar los campos del mantenimiento.", po_exception);
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
                this.ddl_estado.Enabled = pb_habilitados;
                this.txt_nombre.Enabled = pb_habilitados;
                this.txt_descripcion.Enabled = pb_habilitados;
                this.txt_objetivo.Enabled = pb_habilitados;
                this.txt_meta.Enabled = pb_habilitados;
                this.txt_fechaInicio.Enabled = pb_habilitados;
                this.txt_fechaFin.Enabled = pb_habilitados;
                this.txt_horasAsignadas.Enabled = pb_habilitados;

                btn_asignarDepto.Enabled = pb_habilitados;
                btn_removerDepto.Enabled = pb_habilitados;

                this.btn_guardar.Visible = pb_habilitados && (this.pbAgregar || this.pbModificar); 
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al habilitar los controles del mantenimiento.", po_exception);
            }
        }

        /// <summary>
        /// Metodo que carga el nombre del proyecto que se escogió para ser copiado
        /// </summary>
        private void cargarNombreProyecto()
        {
            try
            {
                this.txt_proyecto.Text = cls_variablesSistema.vs_proyecto.pNombre;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar el nombre del proyecto.", po_exception);
            }

        }

        /// <summary>
        /// Metodo que carga el dataSet de los estados a los que se puede asociar un proyecto
        /// </summary>
        private void cargarDataSetEstados()
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                vo_dataSet = cls_gestorProyecto.listarEstado();
                this.ddl_estado.DataSource = vo_dataSet;
                this.ddl_estado.DataTextField = vo_dataSet.Tables[0].Columns["descripcion"].ColumnName.ToString();
                this.ddl_estado.DataValueField = vo_dataSet.Tables[0].Columns["PK_estado"].ColumnName.ToString();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los estados del proyecto.", po_exception);
            } 

        }

        /// <summary>
        /// Metodo que carga el dataSet de los departamentos a los que se puede asociar un proyecto
        /// </summary>
        private void cargarDataSetDepartamentos()
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                    vo_dataSet = cls_gestorDepartamentoProyecto.listarDepartamento();
                    lbx_departamentos.DataSource = vo_dataSet;
                    lbx_departamentos.DataTextField = "nombre";
                    lbx_departamentos.DataValueField = "PK_departamento";
                    lbx_departamentos.DataBind();                  
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los departamentos que pueden asociarse al proyecto.", po_exception);
            } 

        }

        /// <summary>
        /// Método que se ejecuta cuando se le da
        /// en el botón de regresar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void regresarMantenimiento()
        {
            try
            {
                //Se redirecciona a la ventana principal de proyectos
                Response.Redirect("frw_proyectos.aspx", false);
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar regresar al mantenimiento de proyeto.";
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

        /// <summary>
        /// Método que limpia la variable del sistema vs_departamentoProyecto
        /// </summary>
        private void limpiarVariablesSistema()
        {
            cls_variablesSistema.vs_proyecto = new cls_proyecto();
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta cuando se 
        /// guarda un nuevo proyecto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.guardarDatos();

                this.limpiarCampos();

                this.limpiarVariablesSistema();

                this.upd_Principal.Update();

                this.ard_principal.SelectedIndex = 0;

                this.regresarMantenimiento();

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
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al cancelar la operación.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento q asigna el nuevo valor del dropdown list de estados cuando se modifica el proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ddl_estado.Text = ((DropDownList)sender).SelectedValue;
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar cambiar el estado del proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }
        #endregion

        #region Asignación Departamento

        /// <summary>
        /// Evento para asignar departamentos a un proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_asignarDepto_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = lbx_departamentos.Items.Count - 1; i >= 0; i--)
                {
                    if (lbx_departamentos.Items[i].Selected == true)
                    {
                        lbx_depasociados.Items.Add(lbx_departamentos.Items[i]);
                        ListItem li = lbx_departamentos.Items[i];
                        lbx_departamentos.Items.Remove(li);
                    }
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar asignar el departamento seleccionado al proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento para remover departamentos a un proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_removerDepto_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = lbx_depasociados.Items.Count - 1; i >= 0; i--)
                {
                    if (lbx_depasociados.Items[i].Selected == true)
                    {
                        lbx_departamentos.Items.Add(lbx_depasociados.Items[i]);
                        ListItem li = lbx_depasociados.Items[i];
                        lbx_depasociados.Items.Remove(li);
                    }
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar remover el departamento seleccionado al proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        #endregion Asignación Departamento

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
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", "alert('Salida'); document.location.href = '../../Default.aspx';", true);
                Response.Redirect("../../Default.aspx");
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
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Salida", "alert('Salida'); document.location.href = '../../Default.aspx';", true);
                Response.Redirect("../../Default.aspx");
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
                //this.btn_guardar.Visible = this.pbModificar || this.pbAgregar;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar cargar los permisos para la página actual..", po_exception);
            }
        }

        #endregion Seguridad
    }
}