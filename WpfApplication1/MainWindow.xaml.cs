using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgendaWPF
{
    /// <summary>
    /// Pantalla principal
    /// </summary>
    public partial class MainWindow : Window
    {

        private SqlConnection conexion=null;


        
        //Pantalla inicial (Solo activa la opcion de conectarse a una base de datos)
        public MainWindow()
        {
            InitializeComponent();
            if (conexion!=null)
            {
                but_Nuevo.Visibility = System.Windows.Visibility.Visible;
                but_Contactos.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ventana.Height = 120;
                but_Nuevo.Visibility = System.Windows.Visibility.Hidden;
                but_Contactos.Visibility = System.Windows.Visibility.Hidden;
            }


        }

        // Pantalla inicial una vez conectado a la base de datos
        public MainWindow(SqlConnection Conexion)
        {
            InitializeComponent();
            conexion = Conexion;
            if (conexion != null)
            {
                but_Nuevo.Visibility = System.Windows.Visibility.Visible;
                but_Contactos.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                but_Nuevo.Visibility = System.Windows.Visibility.Hidden;
                but_Contactos.Visibility = System.Windows.Visibility.Hidden;
            }
            

        }

 

        // Agregar un nuevo dato
        private void but_Nuevo_Click(object sender, RoutedEventArgs e)
        {

            NuevoContacto win = new NuevoContacto(conexion);
            win.Show();
            
        }


        // Gestion de la conexion con la base de datos
        private void but_BaseDatos_Click(object sender, RoutedEventArgs e)
        {
            SelecInstancia win = new SelecInstancia();
            win.Show();
            this.Close();
            
            
        }

        // Listado de contactos
        private void but_Contactos_Click(object sender, RoutedEventArgs e)
        {
            Listado win = new Listado(conexion);
            win.Show();
        }
    }
}
