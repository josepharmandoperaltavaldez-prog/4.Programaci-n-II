using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Practica3.Modelo;

namespace Practica3
{
    public partial class Categorias : Form
    {
        // Instanciamos conexión ORM
        Practica1Entities db = new Practica1Entities();

        public Categorias()
        {
            InitializeComponent();
        }


        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                // Select para elegir Columnas específicas
                var listaCategorias = db.Categorías.Select(c => new
                {
                    c.CategoriaID,
                    c.NombreCategoria
                }).ToList();

                dgvCategorias.DataSource = listaCategorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error ORM");
            }

        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor, llena el ID y el Nombre de la categoría.");
                return;
            }

            try
            {
                // la ruta completa a la tabla Categorías del modelo
                Practica3.Modelo.Categorías nuevaCat = new Practica3.Modelo.Categorías();
                nuevaCat.CategoriaID = int.Parse(txtID.Text);
                nuevaCat.NombreCategoria = txtNombre.Text;

                db.Categorías.Add(nuevaCat);
                db.SaveChanges();

                MessageBox.Show("¡Categoría agregada con ORM!");
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
            if (string.IsNullOrEmpty(txtIDActualizar.Text) || string.IsNullOrEmpty(txtNombreActualizado.Text))
            {
                MessageBox.Show("Ingresa el ID a buscar y el nuevo nombre.");
                return;
            }

            try
            {
                int idBuscar = int.Parse(txtIDActualizar.Text);
                var catExistente = db.Categorías.Find(idBuscar);

                if (catExistente != null)
                {
                    catExistente.NombreCategoria = txtNombreActualizado.Text;
                    db.SaveChanges();

                    MessageBox.Show("¡Categoría actualizada con ORM!");
                    txtIDActualizar.Clear();
                    txtNombreActualizado.Clear();
                    btnCargar.PerformClick();
                }
                else
                {
                    MessageBox.Show("No se encontró ese ID de categoría.");
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
                MessageBox.Show("Ingresa el ID de la categoría que quieres borrar.");
                return;
            }

            try
            {
                int idEliminar = int.Parse(txtIDEliminar.Text);
                var catEliminar = db.Categorías.Find(idEliminar);

                if (catEliminar != null)
                {
                    db.Categorías.Remove(catEliminar);
                    db.SaveChanges();

                    MessageBox.Show("¡Categoría eliminada con ORM!");
                    txtIDEliminar.Clear();
                    btnCargar.PerformClick();
                }
                else
                {
                    MessageBox.Show("No se encontró ese ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No puedes borrar esta categoría porque ya tiene productos asignados.\nError: " + ex.Message, "Alerta");
            }
        }

        private void Categorias_Load(object sender, EventArgs e)
        {

        }
    }
}
