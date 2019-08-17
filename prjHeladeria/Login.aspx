<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="prjHeladeria.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Ingreso</h4>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="contat-form">
                <form id="contact" action="#" method="post">
                    <fieldset>
                        <input runat="server" name="usuario" type="text" class="form-control" id="txtUsuario" placeholder="Usuario" required>
                    </fieldset>
                    <fieldset>
                        <input runat="server" name="contraseña" type="password" class="form-control" id="txtContraseña" placeholder="Constraseña" required="">
                    </fieldset>
                    <fieldset>
                        <button type="button" id="form-submit" onclick="Ingresar()" class="btn">Ingresar</button>
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
        var usuario;
        $(document).ready(function () {
            $(".alert-danger").hide();
            $(".alert-success").hide();
            $(".ul-nav").hide();
        });
        function Ingresar() {
            $(".alert-danger").hide();
            $(".alert-success").hide();
            usuario = $("#ContentPlaceHolder1_txtUsuario").val();
            var contraseña = $("#ContentPlaceHolder1_txtContraseña").val();
            var mensaje = "";

            if ($.trim(usuario) == "")
                mensaje += "Debe ingresar usuario <br/>";
            if ($.trim(contraseña).trim() == "")
                mensaje += "Debe ingresar contraseña<br/>";
            if (mensaje != "") {
                $(".alert-danger").html(mensaje);
                $(".alert-danger").show();
                hideLoader(200);
            } else {
                var dataValue = {
                    "usuario": usuario,
                    "contraseña": contraseña
                };
                CallAjax("Login.aspx/Ingresar", dataValue, callOk);
            }
        }
        function callOk(retorno) {
            if (retorno == 1) {
                
                document.cookie = "usuario="+usuario;
                window.open("Default.aspx", "_self");
            }
        }
    </script>
</asp:Content>
