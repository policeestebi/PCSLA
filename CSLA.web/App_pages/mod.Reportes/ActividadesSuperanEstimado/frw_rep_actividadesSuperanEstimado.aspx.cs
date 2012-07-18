using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using COSEVI.CSLA.lib.entidades.mod.Administracion;
using CSLA.web.App_Constantes;

namespace CSLA.web.App_pages.mod.Reportes.ActividadesSuperanEstimado
{
    public partial class frw_rep_actividadesSuperanEstimado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.configurarReporte();
        }

        private void configurarReporte()
        {
            if (!Page.IsPostBack)
            {
                this.rpv_actividadesSuperanEstimado.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings[cls_constantes.URLREPORT]);
                this.rpv_actividadesSuperanEstimado.ServerReport.ReportPath = cls_constantes.REP_REG_ACTSUPESTI;
                this.rpv_actividadesSuperanEstimado.ServerReport.SetParameters(this.obtenerParametros());
            }
        }

        private IEnumerable<ReportParameter> obtenerParametros()
        {

            List<ReportParameter> vo_parametros = new List<ReportParameter>();
            String vs_proyecto;
            try
            {

                vs_proyecto = Request.QueryString["proyecto"].ToString();
                vo_parametros.Add(new ReportParameter("proyecto", vs_proyecto));
            }
            catch (Exception po_exception)
            {
                vo_parametros = null;
            }

            return vo_parametros;
        }
    }
}