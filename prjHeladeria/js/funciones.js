$(document).ready(function () {
    $(".loader").hide();

    $("a").on("click", function () {
        if ($(this).hasClass("noShowLoader"))
            $(".loader").hide();
        else
            $(".loader").show();
    });
    $("button").on("click", function () {
        $(".loader").show();
    });

    $.each($(".estado"), function () {
        var estado = $(this).text();
        if (estado == "ACTIVO") {
            $(this).html("<div class='label label-success'>Activo</div>")
        } else if (estado == "INACTIVO") {
            $(this).html("<div class='label label-danger'>Inactivo</div>")
        }
    })
});
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

function hideLoader(timeout) {
    setTimeout(function () {
        $(".loader").hide();
    }, timeout);
}