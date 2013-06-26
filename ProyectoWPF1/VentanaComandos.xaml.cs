using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoWPF1
{
    /// <summary>
    /// Lógica de interacción para VentanaComandos.xaml
    /// </summary>
    public partial class VentanaComandos : Window
    {
        public VentanaComandos()
        {
            InitializeComponent();
        }

        private void CerrarFormulario(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Esto es para que NO cierre la ventana al pulsar Esc,
            //ya que es la tecla asociada al comando Stop.
            if (e.Key == Key.Escape)
                e.Handled = true;
        }

        private void PuedeCerrarFormulario(object sender, CanExecuteRoutedEventArgs e)
        {
            if (textBox1 != null && textBox2 != null)
            {
                //Poner siempre las dos el true y el false
                if (textBox1.Text != "" || textBox2.Text != "")
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
        }

        private void AbrirArchivo(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox caja = e.Source as TextBox;
            if (caja != null)
            {
                string origen = caja.Name;
                MessageBox.Show("Aquí se abriría un cuadro de diálogo para el archivo " + caja.Text ,
                    "Origen: " + origen);
            }
        }

        private void PuedeAbrirArchivo(object sender, CanExecuteRoutedEventArgs e)
        {
            TextBox caja = e.Source as TextBox;
            if (caja != null)
            {
                if (e.Parameter != null && e.Parameter.ToString() != "")
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            else 
                e.CanExecute = false;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //En este caso fuerzo la ejecución de un comando, sin saltarse el CanExecute
            ApplicationCommands.Open.Execute(textBox1.Text , textBox1);
        }
    }
}
