<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeBehind="frw_asignacionActividad.aspx.cs" Inherits="CSLA.web.App_pages.mod.ControlSeguimiento.frw_asignacionActividad" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    &nbsp;Asignación de Actividades
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        var xPosAct, yPosAct, xPosAsg, yPosAsg, xPosUsu, yPosUsu;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=lbx_actividades.ClientID %>') != null) {
                xPosAct = $get('<%=lbx_actividades.ClientID %>').scrollLeft;
                yPosAct = $get('<%=lbx_actividades.ClientID %>').scrollTop;
            }
            if ($get('<%=lbx_usuariosAsociados.ClientID %>') != null) {
                xPosAsg = $get('<%=lbx_usuariosAsociados.ClientID %>').scrollLeft;
                yPosAsg = $get('<%=lbx_usuariosAsociados.ClientID %>').scrollTop;
            }
            if ($get('<%=lbx_usuarios.ClientID %>') != null) {
                xPosUsu = $get('<%=lbx_usuarios.ClientID %>').scrollLeft;
                yPosUsu = $get('<%=lbx_usuarios.ClientID %>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=lbx_actividades.ClientID %>') != null) {
                $get('<%=lbx_actividades.ClientID %>').scrollLeft = xPosAct;
                $get('<%=lbx_actividades.ClientID %>').scrollTop = yPosAct;
            }
            if ($get('<%=lbx_usuariosAsociados.ClientID %>') != null) {
                $get('<%=lbx_usuariosAsociados.ClientID %>').scrollLeft = xPosAsg;
                $get('<%=lbx_usuariosAsociados.ClientID %>').scrollTop = yPosAsg;
            }
            if ($get('<%=lbx_usuarios.ClientID %>') != null) {
                $get('<%=lbx_usuarios.ClientID %>').scrollLeft = xPosUsu;
                $get('<%=lbx_usuarios.ClientID %>').scrollTop = yPosUsu;
            }
        }

        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);        
    </script>  

    <asp:UpdatePanel ID="upd_Principal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <act:Accordion ID="ard_principal" runat="server" SelectedIndex="0" FadeTransitions="false"
                FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                SuppressHeaderPostbacks="true" HeaderCssClass="encabezadoAcordeon" ContentCssClass="contenidoAcordeon"
                HeaderSelectedCssClass="encabezadoSeleccionadoAcordeon">
                <Panes>
                    <act:AccordionPane ID="acp_edicionDatos" runat="server">
                        <Header>
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Asignaci&oacute;n de Actividades</a>
                        </Header>
                        <Content>
                            <table id="Table1">
                                <tr>
                                    <%-- Contenido de agregado Arriba--%>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%-- Espacio--%>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <%-- GridView o Mantenimiento--%>
                                        <table id="tbl_mantenimiento" >
                                            <tr>
                                                <td colspan="3">
                                                    <table>
                                                        <tr align="left">
                                                            <td>    
                                                                <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto:"></asp:Label>   
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_proyecto" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_paquete" runat="server" Text="Paquete:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_paquete" runat="server" OnSelectedIndexChanged="ddlPaquete_SelectedIndexChanged"
                                                                    AutoPostBack="true" OnDataBound="ddlPaquete_DataBound">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr align="left">
                                                            <td>
                                                                <asp:Label ID="lbl_descripcion" runat="server" Text="Descripcion: "></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_descripcion" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfv_descripcion" runat="server" ControlToValidate="txt_descripcion"
                                                                    ToolTip="Ingrese la descripcion de la actividad" ErrorMessage="Descripcion es requerida">
                                                                    <img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/>
                                                                </asp:RequiredFieldValidator>
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
                                                        <tr align="left">
                                                            <td>
                                                                <asp:Label ID="lbl_horasAsignadas" runat="server" Text="Horas Asignadas: "></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_horasAsignadas" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfv_horasAsignadas" runat="server" ControlToValidate="txt_horasAsignadas"
                                                                    ToolTip="Ingrese la cantidad de horas asignadas para el proyecto" ErrorMessage="Horas asignadas son requeridas"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="rfv_horasAsignadaslenght" runat="server" ErrorMessage="Número decimal fuera del rango establecido."
                                                                    ValidationExpression="^[0-9]{1,3}(\,[0-9]{0,2})?$" ControlToValidate="txt_horasAsignadas"
                                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>                                                        
                                                        <tr align="left">
                                                            <td>
                                                                <asp:Label ID="lbl_horasReales" runat="server" Text="Horas Reales: "></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_horasReales" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>                                                        
                                                        <tr align="left">
                                                            <td>
                                                                <asp:Label ID="lbl_estado" runat="server" Text="Estado: "></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_estado" runat="server" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td colspan="3">
                                                    <table id="tbl_asignacion">
                                                        <tr align="left">
                                                            <td>
                                                                <asp:Label ID="lbl_actividades" runat="server" Text="Actividades: "></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_asociados" runat="server" Text="Asociados: "></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_usuarios" runat="server" Text="Usuarios: "></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <act:ListSearchExtender ID="lse_actividad" runat="server" TargetControlID="lbx_actividades"
                                                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <act:ListSearchExtender ID="lse_usuarioAsignado" runat="server" TargetControlID="lbx_usuariosAsociados"
                                                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <act:ListSearchExtender ID="lse_usuarios" runat="server" TargetControlID="lbx_usuarios"
                                                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ListBox ID="lbx_actividades" runat="server" SelectionMode="Single" Width="200px"
                                                                    Height="150px" AutoPostBack="true" OnSelectedIndexChanged="lbx_actividades_SelectedIndexChanged">
                                                                </asp:ListBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="lbx_usuariosAsociados" runat="server" SelectionMode="Multiple" Width="200px"
                                                                    Height="150px"></asp:ListBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btn_asignarUsuario" runat="server" Text="&lt;" OnClick="btn_asignarUsuario_Click"
                                                                    Width="35px" colspan="2" CausesValidation="false" UseSubmitBehavior="False" />
                                                                <br />
                                                                <asp:Button ID="btn_removerUsuario" runat="server" Text="&gt;" OnClick="btn_removerUsuario_Click"
                                                                    Width="35px" colspan="2" CausesValidation="false" UseSubmitBehavior="False" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="lbx_usuarios" runat="server" SelectionMode="Multiple" Width="200px"
                                                                    Height="150px"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="center">
                                            <%--Contenido Agregado Abajo --%>
                                            <table id="Table2">
                                                <tr align="right">
                                                    <%--<td>
                                                        <asp:Button ID="btn_eliminar" runat="server" OnClick="btn_eliminar_Click" Text="Eliminar" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:Button ID="btn_regresar" CausesValidation="false" OnClick="btn_regresar_Click"
                                                            runat="server" Text="Regresar" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_guardar" runat="server" OnClick="btn_guardar_Click" Text="Guardar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </act:AccordionPane>
                </Panes>
            </act:Accordion>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
