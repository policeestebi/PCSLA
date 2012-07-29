using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

[WebService(Namespace = "http://tempuri.org/")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

[System.Web.Script.Services.ScriptService]

public class PageLoader : WebService
{

    [WebMethod]

    public string LoadPage(string pageName, string size)
    {
        return @"<iframe frameborder='0'
                         scrolling='no'
                         marginheight='0'
                         marginwidth='0'
                         width='1020px'
                         visible='false' 
                         id='frame' src='" + pageName + "' height='" + size + "' runat='server'></iframe>";

    }

}

