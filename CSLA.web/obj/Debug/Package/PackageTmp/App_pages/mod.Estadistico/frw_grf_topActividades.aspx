<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_grf_topActividades.aspx.cs" Inherits="CSLA.web.App_pages.mod.Estadistico.frw_grf_topActividades" %>

<%@ Register Assembly="System.Web.DataVisualization" Namespace="System.Web.UI.DataVisualization.Charting"
    TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    &nbsp;Actividades Top por Proyecto
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
                    <asp:Label ID="lbl_tituloMensaje" CssClass="label" runat="server" Text="Gráfico de Top de Actividades"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_mensaje" runat="server" Text="Gráfico que imprime el top de actividades en las cuales un usuario ha invertido más tiempo en un proyecto específico."></asp:Label>
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
                <table id="Table3">
                    <tr align="left">
                        <td>
                            <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto: "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_proyecto" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="lbl_fechaInicio" runat="server" Text="Fecha Inicio: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_fechaInicio" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="img_cldFechaInicio" runat="server" ImageUrl="../../App_Themes/Basico/botones/img_calendario.png"
                                CausesValidation="false" />
                            <act:CalendarExtender ID="dt_fechaInicio" runat="server" TargetControlID="txt_fechaInicio"
                                PopupButtonID="img_cldFechaInicio" Format="dd/MM/yyyy" />
                            <act:MaskedEditExtender runat="server" ID="msk_fechaInicio" TargetControlID="txt_fechaInicio"
                                Mask="99/99/9999" CultureName="es-ES" MessageValidatorTip="true" MaskType="Date"
                                UserDateFormat="DayMonthYear">
                            </act:MaskedEditExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfv_fechaInicio" runat="server" ControlToValidate="txt_fechaInicio"
                                ToolTip="Ingrese la fecha inicio de la actividad" ErrorMessage="Fecha inicio es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="lbl_fechaFin" runat="server" Text="Fecha Fin: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_fechaFin" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="img_cldFechaFinal" runat="server" ImageUrl="../../App_Themes/Basico/botones/img_calendario.png"
                                CausesValidation="false" />
                            <act:CalendarExtender ID="dt_fechaFin" runat="server" TargetControlID="txt_fechaFin"
                                PopupButtonID="img_cldFechaFinal" Format="dd/MM/yyyy" />
                            <act:MaskedEditExtender runat="server" ID="msk_fechaFinal" TargetControlID="txt_fechaFin"
                                Mask="99/99/9999" CultureName="es-ES" MessageValidatorTip="true" MaskType="Date"
                                UserDateFormat="DayMonthYear">
                            </act:MaskedEditExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfv_fechaFin" runat="server" ControlToValidate="txt_fechaFin"
                                ToolTip="Ingrese la fecha fin de la actividad" ErrorMessage="Fecha fin es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table id="Table7">
                    <tr align="left">
                        <td>
                        </td>
                        <td>
                            <act:ListSearchExtender ID="lse_usuarios" runat="server" TargetControlID="lbx_usuarios"
                                PromptText="Digite para buscar..." PromptPosition="Top" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:Label ID="lbl_usuario" runat="server" Text="Usuario: "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:ListBox ID="lbx_usuarios" runat="server" SelectionMode="Single" Width="200px"
                                Height="198px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:Button ID="btn_generar" CausesValidation="false" OnClick="Generar_Click" runat="server"
                    Text="Generar" />
            </td>
        </tr>
    </table>
    <asp:Chart ID="Grafico" runat="server" Height="408px" Width="752px" OnClick="Grafico_Click"
        OnInit="Grafico_Init">
        <Titles>
            <asp:Title ShadowOffset="3" Name="Title1" />
        </Titles>
        <Legends>
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Leyendas"
                LegendStyle="Row" />
        </Legends>
        <Series>
            <asp:Series Name="Leyendas" />
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="AreaGrafico" BorderWidth="0" />
        </ChartAreas>
    </asp:Chart>
</asp:Content>
