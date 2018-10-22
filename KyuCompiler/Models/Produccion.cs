﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KyuCompiler.Models
{
    class Produccion
    {
        public static readonly string EPSILON = "\\e";

        public static bool PuedeSerTerminal(string s) {
            return (s.Length > 1 || (s.Length > 0 && !char.IsUpper(s[0])));
        }

        public string[] Palabras { get; private set; }

        public char Cabeza { get; set; }
        private string cuerpo;
        public string Cuerpo {
            get { return cuerpo; }
            set {
                cuerpo = value;
                Palabras = Regex.Split(cuerpo, @"[ \t]+");
            }
        }

        public Produccion(char cabeza, string cuerpo)
        {
            this.Cabeza = cabeza;
            this.Cuerpo = cuerpo;
        }
    }
}
