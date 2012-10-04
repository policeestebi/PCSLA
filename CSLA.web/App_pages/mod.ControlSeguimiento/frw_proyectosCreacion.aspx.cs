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
using COSEVI.CSLA.lib.entidades.mod.Administracion;

using CSLA.web.App_Variables;
using CSLA.web.App_Constantes;
using System.Data;
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
// Cristian Arce Jiménez  	    11 – 27  - 2011	 	Se agrega el manejo de excepciones personalizadas
// Cristian Arce Jiménez  	    01 – 23  - 2012	 	Se agrega el manejo de filtros
// Cristian Arce Jiménez  	    05 – 01  - 2012	 	Se modifican los métodos y eventos
// Cristian Arce Jiménez  	    05 – 04  - 2012	 	Se modifican el manejo de excepciones
//								
//
// =========================================================================

namespace CSLA.web.App_pages.mod.ControlSeguimiento
{
    public partial class frw_proyectosCreacion : System.Web.UI.Page
    {
        #region Variables

        //Inicialización de botones de la navegación
        Button btnNxt;
        Button btnPrev;

        #endregion Variables

        #region Inicialización

        /// <summary>
        /// Función que se ejecuta al cargar
        /// la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    this.validarSession();
                    this.validarAcceso();
                }
                catch (Exception po_exception)
                {
                    String vs_error_usuario = "Ocurrió un error al inicializar el asistente de creación de proyectos.";
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
                String vs_error_usuario = "Ocurrió un error al inicializar los controles en el asistente de creación de proyectos.";
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
                //Inicialización de botones de la asignación
                //Entregables
                /*this.btn_asignarEntregable = (Button)acp_creacionProyectos.FindControl("btn_asignarEntregable");
                this.btn_removerEntregable = (Button)acp_creacionProyectos.FindControl("btn_removerEntregable");

                //Componentes
                this.btn_asignarComponente = (Button)acp_creacionProyectos.FindControl("btn_asignarComponente");
                this.btn_removerComponente = (Button)acp_creacionProyectos.FindControl("btn_removerComponente");

                //Paquetes
                this.btn_asignarPaquete = (Button)acp_creacionProyectos.FindControl("btn_asignarPaquete");
                this.btn_removerPaquete = (Button)acp_creacionProyectos.FindControl("btn_removerPaquete");

                //Actividad
                this.btn_asignarActividad = (Button)acp_creacionProyectos.FindControl("btn_asignarActividad");
                this.btn_removerActividad = (Button)acp_creacionProyectos.FindControl("btn_removerActividad");*/

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error inicializando los controles del mantenimiento.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        #endregion

        #region Métodos Generales

        /// <summary>
        /// Método para la asignación del nombre del proyecto que se va a crear/modificar
        /// </summary>
        private void cargarProyecto()
        {
            try
            {
                txt_proyecto.Text = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pNombre;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar la información del proyecto seleccionado.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de proyectos-entegables asignada en memoria a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_proyectoEntregable> crearProyectoEntregableMemoria()
        {
            try
            {
                List<cls_proyectoEntregable> vo_proyectoEntregable = new List<cls_proyectoEntregable>();

                vo_proyectoEntregable = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria;

                return vo_proyectoEntregable;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene los entregables asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de entegables-componentes asignada en memoria a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_entregableComponente> crearEntregableComponenteMemoria()
        {
            try
            {
                List<cls_entregableComponente> vo_entregableComponente = new List<cls_entregableComponente>();

                vo_entregableComponente = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria;

                return vo_entregableComponente;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene los componentes asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de componentes-paquetes asignada en memoria a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_componentePaquete> crearComponentePaqueteMemoria()
        {
            try
            {
                List<cls_componentePaquete> vo_componentePaquete = new List<cls_componentePaquete>();

                vo_componentePaquete = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria;

                return vo_componentePaquete;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene los paquetes asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de paquetes-actividades asignada en memoria a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_paqueteActividad> crearPaqueteActividadMemoria()
        {
            try
            {
                List<cls_paqueteActividad> vo_paqueteActividad = new List<cls_paqueteActividad>();

                vo_paqueteActividad = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria;

                return vo_paqueteActividad;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene las actividades asociadas a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de proyectos-entegables asignada en base de datos a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_proyectoEntregable> crearProyectoEntregableBaseDatos()
        {
            try
            {
                List<cls_proyectoEntregable> vo_proyectoEntregable = new List<cls_proyectoEntregable>();

                vo_proyectoEntregable = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaBaseDatos;

                return vo_proyectoEntregable;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene los entregables asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de entegables-componentes asignada en base de datos a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_entregableComponente> crearEntregableComponenteBaseDatos()
        {
            try
            {
                List<cls_entregableComponente> vo_entregableComponente = new List<cls_entregableComponente>();

                vo_entregableComponente = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos;

                return vo_entregableComponente;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene los componentes asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de componentes-paquetes asignada en base de datos a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_componentePaquete> crearComponentePaqueteBaseDatos()
        {
            try
            {
                List<cls_componentePaquete> vo_componentePaquete = new List<cls_componentePaquete>();

                vo_componentePaquete = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos;

                return vo_componentePaquete;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene los paquetes asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para devolver la lista de paquetes-actividades asignada en base de datos a una variable local cuando sea invocada
        /// </summary>
        /// <returns></returns>
        private List<cls_paqueteActividad> crearPaqueteActividadBaseDatos()
        {
            try
            {
                List<cls_paqueteActividad> vo_paqueteActividad = new List<cls_paqueteActividad>();

                vo_paqueteActividad = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos;

                return vo_paqueteActividad;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al asignar la información de la lista que mantiene las actividades asociadas a proyecto.", po_exception);
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
            int llaveProyecto = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPK_proyecto;  

            try
            {
                switch (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado)
                {
                    case cls_constantes.AGREGAR:

                        agregarProyectoEntregable(llaveProyecto);
                        agregarEntregableComponente(llaveProyecto);
                        agregarComponentePaquete(llaveProyecto);
                        agregarPaqueteActividad(llaveProyecto);

                        break;
                    case cls_constantes.EDITAR:

                        editarProyectoEntregable(llaveProyecto);
                        editarEntregableComponente(llaveProyecto);
                        editarComponentePaquete(llaveProyecto);
                        editarPaqueteActividad(llaveProyecto);

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
        /// Método para la inserción de registros de proyectos-entregables
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int agregarProyectoEntregable(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_proyectoEntregable> vl_proyectoEntregable = this.crearProyectoEntregableMemoria();

            try
            {
                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_proyectoEntregable vo_proyectoEntregable in vl_proyectoEntregable)
                {
                    vo_proyectoEntregable.pProyecto.pPK_proyecto = ps_llaveProyecto;

                    vo_proyectoEntregable.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                    
                    vi_resultado = cls_gestorProyectoEntregable.insertProyectoEntregable(vo_proyectoEntregable);
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar los entregables asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para la inserción de registros de entregables-componentes
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int agregarEntregableComponente(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_entregableComponente> vl_entregableComponente = this.crearEntregableComponenteMemoria();

            try
            {
                //Para cada entregable componente, se realiza la inserción
                foreach (cls_entregableComponente vo_entregableComponente in vl_entregableComponente)
                {
                    vo_entregableComponente.pProyecto.pPK_proyecto = ps_llaveProyecto;

                    vo_entregableComponente.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                    vi_resultado = cls_gestorEntregableComponente.insertEntregableComponente(vo_entregableComponente);
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar los componentes asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para la inserción de registros de componentes-paquetes
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int agregarComponentePaquete(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_componentePaquete> vl_componentePaquete = this.crearComponentePaqueteMemoria();

            try
            {
                //Para cada componente paquete, se realiza la inserción
                foreach (cls_componentePaquete vo_componentePaquete in vl_componentePaquete)
                {
                    vo_componentePaquete.pProyecto.pPK_proyecto = ps_llaveProyecto;

                    vo_componentePaquete.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                    vi_resultado = cls_gestorComponentePaquete.insertComponentePaquete(vo_componentePaquete);
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar los paquetes asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para la inserción de registros de paquetes-actividades
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int agregarPaqueteActividad(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_paqueteActividad> vl_paqueteActividad = this.crearPaqueteActividadMemoria();

            try
            {
                //Para cada paquete actividad, se realiza la inserción
                foreach (cls_paqueteActividad vo_paqueteActividad in vl_paqueteActividad)
                {
                    vo_paqueteActividad.pProyecto.pPK_proyecto = ps_llaveProyecto;
                    vo_paqueteActividad.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                    vi_resultado = cls_gestorPaqueteActividad.insertPaqueteActividad(vo_paqueteActividad);
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar las actividades asociadas al proyecto.", po_exception);
            }
        }


        /// <summary>
        /// Método para la modificación de registros de proyectos-entregables
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int editarProyectoEntregable(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_proyectoEntregable> vl_proyectoEntregableMemoria = this.crearProyectoEntregableMemoria();
            List<cls_proyectoEntregable> vl_proyectoEntregableBaseDatos = this.crearProyectoEntregableBaseDatos();

            try
            {
                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_proyectoEntregable vo_proyectoEntregable in vl_proyectoEntregableMemoria)
                {
                    if (!(vl_proyectoEntregableBaseDatos.Where(dep => dep.pPK_Entregable == vo_proyectoEntregable.pPK_Entregable).Count() > 0))
                    {
                        vo_proyectoEntregable.pProyecto.pPK_proyecto = ps_llaveProyecto;
                        vo_proyectoEntregable.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;

                        vi_resultado = cls_gestorProyectoEntregable.updateProyectEntregable(vo_proyectoEntregable, 1);
                    }
                }

                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_proyectoEntregable vo_proyectoEntregable in vl_proyectoEntregableBaseDatos)
                {
                    if (!(vl_proyectoEntregableMemoria.Where(dep => dep.pPK_Entregable == vo_proyectoEntregable.pPK_Entregable).Count() > 0))
                    {
                        vo_proyectoEntregable.pProyecto.pPK_proyecto = ps_llaveProyecto;
                        vo_proyectoEntregable.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        vi_resultado = cls_gestorProyectoEntregable.updateProyectEntregable(vo_proyectoEntregable, 0);
                    }
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar los entregables asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para la modificación de registros de entregables-componentes
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int editarEntregableComponente(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_entregableComponente> vl_entregableComponenteMemoria = this.crearEntregableComponenteMemoria();
            List<cls_entregableComponente> vl_entregableComponenteBaseDatos = this.crearEntregableComponenteBaseDatos();

            try
            {
                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_entregableComponente vo_entregableComponente in vl_entregableComponenteMemoria)
                {
                    if (!(vl_entregableComponenteBaseDatos.Where(dep => dep.pPK_Entregable == vo_entregableComponente.pPK_Entregable &&
                                                                        dep.pPK_Componente == vo_entregableComponente.pPK_Componente).Count() > 0))
                    {
                        vo_entregableComponente.pProyecto.pPK_proyecto = ps_llaveProyecto;

                        vo_entregableComponente.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        vi_resultado = cls_gestorEntregableComponente.updateEntregableComponente(vo_entregableComponente, 1);
                    }
                }

                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_entregableComponente vo_entregableComponente in vl_entregableComponenteBaseDatos)
                {
                    if (!(vl_entregableComponenteMemoria.Where(dep => dep.pPK_Entregable == vo_entregableComponente.pPK_Entregable &&
                                                                      dep.pPK_Componente == vo_entregableComponente.pPK_Componente).Count() > 0))
                    {
                        vo_entregableComponente.pProyecto.pPK_proyecto = ps_llaveProyecto;

                        vo_entregableComponente.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        vi_resultado = cls_gestorEntregableComponente.updateEntregableComponente(vo_entregableComponente, 0);
                    }
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar los componentes asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para la modifiación de registros de componentes-paquetes
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int editarComponentePaquete(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_componentePaquete> vl_componentePaqueteMemoria = this.crearComponentePaqueteMemoria();
            List<cls_componentePaquete> vl_componentePaqueteBaseDatos = this.crearComponentePaqueteBaseDatos();

            try
            {
                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_componentePaquete vo_componentePaquete in vl_componentePaqueteMemoria)
                {
                    if (!(vl_componentePaqueteBaseDatos.Where(dep => dep.pPK_Entregable == vo_componentePaquete.pPK_Entregable &&
                                                                     dep.pPK_Componente == vo_componentePaquete.pPK_Componente &&
                                                                     dep.pPK_Paquete == vo_componentePaquete.pPK_Paquete).Count() > 0))
                    {
                        vo_componentePaquete.pProyecto.pPK_proyecto = ps_llaveProyecto;
                        vo_componentePaquete.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        vi_resultado = cls_gestorComponentePaquete.updateComponentePaquete(vo_componentePaquete, 1);
                    }
                }

                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_componentePaquete vo_componentePaquete in vl_componentePaqueteBaseDatos)
                {
                    if (!(vl_componentePaqueteMemoria.Where(dep => dep.pPK_Entregable == vo_componentePaquete.pPK_Entregable &&
                                                                   dep.pPK_Componente == vo_componentePaquete.pPK_Componente &&
                                                                   dep.pPK_Paquete == vo_componentePaquete.pPK_Paquete).Count() > 0))
                    {
                        vo_componentePaquete.pProyecto.pPK_proyecto = ps_llaveProyecto;
                        vo_componentePaquete.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        vi_resultado = cls_gestorComponentePaquete.updateComponentePaquete(vo_componentePaquete, 0);
                    }
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar los paquetes asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para la modifiación de registros de paquetes-actividades
        /// </summary>
        /// <param name="ps_llaveProyecto"></param>
        /// <returns></returns>
        private int editarPaqueteActividad(int ps_llaveProyecto)
        {
            int vi_resultado = 1;

            List<cls_paqueteActividad> vl_paqueteActividadMemoria = this.crearPaqueteActividadMemoria();
            List<cls_paqueteActividad> vl_componentePaqueteBaseDatos = this.crearPaqueteActividadBaseDatos();

            try
            {
                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_paqueteActividad vo_paqueteActividad in vl_paqueteActividadMemoria)
                {
                    if (!(vl_componentePaqueteBaseDatos.Where(dep => dep.pPK_Entregable == vo_paqueteActividad.pPK_Entregable &&
                                                                     dep.pPK_Componente == vo_paqueteActividad.pPK_Componente &&
                                                                     dep.pPK_Paquete == vo_paqueteActividad.pPK_Paquete &&
                                                                     dep.pPK_Actividad == vo_paqueteActividad.pPK_Actividad).Count() > 0))
                    {
                        vo_paqueteActividad.pProyecto.pPK_proyecto = ps_llaveProyecto;
                        vo_paqueteActividad.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        vi_resultado = cls_gestorPaqueteActividad.updatePaqueteActividad(vo_paqueteActividad, 1);
                    }
                }

                //Para cada proyecto entregable, se realiza la inserción
                foreach (cls_paqueteActividad vo_paqueteActividad in vl_componentePaqueteBaseDatos)
                {
                    if (!(vl_paqueteActividadMemoria.Where(dep => dep.pPK_Entregable == vo_paqueteActividad.pPK_Entregable &&
                                                                  dep.pPK_Componente == vo_paqueteActividad.pPK_Componente &&
                                                                  dep.pPK_Paquete == vo_paqueteActividad.pPK_Paquete &&
                                                                  dep.pPK_Actividad == vo_paqueteActividad.pPK_Actividad).Count() > 0))
                    {
                        vo_paqueteActividad.pProyecto.pPK_proyecto = ps_llaveProyecto;
                        vo_paqueteActividad.pUsuarioTransaccion = ((cls_usuario)Session["cls_usuario"]).pPK_usuario;
                        vi_resultado = cls_gestorPaqueteActividad.updatePaqueteActividad(vo_paqueteActividad, 0);
                    }
                }

                return vi_resultado;
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al tratar de guardar las actividades asociadas al proyecto.", po_exception);
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

        #endregion Métodos Generales

        #region Eventos Generales

        /// <summary>
        /// Evento para finalizar la creación/modifiación de un proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wiz_creacion_FinishButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.guardarDatos();

                this.upd_Principal.Update();

                //this.ard_principal.SelectedIndex = 0;

                //Al finalizar, se envía a la página principal de proyectos
                Response.Redirect("frw_proyectos.aspx", false);

            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar grabar la información del registro de proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            } 
        }

        /// <summary>
        /// Evento que redirecciona a la página principsl de proyectos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //Se redirecciona al cancelar
                Response.Redirect("frw_proyectos.aspx", false);
            }
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al cancelar la operación en el asistente de creación de proyectos.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento para deshabilitar el botón de siguiente cuando no se encuentran registros asociados en el primer paso del wizard de creación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            btnNxt = (Button)sender;
			try
			{
				if (wiz_creacion.ActiveStepIndex == wiz_creacion.WizardSteps.IndexOf(this.wzs_entregables))
				{
					if (lbx_entasociados.Items.Count == 0 && btnNxt != null)
					{
						btnNxt.Enabled = false;
					}
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar movilizarse en el asistente de creación de proyectos.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento sin funcionalidad, sólo existe por si se llega a necesitar realizar validaciones en el devolverse en el wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //El evento solo existe por si se llegara a necesitar
            btnPrev = (Button)sender;
        }

        /// <summary>
        /// Evento encargado de llenar el listbox correspondiente al step donde se encuentre en el wizard, además de habilitar o deshabilitar los botones de navegación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnActiveStepChanged(object sender, EventArgs e)
        {
			try
			{
				//Validación en el primer paso, el de entregables
				if (wiz_creacion.ActiveStepIndex == wiz_creacion.WizardSteps.IndexOf(this.wzs_entregables))
				{
					//Se envía a llenar los datos asociados de los entregables
					llenarDatosEntregables();

					if (lbx_entasociados.Items.Count == 0 && btnNxt != null && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaBaseDatos.Count == 0)
					{
						btnNxt.Enabled = false;
					}
					else
					{
						if (btnNxt != null)
						{
							btnNxt.Enabled = true;
						}
					}

					if (btnPrev != null)
					{
						btnPrev.Visible = false;
					}

				}
				//Validación en el step de componentes ya asociados a entregables
				if (wiz_creacion.ActiveStepIndex == wiz_creacion.WizardSteps.IndexOf(this.wzs_componentes))
				{
					if (lbx_compasociados.Items.Count == 0 && btnNxt != null && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos.Count == 0)
					{
						btnNxt.Enabled = false;
					}

					//Se envía a llenar los datos asociados de los componentes
					llenarDatosComponentes();

					if (btnPrev != null)
					{
						btnPrev.Visible = true;
					}
				}
				//Validación en el step de componentes ya asociados a paquetes
				if (wiz_creacion.ActiveStepIndex == wiz_creacion.WizardSteps.IndexOf(this.wzs_paquetes))
				{
					if (lbx_paqasociados.Items.Count == 0 && btnNxt != null && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos.Count == 0)
					{
						btnNxt.Enabled = false;
					}

					//Se envía a llenar los datos asociados de los paquetes
					llenarDatosPaquetes();

					if (btnPrev != null)
					{
						btnPrev.Visible = true;
					}
				}
				//Validación en el step de componentes ya asociados a paquetes
				if (wiz_creacion.ActiveStepIndex == wiz_creacion.WizardSteps.IndexOf(this.wzs_actividades))
				{
					if (lbx_actasociadas.Items.Count == 0 && btnNxt != null && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos.Count == 0)
					{
						btnNxt.Enabled = false;
					}

					//Se envía a llenar los datos asociados de las actividades
					llenarDatosActividades();

					if (btnPrev != null)
					{
						btnPrev.Visible = true;
					}
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar movilizarse al siguiente paso del asistente de creación de proyectos.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        #endregion Eventos Generales

        #region Métodos Entregables

        /// <summary>
        /// Método para habilitar o deshabilitar los botones de anterior y siguiente
        /// </summary>
        private void deshabilitarBotonesNavegacion()
        {
			try
			{
				if (lbx_entasociados.Items.Count == 0)
				{
					btnNxt.Enabled = false;
				}

				if (wiz_creacion.ActiveStepIndex == wiz_creacion.WizardSteps.IndexOf(this.wzs_entregables))
				{
					btnPrev.Visible = false;
				}
			}
			catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al intentar deshabilitar los botones del asistente de creación.", po_exception);
            }
        }

        /// <summary>
        /// Método para cargar los entregables asociados, y los que se pueden asociar a un proyecto
        /// </summary>
        private void llenarDatosEntregables()
        {
            try
            {
                cargarProyecto();
                cargarListaEntregables();
                cargarEntregablesPorProyecto();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar el registro con los entregables de proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Se manda a cargar la totalidad de los entregables del proyecto
        /// </summary>
        private void cargarListaEntregables()
        {
            try
            {
                /*
                 NOta: * Revisar los selects de los listar, para ver que tanto es necesario cambiar los "pNombre" por los nombres de la tabla => "pNombre" - "pNombreEntregable"
                       * Ver si es relevante cambiar los nombres a los listbox
                 */
                lbx_entregables.DataSource = cls_gestorEntregable.listarEntregable(); ;
                lbx_entregables.DataTextField = "pNombre";
                lbx_entregables.DataValueField = "pPK_entregable";
                lbx_entregables.DataBind();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los datos de la lista de entregables.", po_exception);
            }
        }

        /// <summary>
        /// Se cargan los entregables que están asociados al proyecto, ya sea por Base de Datos o por memoria
        /// </summary>
        private void cargarEntregablesPorProyecto()
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                if(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Count > 0)
                {
                    //Si la variable en memoria SI posee algún valor, se va a efectuar una "Actualizacion" al proyecto
                    ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado = cls_constantes.EDITAR;

                    lbx_entasociados.DataSource = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria;
                    lbx_entasociados.DataTextField = "pNombreEntregable";
                    lbx_entasociados.DataValueField = "pPK_Entregable";
                }
                else
                {

                    vo_dataSet = cls_gestorProyectoEntregable.selectProyectoEntregable(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);

                    if (vo_dataSet.Tables[0].Rows.Count > 0)
                    {
                        //Si la variable en memoria SI posee algún valor, se va a efectuar una "Actualizacion" al proyecto
                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado = cls_constantes.EDITAR;
                    }
                    else
                    {
                        //Si la variable en memoria NO posee algún valor, se va a efectuar una "Insercion" de proyecto
                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).tipoEstado = cls_constantes.AGREGAR;
                    }

                    //Se recorren los entregables pertenecientes a un proyecto y se asignan en el listbox para los asociados
                    foreach (DataRow row in vo_dataSet.Tables[0].Rows)
                    {

                        cls_entregable vo_entregable = new cls_entregable();
                        cls_proyectoEntregable vo_proyectoEntregable = new cls_proyectoEntregable();

                        vo_entregable.pPK_entregable = Convert.ToInt32(row["PK_entregable"]);
                        vo_entregable.pNombre = Convert.ToString(row["nombre"]);

                        vo_proyectoEntregable.pEntregable = vo_entregable;

                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableLista.Add(vo_entregable);
                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Add(vo_proyectoEntregable);
                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaBaseDatos.Add(vo_proyectoEntregable);
                    }

                    lbx_entasociados.DataSource = vo_dataSet;
                    lbx_entasociados.DataTextField = "nombre";
                    lbx_entasociados.DataValueField = "PK_entregable";
                }

                lbx_entasociados.DataBind();

                //Se elimina los entregables ya asociados de la totalidad, para evitar duplicidad en los datos por asignar
                if (lbx_entasociados.Items.Count > 0)
                {
                    foreach (ListItem item in lbx_entasociados.Items)
                    {
                        lbx_entregables.Items.Remove(item);
                    }
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los entregables asociados al proyecto.", po_exception);
            }
        }

        #endregion Métodos Entregables

        #region Eventos Entregables

        /// <summary>
        /// Evento para la asignación de los entregables a un proyecto específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_asignarEntregable_Click(object sender, EventArgs e)
        {
			try
			{
				for (int i = lbx_entregables.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_entregables.Items[i].Selected == true)
					{
						cls_entregable vo_entregable = new cls_entregable();
						vo_entregable.pPK_entregable = Convert.ToInt32(lbx_entregables.Items[i].Value.ToString());
						vo_entregable.pNombre = lbx_entregables.Items[i].Text;

						cls_proyectoEntregable vo_proyectoEntregable = new cls_proyectoEntregable();

						vo_proyectoEntregable.pEntregable = vo_entregable;

						//Si el registro no existe en memoria, se agrega
						if (!(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == vo_entregable.pPK_entregable).Count() > 0))
						{
							((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableLista.Add(vo_entregable);
							((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Add(vo_proyectoEntregable);
						}

						lbx_entasociados.Items.Add(lbx_entregables.Items[i]);
						ListItem li = lbx_entregables.Items[i];
						lbx_entregables.Items.Remove(li);
					}
				}

				//Si hay entregables asociados, se habilita el botón de navegación de siguiente
				if (lbx_entasociados.Items.Count > 0)
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar asignar el entregable al proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento para la eliminación de un entregable asignado a un proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_removerEntregable_Click(object sender, EventArgs e)
        {
			try
			{
				for (int i = lbx_entasociados.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_entasociados.Items[i].Selected == true)
					{

						cls_entregable vo_entregable = new cls_entregable();
						vo_entregable.pPK_entregable = Convert.ToInt32(lbx_entasociados.Items[i].Value.ToString());
						vo_entregable.pNombre = lbx_entasociados.Items[i].Text;

						cls_proyectoEntregable vo_proyectoEntregable = new cls_proyectoEntregable();

						vo_proyectoEntregable.pEntregable = vo_entregable;

						//Se realiza una eliminación de todas las posibles referencias que se presenten a nivel de memoria para el entregable que se está eliminando
						foreach (cls_entregableComponente entComp in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria)
						{
							if (entComp.pPK_Entregable == vo_entregable.pPK_entregable)
							{
								foreach (cls_componentePaquete compPaq in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria)
								{
									if (compPaq.pPK_Entregable == entComp.pPK_Entregable)
									{
										foreach (cls_paqueteActividad paqAct in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria)
										{
											if (paqAct.pPK_Entregable == entComp.pPK_Entregable)
											{
												((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pActividadLista.RemoveAll(searchLinQ => searchLinQ.pPK_Actividad == paqAct.pPK_Actividad);
											}
										}

										((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == compPaq.pPK_Paquete);

										((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteLista.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == compPaq.pPK_Paquete);
									}
								}

								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Componente == entComp.pPK_Componente);

								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponenteLista.RemoveAll(searchLinQ => searchLinQ.pPK_componente == entComp.pPK_Componente);
							}
						}

						((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Entregable == vo_entregable.pPK_entregable);


						((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableLista.RemoveAll(searchLinQ => searchLinQ.pPK_entregable == vo_entregable.pPK_entregable);
						((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Entregable == vo_entregable.pPK_entregable);


						lbx_entregables.Items.Add(lbx_entasociados.Items[i]);
						ListItem li = lbx_entasociados.Items[i];
						lbx_entasociados.Items.Remove(li);
					}
				}

				//Si luego de desasociar los entregables, si la lista queda vacía, no se puede proseguir hasta que no se realice al menos una asignación
				if (lbx_entasociados.Items.Count == 0 && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Count == 0)
				{
					btnNxt.Enabled = false;
				}
				else
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar remover el entregable del proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }

        }


        #endregion Eventos Entregables

        #region Métodos Componentes

        /// <summary>
        /// Método que obtiene los entregables que se encuentran asignados  a proyecto
        /// </summary>
        private void llenarDatosComponentes()
        {
            try
            {
                cargarEntregablesAsociados();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los registros de los componentes de proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Se obtienen los entregables que se asociaron en el paso anterior del wizard, ya sean provenientes de Base de Datos, o recién asignados
        /// </summary>
        private void cargarEntregablesAsociados()
        {
            DataSet vo_dataSet = new DataSet();
            try
            {
                //Si la lista posee datos, se respeta lo que se encuentre en memoria
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Count > 0)
                {
                    lbx_entregablesasociados.DataSource = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria;
                    lbx_entregablesasociados.DataTextField = "pNombreEntregable";
                    lbx_entregablesasociados.DataValueField = "pPK_entregable";
                    lbx_entregablesasociados.DataBind();
                }
                //De lo contrario, se realiza la consulta a nivel de base de datos
                else
                {
                    vo_dataSet = cls_gestorProyectoEntregable.selectProyectoEntregable(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);
                    lbx_entregablesasociados.DataSource = vo_dataSet;
                    lbx_entregablesasociados.DataTextField = "nombre";
                    lbx_entregablesasociados.DataValueField = "PK_entregable";
                    lbx_entregablesasociados.DataBind();
                }

                //Luego de cargar los entregables, se procede a consultar en memoria o base de datos, filtrando por el entregable seleccionado
                cargarComponentesPorEntregable();

            }
            /*
                Nota: revisar el manejo de excepxiones personalizadas en este form
            */
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los datos de la lista de los entregables asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Se manda a cargar la totalidad de los componentes del proyecto
        /// </summary>
        private void cargarListaComponentes()
        {
            try
            {
                /*
                 NOta: * Revisar los selects de los listar, para ver que tanto es necesario cambiar los "pNombre" por los nombres de la tabla => "pNombre" - "pNombreComponente"
                       * Ver si es relevante cambiar los nombres a los listbox
                 */
                lbx_componentes.DataSource = cls_gestorComponente.listarComponente();
                lbx_componentes.DataTextField = "pNombre";
                lbx_componentes.DataValueField = "pPK_componente";
                lbx_componentes.DataBind();

                //Se valida dentro de la misma lista de componentes, que si en algunos de los entregables se encuentra asociado, que se encargue de eliminarlo
                //del listbox, para que no llegue a ser tomado en cuenta entre los componentes de algún otro entregable(son únicos por entregable
                //Este listBox pivot se incorpora debido a que trabajar sobre el listbox original, por el manejo de punteros en C#, se presentan excepciones no controladas
                ListBox lbx_pivot = new ListBox();

                lbx_pivot.DataSource = cls_gestorComponente.listarComponente();
                lbx_pivot.DataTextField = "pNombre";
                lbx_pivot.DataValueField = "pPK_componente";
                lbx_pivot.DataBind();

                //Si se devuelven componentes asociados, se remueven los mismos del listBox que mantiene la totalidad de componentes, estp para mantener la 
                //pertenencia de un componente a un sólo entregable
                if (lbx_compasociados.Items.Count > 0)
                {
                    foreach (ListItem item in lbx_compasociados.Items)
                    {
                        lbx_componentes.Items.Remove(item);
                    }
                }

                //También se procede a eliminar de la lista de componentes aquellos que se encuentren asignados en memoria
                foreach (ListItem item in lbx_pivot.Items)
                {
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Componente.ToString() == item.Value).Count() > 0)
                    {
                        lbx_componentes.Items.Remove(item);
                    }
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los datos de la lista de componentes.", po_exception);
            }

        }

        /// <summary>
        /// Método que obtiene los componentes asociados al entregable que se le indica
        /// </summary>
        /// <param name="po_entregable"></param>
        private void inicializarComponentesPorEntregable(cls_entregable po_entregable)
        {
			try
			{
				cls_entregableComponente vo_entregableComponente = new cls_entregableComponente();

				vo_entregableComponente.pProyecto = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto;
				vo_entregableComponente.pEntregable = po_entregable;

				cargarComponentesPorEntregable(vo_entregableComponente);

				cargarListaComponentes();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al los componentes asociados a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método que obtiene los entregables asociados al entregable seleccionado
        /// </summary>
        /// <param name="po_entregableComponente"></param>
        private void cargarComponentesPorEntregable(cls_entregableComponente po_entregableComponente)
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                //Si la lista de memoria se encuentra vacía, se realiza la consulta a nivel de base de datos
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Count == 0)
                {
                    vo_dataSet = cls_gestorEntregableComponente.selectEntregableComponente(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);

                    //Se recorren los registros obtenidos en la consulta
                    foreach (DataRow row in vo_dataSet.Tables[0].Rows)
                    {

                        cls_entregable vo_entregable = new cls_entregable();
                        cls_componente vo_componente = new cls_componente();
                        cls_entregableComponente vo_entregableComponente = new cls_entregableComponente();

                        vo_entregable.pPK_entregable = Convert.ToInt32(row["PK_entregable"]);
                        vo_componente.pPK_componente = Convert.ToInt32(row["PK_componente"]);
                        vo_componente.pNombre = Convert.ToString(row["nombre"]);

                        vo_entregableComponente.pEntregable = vo_entregable;
                        vo_entregableComponente.pComponente = vo_componente;

                        //Si el objeto no se encuentra en memoria, se agrega
                        if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos.Where(searchLinQ => searchLinQ.pPK_Componente == vo_entregableComponente.pPK_Componente).Count() == 0)
                        {
                            ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos.Add(vo_entregableComponente);
                        }
                        //Si el objeto no se encuentra en memoria, se agrega
                        if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == vo_entregableComponente.pPK_Entregable).Count() > 0)
                        {
                            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Componente == vo_entregableComponente.pPK_Componente).Count() == 0)
                            {
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponenteLista.Add(vo_componente);
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Add(vo_entregableComponente);
                            }                      
                        }
                    }

                }

                //Si el objeto está asociado en memoria se utiliza
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == po_entregableComponente.pPK_Entregable).Count() > 0)
                {
                    lbx_compasociados.DataSource = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == po_entregableComponente.pPK_Entregable);
                    lbx_compasociados.DataTextField = "pNombreComponente";
                    lbx_compasociados.DataValueField = "pPK_Componente";

                    //Se realiza el Binding luego de saber de donde se tomarán los datos
                    lbx_compasociados.DataBind();

                    if (lbx_compasociados.Items.Count > 0)
                    {
                        //Si se leyeron datos asociados, se activa el botón de siguiente
                        btnNxt.Enabled = true;
                    }
                }
                //De lo contrario se consulta en base de datos para obtener, si existen, los registros asociados
                else
                {
                    vo_dataSet = cls_gestorEntregableComponente.selectEntregableComponente(po_entregableComponente);
                    lbx_compasociados.DataSource = vo_dataSet;
                    lbx_compasociados.DataTextField = "Nombre";
                    lbx_compasociados.DataValueField = "PK_Componente";

                    //Se realiza el Binding luego de saber de donde se tomarán los datos
                    lbx_compasociados.DataBind();

                    if (lbx_compasociados.Items.Count > 0)
                    {
                        ListBox lbx_pivot = new ListBox();

                        lbx_pivot.DataSource = vo_dataSet;
                        lbx_pivot.DataTextField = "Nombre";
                        lbx_pivot.DataValueField = "PK_Componente";
                        lbx_pivot.DataBind();

                        foreach (ListItem item in lbx_pivot.Items)
                        {
                            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == po_entregableComponente.pPK_Entregable &&
                                                                                                                       searchLinQ.pPK_Componente == Convert.ToInt32(item.Value)).Count() == 0)
                            {
                                lbx_compasociados.Items.Remove(item);
                            }
                        }

                        //Si se leyeron datos asociados, se activa el botón de siguiente
                        btnNxt.Enabled = true;
                    }
                }

                

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los entregables asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método para obtener los componentes que se encuentran asociados a un entregable específico
        /// </summary>
        private void cargarComponentesPorEntregable()
        {
            DataSet vo_dataSet = new DataSet();
            int cantidadCompAsociados;
            int cantidadComponentes;
            bool validacionMemoria = false;

            try
            {
                //Se limpia el listbox con los componentes asociados
                if (lbx_compasociados.Items.Count > 0)
                {
                    cantidadCompAsociados = lbx_compasociados.Items.Count;

                    for (int i = 0; i < cantidadCompAsociados; i++ )
                    {
                        lbx_compasociados.Items.RemoveAt(0);
                    }
                }
                //Se limpia el listbox que mantiene la totalidad de los componentes
                if (lbx_componentes.Items.Count > 0)
                {
                    cantidadComponentes = lbx_componentes.Items.Count;

                    for (int i = 0; i < cantidadComponentes; i++)
                    {
                        lbx_componentes.Items.RemoveAt(0);
                    }
                }

                //Se realiza la consulta para obtener todos los componentes asociados a un entregable del proyecto seleccionado
                vo_dataSet = cls_gestorEntregableComponente.selectEntregableComponente(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);

                foreach (DataRow row in vo_dataSet.Tables[0].Rows)
                {

                    cls_entregable vo_entregable = new cls_entregable();
                    cls_componente vo_componente = new cls_componente();
                    cls_entregableComponente vo_entregableComponente = new cls_entregableComponente();

                    vo_entregable.pPK_entregable = Convert.ToInt32(row["PK_entregable"]);
                    vo_componente.pPK_componente = Convert.ToInt32(row["PK_componente"]);
                    vo_componente.pNombre = Convert.ToString(row["nombre"]);

                    vo_entregableComponente.pEntregable = vo_entregable;
                    vo_entregableComponente.pComponente = vo_componente;

                    //Si no se encuentra el elemento en la lista de memoria que mantiene los objetos de base de datos, se agrega
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos.Where(searchLinQ => searchLinQ.pPK_Componente == vo_entregableComponente.pPK_Componente).Count() == 0)
                    {
                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos.Add(vo_entregableComponente);
                    }
                    //Si no se encuentra el elemento en la lista de memoria que se está creando, se agrega
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == vo_entregableComponente.pPK_Entregable).Count() > 0)
                    {
                        validacionMemoria = true;

                        if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Componente == vo_entregableComponente.pPK_Componente).Count() == 0)
                        {
                            ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponenteLista.Add(vo_componente);
                            ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Add(vo_entregableComponente);
                        }
                    }
                }
                //Si la validación es True, se encontró al menos un elemento nuevo en la lista, por lo que se puede proseguir
                if (validacionMemoria)
                {
                    if (lbx_compasociados.Items.Count == 0 && btnNxt != null && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos.Count == 0)
                    {
                        btnNxt.Enabled = false;
                    }
                    else
                    {
                        btnNxt.Enabled = true;
                    }
                }
                else
                {
                    btnNxt.Enabled = false;
                }
                
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los entregables asociados al proyecto.", po_exception);
            }
        }

        #endregion Métodos Componentes

        #region Eventos Componentes

        /// <summary>
        /// Evento que maneja el cambio de índice sobre el dropdownlist de entregables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbx_entregables_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				int entregableSeleccionado;
				entregableSeleccionado = Convert.ToInt32(lbx_entregablesasociados.SelectedValue.ToString());

				cls_entregable vo_entregable = new cls_entregable();
				vo_entregable.pPK_entregable = entregableSeleccionado;

				inicializarComponentesPorEntregable(vo_entregable);
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar obtener la información del entregable.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento para la asigación de un nuevo componente para un entregable específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_asignarComponente_Click(object sender, EventArgs e)
        {
            int entSeleccionado;
            //Se obtiene el entregable seleccionado
            entSeleccionado = Convert.ToInt32(lbx_entregablesasociados.SelectedValue.ToString());

            cls_entregable vo_entregable = new cls_entregable();
            vo_entregable.pPK_entregable = entSeleccionado;
			
			try
			{
				//Se recorre la lista de componentes para validar a quien es el que se va a asignar
				for (int i = lbx_componentes.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_componentes.Items[i].Selected == true)
					{
						cls_componente vo_componente = new cls_componente();
						vo_componente.pPK_componente = Convert.ToInt32(lbx_componentes.Items[i].Value.ToString());
						vo_componente.pNombre = lbx_componentes.Items[i].Text;

						cls_entregableComponente vo_entregableComponente = new cls_entregableComponente();

						vo_entregableComponente.pEntregable = vo_entregable;
						vo_entregableComponente.pComponente = vo_componente;

						//Se recorre la lista de los entregables asociados a proyecto en memoria
						foreach (cls_proyectoEntregable proyEnt in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pProyectoEntregableListaMemoria)
						{
							//Si los entregables coinciden, este es el que se va a asignar
							if (proyEnt.pPK_Entregable == vo_entregable.pPK_entregable)
							{
								//Si en el siguiente nivel, en entregable-componente no se encuentra la asignación, se realiza para ese proyecto-entregable
								if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == vo_entregable.pPK_entregable &&
																													 searchLinQ.pPK_Componente == vo_componente.pPK_componente).Count() == 0)
								{
									((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponenteLista.Add(vo_componente);
									((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Add(vo_entregableComponente);
								}
							}
						}

						lbx_compasociados.Items.Add(lbx_componentes.Items[i]);
						ListItem li = lbx_componentes.Items[i];
						lbx_componentes.Items.Remove(li);

					}
				}

				//Si existe al menos un elemento asociado, se puede continuar
				if (lbx_compasociados.Items.Count > 0)
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar asignar el componente seleccionado.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento que se encarga de remover componentes pertenecientes a un entregable específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_removerComponente_Click(object sender, EventArgs e)
        {
            int entSeleccionado;
            //Se escoge el entregable sobre el que realizara la desasignación
            entSeleccionado = Convert.ToInt32(lbx_entregablesasociados.SelectedValue.ToString());

            cls_entregable vo_entregable = new cls_entregable();
            vo_entregable.pPK_entregable = entSeleccionado;
			
			try
			{
				//Se recorren los componentes ya asociados
				for (int i = lbx_compasociados.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_compasociados.Items[i].Selected == true)
					{
						cls_componente vo_componente = new cls_componente();
						vo_componente.pPK_componente = Convert.ToInt32(lbx_compasociados.Items[i].Value.ToString());
						vo_componente.pNombre = lbx_compasociados.Items[i].Text;

						cls_entregableComponente vo_entregableComponente = new cls_entregableComponente();

						vo_entregableComponente.pEntregable = vo_entregable;
						vo_entregableComponente.pComponente = vo_componente;

						//Se realiza un barrido de las asignaciones posteriores que tuviese el elemento a nivel de memoria, de los subniveles siguientes
						if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == vo_entregable.pPK_entregable &&
																												   searchLinQ.pPK_Componente == vo_componente.pPK_componente).Count() > 0)
							{
								//Se realiza una eliminación de todas las posibles referencias que se presenten a nivel de memoria para el entregable que se está eliminando
								foreach (cls_componentePaquete compPaq in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria)
								{
									if (compPaq.pPK_Componente == vo_componente.pPK_componente)
									{
										foreach (cls_paqueteActividad paqAct in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria)
										{
											if (paqAct.pPK_Componente == compPaq.pPK_Componente)
											{
												((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pActividadLista.RemoveAll(searchLinQ => searchLinQ.pPK_Actividad == paqAct.pPK_Actividad);
											}
										}

										((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == compPaq.pPK_Paquete);

										((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteLista.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == compPaq.pPK_Paquete);
									}
								}
								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Componente == vo_componente.pPK_componente);

								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponenteLista.RemoveAll(searchLinQ => searchLinQ.pPK_componente == vo_componente.pPK_componente);
								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Componente == vo_componente.pPK_componente);
							}

						lbx_componentes.Items.Add(lbx_compasociados.Items[i]);
						ListItem li = lbx_compasociados.Items[i];
						lbx_compasociados.Items.Remove(li);

					}
				}

				//Luego del barrido, si no quedó ningún elemento asociado, no se habilita el botón de siguiente
                if (lbx_compasociados.Items.Count == 0 && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Count == 0 && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaBaseDatos.Count == 0)
				{
					btnNxt.Enabled = false;
				}
				else
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al intentar remover el componente seleccionado.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }


        #endregion Eventos Componentes

        #region Métodos Paquetes

        /// <summary>
        /// Método que cargará la información de los componentes ya asociados para el step de paquetes
        /// </summary>
        private void llenarDatosPaquetes()
        {
            try
            {
                cargarComponentesAsociados();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los paquetes del proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Se obtienen los componentes que se asociaron en el paso anterior del wizard, ya sean provenientes de Base de Datos, o recién asignados
        /// </summary>
        private void cargarComponentesAsociados()
        {
            DataSet vo_dataSet = new DataSet();
            
            try
            {
                //Si se encuentran elementos ya en la lista que se mantiene en memoria, los mismos se respetan
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Count > 0)
                {
                    lbx_componentesasociados.DataSource = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria;
                    lbx_componentesasociados.DataTextField = "pNombreComponente";
                    lbx_componentesasociados.DataValueField = "pPK_Componente";
                    lbx_componentesasociados.DataBind();
                }
                //De lo contrario se pregunta a nivel de base de datos si existen
                else
                {
                    vo_dataSet = cls_gestorEntregableComponente.selectEntregableComponente(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);
                    lbx_componentesasociados.DataSource = vo_dataSet;
                    lbx_componentesasociados.DataTextField = "nombre";
                    lbx_componentesasociados.DataValueField = "PK_componente";
                    lbx_componentesasociados.DataBind();

                }

                //Se envía a cargar los paquetes para el componente seleccionado
                cargarPaquetesPorComponente();

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los datos de la lista de componentes del proyecto.", po_exception);
            }

        }

        /// <summary>
        /// Se manda a cargar la totalidad de los paquetes del proyecto
        /// </summary>
        private void cargarListaPaquetes()
        {
            try
            {
                /*
                 NOta: * Revisar los selects de los listar, para ver que tanto es necesario cambiar los "pNombre" por los nombres de la tabla => "pNombre" - "pNombrePaquete"
                       * Ver si es relevante cambiar los nombres a los listbox
                 */
                lbx_paquetes.DataSource = cls_gestorPaquete.listarPaquetes();
                lbx_paquetes.DataTextField = "pNombre";
                lbx_paquetes.DataValueField = "pPK_paquete";
                lbx_paquetes.DataBind();

                //Se valida dentro de la misma lista de paquetes, que si en algunos de los componentes se encuentra asociado, que se encargue de eliminarlo
                //del listbox, para que no llegue a ser tomado en cuenta entre los paquetes de algún otro componente(son únicos por componente)
                //Este listBox pivot se incorpora debido a que trabajar sobre el listbox original, por el manejo de punteros en C#, se presentan excepciones no controladas
                ListBox lbx_pivot = new ListBox();

                lbx_pivot.DataSource = cls_gestorPaquete.listarPaquetes();
                lbx_pivot.DataTextField = "pNombre";
                lbx_pivot.DataValueField = "pPK_paquete";
                lbx_pivot.DataBind();

                //Si se devuelven paquetes asociados, se remueven los mismos del listBox que mantiene la totalidad de paquetes, estp para mantener la 
                //pertenencia de un paquetes a un sólo componente
                if (lbx_paqasociados.Items.Count > 0)
                {
                    foreach (ListItem item in lbx_paqasociados.Items)
                    {
                        lbx_paquetes.Items.Remove(item);
                    }
                }

                //También se procede a eliminar de la lista de paquetes aquellos que se encuentren asignados en memoria
                foreach (ListItem item in lbx_pivot.Items)
                {
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Paquete.ToString() == item.Value).Count() > 0)
                    {
                        lbx_paquetes.Items.Remove(item);
                    }
                }
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los datos de la lista de paquetes del proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método que obtiene los paquetes asociados al componente que se envía como parámetro
        /// </summary>
        /// <param name="po_componente"></param>
        private void inicializarPaquetesPorComponente(cls_componente po_componente)
        {
			try
			{
				cls_componentePaquete vo_componentePaquete = new cls_componentePaquete();

				vo_componentePaquete.pProyecto = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto;
				vo_componentePaquete.pComponente = po_componente;

				cargarPaquetesPorComponente(vo_componentePaquete);
				
				cargarListaPaquetes();
			}
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener los paquetes asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método que filtra los paquetes del componente seleccionado
        /// </summary>
        /// <param name="po_componentePaquete"></param>
        private void cargarPaquetesPorComponente(cls_componentePaquete po_componentePaquete)
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                //Si la lista de memoria se encuentra vacía, se realiza la consulta en base de datos
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Count == 0)
                {
                    vo_dataSet = cls_gestorComponentePaquete.selectComponentePaquete(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);

                    foreach (DataRow row in vo_dataSet.Tables[0].Rows)
                    {

                        cls_entregable vo_entregable = new cls_entregable();
                        cls_componente vo_componente = new cls_componente();
                        cls_paquete vo_paquete = new cls_paquete();
                        cls_componentePaquete vo_componentePaquete = new cls_componentePaquete();

                        vo_entregable.pPK_entregable = Convert.ToInt32(row["PK_entregable"]);
                        vo_componente.pPK_componente = Convert.ToInt32(row["PK_componente"]);
                        vo_paquete.pPK_Paquete = Convert.ToInt32(row["PK_paquete"]);
                        vo_paquete.pNombre = Convert.ToString(row["nombre"]);

                        vo_componentePaquete.pEntregable = vo_entregable;
                        vo_componentePaquete.pComponente = vo_componente;
                        vo_componentePaquete.pPaquete = vo_paquete;

                        //Si el elemento no se encuentra en la lista de memoria que mantiene los elementos que se obtienen de base de datos, se agrega
                        if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos.Where(searchLinQ => searchLinQ.pPK_Paquete == vo_componentePaquete.pPK_Paquete).Count() == 0)
                        {
                            ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos.Add(vo_componentePaquete);
                        }          
                        //Si el elemento no se encuentra asignado a la lista de memoria, se agrega
                        if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Componente == vo_componentePaquete.pPK_Componente).Count() > 0)
                        {
                            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Paquete == vo_componentePaquete.pPK_Paquete).Count() == 0)
                            {
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteLista.Add(vo_paquete);
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Add(vo_componentePaquete);
                            }                         
                        }
                    }

                }

                //Se respetan los paquetes que hayan sido asignados en memoria
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Componente == po_componentePaquete.pPK_Componente).Count() > 0)
                {
                    lbx_paqasociados.DataSource = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Componente == po_componentePaquete.pPK_Componente);
                    lbx_paqasociados.DataTextField = "pNombrePaquete";
                    lbx_paqasociados.DataValueField = "pPK_Paquete";

                    //Se realiza el Binding luego de saber de donde se tomarán los datos
                    lbx_paqasociados.DataBind();

                    if (lbx_paqasociados.Items.Count > 0)
                    {
                        //Si se leyeron datos asociados, se activa el botón de siguiente
                        btnNxt.Enabled = true;
                    }
                }
                //Si el elemento no se encuentra en memoria, se realiza la consulta en base de datos
                else
                {
                    vo_dataSet = cls_gestorComponentePaquete.selectComponentePaquete(po_componentePaquete);
                    lbx_paqasociados.DataSource = vo_dataSet;
                    lbx_paqasociados.DataTextField = "Nombre";
                    lbx_paqasociados.DataValueField = "PK_Paquete";

                    //Se realiza el Binding luego de saber de donde se tomarán los datos
                    lbx_paqasociados.DataBind();

                    if (lbx_paqasociados.Items.Count > 0)
                    {
                        ListBox lbx_pivot = new ListBox();

                        lbx_pivot.DataSource = vo_dataSet;
                        lbx_pivot.DataTextField = "Nombre";
                        lbx_pivot.DataValueField = "PK_Paquete";
                        lbx_pivot.DataBind();

                        foreach (ListItem item in lbx_pivot.Items)
                        {
                            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == po_componentePaquete.pPK_Entregable &&
                                                                                                              searchLinQ.pPK_Componente == po_componentePaquete.pPK_Componente &&
                                                                                                              searchLinQ.pPK_Paquete == Convert.ToInt32(item.Value)).Count() == 0)
                            {
                                lbx_paqasociados.Items.Remove(item);
                            }
                        }

                        //Si se leyeron datos asociados, se activa el botón de siguiente
                        btnNxt.Enabled = true;
                    }
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los paquetes asociados al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método que obtiene los paquetes asociados a un componente en específico
        /// </summary>
        private void cargarPaquetesPorComponente()
        {
            DataSet vo_dataSet = new DataSet();
            int cantidadPaqAsociados;
            int cantidadPaquetes;
            bool validacionMemoria = false;

            try
            {
                //Se limpia el listbox de paquetes asociados
                if (lbx_paqasociados.Items.Count > 0)
                {
                    cantidadPaqAsociados = lbx_paqasociados.Items.Count;

                    for (int i = 0; i < cantidadPaqAsociados; i++ )
                    {
                        lbx_paqasociados.Items.RemoveAt(0);
                    }
                }
                //Se limpia el listbox de la totalidad de paquetes
                if (lbx_paquetes.Items.Count > 0)
                {
                    cantidadPaquetes = lbx_paquetes.Items.Count;

                    for (int i = 0; i < cantidadPaquetes; i++)
                    {
                        lbx_paquetes.Items.RemoveAt(0);
                    }
                }

                //Se aplica la consulta en base de datos para obtener todos los paquetes asociados a los componentes que se encuentran asignados en memoria para el proyecto
                vo_dataSet = cls_gestorComponentePaquete.selectComponentePaquete(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);

                foreach (DataRow row in vo_dataSet.Tables[0].Rows)
                {

                    cls_entregable vo_entregable = new cls_entregable();
                    cls_componente vo_componente = new cls_componente();
                    cls_paquete vo_paquete = new cls_paquete();
                    cls_componentePaquete vo_componentePaquete = new cls_componentePaquete();

                    vo_entregable.pPK_entregable = Convert.ToInt32(row["PK_entregable"]);
                    vo_componente.pPK_componente = Convert.ToInt32(row["PK_componente"]);
                    vo_paquete.pPK_Paquete = Convert.ToInt32(row["PK_paquete"]);
                    vo_paquete.pNombre = Convert.ToString(row["nombre"]);

                    vo_componentePaquete.pEntregable = vo_entregable;
                    vo_componentePaquete.pComponente = vo_componente;
                    vo_componentePaquete.pPaquete = vo_paquete;

                    //Si le elemento leído no se encuentra en la lista de base de datos, se agrega
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos.Where(searchLinQ => searchLinQ.pPK_Paquete == vo_componentePaquete.pPK_Paquete).Count() == 0)
                    {
                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos.Add(vo_componentePaquete);
                    }
                    //Si el elemento leído no se encuentra en la lista de memoria, se agrega
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Componente == vo_componentePaquete.pPK_Componente).Count() == 1)
                    {
                        validacionMemoria = true;

                        cls_entregableComponente vo_entregableComponente = new cls_entregableComponente();
                        vo_entregableComponente = (cls_entregableComponente)((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria.Find(searchLinQ => searchLinQ.pPK_Componente == vo_componentePaquete.pPK_Componente);

                        if (vo_componentePaquete.pPK_Entregable == vo_entregableComponente.pPK_Entregable && vo_componentePaquete.pPK_Componente == vo_entregableComponente.pPK_Componente)
                        {
                            //Si el paquete no ha sido insertado en memoria, se agrega
                            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Paquete == vo_componentePaquete.pPK_Paquete).Count() == 0)
                            {
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteLista.Add(vo_paquete);
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Add(vo_componentePaquete);
                            }
                        }

                    }
                }
                //Si la validación es True significa que se ha agregado al menos un elemento, por lo cuál el botón de siguiente puede habilitarse
                if (validacionMemoria)
                {
                    if (lbx_paqasociados.Items.Count == 0 && btnNxt != null && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos.Count == 0)
                    {
                        btnNxt.Enabled = false;
                    }
                    else
                    {
                        btnNxt.Enabled = true;
                    }
                }
                else
                {
                    btnNxt.Enabled = false;
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al obtener los paquetes asociados al proyecto.", po_exception);
            }
        }

        #endregion Métodos Paquetes

        #region Eventos Paquetes

        /// <summary>
        /// Evento que valida el cambio del índice del listboz de componentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbx_componentes_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				int componenteSeleccionado;
				componenteSeleccionado = Convert.ToInt32(lbx_componentesasociados.SelectedValue.ToString());

				cls_componente vo_componente = new cls_componente();
				vo_componente.pPK_componente = componenteSeleccionado;

				//Se inicializan los paquetes asociados al componente específico
				inicializarPaquetesPorComponente(vo_componente);
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al seleccionar el entregable de proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento que se encarga de asigar un paquete a un componente específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_asignarPaquete_Click(object sender, EventArgs e)
        {
            int compSeleccionado;
            //Se obtiene el componente al que se le va a agregar el paquete seleccionado
            compSeleccionado = Convert.ToInt32(lbx_componentesasociados.SelectedValue.ToString());
			
			try
			{
				cls_componente vo_componente = new cls_componente();
				vo_componente.pPK_componente = compSeleccionado;

				//Se recorre la lista de paquetes hasta encontrar el que se va a asignar
				for (int i = lbx_paquetes.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_paquetes.Items[i].Selected == true)
					{
						cls_paquete vo_paquete = new cls_paquete();
						vo_paquete.pPK_Paquete = Convert.ToInt32(lbx_paquetes.Items[i].Value.ToString());
						vo_paquete.pNombre = lbx_paquetes.Items[i].Text;

						cls_componentePaquete vo_componentePaquete = new cls_componentePaquete();

						vo_componentePaquete.pComponente = vo_componente;
						vo_componentePaquete.pPaquete = vo_paquete;

						//Se recorre la lista de entregables-componentes en memoria
						foreach (cls_entregableComponente entComp in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria)
						{
							//Si el componente es el mismo que se va a asignar
							if (entComp.pPK_Componente == vo_componente.pPK_componente)
							{
								if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == entComp.pPK_Entregable &&
																														searchLinQ.pPK_Componente == vo_componente.pPK_componente &&
																														searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete).Count() == 0)
								{
									//Se agrega el entregable al que pertenece el componentePaquete, puesto que se necesita al guardar el registro
									vo_componentePaquete.pEntregable = entComp.pEntregable;

									((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteLista.Add(vo_paquete);
									((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Add(vo_componentePaquete);
								}                        
							}
						}

						lbx_paqasociados.Items.Add(lbx_paquetes.Items[i]);
						ListItem li = lbx_paquetes.Items[i];
						lbx_paquetes.Items.Remove(li);

					}
				}

				//Si aún queda al menos un elemento, se habilita el botón de siguiente
				if (lbx_paqasociados.Items.Count > 0)
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al asignar el paquete al proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento encargado de eliminar un paquete asociado a un componente específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_removerPaquete_Click(object sender, EventArgs e)
        {
            int compSeleccionado;
            //Se obtiene el componente al que se le removerá el paquete
            compSeleccionado = Convert.ToInt32(lbx_componentesasociados.SelectedValue.ToString());

            cls_componente vo_componente = new cls_componente();
            vo_componente.pPK_componente = compSeleccionado;
			
			try
			{
				//Se recorre la lista de paquetes asociados
				for (int i = lbx_paqasociados.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_paqasociados.Items[i].Selected == true)
					{
						cls_paquete vo_paquete = new cls_paquete();
						vo_paquete.pPK_Paquete = Convert.ToInt32(lbx_paqasociados.Items[i].Value.ToString());
						vo_paquete.pNombre = lbx_paqasociados.Items[i].Text;

						cls_componentePaquete vo_componentePaquete = new cls_componentePaquete();

						vo_componentePaquete.pComponente = vo_componente;
						vo_componentePaquete.pPaquete = vo_paquete;

						//Se recorre la lista
						foreach (cls_entregableComponente entComp in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pEntregableComponenteListaMemoria)
						{

							if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == entComp.pPK_Entregable &&
																													searchLinQ.pPK_Componente == vo_componente.pPK_componente &&
																													searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete).Count() > 0)
							{

								//Se realiza una eliminación de todas las posibles referencias que se presenten a nivel de memoria para el entregable que se está eliminando
								foreach (cls_paqueteActividad paqAct in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria)
								{
									if (paqAct.pPK_Paquete == vo_paquete.pPK_Paquete)
									{
										((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pActividadLista.RemoveAll(searchLinQ => searchLinQ.pPK_Actividad == paqAct.pPK_Actividad);
									}
								}

								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete);

								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteLista.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete);
								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete);
							}
						}

						

						lbx_paquetes.Items.Add(lbx_paqasociados.Items[i]);
						ListItem li = lbx_paqasociados.Items[i];
						lbx_paqasociados.Items.Remove(li);

					}
				}

				//Si luego de la eliminación no se encuentran elementos en las listas, se deshabilita el botón de continuar
                if (lbx_paqasociados.Items.Count == 0 && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Count == 0 && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaBaseDatos.Count == 0)
				{
					btnNxt.Enabled = false;
				}
				else
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al remover el paquete del proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }


        #endregion Eventos Paquetes

        #region Métodos Actividades

        /// <summary>
        /// Método que obtiene los paquetes que han sido asociados a un proyecto en el step de actividades del wizard de creación
        /// </summary>
        private void llenarDatosActividades()
        {
            try
            {
                cargarPaquetesAsociados();
            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar las actividades asociadas al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Se obtienen las paquetes que se asociaron en el paso anterior del wizard, ya sean provenientes de Base de Datos, o recién asignados
        /// </summary>
        private void cargarPaquetesAsociados()
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                //Si la lista de memoria posee elementos, se respetan y asignan
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Count > 0)
                {
                    lbx_paquetesasociados.DataSource = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria;
                    lbx_paquetesasociados.DataTextField = "pNombrePaquete";
                    lbx_paquetesasociados.DataValueField = "pPK_Paquete";
                    lbx_paquetesasociados.DataBind();
                }
                else
                {
                    vo_dataSet = cls_gestorComponentePaquete.selectComponentePaquete(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);
                    lbx_paquetesasociados.DataSource = vo_dataSet;
                    lbx_paquetesasociados.DataTextField = "nombre";
                    lbx_paquetesasociados.DataValueField = "PK_paquete";
                    lbx_paquetesasociados.DataBind();
                }    
                
                //Ya sea de memoria o de base de datos, se envía a cargar las actividades por paquete
                cargarActividadesPorPaquete();

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los datos de la lista de paquetes del proyecto.", po_exception);
            }

        }

        /// <summary>
        /// Se manda a cargar la totalidad de los actividades del proyecto
        /// </summary>
        private void cargarListaActividades()
        {
            try
            {
                /*
                 NOta: * Revisar los selects de los listar, para ver que tanto es necesario cambiar los "pNombre" por los nombres de la tabla => "pNombre" - "pNombreActividad"
                       * Ver si es relevante cambiar los nombres a los listbox
                 */
                lbx_actividades.DataSource = cls_gestorActividad.listarActividad();
                lbx_actividades.DataTextField = "pNombre";
                lbx_actividades.DataValueField = "pPK_actividad";
                lbx_actividades.DataBind();

                //Se valida dentro de la misma lista de actividades, que si en algunos de las actividades se encuentra asociada, que se encargue de eliminarlo
                //del listbox, para que no llegue a ser tomado en cuenta entre las actividades de algún otro paquete(son únicos por paquete)
                //Este listBox pivot se incorpora debido a que trabajar sobre el listbox original, por el manejo de punteros en C#, se presentan excepciones no controladas
                ListBox lbx_pivot = new ListBox();

                lbx_pivot.DataSource = cls_gestorActividad.listarActividad();
                lbx_pivot.DataTextField = "pNombre";
                lbx_pivot.DataValueField = "pPK_actividad";
                lbx_pivot.DataBind();

                //Si se devuelven actividades asociadas, se remueven los mismos del listBox que mantiene la totalidad de actividades, estp para mantener la 
                //pertenencia de una actividad a un sólo paquete
                if (lbx_actasociadas.Items.Count > 0)
                {
                    foreach (ListItem item in lbx_actasociadas.Items)
                    {
                        lbx_actividades.Items.Remove(item);
                    }
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los datos de la lista de actividades del proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método que inicializa las actividades asignadas a un paquete específico
        /// </summary>
        /// <param name="po_paquete"></param>
        private void inicializarActividadesPorPaquete(cls_paquete po_paquete)
        {
			try
			{
				cls_paqueteActividad vo_paqueteActividad = new cls_paqueteActividad();

				vo_paqueteActividad.pProyecto = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto;
				vo_paqueteActividad.pPaquete = po_paquete;

				//Se envía a cargas las actividaes por paquete
				cargarActividadesPorPaquete(vo_paqueteActividad);
				//Se carga la totalidad de las actividades que aún pueden ser escogidas
				cargarListaActividades();
			}
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al ontener las actividades asociadas a proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método que carga las actividades para un paquete en específico
        /// </summary>
        /// <param name="po_paqueteActividad"></param>
        private void cargarActividadesPorPaquete(cls_paqueteActividad po_paqueteActividad)
        {
            DataSet vo_dataSet = new DataSet();

            try
            {
                //Si se encuentran elementos en la lista de memoria, se verifica en estos
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Count == 0)
                {
                    vo_dataSet = cls_gestorPaqueteActividad.selectPaqueteActividad(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);

                    foreach (DataRow row in vo_dataSet.Tables[0].Rows)
                    {

                        cls_entregable vo_entregable = new cls_entregable();
                        cls_componente vo_componente = new cls_componente();
                        cls_paquete vo_paquete = new cls_paquete();
                        cls_actividad vo_actividad = new cls_actividad();
                        cls_paqueteActividad vo_paqueteActividad = new cls_paqueteActividad();

                        vo_entregable.pPK_entregable = Convert.ToInt32(row["PK_entregable"]);
                        vo_componente.pPK_componente = Convert.ToInt32(row["PK_componente"]);
                        vo_paquete.pPK_Paquete = Convert.ToInt32(row["PK_paquete"]);
                        vo_actividad.pPK_Actividad = Convert.ToInt32(row["PK_actividad"]);
                        vo_actividad.pNombre = Convert.ToString(row["nombre"]);

                        vo_paqueteActividad.pEntregable = vo_entregable;
                        vo_paqueteActividad.pComponente = vo_componente;
                        vo_paqueteActividad.pPaquete = vo_paquete;
                        vo_paqueteActividad.pActividad = vo_actividad;

                        //Si en la lista de memoria que mantiene los objetos leídos a nivel de base de datos, no se encuentra el elemente, se agrega
                        if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos.Where(searchLinQ => searchLinQ.pPK_Actividad == vo_paqueteActividad.pPK_Actividad).Count() == 0)
                        {
                            ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos.Add(vo_paqueteActividad);
                        }
                        //Si en la lista de memoria no se encuentra el elemento, se agrega
                        if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == vo_paqueteActividad.pPK_Entregable &&
                                                                                                                searchLinQ.pPK_Componente == vo_paqueteActividad.pPK_Componente &&
                                                                                                                searchLinQ.pPK_Paquete == vo_paqueteActividad.pPK_Paquete).Count() > 0)
                        {
                            ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pActividadLista.Add(vo_actividad);
                            ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Add(vo_paqueteActividad);
                        }
                    }

                }

                //Si el elemento se encuentra en la lista de memoria, se respeta para la asignación
                if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Paquete == po_paqueteActividad.pPK_Paquete).Count() > 0)
                {
                    lbx_actasociadas.DataSource = ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Paquete == po_paqueteActividad.pPK_Paquete);
                    lbx_actasociadas.DataTextField = "pNombreActividad";
                    lbx_actasociadas.DataValueField = "pPK_Actividad";

                    //Se realiza el Binding luego de saber de donde se tomarán los datos
                    lbx_actasociadas.DataBind();

                    if (lbx_actasociadas.Items.Count > 0)
                    {
                        //Si se leyeron datos asociados, se activa el botón de siguiente
                        btnNxt.Enabled = true;
                    }
                }
                else
                {
                    vo_dataSet = cls_gestorPaqueteActividad.selectPaqueteActividad(po_paqueteActividad);
                    lbx_actasociadas.DataSource = vo_dataSet;
                    lbx_actasociadas.DataTextField = "Nombre";
                    lbx_actasociadas.DataValueField = "PK_Actividad";

                    //Se realiza el Binding luego de saber de donde se tomarán los datos
                    lbx_actasociadas.DataBind();

                    //Si se encuentran elementos asociados
                    if (lbx_actasociadas.Items.Count > 0)
                    {
                        ListBox lbx_pivot = new ListBox();

                        lbx_pivot.DataSource = vo_dataSet;
                        lbx_pivot.DataTextField = "Nombre";
                        lbx_pivot.DataValueField = "PK_Actividad";
                        lbx_pivot.DataBind();

                        //Si la actividad ya se encuentra asociada, se elimina del listbox de asociación
                        foreach (ListItem item in lbx_pivot.Items)
                        {
                            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == po_paqueteActividad.pPK_Entregable &&
                                                                                                                   searchLinQ.pPK_Componente == po_paqueteActividad.pPK_Componente &&
                                                                                                                   searchLinQ.pPK_Paquete == po_paqueteActividad.pPK_Paquete &&
                                                                                                                   searchLinQ.pPK_Actividad == Convert.ToInt32(item.Value)).Count() == 0)
                            {
                                lbx_actasociadas.Items.Remove(item);
                            }
                        }
                    }

                    //Si se leyeron datos asociados, se activa el botón de siguiente
                    btnNxt.Enabled = true;
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los actividades asociadas al proyecto.", po_exception);
            }
        }

        /// <summary>
        /// Método que obtiene las actividades asociadas a un paquete específico
        /// </summary>
        private void cargarActividadesPorPaquete()
        {
            DataSet vo_dataSet = new DataSet();
            int cantidadActAsociadas;
            int cantidadActividades;
            bool validacionMemoria = false;

            try
            {
                //Se limpia el listbox que mantiene la asociación de actividades
                if (lbx_actasociadas.Items.Count > 0)
                {
                    cantidadActAsociadas = lbx_actasociadas.Items.Count;

                    for (int i = 0; i < cantidadActAsociadas; i++)
                    {
                        lbx_actasociadas.Items.RemoveAt(0);
                    }
                }
                //Se limpia el listbox que obtiene la totalidad de actividades
                if (lbx_actividades.Items.Count > 0)
                {
                    cantidadActividades = lbx_actividades.Items.Count;

                    for (int i = 0; i < cantidadActividades; i++)
                    {
                        lbx_actividades.Items.RemoveAt(0);
                    }
                }
                
                //Se realiza la consulta en base de datos para obtener las actividades asociadas a los paquetes del proyecto
                vo_dataSet = cls_gestorPaqueteActividad.selectPaqueteActividad(((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto);

                foreach (DataRow row in vo_dataSet.Tables[0].Rows)
                {

                    cls_entregable vo_entregable = new cls_entregable();
                    cls_componente vo_componente = new cls_componente();
                    cls_paquete vo_paquete = new cls_paquete();
                    cls_actividad vo_actividad = new cls_actividad();
                    cls_paqueteActividad vo_paqueteActividad = new cls_paqueteActividad();

                    vo_entregable.pPK_entregable = Convert.ToInt32(row["PK_entregable"]);
                    vo_componente.pPK_componente = Convert.ToInt32(row["PK_componente"]);
                    vo_paquete.pPK_Paquete = Convert.ToInt32(row["PK_paquete"]);
                    vo_actividad.pPK_Actividad = Convert.ToInt32(row["PK_actividad"]);
                    vo_actividad.pNombre = Convert.ToString(row["nombre"]);

                    vo_paqueteActividad.pEntregable = vo_entregable;
                    vo_paqueteActividad.pComponente = vo_componente;
                    vo_paqueteActividad.pPaquete = vo_paquete;
                    vo_paqueteActividad.pActividad = vo_actividad;

                    //El filtro aquí se realiza con el paquete y la actividad, esto debido a que una actividad si puede encontrarse ya asignada a varios paquetes
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos.Where(searchLinQ => searchLinQ.pPK_Paquete == vo_paqueteActividad.pPK_Paquete && 
                                                                                                             searchLinQ.pPK_Actividad == vo_paqueteActividad.pPK_Actividad).Count() == 0)
                    {
                        ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos.Add(vo_paqueteActividad);
                    }
                    //Si el componente-paquete que se va a asignar ya existe en memoria
                    if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Where(searchLinQ => searchLinQ.pPK_Paquete == vo_paqueteActividad.pPK_Paquete).Count() == 1)
                    {
                        validacionMemoria = true;

                        cls_componentePaquete vo_componentePaquete = new cls_componentePaquete();
                        vo_componentePaquete = (cls_componentePaquete)((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria.Find(searchLinQ => searchLinQ.pPK_Paquete == vo_paqueteActividad.pPK_Paquete);

                        //Si se está asignando sobre uno que ya existe en memoria, se tiene que corroborar con toda la llave primaria
                        if (vo_paqueteActividad.pPK_Entregable == vo_componentePaquete.pPK_Entregable && vo_paqueteActividad.pPK_Componente == vo_componentePaquete.pPK_Componente && vo_paqueteActividad.pPK_Paquete == vo_componentePaquete.pPK_Paquete)
                        {
                            //Si la actividad no ha sido insertado en memoria, se agrega
                            if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Paquete == vo_paqueteActividad.pPK_Paquete &&
                                                                                                                   searchLinQ.pPK_Actividad == vo_paqueteActividad.pPK_Actividad).Count() == 0)
                            {
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pActividadLista.Add(vo_actividad);
                                ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Add(vo_paqueteActividad);
                            }
                        }

                    }
                }

                //Si la validación devuelve un True, se ha asignado almenos un elemento, por lo cual se puede habilitar el botón de siguiente
                if (validacionMemoria)
                {
                    if (lbx_actasociadas.Items.Count == 0 && btnNxt != null && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos.Count == 0)
                    {
                        btnNxt.Enabled = false;
                    }
                    else
                    {
                        btnNxt.Enabled = true;
                    }
                }
                else
                {
                    btnNxt.Enabled = false;
                }

            }
            catch (Exception po_exception)
            {
                throw new Exception("Ocurrió un error al cargar los actividades asociadas al proyecto.", po_exception);
            }
        }

        #endregion Métodos Actividades

        #region Eventos Actividades

        /// <summary>
        /// Evento que maneja el cambio de índice para el listbox de paquete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbx_paquetes_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				int paqueteSeleccionado;
				paqueteSeleccionado = Convert.ToInt32(lbx_paquetesasociados.SelectedValue.ToString());

				cls_paquete vo_paquete = new cls_paquete();
				vo_paquete.pPK_Paquete = paqueteSeleccionado;

				//Se envía a inicializa las actividades según el paquete seleccionado
				inicializarActividadesPorPaquete(vo_paquete);
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al obtener la información del paquete seleccionado.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento encargado de asignar la actividad a uno o varios paquetes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_asignarActividad_Click(object sender, EventArgs e)
        {
            int paqueteSeleccionado;
            paqueteSeleccionado = Convert.ToInt32(lbx_paquetesasociados.SelectedValue.ToString());

            cls_paquete vo_paquete = new cls_paquete();
            vo_paquete.pPK_Paquete = paqueteSeleccionado;
			try
			{
				//Se recorre la lista de actividades
				for (int i = lbx_actividades.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_actividades.Items[i].Selected == true)
					{
						cls_actividad vo_actividad = new cls_actividad();
						vo_actividad.pPK_Actividad = Convert.ToInt32(lbx_actividades.Items[i].Value.ToString());
						vo_actividad.pNombre = lbx_actividades.Items[i].Text;

						cls_paqueteActividad vo_paqueteActividad = new cls_paqueteActividad();

						vo_paqueteActividad.pPaquete = vo_paquete;
						vo_paqueteActividad.pActividad = vo_actividad;

						//Se recorre los elementos de la lista de memoria
						foreach (cls_componentePaquete compPaq in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria)
						{
							if (compPaq.pPK_Paquete == vo_paquete.pPK_Paquete)
							{
								//Si la actividad no se encuentra asignada para ese paquete, se agrega
								if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == compPaq.pPK_Entregable &&
																												 searchLinQ.pPK_Componente == compPaq.pPK_Componente &&
																												 searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete &&
																												 searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad).Count() == 0)
								{
									//Se agregam el entregable y componente al que pertenece el paqueteActividad, puesto que se necesita al guardar el registro
									vo_paqueteActividad.pEntregable = compPaq.pEntregable;
									vo_paqueteActividad.pComponente = compPaq.pComponente;

									((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pActividadLista.Add(vo_actividad);
									((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Add(vo_paqueteActividad);
								}
							}
						}

						lbx_actasociadas.Items.Add(lbx_actividades.Items[i]);
						ListItem li = lbx_actividades.Items[i];
						lbx_actividades.Items.Remove(li);

					}
				}

				//Si al menos se encuetra un elemento asociado, se puede habilitar el botón de siguiente
				if (lbx_actasociadas.Items.Count > 0)
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al asignar la actividad al proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }

        /// <summary>
        /// Evento para la eliminación de una actividad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_removerActividad_Click(object sender, EventArgs e)
        {
            int paqueteSeleccionado;
            paqueteSeleccionado = Convert.ToInt32(lbx_paquetesasociados.SelectedValue.ToString());

            cls_paquete vo_paquete = new cls_paquete();
            vo_paquete.pPK_Paquete = paqueteSeleccionado;
			try
			{
				//Se recorre la lista de actividades asociadas
				for (int i = lbx_actasociadas.Items.Count - 1; i >= 0; i--)
				{
					if (lbx_actasociadas.Items[i].Selected == true)
					{
						cls_actividad vo_actividad = new cls_actividad();
						vo_actividad.pPK_Actividad = Convert.ToInt32(lbx_actasociadas.Items[i].Value.ToString());
						vo_actividad.pNombre = lbx_actasociadas.Items[i].Text;

						cls_paqueteActividad vo_paqueteActividad = new cls_paqueteActividad();

						vo_paqueteActividad.pPaquete = vo_paquete;
						vo_paqueteActividad.pActividad = vo_actividad;

						//Se recorren los elementos de la lista de memoria
						foreach (cls_componentePaquete compPaq in ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pComponentePaqueteListaMemoria)
						{
							if (((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Where(searchLinQ => searchLinQ.pPK_Entregable == compPaq.pPK_Entregable &&
																											 searchLinQ.pPK_Componente == compPaq.pPK_Componente &&
																											 searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete &&
																											 searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad).Count() > 0)
							{
								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pActividadLista.RemoveAll(searchLinQ => searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad);
								((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.RemoveAll(searchLinQ => searchLinQ.pPK_Paquete == vo_paquete.pPK_Paquete &&
																													   searchLinQ.pPK_Actividad == vo_actividad.pPK_Actividad);
							}
						}

						lbx_actividades.Items.Add(lbx_actasociadas.Items[i]);
						ListItem li = lbx_actasociadas.Items[i];
						lbx_actasociadas.Items.Remove(li);

					}
				}

				//Si no se encuentra al menos un elemento, no se habilita el botón de siguiente
                if (lbx_actasociadas.Items.Count == 0 && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaMemoria.Count == 0 && ((CSLA.web.App_Variables.cls_variablesSistema)this.Session[CSLA.web.App_Constantes.cls_constantes.VARIABLES]).vs_proyecto.pPaqueteActividadListaBaseDatos.Count == 0)
				{
					btnNxt.Enabled = false;
				}
				else
				{
					btnNxt.Enabled = true;
				}
			}
            catch (Exception po_exception)
            {
                String vs_error_usuario = "Ocurrió un error al remover la actividad del proyecto.";
                this.lanzarExcepcion(po_exception, vs_error_usuario);
            }
        }


        #endregion Eventos Actividades

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

        #endregion Seguridad

    }
}