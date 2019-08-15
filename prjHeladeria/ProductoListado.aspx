<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductoListado.aspx.cs" Inherits="prjHeladeria.ProductoListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Listado de productos</h4>
        <button type="button" onclick="window.open('Producto.aspx?o=1','_self')" class="btn">Agregar</button>
    </div>
    <div class="row">
        <div class="col-md-12">
            <%
                if (dgvListadoProducto.Rows.Count == 0)
                {
            %>
            <div class="alert alert-danger">No hay productos registrados.</div>
            <%
                }
            %>
            <div class="table-responsive">
                <asp:GridView ID="dgvListadoProducto" AutoGenerateColumns="false" runat="server" CssClass="table table-hover">
                    <Columns>
                        <asp:BoundField DataField="ID_PRODUCTO" HeaderText="Código">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataTextField="NOMBRE_PRODUCTO" HeaderText="Nombre" DataNavigateUrlFormatString="~/Producto.aspx?idp={0}&o=4" DataNavigateUrlFields="ID_PRODUCTO">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="COSTO_PRODUCTO" HeaderText="Costo">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CANTIDAD_PRODUCTO" HeaderText="Cantidad en stock">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COSTO_PRODUCTO" HeaderText="Costo">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOMBRE_CATEGORIA_PRODUCTO" HeaderText="Categoría">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTADO_PRODUCTO" HeaderText="Estado">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="estado info" />
                        </asp:BoundField>
                        <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-edit btn btn-info btn-sm" DataNavigateUrlFields="ID_PRODUCTO" HeaderText="Editar" DataNavigateUrlFormatString="Producto.aspx?idp={0}&o=2">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-trash btn btn-danger btn-sm" DataNavigateUrlFields="ID_PRODUCTO" HeaderText="Eliminar" DataNavigateUrlFormatString="Producto.aspx?idp={0}&o=3">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
