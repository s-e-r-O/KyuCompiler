using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KyuCompiler.Models
{
    class Gramatica
    {
        private List<Produccion> producciones;
        public List<Produccion> Produccciones {
            get
            {
                return producciones;
            }
            set
            {
                producciones = value;
                NoTerminales = new List<char>();
                Terminales = new List<string>();
                foreach (Produccion p in producciones)
                {
                    if (!NoTerminales.Contains(p.Cabeza)) {
                        NoTerminales.Add(p.Cabeza);
                    }
                    string[] palabras = Regex.Split(p.Cuerpo, @"[ \t]+");
                    foreach (string palabra in palabras)
                    {
                        if ((palabra.Length > 1 || (palabra.Length > 0 && !char.IsUpper(palabra[0]))) && !Terminales.Contains(palabra))
                        {
                            Terminales.Add(palabra);
                        }
                    }
                }
                Terminales.Remove(Produccion.EPSILON);
            }
        }
        public List<char> NoTerminales { get; private set; }
        public List<string> Terminales { get; private set; }

        public bool EsTerminal(string s) {
            return Terminales.Contains(s);
        }

        public bool EsNoTerminal(char c)
        {
            return NoTerminales.Contains(c);
        }
    }
}
