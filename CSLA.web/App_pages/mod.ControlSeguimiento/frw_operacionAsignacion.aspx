<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_operacionAsignacion.aspx.cs" Inherits="CSLA.web.App_pages.mod.ControlSeguimiento.frw_operacionAsignacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content8" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="tituloPagina" runat="server">
    Asignación de operaciones e imprevistos
</asp:Content>
<asp:Content ID="Content10" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr align="left">
                    <td colspan="4">
                        <strong>
                        <asp:Label ID="lbl_codigo" runat="server" Text="Operación:"></asp:Label>
                        </strong>
                    </td>
                    <td>
                        <asp:Label ID="lbl_codigoValor" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr align="left">
                    <td colspan="4">
                         <strong>
                         <asp:Label ID="lbl_descripcion" runat="server" Text="Descripción:"></asp:Label>
                         </strong>
                    </td>
                    <td>
                        <asp:Label ID="lbl_descripcionValor" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
             
                <tr>
                    <td>
                        <strong>Usuarios Asignados</strong>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <strong>Usuarios Sin Asignar</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox Height="200" Width="200" ID="ltb_usuarioAsignados" runat="server" 
                            DataTextField="pNombreCompleto"  DataValueField="pPK_usuario" 
                            SelectionMode="Multiple" ></asp:ListBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btn_agregar" Width="35px" runat="server" Text="<" 
                            onclick="btn_agregar_Click1" />
                        <br />
                        <asp:Button ID="btn_eliminar" Width="35px" runat="server" Text=">" 
                            onclick="btn_eliminar_Click" />
                    </td>
                    <td>
                    &nbsp;
                    </td>
                    
                    <td>
                        <asp:ListBox Height="200" Width="200" ID="ltb_usuarioNoAsignados" 
                            runat="server" DataTextField="pNombreCompleto"  DataValueField="pPK_usuario" 
                            SelectionMode="Multiple" >
                        </asp:ListBox>
                         <act:ListSearchExtender ID="lte_usuarios" runat="server" TargetControlID="ltb_usuarioNoAsignados"
                                    PromptText="Escriba para buscar" PromptCssClass="serchExtender" PromptPosition="Top" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Button ID="btn_guardar" runat="server" Text="Guardar" 
                            onclick="btn_guardar_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
