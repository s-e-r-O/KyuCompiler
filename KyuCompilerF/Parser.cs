using KyuCompilerF.Exceptions;
using KyuCompilerF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF
{
    public class Parser
    {
        public static readonly string DOLAR = "\\$";

        public Gramatica Gramatica { get; private set; }
        public Dictionary<char, List<string>> Primeros { get; private set; }
        public Dictionary<char, List<string>> Siguientes { get; private set; }
        public Dictionary<char, Dictionary<string, Produccion>> Tabla { get; set; }

        public void CalcularLL1(Gramatica g)
        {
            Gramatica = g;
            Primeros = new Dictionary<char, List<string>>();
            Siguientes = new Dictionary<char, List<string>>();
            Tabla = new Dictionary<char, Dictionary<string, Produccion>>();
            foreach (char nt in Gramatica.NoTerminales)
            {
                P(nt.ToString());
                S(nt.ToString());
            }
            LlenarTabla();
        }

        public void Evaluar(List<Token> input)
        {
            Stack<string> productionsStack = new Stack<string>();
            Stack<Token> wordStack = new Stack<Token>();

            productionsStack.Push(DOLAR);
            productionsStack.Push(Gramatica.Produccciones[0].Cabeza.ToString());

            wordStack.Push(new Token(Token.TokenType.PARSER, DOLAR, 0, 0));

            input.Reverse();
            foreach (Token t in input)
            {
                wordStack.Push(t);
            }
            input.Reverse();

            string topProduction;
            Token topWord;
            Produccion production;
            bool found = false;

            topProduction = productionsStack.Pop();
            topWord = wordStack.Pop();

            while (!topProduction.Equals(DOLAR) && !topWord.value().Equals(DOLAR))
            {
                found = false;
                if (Gramatica.EsTerminal(topProduction))
                {
                    if (!topProduction.Equals(topWord.value()))
                    {
                        throw new KyuSyntaxException(topWord);
                    }
                    else
                    {
                        found = true;
                    }
                }
                else
                {
                    try
                    {
                        production = Tabla[topProduction[0]][topWord.value()];
                        if (!production.Cuerpo.Equals(Produccion.EPSILON))
                        {
                            List<string> listaDePalbaras = production.Palabras.ToList();
                            listaDePalbaras.Reverse();
                            listaDePalbaras.ForEach(s => productionsStack.Push(s));
                        }

                    }
                    catch
                    {
                        throw new KyuSyntaxException(topWord);
                    }
                }
                if (found)
                {
                    topWord = wordStack.Pop();
                }
                topProduction = productionsStack.Pop();
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
                if (n <= 0)
                {
                    primeros.Add(Produccion.EPSILON);
                }
            }
            primeros = primeros.Distinct().ToList();
            if (!Primeros.ContainsKey(nt))
            {
                Primeros.Add(nt, primeros);
            }
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
                siguientes.Add(DOLAR);
            }
            List<Produccion> producciones = Gramatica.Produccciones.Where(p => p.Palabras.Contains(s)).ToList();

            foreach (Produccion p in producciones)
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
                if (index >= p.Palabras.Length && Siguientes.TryGetValue(p.Cabeza, out List<string> siguientesA))
                {
                    siguientes.AddRange(siguientesA);
                }
            }

            siguientes = siguientes.Distinct().ToList();
            if (!Siguientes.ContainsKey(nt))
            {
                Siguientes.Add(nt, siguientes);
            }
            return siguientes;
        }

        private void LlenarTabla()
        {
            Gramatica.NoTerminales.ForEach(nt => Tabla.Add(nt, new Dictionary<string, Produccion>()));
            foreach (Produccion p in Gramatica.Produccciones)
            {
                bool usarSiguientes = true;
                foreach (string palabra in p.Palabras)
                {
                    if (Gramatica.EsTerminal(palabra))
                    {
                        if (!Tabla[p.Cabeza].ContainsKey(palabra))
                        {
                            Tabla[p.Cabeza].Add(palabra, p);
                        }
                        else
                        {
                            if (Tabla[p.Cabeza][palabra] != p)
                            {
                                throw new KyuGrammarException(p, palabra);
                            }
                        }
                        
                        usarSiguientes = false;
                        break;
                    }
                    else if (palabra != Produccion.EPSILON)
                    {
                        List<string> primeros = P(palabra).Where(x => !x.Equals(Produccion.EPSILON)).ToList();
                        foreach (string primero in primeros)
                        {
                            if (!Tabla[p.Cabeza].ContainsKey(primero))
                            {
                                Tabla[p.Cabeza].Add(primero, p);
                            }
                            else
                            {
                                if (Tabla[p.Cabeza][palabra] != p)
                                {
                                    throw new KyuGrammarException(p, palabra);
                                }
                            }

                        }
                        if (!P(palabra).Contains(Produccion.EPSILON))
                        {
                            usarSiguientes = false;
                            break;
                        }
                    }
                }
                if (usarSiguientes)
                {
                    foreach (string siguiente in S(p.Cabeza.ToString()))
                    {
                        if (!Tabla[p.Cabeza].ContainsKey(siguiente))
                        {
                            Tabla[p.Cabeza].Add(siguiente, p);
                        }
                        else
                        {
                            if (Tabla[p.Cabeza][siguiente] != p)
                            {
                                throw new KyuGrammarException(p, siguiente);
                            }
                        }
                    }
                }
            }
        }
    }
}
