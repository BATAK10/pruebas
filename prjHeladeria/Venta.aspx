<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="prjHeladeria.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Datos de venta</h4>
        <button type="button" onclick="window.open('VentaListado.aspx','_self')" class="btn">Listado</button>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="contat-form">
                <form id="contact" action="#" method="post">
                    <fieldset>
                        <input runat="server" name="id_venta" type="number" class="form-control" id="txtIdVenta" placeholder="Código" disabled>
                    </fieldset>
                    <fieldset>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cmbIdClienteVenta"></asp:DropDownList>
                    </fieldset>
                    <fieldset>
                        <select runat="server" class="form-control" onchange="ObtenerProducto()" id="cmbIdCategoriaProductoVenta"></select>
                    </fieldset>

                    <fieldset>
                        <select runat="server" class="form-control" onchange="ObtenerPrecioUnitario()" id="cmbIdProductoVenta"></select>
                    </fieldset>

                    <fieldset>
                        <input runat="server" name="fecha_venta" type="text" class="form-control datepicker" id="txtFechaVenta2" placeholder="Fecha de venta" required="" disabled>
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="fecha_entrega_venta" type="date" class="form-control" id="txtFechaEntregaVenta" placeholder="Fecha de entrega" required="">
                    </fieldset>
                    <div class="home-content">
                        <h5 style="color: white">Detalle de venta</h5>
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <div class="home-box-content">
                                    <fieldset>
                                        <input runat="server" name="cantidad_venta" type="number" class="form-control" id="txtCantidadVenta" placeholder="Cantidad" required="">
                                        <input runat="server" name="costo_unidad" type="number" class="form-control" id="txtCostoUnidad" placeholder="Costo unidad" required="">
                                        <input runat="server" name="cantidad_stock_producto" type="number" class="form-control" id="txtCantidadStockProducto" placeholder="Cantidad en stock" required="">
                                    </fieldset>
                                    <fieldset>
                                        <input runat="server" name="costo_total_producto" type="number" class="form-control" id="txtCostoTotal" placeholder="Costo total" disabled required="">
                                    </fieldset>
                                    <fieldset>
                                        <button type="button" id="form-submit-detail" onclick="OperarVentaDetalle()" class="btn noShowLoader"><i class="glyphicon glyphicon-plus"></i></button>
                                    </fieldset>
                                    <div class="table-responsive">
                                        <table id="tblVentaDetalle" class="table table-hover table-bordered">
                                            <tr>
                                                <th class="warning">Id Categoría</th>
                                                <th class="warning">Categoría</th>
                                                <th class="warning">Id Producto</th>
                                                <th class="warning">Producto</th>
                                                <th class="warning">Cantidad stock</th>
                                                <th class="warning">Cantidad</th>
                                                <th class="warning">Costo unitario</th>
                                                <th class="warning">Costo total</th>
                                                <th class="warning">Eliminar</th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <fieldset>
                        <input runat="server" name="costo_total_venta" type="number" class="form-control" id="txtCostoTotalVenta" placeholder="Costo total" disabled required="">
                    </fieldset>
                    <fieldset>
                        <select runat="server" name="estado_venta" class="form-control" id="cmbEstadoVenta">
                            <option value="0">Seleccione estado</option>
                            <option value="1">Pendiente</option>
                            <option value="2">Entregado</option>
                        </select>
                    </fieldset>

                    <fieldset>
                        <button type="button" id="form-submit" onclick="OperarVenta()" class="btn">Guardar</button>
                        <button type="button" id="form-cancel" onclick="window.open('VentaListado.aspx','_self')" class="btn">Cancelar</button>
                    </fieldset>
                    <fieldset>
                    </fieldset>
                    <div class="alert alert-danger">
                    </div>
                    <div class="alert alert-success">
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var costo_total_venta_ = 0;
        $(document).ready(function () {
            costo_total_venta_ = 0;
            $(".alert-danger").hide();
            $(".alert-success").hide();
            var operacion = "<%=_Operacion%>";
            if (operacion == "1") {
                $("#form-submit").text("Agregar")
            }
            if (operacion == "2") {
                $("#form-submit").text("Modificar")
            }
            if (operacion == "3") {
                $("#form-submit").text("Eliminar")
            }
            if (operacion == "4") {
                LlenarGridVentaDetalle();
                $("#form-submit-detail").attr("disabled", "disabled");
                $("#form-submit").attr("disabled", "disabled")
            }
        });
        function LlenarGridVentaDetalle() {
            var table = document.getElementById("tblVentaDetalle");
            <%
        foreach (System.Data.DataRow _Detalle in dtDatosDetalle.Rows)
        {
            %>
            var contadorFilas = ($('#tblVentaDetalle tr').length - 1) + 1;
            var row = table.insertRow(contadorFilas);
            $(row).addClass("info");
            var idt_categoria = row.insertCell(0);
            var categoria = row.insertCell(1);
            var idt_producto = row.insertCell(2);
            var producto = row.insertCell(3);
            var cantidad_stock = row.insertCell(4);
            var cantidad = row.insertCell(5);
            var costo_unitario = row.insertCell(6);
            var costo_total = row.insertCell(7);
            var btnEliminar = row.insertCell(8);
            idt_categoria.innerHTML = "<%=_Detalle["id_categoria_producto_venta"]%>";
            categoria.innerHTML = "<%=_Detalle["nombre_categoria_producto"]%>";
            idt_producto.innerHTML = "<%=_Detalle["id_producto_venta"]%>";
            producto.innerHTML = "<%=_Detalle["nombre_producto"]%>";
            cantidad_stock.innerHTML = "";
            cantidad.innerHTML = "<%=_Detalle["cantidad_venta"]%>";
            costo_unitario.innerHTML = "";
            costo_total.innerHTML = "<%=_Detalle["costo_total_venta"]%>";
            btnEliminar.innerHTML = "<button disabled='disabled' type='button' id='" + contadorFilas + "' class='btn'><i class='glyphicon glyphicon-remove'></i></button>";
            <%
        }
            %>
        }
        function OperarVentaDetalle() {
            var operacion = 1;
            var mensaje = "";
            
            var id_categoria_producto = $("#ContentPlaceHolder1_cmbIdCategoriaProductoVenta option:selected");
            var id_producto = $("#ContentPlaceHolder1_cmbIdProductoVenta option:selected");
            var cantidad_producto = $("#ContentPlaceHolder1_txtCantidadVenta");
            var costo_unidad_producto = $("#ContentPlaceHolder1_txtCostoUnidad");
            var costo_total_producto = $("#ContentPlaceHolder1_txtCostoTotal");
            var cantidad_stock_producto = $("#ContentPlaceHolder1_txtCantidadStockProducto");
            if (id_categoria_producto.val() == "" || id_categoria_producto.val() == "0")
                mensaje += "Debe ingresar una categoría de producto<br/>";
            if (id_producto.val() == "" || id_producto.val() == "0")
                mensaje += "Debe ingresar un producto<br/>";
            if (cantidad_producto.val() == "" || cantidad_producto.val() == "0")
                mensaje += "Debe ingresar la cantidad de venta <br/>";
            if (costo_unidad_producto.val() == "" || costo_unidad_producto.val() == "0")
                mensaje += "Debe ingresar el costo por unidad<br/>";
            if (costo_total_producto.val() == "" || costo_total_producto.val() == "0")
                mensaje += "Debe ingresar el costo total<br/>";
            if (cantidad_stock_producto.val() == "" || cantidad_stock_producto.val() == "0")
                mensaje += "Debe ingresar la cantidad en stock del producto<br/>";

            if (mensaje != "") {
                $(".alert-danger").html(mensaje);
                $(".alert-danger").show();
                scrollTo(".alert-danger");
                hideLoader(200);
            } else {
                $(".alert-danger").html("");
                $(".alert-danger").hide();
                var table = document.getElementById("tblVentaDetalle");
                var contadorFilas = ($('#tblVentaDetalle tr').length - 1) + 1;
                var row = table.insertRow(contadorFilas);
                $(row).addClass("info");
                var idt_categoria = row.insertCell(0);
                var categoria = row.insertCell(1);
                var idt_producto = row.insertCell(2);
                var producto = row.insertCell(3);
                var cantidad_stock = row.insertCell(4);
                var cantidad = row.insertCell(5);
                var costo_unitario = row.insertCell(6);
                var costo_total = row.insertCell(7);
                var btnEliminar = row.insertCell(8);
                idt_categoria.innerHTML = id_categoria_producto.val();
                categoria.innerHTML = id_categoria_producto.text();
                idt_producto.innerHTML = id_producto.val();
                producto.innerHTML = id_producto.text();
                cantidad_stock.innerHTML = cantidad_stock_producto.val();
                cantidad.innerHTML = cantidad_producto.val();
                costo_unitario.innerHTML = costo_unidad_producto.val();
                costo_total.innerHTML = costo_total_producto.val();
                btnEliminar.innerHTML = "<button onclick='EliminarFila(this)' type='button' id='" + contadorFilas + "' class='btn'><i class='glyphicon glyphicon-remove'></i></button>";
                costo_total_venta_ = parseFloat(costo_total_venta_) + parseFloat(costo_total_producto.val());
                $("#ContentPlaceHolder1_txtCostoTotalVenta").val(costo_total_venta_);
                // Limpiar campos
                document.getElementById('ContentPlaceHolder1_cmbIdProductoVenta').getElementsByTagName('option')[0].selected = 'selected';
                $("#ContentPlaceHolder1_txtCantidadVenta").val("");
                $("#ContentPlaceHolder1_txtCostoUnidad").val("");
                $("#ContentPlaceHolder1_txtCostoTotal").val("");
                $("#ContentPlaceHolder1_txtCantidadStockProducto").val("");

            }
        }
        function EliminarFila(botonEliminar) {
            
            var idFila = $(botonEliminar).attr("id");
            var table = document.getElementById("tblVentaDetalle");
            var costo_total_fila = table.rows.item(idFila).cells[7].innerHTML;
            costo_total_venta_ = parseFloat(costo_total_venta_) - parseFloat(costo_total_fila);
            $("#ContentPlaceHolder1_txtCostoTotalVenta").val(costo_total_venta_);
            table.deleteRow(idFila);
        }
        function OperarVenta() {
            $(".alert-danger").hide();
            $(".alert-success").hide();
            var operacion = "<%=_Operacion%>";
            var id_venta = $("#ContentPlaceHolder1_txtIdVenta").val();
            var id_cliente_venta = $("#ContentPlaceHolder1_cmbIdClienteVenta option:selected").val();
            //var id_categoria_producto_venta = $("#ContentPlaceHolder1_cmbIdCategoriaProductoVenta option:selected").val();
            //var id_producto_venta = $("#ContentPlaceHolder1_cmbIdProductoVenta option:selected").val();
            var fecha_venta = $("#ContentPlaceHolder1_txtFechaVenta2").val();
            var fecha_entrega_venta = $("#ContentPlaceHolder1_txtFechaEntregaVenta").val();
            //var cantidad_venta = $("#ContentPlaceHolder1_txtCantidadVenta").val();
            var costo_total_venta = $("#ContentPlaceHolder1_txtCostoTotalVenta").val();
            //var cantidad_stock_producto = $("#ContentPlaceHolder1_txtCantidadStockProducto").val();
            var estado_venta = $("#ContentPlaceHolder1_cmbEstadoVenta option:selected").val();
            var mensaje = "";
            if (operacion == "")
                mensaje += "Debe ingresar la operación <br/>";
            if (id_cliente_venta == "" || id_cliente_venta == "0")
                mensaje += "Debe ingresar código cliente<br/>";
            if (id_venta == "" && operacion != "1")
                mensaje += "Debe ingresar código de venta<br/>";
            if (fecha_venta == "")
                mensaje += "Debe ingresar el fecha de venta <br/>";
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
                
                var tablaDatos = document.getElementById("tblVentaDetalle");
                var data;
                for (var i = 1; i < tablaDatos.rows.length; i++) {
                    data = {
                        "id_categoria_producto": tablaDatos.rows.item(i).cells[0].innerHTML,
                        "id_producto": tablaDatos.rows.item(i).cells[2].innerHTML,
                        "cantidad_stock_producto": tablaDatos.rows.item(i).cells[4].innerHTML,
                        "cantidad_producto": tablaDatos.rows.item(i).cells[5].innerHTML,
                        "costo_unitario": tablaDatos.rows.item(i).cells[6].innerHTML,
                        "costo_total": tablaDatos.rows.item(i).cells[7].innerHTML
                    }
                    ventaDetalle.push(data);
                }
                var dataValue = {
                    "operacion": operacion,
                    "usuario": "<%=Request.Cookies["usuario"].Value.ToString()%>",
                    "id_venta": id_venta,
                    "id_cliente_venta": id_cliente_venta,
                    "fecha_venta": fecha_venta,
                    "fecha_entrega_venta": fecha_entrega_venta,
                    "costo_total_venta": costo_total_venta,
                    "estado_venta": estado_venta,
                    "venta_detalle": JSON.stringify(ventaDetalle)
                };
                CallAjax("Venta.aspx/OperarVenta", dataValue, callOk)
            }
        }
        function callOk(retorno) {
            if (retorno == 1) {
                $("#ContentPlaceHolder1_txtIdVenta").val("");
                document.getElementById('ContentPlaceHolder1_cmbIdClienteVenta').getElementsByTagName('option')[0].selected = 'selected';
                $("#ContentPlaceHolder1_txtFechaVenta2").val("");
                $("#ContentPlaceHolder1_txtFechaEntregaVenta").val("");
                $("#ContentPlaceHolder1_txtCostoTotalVenta").val("");
                document.getElementById('ContentPlaceHolder1_cmbEstadoVenta').getElementsByTagName('option')[0].selected = 'selected';
            }
        }
        $("#ContentPlaceHolder1_txtCantidadVenta").on("change", function () {
            var cantidad = parseInt($(this).val());
            var cantidad_stock = parseInt($("#ContentPlaceHolder1_txtCantidadStockProducto").val());
            if (cantidad > cantidad_stock) {
                $("#ContentPlaceHolder1_txtCostoTotal").val("0");
                $(".alert-error").html("Error: La cantidad ingresada sobrepasa la cantidad en stock del producto");
                $(".alert-error").show();
            } else {
                var total = parseFloat($(this).val()) * parseFloat($("#ContentPlaceHolder1_txtCostoUnidad").val());
                $("#ContentPlaceHolder1_txtCostoTotal").val(total);
            }
        });
        function ObtenerProducto() {
            var id_categoria_producto_venta = $("#ContentPlaceHolder1_cmbIdCategoriaProductoVenta option:selected").val();
            var dataValue = {
                "id_categoria_producto_venta": id_categoria_producto_venta,
                "usuario": "<%=_usuario%>",
            }
            LlenarCombo("Venta.aspx/ObtenerProducto", dataValue, "ContentPlaceHolder1_cmbIdProductoVenta")
        }
        function ObtenerPrecioUnitario() {
            var id_producto_venta = $("#ContentPlaceHolder1_cmbIdProductoVenta option:selected").val();
            var dataValue = {
                "id_producto_venta": id_producto_venta,
                "usuario": "<%=_usuario%>",
            }
            CallAjaxValor("Venta.aspx/ObtenerPrecioYStock", dataValue, callOkPrecio)
            function callOkPrecio(listaProductos) {
                $("#ContentPlaceHolder1_txtCostoUnidad").val(listaProductos.dato[0][0]);
                $("#ContentPlaceHolder1_txtCantidadStockProducto").val(listaProductos.dato[0][1]);
            }
        }
        function LlenarCombo(url, dataValue, idcombo) {

            CallAjaxValor(url, dataValue, callOkProducto)

            function callOkProducto(listaProductos) {
                var select = document.getElementById(idcombo);
                select.options[select.options.length] = new Option("Seleccione valor", "0");
                var cantidadProductos = listaProductos.dato.length;
                for (var i = 0; i < cantidadProductos; i++) {
                    if (listaProductos.dato[i] != null)
                        select.options[select.options.length] = new Option(listaProductos.dato[i][1], listaProductos.dato[i][0]);
                }
            }
        }
    </script>
</asp:Content>
