using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COSEVI.CSLA.lib.entidades.mod.Administracion;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_actividad.cs
//
// Clase que asocia la información de los proyectos con las actividades por realizar de los mismos
// del Consejo de Seguridad Vial.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Cristian Arce            05  - 15    2011    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento
{
    /// <summary>
    /// Clase que asocia la información de los proyectos con las actividades por realizar de los mismos
    // del Consejo de Seguridad Vial.
    /// </summary>
	public class cls_asignacionActividad
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_actividad.
        /// </summary>
        public cls_asignacionActividad()
        {
            this.estado = new cls_estado();
            this.usuario = new cls_usuario();
            this.usuarioLista = new List<cls_usuario>();
        }

        #endregion
       
        #region Propiedades

        public int pPK_Actividad
        {
            get { return PK_actividad; }
            set { this.PK_actividad = value; }
        }


        public int pPK_Paquete
        {
            get { return PK_paquete; }
            set { this.PK_paquete = value; }
        }


        public int pPK_Componente
        {
            get { return PK_componente; }
            set { this.PK_componente = value; }
        }


        public int pPK_Entregable
        {
            get { return PK_entregable; }
            set { this.PK_entregable = value; }
        }


        public int pPK_Proyecto
        {
            get { return PK_proyecto; }
            set { this.PK_proyecto = value; }
        }


        public string pPK_Usuario
        {
            get { return usuario.pPK_usuario; }
            set { this.usuario.pPK_usuario = value; }
        }

        public string pUsuarioPivot
        {
            get { return usuarioPivot; }
            set { this.usuarioPivot = value; }
        }

        public string pNombreUsuario
        {
            get { return usuario.pNombre; }
            set { this.usuario.pNombre = value; }
        }


        public int pFK_Estado
        {
            get { return estado.pPK_estado; }
            set { this.estado.pPK_estado = value; }
        }


        public string pNombrePaquete
        {
            get { return nombrePaquete; }
            set { this.nombrePaquete = value; }
        }


        public string pNombreActividad
        {
            get { return nombreActividad; }
            set { this.nombreActividad  = value; }
        }


        public string pDescripcion
        {
            get { return descripcion; }
            set { this.descripcion = value; }
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


        public cls_estado pEstado
        {
            get { return estado; }
            set { this.estado = value; }
        }

        public cls_usuario pUsuario
        {
            get { return usuario; }
            set { this.usuario = value; }
        }

        public cls_actividad pActividad
        {
            get { return actividad; }
            set { actividad = value; }
        }

        public List<cls_usuario> pUsuarioLista
        {
            get { return usuarioLista; }
            set { this.usuarioLista = value; }
        }


        #endregion 

		#region Atributos

        /// <summary>
        /// Código de actividad
        /// </summary>
 	    private int PK_actividad;

        /// <summary>
        /// Instancia de la actividad.
        /// </summary>
        private cls_actividad actividad;

        /// <summary>
        /// Código de paquete
        /// </summary>
        private int PK_paquete;

        /// <summary>
        /// Código de componente
        /// </summary>
        private int PK_componente;

        /// <summary>
        /// Código de entregable
        /// </summary>
        private int PK_entregable;

        /// <summary>
        /// Código de proyecto
        /// </summary>
        private int PK_proyecto;

        /// <summary>
        /// Nombre del paquete
        /// </summary>
        private string nombrePaquete;

        /// <summary>
        /// Nombre de la actividad
        /// </summary>
        private string nombreActividad;

        /// <summary>
        /// Descripción de la asignación de actividad
        /// </summary>
	    private string descripcion;

        /// <summary>
        /// Fecha Inicio de la asignación de actividad
        /// </summary>
        private DateTime fechaInicio;

        /// <summary>
        /// Fecha Fin de la asignación de actividad
        /// </summary>
	    private DateTime fechaFin;

        /// <summary>
        /// Horas asignadas a la asignación de actividad
        /// </summary>
	    private decimal horasAsignadas;

        /// <summary>
        /// Horas reales de la asignación de la asignación de actividad
        /// </summary>
        private decimal horasReales;

        private cls_estado estado;

	    private cls_usuario usuario;

        private List<cls_usuario> usuarioLista;

        /// <summary>
        /// Usuario pivot al guardar la asignación de la actividad
        /// </summary>
        private string usuarioPivot;

        #endregion

    }

}

