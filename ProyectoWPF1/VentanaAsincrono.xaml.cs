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
    /// Lógica de interacción para VentanaAsincrono.xaml
    /// </summary>
    public partial class VentanaAsincrono : Window
    {
        //creación de procesos paralelos
        //estructura de la delegada
        delegate void delegadaActualizaPB(int valor);
        //Variable
        delegadaActualizaPB delActualizaPB;

        //Segunda delegada con dos parametros
        //estructura de la delegada
        delegate void delegadaActualizaLBL(int i, int cont);
        //Variable
        delegadaActualizaLBL delActualizaLBL;

        //Delegada botones
        delegate void delegadaBotones(bool habilitar);
        //variable
        delegadaBotones delBotones;

        public VentanaAsincrono()
        {
            InitializeComponent();
        }

        //Función para el cálculo de números primos
        bool EsPrimo(int x)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException("x", x, "No se puede calcular sobre valores negativos");

            if (x <= 3)
                return true;
            else
            {
                for (int i = 2; i < x; i++)
                    if (x % i == 0)
                        return false;
            }

            return true;
        }

        //Función para habilitar y deshabilitar los botones
        void Botones(bool habilitar)
        {
            button1.IsEnabled = habilitar;
            button2.IsEnabled = habilitar;
            button3.IsEnabled = !habilitar;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int hasta;

            if (int.TryParse(textBox1.Text, out hasta))
            {
                Botones(false);
                button3.IsEnabled = false;
                progressBar1.Maximum = hasta;
                System.Windows.Forms.Application.DoEvents();
                BuscarPrimosSincrono(hasta);
                Botones(true);
            }
        }

        void BuscarPrimosSincrono(int hasta)
        {
            int cont = 0;

            for (int i = 1; i <= hasta; i++)
            {
                if (EsPrimo(i))
                {
                    listBox1.Items.Add(i);
                    cont++;
                    label2.Content = "Encontrados: " + cont;
                }
                progressBar1.Value = i;
                //System.Threading.Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();

            }
        }

        //Pulsado Botón PARALELO
        System.Threading.Thread hiloPrimos;

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //hiloPrimos = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(BuscarPrimosParalelo));
            hiloPrimos = new System.Threading.Thread(BuscarPrimosParalelo);

            int hasta;

            if (int.TryParse(textBox1.Text, out hasta))
            {
                Botones(false);
                button3.IsEnabled = false;
                progressBar1.Maximum = hasta;
                hiloPrimos.Start(hasta);
                //BuscarPrimosParalelo(hasta);
                //Botones(true);
            }

        }

        //Cambiamos el parametro a object para evitar el error de la delegada
        void BuscarPrimosParalelo(object ohasta)
        {
            //instancio la delegada en la variable delActualizaPB
            delActualizaPB = new delegadaActualizaPB(ActualizaPB);
            delActualizaLBL = new delegadaActualizaLBL(ActualizaListaLabel);
            delBotones = new delegadaBotones(Botones);

            int cont = 0;
            int hasta = (int)ohasta;

            for (int i = 1; i <= hasta; i++)
            {
                if (EsPrimo(i))
                {
                    cont++;
                    //listBox1.Items.Add(i);
                    //label2.Content = "Encontrados: " + cont;
                    this.Dispatcher.Invoke(delActualizaLBL, System.Windows.Threading.DispatcherPriority.Background,
                        new object[] {i, cont});
                }
                //progressBar1.Value = i;
                progressBar1.Dispatcher.Invoke(delActualizaPB , 
                    System.Windows.Threading.DispatcherPriority.Background, 
                    i);
                //System.Threading.Thread.Sleep(100);
            }

            MessageBox.Show("Terminado", "Paralelo");
            this.Dispatcher.Invoke(delBotones, true);
        }

        void ActualizaPB(int valor)
        {
            progressBar1.Value = valor;
        }

        void ActualizaListaLabel(int i, int cont)
        {
            //int i = (int)valores[0];
            //int cont = (int)valores[1];

            listBox1.Items.Add(i);
            label2.Content = "Encontrados: " + cont;
        }
    }
}
