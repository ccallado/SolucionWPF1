using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ProyectoWPF1
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string cad = "Antes\nCultura: " + System.Threading.Thread.CurrentThread.CurrentCulture +
               "\nCulturaUI: " + System.Threading.Thread.CurrentThread.CurrentUICulture ;
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                System.Threading.Thread.CurrentThread.CurrentCulture;

            cad += "\n\nDespués\nCultura: " + System.Threading.Thread.CurrentThread.CurrentCulture +
               "\nCulturaUI: " + System.Threading.Thread.CurrentThread.CurrentUICulture;

            //MessageBox.Show(cad, "Cultura");
            System.Diagnostics.Debug.WriteLine(cad);
        } 
    }
}
