using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Models
{
    class TablaSimbolo
    {
        public Dictionary<string, Simbolo<Object>> tablaSimbolos { get; set; }

        public TablaSimbolo() { }

        public TablaSimbolo(Dictionary<string, Simbolo<Object>> tablaSimbolos)
        {
            this.tablaSimbolos = tablaSimbolos;
        }
    }
}
