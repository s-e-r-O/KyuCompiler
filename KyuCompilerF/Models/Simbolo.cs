using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Models
{
    class Simbolo<T>
    {

        public enum SimboloTipo
        {
            NUMERO = 1,
            BOOLEANO = 2,
            CADENA = 3,
            CHAR = 4,
            OPERADOR_BOOLEANO = 5,
            OPERADOR_ARITMETICO = 6,
            NULL = -1
        }
        public SimboloTipo tipo { get; set; }
        public T valor { get; set; }



        public Simbolo(SimboloTipo tipo, T valor)
        {
            this.tipo = tipo;
            this.valor = valor;
        }

        public Simbolo()
        {
            this.tipo = SimboloTipo.NULL;
        }

        public string ToString()
        {
            return "tipo: " + this.tipo + "valor: " + this.valor;
        }

    }
}
