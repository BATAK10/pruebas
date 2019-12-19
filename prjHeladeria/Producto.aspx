<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="prjHeladeria.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/FileUpload.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Datos de producto</h4>
        <button type="button" onclick="window.open('ProductoListado.aspx','_self')" class="btn">Listado</button>
        <button type="button" onclick="window.open('ProductoCatalogo.aspx','_self')" class="btn">Catálogo <i class="glyphicon glyphicon-shopping-cart"></i></button>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="contat-form">
                <form id="contact" action="#" method="post">
                    <fieldset>
                        <input runat="server" name="id_producto" type="text" class="form-control" id="txtIdProducto" placeholder="Código" disabled>
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="nombre_producto" type="text" class="form-control" id="txtNombreProducto" placeholder="Nombre" required="">
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="costo_producto" type="text" class="form-control" id="txtCostoProducto" placeholder="Costo" required="">
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="cantidad_producto" type="text" class="form-control" id="txtCantidadProducto" placeholder="Cantidad en stock" required="">
                    </fieldset>
                    <fieldset>
                        <select runat="server" name="id_producto_servicio" class="form-control" id="cmbIdCategoriaProducto">
                        </select>
                    </fieldset>
                    <br />
                    <fieldset>
                        <select runat="server" name="estado_producto" class="form-control" id="cmbEstadoProducto">
                            <option value="0">Seleccione estado</option>
                            <option value="1" selected="selected">Activo</option>
                            <option value="2">Inactivo</option>
                        </select>
                    </fieldset>
                    <br />
                    <fieldset>
                        <span class="control-fileupload">
                            <label for="file">Seleccionar archivo</label>
                            <asp:FileUpload ID="fupArchivo" onchange="setImagen(this)" runat="server"></asp:FileUpload>
                        </span>
                    </fieldset>
                    <fieldset>
                        <asp:Image CssClass="img-responsive" Width="50%" runat="server" ID="imgPrevistaFotografia" />
                    </fieldset>
                    <br />
                    <fieldset>
                        <button type="button" id="form-submit" onclick="OperarProducto()" class="btn">Guardar</button>
                        <button type="button" id="form-cancel" onclick="window.open('ProductoListado.aspx','_self')" class="btn">Cancelar</button>
                        <button type="button" id="form-add" onclick="window.open('Producto.aspx','_self')" class="btn">Agregar otro</button>
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
        var FileUploadContent;
        function setImagen(input) {
            if (input.files && input.files[0]) {
                var _NombreArchivo = input.files[0].name.split(".");
                var _ExtensionArchivo = _NombreArchivo[_NombreArchivo.length - 1];
                if (_ExtensionArchivo.toUpperCase() == "PNG" || _ExtensionArchivo.toUpperCase() == "JPG" || _ExtensionArchivo.toUpperCase() == "JPEG") {
                    var reader = new FileReader();
                    var img = document.createElement("img");
                    reader.onload = function (e) {
                        debugger;
                        //img.src = e.target.result;
                        //var MAX_WIDTH = 800;
                        //var MAX_HEIGHT = 600;
                        //var width = img.naturalWidth;
                        //var height = img.naturalHeight;
                        ////resize the image if it higher than MAX_WIDTH or MAX_HEIGHT
                        ////if ((width > MAX_WIDTH) || (height > MAX_HEIGHT)) {
                        //if ((width / height) > (MAX_WIDTH / MAX_HEIGHT)) {
                        //    height *= MAX_WIDTH / width;
                        //    width = MAX_WIDTH;
                        //}
                        //else {
                        //    width *= MAX_HEIGHT / height;
                        //    height = MAX_HEIGHT;
                        //}
                        //var canvas = document.createElement("canvas");
                        //canvas.width = 800;
                        //canvas.height = 600;
                        //var ctx = canvas.getContext("2d");
                        //ctx.drawImage(img, 0, 0, 800, 600);
                        //var image = canvas.toDataURL("image/png");//.replace("image/png", "image/octet-stream");
                        ////}
                        //$('#ContentPlaceHolder1_imgPrevistaFotografia').attr('src', "");
                        //$('#ContentPlaceHolder1_imgPrevistaFotografia').attr('src', image);
                        $('#ContentPlaceHolder1_imgPrevistaFotografia').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                    FileUploadContent = input.files[0];
                } else {
                    $('#ContentPlaceHolder1_imgPrevistaFotografia').attr("src", "");
                }
            } else {
                $('#ContentPlaceHolder1_imgPrevistaFotografia').attr("src", "");
            }
        }
        $(document).ready(function () {
            $(".alert-danger").hide();
            $(".alert-success").hide();
            $("#form-add").hide();
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
            $('input[type=file]').change(function () {
                var t = $(this).val();
                var labelText = '' + t.substr(12, t.length);
                $(this).prev('label').text(labelText);
            });
        });
        function OperarProducto() {
            $(".alert-danger").hide();
            $(".alert-success").hide();
            var operacion = "<%=_Operacion%>";
            var id_producto = $("#ContentPlaceHolder1_txtIdProducto").val();
            var nombre_producto = $("#ContentPlaceHolder1_txtNombreProducto").val();
            var costo_producto = $("#ContentPlaceHolder1_txtCostoProducto").val();
            var cantidad_producto = $("#ContentPlaceHolder1_txtCantidadProducto").val();
            var id_categoria_producto = $("#ContentPlaceHolder1_cmbIdCategoriaProducto option:selected").val();
            var estado_producto = $("#ContentPlaceHolder1_cmbEstadoProducto option:selected").val();
            var mensaje = "";
            if (operacion == "")
                mensaje += "Debe ingresar la operación <br/>";
            if (id_producto == "" && operacion != "1")
                mensaje += "Debe ingresar código de producto <br/>";
            if (nombre_producto == "")
                mensaje += "Debe ingresar nombre de producto <br/>";
            if (id_categoria_producto == "0")
                mensaje += "Debe ingresar la categoría del producto<br/>";
            if (estado_producto == "0")
                mensaje += "Debe ingresar el estado del producto <br/>";
            if (mensaje != "") {
                $(".alert-danger").html(mensaje);
                $(".alert-danger").show();
                scrollTo(".alert-danger");
                hideLoader(200);
            } else {
                var dataValue = {
                    "operacion": operacion,
                    "usuario": "<%=Request.Cookies["usuario"].Value.ToString()%>",
                    "id_producto": id_producto,
                    "nombre_producto": nombre_producto,
                    "costo_producto": costo_producto,
                    "cantidad_producto": cantidad_producto,
                    "id_categoria_producto": id_categoria_producto,
                    "estado_producto": estado_producto,
                    "imagen_producto": ($('#ContentPlaceHolder1_imgPrevistaFotografia').attr("src"))
                };
                debugger;
                CallAjax("Producto.aspx/OperarProducto", dataValue, callOk)
            }
        }
        function callOk(retorno) {
            if (retorno == 1) {
                $("#ContentPlaceHolder1_txtIdProducto").val("");
                $("#ContentPlaceHolder1_txtNombreProducto").val("");
                $("#ContentPlaceHolder1_txtCostoProducto").val("");
                $("#ContentPlaceHolder1_txtCantidadProducto").val("");
                document.getElementById('ContentPlaceHolder1_cmbIdCategoriaProducto').getElementsByTagName('option')[0].selected = 'selected';
                document.getElementById('ContentPlaceHolder1_cmbEstadoProducto').getElementsByTagName('option')[0].selected = 'selected';
                $("#form-add").show();
                $("#form-submit").attr("disabled", true);
            }
        }
    </script>
</asp:Content>
