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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modelo;
using Repository;
using System.Data.Entity;

namespace WpfAlu
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        IRepository Repository = new Modelo.Repository();
        //IUnitOfWork Repository = new Modelo.RepositoryUoW();

        private void Limpiar()
        {
            txtnombre.Text = string.Empty;
            txtventas.Text = string.Empty;
            txtestado.Text = string.Empty;
            txtnombre.Focus();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtnombre.Text == "" || txtventas.Text == "" || txtestado.Text == "")
                {
                    MessageBox.Show("Hay espacios en blanco", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        Repository.Create(new TotalVenta
                        {
                            Nombre = txtnombre.Text,
                            Total_ventas = Convert.ToInt64(txtventas.Text),
                            Estado = txtestado.Text
                        });

                        //Repository.Save();

                        MessageBox.Show("Los datos se guardaron corretamente...", "Exito!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        Limpiar();
                    }catch(Exception)
                    {
                        MessageBox.Show("Los datos no se guardaron", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hubo un error...", "Error!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            Consulta _ver = new Consulta();
            this.Close();
            _ver.ShowDialog();
        }
    }
}
