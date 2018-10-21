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
        Dictionary<char, List<string>> Siguientes { get; set; }
        Gramatica Gramatica { get; set; }
        
        public void Calcular(Gramatica g)
        {
            Gramatica = g;
            Primeros = new Dictionary<char, List<string>>();
            Siguientes = new Dictionary<char, List<string>>();
            foreach (Produccion p in Gramatica.Produccciones)
            {
                P(p.Cabeza.ToString());
            }
            foreach (Produccion p in Gramatica.Produccciones)
            {
                S(p.Cabeza.ToString());
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

            foreach (KeyValuePair<char, List<string>> p in Siguientes)
            {
                Console.Write(p.Key + ": ");
                foreach (string s in p.Value)
                {
                    Console.Write("\"{0}\", ", s);
                }
                Console.WriteLine();
            }
        }

        private List<string> P(string s)
        {
            List<string> primeros = new List<string>();
            if (Gramatica.EsTerminal(s))
            {
                primeros.Add(s);
                return primeros;
            }
            
            char nt = s[0];
            if (Primeros.TryGetValue(nt, out List<string> primerosD))
            {
                return primerosD;
            }

            if (Gramatica.Produccciones.Exists(p => p.Cabeza == nt && p.Cuerpo == Produccion.EPSILON))
            {
                primeros.Add(Produccion.EPSILON);
            }
            List<Produccion> producciones = Gramatica.Produccciones.Where(p => p.Cabeza == nt && p.Cuerpo != Produccion.EPSILON).ToList();
            foreach (Produccion p in producciones)
            {
                int n = p.Palabras.Length;
                foreach (string a in p.Palabras)
                {
                    List<string> primerosA = P(a);
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
            return primeros;
        }

        private List<string> S(string s)
        {
            List<string> siguientes = new List<string>();
            char nt = s[0];
            if (Siguientes.TryGetValue(nt, out List<string> siguientesD))
            {
                return siguientesD;
            }
            if (Gramatica.Produccciones.ElementAt(0).Cabeza == nt)
            {
                siguientes.Add("$");
            }
            List<Produccion> producciones = Gramatica.Produccciones.Where(p => p.Palabras.Contains(s)).ToList();

            foreach(Produccion p in producciones)
            {
                int index = p.Palabras.ToList().FindIndex(w => w == s) + 1;
                while (index < p.Palabras.Length)
                {
                    List<string> primerosA = P(p.Palabras[index]);
                    if (!primerosA.Contains(Produccion.EPSILON))
                    {
                        break;
                    }
                    index++;
                }
                if (index >= p.Palabras.Length)
                {
                    siguientes.AddRange(S(p.Cabeza.ToString()));
                }
            }

            siguientes = siguientes.Distinct().ToList();
            Siguientes.TryAdd(nt, siguientes);
            return siguientes;
        }
    }
}
