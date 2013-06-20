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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class VentanaHeadered_ItemsControls : Window
    {
        public VentanaHeadered_ItemsControls()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox1.Items.Add(new CheckBox() { Content = "Sábado" });
            CheckBox ch1 = new CheckBox() { Content = "Domingo" };
            ComboBoxItem cbi = new ComboBoxItem();
            cbi.Content = ch1;
            comboBox1.Items.Add(cbi);

            //Para el segundo combo
            Ellipse el = new Ellipse()
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Blue,
                Margin = new Thickness(5, 0, 5, 0)
            };
            //Ellipse el = new Ellipse() { Width = 10, Height = 10, Fill = new SolidColorBrush(Colors.Blue) };
            TextBlock tb = new TextBlock() { Text = "Azul", Foreground = Brushes.Blue };
            StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal };
            sp.Children.Add(el);
            sp.Children.Add(tb);
            ComboBoxItem cbi2 = new ComboBoxItem();
            cbi2.Content = sp;
            comboBox2.Items.Add(cbi2);

            //Para la Lista
            listBox1.Items.Add(new ListBoxItem() { Content = "Jueves" });
            listBox1.Items.Add(new ListBoxItem() { Content = "Viernes" });
            listBox1.Items.Add(new CheckBox() { Content = "Sábado" });
            listBox1.Items.Add(new ListBoxItem() { Content = new CheckBox() { Content = "Domingo" } });

            //Control de la hora del status bar
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            //Creación del evento
            timer.Tick += new EventHandler(timer_Tick);
            timer_Tick(null, null);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            tblFecha.Text = DateTime.Now.ToLongDateString();
            tblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void Seleccion_ComboBox(object sender, SelectionChangedEventArgs e)
        {
            object item = ((ComboBox)sender).SelectedItem;
            string tipo = item.GetType().ToString();
            string cad = "Tipo item seleccionado: " + tipo;
            tipo = tipo.Substring(tipo.LastIndexOf(".") + 1);
            switch (tipo.ToLower())
            {
                case "comboboxitem":
                    item = ((ComboBoxItem)item).Content;
                    string tipoContenido = item.GetType()
                                             .ToString()
                                             .Substring(item.GetType()
                                                           .ToString()
                                                           .LastIndexOf(".") + 1).ToLower();
                    cad += "\nTipo contenido: " + tipoContenido;
                    if (tipoContenido == "checkbox")
                        goto case "checkbox";
                    if (tipoContenido == "textblock")
                        goto case "textblock";
                    if (tipoContenido == "label")
                        goto case "label";
                    if (tipoContenido == "string")
                        goto case "string";

                    //Este si no puede convertir deja null en sp
                    StackPanel sp = item as StackPanel;
                    //este si no puede convertir produce una excepción
                    //StackPanel sp = (StackPanel)item;
                    if (sp != null)
                        cad += "\nContenido: " + ((TextBlock)sp.Children[1]).Text;
                    break;
                case "string":
                    cad += "\nContenido: " + item;
                    break;
                case "checkbox":
                    cad += "\nContenido: " + ((CheckBox)item).Content;
                    break;
                case "textblock":
                    cad += "\nContenido: " + ((TextBlock)item).Text;
                    break;
                case "label":
                    cad += "\nContenido: " + ((Label)item).Content;
                    break;
                default:
                    throw new Exception("Por aquí no debe pasar... \nTipo: " + tipo);
                //                    break;
            }
            MessageBox.Show(cad);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string cad = "Seleccionados:";
            foreach (object o in listBox1.SelectedItems)
            {
                cad += "\n  - ";
                if (o is ListBoxItem)
                {
                    ListBoxItem l = (ListBoxItem)o;
                    if (l.Content is string)
                        cad += l.Content;
                    else
                        cad += ExtraeCheck(l.Content);

                }
                else //asumo que es el checkbox
                {
                    cad += ExtraeCheck(o);
                }
            }

            MessageBox.Show(cad, "Cantidad: " + listBox1.SelectedItems.Count);
        }

        private string ExtraeCheck(object o)
        {
            CheckBox c = o as CheckBox;
            return c.Content +
                   (c.IsChecked.HasValue && c.IsChecked.Value
                    ? " - Marcado"
                    : "");
        }

        private void mnuSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnuAbrir_Click(object sender, RoutedEventArgs e)
        {
            BuscaArchivo("");
        }

        private void BuscaArchivo(string archivo)
        {
            System.Windows.Forms.OpenFileDialog ofd1;
            ofd1 = new System.Windows.Forms.OpenFileDialog();
            ofd1.FileName = archivo;

            ofd1.Filter = "Archivos de código (*.cs)|*.cs";
            ofd1.Filter += "|Archivos de código y contenido(*.cs y *.xaml)|*.cs;*.xaml";
            ofd1.Filter += "|Todos los archivos|*.*";
            ofd1.FilterIndex = 3;

            if (ofd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                MessageBox.Show("Archivo: " + ofd1.FileName);
            else
                MessageBox.Show("No hay seleccionado ningún archivo.");
        }

        private void tbBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscaArchivo(textBox2.Text);
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker2.SelectedDate < datePicker1.SelectedDate)
                datePicker2.SelectedDate = datePicker1.SelectedDate;

            datePicker2.DisplayDateStart = datePicker1.SelectedDate;

            string cad = "";
            DateTime f = datePicker1.SelectedDate.Value;

            cad = "Año: " + f.Year;
            cad += "\nDía: " + f.Day;
            cad += "\nMinutos: " + f.Minute;
            cad += "\nTicks: " + f.Ticks;
            cad += "\nDía de la semana: " + f.DayOfWeek;
            cad += "\nDía del año: " + f.DayOfYear;

            cad += "\nDías este mes: " + DateTime.DaysInMonth(f.Year, f.Month);
            cad += "\nBisiesto: " + (DateTime.IsLeapYear(f.Year) ? "Sí" : "No");

            DateTime f2 = DateTime.UtcNow;
            cad += "\nUTCNow: ";
            //            if (f2.Kind == DateTimeKind.Local)
            cad += "UTC: " + f2.TimeOfDay.ToString();
            //            else
            cad += " Local: " + f2.ToLocalTime().TimeOfDay.ToString();

            f2 = DateTime.Now;
            cad += "\nNow: ";
            //            if (f2.Kind == DateTimeKind.Local  )
            cad += "Local: " + f2.TimeOfDay.ToString();
            //            else
            cad += " UTC: " + f2.ToUniversalTime().TimeOfDay.ToString();

            cad += "\n\nNormal: " + f2.ToString();
            cad += "\nFecha larga: " + f2.ToLongDateString();
            cad += "\nFecha corta: " + f2.ToShortDateString();
            cad += "\nHora larga: " + f2.ToLongTimeString();
            cad += "\nHora corta: " + f2.ToShortTimeString();

            MessageBox.Show(cad, "Info fecha");

        }

        private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeSpan lapso = datePicker2.SelectedDate.Value.Subtract(datePicker1.SelectedDate.Value);
            //            TimeSpan lapso = datePicker2.SelectedDate.Value.Date.Subtract(datePicker1.SelectedDate.Value.Date);

            string cad = "";
            cad += "Días: " + lapso.Days.ToString();
            cad += "\nHoras: " + lapso.Hours.ToString();
            cad += "\nDías totales: " + lapso.TotalDays.ToString();
            cad += "\nHoras totales: " + lapso.TotalHours.ToString();
            cad += "\nTicks: " + lapso.Ticks.ToString();

            DateTime fActual = DateTime.Now;
            lapso = new TimeSpan(4, 9, 24, 15);
            DateTime fNueva = fActual.Subtract(lapso);

            cad += "\n\nFecha actual: " + fActual.ToString();
            cad += "\nFecha nueva: " + fNueva.ToString();
            MessageBox.Show(cad, "Diferencias fechas");
        }

        private void mnuStBar_Checked(object sender, RoutedEventArgs e)
        {
            if (statusBar1 != null)
                statusBar1.Visibility = System.Windows.Visibility.Visible;
        }

        private void mnuStBar_Unchecked(object sender, RoutedEventArgs e)
        {
            if (statusBar1 != null)
                statusBar1.Visibility = System.Windows.Visibility.Hidden;
        }

        private void mnuVer_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            mnuBh.Items.Clear();
            foreach (ToolBar tb in tbTray.ToolBars)
            {
                MenuItem mi = new MenuItem();
                mi.Header = tb.ToolTip != null ? tb.ToolTip : tb.Name;
                mi.IsCheckable = true;
                mi.IsChecked = tb.IsVisible;
                mi.StaysOpenOnClick = true;
                mi.Tag = tb;
                mi.Checked += new RoutedEventHandler(mi_CheckedUnchecked);
                mi.Unchecked += new RoutedEventHandler(mi_CheckedUnchecked);

                mnuBh.Items.Add(mi);
            }
        }
        private void mi_CheckedUnchecked(object sender, RoutedEventArgs e)
        {
            //Como reconocer el objeto que indica el menu pulsado
            ToolBar t = (ToolBar)((MenuItem)sender).Tag;

            t.Visibility = (t.IsVisible ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible);
        }

        private void ExpandirContraerTreeview(object sender, RoutedEventArgs e)
        {
            foreach (object o in treeView1.Items)
            {
                TreeViewItem nodo = o as TreeViewItem;
                if (nodo != null)
                    nodo.IsExpanded =
                    ((MenuItem)sender).Header.ToString().StartsWith("E") ?
                        true : false;
            }
        }

    }
}
