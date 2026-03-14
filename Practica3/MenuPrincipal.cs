using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica3
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categorias frmCat = new Categorias();

            frmCat.MdiParent = this;

            frmCat.Show();

        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Proveedores frmCat = new Proveedores();

            frmCat.MdiParent = this;

            frmCat.Show();

        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clientes frmCat = new Clientes();

            frmCat.MdiParent = this;

            frmCat.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productos frmCat = new Productos();

            frmCat.MdiParent = this;    

            frmCat.Show();
        }
    }
}
