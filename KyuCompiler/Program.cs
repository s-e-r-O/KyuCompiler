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
            Parser p = new Parser();
            p.CalcularLL1(KyuValues.Gramatica);
            Validator v = new Validator();
            v.validate(tokens, p.Tabla);
            Console.ReadKey();
        }
    }
}
