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
    /// Lógica de interacción para VentanaInicio.xaml
    /// </summary>
    public partial class VentanaInicio : Window
    {
        public VentanaInicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow v = new MainWindow();
            Window v = new MainWindow();
            v.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaContenedores();
            v.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaHeadered_ItemsControls();
            v.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaRecursos();
            v.Show();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaEstilos();
            v.Show();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaPlantillas();
            v.Show();
        }
    }
}
