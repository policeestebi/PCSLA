using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSLA.web.App_Constantes;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using COSEVI.CSLA.lib.entidades.mod.Administracion;

namespace CSLA.web.App_pages.mod.Reportes.RegistroTiemposUsuarios
{
    public partial class frw_rep_registroTiemposUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.configurarReporte();
        }

        private void configurarReporte()
        {
            if (!Page.IsPostBack)
            {
                this.rpv_registro.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings[cls_constantes.URLREPORT]);
                this.rpv_registro.ServerReport.ReportPath = cls_constantes.REP_REG_TIEMPOS_USUARIOS;
                this.rpv_registro.ServerReport.SetParameters(this.obtenerParametros());
            }
        }

        private IEnumerable<ReportParameter> obtenerParametros()
        {

            List<ReportParameter> vo_parametros = new List<ReportParameter>();
            String vs_fechaInicio;
            String vs_fechaFinal;
            string vs_usuario = String.Empty;
            try
            {

                vs_fechaInicio = Request.QueryString["fechaInicio"].ToString();
                vo_parametros.Add(new ReportParameter("fechaInicio", vs_fechaInicio));

                vs_fechaFinal = Request.QueryString["fechaFinal"].ToString();
                vo_parametros.Add(new ReportParameter("fechaFinal", vs_fechaFinal));

                vs_usuario = Request.QueryString["usr"].ToString();
                vo_parametros.Add(new ReportParameter("usuario", vs_usuario));

            }
            catch (Exception po_exception)
            {
                vo_parametros = null;
            }

            return vo_parametros;
        }
    }
}