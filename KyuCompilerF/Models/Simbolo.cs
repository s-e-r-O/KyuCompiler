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

    public class Simbolo
    {
        public string Id { get; set; }
        public SimboloTipo Tipo { get; set; }
        public Object Valor { get; set; }
        public int Nargs { get; set; }
        public string Operador { get; set; }

        public Simbolo(SimboloTipo tipo, Object valor)
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

        public static bool IsList(SimboloTipo t)
        {
            return t == SimboloTipo.LIST_BOOLEANO || t == SimboloTipo.LIST_CHAR || t == SimboloTipo.LIST_NUMERO;
        }

        public static SimboloTipo ListOf(SimboloTipo t)
        {
            switch (t)
            {
                case SimboloTipo.NUMERO:
                    return SimboloTipo.LIST_NUMERO;
                case SimboloTipo.CHAR:
                    return SimboloTipo.LIST_CHAR;
                case SimboloTipo.BOOLEANO:
                    return SimboloTipo.LIST_BOOLEANO;
            }
            return SimboloTipo.ERROR;
        }

        public static SimboloTipo UnitOf(SimboloTipo t)
        {
            switch (t)
            {
                case SimboloTipo.LIST_NUMERO:
                    return SimboloTipo.NUMERO;
                case SimboloTipo.LIST_CHAR:
                    return SimboloTipo.CHAR;
                case SimboloTipo.LIST_BOOLEANO:
                    return SimboloTipo.BOOLEANO;
            }
            return SimboloTipo.ERROR;
        }

    }
}
