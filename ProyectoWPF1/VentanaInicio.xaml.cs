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

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaEventos();
            v.Show();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaComandos();
            v.Show();
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            Window v = new VentanaAsincrono();
            v.Show();
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            //Antes de inicializar la ventana (antes del NEW)
            System.Globalization.CultureInfo ci = null;

            if (radioButton1.IsChecked.Value)
                ci = new System.Globalization.CultureInfo("es-ES");
            else
                if (radioButton2.IsChecked.Value)
                    ci = new System.Globalization.CultureInfo("es-AR");
                else
                    if (radioButton3.IsChecked.Value)
                        ci = new System.Globalization.CultureInfo("es");
                    else
                        if (radioButton4.IsChecked.Value)
                            ci = new System.Globalization.CultureInfo("en-US");
                        else
                            if (radioButton5.IsChecked.Value)
                                ci = new System.Globalization.CultureInfo("en");
                            else
                                if (radioButton6.IsChecked.Value)
                                    ci = new System.Globalization.CultureInfo("ar-AE");

            string cad = "Antes\nCultura: " + System.Threading.Thread.CurrentThread.CurrentCulture +
               "\nCulturaUI: " + System.Threading.Thread.CurrentThread.CurrentUICulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                System.Threading.Thread.CurrentThread.CurrentCulture;

            if (ci != null)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            }

            cad += "\n\nDespués\nCultura: " + System.Threading.Thread.CurrentThread.CurrentCulture +
               "\nCulturaUI: " + System.Threading.Thread.CurrentThread.CurrentUICulture;

            MessageBox.Show(cad, "Cambio Cultura");
            //System.Diagnostics.Debug.WriteLine(cad);

            Window v = new VentanaLocalizable();
            v.Show();
        }
    }
}
