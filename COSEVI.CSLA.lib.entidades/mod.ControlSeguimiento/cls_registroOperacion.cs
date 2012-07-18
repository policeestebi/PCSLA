using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//======================================================================
// Consejo de Seguridad Vial (COSEVI). - 2011
// Sistema CSLA
//
// cls_actividad.cs
//
// Clase que asocia la información del registro de tiempos de los
// funcionarios en las operaciones.
// =====================================================================
// Historial
// PERSONA 			        MES - DIA - AÑO		DESCRIPCION
// Esteban Ramírez          04  - 08    2012    Se crea la clase.
//							
// 
//								
//								
//
//======================================================================
namespace COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento
{
    public class cls_registroOperacion
    {
        #region Contructor

        #endregion

        #region Propiedades

        public string pComentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public cls_asignacionOperacion pFK_Asignacion
        {
            get { return FK_asignacion; }
            set { FK_asignacion = value; }
        }

        public DateTime pFecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public decimal pHoras
        {
            get { return horas; }
            set { horas = value; }
        }

        public decimal pPK_registro
        {
            get { return PK_registro; }
            set { PK_registro = value; }
        }

        #endregion

        #region Atributos

        private decimal PK_registro;

        
        private cls_asignacionOperacion FK_asignacion;

        private string comentario;

        private DateTime fecha;

        private decimal horas;

        #endregion
    }
}
