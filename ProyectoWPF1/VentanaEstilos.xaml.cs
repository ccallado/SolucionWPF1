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
    /// Lógica de interacción para VentanaEstilos.xaml
    /// </summary>
    public partial class VentanaEstilos : Window
    {
        public VentanaEstilos()
        {
            InitializeComponent();

            //Creación de un estilo por código dependiente de otro
            Style estiloSuperior = (Style)this.TryFindResource("EstiloBoton1Diccionario");
            Style estilo;
            if (estiloSuperior != null)
            {
                //Creación de un estilo por código
                estilo = new Style(typeof(Button), estiloSuperior );
                //igual que la anterior (otra forma)
                // Style estilo = new Style(button1.GetType());
            }
            else 
            {
                //Creación de un estilo por código
                estilo = new Style(typeof(Button));
                //igual que la anterior (otra forma)
                // Style estilo = new Style(button1.GetType());
            }
            //propiedad de dependencia
            //propiedad con propiedades y metodos
            //graficamente son practicamente todas
            //suelen terminar todas en Property
            Setter s1 = new Setter(Button.FontWeightProperty, FontWeights.Bold);
            estilo.Setters.Add(s1);
            //Terminan en Event
            EventSetter s2 = new EventSetter(Button.ClickEvent, new RoutedEventHandler(btnEstilos_Click));
            estilo.Setters.Add(s2);
            button7.Style = estilo;
        }

        private void btnEstilos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click en botón " + ((Button)sender).Content);
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
