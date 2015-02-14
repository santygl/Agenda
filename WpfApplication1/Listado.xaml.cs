using Contacto.DTO;
using Contactos.DAO;
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

namespace AgendaWPF
{
    /// <summary>
    /// Ventana para el manejo de contactos existentes
    /// </summary>
    public partial class Listado : Window
    {

        // conexion a la base de datos
        SqlConnection conexion = null;

        // constructor e inicializador de la gui
        public Listado(SqlConnection Conexion)
        {
            InitializeComponent();
            conexion = Conexion;
            DAOContacto dc = new DAOContacto();
            List<DTOContactos> contactos =dc.selectAll(conexion);


            foreach (DTOContactos i in contactos)
            {

                listView.Items.Add(i );
            }
        }

        // Refrescar datos desde la base de datos
        private void refrescar_Click(object sender, RoutedEventArgs e)
        {
            Listado win = new Listado(conexion);
            win.Show();
            this.Close();
        }

        // Accion del menu contextual del listview (Eliminar Elemento)
        private void RemoveContextMenu_OnClick(object sender, RoutedEventArgs e) 
        {
            DTOContactos item = (DTOContactos)listView.SelectedItem;
            DAOContacto dc = new DAOContacto();
            dc.delete(conexion, item);
            listView.Items.Remove(listView.SelectedItem);
        }

        // Accion del menu contextual del listview (Editar Elemento)
        private void EditContextMenu_OnClick(object sender, RoutedEventArgs e) 
        {
            DTOContactos item = (DTOContactos)listView.SelectedItem;
            Modificar win = new Modificar(conexion,item);
            win.Show();
            this.Close();
 
        }
    }
}
