using Practica3.Modelo;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Practica3
{
    public partial class Productos : Form
    {
        // Instanciamos tu conexión ORM
        Practica1Entities db = new Practica1Entities();

        public Productos()
        {
            InitializeComponent();
        }

        // --- AL ABRIR LA VENTANA (LLENAR EL COMBOBOX) ---
        private void Productos_Load(object sender, EventArgs e)
        {
            try
            {
                // Traemos todas las categorías de la base de datos
                var listaCategorias = db.Categorías.ToList();

                // Le damos los datos al ComboBox (Asegúrate de que se llame cmbCategoria en tu diseño)
                cmbCategoria.DataSource = listaCategorias;
                cmbCategoria.DisplayMember = "NombreCategoria"; // Lo que el usuario VE
                cmbCategoria.ValueMember = "CategoriaID";       // Lo que se GUARDA (El ID oculto)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías en la lista: " + ex.Message, "Error ORM");
            }
        }

        // --- BOTÓN CARGAR (MOSTRAR PRODUCTOS EN LA TABLA) ---
        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                // Select limpio para evitar errores con las llaves foráneas
                var listaProductos = db.Productos.Select(p => new
                {
                    p.ProductoID,
                    p.NombreProducto,
                    // Si tienes precio o stock en tu BD, quítales las barras "//":
                    // p.Precio,
                    // p.Stock,
                    p.CategoriaID
                }).ToList();

                dgvProductos.DataSource = listaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message, "Error ORM");
            }
        }

        // --- BOTÓN AGREGAR (INSERTAR) ---
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor llena el ID y el Nombre del producto.");
                return;
            }

            try
            {
                Practica3.Modelo.Productos nuevoProd = new Practica3.Modelo.Productos();

                nuevoProd.ProductoID = int.Parse(txtID.Text);
                nuevoProd.NombreProducto = txtNombre.Text;

                // ¡Magia del ComboBox! Tomamos el ID de la categoría que el usuario seleccionó
                nuevoProd.CategoriaID = (int)cmbCategoria.SelectedValue;

                // Si agregas más campos (precio, stock), hazlo aquí:
                // nuevoProd.Precio = decimal.Parse(txtPrecio.Text);

                db.Productos.Add(nuevoProd);
                db.SaveChanges();

                MessageBox.Show("¡Producto agregado con ORM!");
                txtID.Clear();
                txtNombre.Clear();
                btnCargar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message, "Error ORM");
            }
        }

        // --- BOTÓN ACTUALIZAR ---
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDActualizar.Text))
            {
                MessageBox.Show("Ingresa el ID del producto a actualizar.");
                return;
            }

            try
            {
                int idBuscar = int.Parse(txtIDActualizar.Text);
                var prodExistente = db.Productos.Find(idBuscar);

                if (prodExistente != null)
                {
                    prodExistente.NombreProducto = txtNombreActualizado.Text;

                    // Actualizamos la categoría tomando la nueva selección del ComboBox
                    prodExistente.CategoriaID = (int)cmbCategoria.SelectedValue;

                    db.SaveChanges();

                    MessageBox.Show("¡Producto actualizado con ORM!");
                    txtIDActualizar.Clear();
                    txtNombreActualizado.Clear();
                    btnCargar.PerformClick();
                }
                else
                {
                    MessageBox.Show("No se encontró el ID del producto.");
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
                MessageBox.Show("Ingresa el ID del producto a eliminar.");
                return;
            }

            try
            {
                int idEliminar = int.Parse(txtIDEliminar.Text);
                var prodEliminar = db.Productos.Find(idEliminar);

                if (prodEliminar != null)
                {
                    db.Productos.Remove(prodEliminar);
                    db.SaveChanges();

                    MessageBox.Show("¡Producto eliminado con ORM!");
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
                MessageBox.Show("Error al eliminar el producto: " + ex.Message, "Error ORM");
            }
        }
    }
}