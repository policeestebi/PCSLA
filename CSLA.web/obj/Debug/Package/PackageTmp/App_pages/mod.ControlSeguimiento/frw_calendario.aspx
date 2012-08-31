<%@ Page Title="" Language="C#" MasterPageFile="~/msp.EstiloBasico/mspContenido.Master"
    AutoEventWireup="true" CodeBehind="frw_calendario.aspx.cs" Inherits="CSLA.web.App_pages.mod.ControlSeguimiento.frm_calendario" %>

<%@ Register Assembly="COSEVI.web.controls" Namespace="COSEVI.web.controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tituloPagina" runat="server">
    Calendario de Actividades
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cuerpoPagina" runat="server">
    
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Load);

        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {//Condicion si queremos manejar que hubo un error y no ejecutar nada
                Load();
            }
        }

        $(document).ready(function () {
            Load();
        });

        function Load() {
            $(document).ready(function () {
                $(".calendarLink").fancybox({
                    'width': '100%',
                    'height': 500,
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'type': 'iframe',
                    'closeBtn': true,
                    onClosed: function () {
                        __doPostBack('<%= upd_Principal.ClientID  %>', '');

                    }
                });

                $(".imprevisto").fancybox({
                    'width': '100%',
                    'height': '100%',
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'type': 'iframe',
                    'closeBtn': true,
                    onClosed: function () {
                        __doPostBack('<%= upd_Principal.ClientID  %>', '');

                    }
                });


                //                $(".showMore").shorten({
                //                    "showChars": 30,
                //                    "moreText": "Más",
                //                    "lessText": "Menos"
                //                });


                var showChar = 35;
                var ellipsestext = "...";
                var moretext = "más";
                var lesstext = "menos";
                $('.showMore').each(function () {
                    var content = $(this).html();
                    if (content.length > showChar) {
                        var c = content.substr(0, showChar);
                        var h = content.substr(showChar, content.length - showChar);
                        var html = c + '<span class="moreellipses">' + ellipsestext + '&nbsp;</span><span class="morecontent"><span>' + h + '</span> <a href="javascript://nop/" class="morelink">' + moretext + '</a></span>';
                        $(this).html(html);
                        $(".morecontent span").hide();
                    }

                });

                $(".morelink").click(function () {
                    if ($(this).hasClass("less")) {
                        $(this).removeClass("less");
                        $(this).html(moretext);
                    } else {
                        $(this).addClass("less");
                        $(this).html(lesstext);
                    }
                    $(this).parent().prev().toggle();
                    $(this).prev().toggle();
                    return false;
                });



            });
        }

    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd_Principal" runat="server">
        <ContentTemplate>
            <div class="calendarContainer">
                <cc1:ucCalendar runat="server" ID="calendario" Css="calendar" CssHeader="calendarHeader"
                    CssDays="days" CssCalendarRow="calendarRow" CssLink="calendarLink" CssBotonImprevisto="imprevisto"
                    UrlLink="frw_registroTiempos.aspx" UrlImprevisto="frw_operaciones.aspx" UrlSiguiente="../../App_Themes/Basico/imagenes/iconos/img_siguiente.png"
                    UrlAnterior="../../App_Themes/Basico/imagenes/iconos/img_anterior.png" UrlBotonCalendario="../../App_Themes/Basico/botones/img_calendario.png"
                    OnvoCambioFecha="Unnamed1_voCambioFecha" DataTextFieldProyecto="nombre" DataValueFieldProyecto="PK_proyecto"
                    CodigoActividadField="PK_codigo" DescripcionField="descripcion" FechaField="fecha"
                    TipoField="tipo" RegistroField="PK_registro" HorasField="horas" CssShowMore="showMore" 
                    PaqueteField="PK_paquete" ComponenteField="PK_componente" EntregableField="PK_entregable" DescripcionPaquete="descripcion_paquete" UsuarioField="PK_usuario"
                    CssAjaxCalendar="tdCalendar"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
