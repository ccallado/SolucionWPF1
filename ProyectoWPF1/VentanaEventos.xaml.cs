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
    /// Lógica de interacción para VentanaEventos.xaml
    /// </summary>
    public partial class VentanaEventos : Window
    {
        public VentanaEventos()
        {
            InitializeComponent();
        }

        private void ClickCalculadora(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            if (b != null)
            {
                if (b.Content.ToString() != "C")
                    CajaCalculadora.Text += b.Content.ToString();
                else
                    CajaCalculadora.Text = "";
            }
            string cad = "Sender: " + sender.ToString();
            cad += "\nSource: " + b;
            MessageBox.Show(cad);
        }
    }
}
