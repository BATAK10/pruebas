<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CategoriaProductoListado.aspx.cs" Inherits="prjHeladeria.CategoriaProductoListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Listado de categorías</h4>
        <button type="button" onclick="window.open('CategoriaProducto.aspx?o=1','_self')" class="btn">Agregar</button>
    </div>
    <div class="row">
        <div class="col-md-12">
            <%
                if (dgvListadoCategoriaProducto.Rows.Count == 0)
                {
            %>
            <div class="alert alert-danger">No hay categorías registradas.</div>
            <%
                }
            %>
            <div class="table-responsive">
                <asp:GridView ID="dgvListadoCategoriaProducto" AutoGenerateColumns="false" runat="server" CssClass="table table-hover">
                    <Columns>
                        <asp:BoundField DataField="ID_CATEGORIA_PRODUCTO" HeaderText="Código">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataTextField="NOMBRE_CATEGORIA_PRODUCTO" HeaderText="Nombre" DataNavigateUrlFormatString="~/CategoriaProducto.aspx?icp={0}&o=4" DataNavigateUrlFields="ID_CATEGORIA_PRODUCTO">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="ESTADO_CATEGORIA_PRODUCTO" HeaderText="Estado">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="estado info" />
                        </asp:BoundField>
                        <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-edit btn btn-info btn-sm" DataNavigateUrlFields="ID_CATEGORIA_PRODUCTO" HeaderText="Editar" DataNavigateUrlFormatString="CategoriaProducto.aspx?icp={0}&o=2">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-trash btn btn-danger btn-sm" DataNavigateUrlFields="ID_CATEGORIA_PRODUCTO" HeaderText="Eliminar" DataNavigateUrlFormatString="CategoriaProducto.aspx?icp={0}&o=3">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
