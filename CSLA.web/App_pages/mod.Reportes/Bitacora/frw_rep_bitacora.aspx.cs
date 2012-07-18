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

namespace CSLA.web.App_pages.mod.Reportes.Bitacora
{
    public partial class frw_rep_bitacora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.configurarReporte();
        }

        private void configurarReporte()
        {
            if (!Page.IsPostBack)
            {
                this.rpv_bitacora.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings[cls_constantes.URLREPORT]);
                this.rpv_bitacora.ServerReport.ReportPath = cls_constantes.REP_REG_BITACORA;
                this.rpv_bitacora.ServerReport.SetParameters(this.obtenerParametros());
            }
        }

        private IEnumerable<ReportParameter> obtenerParametros()
        {

            List<ReportParameter> vo_parametros = new List<ReportParameter>();
            String vs_fechaInicio;
            String vs_fechaFinal;
            string vs_usuario = String.Empty;
            string vs_usuarioDesde = String.Empty;
            string vs_usuarioHasta = String.Empty;
            string vs_tabla = String.Empty;
            string vs_accion = String.Empty;
            string vs_registro = String.Empty;
            try
            {

                vs_fechaInicio = Request.QueryString["fechaInicio"].ToString();
                vo_parametros.Add(new ReportParameter("fechaInicio", vs_fechaInicio));

                vs_fechaFinal = Request.QueryString["fechaFinal"].ToString();
                vo_parametros.Add(new ReportParameter("fechaFinal", vs_fechaFinal));

                vs_usuarioDesde = Request.QueryString["usD"].ToString();
                vo_parametros.Add(new ReportParameter("usuarioDesde", vs_usuarioDesde));

                vs_usuarioHasta = Request.QueryString["usH"].ToString();
                vo_parametros.Add(new ReportParameter("usuarioHasta", vs_usuarioHasta));

                vs_tabla = Request.QueryString["tab"].ToString();
                vo_parametros.Add(new ReportParameter("tabla", vs_tabla));

                vs_accion = Request.QueryString["acc"].ToString();
                vo_parametros.Add(new ReportParameter("accion", vs_accion));

                vs_registro = Request.QueryString["preg"].ToString();
                vo_parametros.Add(new ReportParameter("registro", vs_registro));

       
            }
            catch (Exception po_exception)
            {
                vo_parametros = null;
            }

            return vo_parametros;
        }
    }
}