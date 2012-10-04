using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COSEVI.CSLA.lib.entidades.mod.Administracion;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_proyecto.cs
//
// Clase que contiene la información relacionada con los proyectos
// del Consejo de Seguridad Vial.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Cristian Arce            05  - 15  - 2011    Se crea la clase.
// Cristian Arce            31  - 10  - 2011    Se crea la clase.//							
// 
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento
{
	public class cls_proyecto
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_proyecto.
        /// </summary>
        public cls_proyecto()
        {
            this.estadoProyecto = new cls_estado();
            this.entregableLista = new List<cls_entregable>();
            this.componenteLista = new List<cls_componente>();
            this.paqueteLista = new List<cls_paquete>();
            this.actividadLista = new List<cls_actividad>();
            this.asignacionActividadListaMemoria = new List<cls_asignacionActividad>();
            this.asignacionActividadListaBaseDatos = new List<cls_asignacionActividad>();

            this.proyectoEntregableListaMemoria = new List<cls_proyectoEntregable>();
            this.entregableComponenteListaMemoria = new List<cls_entregableComponente>();
            this.componentePaqueteListaMemoria = new List<cls_componentePaquete>();
            this.paqueteActividadListaMemoria = new List<cls_paqueteActividad>();

        }

        #endregion

        #region Propiedades

        public int pPK_proyecto
        {
            get { return PK_proyecto; }
            set { this.PK_proyecto = value; }
        }

		public int pFK_estado
        {
            get { return pEstado.pPK_estado; }
            set { this.pEstado.pPK_estado = value; }
        }

        public string pDescripcionEstado
        {
            get { return pEstado.pDescripcion; }
            set { this.pEstado.pDescripcion = value; }
        }

		public string pNombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

		public string pDescripcion
        {
            get { return descripcion; }
            set { this.descripcion = value; }
        }

		public string pObjetivo
        {
            get { return objetivo; }
            set { this.objetivo = value; }
        }

		public string pMeta
        {
            get { return meta; }
            set { this.meta = value; }
        }

		public DateTime pFechaInicio
        {
            get { return fechaInicio; }
            set { this.fechaInicio = value; }
        }

		public DateTime pFechaFin
        {
            get { return fechaFin; }
            set { this.fechaFin = value; }
        }

		public decimal pHorasAsignadas
        {
            get { return horasAsignadas; }
            set { this.horasAsignadas = value; }
        }

		public decimal pHorasReales
        {
            get { return horasReales; }
            set { this.horasReales = value; }
        }

        public string pNombreEstado
        {
            get { return pEstado.pDescripcion; }
            set { this.pEstado.pDescripcion = value; }
        }

        public cls_estado pEstado
        {
            get { return estadoProyecto; }
            set { this.estadoProyecto = value; }
        }

        public List<cls_departamentoProyecto> pDptoProyLista
        {
            get { return dptoProyLista; }
            set { this.dptoProyLista = value; }
        }

        public List<cls_departamento> pDepartamentoLista
        {
            get { return departamentoLista; }
            set { this.departamentoLista = value; }
        }

        #endregion

		#region Atributos

        /// <summary>
        /// Código del proyecto
        /// </summary>
 	    private int PK_proyecto;

        /// <summary>
        /// Código del estado
        /// </summary>
	    private int FK_estado;

        /// <summary>
        /// Nombre del proyecto
        /// </summary>
	    private string nombre;

        /// <summary>
        /// Descripción del proyecto
        /// </summary>
	    private string descripcion;

        /// <summary>
        /// Objetivo del proyecto
        /// </summary>
	    private string objetivo;

        /// <summary>
        /// Meta del proyecto
        /// </summary>
	    private string meta;

        /// <summary>
        /// Fecha de Inicio del proyecto
        /// </summary>
	    private DateTime fechaInicio;

        /// <summary>
        /// Fecha de Finalización del proyecto
        /// </summary>
	    private DateTime fechaFin;

        /// <summary>
        /// Horas asignadas para el proyecto
        /// </summary>
	    private decimal horasAsignadas;

        /// <summary>
        /// Horas reales de duración del proyecto
        /// </summary>
	    private decimal horasReales;

	    private cls_estado estadoProyecto;

        private List<cls_departamento> departamentoLista = new List<cls_departamento>();

        private List<cls_departamentoProyecto> dptoProyLista = new List<cls_departamentoProyecto>();

        private string usuarioTransaccion;

        public string pUsuarioTransaccion
        {
            get { return usuarioTransaccion; }
            set { usuarioTransaccion = value; }
        }

        #endregion


        #region Creación de Proyecto


        #region Atributos

        private List<cls_entregable> entregableLista = new List<cls_entregable>();

        private List<cls_componente> componenteLista = new List<cls_componente>();

        private List<cls_paquete> paqueteLista = new List<cls_paquete>();

        private List<cls_actividad> actividadLista = new List<cls_actividad>();


        private List<cls_proyectoEntregable> proyectoEntregableListaMemoria = new List<cls_proyectoEntregable>();

        private List<cls_entregableComponente> entregableComponenteListaMemoria = new List<cls_entregableComponente>();

        private List<cls_componentePaquete> componentePaqueteListaMemoria = new List<cls_componentePaquete>();

        private List<cls_paqueteActividad> paqueteActividadListaMemoria = new List<cls_paqueteActividad>();


        private List<cls_proyectoEntregable> proyectoEntregableListaBaseDatos = new List<cls_proyectoEntregable>();

        private List<cls_entregableComponente> entregableComponenteListaBaseDatos = new List<cls_entregableComponente>();

        private List<cls_componentePaquete> componentePaqueteListaBaseDatos = new List<cls_componentePaquete>();

        private List<cls_paqueteActividad> paqueteActividadListaBaseDatos = new List<cls_paqueteActividad>();


        #endregion Atributos


        #region Propiedades

        public List<cls_entregable> pEntregableLista
        {
            get { return entregableLista; }
            set { this.entregableLista = value; }
        }

        public List<cls_componente> pComponenteLista
        {
            get { return componenteLista; }
            set { this.componenteLista = value; }
        }

        public List<cls_paquete> pPaqueteLista
        {
            get { return paqueteLista; }
            set { this.paqueteLista = value; }
        }

        public List<cls_actividad> pActividadLista
        {
            get { return actividadLista; }
            set { this.actividadLista = value; }
        }


        public List<cls_proyectoEntregable> pProyectoEntregableListaMemoria
        {
            get { return proyectoEntregableListaMemoria; }
            set { this.proyectoEntregableListaMemoria = value; }
        }

        public List<cls_entregableComponente> pEntregableComponenteListaMemoria
        {
            get { return entregableComponenteListaMemoria; }
            set { this.entregableComponenteListaMemoria = value; }
        }

        public List<cls_componentePaquete> pComponentePaqueteListaMemoria
        {
            get { return componentePaqueteListaMemoria; }
            set { this.componentePaqueteListaMemoria = value; }
        }

        public List<cls_paqueteActividad> pPaqueteActividadListaMemoria
        {
            get { return paqueteActividadListaMemoria; }
            set { this.paqueteActividadListaMemoria = value; }
        }


        public List<cls_proyectoEntregable> pProyectoEntregableListaBaseDatos
        {
            get { return proyectoEntregableListaBaseDatos; }
            set { this.proyectoEntregableListaBaseDatos = value; }
        }

        public List<cls_entregableComponente> pEntregableComponenteListaBaseDatos
        {
            get { return entregableComponenteListaBaseDatos; }
            set { this.entregableComponenteListaBaseDatos = value; }
        }

        public List<cls_componentePaquete> pComponentePaqueteListaBaseDatos
        {
            get { return componentePaqueteListaBaseDatos; }
            set { this.componentePaqueteListaBaseDatos = value; }
        }

        public List<cls_paqueteActividad> pPaqueteActividadListaBaseDatos
        {
            get { return paqueteActividadListaBaseDatos; }
            set { this.paqueteActividadListaBaseDatos = value; }
        }


        #endregion Propiedades


        #endregion Creación de Proyecto


        #region Asignacion Actividades


        #region Atributos

        private List<cls_asignacionActividad> actividadesPaqueteLista = new List<cls_asignacionActividad>();

        private List<cls_asignacionActividad> asignacionActividadListaMemoria = new List<cls_asignacionActividad>();
        private List<cls_asignacionActividad> asignacionActividadListaBaseDatos = new List<cls_asignacionActividad>();

        #endregion Atributos

        #region Propiedades

        public List<cls_asignacionActividad> pActividadesPaqueteLista
        {
            get { return actividadesPaqueteLista; }
            set { this.actividadesPaqueteLista = value; }
        }

        public List<cls_asignacionActividad> pAsignacionActividadListaMemoria
        {
            get { return asignacionActividadListaMemoria; }
            set { this.asignacionActividadListaMemoria = value; }
        }

        public List<cls_asignacionActividad> pAsignacionActividadListaBaseDatos
        {
            get { return asignacionActividadListaBaseDatos; }
            set { this.asignacionActividadListaBaseDatos = value; }
        }

        public List<cls_paquete> pPaquetesAsignadosLista
        {
            get {
                    List<cls_paquete> vl_paquete = new List<cls_paquete>();
                    cls_paquete vo_paquete;

                    //foreach (cls_actividadAsignada vo_asignacionActividad in actividadAsignada)
                    //{
                    //    if (vl_paquete.Where(test => test.pPK_Paquete == vo_asignacionActividad.pPK_Paquete).Count() == 0)
                    //    {
                    //        vo_paquete = new cls_paquete();
                    //        vo_paquete.pPK_Paquete = vo_asignacionActividad.pPK_Paquete;
                    //        vo_paquete.pNombre = vo_asignacionActividad.pNombrePaquete;

                    //        vl_paquete.Add(vo_paquete);
                    //    }
                    //}

                    return vl_paquete; 
                }
        }

        public void listarPaquetesAsignados()
        {
            List<cls_paquete> vl_paquete = new List<cls_paquete>();
            cls_paquete vo_paquete;

            //foreach (cls_actividadAsignada vo_asignacionActividad in actividadAsignada)
            //{ 
            //    if(vl_paquete.Where(test => test.pPK_Paquete == vo_asignacionActividad.pPK_Paquete).Count() == 0)
            //    {
            //        vo_paquete = new cls_paquete();
            //        vo_paquete.pPK_Paquete = vo_asignacionActividad.pPK_Paquete;
            //        vo_paquete.pNombre = vo_asignacionActividad.pNombrePaquete;

            //        vl_paquete.Add(vo_paquete);
            //    }
            //}

        }

        #endregion Propiedades


        #endregion Asignacion Actividades

    }

}

