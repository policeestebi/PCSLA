<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_copiarProyecto.aspx.cs" Inherits="CSLA.web.App_pages.mod.ControlSeguimiento.frw_copiarProyecto" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Copia de Proyectos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    <asp:ScriptManager ID="scr_Man" runat="server">
    </asp:ScriptManager>
    
    <script type="text/javascript">

        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=lbx_depasociados.ClientID %>') != null) {
                xPos = $get('<%=lbx_depasociados.ClientID %>').scrollLeft;
                yPos = $get('<%=lbx_depasociados.ClientID %>').scrollTop;
            }
            if ($get('<%=lbx_departamentos.ClientID %>') != null) {
                xPos = $get('<%=lbx_departamentos.ClientID %>').scrollLeft;
                yPos = $get('<%=lbx_departamentos.ClientID %>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=lbx_depasociados.ClientID %>') != null) {
                $get('<%=lbx_depasociados.ClientID %>').scrollLeft = xPos;
                $get('<%=lbx_depasociados.ClientID %>').scrollTop = yPos;
            }
            if ($get('<%=lbx_departamentos.ClientID %>') != null) {
                $get('<%=lbx_departamentos.ClientID %>').scrollLeft = xPos;
                $get('<%=lbx_departamentos.ClientID %>').scrollTop = yPos;
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
                            <a href="" style="color: #FFFFFF; font-size: 12px;">Edici&oacute;n Copia de Proyecto</a>
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
                                    <td>
                                        <%-- GridView o Mantenimiento--%>
                                        <table id="tbl_mantenimiento">
                                        <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_proyecto" runat="server" Text="Proyecto: " Width="90px" ></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_proyecto" runat="server" Width="200px" TextMode="SingleLine" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            <tr align="left">
                                                
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
                                                    <td>
                                                        <asp:Label ID="lbl_fechaInicio" runat="server" Text="Fecha Inicio: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_fechaInicio" runat="server"></asp:TextBox>
                                                        <act:CalendarExtender ID="dt_fechaInicio" runat="server" TargetControlID="txt_fechaInicio"
                                                            Format="MMMM d, yyyy" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_fechaInicio" runat="server" ControlToValidate="txt_fechaInicio"
                                                            ToolTip="Ingrese la fecha inicio de la actividad" ErrorMessage="Fecha inicio es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_nombre" runat="server" Text="Nombre: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_nombre" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_nombre"
                                                            ToolTip="Ingrese el nombre del proyecto" ErrorMessage="Nombre es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rfv_nombrelenght" runat="server" ErrorMessage="La longitud máxima son 100 caracteres."
                                                            ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txt_nombre" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_fechaFin" runat="server" Text="Fecha Fin: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_fechaFin" runat="server"></asp:TextBox>
                                                        <act:CalendarExtender ID="dt_fechaFin" runat="server" TargetControlID="txt_fechaFin"
                                                            Format="MMMM d, yyyy" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_fechaFin" runat="server" ControlToValidate="txt_fechaFin"
                                                            ToolTip="Ingrese la fecha fin de la actividad" ErrorMessage="Fecha fin es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_descripcion" runat="server" Text="Descripcion: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_descripcion" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_descripcion" runat="server" ControlToValidate="txt_descripcion"
                                                            ToolTip="Ingrese la descripcion del proyecto" ErrorMessage="Descripcion es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rfv_descripcionlenght" runat="server" ErrorMessage="La longitud máxima son 100 caracteres."
                                                            ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txt_descripcion"
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>
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
                                                            ValidationExpression="^[0-9]{1,3}(\.[0-9]{0,2})?$" ControlToValidate="txt_horasAsignadas"
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_objetivo" runat="server" Text="Objetivo: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_objetivo" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_objetivo" runat="server" ControlToValidate="txt_objetivo"
                                                            ToolTip="Ingrese el objetivo del proyecto" ErrorMessage="Objetivo es requerido"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rfv_objetivolenght" runat="server" ErrorMessage="La longitud máxima son 500 caracteres."
                                                            ValidationExpression="^([\S\s]{0,500})$" ControlToValidate="txt_objetivo" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                                   
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lbl_meta" runat="server" Text="Meta: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_meta" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfv_meta" runat="server" ControlToValidate="txt_meta"
                                                            ToolTip="Ingrese la meta del proyecto" ErrorMessage="Meta es requerida"><img alt="imagen" width="25px" height="20px" src="../../App_Themes/Basico/botones/img_warning.gif" border="none"/></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="rfv_metalenght" runat="server" ErrorMessage="La longitud máxima son 500 caracteres."
                                                            ValidationExpression="^([\S\s]{0,500})$" ControlToValidate="txt_objetivo" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        <td>
                                                            <asp:Label ID="lbl_horasReales" runat="server" Text="Horas Reales: "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_horasReales" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </tr>
                                                <tr align="left" style="text-align:center;">
                                                    <td colspan="6">
                                                        <table id="Table3">
                                                            <tr>
                                                                <td>
                                                                &nbsp;
                                                                </td>
                                                                <td>
                                                                    <act:ListSearchExtender ID="lse_depAsociados" runat="server" TargetControlID="lbx_depasociados"
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
                                                                <td align="left">
                                                                    <act:ListSearchExtender ID="lse_departamentos" runat="server" TargetControlID="lbx_departamentos"
                                                                    PromptText="Digite para buscar..." PromptPosition="Top" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="left" style="text-align:center;">
                                                    <td colspan="6">
                                                        <table id="tbl_departamentos">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text="Departamentos: "></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:ListBox ID="lbx_depasociados" runat="server" SelectionMode="Multiple" Width="300px"
                                                                        Height="100px"></asp:ListBox>
                                                                </td>
                                                                <td>
                                                                &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btn_asignarDepto" runat="server" Text="&lt;" OnClick="btn_asignarDepto_Click"
                                                                        Width="35px" colspan="2" />
                                                                    <br />
                                                                    <asp:Button ID="btn_removerDepto" runat="server" Text="&gt;" OnClick="btn_removerDepto_Click"
                                                                        Width="35px" colspan="2" />
                                                                </td>
                                                                <td>
                                                                &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:ListBox ID="lbx_departamentos" runat="server" SelectionMode="Multiple" Width="300px"
                                                                        Height="100px"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="center">
                                            <%--Contenido Agregado Abajo --%>
                                            <table id="Table2">
                                                <tr align="right">
                                                    <td>
                                                        <asp:Button ID="btn_cancelar" CausesValidation="false" OnClick="btn_cancelar_Click"
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
                                </td> </tr>
                            </table>
                        </Content>
                    </act:AccordionPane>
                </Panes>
            </act:Accordion>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
