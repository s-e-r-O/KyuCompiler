using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Models
{
    public enum SimboloTipo
    {
        NUMERO,
        BOOLEANO,
        CHAR,
        LIST_NUMERO,
        LIST_BOOLEANO,
        LIST_CHAR,
        OPERADOR_BOOLEANO,
        OPERADOR_ARITMETICO,
        EMPTY,
        ERROR,
        NULL
    }

    public class Simbolo<T>
    {
        public SimboloTipo Tipo { get; set; }
        public T Valor { get; set; }
        public int Nargs { get; set; }
        public string Operador { get; set; }

        public Simbolo(SimboloTipo tipo, T valor)
        {
            this.Tipo = tipo;
            this.Valor = valor;
        }

        public Simbolo()
        {
            this.Tipo = SimboloTipo.NULL;
        }

        public string ToString()
        {
            return "tipo: " + this.Tipo + "valor: " + this.Valor;
        }

    }
}
