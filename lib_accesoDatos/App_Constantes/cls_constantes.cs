using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSEVI.CSLA.lib.accesoDatos.App_Constantes
{
    public class cls_constantes
    {

        //Parámetros transacciones
        public const String INSERTAR = "Insertar";
        public const String MODIFICAR = "Modificar";
        public const String ELIMINAR = "Eliminar";
        public const String LOGIN = "Login";
        public const String LOGOFF = "Logoff";

        //Nombre Tablas
        public const String PERMISO = "t_admi_permiso";
        public const String ROL = "t_admi_rol";
        public const String MENU = "t_admi_menu";
        public const String COMPONENTE = "t_cont_componente";
        public const String ENTREGABLE = "t_cont_entregable";
        public const String ESTADO = "t_cont_estado";
        public const String PAQUETE = "t_cont_paquete";
        public const String CONSECUTIVO = "t_admi_consecutivo";
        public const String DEPARTAMENTO = "t_admi_departamento";
        public const String PAGINA = "t_admi_pagina";
        public const String PAGINA_PERMISO = "t_admi_pagina_permiso";
        public const String ROL_PAGINA_PERMISO = "t_admi_rol_pagina_permiso";
        public const String USUARIO = "t_admi_usuario";
        public const String ACTIVIDAD = "t_cont_actividad";
        public const String BITACORA = "t_admi_bitacora";
        public const String ENTREGABLE_COMPONENTE = "t_cont_entregable_componente";
        public const String PROYECTO_ENTREGABLE = "t_cont_proyecto_entregable";
        public const String COMPONENTE_PAQUETE = "t_cont_componente_paquete";
        public const String PROYECTO = "t_cont_proyecto";
        public const String DEPARTAMENTO_PROYECTO = "t_cont_departamento_proyecto";
        public const String OPERACION = "t_cont_operacion";
        public const String ACTIVIDAD_ASIGNACION = "t_cont_asignacion_actividad";
        public const String OPERACION_ASIGNACION = "t_cont_asignacion_operacion";
        public const String OPERACION_REGISTRO = "t_cont_registro_operacion";
        public const String ACTIVIDAD_REGISTRO = "t_cont_registro_actividad";

        public const String PROYECTO_COPIA = "proyecto_copia";
        public const String NOMBRE_IMPREVISTO = "Imprevisto";
        public const String NOMBRE_OPERACION = "Operación";
        public const int CODIGO_IMPREVISTO = 0;
        public const int CODIGO_OPERACION = -1;
        public const int CODIGO_INVALIDO = -1;

    }

    public enum Accion
    {
        Insertar,
        Modificar,
        Eliminar,
        Login,
        Logoff
    }

    public enum Tablas
    {
        t_admi_permiso,
        t_admi_rol,
        t_admi_menu,
        t_cont_componente,
        t_cont_entregable,
        t_cont_estado,
        t_cont_paquete,
        t_admi_consecutivo,
        t_admi_departamento,
        t_admi_pagina,
        t_admi_pagina_permiso,
        t_admi_rol_pagina_permiso,
        t_admi_usuario,
        t_cont_actividad,
        t_admi_bitacora,
        t_cont_entregable_componente,
        t_cont_proyecto_entregable,
        t_cont_componente_paquete,
        t_cont_departamento_proyecto,
        t_cont_operacion,
        t_cont_asignacion_actividad,
        t_cont_asignacion_operacion,
        t_cont_registro_operacion,
        t_cont_registro_actividad
    }
}
