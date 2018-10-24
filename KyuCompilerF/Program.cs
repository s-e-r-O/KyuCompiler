using KyuCompilerF.Models;
using KyuCompilerF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF
{
    class Program
    {
        static void Main(string[] args)
        {
            Lector l = new Lector();
            try
            {
                string[] file = l.Leer("Examples/test.kyu");
                Tokenizer t = new Tokenizer();
                List<Token> tokens = t.Analizar(file).ToList();
                Parser p = new Parser();
                p.CalcularLL1(KyuValues.Gramatica);
                p.Evaluar(tokens);
                Console.WriteLine("All good!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}
