<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VentaListado.aspx.cs" Inherits="prjHeladeria.VentaListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="mensajes">
        <%if (_MensajeSatisfactorio != "")
            { %>
        <div class="alert alert-success"><%=_MensajeSatisfactorio %></div>
        <%} %>
        <%if (_MensajeDeError != "")
            { %>
        <div class="alert alert-danger"><%=_MensajeDeError%></div>
        <%} %>
    </div>
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
                <asp:GridView ID="dgvListadoVenta" AutoGenerateColumns="false" runat="server" CssClass="table table-hover" OnRowEditing="dgvListadoVenta_RowEditing" OnRowDeleting="dgvListadoVenta_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="ID_VENTA" HeaderText="Código">
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="info text-center" />
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
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="info text-right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTADO_VENTA" HeaderText="Estado">
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="estado info text-center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Entregado">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEntregado" CommandName="Edit" ControlStyle-CssClass="glyphicon glyphicon-ok btn btn-success btn-sm" runat="server" OnClick="btnEntregado_Click">                                                        
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="estado info text-center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pendiente">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPendiente" CommandName="Delete" ControlStyle-CssClass="glyphicon glyphicon-time btn btn-warning btn-sm" runat="server" OnClick="btnPendiente_Click">                                    
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="warning text-center" />
                            <ItemStyle CssClass="estado info text-center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
