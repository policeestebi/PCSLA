<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master" AutoEventWireup="true" CodeBehind="frw_rep_registroTiemposUsuario.aspx.cs" Inherits="CSLA.web.App_pages.mod.Reportes.RegistroTiemposUsuario.frw_rep_registroTiemposUsuario" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="tituloPagina" runat="server">
Reporte de Registro de Tiempos Usuario
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="srm_principal" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="rpv_registroTiemposUsuario" Width="100%" Height="800px" runat="server"
    ProcessingMode="Remote" ShowParameterPrompts="False" >
    </rsweb:ReportViewer>
</asp:Content>
