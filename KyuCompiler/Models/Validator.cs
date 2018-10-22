using System;
using System.Collections.Generic;
using System.Text;
using KyuCompiler.Utils;

namespace KyuCompiler.Models
{
    class Validator
    {
        Stack<string> productionsStack = new Stack<string>();
        Stack<string> wordStack = new Stack<string>();
        string initialSymbol = KyuValues.Gramatica.Produccciones[0].Cabeza.ToString();
        Gramatica gram = new Gramatica();


        public Validator()
        {
            productionsStack.Push("$");
            wordStack.Push("$");
        }

        public bool validate(string input, Dictionary<char, Dictionary<string, Produccion>> table)
        {
            string topProduction;
            string topWord;
            Produccion production;

            this.init(input, this.initialSymbol);

            do
            {
                topProduction = this.productionsStack.Pop();
                topWord = this.wordStack.Pop();

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

        private void init(string input, string initialSymbol)
        {
            string[] noSpaceInput = input.Split(' ');
            for(int i=noSpaceInput.Length-1; i>=0;i--)
            {
                this.wordStack.Push(noSpaceInput[i]);
            }
            this.productionsStack.Push(initialSymbol);
        }
 
    }
}
