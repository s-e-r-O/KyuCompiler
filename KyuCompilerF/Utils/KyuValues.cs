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
                            new Produccion('S', "kyu# \n { \n M }", d => AssignType(d, "S", 0, "M", 0)),
                            new Produccion('M', "I \n J", d => AssignType(d, "M", 0, "I", 0)),
                            new Produccion('J', "M", d => AssignType(d, "J", 0, "M", 0)),
                            new Produccion('J', Produccion.EPSILON, d => d["J"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('I', "D", d => AssignType(d, "I", 0, "D", 0)),
                            new Produccion('I', "H", d => AssignType(d, "I", 0, "H", 0)),
                            new Produccion('I', "G", d => AssignType(d, "I", 0, "G", 0)),
                            new Produccion('I', "L", d => AssignType(d, "I", 0, "L", 0)),
                            new Produccion('I', "stop", d=> d["I"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('I', "return E", d => AssignType(d, "I", 0, "E", 0)),
                            new Produccion('D', "i K", (d) => {
                                if (!TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id))
                                {
                                    Assign(d, "i", 0, "K", 0);
                                    TablaSimbolo.Tabla.Update(d["i"][0].Id, d["i"][0]);
                                } else
                                {
                                    SetError(d, "D", 0);
                                }
                            }),
                            new Produccion('D', "function i P is \n M done"),
                            new Produccion('K', "is B", d => Assign(d, "K", 0, "B", 0)),
                            new Produccion('K', "R", d => AssignType(d, "K", 0, "R", 0)),
                            new Produccion('B', "E", d => Assign(d, "B", 0, "E", 0)),
                            new Produccion('B', "[ X ]", d => d["B"][0].Tipo = Simbolo.ListOf(d["X"][0].Tipo)),
                            new Produccion('X', "E Z", (d) => {
                                if (d["E"][0].Tipo != d["Z"][0].Tipo){
                                    SetError(d, "X", 0);
                                } else
                                {
                                    AssignType(d, "X", 0, "E", 0);
                                }
                            }),
                            new Produccion('X', Produccion.EPSILON, d => d["X"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('Z', ", E Z", (d) => {
                                if (d["E"][0].Tipo != d["Z"][1].Tipo){
                                    SetError(d, "Z", 0);
                                } else
                                {
                                    AssignType(d, "Z", 0, "E", 0);
                                }
                            }),
                            new Produccion('Z', Produccion.EPSILON, d => d["Z"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('H', "change A changed", d => AssignType(d, "H", 0, "A", 0)),
                            new Produccion('A', "i N \n", (d) =>{
                                if (d["N"][0].Tipo != d["i"][0].Tipo) {
                                    SetError(d, "A", 0);
                                }
                            }),
                            new Produccion('N', ", A E", d => Assign(d, "N", 0, "E", 0)),
                            new Produccion('N', "\n E", d => Assign(d, "N", 0, "E", 0)),
                            new Produccion('P', "i Q", (d) => {
                                if (d["Q"][0].Tipo == SimboloTipo.ERROR)
                                {
                                    SetError(d, "P", 0);
                                } else
                                {
                                    d["P"][0].Nargs = d["Q"][0].Nargs + 1;
                                }
                            }),
                            new Produccion('P', Produccion.EPSILON, d => d["P"][0].Nargs = 0),
                            new Produccion('Q', ", i Q", (d) => {
                                if (d["Q"][1].Tipo == SimboloTipo.ERROR)
                                {
                                    SetError(d, "Q", 0);
                                } else
                                {
                                    d["Q"][0].Nargs = d["Q"][1].Nargs + 1;
                                }
                            }),
                            new Produccion('Q', Produccion.EPSILON, d => d["Q"][0].Nargs = 0),
                            new Produccion('R', "E U", (d) => {
                                if (d["E"][0].Tipo == SimboloTipo.ERROR || d["U"][0].Tipo == SimboloTipo.ERROR)
                                {
                                    SetError(d, "R", 0);
                                } else
                                {
                                    d["R"][0].Nargs = d["U"][0].Nargs + 1;
                                }
                            }),
                            new Produccion('R', Produccion.EPSILON, d => d["R"][0].Nargs = 0),
                            new Produccion('U', ", E U", (d) => {
                                if (d["E"][0].Tipo == SimboloTipo.ERROR || d["U"][1].Tipo == SimboloTipo.ERROR)
                                {
                                    SetError(d, "U", 0);
                                } else
                                {
                                    d["U"][0].Nargs = d["U"][1].Nargs + 1;
                                }
                            }),
                            new Produccion('U', Produccion.EPSILON, d => d["U"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('L', "forevery i in i \n M done", (d) => {
                                if (Simbolo.IsList(d["i"][1].Tipo)){
                                    d["i"][0].Tipo = Simbolo.UnitOf(d["i"][1].Tipo);
                                } else
                                {
                                    SetError(d, "L", 0);
                                }
                            }),
                            new Produccion('L', "forever \n M done", d => AssignType(d, "L", 0, "M", 0)),
                            new Produccion('G', "given E \n M V", (d) => {
                                if (d["E"][0].Tipo != SimboloTipo.BOOLEANO)
                                {
                                    SetError(d, "G", 0);
                                }
                                else if (d["E"][0].Tipo == SimboloTipo.ERROR || d["M"][0].Tipo == SimboloTipo.ERROR || d["V"][0].Tipo == SimboloTipo.ERROR)
                                {
                                    SetError(d, "G", 0);
                                }
                            }),
                            new Produccion('V', "done", d => d["V"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('V', "otherwise \n M done", d => AssignType(d, "V", 0, "M", 0)),
                            new Produccion('E', "C W", (d) => {
                                if (d["W"][0].Tipo == SimboloTipo.EMPTY)
                                {
                                    Assign(d, "E", 0, "C", 0);
                                } else if (d["C"][0].Tipo == SimboloTipo.BOOLEANO && d["W"][0].Tipo != SimboloTipo.ERROR)
                                {
                                    AssignType(d, "E", 0, "C", 0);
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
                                    SetError(d, "E", 0);
                                }
                            }),
                            new Produccion('W', "b E", (d) => {
                                if (d["E"][0].Tipo != SimboloTipo.BOOLEANO) {
                                    SetError(d, "W", 0);
                                } else
                                {
                                    Assign(d, "W", 0, "E", 0);
                                    d["W"][0].Operador = (string) d["b"][0].Valor;
                                }
                            }),
                            new Produccion('W', Produccion.EPSILON, d => d["W"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('C', "O Y", (d) => {
                                if (d["Y"][0].Tipo == SimboloTipo.EMPTY) {
                                    Assign(d, "C", 0, "O", 0);
                                } else if (d["O"][0].Tipo != d["Y"][0].Tipo) {
                                    SetError(d, "C", 0);
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
                                    SetError(d, "C", 0);
                                }
                            }),
                            new Produccion('C', "~ ( C )", (d) => {
                                if (d["C"][1].Tipo != SimboloTipo.BOOLEANO) {
                                    SetError(d, "C", 0);
                                } else
                                {
                                    d["C"][0].Valor = !(bool) d["C"][1].Valor;
                                    AssignType(d, "C", 0, "C", 1);
                                }
                            }),
                            new Produccion('Y', "x O", (d) => {
                                Assign(d, "Y", 0, "O", 0);
                                d["Y"][0].Operador = (string) d["x"][0].Valor;
                            }),
                            new Produccion('Y', Produccion.EPSILON, d => d["Y"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('O', "z O , O", (d) => {
                                if (d["O"][2].Tipo == SimboloTipo.NUMERO && d["O"][1].Tipo == SimboloTipo.NUMERO){
                                    d["O"][0].Tipo = SimboloTipo.NUMERO;
                                    switch((string)d["z"][0].Valor){
                                        case "+":
                                            d["O"][0].Valor =(float) d["O"][1].Valor + (float) d["O"][2].Valor;
                                            break;
                                        case "-":
                                            d["O"][0].Valor =(float) d["O"][1].Valor - (float) d["O"][2].Valor;
                                            break;
                                        case "*":
                                            d["O"][0].Valor =(float) d["O"][1].Valor * (float) d["O"][2].Valor;
                                            break;
                                        case "/":
                                            d["O"][0].Valor =(float) d["O"][1].Valor / (float) d["O"][2].Valor;
                                            break;
                                        case "^":
                                            d["O"][0].Valor = Math.Pow((float) d["O"][1].Valor,(float) d["O"][2].Valor);
                                            break;
                                        case "%":
                                            d["O"][0].Valor =(int) d["O"][1].Valor % (int) d["O"][2].Valor;
                                            break;
                                    }
                                } else if ((string) d["z"][0].Valor == "+") {
                                    if (Simbolo.IsList(d["O"][1].Tipo)){
                                        AssignType(d, "O", 0, "O", 1);
                                        if (Simbolo.IsList(d["O"][2].Tipo)){
                                            if (d["O"][1].Tipo == d["O"][2].Tipo){
                                                // Concatenate
                                            } else {
                                                SetError(d, "O", 0);
                                            }
                                        } else if (Simbolo.UnitOf(d["O"][1].Tipo) == d["O"][2].Tipo) {
                                            // Add
                                        } else {
                                            SetError(d, "O", 0);
                                        }
                                    } else if (Simbolo.IsList(d["O"][2].Tipo)){
                                        AssignType(d, "O", 0, "O", 2);
                                        if (Simbolo.IsList(d["O"][1].Tipo)){
                                            if (d["O"][1].Tipo == d["O"][2].Tipo){
                                                // Concatenate
                                            } else {
                                                SetError(d, "O", 0);
                                            }
                                        } else if (Simbolo.UnitOf(d["O"][2].Tipo) == d["O"][1].Tipo) {
                                            // Add
                                        } else {
                                            SetError(d, "O", 0);
                                        }
                                    }
                                } else
                                {
                                    SetError(d, "O", 0);
                                }
                            }),
                            new Produccion('O', "T", d => Assign(d, "O", 0, "T", 0)),
                            new Produccion('T', "i", (d) => {
                                if (!TablaSimbolo.Tabla.IsInTable(d["i"][0].Id))
                                {
                                    SetError(d, "T", 0);
                                } else
                                {
                                    Assign(d, "T", 0, "i", 0);
                                }
                            }),
                            new Produccion('T', "v", d => Assign(d, "T", 0, "v", 0))
                        }
                    };
                }
                return gramatica;
            }
        }

        static void Assign(Dictionary<string, List<Simbolo>> d, string target, int targetIndex, string origin, int originIndex)
        {
            AssignType(d, target, targetIndex, origin, originIndex);
            d[target][targetIndex].Valor = d[origin][originIndex].Valor;
        }

        static void AssignType(Dictionary<string, List<Simbolo>> d, string target, int targetIndex, string origin, int originIndex)
        {
            d[target][targetIndex].Tipo = d[origin][originIndex].Tipo;
        }

        static void SetError(Dictionary<string, List<Simbolo>> d, string target, int targetIndex)
        {
            d[target][targetIndex].Tipo = SimboloTipo.ERROR;
        }
    }
}
