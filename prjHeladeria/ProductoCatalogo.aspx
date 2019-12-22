<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductoCatalogo.aspx.cs" Inherits="prjHeladeria.ProductoCatalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Catálogo de productos <i class="glyphicon glyphicon-shopping-cart"></i></h4>
        <%--<button type="button" onclick="window.open('ProductoListado.aspx','_self')" class="btn">Ir al carrito <i class="glyphicon glyphicon-shopping-cart"></i></button>--%>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="contat-form">
                <form id="contact" action="#" method="post">
                    <%
                        int contador = 4;
                        int contador2 = 1;
                        foreach (System.Data.DataRow _Producto in dtDatos.Rows)
                        {
                            if ((contador % 4) == 0)
                            {
                                contador2 = 1;
                    %>
                    <div class="row img-row">
                        <%                                
                            }
                        %>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="text-center">
                                <img class="img-responsive img-thumbnail img-width" src="<%=_Producto["id_foto"].ToString()%>" />
                            </div>
                            <div class="row">
                                <div class="text-center text-white font-catalogo"><span class="bold">Nombre:</span> <%=_Producto["nombre_producto"].ToString()%></div>
                            </div>
                            <div class="row">
                                <div class="text-center text-white font-catalogo"><span class="bold">Precio: Q. </span><span class="formatoNumerosConComas"><%=_Producto["costo_producto"].ToString()%></span></div>
                            </div>
                            <div class="row">
                                <div class="text-center text-white font-catalogo"><span class="bold">Descripción: </span><span class=""><%=_Producto["descripcion_producto"].ToString()%></span></div>
                            </div>
                        </div>
                        <%                        
                            if (contador2 == 4)
                            {
                        %>
                    </div>
                    <%                                
                            }
                            contador2++;
                            contador++;
                        }
                    %>
                </form>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
        <%
        if (_usuario == null || _usuario == "")
        {
        %>
            $("#menu-principal").hide();
            $("#usuario-conectarse").hide();
        <%
        }
        %>
        });
    </script>
    <style>
        .img-row {
            padding-top: 10px;
        }

        .img-width {
            width: 300px !important;
            height: 200px !important;
        }

            .img-width:hover {
                transform: scale(1.2);
                filter: alpha(opacity=90);
                outline: 0;
                opacity: .9;
            }
    </style>
</asp:Content>
