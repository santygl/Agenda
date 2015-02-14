using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Contactos.DAO;

namespace AgendaWPF
{
    /// <summary>
    /// Seleccion de instancia
    /// </summary>
    public partial class SelecInstancia : Window
    {

        private string instaciaSelec = "";
        private string error = "";

        //Carga de instancias disponibles en el ordenador
        public SelecInstancia()
        {
            System.Data.Sql.SqlDataSourceEnumerator servidores;
            System.Data.DataTable tablaServidores;
            InitializeComponent();
            servidores = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            tablaServidores = servidores.GetDataSources();


            foreach (System.Data.DataRow rowServidor in tablaServidores.Rows)
            {
                if (String.IsNullOrEmpty(rowServidor["InstanceName"].ToString()))
                    comboBox_Instancias.Items.Add(rowServidor["ServerName"].ToString());
                else
                    comboBox_Instancias.Items.Add(rowServidor["ServerName"] + "\\" + rowServidor["InstanceName"]);
            }

      
      }

        // Instancia seleccionada
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            instaciaSelec = comboBox_Instancias.Text;
        }

        // confiracion e valores corectos
        private void but_Selec_Click(object sender, RoutedEventArgs e)
        {
            SelecBase win;
            string usuario = this.text_Usuario.Text;
            string pass = this.pass_Pass.Password;
            instaciaSelec = comboBox_Instancias.Text;

            bool continuar = true;
            if (usuario.Equals(""))
            {
                continuar = false;
                this.text_Usuario.Background = Brushes.Red;
            }

            if (pass.Equals(""))
            {
                continuar = false;
                this.pass_Pass.Background = Brushes.Red;
            }

            if (instaciaSelec.Equals(""))
            {

                continuar = false;
                textInstancia.Foreground = Brushes.Red;
            }


            if (continuar)
            {
                SqlConnection conexion = new SqlConnection(@"Server=" + instaciaSelec + "; User Id=" + usuario + "; Password=" + pass);
                bool conectado = true;

                try
                {
                    conexion.Open();
                    
                }
                catch (Exception ex)
                {
                    error = ex.ToString(); 
                    MessageBox.Show("¿Usuario o contraseña incorrecto?");               
                    butError.Visibility = System.Windows.Visibility.Visible;
                    butError.IsEnabled = true;
                    conectado = false;
                }


                if (conectado)
                {
                    win = new SelecBase(conexion);
                    win.setParametros(instaciaSelec, usuario, pass);
                    win.Show();
                    this.Close();
                }
            }
        }

        // En caso de error podremos ver el error de la conexion
        private void error_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(error);
        }

    }
}
