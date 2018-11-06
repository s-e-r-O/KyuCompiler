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
            EvaluarConLineas(nodo);
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        private void EvaluarConLineas(Nodo nodo)
        {
            if (!nodo.FueEvaluado)
            {
                Dictionary<string, List<Simbolo>> simbolosHijos = new Dictionary<string, List<Simbolo>>();
                simbolosHijos.Add(nodo.Contenido, new List<Simbolo>() { nodo.Symbol });
                if (nodo.Hijos != null)
                {
                    foreach (Nodo hijo in nodo.Hijos)
                    {
                        EvaluarConLineas(hijo);
                        if (!simbolosHijos.ContainsKey(hijo.Contenido))
                        {
                            simbolosHijos.Add(hijo.Contenido, new List<Simbolo>() { hijo.Symbol });
                        }
                        else
                        {
                            simbolosHijos[hijo.Contenido].Add(hijo.Symbol);
                        }
                    }
                    if (nodo.Hijos.Count > 0)
                    {
                        //tomar el menor numero de linea de los hijos
                        nodo.Linea = nodo.Hijos[0].Linea;
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
