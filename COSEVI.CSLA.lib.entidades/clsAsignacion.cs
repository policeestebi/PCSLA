using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COSEVI.CSLA.lib.entidades
{
    public class clsAsignacion
    {
        public clsAsignacion(string actividad, double lunes)
        {
            this.actividad = actividad;
            this.lunes = lunes;
        }

        private string actividad;

        public string Actividad
        {
            get { return actividad; }
            set { actividad = value; }
        }
        private double lunes;

        public double Lunes
        {
            get { return lunes; }
            set { lunes = value; }
        }
    }
}
