using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSLA.web.msp.EstiloBasico
{
    public partial class mspEstiloGeneral : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ColocarFecha();
        }

        private void ColocarFecha()
        {
            this.lbl_fecha.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }
    }
}
