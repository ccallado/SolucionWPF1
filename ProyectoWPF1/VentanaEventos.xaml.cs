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

                CajaCalculadora.Focus();
                CajaCalculadora.SelectionStart = CajaCalculadora.Text.Length;
            }
            //string cad = "Sender: " + sender.ToString();
            //cad += "\nSource: " + b;
            //MessageBox.Show(cad);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            label1.Content = "PMM - " + ((FrameworkElement)sender).Name + ": " +
                Mouse.GetPosition(stackPanel1) + " (" +
                ((FrameworkElement)e.Source).Name + ")";
        }

        private void stackPanel1_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            label2.Content = "PMM - " + ((FrameworkElement)sender).Name + ": " +
                Mouse.GetPosition(stackPanel1) + " (" +
                ((FrameworkElement)e.Source).Name + ")";
            // Si en tunneling pongo Handled = true el evento no sigue
            // ni en ventanas siguientes
            // ni en el evento boobling MouseMove
            if (e.RightButton == MouseButtonState.Pressed)
                e.Handled = true;
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Content = "MM - " + ((FrameworkElement)sender).Name + ": " +
                Mouse.GetPosition(stackPanel1) + " (" +
                ((FrameworkElement)e.Source).Name + ")";
        }

        private void stackPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Content = "MM - " + ((FrameworkElement)sender).Name + ": " +
                Mouse.GetPosition(stackPanel1) + " (" +
                ((FrameworkElement)e.Source).Name + ")";
        }

        private void eventoTecla(object sender, KeyEventArgs e)
        {
            if (((FrameworkElement)e.Source).Name == "listBox1" &&
                e.Key == Key.Delete)
            {
                e.Handled = true;
                if (e.RoutedEvent.Name == "PreviewKeyDown")
                    listBox1.Items.Clear();
            }
        }

        private void PKD(object sender, KeyEventArgs e)
        {
            //PreviewKeyDown
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                " PKD en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }

        private void KD(object sender, KeyEventArgs e)
        {
            //KeyDown
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                " KD en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }

        private void PKU(object sender, KeyEventArgs e)
        {
            //PreviewKeyUp
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                " PKU en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }

        private void KU(object sender, KeyEventArgs e)
        {
            //KeyUP
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                " KU en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }

        //No vamos a permitir que se imprima el espacio en la caja de texto2
        //No podemos usar el evento KeyDown algunas pulsaciones no se recogen en este
        //evento. 
        //Se debe utilizar PreviewKeyDown
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //Por aquí NUNCA entrará porque el KeyDown NO captura el espacio 
            //(entre otras teclas)
            //KeyDown
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                " KD en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
            //if (e.Key == Key.Space)
            //{
            //    e.Handled = true;
            //    listBox1.Items.Add("Detenido");
            //}
            //Dtengo si es un dígito del teclado numérico.
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = true;
                //la tecla no se pasa a la caja de texto
                listBox1.Items.Add((listBox1.Items.Count + 1) + " Detenido");
            }
        }

        private void textBox2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //PreviewKeyDown
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                " PKD en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                //la tecla no se pasa a la caja de texto
                listBox1.Items.Add((listBox1.Items.Count + 1) + " Detenido");
            }
        }

        private void CambioCaja(object sender, TextChangedEventArgs e)
        {
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                " Cambio: " + ((TextBox)e.Source).Text);
        }

        bool tControl, tShift, tAlt;

        private void chequeoCaja(object sender, KeyEventArgs e)
        {
            //Si las variables estubiesen definidas aquí dentro habría que inicializarlas.
            //Al estar fuera ya vienen inicializadas a false

            //Para PreviewKeyDown y PreviewKeyUp

            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                tControl = e.IsDown;
            label5.Content = (tControl ? "Ctrl" : "");

            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                tShift = e.IsDown;
            if (tShift)
                label5.Content += (label5.Content.ToString() != "" ? "+" : "") + "Shift";

            if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
                tAlt = e.IsDown;

            if (tAlt)
                label5.Content += (label5.Content.ToString() != "" ? "+" : "") + "Alt";

        }
    }
}
