using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Examen2Parcial
{
    public partial class FrmMenu_Syncfusion : Syncfusion.Windows.Forms.Office2007Form
    {
        public FrmMenu_Syncfusion()
        {
            InitializeComponent();
        }

        FrmProducto frmProducto = null;
        FrmPedido frmPedido = null;
        private void RegistroProductoToolStripButton_Click(object sender, EventArgs e)
        {
            if (frmProducto == null)
            {
                frmProducto = new FrmProducto();
                frmProducto.MdiParent = this;
                frmProducto.FormClosed += FrmProducto_FormClosed;
                frmProducto.Show();
            }
            else
            {
                frmProducto.Activate();
            }
        }

        private void FrmProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmProducto = null;
        }

        private void RegistroPedidosToolStripButton_Click(object sender, EventArgs e)
        {
            if (frmProducto == null)
            {
                frmPedido = new FrmPedido();
                frmPedido.MdiParent = this;
                frmPedido.FormClosed += FrmPedido_FormClosed;
                frmPedido.Show();
            }
            else
            {
                frmProducto.Activate();
            }
        }

        private void FrmPedido_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmPedido= null;
        }
    }
}
