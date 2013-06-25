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

        private void eventoTecla(object sender, KeyEventArgs e)
        {
            if (((FrameworkElement)e.Source).Name == "listBox1" &&
                e.Key==Key.Delete )
            {
                listBox1.Items.Clear();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            label1.Content = "PMM - " + ((FrameworkElement )sender).Name + ": " +
                Mouse.GetPosition(stackPanel1) + " (" +
                ((FrameworkElement)e.Source).Name + ")";
        }

        private void stackPanel1_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            label2.Content = "PMM - " + ((FrameworkElement)sender).Name + ": " +
                Mouse.GetPosition(stackPanel1 ) + " (" +
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

        private void PKD(object sender, KeyEventArgs e)
        {
            //PreviewKeyDown
            listBox1.Items.Add((listBox1.Items.Count+1) +
                "PKD en " + ((FrameworkElement)sender).Name + 
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }

        private void KD(object sender, KeyEventArgs e)
        {
            //KeyDown
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                "KD en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }

        private void PKU(object sender, KeyEventArgs e)
        {
            //PreviewKeyUp
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                "PKU en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }

        private void KU(object sender, KeyEventArgs e)
        {
            //KeyUP
            listBox1.Items.Add((listBox1.Items.Count + 1) +
                "KU en " + ((FrameworkElement)sender).Name +
                " => Origen: " +
                ((FrameworkElement)e.Source).Name);
        }
    }
}
