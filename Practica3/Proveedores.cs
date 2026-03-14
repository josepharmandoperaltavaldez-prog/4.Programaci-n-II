using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Practica3.Modelo; 

namespace Practica3
{
    public partial class Proveedores : Form
    {
        // Instanciamos conexión ORM
        Practica1Entities db = new Practica1Entities();

        public Proveedores()
        {
            InitializeComponent();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                // select para traer solo las columnas
                var listaProveedores = db.Proveedores.Select(p => new
                {
                    p.ProveedorID,
                    p.NombreProveedor,
                    p.Telefono,
                    p.CorreoElectronico
                }).ToList();

                dgvProveedores.DataSource = listaProveedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los proveedores: " + ex.Message, "Error ORM");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor llena el ID y el Nombre del proveedor.");
                return;
            }

            try
            {
                Practica3.Modelo.Proveedores nuevoProv = new Practica3.Modelo.Proveedores();

                nuevoProv.ProveedorID = int.Parse(txtID.Text);
                nuevoProv.NombreProveedor = txtNombre.Text;

                db.Proveedores.Add(nuevoProv);
                db.SaveChanges();

                MessageBox.Show("¡Proveedor agregado con ORM!");
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
                MessageBox.Show("Ingresa el ID del proveedor a actualizar.");
                return;
            }

            try
            {
                int idBuscar = int.Parse(txtIDActualizar.Text);
                var provExistente = db.Proveedores.Find(idBuscar);

                if (provExistente != null)
                {
                    provExistente.NombreProveedor = txtNombreActualizado.Text;

                    db.SaveChanges();

                    MessageBox.Show("¡Proveedor actualizado con ORM!");
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

        // --- BOTÓN ELIMINAR ---
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
                var provEliminar = db.Proveedores.Find(idEliminar);

                if (provEliminar != null)
                {
                    db.Proveedores.Remove(provEliminar);
                    db.SaveChanges();

                    MessageBox.Show("¡Proveedor eliminado con ORM!");
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
                MessageBox.Show("No puedes borrar este proveedor porque nos surte productos.\nError: " + ex.Message, "Alerta de Seguridad");
            }
        }
    }
}