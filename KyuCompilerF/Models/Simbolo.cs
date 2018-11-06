using System;
using System.Collections;
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
        LIST_EMPTY,
        OPERADOR,
        EMPTY,
        ERROR,
        FUNCION,
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
            Nargs = -1;
        }

        public Simbolo()
        {
            this.Tipo = SimboloTipo.NULL;
            Nargs = -1;
        }

        public override string ToString()
        {
            if (!IsList(Tipo))
            {
                return Valor + " (" + Tipo + ")";
            }
            string s = "";
            IList list = (IList)Valor;
            for (int i = 0; i < list.Count; i++) {
                s += list[i].ToString();
                if (i < list.Count - 1)
                {
                    s += ", ";
                }
            }
            return "[" + s + "] (" + Tipo + ")";
        }

        public static bool IsList(SimboloTipo t)
        {
            return t == SimboloTipo.LIST_BOOLEANO || t == SimboloTipo.LIST_CHAR || t == SimboloTipo.LIST_NUMERO || t == SimboloTipo.LIST_EMPTY;
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
                case SimboloTipo.EMPTY:
                    return SimboloTipo.LIST_EMPTY;
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
                case SimboloTipo.LIST_EMPTY:
                    return SimboloTipo.EMPTY;
            }
            return SimboloTipo.ERROR;
        }

        public static bool isEmptyList(SimboloTipo t)
        {
            return t == SimboloTipo.LIST_EMPTY;
        }

    }
}
