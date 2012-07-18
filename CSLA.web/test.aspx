<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CSLA.web.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="App_Themes/Basico/css/pages_style.css"
</head> 
<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
    <div>
    <cc1:ucCalendar runat="server" UrlAnterior="/App_Themes/Basico/imagenes/iconos/img_previous.png" Css="calendar" UrlSiguiente="/App_Themes/Basico/imagenes/iconos/img_next.png"  />
    <asp:HyperLink runat="server" Text="Prueba">
    </asp:HyperLink>
    <asp:TextBox  runat="server" Text="prueba" id="btnPrueba"/>


    <act:CalendarExtender runat="server" TargetControlID="btnPrueba"></act:CalendarExtender>
    </div>
    </form>
</body>
</html>
