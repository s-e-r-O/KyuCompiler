using KyuCompilerF.Exceptions;
using KyuCompilerF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF
{
    public class SemanticAnalyzer
    {
        private List<Exception> exceptions;

        public void Evaluar(Nodo nodo)
        {
            exceptions = new List<Exception>();
            Evaluar(nodo, 1);
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        private void Evaluar(Nodo nodo, int linea)
        {
            if (!nodo.FueEvaluado)
            {
                nodo.Linea = linea;
                Dictionary<string, List<Simbolo>> simbolosHijos = new Dictionary<string, List<Simbolo>>();
                simbolosHijos.Add(nodo.Contenido, new List<Simbolo>() { nodo.Symbol });
                if (nodo.Hijos != null)
                {
                    foreach (Nodo hijo in nodo.Hijos)
                    {
                        if (hijo.Contenido == "\n")
                        {
                            linea++;
                        }
                        Evaluar(hijo, linea);
                        if (!simbolosHijos.ContainsKey(hijo.Contenido))
                        {
                            simbolosHijos.Add(hijo.Contenido, new List<Simbolo>() { hijo.Symbol });
                        }
                        else
                        {
                            simbolosHijos[hijo.Contenido].Add(hijo.Symbol);
                        }
                    }
                    try
                    {
                        nodo.ProduccionUsada.reglaAtributo?.Invoke(simbolosHijos);
                    }
                    catch (KyuSemanticException e)
                    {
                        e.Linea = nodo.Linea;
                        exceptions.Add(e);
                    }
                }
                nodo.FueEvaluado = true;
            }
        }
    }
}
