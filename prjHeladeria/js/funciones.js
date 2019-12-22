
window.onbeforeunload = window.onunload = function () {
    $(".loader").hide();
};
$(document).ready(function () {

    $(".loader").hide();
    setTimeout(function () {
        $(".loader").fadeOut("slow");
        $(".loader").attr("style", "display: none;");
    }
        , 5000);
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