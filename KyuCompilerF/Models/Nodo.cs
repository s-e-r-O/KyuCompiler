using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Models
{
    public class Nodo
    {
        public List<Nodo> Hijos { get; set; }
        public bool FueEvaluado { get; set; }
        public Simbolo Symbol{ get; set; }
        public string Contenido { get; set; }
        public Produccion ProduccionUsada { get; set; }

        public Nodo(string contenido)
        {
            Contenido = contenido;
            Hijos = null;
            Symbol = new Simbolo();
            FueEvaluado = default(bool);
            ProduccionUsada = null;
        }
    }
}
