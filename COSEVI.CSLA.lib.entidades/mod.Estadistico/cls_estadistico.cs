using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_totalidadLabores.cs
//
/// Clase que asocia la información de las actividades de los proyectos
// del Consejo de Seguridad Vial.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Cristian Arce            06  - 01    2012    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================

namespace COSEVI.CSLA.lib.entidades.mod.Estadistico
{
    /// <summary>
    /// Clase utilizada para la obtención de datos en el gráfico de labores por proyecto.
    /// </summary>
	public class cls_totalidadLabores
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_totalidadLabores.
        /// </summary>
        public cls_totalidadLabores()
        {
        }

        #endregion

        #region Propiedades

        public int pPK_proyecto
        {
            get { return PK_proyecto; }
            set { this.PK_proyecto = value; }
        }

        public string pPK_usuario
        {
            get { return PK_usuario; }
            set { this.PK_usuario = value; }
        }

        public string pTipoLabor
        {
            get { return tipoLabor; }
            set { this.tipoLabor = value; }
        }

        public int pCantidad
        {
            get { return cantidad; }
            set { this.cantidad = value; }
        }

        #endregion

		#region Atributos

        /// <summary>
        /// Código del proyecto
        /// </summary>
 	    private int PK_proyecto;

        /// <summary>
        /// Código del usuario
        /// </summary>
        private string PK_usuario;

        /// <summary>
        /// Tipo de labor para el desgloce
        /// </summary>
	    private string tipoLabor;

        /// <summary>
        /// Cantidad de registros
        /// </summary>
        private int cantidad;

        #endregion

    }

    /// <summary>
    /// Clase utilizada para la obtención de datos en el gráfico de Top actividades por proyecto.
    /// </summary>
    public class cls_topActividades
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_topActividades.
        /// </summary>
        public cls_topActividades()
        {
        }

        #endregion

        #region Propiedades

        public int pPK_proyecto
        {
            get { return PK_proyecto; }
            set { this.PK_proyecto = value; }
        }

        public string pPK_usuario
        {
            get { return PK_usuario; }
            set { this.PK_usuario = value; }
        }

        public string pNombreActividad
        {
            get { return nombreActividad; }
            set { this.nombreActividad = value; }
        }

        public decimal pCantidadHoras
        {
            get { return cantidadHoras; }
            set { this.cantidadHoras = value; }
        }

        public DateTime pFechaDesde
        {
            get { return fechaDesde; }
            set { this.fechaDesde = value; }
        }

        public DateTime pFechaHasta
        {
            get { return fechaHasta; }
            set { this.fechaHasta = value; }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Código del proyecto
        /// </summary>
        private int PK_proyecto;

        /// <summary>
        /// Código del usuario
        /// </summary>
        private string PK_usuario;

        /// <summary>
        /// Nombre de la actividad para el desgloce
        /// </summary>
        private string nombreActividad;

        /// <summary>
        /// Cantidad de registros
        /// </summary>
        private decimal cantidadHoras;

        /// <summary>
        /// Fecha Inicio del filtro
        /// </summary>
        private DateTime fechaDesde;

        /// <summary>
        /// Fecha Fin del filtro
        /// </summary>
        private DateTime fechaHasta;

        #endregion

    }

    /// <summary>
    /// Clase utilizada para la obtención de datos en el gráfico de comparación de horas de actividades por proyecto.
    /// </summary>
    public class cls_compHorasActividades
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_compHorasActividades.
        /// </summary>
        public cls_compHorasActividades()
        {
        }

        #endregion

        #region Propiedades

        public int pPK_proyecto
        {
            get { return PK_proyecto; }
            set { this.PK_proyecto = value; }
        }

        public int pPK_paquete
        {
            get { return PK_paquete; }
            set { this.PK_paquete = value; }
        }

        public string pPK_usuario
        {
            get { return PK_usuario; }
            set { this.PK_usuario = value; }
        }

        public string pNombreActividad
        {
            get { return nombreActividad; }
            set { this.nombreActividad = value; }
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

        #endregion

        #region Atributos

        /// <summary>
        /// Código del proyecto
        /// </summary>
        private int PK_proyecto;

        /// <summary>
        /// Código del paquete
        /// </summary>
        private int PK_paquete;

        /// <summary>
        /// Código del usuario
        /// </summary>
        private string PK_usuario;

        /// <summary>
        /// Nombre de la actividad para el desgloce
        /// </summary>
        private string nombreActividad;
        
        /// <summary>
        /// Horas asignadas a la actividad
        /// </summary>
        private decimal horasAsignadas;

        /// <summary>
        /// Horas reales invertidas en la actividad
        /// </summary>
        private decimal horasReales;

        #endregion

    }

    /// <summary>
    /// Clase utilizada para la obtención de datos en el gráfico de consulta de actividades retrasadas por proyecto.
    /// </summary>
    public class cls_consActRetrasadas
    {

        #region Constructor

        /// <summary>
        /// Constructor de la clase cls_consActRetrasadas.
        /// </summary>
        public cls_consActRetrasadas()
        {
        }

        #endregion

        #region Propiedades

        public int pPK_proyecto
        {
            get { return PK_proyecto; }
            set { this.PK_proyecto = value; }
        }

        public int pPK_paquete
        {
            get { return PK_paquete; }
            set { this.PK_paquete = value; }
        }

        public string pPK_usuario
        {
            get { return PK_usuario; }
            set { this.PK_usuario = value; }
        }

        public string pNombreActividad
        {
            get { return nombreActividad; }
            set { this.nombreActividad = value; }
        }

        public decimal pDiasRetraso
        {
            get { return diasRetraso; }
            set { this.diasRetraso = value; }
        }

        public decimal pHorasRetraso
        {
            get { return horasRetraso; }
            set { this.horasRetraso = value; }
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Código del proyecto
        /// </summary>
        private int PK_proyecto;

        /// <summary>
        /// Código del paquete
        /// </summary>
        private int PK_paquete;

        /// <summary>
        /// Código del usuario
        /// </summary>
        private string PK_usuario;

        /// <summary>
        /// Nombre de la actividad para el desgloce
        /// </summary>
        private string nombreActividad;

        /// <summary>
        /// Dias de retraso de la actividad
        /// </summary>
        private decimal diasRetraso;

        /// <summary>
        /// Horas reales de retraso en la actividad
        /// </summary>
        private decimal horasRetraso;

        #endregion

    }

}

