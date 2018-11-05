using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KyuCompilerF.Models
{
    public class Produccion
    {
        public static readonly string EPSILON = "\\e";

        public static bool PuedeSerTerminal(string s)
        {
            return (s.Length > 1 || (s.Length > 0 && !char.IsUpper(s[0])));
        }

        public string[] Palabras { get; private set; }

        public char Cabeza { get; set; }
        public Action<Dictionary<string, List<Simbolo>>> reglaAtributo;
        private string cuerpo;
        public string Cuerpo
        {
            get { return cuerpo; }
            set
            {
                cuerpo = value;
                Palabras = Regex.Split(cuerpo, @"[ \t]+");
            }
        }

        public Produccion(char cabeza, string cuerpo)
        {
            this.Cabeza = cabeza;
            this.Cuerpo = cuerpo;
        }

        public Produccion(char cabeza, string cuerpo,Action<Dictionary<string, List<Simbolo>>> atributoFunc)
        {
            this.Cabeza = cabeza;
            this.Cuerpo = cuerpo;
            this.reglaAtributo = atributoFunc;
        }
    }
}
