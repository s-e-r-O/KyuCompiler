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
                            new Produccion('J', Produccion.EPSILON),
                            new Produccion('I', "D"),
                            new Produccion('I', "H"),
                            new Produccion('I', "G"),
                            new Produccion('I', "L"),
                            new Produccion('I', "stop"),
                            new Produccion('I', "return E"),
                            new Produccion('D', "i K", (d) => {
                                if (!TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id))
                                {
                                    if (d["K"][0].Tipo == SimboloTipo.FUNCION){
                                        SetError(d, "D", 0, "Llamar a una funcion no inicializada"); // Llamar a una funcion no inicializada
                                    } else
                                    {
                                        Assign(d, "i", 0, "K", 0);
                                        TablaSimbolo.Tabla.Update(d["i"][0].Id, d["i"][0]);
                                    }
                                } 
                                else
                                {
                                    d["i"][0] = TablaSimbolo.Tabla.Get(d["i"][0].Id);
                                    if (d["i"][0].Tipo == SimboloTipo.FUNCION)
                                    {
                                        if (d["K"][0].Tipo != SimboloTipo.FUNCION) {
                                            SetError(d, "D", 0, "Redeclarar una funcion como variable"); // Redeclarar una funcion como variable
                                        }
                                        else if (d["i"][0].Nargs != d["K"][0].Nargs)
                                        {
                                            SetError(d, "D", 0, "Numero de argumentos no es el correcto"); // Numero de argumentos no es el correcto
                                        }
                                    } else
                                    {
                                        SetError(d, "D", 0, "Redeclarar una variable"); // Redeclarar una variable
                                    }
                                }
                            }),
                            new Produccion('D', "function i P is \n M done", (d) => {
                                if (!TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id))
                                {
                                    AssignType(d, "i", 0, "P", 0);
                                    d["i"][0].Nargs = d["P"][0].Nargs;
                                    TablaSimbolo.Tabla.Update(d["i"][0].Id, d["i"][0]);
                                } else
                                {
                                    SetError(d, "D", 0, "Redeclarar una funcion"); // Redeclarar una funcion
                                }
                            }),
                            new Produccion('K', "is B", d => Assign(d, "K", 0, "B", 0)),
                            new Produccion('K', "R", d => {
                                AssignType(d, "K", 0, "R", 0);
                                d["K"][0].Nargs = d["R"][0].Nargs;
                            }),
                            new Produccion('B', "E", d => Assign(d, "B", 0, "E", 0)),
                            new Produccion('B', "[ X ]", d => d["B"][0].Tipo = Simbolo.ListOf(d["X"][0].Tipo)),
                            new Produccion('X', "E Z", (d) => {
                                if (d["Z"][0].Tipo != SimboloTipo.EMPTY && d["E"][0].Tipo != d["Z"][0].Tipo){
                                    SetError(d, "X", 0, "Conflicto de tipos"); // Listas de distinto tipo
                                } else
                                {
                                    AssignType(d, "X", 0, "E", 0);
                                }
                            }),
                            new Produccion('X', Produccion.EPSILON, d => d["X"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('Z', ", E Z", (d) => {
                                if (d["E"][0].Tipo != d["Z"][1].Tipo){
                                    SetError(d, "Z", 0, "Confilcto de tipos"); // Listas de distinto tipo
                                } else
                                {
                                    AssignType(d, "Z", 0, "E", 0);
                                }
                            }),
                            new Produccion('Z', Produccion.EPSILON, d => d["Z"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('H', "change A changed", d => AssignType(d, "H", 0, "A", 0)),
                            new Produccion('A', "i N \n", (d) =>{
                                if (TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id)){
                                    d["i"][0] = TablaSimbolo.Tabla.Get(d["i"][0].Id);
                                    if (d["N"][0].Tipo != d["i"][0].Tipo) {
                                        SetError(d, "A", 0, "Conflicto de tipos"); // Tipos no concuerdan
                                    } else {
                                        d["i"][0].Valor = d["N"][0].Valor;
                                        TablaSimbolo.Tabla.Update(d["i"][0].Id, d["i"][0]);
                                    }
                                } else {
                                    SetError(d, "A", 0, "Variable no declarada"); // Uso de varibale no declarada
                                }
                            }),
                            new Produccion('N', ", A E", d => Assign(d, "N", 0, "E", 0)),
                            new Produccion('N', "\n E", d => Assign(d, "N", 0, "E", 0)),
                            new Produccion('P', "i Q", (d) => {
                                d["P"][0].Tipo = SimboloTipo.FUNCION;
                                d["P"][0].Nargs = d["Q"][0].Nargs + 1;
                            }),
                            new Produccion('P', Produccion.EPSILON, (d) => {
                                d["P"][0].Nargs = 0;
                                d["P"][0].Tipo = SimboloTipo.FUNCION;
                            }),
                            new Produccion('Q', ", i Q", (d) => {
                                    d["Q"][0].Nargs = d["Q"][1].Nargs + 1;
                            }),
                            new Produccion('Q', Produccion.EPSILON, (d) => {
                                d["Q"][0].Nargs = 0;
                                d["Q"][0].Tipo = SimboloTipo.FUNCION;
                            }),
                            new Produccion('R', "E U", (d) => {
                                    d["R"][0].Tipo = SimboloTipo.FUNCION;
                                    d["R"][0].Nargs = d["U"][0].Nargs + 1;
                            }),
                            new Produccion('R', Produccion.EPSILON, (d) => {
                                    d["R"][0].Tipo = SimboloTipo.FUNCION;
                                    d["R"][0].Nargs = 0;
                            }),
                            new Produccion('U', ", E U", (d) => {
                                d["U"][0].Nargs = d["U"][1].Nargs + 1;
                            }),
                            new Produccion('U', Produccion.EPSILON, d => d["U"][0].Nargs = 0),
                            new Produccion('L', "forevery i in i \n M done", (d) => {
                                if (!TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id)){
                                    if (TablaSimbolo.Tabla.IsInitialized(d["i"][1].Id)){
                                        d["i"][1] = TablaSimbolo.Tabla.Get(d["i"][1].Id);
                                        if (Simbolo.IsList(d["i"][1].Tipo)){
                                            d["i"][0].Tipo = Simbolo.UnitOf(d["i"][1].Tipo);
                                            TablaSimbolo.Tabla.Update(d["i"][0].Id, d["i"][0]);
                                        } else
                                        {
                                            SetError(d, "L", 0, "No es lista"); // No es lista
                                        }
                                    } else
                                    {
                                        SetError(d, "L", 0, "Variable no declarada"); // Variable no declarada
                                    }
                                } else
                                {
                                    SetError(d, "L", 0, "Nombre de variable ya utilizado"); // Nombre de variable ya utilizado 
                                }
                            }),
                            new Produccion('L', "forever \n M done"),
                            new Produccion('G', "given E \n M V", (d) => {
                                if (d["E"][0].Tipo != SimboloTipo.BOOLEANO)
                                {
                                    SetError(d, "G", 0, "Condicion no valida"); // Condicion no valida 
                                }   
                            }),
                            new Produccion('V', "done"),
                            new Produccion('V', "otherwise \n M done"),
                            new Produccion('E', "C W", (d) => {
                                if (d["W"][0].Tipo == SimboloTipo.EMPTY)
                                {
                                    Assign(d, "E", 0, "C", 0);
                                } else if (d["C"][0].Tipo == SimboloTipo.BOOLEANO && d["W"][0].Tipo != SimboloTipo.BOOLEANO)
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
                                    SetError(d, "E", 0, "Conflicto de tipos"); // Conflicto de tipos
                                }
                            }),
                            new Produccion('W', "b E", (d) => {
                                if (d["E"][0].Tipo == SimboloTipo.BOOLEANO)
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
                                    SetError(d, "C", 0, "Conflicto de tipos"); // Conflicto de tipos
                                } else if (d["Y"][0].Operador == "==") {
                                    d["C"][0].Tipo = SimboloTipo.BOOLEANO;
                                    d["C"][0].Valor = d["O"][0].Valor == d["Y"][0].Valor;
                                } else if (d["O"][0].Tipo == SimboloTipo.NUMERO){
                                    d["C"][0].Tipo = SimboloTipo.BOOLEANO;
                                    switch(d["Y"][0].Operador){
                                        case ">":
                                            d["C"][0].Valor = (decimal) d["O"][0].Valor > (decimal) d["Y"][0].Valor;
                                            break;
                                        case ">=":
                                            d["C"][0].Valor = (decimal) d["O"][0].Valor >= (decimal) d["Y"][0].Valor;
                                            break;
                                        case "<":
                                            d["C"][0].Valor = (decimal) d["O"][0].Valor < (decimal) d["Y"][0].Valor;
                                            break;
                                        case "<=":
                                            d["C"][0].Valor = (decimal) d["O"][0].Valor <= (decimal) d["Y"][0].Valor;
                                            break;
                                    }
                                } else
                                {
                                    SetError(d, "C", 0, "Conflicto de tipos"); // Conflicto de tipos
                                }
                            }),
                            new Produccion('C', "~ ( C )", (d) => {
                                if (d["C"][1].Tipo != SimboloTipo.BOOLEANO) {
                                    SetError(d, "C", 0, "Conflicto de tipos"); // Conflicto de tipos
                                } else
                                {
                                    AssignType(d, "C", 0, "C", 1);
                                    d["C"][0].Valor = !(bool) d["C"][1].Valor;
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
                                            d["O"][0].Valor = (decimal) d["O"][1].Valor + (decimal) d["O"][2].Valor;
                                            break;
                                        case "-":
                                            d["O"][0].Valor = (decimal) d["O"][1].Valor - (decimal) d["O"][2].Valor;
                                            break;
                                        case "*":
                                            d["O"][0].Valor = (decimal) d["O"][1].Valor * (decimal) d["O"][2].Valor;
                                            break;
                                        case "/":
                                            d["O"][0].Valor = (decimal) d["O"][1].Valor / (decimal) d["O"][2].Valor;
                                            break;
                                        case "^":
                                            d["O"][0].Valor = Math.Pow((double) d["O"][1].Valor,(double) d["O"][2].Valor);
                                            break;
                                        case "%":
                                            d["O"][0].Valor = Math.Floor((double) d["O"][1].Valor) % (int) Math.Floor((double) d["O"][2].Valor);
                                            break;
                                    }
                                } else if ((string) d["z"][0].Valor == "+") {
                                    if (Simbolo.IsList(d["O"][1].Tipo)){
                                        AssignType(d, "O", 0, "O", 1);
                                        if (Simbolo.IsList(d["O"][2].Tipo)){
                                            if (d["O"][1].Tipo == d["O"][2].Tipo){
                                                // Concatenate
                                            } else {
                                                SetError(d, "O", 0, "Conflicto de tipos"); // Conflicto de tipo
                                            }
                                        } else if (Simbolo.UnitOf(d["O"][1].Tipo) == d["O"][2].Tipo) {
                                            // Add
                                        } else {
                                            SetError(d, "O", 0, "Conflicto de tipos"); // Conflicto de tipo
                                        }
                                    } else if (Simbolo.IsList(d["O"][2].Tipo)){
                                        AssignType(d, "O", 0, "O", 2);
                                        if (Simbolo.IsList(d["O"][1].Tipo)){
                                            if (d["O"][1].Tipo == d["O"][2].Tipo){
                                                // Concatenate
                                            } else {
                                                SetError(d, "O", 0, "Conflicto de tipos");  // Conflicto de tipo
                                            }
                                        } else if (Simbolo.UnitOf(d["O"][2].Tipo) == d["O"][1].Tipo) {
                                            // Add
                                        } else {
                                            SetError(d, "O", 0, "Conflicto de tipos");  // Conflicto de tipo
                                        }
                                    }
                                } else
                                {
                                    SetError(d, "O", 0, "Conflicto de tipos");  // Conflicto de tipo
                                }
                            }),
                            new Produccion('O', "T", d => Assign(d, "O", 0, "T", 0)),
                            new Produccion('T', "i", (d) => {
                                if (!TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id))
                                {
                                    SetError(d, "T", 0, "Variable no declarada"); // Variable no declarada
                                } else
                                {
                                    d["i"][0] = TablaSimbolo.Tabla.Get(d["i"][0].Id);
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

        static void SetError(Dictionary<string, List<Simbolo>> d, string target, int targetIndex, string desc)
        {
            Console.WriteLine("ERROR: " + desc);
            d[target][targetIndex].Tipo = SimboloTipo.ERROR;
        }
    }
}
