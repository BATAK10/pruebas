
window.onbeforeunload = window.onunload = function () {
    $(".loader").hide();
};
$(document).ready(function () {
    $("#divCarrito").hide();
    $("#alertaCarrito").hide();
    $(".loader").hide();

    $("button").on("click", function () {
        if (!$(this).hasClass("noShowLoader")) {
            $(".loader").show();
        }

    });

    $("a").on("click", function () {
        if (!$(this).hasClass("noShowLoader")) {
            $(".loader").show();
        }

    });

    $.each($(".estado"), function () {
        var estado = $(this).text();
        if (estado == "ACTIVO" || estado == "Entregado") {
            $(this).html("<div class='label label-success'>" + estado.toLowerCase() + "</div>")
        } else if (estado == "INACTIVO" || estado == "Pendiente") {
            $(this).html("<div class='label label-danger'>" + estado.toLowerCase() + "</div>")
        }
    });
    $.each($(".pedido"), function () {
        var pedido = $(this).text();
        if (pedido == "Pedido") {
            $(this).html("<div class='label label-danger'>" + pedido.toLowerCase() + "</div>")
        } else if (pedido == "Venta") {
            $(this).html("<div class='label label-primary'>" + pedido.toLowerCase() + "</div>")
        }
    });
    $.each($(".formatoNumerosConComas"), function () {
        $(this).text(FormatoNumerosComas($(this).text()));
    });
    $.each($(".formatoNumerosEnteros"), function () {
        $(this).text(FormatoNumerosEnteros($(this).text()));
    });
    $.each($(".formatoNumerosEnterosConComas"), function () {
        $(this).text(FormatoNumerosEnterosConComas($(this).text()));
    });
    // For select 2
    $("select").select2({ width: '100%' });

    $(function () {
        $('.datepicker').datepicker({
            dateFormat: 'dd/mm/yy',
            showButtonPanel: false,
            changeMonth: false,
            changeYear: false,
            /*showOn: "button",
            buttonImage: "images/calendar.gif",
            buttonImageOnly: true,
            minDate: '+1D',
            maxDate: '+3M',*/
            inline: true,
            changeYear: true
        });
    });
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);

    // pintar carrito de compras
    var CarritoEventosStr = "n";
    var cookieArr = document.cookie.split(";");

    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");
        if ("HeladosEnCarrito" == cookiePair[0].trim()) {
            CarritoEventosStr = (cookiePair[1]);
            break;
        }
    }
    var agregados = 0;
    if (CarritoEventosStr != "n") {
        var _ArregloTemporal = CarritoEventosStr.toString().split(",");
        var _ContadorArregloTemporal = _ArregloTemporal.length;
        var contenidoHtmlCarrito = "";
        for (var j = 0; j < _ContadorArregloTemporal; j++) {
            var evento = _ArregloTemporal[j].split(":");
            var idEvento = evento[0];
            var nombreEvento = evento[1];
            if (idEvento != "") {
                contenidoHtmlCarrito = "<span class='evento' id='" + idEvento + "'><p>" + nombreEvento + "</p></span>";
                $("#listaProductos").append(contenidoHtmlCarrito);
                agregados++;
            }

        }
        if (agregados > 0) {
            $("#listaProductos").append("<div style='float:right'><a href='CarritoDeCompras.aspx'>Ir al carrito</a></div>");
        }
        $("#numeroEventosCarrito").text(agregados);
    }
    $("#iCarrito").on("click", function () {
        if ($("#divCarrito").is(":visible")) {
            $("#divCarrito").hide();
        } else
            $("#divCarrito").show();
    });

});
function FormatoNumerosComas(valor) {
    if ($.trim(valor) == "") {
        return "0.00";
    }
    valor = parseFloat(valor);
    valor = valor.toFixed(2);
    return valor.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function CallAjax(url, dataValue, callOk) {
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(dataValue),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".alert-error").html("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            $(".alert-error").show();
            $(".loader").hide();
            callOk(-1);
        },
        success: function (result) {
            $(".loader").hide();
            if (result.d.indexOf("Error") > -1) {
                $(".alert-danger").html(result.d);
                $(".alert-danger").show();
                callOk(0);
            } else {
                $(".alert-success").html("Completado: " + result.d);
                $(".alert-success").show();
                callOk(1);
            }
        }
    });
}

function CallAjaxValor(url, dataValue, callOk) {
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(dataValue),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".alert-error").html("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            $(".alert-error").show();
            $(".loader").hide();
            callOk(-1);
        },
        success: function (result) {
            $(".loader").hide();
            if (result.d.indexOf("Error") > -1) {
                $(".alert-danger").html(result.d);
                $(".alert-danger").show();
                callOk(0);
            } else {
                callOk($.parseJSON(result.d));
            }
        }
    });
}

function hideLoader(timeout) {
    setTimeout(function () {
        $(".loader").hide();
    }, timeout);
}

$(".soloNumeros").on("keyup", function (e) {
    soloNumeros(this, e);
});

$(".soloDecimal").on("keyup", function (e) {
    soloNumerosDecimales(this, e);
});

function soloNumeros(input, event) {
    var valorRetorno = 1;
    if (event.keyCode != 13 && event.keyCode != 8 && event.keyCode != 9 && event.keyCode != 16 && event.keyCode != 35 && event.keyCode != 36 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46) {
        $(input).val($(input).val().replace(/[^0-9]/g, ""));
        if ((event.which < 48 || event.which > 57)) {
            valorRetorno = -1;
            event.preventDefault();
        } else {
            valorRetorno = 0;
        }
    }
    return valorRetorno;
}
function soloNumerosDecimales(input, e) {
    ejecutarExpresionRegular(input, e, /^([0-9]+[\.]?[0-9]?[0-9]?|[0-9]+)$/g, /^([0-9]+[\.]?[0-9]?[0-9]?|[0-9]+)/g);
}
function ejecutarExpresionRegular(input, e, expresionRegular1, expresionRegular2) {
    var valorRetorno = 1;
    var valorInput = input.value;
    var expresionRegularDecimales1 = expresionRegular1;
    var expresionRegularDecimales2 = expresionRegular2;
    if (expresionRegularDecimales1.test(valorInput)) {
        valorRetorno = 0;
    } else {
        valorInput = expresionRegularDecimales2.exec(valorInput);
        if (valorInput) {
            valorRetorno = 0;
            input.value = valorInput[0];
        } else {
            valorRetorno = -1;
            input.value = "";
        }
    }
    return valorRetorno;
}
function scrollTo(id) {

    $('html, body').animate({
        scrollTop: $(id).offset().top
    }, 1000)
}

function AgregarAlCarrito(evento) {
    debugger;
    CarritoEventos = "";
    var CarritoEventosStr = "n";
    var cookieArr = document.cookie.split(";");

    // Loop through the array elements
    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");

        if ("HeladosEnCarrito" == cookiePair[0].trim()) {
            CarritoEventosStr = (cookiePair[1]);
            break;
        }
    }
    if (CarritoEventosStr == "n") {
    } else {
        var _ArregloTemporal = CarritoEventosStr.toString().split(",");
        var _ContadorArregloTemporal = _ArregloTemporal.length;
        var _Existe = false;
        for (var j = 0; j < _ContadorArregloTemporal; j++) {
            if (evento.split(":")[0] == _ArregloTemporal[j].split(":")[0]) {
                _Existe = true;
            }
            if (CarritoEventos == "") {
                if (_ArregloTemporal[j] != "")
                    CarritoEventos = _ArregloTemporal[j];
            } else {
                if (_ArregloTemporal[j] != "")
                    CarritoEventos += "," + _ArregloTemporal[j];
            }
        }
    }
    if (_Existe) {
        $("#alertaCarrito").removeClass("alert-success")
        $("#alertaCarrito").addClass("alert-warning")
        $("#alertaCarrito").text("Este producto ya está agregado en el carrito");
        $("#alertaCarrito").show();
        $(".loader").hide();
        setTimeout(function () {
            $(".loader").hide();
        }
            , 500);
    } else {
        $("#alertaCarrito").removeClass("alert-warning")
        $("#alertaCarrito").addClass("alert-success")
        $("#alertaCarrito").text("Se ha agregado al carrito con éxito");
        $("#alertaCarrito").show();
        if (CarritoEventos == "") {
            CarritoEventos = evento;
        } else {
            CarritoEventos += "," + evento;
        }
        document.cookie = "HeladosEnCarrito=" + CarritoEventos + ";";
        //location.reload();
        var _UlrHref = window.location.href;
        _UlrHref = _UlrHref.replace("&agr=1", "");
        window.open(_UlrHref, "_self");
    }

}
function sendMail(subject, body) {
    var link = "mailto:perlavemis@gmail.com"
        + "?cc=dwdrumsbryan@gmail.com"
        + "&subject=" + escape(subject)
        + "&body=" + escape(body)
        ;
    window.location.href = link;
}

function enviarCorreoJS(pedidoPor) {
    debugger;
    var template_params = {
        "reply_to": "perlavemis@gmail.com",
        "from_name": "Heladería",
        "to_name": "Perla",
        "message_html": "Se ha ingresado un nuevo pedido por "+pedidoPor+". </br> <a href='http://localhost:57719/VentaListado.aspx'>Ver pedidos</a>"
    }

    var service_id = "default_service";
    var template_id = "template_mG7uT5fp";
    emailjs.send(service_id, template_id, template_params);
}