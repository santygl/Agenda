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
using Contacto.DTO;
using Contactos.DAO;
using System.Text.RegularExpressions;

namespace AgendaWPF
{
    /// <summary>
    /// añadir un contacto
    /// </summary>
    public partial class NuevoContacto : Window
    {
        private SqlConnection conexion = null;

        //constructor con la conexion
        public NuevoContacto(SqlConnection Conexion)
        {
            InitializeComponent();
            conexion = Conexion;
        }

        // Añadir nuevo contacto
        private void but_AddContacto_Click(object sender, RoutedEventArgs e)
        {
            DTOContactos contacto = new DTOContactos();

            contacto.Nombre = text_Nombre.Text;
            DateTime? date = date_Fecha.SelectedDate;
            
            contacto.Telefono = text_Telefono.Text;
            
            bool continuar = true;
            if (contacto.Nombre.Equals("") )
            {
                continuar = false;
                MessageBox.Show("El nobre no puede ser nulo");
            }
            if(!Regex.IsMatch(contacto.Telefono,"^\\+?\\d+$"))
            {
                continuar = false;
                MessageBox.Show("El número debe ser  tipo +número o número ");
            }
            if (date == null)
            {
                continuar = false;
                MessageBox.Show("selecione una fecha");
            }
            else
            {
                DateTime auxDate = (DateTime) date;

                if ( (auxDate.Year <= 1753) || (auxDate.Year >= 9999) )
                {
                    continuar = false;
                    MessageBox.Show("selecione un año ente 1753 y 9999");
                }
            }


            
            if (continuar)
            {
                contacto.Cumpleanhos = (DateTime)date;
                DAOContacto dc = new DAOContacto();
                dc.insert(conexion, contacto);
                this.Close();
            }


        }
    }
}
