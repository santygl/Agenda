using System;
using System.Collections.Generic;
using System.Text;

namespace Contacto.DTO
{

    public class DTOContactos
    {

        private int idContacto =-1;
        private string nombre = null;
        private string telefono = null;
        private DateTime cumpleanhos = DateTime.Now;



        public int IdContacto
        {
            get { return idContacto; }
            set { idContacto = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public DateTime Cumpleanhos
        {
            get { return cumpleanhos; }
            set { cumpleanhos = value; }
        }


        public string toInsert()
        {
            return " '"+ nombre + "', " + "'" + telefono + "', " + "' " + cumpleanhos.ToString("dd/MM/yyyy") + " '";
        }


        public DTOContactos()
        {
        }


        public DTOContactos(string Nombre, string Telefono, DateTime Cumpleanhos)
        {
            nombre = Nombre;
            telefono = Telefono;
            cumpleanhos = Cumpleanhos;
        }

        public DTOContactos(int IdContacto, string Nombre, string Telefono, DateTime Cumpleanhos)
        {
            idContacto = IdContacto;
            nombre = Nombre;
            telefono = Telefono;
            cumpleanhos = Cumpleanhos;
        }
    }
}