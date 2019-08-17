<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VentaListado.aspx.cs" Inherits="prjHeladeria.VentaListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Listado de ventas</h4>
        <button type="button" onclick="window.open('Venta.aspx?o=1','_self')" class="btn">Agregar</button>
    </div>
    <div class="row">
        <div class="col-md-12">
            <%
                if (dgvListadoVenta.Rows.Count == 0)
                {
            %>
            <div class="alert alert-danger">No hay ventas registradas.</div>
            <%
                }
            %>
            <div class="table-responsive">
                <asp:GridView ID="dgvListadoVenta" AutoGenerateColumns="false" runat="server" CssClass="table table-hover">
                    <Columns>
                        <asp:BoundField DataField="ID_VENTA" HeaderText="Código">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>

                        <asp:HyperLinkField DataTextField="NOMBRE_CLIENTE" HeaderText="Cliente" DataNavigateUrlFormatString="~/Venta.aspx?idv={0}&o=4" DataNavigateUrlFields="ID_VENTA">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField DataTextField="FECHA_VENTA" HeaderText="Fecha" DataTextFormatString="{0:dd/MM/yyyy}" DataNavigateUrlFormatString="~/Venta.aspx?idv={0}&o=4" DataNavigateUrlFields="ID_VENTA">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:HyperLinkField>

                        <asp:BoundField DataField="FECHA_ENTREGA_VENTA" HeaderText="F. entrega" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>

                        <asp:BoundField DataField="COSTO_TOTAL_VENTA" HeaderText="Costo total">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTADO_VENTA" HeaderText="Estado">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="estado info" />
                        </asp:BoundField>
                       <%-- <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-edit btn btn-info btn-sm" DataNavigateUrlFields="ID_VENTA" HeaderText="Editar" DataNavigateUrlFormatString="Venta.aspx?idv={0}&o=2">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-trash btn btn-danger btn-sm" DataNavigateUrlFields="ID_VENTA" HeaderText="Eliminar" DataNavigateUrlFormatString="Venta.aspx?idv={0}&o=3">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>--%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
