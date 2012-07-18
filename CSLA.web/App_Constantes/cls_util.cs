using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSLA.web.App_Constantes
{
    public class cls_util
    {
        /// <summary>
        /// Obtiene la direccion correcta
        /// para la dirección de las páginas.
        /// </summary>
        /// <param name="ps_path"></param>
        /// <returns></returns>
        public static String ObtenerDireccion(String ps_path)
        {
            String vs_path = String.Empty;

            if (!String.IsNullOrEmpty(ps_path))
            {
                int vi_index = ps_path.IndexOf("App_pages");

                vs_path = ps_path.Substring(vi_index);
            }
            return vs_path;

        }
    }
}