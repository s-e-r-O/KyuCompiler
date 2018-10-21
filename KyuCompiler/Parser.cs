using KyuCompiler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyuCompiler
{
    class Parser
    {
        Dictionary<char, List<string>> Primeros { get; set; }
        Gramatica gramatica;
        
        public void Calcular(Gramatica g)
        {
            Primeros = new Dictionary<char, List<string>>();
            gramatica = g;
            foreach (Produccion p in gramatica.Produccciones) {
                P(p.Cabeza.ToString());
            }
            foreach (KeyValuePair<char, List<string>> p in Primeros)
            {
                Console.Write(p.Key + ": ");
                foreach (string s in p.Value)
                {
                    Console.Write("\"{0}\", ", s);
                }
                Console.WriteLine();
            }
        }

        public List<string> P(string s)
        {
            List<string> primeros = new List<string>();
            if (gramatica.EsTerminal(s))
            {
                primeros.Add(s);
            }
            else
            {
                char nt = s[0];
                if (gramatica.Produccciones.Exists(p => p.Cabeza == nt && p.Cuerpo == Produccion.EPSILON))
                {
                    primeros.Add(Produccion.EPSILON);
                }
                List<Produccion> producciones = gramatica.Produccciones.Where(p => p.Cabeza == nt && p.Cuerpo != Produccion.EPSILON).ToList();
                foreach (Produccion p in producciones)
                {
                    int n = p.Palabras.Count();
                    foreach (string a in p.Palabras)
                    {
                        List<string> primerosA;
                        if (!(a.Length == 1 && Primeros.TryGetValue(a[0], out primerosA)))
                        {
                            primerosA = P(a);
                        }
                        primeros.AddRange(primerosA);
                        if (!primerosA.Contains(Produccion.EPSILON))
                        {
                            break;
                        }
                        n--;
                    }
                    if (n <= 0) {
                        primeros.Add(Produccion.EPSILON);
                    }
                }
                primeros = primeros.Distinct().ToList();
                Primeros.TryAdd(nt, primeros);
            }
            return primeros;
        }
    }
}
