using Datos.Accesos;
using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen2Parcial
{
    public partial class FrmPedido : Form
    {
        public FrmPedido()
        {
            InitializeComponent();
        }

        Pedido pedido = new Pedido();
        ProductoDA productoDA = new ProductoDA();
        Producto producto;
        List<DetallePedido> detallePedidosLista = new List<DetallePedido>();
        PedidoDA PedidoDA = new PedidoDA();

        decimal subTotal = decimal.Zero;
        decimal isv = decimal.Zero;
        decimal totalAPagar = decimal.Zero;

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            DetalleDataGridView.DataSource = detallePedidosLista;
        }

        private void CodigoProductoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                producto = new Producto();
                producto = productoDA.GetProductoPorCodigo(CodigoProductoTextBox.Text);
                DescripcionTextBox.Text = producto.Descripcion;
                CantidadTextBox.Focus();

            }
            else 
            {
                producto = null;
                DescripcionTextBox.Clear();
                CantidadTextBox.Clear();
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                DetallePedido detallepedido = new DetallePedido();
                detallepedido.CodigoProducto = producto.Codigo;
                detallepedido.Descripcion = producto.Descripcion;
                detallepedido.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                detallepedido.Precio = producto.Precio;
                detallepedido.Total = producto.Precio * Convert.ToInt32(CantidadTextBox.Text);

                subTotal += detallepedido.Total;
                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv;

                SubTotalTextBox.Text = subTotal.ToString();
                ISVTextBox.Text = isv.ToString();
                TotalTextBox.Text = totalAPagar.ToString();

                detallePedidosLista.Add(detallepedido);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = detallePedidosLista;


            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            pedido.IdentidadCliente = IdentidadmaskedTextBox.Text;
            pedido.Cliente = NombreTextBox.Text;
            pedido.Fecha = FechaDateTimePicker.Value;
            pedido.SubTotal = Convert.ToDecimal(SubTotalTextBox.Text);
            pedido.ISV = Convert.ToDecimal(ISVTextBox.Text);
            pedido.Total = Convert.ToDecimal(TotalTextBox.Text);

            int idPedido = 0;

            idPedido = PedidoDA.InsertarPedido(pedido);

            if (idPedido != 0)
            {
                foreach (var item in detallePedidosLista)
                {
                    item.IdPedido = idPedido;
                    PedidoDA.InsertarDetalle(item);
                }
            }
        }
    }
}
