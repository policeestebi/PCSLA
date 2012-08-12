<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_grf_consActRetrasadas.aspx.cs" Inherits="CSLA.web.App_pages.mod.Estadistico.frw_grf_consActRetrasadas" %>

<%@ Register Assembly="System.Web.DataVisualization" Namespace="System.Web.UI.DataVisualization.Charting"
    TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    &nbsp;Consulta de Actividades Retrasadas por Proyecto
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    <%-- GridView o Mantenimiento--%>
        <div class="centrado">
        <table id="tbl_mensaje" style="display: block;" class="advertencia">
            <br />
            <br />
            <tr>
                <td>
                    <asp:Label ID="lbl_tituloMensaje" CssClass="label" runat="server" Text="Gráfico de Consulta de Actividades Retrasadas por Proyecto"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_mensaje" runat="server" Text="Gráfico que imprime una consulta sobre las actividades que se encuentran retradas en un proyecto, según un proyecto y usuario específico."></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    <table id="Table5">
        <tr align="center">
            <td colspan="2">
                <strong>Parámetros</strong>
            </td>
        </tr>
        <tr align="center">
            <td>
                <table id="Table2">
                    
                </table>
            </td>
            <td>
                <table id="Table6">
                    <tr align="left">
                        <td>
                            <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_proyecto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="lbl_paquete" runat="server" Text="Paquete:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_paquete" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="right">
                        <td>
                            <asp:Label ID="lbl_usuario" runat="server" Text="Usuario: "></asp:Label>
                        </td>
                        <td>
                            <act:ListSearchExtender ID="lse_usuarios" runat="server" TargetControlID="lbx_usuarios"
                                PromptText="Digite para buscar..." PromptPosition="Top" />
                            <asp:ListBox ID="lbx_usuarios" runat="server" SelectionMode="Single" Width="200px"
                                Height="198px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btn_generar" CausesValidation="false" OnClick="Generar_Click" runat="server"
                    Text="Generar" />
            </td>
        </tr>
    </table>
    <asp:Chart ID="Grafico" runat="server" Height="408px" Width="752px" BorderlineDashStyle="Solid"
        Palette="BrightPastel" BackSecondaryColor="White" BackGradientStyle="TopBottom"
        BorderWidth="2" BackColor="WhiteSmoke" BorderColor="26, 59, 105" OnInit="Grafico_Init">
        <Titles>
            <asp:Title ShadowOffset="3" Name="Title1" />
        </Titles>
        <Legends>
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Leyendas"
                BackColor="Transparent" LegendStyle="Row" Font="Trebuchet MS, 8.25pt, style=Bold" />
        </Legends>
        <Series>
            <asp:Series Name="DiasRetraso" />
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="AreaGrafico" BorderWidth="0" BorderColor="64, 64, 64, 64" BackSecondaryColor="White"
                BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom" />
        </ChartAreas>
    </asp:Chart>
</asp:Content>
