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


        //ThreadPool
        DateTime horaEntrada;

        public VentanaAsincrono()
        {
            InitializeComponent();

            Botones(true);
            System.Windows.Threading.DispatcherTimer timerWpf;

            //Modo mixto: constructor y propiedades
            timerWpf = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Background,
                this.Dispatcher);
            timerWpf.Interval = new TimeSpan(0, 0, 1);
            timerWpf.Tick += new EventHandler(timerWpf_Tick);

            //Todo en un constructor
            //timerWpf = new System.Windows.Threading
            //    .DispatcherTimer(new TimeSpan(0, 0, 1),
            //    System.Windows.Threading.DispatcherPriority.Background,
            //    timerWpf_Tick,
            //    this.Dispatcher);

            //Pasar el código del evento
            //timerWpf.Tick +=
            //    new EventHandler(delegate(object sender, EventArgs e)
            //                    {
            //                        label3.Content = DateTime.Now.ToLongTimeString();
            //                    }
            //                    );

            label3.Content = DateTime.Now.ToLongTimeString();
            timerWpf.Start();

            //instancio la delegada en la variable delActualizaPB
            delActualizaPB = new delegadaActualizaPB(ActualizaPB);
            delActualizaLBL = new delegadaActualizaLBL(ActualizaListaLabel);
            delBotones = new delegadaBotones(Botones);
        }

        //Codigo de evento para el constructor del timer
        void timerWpf_Tick(object sender, EventArgs e)
        {
            label3.Content = DateTime.Now.ToLongTimeString();
        }

        ////Objeto de bloqueo no se usa
        //object objetoBloqueo = new object(); //Para bloquear la sección crítica
        //Función para el cálculo de números primos
        bool EsPrimo(int x)
        {
            //Esto es absurdo porque no hay variables globales y se puede ejecutar en paralelo
            //Esto obligaría a que este proceso no se podría ejecutar en paralelo.
            //Pero es esta función la que hemos puesto en paralelo. Con lo que es absurdo.
            //Solo como ejemplo de SECCIONES CRITICAS
            //lock (objetoBloqueo)
            //{
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
            //}
        }

        //Función para habilitar y deshabilitar los botones
        void Botones(bool habilitar)
        {
            button1.IsEnabled = habilitar;
            button2.IsEnabled = habilitar;
            button3.IsEnabled = !habilitar;
            button4.IsEnabled = habilitar;
            button5.IsEnabled = !habilitar;
            button6.IsEnabled = habilitar;
            button7.IsEnabled = !habilitar;
            button8.IsEnabled = habilitar;
            button9.IsEnabled = !habilitar;
            if (!habilitar)
            {
                progressBar1.Value = 0;
                listBox1.Items.Clear();
                label2.Content = "";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int hasta;

            if (int.TryParse(textBox1.Text, out hasta))
            {
                Botones(false);
                button3.IsEnabled = false;
                button5.IsEnabled = false;
                button7.IsEnabled = false;
                button9.IsEnabled = false;
                progressBar1.Maximum = hasta;
                //Actualiza el formulario para que se vean los cambios
                System.Windows.Forms.Application.DoEvents();
                BuscarPrimosSincrono(hasta);
                Botones(true);
            }
        }

        void BuscarPrimosSincrono(int hasta)
        {
            //Control de tiempos
            horaEntrada = DateTime.Now;

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
            ActualizaPB((int)progressBar1.Maximum);
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
                button5.IsEnabled = false;
                button7.IsEnabled = false;
                button9.IsEnabled = false;
                progressBar1.Maximum = hasta;
                hiloPrimos.Start(hasta);
                //BuscarPrimosParalelo(hasta);
                //Botones(true);
            }

        }

        //Cambiamos el parametro a object para evitar el error de la delegada
        void BuscarPrimosParalelo(object ohasta)
        {
            //El control de excepciones ha de hacerse en el hilo, 
            //sino se pierde.
            try
            {
                //Control de tiempos
                horaEntrada = DateTime.Now;

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
                            new object[] { i, cont });
                    }
                    //progressBar1.Value = i;
                    progressBar1.Dispatcher.Invoke(delActualizaPB,
                        System.Windows.Threading.DispatcherPriority.Background,
                        i);
                    //System.Threading.Thread.Sleep(100);
                }

                MessageBox.Show("Terminado", "Paralelo");
                this.Dispatcher.Invoke(delBotones, true);
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                MessageBox.Show(ex.Message, "ThreadAbortException");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Excepcion general");
            }
        }

        void ActualizaPB(int valor)
        {
//            progressBar1.Value = valor;
            progressBar1.Value++;
            //Si el progressbar llega al final muestro la diferencia en segundos.
            if (progressBar1.Value == progressBar1.Maximum)
            {
                label2.Content += " - Tardó " + DateTime.Now.Subtract(horaEntrada).TotalSeconds + " s.";
                Botones(true);
            }
        }

        void ActualizaListaLabel(int i, int cont)
        {
            //int i = (int)valores[0];
            //int cont = (int)valores[1];

            listBox1.Items.Add(i);
            label2.Content = "Encontrados: " + cont;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string cad = "Existe: " + hiloPrimos.IsAlive;
            cad += "\nEstado: " + hiloPrimos.ThreadState;
            //Cancela el hilo y lanza una excepción T.A.E.
            hiloPrimos.Abort();
            cad += "\nDespués...\nExiste: " + hiloPrimos.IsAlive;
            cad += "\nEstado: " + hiloPrimos.ThreadState;
            //MessageBox.Show(cad, "Hilo abortado");
            Botones(true);
        }

        #region BackgroundWorker

        System.ComponentModel.BackgroundWorker bgw = null;
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            bgw = new System.ComponentModel.BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.WorkerReportsProgress = true;

            bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);

            int hasta;
            if (int.TryParse(textBox1.Text, out hasta))
            {
                Botones(false);
                button3.IsEnabled = false;
                button7.IsEnabled = false;
                button9.IsEnabled = false;
                //Controlo negativos para que la barra de progreso no salga all 100%
                progressBar1.Maximum = (hasta < 0 ? 100 : hasta);
                bgw.RunWorkerAsync(hasta);
            }
        }

        void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //Control de tiempos
            horaEntrada = DateTime.Now;

            int cont = 0;
            int hasta = (int)e.Argument;

            if (hasta < 0)
                throw new ArgumentOutOfRangeException("hasta", hasta, "No se permiten valores negativos");
            if (hasta > 1000)
                throw new OverflowException("Ande vas...");

            for (int i = 1; i <= hasta; i++)
            {
                if (bgw.CancellationPending)
                {
                    //if (MessageBox.Show("¿Desea cancelar la búsqueda?",
                    //    "Cancelación", MessageBoxButton.YesNo,
                    //    MessageBoxImage.Question,
                    //    MessageBoxResult.No) == MessageBoxResult.Yes)
                    //{
                    e.Cancel = true;
                    return;
                    //}
                    //else 
                    //{
                    //    e.Cancel = false;
                    //}
                }

                bool primo = EsPrimo(i);
                if (primo)
                {
                    cont++;
                }
                bgw.ReportProgress(i, (primo ? (int?)cont : null));
                //System.Threading.Thread.Sleep(1);
            }
            e.Result = cont;
        }

        void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //Esto lo hace siempre
            progressBar1.Value = e.ProgressPercentage;
            //Si es primo actualizo etos dos controles (Si el segundo argumento no es null)
            if (e.UserState != null)
            {
                listBox1.Items.Add(e.ProgressPercentage);
                label2.Content = "Encontrados: " + ((int?)e.UserState).Value;
            }
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Cancelado por el usuario",
                                "Cancelado",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

            }
            else
            {
                if (e.Error != null)
                    MessageBox.Show(e.Error.Message,
                        //"Error",
                                    e.Error.GetType().ToString(),
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                else
                    ActualizaPB((int)progressBar1.Maximum);
                MessageBox.Show("Primos entre 1 y " + textBox1.Text + ": " + e.Result, "Paralelo");
            }

            Botones(true);
        }

        #endregion

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            bgw.CancelAsync();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button)
            {
                if (chkThreadPool.IsChecked.Value)
                {
                    VerValoresThreadPool("Antes");
                    //los valores CUIDADIN
                    //Si no pongo valores o la variable = 0 eso es lo que pone
                    //Y si te quedas sin hilos puede ir muy mal
                    System.Threading.ThreadPool.SetMaxThreads(10, maxHilosIO);
                    System.Threading.ThreadPool.SetMinThreads(10, minHilosIO);
                    VerValoresThreadPool("Después");
                }

                int hasta;

                if (int.TryParse(textBox1.Text, out hasta))
                {
                    Botones(false);
                    button3.IsEnabled = false;
                    button5.IsEnabled = false;
                    //button7.IsEnabled = false;
                    button9.IsEnabled = false;
                    progressBar1.Maximum = hasta;
                    cancelarThreadPool = false;
                    System.Windows.Forms.Application.DoEvents();
                    BuscarPrimoThreadPool(hasta);
                    //Botones(true);
                }
            }

        }

        int contadorThreadPool = 0;
        public void ComprobarPrimoThreadPool(object o)
        {
            int i = (int)o;
            if (cancelarThreadPool)
                //Esto detiene el hilos que vayan a empezar a ejecutarse
                return;

            if (EsPrimo(i))
            {
                //contadorThreadPool++;
                //No se puede tocar esta variable hasta que no termine el hilo
                //Esto solo es para variables de tipo INT y LONG
                System.Threading.Interlocked.Increment(ref contadorThreadPool);
                this.Dispatcher.Invoke(delActualizaLBL, System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { i, contadorThreadPool });
            }
            progressBar1.Dispatcher.Invoke(delActualizaPB,
                System.Windows.Threading.DispatcherPriority.Background,
                i);
            //System.Threading.Thread.Sleep(100);
        }

        void BuscarPrimoThreadPool(int hasta)
        {
            //Control de tiempos
            horaEntrada = DateTime.Now;
            contadorThreadPool = 0;

            for (int i = 1; i <= hasta; i++)
            {
                if (cancelarThreadPool)
                    //Esto detiene el for pero no para los hilos
                    return;
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(ComprobarPrimoThreadPool), i);
                //Nuevo método que funciona.
                //System.Threading.ThreadPool.QueueUserWorkItem(ComprobarPrimoThreadPool, i);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        int maxHilos, maxHilosIO, minHilos, minHilosIO;

        void VerValoresThreadPool(string info)
        {
            System.Threading.ThreadPool.GetMaxThreads(out maxHilos, out maxHilosIO);
            System.Threading.ThreadPool.GetMinThreads(out minHilos, out minHilosIO);
            MessageBox.Show("\tHilos\tIO" +
                            "\nMáximo:\t" + maxHilos + "\t" + maxHilosIO +
                            "\nMínimo:\t" + minHilos + "\t" + minHilosIO,
            "ThreadPool " + (info != "" ? info : ""));
        }

        bool cancelarThreadPool = false;

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            cancelarThreadPool = true;
            Botones(true);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            int hasta;

            if (int.TryParse(textBox1.Text, out hasta))
            {
                Botones(false);
                button3.IsEnabled = false;
                button5.IsEnabled = false;
                button7.IsEnabled = false;
                progressBar1.Maximum = hasta;
                cancelarThreadPool = false;
                System.Windows.Forms.Application.DoEvents();
                BuscarPrimosTPL(hasta);
                //Botones(true);
            }
        }

        void BuscarPrimosTPL(int hasta)
        {
            //Control de tiempos
            horaEntrada = DateTime.Now;
            contadorThreadPool = 0;

            //Voy a usar una Lambda como parametro de la funcion FOR
            //(variable/s)=>{Código}
            //La Lambda que vamos a crear queda
            //(i) => { ComprobarPrimoTPL(i); }

            System.Threading.Tasks.Parallel
                .For(1, hasta + 1, (i, estado) => { ComprobarPrimoTPL(i, estado);});
        }

        public void ComprobarPrimoTPL(int i, System.Threading.Tasks.ParallelLoopState estado)
        {
            //int i = (int)o;
            if (cancelarThreadPool)
                //Esto detiene el hilos que vayan a empezar a ejecutarse
                estado.Break();

            if (EsPrimo(i))
            {
                //contadorThreadPool++;
                //No se puede tocar esta variable hasta que no termine el hilo
                //Esto solo es para variables de tipo INT y LONG
                System.Threading.Interlocked.Increment(ref contadorThreadPool);
                this.Dispatcher.Invoke(delActualizaLBL, System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { i, contadorThreadPool });
            }
            progressBar1.Dispatcher.Invoke(delActualizaPB,
                System.Windows.Threading.DispatcherPriority.Background,
                i);
            //Actualiza el formulario para que se vean los cambios
            System.Windows.Forms.Application.DoEvents();
            //System.Threading.Thread.Sleep(100);
        }

    }
}
