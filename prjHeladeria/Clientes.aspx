<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="prjHeladeria.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Datos del cliente</h4>
        <button type="button" onclick="window.open('ClientesListado.aspx','_self')" class="btn">Listado</button>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="contat-form">
                <form id="contact" action="#" method="post">
                    <fieldset>
                        <input runat="server" name="id_cliente" type="text" class="form-control" id="txtIdCliente" placeholder="Código" disabled>
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="nombre_cliente" type="text" class="form-control" id="txtNombreCliente" placeholder="Nombre" required="">
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="apellido_cliente" type="text" class="form-control" id="txtApellidoCliente" placeholder="Apellido" required="">
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="telefono_cliente" type="text" class="form-control" id="txtTelefonoCliente" placeholder="Teléfono" required="">
                    </fieldset>
                    <fieldset>
                        <textarea runat="server" name="direccion_cliente" rows="3" class="form-control" id="txtDireccionCliente" placeholder="Dirección"></textarea>
                    </fieldset>
                    <fieldset>
                        <select runat="server" name="estado_cliente" class="form-control" id="cmbEstadoCliente">
                            <option value="0">Seleccione estado</option>
                            <option value="1">Activo</option>
                            <option value="2">Inactivo</option>
                        </select>
                    </fieldset>
                    <fieldset>
                        <button type="button" id="form-submit" onclick="OperarCliente()" class="btn">Guardar</button>
                        <button type="button" id="form-cancel" onclick="window.open('ClientesListado.aspx','_self')" class="btn">Cancelar</button>
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
        function OperarCliente() {
            $(".alert-danger").hide();
            $(".alert-success").hide();
            var operacion = "<%=_Operacion%>";
            var id_cliente = $("#ContentPlaceHolder1_txtIdCliente").val();
            var nombre_cliente = $("#ContentPlaceHolder1_txtNombreCliente").val();
            var apellido_cliente = $("#ContentPlaceHolder1_txtApellidoCliente").val();
            var telefono_cliente = $("#ContentPlaceHolder1_txtTelefonoCliente").val();
            var direccion_cliente = $("#ContentPlaceHolder1_txtDireccionCliente").val();
            var estado_cliente = $("#ContentPlaceHolder1_cmbEstadoCliente option:selected").val();
            var mensaje = "";
            if (operacion == "")
                mensaje += "Debe ingresar la operación <br/>";
            if (id_cliente == "" && operacion != "1")
                mensaje += "Debe ingresar código de cliente <br/>";
            if (nombre_cliente == "")
                mensaje += "Debe ingresar nombre de cliente <br/>";
            if (apellido_cliente == "")
                mensaje += "Debe ingresar apellido de cliente <br/>";
            if (telefono_cliente == "")
                mensaje += "Debe ingresar un número de teléfono <br/>";
            if (estado_cliente == "0")
                mensaje += "Debe ingresar el estado del cliente  <br/>";
            if (mensaje != "") {
                $(".alert-danger").html(mensaje);
                $(".alert-danger").show();
                scrollTo(".alert-danger");
                hideLoader(200);
            } else {
                var dataValue = {
                    "operacion": operacion,
                    "usuario": "<%=Request.Cookies["usuario"].Value.ToString()%>",
                    "id_cliente": id_cliente,
                    "nombre_cliente": nombre_cliente,
                    "apellido_cliente": apellido_cliente,
                    "telefono_cliente": telefono_cliente,
                    "direccion_cliente": direccion_cliente,
                    "estado_cliente": estado_cliente
                };
                CallAjax("Clientes.aspx/OperarCliente", dataValue, callOk);
            }
        }
        function callOk(retorno) {
            if (retorno == 1) {
                $("#ContentPlaceHolder1_txtIdCliente").val("");
                $("#ContentPlaceHolder1_txtNombreCliente").val("");
                $("#ContentPlaceHolder1_txtApellidoCliente").val("");
                $("#ContentPlaceHolder1_txtTelefonoCliente").val("");
                $("#ContentPlaceHolder1_txtDireccionCliente").val("");
                document.getElementById('ContentPlaceHolder1_cmbEstadoCliente').getElementsByTagName('option')[0].selected = 'selected';
            }
        }
    </script>
</asp:Content>
