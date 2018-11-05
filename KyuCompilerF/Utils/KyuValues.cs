using KyuCompilerF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Utils
{
    public class KyuValues
    {
        private static Gramatica gramatica;
        public static Gramatica Gramatica
        {
            get
            {
                if (gramatica == null)
                {
                    gramatica = new Gramatica()
                    {
                        Produccciones = new List<Produccion>
                        {
                            new Produccion('S', "kyu# \n { \n M }"),
                            new Produccion('M', "I \n J"),
                            new Produccion('J', "M"),
                            new Produccion('J', Produccion.EPSILON, d => d["J"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('I', "D"),
                            new Produccion('I', "H"),
                            new Produccion('I', "G"),
                            new Produccion('I', "L"),
                            new Produccion('I', "stop"),
                            new Produccion('I', "return E"),
                            new Produccion('D', "i K"),
                            new Produccion('D', "function i P is \n M done"),
                            new Produccion('K', "is B", (d) => { d["K"][0].Tipo = d["B"][0].Tipo; d["K"][0].Valor = d["B"][0].Valor; }),
                            new Produccion('K', "R", (d) => { d["K"][0].Tipo = d["R"][0].Tipo; }),
                            new Produccion('B', "E", (d) => { d["B"][0].Tipo = d["E"][0].Tipo; d["B"][0].Valor = d["E"][0].Valor; }),
                            new Produccion('B', "[ X ]"),
                            new Produccion('X', "E Z"),
                            new Produccion('X', Produccion.EPSILON, d => d["X"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('Z', ", E Z"),
                            new Produccion('Z', Produccion.EPSILON, d => d["Z"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('H', "change A changed", d => d["H"][0].Tipo = d["A"][0].Tipo),
                            new Produccion('A', "i N \n", (d) =>{
                                if (d["N"][0].Tipo != d["i"][0].Tipo) {
                                    d["A"][0].Tipo = SimboloTipo.ERROR;
                                }
                            }),
                            new Produccion('N', ", A E", (d) => { d["N"][0].Tipo = d["E"][0].Tipo; d["N"][0].Valor = d["E"][0].Valor; }),
                            new Produccion('N', "\n E", (d) => { d["N"][0].Tipo = d["E"][0].Tipo; d["N"][0].Valor = d["E"][0].Valor; }),
                            new Produccion('P', "i Q", (d) => {
                                if (d["Q"][0].Tipo == SimboloTipo.ERROR)
                                {
                                    d["P"][0].Tipo = SimboloTipo.ERROR;
                                } else
                                {
                                    d["P"][0].Nargs = d["Q"][0].Nargs + 1;
                                }
                            }),
                            new Produccion('P', Produccion.EPSILON, d => d["P"][0].Nargs = 0),
                            new Produccion('Q', ", i Q", (d) => {
                                if (d["Q"][1].Tipo == SimboloTipo.ERROR)
                                {
                                    d["Q"][0].Tipo = SimboloTipo.ERROR;
                                } else
                                {
                                    d["Q"][0].Nargs = d["Q"][1].Nargs + 1;
                                }
                            }),
                            new Produccion('Q', Produccion.EPSILON, d => d["Q"][0].Nargs = 0),
                            new Produccion('R', "E U", (d) => {
                                if (d["E"][0].Tipo == SimboloTipo.ERROR || d["U"][0].Tipo == SimboloTipo.ERROR)
                                {
                                    d["R"][0].Tipo = SimboloTipo.ERROR;
                                } else
                                {
                                    d["R"][0].Nargs = d["U"][0].Nargs + 1;
                                }
                            }),
                            new Produccion('R', Produccion.EPSILON, d => d["R"][0].Nargs = 0),
                            new Produccion('U', ", E U", (d) => {
                                if (d["E"][0].Tipo == SimboloTipo.ERROR || d["U"][1].Tipo == SimboloTipo.ERROR)
                                {
                                    d["U"][0].Tipo = SimboloTipo.ERROR;
                                } else
                                {
                                    d["U"][0].Nargs = d["U"][1].Nargs + 1;
                                }
                            }),
                            new Produccion('U', Produccion.EPSILON, d => d["U"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('L', "forevery i in i \n M done", (d) => {
                                
                            }),
                            new Produccion('L', "forever \n M done", d => d["L"][0].Tipo = d["M"][0].Tipo),
                            new Produccion('G', "given E \n M V", (d) => {
                                if (d["E"][0].Tipo != SimboloTipo.BOOLEANO)
                                {
                                    d["G"][0].Tipo = SimboloTipo.ERROR;
                                } else if (d["E"][0].Tipo == SimboloTipo.ERROR || d["M"][0].Tipo == SimboloTipo.ERROR || d["V"][0].Tipo == SimboloTipo.ERROR)
                                {
                                    d["G"][0].Tipo = SimboloTipo.ERROR;
                                }
                            }),
                            new Produccion('V', "done", d => d["V"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('V', "otherwise \n M done", d => d["V"][0].Tipo = d["M"][0].Tipo),
                            new Produccion('E', "C W", (d) => {
                                if (d["W"][0].Tipo == SimboloTipo.EMPTY)
                                {
                                    d["E"][0].Tipo = d["C"][0].Tipo;
                                    d["E"][0].Valor = d["C"][0].Valor;
                                } else if (d["C"][0].Tipo == SimboloTipo.BOOLEANO && d["W"][0].Tipo != SimboloTipo.ERROR)
                                {
                                    d["E"][0].Tipo = d["C"][0].Tipo;
                                    switch (d["W"][0].Operador)
                                    {
                                        case "&&":
                                            d["E"][0].Valor = (bool)d["C"][0].Valor && (bool)d["W"][0].Valor;
                                            break;
                                        case "||":
                                            d["E"][0].Valor = (bool)d["C"][0].Valor || (bool)d["W"][0].Valor;
                                            break;
                                    }
                                } else
                                {
                                    d["E"][0].Tipo = SimboloTipo.ERROR;
                                }
                            }),
                            new Produccion('W', "b E", (d) => {
                                if (d["E"][0].Tipo != SimboloTipo.BOOLEANO) {
                                    d["W"][0].Tipo = SimboloTipo.ERROR;
                                } else
                                {
                                    d["W"][0].Tipo = d["E"][0].Tipo;
                                    d["W"][0].Valor = d["E"][0].Valor;
                                    d["W"][0].Operador = (string) d["b"][0].Valor;
                                }
                            }),
                            new Produccion('W', Produccion.EPSILON, d => d["W"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('C', "O Y", (d) => {
                                if (d["Y"][0].Tipo == SimboloTipo.EMPTY)
                                {
                                    d["C"][0].Tipo = d["O"][0].Tipo;
                                    d["C"][0].Valor = d["O"][0].Valor;
                                } else if (d["O"][0].Tipo != d["Y"][0].Tipo) {
                                    d["C"][0].Tipo = SimboloTipo.ERROR;
                                } else if (d["Y"][0].Operador == "==") {
                                    d["C"][0].Tipo = SimboloTipo.BOOLEANO;
                                    d["C"][0].Valor = d["O"][0].Valor == d["Y"][0].Valor;
                                } else if (d["O"][0].Tipo == SimboloTipo.NUMERO || d["O"][0].Tipo == SimboloTipo.CHAR){
                                    d["C"][0].Tipo = SimboloTipo.BOOLEANO;
                                    switch(d["Y"][0].Operador){
                                        case ">":
                                            d["C"][0].Valor = (float) d["O"][0].Valor > (float) d["Y"][0].Valor;
                                            break;
                                        case ">=":
                                            d["C"][0].Valor = (float) d["O"][0].Valor >= (float) d["Y"][0].Valor;
                                            break;
                                        case "<":
                                            d["C"][0].Valor = (float) d["O"][0].Valor < (float) d["Y"][0].Valor;
                                            break;
                                        case "<=":
                                            d["C"][0].Valor = (float) d["O"][0].Valor <= (float) d["Y"][0].Valor;
                                            break;
                                    }
                                } else
                                {
                                    d["C"][0].Tipo = SimboloTipo.ERROR;
                                }
                            }),
                            new Produccion('C', "~ ( C )", (d) => {
                                if (d["C"][1].Tipo != SimboloTipo.BOOLEANO) {
                                    d["C"][0].Tipo = SimboloTipo.ERROR;
                                } else
                                {
                                    d["C"][0].Valor = !(bool) d["C"][1].Valor;
                                    d["C"][0].Tipo = d["C"][1].Tipo;
                                }
                            }),
                            new Produccion('Y', "x O", (d) => {
                                d["Y"][0].Tipo = d["O"][0].Tipo;
                                d["Y"][0].Valor = d["O"][0].Valor;
                                d["Y"][0].Operador = (string) d["x"][0].Valor;
                            }),
                            new Produccion('Y', Produccion.EPSILON, d => d["Y"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('O', "z O , O"),
                            new Produccion('O', "T", (d) => { d["O"][0].Tipo = d["T"][0].Tipo; d["O"][0].Valor = d["T"][0].Valor; }),
                            new Produccion('T', "i"),
                            new Produccion('T', "v", (d) => { d["T"][0].Tipo = d["v"][0].Tipo; d["T"][0].Valor = d["v"][0].Valor; })
                        }
                    };
                }
                return gramatica;
            }
        }
    }
}
