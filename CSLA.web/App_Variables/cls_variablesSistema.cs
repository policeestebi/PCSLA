using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using COSEVI.CSLA.lib.entidades.mod.ControlSeguimiento;
using System.Web.UI.DataVisualization.Charting;

namespace CSLA.web.App_Variables
{
    /// <summary>
    /// Clase que contiene 
    /// variables utilizadas 
    /// para los diferentes
    /// mantenimientos del sistema
    /// </summary>
    public class cls_variablesSistema
    {
        //Añadir el usuario
        public static String tipoEstado;            //Agregar o Modificar
        public static Object obj = new Object();    //Objeto que trae los datos

        //Variable Estática para llevar la creación de un proyecto!!!
        public static cls_proyecto vs_proyecto = new cls_proyecto();

    }
}
