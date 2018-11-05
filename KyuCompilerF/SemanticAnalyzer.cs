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
                Dictionary<string, Simbolo<Object>> simbolosHijos = new Dictionary<string, Simbolo<Object>>();
                if (nodo.Hijos != null)
                {
                    
                    foreach (Nodo hijo in nodo.Hijos)
                    {
                        Evaluar(hijo);
                        simbolosHijos.Add(hijo.Contenido, hijo.Symbol);
                    }
                    nodo.Produccion.reglaAtributo(simbolosHijos);
                }
                nodo.FueEvaluado = true;
            }
        }
    }
}
