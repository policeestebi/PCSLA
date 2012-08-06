using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace CSLA.web.App_Constantes
{
    public class cls_constantes
    {
        //Parámetros operaciones
        public const String AGREGAR = "Agregar";
        public const String ELIMINAR = "Eliminar";
        public const String MODIFICAR = "Modificar";
        public const String EDITAR = "Editar";
        public const String VER = "Ver";
        public const String ACCESO = "Acceso";
        public const String TODOS_USUARIOS = "TodosUsuarios";
        public const String ASIGNACION_MASIVA = "AsignacionMasiva";
        public const String CAMBIAR = "Cambiar";
        public const String CREAR= "Crear";
        public const String ASIGNAR = "Asignar";
        public const String COPIAR = "Copiar";


        //Parámetros para las urls
        public const string MODO = "modo";
        public const string PERMISO = "permiso";
        public const string PERMISONOMBRE = "pN";

        //Constantes de la sesion
        public const string PAGINA = "vo_pagina";

        /// <summary>
        /// Constante que se utiliza para
        /// obtener el Key en el web.config
        /// con la dirección URL.
        /// </summary>
        public const String URLKEY =  "URL";

        /// <summary>
        /// Constante que se utiliza para
        /// obtener el Key en el web.config
        /// con la dirección URL de servidor de reportes.
        /// </summary>
        public const String URLREPORT = "ReportServerUrl";

        /// <summary>
        /// Script que se utiliza
        /// para salir al login cuando 
        /// se validan las páginas.
        /// </summary>
        public static String SCRIPTLOGOUT = String.Format("top.location = '{0}';",ConfigurationManager.AppSettings[URLKEY]);

        /// <summary>
        /// Constante que se utiliza para determinar el folder
        /// en donde se ubican todas las páginas del sistema.
        /// </summary>
        public const String FOLDER_PAGES = "App_pages";


        //Constantes de Reportes
        public const String REP_REG_TIEMPOS_USUARIO = "/CSLA.Reports/r_csla_cont_registroTiemposUsuario";
        public const String REP_REG_BITACORA = "/CSLA.Reports/r_csla_admi_bitacora";
        public const String REP_REG_ACTSUPESTI = "/CSLA.Reports/r_csla_cont_actividadSuperaEstimado";
        public const String REP_REG_ACTRET = "/CSLA.Reports/r_csla_cont_actividadRetrasada";
        public const String REP_REG_TIEMPOS_USUARIOS = "/CSLA.Reports/r_csla_cont_registroTiempos";

        /// <summary>
        /// Constante que se utiliza para guardar el código
        /// del proyecto del que se va a obtener información 
        /// para los gráficos.
        /// </summary>
        public const string CODIGOPROYECTO = "vs_codigoProyecto";

        /// <summary>
        /// Constante que se utiliza para guardar el código
        /// del paquete del que se va a obtener información 
        /// para los gráficos.
        /// </summary>
        public const string CODIGOPAQUETE = "vs_codigoPaquete";

        /// <summary>
        /// Constante que se utiliza para guardar la "fecha desde"
        /// del proyecto del que se va a obtener información 
        /// para los gráficos.
        /// </summary>
        public const string FECHADESDE = "vs_fechaDesde";

        /// <summary>
        /// Constante que se utiliza para guardar la "fecha hasta"
        /// del proyecto del que se va a obtener información 
        /// para los gráficos.
        /// </summary>
        public const string FECHAHASTA = "vs_fechaHasta";

        /// <summary>
        /// Constante que se utiliza para guardar el código
        /// del usuario del que se va a obtener información 
        /// para los gráficos.
        /// </summary>
        public const string USUARIOCONSULTA = "vs_usuarioConsulta";


    }

}
