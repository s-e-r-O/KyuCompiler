using System;
using System.Collections.Generic;
using System.Text;

namespace KyuCompiler.Models
{
    class Produccion
    {
        public char Cabeza { get; set; }
        public string Cuerpo { get; set; }

        public Produccion(char cabeza, string cuerpo)
        {
            this.Cabeza = cabeza;
            this.Cuerpo = cuerpo;
        }
    }
}
