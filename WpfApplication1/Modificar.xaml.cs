using Contacto.DTO;
using Contactos.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Modificacion de un contacto
    /// </summary>
    public partial class Modificar : Window
    {
        private SqlConnection conexion = null;
        private DTOContactos oldContacto;

        //Datos necesarios para el funcionamiento
        public Modificar(SqlConnection Conexion, DTOContactos Contacto)
        {
            InitializeComponent();
            conexion = Conexion;
            date_Fecha.SelectedDate = Contacto.Cumpleanhos;
            text_Nombre.Text = Contacto.Nombre;
            text_Telefono.Text = Contacto.Telefono;
            oldContacto = Contacto;
        }


        //precarga de datos y modificaciond e un contacto existente
        private void but_Modificar_Click(object sender, RoutedEventArgs e)
        {
            DTOContactos contacto = new DTOContactos();

            contacto.Nombre = text_Nombre.Text;
            DateTime? date = date_Fecha.SelectedDate;
            contacto.Cumpleanhos = (DateTime)date;
            contacto.Telefono = text_Telefono.Text;
            bool continuar = true;
            if (contacto.Nombre.Equals(""))
            {
                continuar = false;
                MessageBox.Show("El nobre no puede ser nulo");
            }
            if (!Regex.IsMatch(contacto.Telefono, "^\\+?\\d+$"))
            {
                continuar = false;
                MessageBox.Show("El número debe ser  tipo +número o número ");
            }
            DateTime auxDate = (DateTime)date;

            if ( (auxDate.Year <= 1753) || (auxDate.Year >= 9999) )
            {
                continuar = false;
                MessageBox.Show("selecione un año ente 1753 y 9999");
                
            }

            if (continuar)
            {
                DAOContacto dc = new DAOContacto();
                dc.update(conexion, oldContacto, contacto);
                this.Close();
                Listado win = new Listado(conexion);
                win.Show();
            }


        }

    }
}
