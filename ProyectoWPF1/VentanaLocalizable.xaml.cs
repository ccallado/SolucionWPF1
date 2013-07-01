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
    /// Lógica de interacción para VentanaLocalizable.xaml
    /// </summary>
    public partial class VentanaLocalizable : Window
    {
        public VentanaLocalizable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Single moneda;
            if (Single.TryParse(textBox1.Text, out moneda))
            {
                //Con la "c" le decimos currency y nos pone la moneda de la localización
                label1.Content = moneda.ToString("c");
                label3.Content = "España: " + moneda.ToString("c", new System.Globalization.CultureInfo("es-ES"));
                label3.Content += "\nArgentina: " + moneda.ToString("c", new System.Globalization.CultureInfo("es-AR"));
                label3.Content += "\nInglaterra: " + moneda.ToString("c", new System.Globalization.CultureInfo("en-GB"));
                label3.Content += "\nAmericano: " + moneda.ToString("c", new System.Globalization.CultureInfo("en-US"));
                label3.Content += "\nEmiratos Arabes Unidos: " + moneda.ToString("c", new System.Globalization.CultureInfo("ar-AE"));
                label3.Content += "\nJapones: " + moneda.ToString("c", new System.Globalization.CultureInfo("ja-JP"));
                label3.Content += "\nIndia: " + moneda.ToString("c", new System.Globalization.CultureInfo("ta-IN"));
            }
            else
                label1.Content = "Incorrecto";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DateTime fecha;
            if (DateTime.TryParse(textBox2.Text, out fecha))
            {
                //Con la "c" le decimos currency y nos pone la moneda de la localización
                label2.Content = fecha.ToLongDateString();
                label3.Content = "España: " + fecha.ToString(new System.Globalization.CultureInfo("es-ES"));
                label3.Content += "\nArgentina: " + fecha.ToString(new System.Globalization.CultureInfo("es-AR"));
                label3.Content += "\nInglaterra: " + fecha.ToString(new System.Globalization.CultureInfo("en-GB"));
                label3.Content += "\nAmericano: " + fecha.ToString(new System.Globalization.CultureInfo("en-US"));
                label3.Content += "\nEmiratos Arabes Unidos: " + fecha.ToString(new System.Globalization.CultureInfo("ar-AE"));
                label3.Content += "\nJapones: " + fecha.ToString(new System.Globalization.CultureInfo("ja-JP"));
                label3.Content += "\nIndia: " + fecha.ToString(new System.Globalization.CultureInfo("ta-IN"));

                label3.Content += "\n\nEspaña: " + fecha.ToString("D", new System.Globalization.CultureInfo("es-ES"));
                label3.Content += "\nArgentina: " + fecha.ToString("D", new System.Globalization.CultureInfo("es-AR"));
                label3.Content += "\nInglaterra: " + fecha.ToString("D", new System.Globalization.CultureInfo("en-GB"));
                label3.Content += "\nAmericano: " + fecha.ToString("D", new System.Globalization.CultureInfo("en-US"));
                label3.Content += "\nEmiratos Arabes Unidos: " + fecha.ToString("D", new System.Globalization.CultureInfo("ar-AE"));
                label3.Content += "\nJapones: " + fecha.ToString("D", new System.Globalization.CultureInfo("ja-JP"));
                label3.Content += "\nIndia: " + fecha.ToString("D", new System.Globalization.CultureInfo("ta-IN"));
            }
            else
                label2.Content = "Incorrecto";
        }
    }
}
