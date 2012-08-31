
/*
Consejo de Seguridad Vial (CONSEVI),
Sistema de Control y Seguimiento de Labores (CSLA)
Fecha de Creación: 04/06/2011
Creado por: Esteban Ramírez González.

Modificaciones:
*/

//Función utilizada para eliminar registros de un 
//gridview pregunta si se desea eliminar el registro.
function deleteRow(url, params) {

    if (confirm('¿Está seguro que desea eliminar este registro?')) {

        return loadPage(url, params);
    }
    else {

        return false;
    }
}

//Función que carga una página.
//function loadPage(url, params) {

//    var href = url + '?' + params;
//    $('#main').load(href);

//    return false;
//}

//Nuevas funciones para cargar las página dentro de un div.
function loadPage(url, params, height) {
    var href = url + '?' + params;
    var href = href.replace('#', '');
    document.getElementById('Loading').style.display = 'block';
    ret = PageLoader.LoadPage(href, height, OnComplete, OnTimeOut, OnError);
    return (true);
}

function OnComplete(args) {
    document.getElementById('main').innerHTML = args;
    document.getElementById('Loading').style.display = 'none';
}

function OnTimeOut(args) {
    alert("Service call timed out.");
}

function OnError(args) {
    alert("Error calling service method.");
}

//Nuevas funciones para cargar las página dentro de un div.

    
//Función que se encarga de cargar la primera 
//página del sistema.
$(document).ready(function () {

    //var href = '/App_pages/mod.Administracion/frw_permisos.aspx'
    var href = 'App_pages/mod.ControlSeguimiento/frw_calendario.aspx'

    loadPage(href, '', 750);

    $("li a").click(function (){
       
        var toLoad = $(this).attr('href');
        var height = $(this).attr("name");
        if (toLoad != "#") {  

            var resultado;

            //$('#main').hide();

            // document.getElementById('main').style.display = 'none';

            //alert("El tamaño es de " + height);

            resultado = loadPage(toLoad, '', height);

            // document.getElementById('main').style.display = 'block';

            //$('#main').show();

            return resultado;
        }

    });

});


function mostrarMensaje() {
    alert('Se ha cargado la pagina exitosamente');
}


//Función para el manejo de 
//del acceso de las opciones de menú.
//$("p").click(function () {

//    alert("hola");
//    var toLoad = $(this).attr('href'); //+ ' #main';

//    loadPage(toLoad, '', 600);

//    //    $('#main').hide('fast', loadContent);
//    //    //        $('#load').remove();
//    //    //        $('#container_layout').append('<span id="load">LOADING...</span>');
//    //    //        $('#load').fadeIn('normal');
//    //    window.location.hash = $(this).attr('href').substr(0, $(this).attr('href').length - 5);
//    //    // alert(loadContent);
//    //    function loadContent() {
//    //        $('#main').load(toLoad, '', showNewContent())
//    //    }
//    //    function showNewContent() {
//    //        $('#main').show('normal', hideLoader());
//    //    }
//    //    function hideLoader() {
//    //        $('#load').fadeOut('normal');
//    //    }
//    //return false;

//});

//
function urlencode(str) {

    var histogram = {}, histogram_r = {}, code = 0, tmp_arr = [];
    var ret = str.toString();

    var replacer = function (search, replace, str) {
        var tmp_arr = [];
        tmp_arr = str.split(search);
        return tmp_arr.join(replace);
    };

    // The histogram is identical to the one in urldecode.  
    histogram['!'] = '%21';
    histogram['%20'] = '+';

    // Begin with encodeURIComponent, which most resembles PHP's encoding functions  
    ret = encodeURIComponent(ret);

    for (search in histogram) {
        replace = histogram[search];
        ret = replacer(search, replace, ret) // Custom replace. No regexing  
    }

    // Uppercase for full PHP compatibility  
    return ret.replace(/(\%([a-z0-9]{2}))/g, function (full, m1, m2) {
        return "%" + m2.toUpperCase();
    });

    return ret;
}

function load(url, params) {

    $.ajax({
        type: "GET",
        url: url,
        data: params,
        context:"#main"
    });

}






 
    



