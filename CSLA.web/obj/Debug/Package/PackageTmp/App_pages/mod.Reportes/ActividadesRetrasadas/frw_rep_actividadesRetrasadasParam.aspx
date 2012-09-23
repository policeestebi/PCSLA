<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_rep_actividadesRetrasadasParam.aspx.cs"
    Inherits="CSLA.web.App_pages.mod.Reportes.ActividadesRetrasadas.frw_rep_actividadesRetrasadasParam" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="tituloPagina" runat="server">
    Reporte de Actividades Retradas
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="centrado">
        <table id="tbl_mensajePaquetes" style="display: block;" class="advertencia">
            <br />
            <br />
            <tr>
                <td>
                    <asp:Label ID="lbl_PaqueteTitulo" CssClass="label" runat="server" Text="Reporte de Actividades Atrasadas"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_PaqueteMensaje" runat="server" Text="Este reporte imprime todas aquellas actividades de un proyecto específico, en donde última fecha de registro del usuario supere a la fecha de finalización estimada."></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    <table>
        <tr>
            <td colspan="3">
                <strong>Parámetros</strong>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto: "></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_Proyectos" runat="server" DataTextField="pNombre" DataValueField="pPK_proyecto"
                    Width="150px">
                </asp:DropDownList>
                <act:ListSearchExtender ID="lte_proyectos" runat="server" TargetControlID="ddl_Proyectos"
                    PromptText="Escriba para buscar" PromptCssClass="serchExtender" PromptPosition="Top" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr align="center">
            <td colspan="3">
                <asp:Button ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click"
                    OnClientClick="aspnetForm.target ='_blank';" />
            </td>
        </tr>
    </table>
</asp:Content>
