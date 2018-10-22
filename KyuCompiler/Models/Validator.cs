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
        Stack<Token> wordStack = new Stack<Token>();
        string initialSymbol = KyuValues.Gramatica.Produccciones[0].Cabeza.ToString();
        Gramatica gram = KyuValues.Gramatica;
        public Validator()
        {
            this.productionsStack.Push("$");
            Token dolar = new Token(Token.TokenType.DOLLAR, "$",-1,-1);
            this.wordStack.Push(dolar);
        }

        public bool validate(List<Token> tokenList, Dictionary<char, Dictionary<string, Produccion>> table)
        {
            string topProduction;
            Token topWord;
            Produccion production;
            bool found = false; ;

            this.init(tokenList, this.initialSymbol);
            topProduction = this.productionsStack.Pop();
            topWord = this.wordStack.Pop();

            while (!topProduction.Equals("$") && !topWord.value().Equals("$"))
            {
                if (this.gram.EsTerminal(topProduction))
                {
                    if (!topProduction.Equals(topWord.value()))
                    {
                        Console.WriteLine("Syntax Error at line: " + topWord.linea + " and column: " + topWord.columna);
                        return false;
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
                        production = table[topProduction[0]][topWord.value()];
                        this.addToProductionStack(production);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Syntax Error at line: " + topWord.linea + " and column: " + topWord.columna);
                        return false;
                    }
                }
                if(found)
                {
                    topWord = this.wordStack.Pop();
                }
                topProduction = this.productionsStack.Pop();
            }
            
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
        private void init(List<Token> tokenList, string initialSymbol)
        {

            tokenList.Reverse();
            foreach(Token t in tokenList)
            {
                this.wordStack.Push(t);
            }
            this.productionsStack.Push(initialSymbol);
        }
    }
}
