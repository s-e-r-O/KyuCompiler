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
        public void Evaluar(Nodo nodo)
        {
            if (!nodo.FueEvaluado)
            {
                Dictionary<string, List<Simbolo>> simbolosHijos = new Dictionary<string, List<Simbolo>>();
                simbolosHijos.Add(nodo.Contenido, new List<Simbolo>() { nodo.Symbol });
                if (nodo.Hijos != null)
                {
                    foreach (Nodo hijo in nodo.Hijos)
                    {
                        Evaluar(hijo);
                        if (simbolosHijos.ContainsKey(nodo.Contenido))
                        {
                            simbolosHijos.Add(hijo.Contenido, new List<Simbolo>() { hijo.Symbol });
                        }
                        else
                        {
                            simbolosHijos[hijo.Contenido].Add(hijo.Symbol);
                        }
                    }
                    nodo.ProduccionUsada.reglaAtributo(simbolosHijos);
                }
                nodo.FueEvaluado = true;
            }
        }
    }
}
