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
    /// Lógica de interacción para VentanaContenedores.xaml
    /// </summary>
    public partial class VentanaContenedores : Window
    {
        public VentanaContenedores()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (!checkBox2.IsChecked.Value)
                GridUniforme.Children.Clear();

            int cant = int.Parse(txtNumBotones.Text);

            int numhijos = GridUniforme.Children.Count;
            for (int x = numhijos + 1; x <= cant + numhijos; x++)
            {
                //Button b = new Button();
                //b.Content = "Botón " + x;
                //b.Width = 75;
                //b.Height = 23;
                //GridUniforme.Children.Add(b);
                //GridUniforme.Children.Add(new Button()
                //{
                //    Width = 75,
                //    Height = 23,
                //    Content = "Botón " + x
                //});
                Button b = new Button() {
                    Width = 75,
                    Height = 23,
                    Content = "Botón " + x
                };
                b.Click += botonNuevo_Click;
                GridUniforme.Children.Add(b);
            }

        }

        void botonNuevo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hola desde " + ((Button)sender).Content);
        }
    }
}
