using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoWPF1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ProyectoWPF1.Clase1 c1 = new Clase1(5);
            ProyectoWPF1bis.Clase1 c2 = new ProyectoWPF1bis.Clase1();
            System.Clase1deJavier cc1 = new Clase1deJavier();
            //c1.Dato2;
            //c1.Dato4;
            ProyectoWPF1bis.Clase2 c4 = new ProyectoWPF1bis.Clase2(1, 2);
        }

        void button4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hola");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hola, mundo...");
        }

        //Manejo de colecciones
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //comenzamos con el arraylist
            System.Collections.ArrayList lista;

            lista = new System.Collections.ArrayList();

            //voy a cargar a mano los datos del arraylist
            lista.Add("Lunes");
            lista.Add("Martes");
            lista.Add("Miercoles");
            lista.Add("Jueves");
            lista.Add("Viernes");
            lista.Add("Sabado");
            lista.Add("Domingo");

            //La inicializo cadena vacia para luego poder concatenar
            string cad = "Objetos:\n";

            foreach (object o in lista)
            {
                //La conversión ToString() no es necesaria porque lo hace .NET automáticamente
                cad += " - " + o.ToString() + "\n";
            }

            cad += "\n\nCadenas:\n";
            foreach (string o in lista)
            {
                cad += " - " + o + "\n";
            }

            MessageBox.Show(cad, "Colección normal");

            //Diccionarios
            System.Collections.Specialized.ListDictionary dic1;
            dic1 = new System.Collections.Specialized.ListDictionary();

            for (int pos = 1; pos <= lista.Count ; pos++)
                dic1.Add(pos, lista[pos-1]);

            cad = "Claves:\n";
            foreach (int k in dic1.Keys)
                cad += " - " + k + "\n";

            cad += "Valores:\n";
            foreach (string v in dic1.Values )
                cad += " - " + v + "\n";

            cad += "Claves y Valores:\n";
            foreach (int k in dic1.Keys)
                cad += " - " + k + ": " + dic1[k] + "\n";

            MessageBox.Show(cad, "Diccionario normal");

            //No se garantiza que los datos salgan ordenados al usar diccionarios.

            //Genéricos..

            System.Collections.Generic.List<string> listaCadenas;
            listaCadenas = new List<string>(); //No pongo el namespace por que está el using arriba
            listaCadenas.Add("Lunes");
            listaCadenas.Add("Martes");
            listaCadenas.Add("Miercoles");
            listaCadenas.Add("Jueves");
            listaCadenas.Add("Viernes");
            listaCadenas.Add("Sabado");
            listaCadenas.Add("Domingo");

            cad = "Lista Cadenas:\n";
            foreach (string o in listaCadenas )
                cad += " - " + o + "\n";

            MessageBox.Show(cad, "Lista genérica");

            //Diccionario
            System.Collections.Generic.Dictionary<int, string> dic2;
            dic2 = new Dictionary<int, string>();

            for (int pos = 1; pos <= listaCadenas.Count; pos++)
                dic2.Add(pos, listaCadenas[pos - 1]);

            cad = "Claves:\n";
            foreach (int k in dic2.Keys)
                cad += " - " + k + "\n";

            cad += "Valores:\n";
            foreach (string v in dic2.Values)
                cad += " - " + v + "\n";

            cad += "Claves y Valores:\n";
            foreach (int k in dic2.Keys)
                cad += " - " + k + ": " + dic2[k] + "\n";

            MessageBox.Show(cad, "Diccionario genérico");


            //Varios con colecciones
            //if (textBox1.Text.Trim() != "")
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                //Si es un numero lo buscamos por la clave
                int k;

                if (int.TryParse(textBox1.Text, out k))
                {
                    object o1 = dic1[k];
                    cad = "Valor No genérico: " + (o1 != null ? o1.ToString() : "No existe");
                    cad += "\nValor genérico: ";
                    if (dic2.ContainsKey(k))
                        cad += dic2[k];
                    else
                        cad += "No existe";

                    MessageBox.Show(cad, "Valor clave " + k);
                }
                else
                {
                    cad = "\nDiccionario NO Genérico: ";
                    object o = null;
                    foreach (object aux in dic1.Values)
                    {
                        if (aux.ToString() == textBox1.Text)
                        {
                            o = aux;
                            break;
                        }
                    }
                    if (o != null)
                        cad += "Existe el valor";
                    else
                        cad += "No existe el valor";

                    cad += "\nDiccionario Genérico: ";
                    if (dic2.ContainsValue(textBox1.Text))
                        cad += "Existe el valor";
                    else
                        cad += "No existe el valor";
                }
                MessageBox.Show(cad, "Valor: " + textBox1.Text);
            }
            else
                MessageBox.Show("Caja vacía");

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox1.IsChecked.HasValue &&
                checkBox1.IsChecked.Value)
            {
                if (CajaBoton.Text.Trim() != "")
                    MessageBox.Show("Hola, " + CajaBoton.Text);
                else
                    MessageBox.Show("No hay dato");
            }
        }

        private void CajaBoton_TextChanged(object sender, TextChangedEventArgs e)
        {
            BloqueTexto.Text = CajaBoton.Text;
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            //button4.Click += new RoutedEventHandler(button4_Click);
            //button4.Click += button4_Click;
            button4.Click +=new RoutedEventHandler(button4_Click);
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            button4.Click -= new RoutedEventHandler(button4_Click);
        }

    }
}
