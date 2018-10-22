using KyuCompiler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyuCompiler
{
    class Parser
    {
        public static readonly string DOLAR = "$";

        public Dictionary<char, List<string>> Primeros { get; private set; }
        public Dictionary<char, List<string>> Siguientes { get; private set; }
        public Gramatica Gramatica { get; private set; }
        
        public void CalcularLL1(Gramatica g)
        {
            Gramatica = g;
            Primeros = new Dictionary<char, List<string>>();
            Siguientes = new Dictionary<char, List<string>>();

            foreach (char nt in Gramatica.NoTerminales)
            {
                P(nt.ToString());
                S(nt.ToString(), true);
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

        private List<string> S(string s, bool final)
        {
            List<string> siguientes = new List<string>();
            char nt = s[0];
            if (Siguientes.TryGetValue(nt, out List<string> siguientesD))
            {
                return siguientesD;
            }
            else if (!final)
            {
                return siguientes;
            }
            if (Gramatica.Produccciones.ElementAt(0).Cabeza == nt)
            {
                siguientes.Add(DOLAR);
            }
            List<Produccion> producciones = Gramatica.Produccciones.Where(p => p.Palabras.Contains(s)).ToList();

            foreach(Produccion p in producciones)
            {
                int index = p.Palabras.ToList().FindIndex(w => w == s) + 1;
                while (index < p.Palabras.Length)
                {
                    List<string> primerosA = P(p.Palabras[index]);
                    siguientes.AddRange(primerosA);
                    if (!primerosA.Contains(Produccion.EPSILON))
                    {
                        break;
                    }
                    siguientes.Remove(Produccion.EPSILON);
                    index++;
                }
                if (index >= p.Palabras.Length)
                {
                    siguientes.AddRange(S(p.Cabeza.ToString(), false));
                }
            }

            siguientes = siguientes.Distinct().ToList();
            Siguientes.TryAdd(nt, siguientes);
            return siguientes;
        }
    }
}
