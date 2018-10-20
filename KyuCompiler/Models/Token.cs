using System;
using System.Collections.Generic;
using System.Text;

namespace KyuCompiler.Models
{
    class Token
    {
        public string token { get; set; }//describe si es palabra reservada u otro
        public string lexema { get; set; }//lo que es
        public int linea { get; set; }
        public int columna { get; set; }
        public string descripcion { get; set; }//en caso de necesitar una descripción más detallada

        public Token()
        {
            this.token = "";
            this.lexema = "";
            this.linea = 0;
            this.columna = 0;
            this.descripcion = "";
        }

        public Token(string token, string lexema, int linea, int columna)
        {
            this.token = token;
            this.lexema = lexema;
            this.linea = linea;
            this.columna = columna;
            this.descripcion = "";
        }

        public Token(string lexema, int linea, int columna)
        {
            this.token = "";
            this.lexema = lexema;
            this.linea = linea;
            this.columna = columna;
            this.descripcion = "";
        }

        override
        public string ToString()
        {
            return this.lexema + "\t es: " + this.token + "\tlinea: " + this.linea + "\tcolumna: " + this.columna + "\tdescripción: " + this.descripcion;
        }
    }
}
