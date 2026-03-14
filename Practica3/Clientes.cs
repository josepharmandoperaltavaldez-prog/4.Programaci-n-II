using Practica3.Modelo;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Practica3
{
    public partial class Clientes : Form
    {
        // Instanciamos conexión ORM
        Practica1Entities db = new Practica1Entities();

        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                // Select para traer columnas
                var listaClientes = db.Clientes.Select(c => new
                {
                    c.ClienteID,
                    c.NombreCompleto,
                    c.Direccion,
                    c.Telefono
                }).ToList();

                dgvClientes.DataSource = listaClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message, "Error ORM");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor llena el ID y el Nombre.");
                return;
            }

            try
            {

                Practica3.Modelo.Clientes nuevoCli = new Practica3.Modelo.Clientes();

                nuevoCli.ClienteID = int.Parse(txtID.Text);
                nuevoCli.NombreCompleto = txtNombre.Text;

                db.Clientes.Add(nuevoCli);
                db.SaveChanges();

                MessageBox.Show("¡Cliente agregado con ORM!");
                txtID.Clear();
                txtNombre.Clear();
                btnCargar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message, "Error ORM");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDActualizar.Text))
            {
                MessageBox.Show("Ingresa el ID del cliente a actualizar.");
                return;
            }

            try
            {
                int idBuscar = int.Parse(txtIDActualizar.Text);
                var cliExistente = db.Clientes.Find(idBuscar);

                if (cliExistente != null)
                {
                    cliExistente.NombreCompleto = txtNombreActualizado.Text;

                    db.SaveChanges();

                    MessageBox.Show("¡Cliente actualizado con ORM!");
                    txtIDActualizar.Clear();
                    txtNombreActualizado.Clear();
                    btnCargar.PerformClick();
                }
                else
                {
                    MessageBox.Show("No se encontró el ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error ORM");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDEliminar.Text))
            {
                MessageBox.Show("Ingresa el ID a eliminar.");
                return;
            }

            try
            {
                int idEliminar = int.Parse(txtIDEliminar.Text);
                var cliEliminar = db.Clientes.Find(idEliminar);

                if (cliEliminar != null)
                {
                    db.Clientes.Remove(cliEliminar);
                    db.SaveChanges();

                    MessageBox.Show("¡Cliente eliminado con ORM!");
                    txtIDEliminar.Clear();
                    btnCargar.PerformClick();
                }
                else
                {
                    MessageBox.Show("No se encontró el ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No puedes borrar este cliente porque tiene facturas. Error: " + ex.Message, "Alerta");
            }
        }
    }
}