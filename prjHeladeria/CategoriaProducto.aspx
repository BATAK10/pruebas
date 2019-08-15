<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CategoriaProducto.aspx.cs" Inherits="prjHeladeria.CategoriaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Datos de categoría</h4>
        <button type="button" onclick="window.open('CategoriaProductoListado.aspx','_self')" class="btn">Listado</button>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="contat-form">
                <form id="contact" action="#" method="post">
                    <fieldset>
                        <input runat="server" name="id_categoria_producto" type="text" class="form-control" id="txtIdCategoriaProducto" placeholder="Código" disabled>
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="nombre_categoria_producto" type="text" class="form-control" id="txtNombreCategoriaProducto" placeholder="Nombre" required="">
                    </fieldset>
                    <fieldset>
                        <select runat="server" name="estado_categoria_producto" class="form-control" id="cmbEstadoCategoriaProducto">
                            <option value="0">Seleccione estado</option>
                            <option value="1">Activo</option>
                            <option value="2">Inactivo</option>
                        </select>
                    </fieldset>
                    <br />
                    <fieldset>
                        <button type="button" id="form-submit" onclick="OperarCategoriaProducto()" class="btn">Guardar</button>
                        <button type="button" id="form-cancel" onclick="window.open('CategoriaProductoListado.aspx','_self')" class="btn">Cancelar</button>
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
        $(document).ready(function () {
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
                $("#form-submit").attr("disabled", "disabled")
            }
        });
        function OperarCategoriaProducto() {            
            $(".alert-danger").hide();
            $(".alert-success").hide();
            var operacion = "<%=_Operacion%>";
            var id_categoria_producto = $("#ContentPlaceHolder1_txtIdCategoriaProducto").val();
            var nombre_categoria_producto = $("#ContentPlaceHolder1_txtNombreCategoriaProducto").val();
            var estado_categoria_producto = $("#ContentPlaceHolder1_cmbEstadoCategoriaProducto option:selected").val();
            var mensaje = "";
            if (operacion == "")
                mensaje += "Debe ingresar la operación <br/>";
            if (id_categoria_producto == "" && operacion != "1")
                mensaje += "Debe ingresar código de categoría <br/>";
            if (nombre_categoria_producto == "")
                mensaje += "Debe ingresar nombre de categoría <br/>";
            if (estado_categoria_producto == "0")
                mensaje += "Debe ingresar el estado del categoría <br/>";
            if (mensaje != "") {
                $(".alert-danger").html(mensaje);
                $(".alert-danger").show();
                hideLoader(200);
            } else {
                var dataValue = {
                    "operacion": operacion,
                    "id_categoria_producto": id_categoria_producto,
                    "nombre_categoria_producto": nombre_categoria_producto,
                    "estado_categoria_producto": estado_categoria_producto
                };
                CallAjax("CategoriaProducto.aspx/OperarCategoriaProducto", dataValue, callOk);
            }
        }
        function callOk(retorno) {
            if (retorno == 1) {
                $("#ContentPlaceHolder1_txtIdCategoriaProducto").val("");
                $("#ContentPlaceHolder1_txtNombreCategoriaProducto").val("");
                document.getElementById('ContentPlaceHolder1_cmbEstadoCategoriaProducto').getElementsByTagName('option')[0].selected = 'selected';
            }
        }
    </script>
</asp:Content>
