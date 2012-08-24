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
using System.Data;


// =========================================================================
// COSEVI - Consejo de Seguridad Vial. - 2011
// Sistema CSLA (Sistema para el Control y Seguimiento de Labores)
//
// frw_actividades.aspx.cs
//
// Explicación de los contenidos del archivo.
// =========================================================================
// Historial
// PERSONA 			           MES – DIA - AÑO		DESCRIPCIÓN
// Esteban Ramírez Gónzalez  	03 – 06  - 2011	 	Se crea la clase
// Cristian Arce Jiménez  	    11 – 27  - 2011	 	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	    01 – 23  - 2012	 	Se agrega el manejo de filtros
// Cristian Arce Jiménez  	    05 – 01  - 2012	 	Se agregan cambios en la lógica para la asignación de actividades
// 
//								
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.ControlSeguimiento
{
    public partial class frw_asignacionActividad : System.Web.UI.Page
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
            try
            { 
                this.validarSession();

                if (!Page.IsPostBack)
                {
                      
                        this.validarAcceso();
                        this.inicializarRegistros();
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Error al inicializar el mantenimiento para la asignación de actividades.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
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
                String vs_error_usuario = "Error al inicializar los controles del mantenimiento para la asignación de actividades.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        ///Método que inicializa los controles 
        ///que se encuentra dentro de los acordeones
        ///esto porque tienen a perderse cuando 
        ///la página se inicializa.
        /// </summary>
        private void inicializarControles()
        {
            try
            {
                
                //Inicializacón de botón de asignación
                this.btn_asignarUsuario = (Button)acp_edicionDatos.FindControl("btn_asignarUsuario");
                this.btn_removerUsuario = (Button)acp_edicionDatos.FindControl("btn_removerUsuario");

                //Botones comunes
                this.btn_regresar = (Button)acp_edicionDatos.FindControl("btn_regresar");
                this.btn_guardar = (Button)acp_edicionDatos.FindControl("btn_guardar");

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error inicializando los controles del mantenimiento.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que se encarga de 
        /// inicializar los registros que 
        /// pertenecen al proyecto seleccionado
        /// </summary>
        private void inicializarRegistros()
        {
            try
            {
                //Nombre del proyecto seleccionado
                cargarNombreProyecto();
                //Paquetes que están asociados al proyecto seleccionado
                cargarSourcePaquetes();
                //Se carga el dataset con los estados que puede adquirir una actividad
                cargarDataSetEstados();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error inicializando los registros del mantenimiento.", po_exception);
            }
        }

        /// <summary>
        /// Nombre del proyecto seleccionado
        /// </summary>
        private void cargarNombreProyecto()
        {
            try
            {
                txt_proyecto.Text = cls_variablesSistema.vs_proyecto.pNombre;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error asignando el nombre del proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Paquetes que están asociados al proyecto seleccionado
        /// </summary>
        private void cargarSourcePaquetes()
        {
            try
            {
                this.ddl_paquete.DataSource = cls_gestorAsignacionActividad.listarPaquetesProyecto(cls_variablesSistema.vs_proyecto.pPK_proyecto);
                this.ddl_paquete.DataTextField = "pNombre";
                this.ddl_paquete.DataValueField = "pPK_Paquete";
                this.ddl_paquete.DataBind();

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error cargando los paquetes asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Metodo que carga el dataSet de los estados que presenta una actividad asignada
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
                this.ddl_estado.DataBind();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error cargando los estados para las actividades asociadas al proyecto.", po_exception);
            }

        }

        /// <summary>
        /// Se carga la lista con la totalidad de usuarios que pueden ser asignados a una actividad
        /// </summary>
        private void cargarUsuarios()
        {
            try
            {
                /*
                 NOta: * Revisar los selects de los listar, para ver que tanto es necesario cambiar los "pNombre" por los nombres de la tabla => "pNombre" - "pNombreUsuario"
                       * Ver si es relevante cambiar los nombres a los listbox
                 */
                lbx_usuarios.DataSource = cls_gestorUsuario.listarUsuarios();
                lbx_usuarios.DataTextField = "pNombre";
                lbx_usuarios.DataValueField = "pPK_usuario";
                lbx_usuarios.DataBind();

                //Si se devuelven usuarios asociados, se remueven los mismos del listBox que mantiene la totalidad de usuarios, esto para evitar la 
                //duplicidad del mismo usuario a una misma actividad
                if (lbx_usuariosAsociados.Items.Count > 0)
                {
                    foreach (ListItem item in lbx_usuariosAsociados.Items)
                    {
                        lbx_usuarios.Items.Remove(item);
                    }
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error cargando la lista de usuarios.", po_exception);
            }
        }

        /// <summary>
        /// Método para cargar las actividades que están asociadas al paquete seleccionado
        /// </summary>
        private void cargarActividadesPorPaquete(int pi_paquete)
        {
            try
            {
                cls_variablesSistema.vs_proyecto.pActividadesPaqueteLista = cls_gestorAsignacionActividad.listarActividadesPorPaquete(cls_variablesSistema.vs_proyecto.pPK_proyecto, pi_paquete);

                lbx_actividades.DataSource = cls_variablesSistema.vs_proyecto.pActividadesPaqueteLista;
                lbx_actividades.DataTextField = "pNombreActividad";
                lbx_actividades.DataValueField = "pPK_Actividad";
                lbx_actividades.DataBind();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error cargando las actividades asociadas al paquete.", po_exception);
            }
        }

        /// <summary>
        /// Método para cargar una actividad asignada de manera específica, enviando el proyecto, paquete y actividad 
        /// </summary>
        /// <param name="pi_paquete"></param>
        private void cargarAsignacionActividad(cls_paqueteActividad po_paqueteActividad)
        {
            try
            {
                //Se crean 2 variables para evitar que la asignación de una única presente inconcistencia de punteros a la hora de comparar y eliminar
                //registro  de memoria con registros de la lista de base de datos (en el evento para remover usuarios del listbox se presenta la inconsistencia de puntero)
                cls_asignacionActividad vo_asignacionActividadMemoria = new cls_asignacionActividad();
                cls_asignacionActividad vo_asignacionActividadBaseDatos = new cls_asignacionActividad();

                //La variables se cargan con el registro de base de datos
                vo_asignacionActividadMemoria = cls_gestorAsignacionActividad.seleccionarAsignacionActividad(po_paqueteActividad);
                vo_asignacionActividadBaseDatos = cls_gestorAsignacionActividad.seleccionarAsignacionActividad(po_paqueteActividad);

                //Se verifica si la consulta de base de datos devolvió algún registro válido, de lo contrario no se debe registrar ni en la lista de memoria 
                //ni en la de base de datos
                if (vo_asignacionActividadMemoria.pPK_Proyecto == cls_variablesSistema.vs_proyecto.pPK_proyecto)
                {
                    //De no encontrarse ya en la lista de base de datos, se agrega
                    if (cls_variablesSistema.vs_proyecto.pAsignacionActividadListaBaseDatos.Where(searchLinQ => searchLinQ.pPK_Actividad == vo_asignacionActividadBaseDatos.pPK_Actividad &&
                                                                                                          searchLinQ.pPK_Paquete == vo_asignacionActividadBaseDatos.pPK_Paquete).Count() == 0)
                    {
                        cls_variablesSistema.vs_proyecto.pAsignacionActividadListaBaseDatos.Add(vo_asignacionActividadBaseDatos);
                    }
                    //Se verifica si existe en la lista de memoria para agregarlo en la variable objeto local y en la misma lista
                    if (cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Actividad == vo_asignacionActividadMemoria.pPK_Actividad &&
                                                                                                        searchLinQ.pPK_Paquete == vo_asignacionActividadMemoria.pPK_Paquete).Count() == 0)
                    {
                        cls_variablesSistema.obj = vo_asignacionActividadMemoria;
                        cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Add(vo_asignacionActividadMemoria);
                    }
                    else
                    {
                        //Si la asigancion ya ha sido leída, se carga la variable objeto con la que se encuentra en memoria
                        vo_asignacionActividadMemoria = (cls_asignacionActividad)cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Find(searchLinQ => searchLinQ.pPK_Actividad == vo_asignacionActividadMemoria.pPK_Actividad &&
                                                                                                                                                        searchLinQ.pPK_Paquete == vo_asignacionActividadMemoria.pPK_Paquete);
                        cls_variablesSistema.obj = vo_asignacionActividadMemoria;
                    }

                    //Se encuentre la asignació ya en memoria, o no, se envía a cargar la información del registro para cargar los campos de la ventana
                    cargarObjeto();

                }

                //Si la actividad ya se encuentra en memoria, se procede a asignar a los usuarios asignados
                if (cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Actividad == po_paqueteActividad.pPK_Actividad &&
                                                                                                    searchLinQ.pPK_Paquete == po_paqueteActividad.pPK_Paquete).Count() > 0)
                {
                    vo_asignacionActividadMemoria = (cls_asignacionActividad)cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Find(searchLinQ => searchLinQ.pPK_Actividad == po_paqueteActividad.pPK_Actividad &&
                                                                                                                                                   searchLinQ.pPK_Paquete == po_paqueteActividad.pPK_Paquete);
                    cls_variablesSistema.obj = vo_asignacionActividadMemoria;
                    
                    cargarObjeto();

                    lbx_usuariosAsociados.DataSource = vo_asignacionActividadMemoria.pUsuarioLista;
                    lbx_usuariosAsociados.DataTextField = "pNombre";
                    lbx_usuariosAsociados.DataValueField = "pPK_usuario";
                    lbx_usuariosAsociados.DataBind();
                }
                //Si no se encuentra en memoria es porque esa actividad aún no se encuentra asignada, por lo que sólo se procede a cargar la llave primaria de la actividad
                //por si se va a realizar la asignación en ese momento
                else
                {
                    vo_asignacionActividadMemoria = cls_gestorAsignacionActividad.listarActividadesPorPaquete(po_paqueteActividad.pPK_Proyecto, po_paqueteActividad.pPK_Paquete, po_paqueteActividad.pPK_Actividad);

                    cls_variablesSistema.obj = vo_asignacionActividadMemoria;

                    lbx_usuariosAsociados.DataSource = vo_asignacionActividadMemoria.pUsuarioLista;
                    lbx_usuariosAsociados.DataTextField = "pNombre";
                    lbx_usuariosAsociados.DataValueField = "pPK_usuario";
                    lbx_usuariosAsociados.DataBind();

                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error cargando la información para la actividad seleccionada.", po_exception);
            }
        }

        /// <summary>
        /// Crea un objeto de tipo
        /// cls_asignacionActividad con la informacón
        /// que se encuentra en memoria y la complementa 
        /// con la información presente en el formulario web
        /// </summary>
        /// <returns>cls_asignacionActividad</returns>
        private cls_asignacionActividad crearObjeto()
        {
            cls_asignacionActividad vo_asignacionActividad = new cls_asignacionActividad();

            try
            {
                //Se asignan los datos que conforman la llave primaria al objeto
                vo_asignacionActividad = (cls_asignacionActividad)cls_variablesSistema.obj;

                vo_asignacionActividad.pDescripcion = txt_descripcion.Text;
                vo_asignacionActividad.pFechaInicio = Convert.ToDateTime(txt_fechaInicio.Text);
                vo_asignacionActividad.pFechaFin = Convert.ToDateTime(txt_fechaFin.Text);
                vo_asignacionActividad.pHorasAsignadas = Convert.ToDecimal(txt_horasAsignadas.Text);
                vo_asignacionActividad.pEstado.pPK_estado = Convert.ToInt32(ddl_estado.SelectedValue);

                return vo_asignacionActividad;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear el objeto para guardar el registro.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga la información
        /// de del objeto a memoria.
        /// </summary>
        private void cargarObjeto()
        {
            cls_asignacionActividad vo_actividad = null;

            try
            {
                //Se carga el objeto que se encuentra asignado en memoria
                vo_actividad = (cls_asignacionActividad)cls_variablesSistema.obj;

                this.txt_descripcion.Text = vo_actividad.pDescripcion;
                this.txt_fechaInicio.Text = vo_actividad.pFechaInicio.ToShortDateString();
                this.txt_fechaFin.Text = vo_actividad.pFechaFin.ToShortDateString(); ;
                this.txt_horasAsignadas.Text = vo_actividad.pHorasAsignadas.ToString();
                this.txt_horasReales.Text = vo_actividad.pHorasReales.ToString();
                this.ddl_estado.SelectedValue = vo_actividad.pFK_Estado == 0 ? "1" : vo_actividad.pFK_Estado.ToString();

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
                throw new Exception("Ocurrió un error al cargar el registro de la lista de memoria.", po_exception);
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

            //Se obtiene la llave primaria de proyecto, es decir, PK_proyecto, de la variables del sistema, para luego sólo enviarla por parámetro en los guardar
            int llaveProyecto = cls_variablesSistema.vs_proyecto.pPK_proyecto;

            try
            {
                asignarActividades(llaveProyecto);
                
                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al guardar el registro.", po_exception);
            } 
        }

        /// <summary>
        /// Método que verifica las actividades que se van a asignar para su posterior inserción/modificación en base de datos
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int asignarActividades(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_asignacionActividad> vl_actividadAsignadaMemoria = this.crearAsignacionActividadMemoria();
            List<cls_asignacionActividad> vl_actividadAsignadaBaseDatos = this.crearAsignacionActividadBaseDatos();

            try
            {
                //Se recorre toda la lista de actividades recién asignadas en memoria
                foreach (cls_asignacionActividad vo_actividadAsignada in vl_actividadAsignadaMemoria)
                {
                    //Lo que importa es la asignación de cada uno de los usuarios que se han agregado/modificado en memoria
                    foreach (cls_usuario vo_usuario in vo_actividadAsignada.pUsuarioLista)
                    {
                        vo_actividadAsignada.pPK_Proyecto = ps_llaveProyecto;
                        vo_actividadAsignada.pUsuarioPivot = vo_usuario.pPK_usuario;
                        //Por cada usuario, se envía a realizar la inserción/modificación del mismo, el store procedure indica la operación a realizar
                        vi_resultado = cls_gestorAsignacionActividad.updateAsignacionActividad(vo_actividadAsignada, 1);
                    }
                }

                //Se recorre toda la lista de actividades que se obtuvieron desde un principio a nivel de base de datos
                foreach (cls_asignacionActividad vo_actividadAsignada in vl_actividadAsignadaBaseDatos)
                {
                    //Lo que importa es la asignación de cada uno de los usuarios que se han agregado/modificado en memoria
                    foreach (cls_usuario vo_usuario in vo_actividadAsignada.pUsuarioLista)
                    {
                        //Si en memoria no se encuentra la actividad leída de base de datos, se envía a realizar un borrado lógico
                        if (!(vl_actividadAsignadaMemoria.Where(dep => dep.pPK_Entregable == vo_actividadAsignada.pPK_Entregable &&
                                                                       dep.pPK_Componente == vo_actividadAsignada.pPK_Componente &&
                                                                       dep.pPK_Paquete == vo_actividadAsignada.pPK_Paquete &&
                                                                       dep.pPK_Actividad == vo_actividadAsignada.pPK_Actividad &&
                                                                       (dep.pUsuarioLista.Count(t => t.pPK_usuario == vo_usuario.pPK_usuario) > 0)).Count() > 0))
                        {
                            vo_actividadAsignada.pPK_Proyecto = ps_llaveProyecto;
                            vo_actividadAsignada.pUsuarioPivot = vo_usuario.pPK_usuario;
                            vi_resultado = cls_gestorAsignacionActividad.updateAsignacionActividad(vo_actividadAsignada, 0);
                        }
                    }
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al guardar la asignación de las actividades.", po_exception);
            }
        }

        /// <summary>
        /// Método que lee la lista de asignaciones de memoria y la devuelve para una variable local cuando el método sea invocado
        /// </summary>
        /// <returns></returns>
        private List<cls_asignacionActividad> crearAsignacionActividadMemoria()
        {
            try
            {
                List<cls_asignacionActividad> vo_asignacionActividad = new List<cls_asignacionActividad>();

                vo_asignacionActividad = cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria;

                return vo_asignacionActividad;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear intentar obtener los registros que se están editando en memoria.", po_exception);
            }
        }

        /// <summary>
        /// Método que lee la lista de asignaciones de base de datos y la devuelve para una variable local cuando el método sea invocado
        /// </summary>
        /// <returns></returns>
        private List<cls_asignacionActividad> crearAsignacionActividadBaseDatos()
        {
            try
            {
                List<cls_asignacionActividad> vo_asignacionActividad = new List<cls_asignacionActividad>();

                vo_asignacionActividad = cls_variablesSistema.vs_proyecto.pAsignacionActividadListaBaseDatos;

                return vo_asignacionActividad;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al crear intentar obtener los registros que se están editando.", po_exception);
            }
        }

        /// <summary>
        /// Método que limpia la información
        /// existente en los diferentes
        /// controles del formulario web
        /// </summary>
        private void limpiarCamposTexto()
        {
            try
            {
                this.txt_descripcion.Text = String.Empty;
                this.txt_fechaInicio.Text = String.Empty;
                this.txt_fechaFin.Text = String.Empty;
                this.txt_horasAsignadas.Text = String.Empty;
                this.txt_horasReales.Text = String.Empty;
                this.ddl_estado.SelectedIndex = -1;

                limpiarListBoxUsuariosAsignados();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al limpiar los campos del registro.", po_exception);
            }
        }

        /// <summary>
        /// Limpia el listbox que presenta la asociación de usuario a las actividades
        /// </summary>
        private void limpiarListBoxUsuariosAsignados()
        {
            try
            {
                if (lbx_usuariosAsociados.Items.Count > 0)
                {
                    int cantidadUsuAsociados = lbx_usuariosAsociados.Items.Count;
                    cantidadUsuAsociados = lbx_usuariosAsociados.Items.Count;

                    for (int i = 0; i < cantidadUsuAsociados; i++)
                    {
                        lbx_usuariosAsociados.Items.RemoveAt(0);
                    }
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al limpiar los la lista con los usuarios asignados a la actividad.", po_exception);
            }
        }

        /// <summary>
        /// Limpia el listbox que presenta las actividades del proyecto
        /// </summary>
        private void limpiarListBoxActividades()
        {
            try
            {
                if (lbx_actividades.Items.Count > 0)
                {
                    int cantidadActividades = lbx_actividades.Items.Count;
                    cantidadActividades = lbx_actividades.Items.Count;

                    for (int i = 0; i < cantidadActividades; i++)
                    {
                        lbx_actividades.Items.RemoveAt(0);
                    }
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al limpiar los la lista de actividades.", po_exception);
            }
        }

        /// <summary>
        /// Limpia el listbox que presenta los usuarios que se pueden asociar a las actividades del proyecto
        /// </summary>
        private void limpiarListBoxUsuarios()
        {
            try
            {
                if (lbx_usuarios.Items.Count > 0)
                {
                    int cantidadActividades = lbx_usuarios.Items.Count;
                    cantidadActividades = lbx_usuarios.Items.Count;

                    for (int i = 0; i < cantidadActividades; i++)
                    {
                        lbx_usuarios.Items.RemoveAt(0);
                    }
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al limpiar los la lista de usuarios.", po_exception);
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
                this.txt_descripcion.Enabled = pb_habilitados;
                this.txt_fechaInicio.Enabled = pb_habilitados;
                this.txt_fechaFin.Enabled = pb_habilitados;
                this.txt_horasAsignadas.Enabled = pb_habilitados;
                this.ddl_estado.Enabled = pb_habilitados;
                this.txt_horasReales.Enabled = pb_habilitados;
                this.btn_guardar.Visible = pb_habilitados;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al habilitar los controles del registro.", po_exception);
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
        /// Método encargado de enfocar el primer registro de la lista de actividades, eso si existe
        /// </summary>
        private void enfocarRegistro()
        {
            if (lbx_actividades.Items.Count > 0)
            {
                lbx_actividades.SelectedIndex = 0; 
                lbx_actividades_SelectedIndexChanged(this, new EventArgs());
                //lbx_actividades.SetSelected = true;
                //lbx_actividades.Focus();
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento para asignar el primer valor del dropdownlist, que en este caso es la leyenda "Seleccione un paquete", en la posición "0"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPaquete_DataBound(object sender, EventArgs e)
        {
            try
            {
                this.ddl_paquete.Items.Insert(0, new ListItem("Seleccione un paquete", "0"));
                this.ddl_paquete.SelectedIndex = 0;
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error mientras se asociaba la lista de paquetes.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento que asigna el nuevo valor del dropdown list de estados cuando se modifica la asignación de la actividad
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
                String vs_error_usuario = "Ocurrió un error mientras se obtenía la información asociada al paquete.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento para el cambio de índice del dropDownList de paquetes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPaquete_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //El index del paquete es reasignado
                ddl_paquete.SelectedValue = ((DropDownList)sender).SelectedValue;

                //Se limpia todo el listBox antes de realizar la nueva asignación
                limpiarListBoxUsuariosAsignados();

                //Se limpia todo el listBox antes de realizar la nueva asignación
                limpiarListBoxUsuarios();

                //Se envía a llamar al método que trae las actividades para el paquete seleccionado
                cargarActividadesPorPaquete(Convert.ToInt32(ddl_paquete.SelectedValue));

                //Como aún no se ha escogido actividad alguna, se limpian los campos hasta que se vuelvan a asignar
                limpiarCamposTexto();

                //Se envía a poner el foco en el primer registro actividad que existe para el paquete
                enfocarRegistro();
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error mientras se cargaba la información del registro asociado a los paquetes.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento para el cambio de índice en las actividades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbx_actividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int proyectoSeleccionado;
                int paqueteSeleccionado;
                int actividadSeleccionada;

                //Se asignan las 3 llaves para la búsqueda
                proyectoSeleccionado = cls_variablesSistema.vs_proyecto.pPK_proyecto;
                paqueteSeleccionado = Convert.ToInt32(ddl_paquete.SelectedValue.ToString());
                actividadSeleccionada = Convert.ToInt32(lbx_actividades.SelectedValue.ToString());

                //Se arma el objeto que envía las 3 llaves para la búsqueda
                cls_paqueteActividad vo_paqueteActividad = new cls_paqueteActividad();
                vo_paqueteActividad.pPK_Proyecto = proyectoSeleccionado;
                vo_paqueteActividad.pPK_Paquete = paqueteSeleccionado;
                vo_paqueteActividad.pPK_Actividad = actividadSeleccionada;

                limpiarCamposTexto();

                //Se limpia el listbox antes de su asignación
                limpiarListBoxUsuariosAsignados();

                //Se limpia el listbox antes de su asignación
                limpiarListBoxUsuarios();

                //Se envía a cargar, de existir, la información relacionada a la actividad
                cargarAsignacionActividad(vo_paqueteActividad);

                //Se cargan los usuarios restantes que pueden ser asignados a la actividad seleccionada
                cargarUsuarios();
                
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState","document.getElementById('" + lbx_actividades.ClientID + "').focus(); ", true);

                //ScriptManager manager = ScriptManager.GetCurrent(this);
                //manager.SetFocus("lbx_actividades");

                lbx_actividades.Focus();

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error mientras se asignada la información del registro para las actividades.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento que se ejecuta cuando se 
        /// guarda una nueva asigación de actividad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                guardarDatos();

                inicializarRegistros();

                limpiarCamposTexto();

                limpiarListBoxActividades();

                limpiarListBoxUsuarios();

                upd_Principal.Update();

                ard_principal.SelectedIndex = 0;
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error mientras se guardaba la asignación de las actividades.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da
        /// en el botón de regresar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_regresar_Click(object sender, EventArgs e)
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
        /// Evento que asigna usuarios a las actividades que se seleccionan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_asignarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                cls_paquete vo_paquete = new cls_paquete();
                vo_paquete.pPK_Paquete = Convert.ToInt32(ddl_paquete.SelectedValue.ToString());

                //Se recorren las actividades del listBox hasta que se llegue a la que se encuentra seleccionada
                for (int i = lbx_actividades.Items.Count - 1; i >= 0; i--)
                {
                    if (lbx_actividades.Items[i].Selected == true)
                    {
                        cls_actividad vo_actividad = new cls_actividad();
                        vo_actividad.pPK_Actividad = Convert.ToInt32(lbx_actividades.Items[i].Value.ToString());
                        vo_actividad.pNombre = lbx_actividades.Items[i].Text.ToString();

                        //Se recorren los usuarios del listBox hasta que se llegue al que se encuentra seleccionado
                        for (int j = lbx_usuarios.Items.Count - 1; j >= 0; j--)
                        {
                            if (lbx_usuarios.Items[j].Selected == true)
                            {
                                cls_asignacionActividad vo_actividadAsignada = new cls_asignacionActividad();

                                cls_usuario vo_usuario = new cls_usuario();
                                vo_usuario.pPK_usuario = lbx_usuarios.Items[j].Value.ToString();
                                vo_usuario.pNombre = lbx_usuarios.Items[j].Text.ToString();

                                //Si la asignación no se encuentra en memoria
                                if (cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad &&
                                                                                                                    searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete).Count() == 0)
                                {

                                    vo_actividadAsignada = (cls_asignacionActividad)cls_variablesSistema.vs_proyecto.pActividadesPaqueteLista.Find(searchLinQ => searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad &&
                                                                                                                                                                 searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete);
                                    cls_variablesSistema.obj = vo_actividadAsignada;

                                    vo_actividadAsignada = crearObjeto();

                                    //Si el usuario no se encuentra asignado, se procede a agregarlo a la lista de usuarios de la asignación y a agregar la asignación en si
                                    if (vo_actividadAsignada.pUsuarioLista.Where(searchLinQ => searchLinQ.pPK_usuario == vo_usuario.pPK_usuario).Count() == 0)
                                    {

                                        vo_actividadAsignada.pUsuarioLista.Add(vo_usuario);

                                        lbx_usuariosAsociados.Items.Add(lbx_usuarios.Items[j]);
                                        ListItem li = lbx_usuarios.Items[j];
                                        lbx_usuarios.Items.Remove(li);

                                        cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Add(vo_actividadAsignada);
                                    }
                                }
                                //De encontrarse la asignación ya en memoria, sólo se asigna y se corrobora el usuario que se intenta asignar
                                else
                                {
                                    vo_actividadAsignada = (cls_asignacionActividad)cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Find(searchLinQ => searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad &&
                                                                                                                                                                         searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete);
                                    cls_variablesSistema.obj = vo_actividadAsignada;

                                    vo_actividadAsignada = crearObjeto();

                                    if (vo_actividadAsignada.pUsuarioLista.Where(searchLinQ => searchLinQ.pPK_usuario == vo_usuario.pPK_usuario).Count() == 0)
                                    {
                                        vo_actividadAsignada.pUsuarioLista.Add(vo_usuario);

                                        lbx_usuariosAsociados.Items.Add(lbx_usuarios.Items[j]);
                                        ListItem li = lbx_usuarios.Items[j];
                                        lbx_usuarios.Items.Remove(li);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error mientras se asignada la información del usuario a la actividad.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento para remover usuarios de la asignación de actividades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_removerUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                cls_paquete vo_paquete = new cls_paquete();
                vo_paquete.pPK_Paquete = Convert.ToInt32(ddl_paquete.SelectedValue.ToString());

                for (int i = lbx_actividades.Items.Count - 1; i >= 0; i--)
                {
                    if (lbx_actividades.Items[i].Selected == true)
                    {
                        cls_actividad vo_actividad = new cls_actividad();
                        vo_actividad.pPK_Actividad = Convert.ToInt32(lbx_actividades.Items[i].Value.ToString());
                        vo_actividad.pNombre = lbx_actividades.Items[i].Text.ToString();

                        for (int j = lbx_usuariosAsociados.Items.Count - 1; j >= 0; j--)
                        {
                            if (lbx_usuariosAsociados.Items[j].Selected == true)
                            {
                                cls_asignacionActividad vo_actividadAsignada = new cls_asignacionActividad();

                                cls_usuario vo_usuario = new cls_usuario();
                                vo_usuario.pPK_usuario = lbx_usuariosAsociados.Items[j].Value.ToString();
                                vo_usuario.pNombre = lbx_usuariosAsociados.Items[j].Text.ToString();

                                if (cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad &&
                                                                                                                          searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete).Count() > 0)
                                {
                                    vo_actividadAsignada = (cls_asignacionActividad)cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.Find(searchLinQ => searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad &&
                                                                                                                                                                         searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete);
                                    //Si luego de esta eliminación, la lista aún va a quedar con elementos, solo se remueve el usuario
                                    if (vo_actividadAsignada.pUsuarioLista.Count > 1)
                                    {
                                        vo_actividadAsignada.pUsuarioLista.RemoveAll(searchLinQ => searchLinQ.pPK_usuario == vo_usuario.pPK_usuario);
                                    }
                                    //Si solo se haya un elemento en la lista de usuarios, no se realiza la eliminación del mismo, sino de la actividad asignada
                                    //del objeto en memoria, esto debido a que la llave primaria de la asignación está compuesta en parte por la llave primario
                                    //del usuario, y si el mismo no se asigna, pues no se obtiene y presentaría un error
                                    else
                                    {
                                        cls_variablesSistema.vs_proyecto.pAsignacionActividadListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad &&
                                                                                                                                  searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete);
                                    }
                                }

                                lbx_usuarios.Items.Add(lbx_usuariosAsociados.Items[j]);
                                ListItem li = lbx_usuariosAsociados.Items[j];
                                lbx_usuariosAsociados.Items.Remove(li);

                            }
                        }
                    }
                }
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error mientras se desasociaba la información del usuario a la actividad.";
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

        #endregion Seguridad
    }
}