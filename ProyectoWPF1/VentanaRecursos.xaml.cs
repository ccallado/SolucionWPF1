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
    /// Lógica de interacción para VentanaRecursos.xaml
    /// </summary>
    public partial class VentanaRecursos : Window
    {
        public VentanaRecursos()
        {
            InitializeComponent();
        }

        private void CambiaColor(object sender, RoutedEventArgs e)
        {
            string cad = "";
            MenuItem mi = sender as MenuItem;
            cad = "Elemento: " + mi.Header;
            Color c;
            switch (mi.Header.ToString()[0])
            {
                case 'R':
                    c= Colors.Red;
                    break;
                case 'V':
                    c = Colors.Green;
                    break;
                case 'A':
                    c = Colors.Blue;
                    break;
                default:
                    c = Colors.Black;
                    break;
            }
            
            ContextMenu ctx = mi.Parent as ContextMenu;
            cad += "\nContexMenu: " + ctx.ToString();
            //objeto sobre el cual se ha abierto el menú contextual
            Rectangle r = ctx.PlacementTarget as Rectangle ;
            if (r != null)
            {
                cad += "\nRectángulo: " + r.Name;
                r.Fill = new SolidColorBrush(c);
            }
            else
            {
                Button btn = ctx.PlacementTarget as Button;
                //Asumo que ES un botón...
                //Tres maneras de llegar a un recurso
                SolidColorBrush  b;
                //recursos de la ventana "this"
                //b = this.Resources["BrochaTexto"] as SolidColorBrush;
                //Cualquier punto busca del control para arriba Grid, Ventana, Aplicacion, Diccionario
                //Puede dar excepcion.
                b = btn.FindResource("BrochaTexto") as SolidColorBrush;
                //No da excepción devuelve "null"
                //b = btn.TryFindResource("BrochaTexto") as SolidColorBrush;
                b.Color = c;
            }
            //MessageBox.Show(cad);

        }
    }
}
