<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_grf_compHorasActividades.aspx.cs" Inherits="CSLA.web.App_pages.mod.Estadistico.frw_grf_compHorasActividades" %>

<%@ Register Assembly="System.Web.DataVisualization" Namespace="System.Web.UI.DataVisualization.Charting"
    TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    &nbsp;Comparación de horas en Actividades por Proyecto
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
                    <asp:Label ID="lbl_tituloMensaje" CssClass="label" runat="server" Text="Gráfico de Comparación de horas en Actividades por Proyecto"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_mensaje" runat="server" Text="Gráfico que imprime una comparación de los tiempos reales contra los tiempos estimados, según un proyecto y usuario específico."></asp:Label>
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
                    <tr align="right">
                        <td>
                            <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_proyecto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged"
                                Width="175px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr align="right">
                        <td>
                            <asp:Label ID="lbl_paquete" runat="server" Text="Paquete:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_paquete" runat="server" Enabled="false" Width="175px">
                            </asp:DropDownList>
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
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btn_generar" CausesValidation="false" OnClick="Generar_Click" runat="server"
                    Text="Generar" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Chart ID="Grafico" runat="server" Height="393px" Width="540px" OnInit="Grafico_Init">
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
            </td>
        </tr>
    </table>
</asp:Content>
