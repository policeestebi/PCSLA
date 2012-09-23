<%@ Page Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master" AutoEventWireup="true"
    CodeBehind="frw_error.aspx.cs" Inherits="CSLA.web.App_pages.frw_error" Title="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Control y Seguimiento de Labores
    <br />
    Se ha generado un error
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <div class="centrado" style="width:300px">
        <table class="advertencia" style="display:block;">
            <tbody >
                <tr>
                    <td>
                        &nbsp;Error usuario
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txt_error_usuario" runat="server" TextMode="MultiLine"
                            Columns="70" Rows="5" ReadOnly="true" Height="92px" Width="286px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;Error t&eacute;cnico
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox  ID="txt_error_tecnico" runat="server" TextMode="MultiLine"
                            Columns="70" Rows="5" ReadOnly="true" Height="78px" Width="286px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CausesValidation="false"
                            OnClientClick="window.close();" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
