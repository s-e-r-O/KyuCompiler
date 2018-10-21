using KyuCompiler.Models;
using KyuCompiler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KyuCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Lector l = new Lector();
            string[] file = l.Leer("Examples/test.kyu");
            Tokenizer t = new Tokenizer();
            List<Token> tokens = t.Analizar(file).ToList();
            Console.WriteLine("({0} Rules) NT: {1}, T: {2}", 
                                KyuValues.Gramatica.Produccciones.Count, 
                                KyuValues.Gramatica.NoTerminales.Count, 
                                KyuValues.Gramatica.Terminales.Count);
            Parser p = new Parser();
            p.Calcular(KyuValues.Gramatica);
            Console.ReadKey();
        }
    }
}
