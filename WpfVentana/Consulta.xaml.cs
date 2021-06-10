using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Modelo;
using Repository;
using System.Data.Entity;
using System.Data;

namespace WpfAlu
{
    /// <summary>
    /// Lógica de interacción para Consulta.xaml
    /// </summary>
    public partial class Consulta : Window
    {
        public Consulta()
        {
            InitializeComponent();
        }

        //Creando una referencia con la tabla total ventas
        TotalVenta _en = new TotalVenta();
        VentasBL _bl = new VentasBL();

        //IRepository Repository = new Modelo.Repository();
        IUnitOfWork Repository = new Modelo.RepositoryUoW();

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtmostrar.Text == ""))
            {                
                _en.Nombre = txtmostrar.Text;
                dgdatos.ItemsSource = _bl.MostrarVentasPorNombre(_en);
            }
        }

        private void dgdatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _en = dgdatos.SelectedItem as TotalVenta;
            if (_en != null)
            {
                txtnombre.Text = _en.Nombre;
                txtventas.Text = Convert.ToString(_en.Total_ventas);
                txtestado.Text = _en.Estado;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtnombre.Text != "" && txtventas.Text != "" && txtestado.Text != "")
            {
                MessageBoxResult r = MessageBox.Show("Estas seguro eliminar este registro?", "Alerta!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (r == MessageBoxResult.OK)
                {
                    try
                    {
                        Repository.Delete(new TotalVenta { id = _en.id });
                        dgdatos.Items.Refresh();
                        dgdatos.ItemsSource = _bl.MostrarVentas();
                        Repository.Save();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al utilizar el metodo de eliminar", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if (r == MessageBoxResult.Cancel)
                {
                }
            } else
            {
                MessageBox.Show("No puede utilizar este botón, primero seleccione un registro", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow _ver = new MainWindow();
            this.Close();
            _ver.ShowDialog();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtnombre.Text != "" && txtventas.Text != "" && txtestado.Text != "")
            {
                try
                {
                    var UpdateID = Repository.FindEntity<TotalVenta>(p => p.id == _en.id);
                    UpdateID.Nombre = txtnombre.Text;
                    UpdateID.Total_ventas = Convert.ToInt64(txtventas.Text);
                    UpdateID.Estado = txtestado.Text;
                    Repository.Update(UpdateID);
                    Repository.Save();

                    MessageBox.Show("El registro se modificó correctamente...", "Éxito!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    dgdatos.Items.Refresh();
                    dgdatos.ItemsSource = _bl.MostrarVentas();
                }
                catch(Exception)
                {
                    MessageBox.Show("No se pudo modificar...", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                                
            }
            else
            {
                MessageBox.Show("Seleccione un registro...", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
