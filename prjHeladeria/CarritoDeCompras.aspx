<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CarritoDeCompras.aspx.cs" Inherits="prjHeladeria.CarritoDeCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Carrito de compras <i class="glyphicon glyphicon-shopping-cart"></i></h4>
        <%--<button type="button" onclick="window.open('ProductoListado.aspx','_self')" class="btn">Ir al carrito <i class="glyphicon glyphicon-shopping-cart"></i></button>--%>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="contat-form">
                <form id="contact" action="#" method="post">
                    <div class="table-responsive">
                        <table id="tablaCursos" class="table table-bordered table-condensed">
                            <tr>
                                <th style="display: none !important;">IdCategoriaProducto</th>
                                <th style="display: none !important;">IdProducto</th>
                                <th>Producto</th>
                                <th>Nombre del evento</th>
                                <th class="text-center">Cantidad</th>
                                <th class="text-center">Precio</th>
                                <th class="text-center">Eliminar</th>
                            </tr>
                            <%
                                foreach (System.Data.DataRow _Fila in dtDatos.Rows)
                                {
                            %>
                            <tr id='id<%=_Fila["id_producto"] %>' class=''>
                                <td width="0%" style="display: none !important;" class='evento'><%=_Fila["id_categoria_producto"] %></td>
                                <td width="0%" style="display: none !important;" class='evento'><%=_Fila["id_producto"] %></td>
                                <td width="30%" class="text-center">
                                    <img class="img-responsive img-thumbnail img-width-carrito" src="<%=_Fila["id_foto"].ToString()%>" />
                                </td>

                                <td width="45%" class='evento' id='<%=_Fila["id_producto"] %>'>
                                    <a class="a-white" href="AsignacionParticipantesEventosACTAFIConsultar.aspx?ideu=<%=_Fila["id_producto"] %>"><%=_Fila["nombre_producto"] %></a>&nbsp;
                                <br />
                                    <small><%=_Fila["descripcion_producto"] %></small><br />
                                </td>
                                <td>
                                    <input type="text" class="soloNumeros cantidad" value="1" id="txtProducto<%=_Fila["id_producto"] %>" />
                                </td>

                                <td width="15%" class='text-right'><span class="formatoNumerosConComas costo"><%=_Fila["costo_producto"] %></span></td>
                                <td width="10%" class='text-center'><a class='btn btn-sm text-danger' onclick='eliminarCurso(<%=_Fila["id_producto"] %>)'><i class='glyphicon glyphicon-trash glyphicon-2x'></i></a></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td class="text-right"></td>
                                <td class="text-right"></td>
                                <td class="text-right">Total: </td>
                                <td class='text-right'><span id="costoTotal" class="formatoNumerosConComas"><%=_CostoTotal  %></span></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <fieldset>
                        <button type="button" id="form-submit" onclick="OperarPedido()" class="btn">Realizar pedido</button>
                        <button type="button" id="form-cancel" onclick="window.open('ProductoCatalogo.aspx','_self')" class="btn">Cancelar</button>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function eliminarCurso(idproducto) {
            $("#id" + idproducto).remove();
            CarritoEventos = "";
            var CarritoEventosStr = "n";
            var cookieArr = document.cookie.split(";");

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
                    if (idproducto == _ArregloTemporal[j].split(":")[0]) {
                    } else {
                        if (CarritoEventos == "") {
                            if (_ArregloTemporal[j] != "")
                                CarritoEventos = _ArregloTemporal[j];
                        } else {
                            if (_ArregloTemporal[j] != "")
                                CarritoEventos += "," + _ArregloTemporal[j];
                        }
                    }
                }
            }
            document.cookie = "HeladosEnCarrito=" + CarritoEventos + ";";
            location.reload();
        };
        $(".cantidad").on("change", function () {
            var total = 0;
            $.each($(".cantidad"), function () {
                debugger;
                var cantidad = $(this).val();
                var precio = $($(this).parent().parent()).find('.costo').text();
                total = total + (cantidad * precio);
            });

            $("#costoTotal").text(FormatoNumerosComas(total));
        });


        function OperarPedido() {
            $(".alert-danger").hide();
            $(".alert-success").hide();
            var operacion = "<%=_Operacion%>";
            var id_cliente_venta = "1";
            var costo_total_venta = $("#costoTotal").text();
            var estado_venta = 1;//$("#ContentPlaceHolder1_cmbEstadoVenta option:selected").val();
            var mensaje = "";
            if (operacion == "")
                mensaje += "Debe ingresar la operación <br/>";
            if (costo_total_venta == "" || costo_total_venta == "0")
                mensaje += "Debe ingresar el costo total de venta <br/>";
            if (estado_venta == "")
                mensaje += "Debe ingresar el estado de venta <br/>";
            if (mensaje != "") {
                $(".alert-danger").html(mensaje);
                $(".alert-danger").show();
                hideLoader(200);
            } else {
                // 1. Obtener detalle de venta
                var ventaDetalle = [];
                debugger;
                var tablaDatos = document.getElementById("tablaCursos");
                var data;
                for (var i = 1; i < tablaDatos.rows.length - 1; i++) {
                    data = {
                        "id_categoria_producto": tablaDatos.rows.item(i).cells[0].innerHTML,
                        "id_producto": tablaDatos.rows.item(i).cells[1].innerHTML,
                        "cantidad_producto": $(tablaDatos.rows.item(i).cells[4]).find('.cantidad').val(),
                        "costo_unitario": $(tablaDatos.rows.item(i).cells[5]).find('.costo').text(),
                        "costo_total": parseFloat($(tablaDatos.rows.item(i).cells[4]).find('.cantidad').val()) * parseFloat($(tablaDatos.rows.item(i).cells[5]).find('.costo').text())
                    }
                    ventaDetalle.push(data);
                }
                var dataValue = {
                    "operacion": operacion,
                    //"id_venta": id_venta,
                    "id_cliente_venta": id_cliente_venta,
                    //"fecha_venta": fecha_venta,
                    //"fecha_entrega_venta": fecha_entrega_venta,
                    "costo_total_venta": costo_total_venta,
                    "estado_venta": estado_venta,
                    "venta_detalle": JSON.stringify(ventaDetalle)
                };
                CallAjax("CarritoDeCompras.aspx/OperarPedido", dataValue, callOk)
            }
        }
        function callOk(retorno) {
            debugger;
            if (retorno == 1) {
                document.cookie = "HeladosEnCarrito=;";
            }
        }
    </script>
</asp:Content>
