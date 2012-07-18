<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master" AutoEventWireup="true" CodeBehind="frw_rep_registroTiempos.aspx.cs" Inherits="CSLA.web.App_pages.mod.Reportes.frw_rep_registroTiempos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="tituloPagina" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cuerpoPagina" runat="server">

<asp:ScriptManager ID="srm_principal" runat="server"></asp:ScriptManager>

    <rsweb:ReportViewer ID="rpt_registro" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <ServerReport ReportPath="/CSLA.Reports/r_csla_cont_registroTiempos" />
    </rsweb:ReportViewer>

</asp:Content>
