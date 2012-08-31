using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace CSLA.web.App_pages
{
    public partial class frw_error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String vs_error_usuario = Request.QueryString["vs_error_usuario"];
            vs_error_usuario = vs_error_usuario.Replace("_", " ");
            vs_error_usuario = vs_error_usuario.Replace("|", "'");
            this.txt_error_usuario.Text = vs_error_usuario;

            String vs_error_tecnico = Request.QueryString["vs_error_tecnico"];
            vs_error_tecnico = vs_error_tecnico.Replace("_", " ");
            vs_error_tecnico = vs_error_tecnico.Replace("|", "'");
            this.txt_error_tecnico.Text = vs_error_tecnico;
        }
    }
}
