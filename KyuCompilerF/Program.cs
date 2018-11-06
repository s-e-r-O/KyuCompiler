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
                string[] file = l.Leer("Examples/test.kyu", true);
                Tokenizer t = new Tokenizer();
                List<Token> tokens = t.Analizar(file).ToList();
                Parser p = new Parser();
                p.CalcularLL1(KyuValues.Gramatica);
                Nodo raiz = p.Evaluar(tokens);
                SemanticAnalyzer s = new SemanticAnalyzer();
                s.Evaluar(raiz);
                Console.WriteLine("All good!");
                Console.WriteLine(TablaSimbolo.Tabla);
            }
            catch (AggregateException e)
            {
                e.Handle(ex => {
                    Console.WriteLine(ex);
                    return true;
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}
