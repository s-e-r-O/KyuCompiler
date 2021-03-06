﻿using KyuCompilerF.Exceptions;
using KyuCompilerF.Models;
using System;
using System.Collections;
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
                                        SetError(d, "D", 0, KyuSemanticException.Type.FUNCTION_NOT_INITIALIZED, d["i"][0].Id); // Llamar a una funcion no inicializada
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
                                            SetError(d, "D", 0, KyuSemanticException.Type.FUNCTION_ALREADY_DECLARED, d["i"][0].Id); // Redeclarar una funcion como variable
                                        }
                                        else if (d["i"][0].Nargs != d["K"][0].Nargs)
                                        {
                                            SetError(d, "D", 0, KyuSemanticException.Type.NUMBER_OF_ARGUMENTS_INVALID, d["i"][0].Id + " has " + d["i"][0].Nargs + " args, not " + d["K"][0].Nargs ); // Numero de argumentos no es el correcto
                                        }
                                    } else
                                    {
                                        SetError(d, "D", 0, KyuSemanticException.Type.VARIABLE_ALREADY_DECLARED, d["i"][0].Id); // Redeclarar una variable
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
                                    SetError(d, "D", 0, KyuSemanticException.Type.FUNCTION_ALREADY_DECLARED, d["i"][0].Id); // Redeclarar una funcion
                                }
                            }),
                            new Produccion('K', "is B", d => Assign(d, "K", 0, "B", 0)),
                            new Produccion('K', "R", d => {
                                AssignType(d, "K", 0, "R", 0);
                                d["K"][0].Nargs = d["R"][0].Nargs;
                            }),
                            new Produccion('B', "E", d => Assign(d, "B", 0, "E", 0)),
                            new Produccion('B', "[ X ]", (d) => {
                                d["B"][0].Tipo = Simbolo.ListOf(d["X"][0].Tipo);
                                d["B"][0].Valor = d["X"][0].Valor;
                            }),
                            new Produccion('X', "E Z", (d) => {
                                if (d["Z"][0].Tipo != SimboloTipo.EMPTY){
                                    if (d["E"][0].Tipo != d["Z"][0].Tipo){
                                        SetError(d, "X", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["E"][0].Tipo.ToString() + " - " + d["Z"][0].Tipo.ToString()); // Listas de distinto tipo
                                    } else
                                    {
                                        AssignType(d, "X", 0, "E", 0);
                                        d["X"][0].Valor = new List<Object>();
                                        ((IList)d["X"][0].Valor).Add(d["E"][0].Valor);
                                        foreach (Object o in ((IList)d["Z"][0].Valor))
                                        {
                                            ((IList)d["X"][0].Valor).Add(o);
                                        }
                                    }
                                } else
                                {
                                    AssignType(d, "X", 0, "E", 0);
                                    d["X"][0].Valor = new List<Object>();
                                    ((IList)d["X"][0].Valor).Add(d["E"][0].Valor);
                                }
                            }),
                            new Produccion('X', Produccion.EPSILON, d => {
                                d["X"][0].Tipo = SimboloTipo.EMPTY;
                                d["X"][0].Valor = new List<Object>();
                            }),
                            new Produccion('Z', ", E Z", (d) => {
                                if (d["Z"][1].Tipo != SimboloTipo.EMPTY){
                                    if (d["E"][0].Tipo != d["Z"][1].Tipo){
                                        SetError(d, "Z", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["E"][0].Tipo.ToString() + " - " + d["Z"][1].Tipo.ToString()); // Listas de distinto tipo
                                    } else
                                    {
                                        AssignType(d, "Z", 0, "E", 0);
                                        d["Z"][0].Valor = new List<Object>();
                                        ((IList)d["Z"][0].Valor).Add(d["E"][0].Valor);
                                        foreach (Object o in ((IList)d["Z"][1].Valor))
                                        {
                                            ((IList)d["Z"][0].Valor).Add(o);
                                        }
                                    }
                                } else
                                {
                                    AssignType(d, "Z", 0, "E", 0);
                                    d["Z"][0].Valor = new List<Object>();
                                    ((IList)d["Z"][0].Valor).Add(d["E"][0].Valor);
                                }
                            }),
                            new Produccion('Z', Produccion.EPSILON, d => d["Z"][0].Tipo = SimboloTipo.EMPTY),
                            new Produccion('H', "change A changed"),
                            new Produccion('A', "i N \n", (d) =>{
                                if (TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id)){
                                    d["i"][0] = TablaSimbolo.Tabla.Get(d["i"][0].Id);
                                    if (d["N"][0].Tipo != d["i"][0].Tipo) {
                                        SetError(d, "A", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["i"][0].Id + ": " + d["i"][0].Tipo.ToString() + " - " + d["N"][0].Tipo.ToString()); // Tipos no concuerdan
                                    } else {
                                        d["i"][0].Valor = d["N"][0].Valor;
                                        TablaSimbolo.Tabla.Update(d["i"][0].Id, d["i"][0]);
                                    }
                                } else {
                                    SetError(d, "A", 0, KyuSemanticException.Type.VARIABLE_NOT_INITIALIZED, d["i"][0].Id); // Uso de varibale no declarada
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
                                            if (((IList)d["i"][1].Valor).Count > 0)
                                            {
                                                d["i"][0].Valor = ((IList) d["i"][1].Valor)[0];
                                            }
                                            TablaSimbolo.Tabla.Update(d["i"][0].Id, d["i"][0]);
                                        } else
                                        {
                                            SetError(d, "L", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["i"][1].Id + " : "+ d["1"][0].Tipo.ToString()+ "  is not a list"); // No es lista
                                        }
                                    } else
                                    {
                                        SetError(d, "L", 0, KyuSemanticException.Type.VARIABLE_NOT_INITIALIZED, d["i"][1].Id); // Variable no declarada
                                    }
                                } else
                                {
                                    SetError(d, "L", 0, KyuSemanticException.Type.VARIABLE_ALREADY_DECLARED, d["i"][0].Id); // Nombre de variable ya utilizado 
                                }
                            }),
                            new Produccion('L', "forever \n M done"),
                            new Produccion('G', "given E \n M V", (d) => {
                                if (d["E"][0].Tipo != SimboloTipo.BOOLEANO)
                                {
                                    SetError(d, "G", 0, KyuSemanticException.Type.INVALID_CONDITION, d["E"][0].Tipo.ToString()); // Condicion no valida 
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
                                    SetError(d, "E", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["C"][0].Tipo.ToString() + " - " + d["W"][0].Tipo.ToString()); // Conflicto de tipos
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
                                    SetError(d, "C", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["O"][0].Tipo.ToString() + " - " + d["Y"][0].Tipo.ToString()); // Conflicto de tipos
                                } else if (d["Y"][0].Operador == "==") {
                                    d["C"][0].Tipo = SimboloTipo.BOOLEANO;
                                    d["C"][0].Valor = d["O"][0].Valor == d["Y"][0].Valor;
                                } else if (d["O"][0].Tipo == SimboloTipo.NUMERO){
                                    d["C"][0].Tipo = SimboloTipo.BOOLEANO;
                                    switch(d["Y"][0].Operador){
                                        case ">":
                                            d["C"][0].Valor = Convert.ToDecimal(d["O"][0].Valor) > Convert.ToDecimal(d["Y"][0].Valor);
                                            break;
                                        case ">=":
                                            d["C"][0].Valor = Convert.ToDecimal(d["O"][0].Valor) >= Convert.ToDecimal(d["Y"][0].Valor);
                                            break;
                                        case "<":
                                            d["C"][0].Valor = Convert.ToDecimal(d["O"][0].Valor) < Convert.ToDecimal(d["Y"][0].Valor);
                                            break;
                                        case "<=":
                                            d["C"][0].Valor = Convert.ToDecimal(d["O"][0].Valor) <= Convert.ToDecimal(d["Y"][0].Valor);
                                            break;
                                    }
                                } else
                                {
                                    SetError(d, "C", 0, KyuSemanticException.Type.CONFLICTING_TYPES, "Only numbers can be compared"); // Conflicto de tipos
                                }
                            }),
                            new Produccion('C', "~ ( C )", (d) => {
                                if (d["C"][1].Tipo != SimboloTipo.BOOLEANO) {
                                    SetError(d, "C", 0, KyuSemanticException.Type.CONFLICTING_TYPES, "Can't get negation of a non boolean type"); // Conflicto de tipos
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
                                            d["O"][0].Valor = Math.Pow(Convert.ToDouble(d["O"][1].Valor),Convert.ToDouble(d["O"][2].Valor));
                                            break;
                                        case "%":
                                            d["O"][0].Valor = Math.Floor(Convert.ToDouble(d["O"][1].Valor)) % Math.Floor(Convert.ToDouble(d["O"][2].Valor));
                                            break;
                                    }
                                } else if ((string) d["z"][0].Valor == "+") {
                                    if (Simbolo.IsList(d["O"][1].Tipo)){
                                        AssignType(d, "O", 0, "O", 1);
                                        d["O"][0].Valor = new List<Object>();
                                        if (Simbolo.IsList(d["O"][2].Tipo)){
                                            if (d["O"][1].Tipo == d["O"][2].Tipo || Simbolo.isEmptyList(d["O"][1].Tipo) || Simbolo.isEmptyList(d["O"][2].Tipo)){
                                                foreach (Object o in ((IList)d["O"][1].Valor))
                                                {
                                                    ((IList)d["O"][0].Valor).Add(o);
                                                }
                                                foreach (Object o in ((IList)d["O"][2].Valor))
                                                {
                                                    ((IList)d["O"][0].Valor).Add(o);
                                                }
                                                //tener en cuenta que O1 puede ser vacia
                                                if (Simbolo.isEmptyList(d["O"][1].Tipo))
                                                {
                                                    AssignType(d, "O", 0, "O", 2);
                                                }
                                            }
                                            else {
                                                SetError(d, "O", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["O"][1].Tipo.ToString() + " - " + d["O"][2].Tipo.ToString()); // Conflicto de tipo
                                            }
                                        } else if (Simbolo.UnitOf(d["O"][1].Tipo) == d["O"][2].Tipo || Simbolo.isEmptyList(d["O"][1].Tipo)) {
                                            foreach (Object o in ((IList)d["O"][1].Valor))
                                            {
                                                ((IList)d["O"][0].Valor).Add(o);
                                            }
                                            ((IList)d["O"][0].Valor).Add(d["O"][2].Valor);
                                            //tener en cuenta que O1 puede ser vacia
                                            if (Simbolo.isEmptyList(d["O"][1].Tipo))
                                            {
                                                d["O"][0].Tipo = Simbolo.ListOf(d["O"][2].Tipo);
                                            }
                                        } else {
                                            SetError(d, "O", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["O"][1].Tipo.ToString() + " - " + d["O"][2].Tipo.ToString()); // Conflicto de tipo
                                        }
                                    } else if (Simbolo.IsList(d["O"][2].Tipo)){
                                        AssignType(d, "O", 0, "O", 2);
                                        d["O"][0].Valor = new List<Object>();
                                        if (Simbolo.UnitOf(d["O"][2].Tipo) == d["O"][1].Tipo ||Simbolo.isEmptyList(d["O"][2].Tipo)) {
                                            ((IList)d["O"][0].Valor).Add(d["O"][1].Valor);
                                            foreach (Object o in ((IList)d["O"][2].Valor))
                                            {
                                                ((IList)d["O"][0].Valor).Add(o);
                                            }
                                            //tener en cuenta que O2 puede ser vacia
                                            if (Simbolo.isEmptyList(d["O"][2].Tipo))
                                            {
                                                d["O"][0].Tipo = Simbolo.ListOf(d["O"][1].Tipo);
                                            }
                                        } else {
                                            SetError(d, "O", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["O"][1].Tipo.ToString() + " - " + d["O"][2].Tipo.ToString());  // Conflicto de tipo
                                        }
                                    }
                                    else
                                    {
                                        SetError(d, "O", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["O"][1].Tipo.ToString() + " - " + d["O"][2].Tipo.ToString());  // Conflicto de tipo
                                    }
                                } else
                                {
                                    SetError(d, "O", 0, KyuSemanticException.Type.CONFLICTING_TYPES, d["O"][1].Tipo.ToString() + " - " + d["O"][2].Tipo.ToString());  // Conflicto de tipo
                                }
                            }),
                            new Produccion('O', "T", d => Assign(d, "O", 0, "T", 0)),
                            new Produccion('T', "i", (d) => {
                                if (!TablaSimbolo.Tabla.IsInitialized(d["i"][0].Id))
                                {
                                    SetError(d, "T", 0, KyuSemanticException.Type.VARIABLE_NOT_INITIALIZED, d["i"][0].Id); // Variable no declarada
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

        static void SetError(Dictionary<string, List<Simbolo>> d, string target, int targetIndex, KyuSemanticException.Type tipo, string desc)
        {
            d[target][targetIndex].Tipo = SimboloTipo.ERROR;
            throw new KyuSemanticException(tipo, desc); 
        }
    }
}
