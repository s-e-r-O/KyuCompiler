using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KyuCompiler.Utils;

namespace KyuCompiler.Models
{
    class Validator
    {
        Stack<string> productionsStack = new Stack<string>();
        //Stack<string> wordStack = new Stack<string>();
        string initialSymbol = KyuValues.Gramatica.Produccciones[0].Cabeza.ToString();
        Gramatica gram = KyuValues.Gramatica;


        public Validator()
        {
            productionsStack.Push("$");
        }

        public bool validate(List<Token> input, Dictionary<char, Dictionary<string, Produccion>> table)
        {
            string topProduction;
            Produccion production;
            Stack<Token> tokenStack = new Stack<Token>();
            tokenStack.Push(new Token(Token.TokenType.PARSER, Parser.DOLAR, 0, 0));
            input.Reverse();
            input.ToList().ForEach(t => tokenStack.Push(t));
            //this.init(input, this.initialSymbol);
            string topWord;
            do
            {
                topProduction = this.productionsStack.Pop();
                topWord = tokenStack.Pop().value();

                if (this.gram.EsTerminal(topProduction))
                {
                    if (!topProduction.Equals(topWord))
                    {
                        Console.WriteLine("Syntax Error near: " + topWord);
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        production = table[topProduction[0]][topWord];
                        this.addToProductionStack(production);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Syntax Error near: " + topWord);
                        return false;
                    }
                }
            } while (!topProduction.Equals("$") && !topWord.Equals("$"));
            
            return true;
        }

        private void addToProductionStack(Produccion p)
        {
            string[] splitedBody = p.Cuerpo.Split(' ');
            for(int i=splitedBody.Length-1;i>=0;i--)
            {
                this.productionsStack.Push(splitedBody[i]);
            }
        }
 
    }
}
