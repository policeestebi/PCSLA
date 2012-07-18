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

namespace CSLA.web.App_pages.mod.Reportes.RegistroTiemposUsuario
{
    public partial class frw_rep_registroTiemposUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.configurarReporte();
        }

        private void configurarReporte()
        {
            if (!Page.IsPostBack)
            {
                this.rpv_registroTiemposUsuario.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings[cls_constantes.URLREPORT]);
                this.rpv_registroTiemposUsuario.ServerReport.ReportPath = cls_constantes.REP_REG_TIEMPOS_USUARIO;
                this.rpv_registroTiemposUsuario.ServerReport.SetParameters(this.obtenerParametros());
            }
        }

        private IEnumerable<ReportParameter> obtenerParametros()
        {

            List<ReportParameter> vo_parametros = new List<ReportParameter>();
            String vs_fechaInicio;
            String vs_fechaFinal;
            string vs_usuario = String.Empty;
            string vs_oficio = String.Empty;
            try
            {

                vs_fechaInicio = Request.QueryString["fechaInicio"].ToString() ;
                vo_parametros.Add(new ReportParameter("fechaInicio",vs_fechaInicio));

                vs_fechaFinal = Request.QueryString["fechaFinal"].ToString();
                vo_parametros.Add(new ReportParameter("fechaFinal", vs_fechaFinal));

                vs_oficio = Request.QueryString["oficio"].ToString();
                vo_parametros.Add(new ReportParameter("oficio", vs_oficio));

                vs_usuario = ((cls_usuario)this.Session["cls_usuario"]).pPK_usuario;
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