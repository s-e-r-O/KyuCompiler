using System;
using System.Collections.Generic;
using System.Text;

namespace KyuCompiler.Models
{
    class Produccion
    {
        public static readonly string EPSILON = "\\e"; 
        public char Cabeza { get; set; }
        public string Cuerpo { get; set; }

        public Produccion(char cabeza, string cuerpo)
        {
            this.Cabeza = cabeza;
            this.Cuerpo = cuerpo;
        }
    }
}
