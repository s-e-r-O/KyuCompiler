using KyuCompiler.Models;
using System;
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
            foreach (Token tok in t.Analizar(file))
            {
                Console.WriteLine(tok);
            }
            Console.ReadKey();
        }
    }
}
