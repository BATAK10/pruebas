<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ClientesListado.aspx.cs" Inherits="prjHeladeria.frmClientesListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="heading">
        <h4>Listado de clientes</h4>
        <button type="button" onclick="window.open('Clientes.aspx?o=1','_self')" class="btn">Agregar</button>
    </div>
    <div class="row">
        <div class="col-md-12">
            <%
                if (dgvListadoClientes.Rows.Count == 0)
                {
            %>
            <div class="alert alert-danger">No hay clientes registrados.</div>
            <%
                }
            %>
            <div class="table-responsive">
                <asp:GridView ID="dgvListadoClientes" AutoGenerateColumns="false" runat="server" CssClass="table table-hover">
                    <Columns>
                        <asp:BoundField DataField="ID_CLIENTE" HeaderText="Código">
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="info text-center" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataTextField="NOMBRE_CLIENTE" HeaderText="Nombre" DataNavigateUrlFormatString="~/Clientes.aspx?idc={0}&o=4" DataNavigateUrlFields="ID_CLIENTE">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="APELLIDO_CLIENTE" HeaderText="Apellido">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TELEFONO_CLIENTE" HeaderText="Teléfono">
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="info text-center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DIRECCION_CLIENTE" HeaderText="Dirección">
                            <HeaderStyle CssClass="warning" />
                            <ItemStyle CssClass="info" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTADO_CLIENTE" HeaderText="Estado">
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="estado info text-center" />
                        </asp:BoundField>
                        <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-edit btn btn-info btn-sm" DataNavigateUrlFields="ID_CLIENTE" HeaderText="Editar" DataNavigateUrlFormatString="Clientes.aspx?idc={0}&o=2">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField ControlStyle-CssClass="glyphicon glyphicon-trash btn btn-danger btn-sm" DataNavigateUrlFields="ID_CLIENTE" HeaderText="Eliminar" DataNavigateUrlFormatString="Clientes.aspx?idc={0}&o=3">
                            <ItemStyle CssClass="info" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="warning" />
                        </asp:HyperLinkField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>  
</asp:Content>