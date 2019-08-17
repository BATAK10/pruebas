<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="prjHeladeria.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if (usuario == "perla")
        { %>
    <div class="heading">
        <h4>Heladería</h4>
    </div>
    <div class="row">
        <div class="col-md-1">
            <div class="contat-form">
                <div class="row text-center">
                    <span style="color: white;">Gestión de contactos, productos y ventas.</span>
                </div>
                <div class="row">
                    <div class="col-md-8 col-sm-8"></div>
                    <div class="col-md-4 col-sm-4">
                        <img style="width: 60%" class="img-circle img-responsive" src="img/helados.jpg" alt="">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%}
        else %>
    <%if (usuario == "carina")
        { %>
    <div class="heading">
        <h4>Ventas</h4>
    </div>
    <div class="row">
        <div class="col-md-1">
            <div class="contat-form">
                <div class="row text-center">
                    <span style="color: white;">Gestión de contactos, productos y ventas.</span>
                </div>
                <div class="row">
                    <div class="col-md-8 col-sm-8"></div>
                    <div class="col-md-4 col-sm-4">
                        <img style="width: 60%" class="img-circle img-responsive" src="img/ventaropa.jpg" alt="">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%}
        else
        { %>
     <div class="heading">
        <h4>Bienvenido</h4>
    </div>
    <div class="row">
        <div class="col-md-1">
            <div class="contat-form">
                <div class="row text-center">
                    <span style="color: white;">Gestión de contactos, productos y ventas.</span>
                </div>
                <div class="row">
                    <div class="col-md-8 col-sm-8"></div>
                    <div class="col-md-4 col-sm-4">
                        <img style="width: 60%" class="img-circle img-responsive" src="img/project-item-02.jpg" alt="">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
