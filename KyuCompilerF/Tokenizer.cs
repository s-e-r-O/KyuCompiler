using KyuCompilerF.Exceptions;
using KyuCompilerF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KyuCompilerF
{
    public class Tokenizer
    {
        public Token[] Analizar(string[] codigo)
        {
            Token[] tokens = this.Reconocer(codigo);

            return tokens;
        }

        public bool EsIdentificador(string cadena)
        {

            string patron = "^[a-zA-Z_].*$";

            Match match = Regex.Match(cadena, patron);

            return match.Success;
        }

        public bool EsPalabraReservada(string cadena)
        {

            string patron = @"^change$|^changed$|^given$|^otherwise$|^done$|^return$|^forevery$|^forever$|^done$|^in$|^stop$|^kyu#$|^is$|^function$";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsBoolean(string cadena)
        {
            string patron = "^true$|^false$";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsNumero(string cadena)
        {

            string patron = @"^~?[0-9]+(.[0-9]+)?$";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsOperadorA(string cadena)
        {

            string patron = "\\+|-|\\*|/|\\^|!";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsOperadorL(string cadena)
        {
            string patron = "&|&&|\\||\\|\\||~";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsComparador(string cadena)
        {
            string patron = "<|>|==|~=|>=|<=";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsSimboloA(string cadena)
        {

            string patron = "\\(|\\)|\\[|\\]|{|}";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsComilla(string cadena)
        {
            string patron = "\"|'";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsCadena(string cadena)
        {

            string patron = "^\"[\\w|\\s|\\W]*\"$";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public bool EsSimbolo(string cadena)
        {
            string patron = ":|\\(|\\)|=|\\[|\\]|{|}|-|\\+|>|<|,|\\*|/|!|\"|'|&|\\|\\||\\||~";
            Match match = Regex.Match(cadena, patron);
            return match.Success;
        }

        public Token[] Reconocer(string[] codigo)
        {
            List<Token> auxTokens = new List<Token>();
            Token[] tokens;
            string[] caracteres;
            string aux = "";

            for (int i = 0; i < codigo.Length; i++)
            {
                //no borrar estas 2 líneas por favor, son para evitar excepciones
                caracteres = new string[codigo[i].Length + 1];
                caracteres[codigo[i].Length] = "\n";

                //no quitar el -1
                for (int j = 0; j < caracteres.Length - 1; j++)
                {
                    caracteres[j] = codigo[i].Substring(j, 1);
                }

                //no quitar el -1
                for (int j = 0; j < caracteres.Length - 1; j++)
                {
                    if (!caracteres[j].Equals(" ") && !caracteres[j].Equals("\t") && !EsSimbolo(caracteres[j]))
                    {
                        aux += caracteres[j];
                    }//si es una cadena
                    else if (EsComilla(caracteres[j]))
                    {
                        aux = caracteres[j];
                        j++;
                        while (!EsComilla(caracteres[j]))
                        {
                            aux += caracteres[j];
                            j++;
                        }
                        aux += caracteres[j];
                        auxTokens.Add(new Token(aux, i, j));
                        aux = "";
                    }
                    else if (caracteres[j].Equals("~") && EsNumero(caracteres[j + 1]))
                    {
                        aux = caracteres[j];
                        j++;
                        while ((!caracteres[j].Equals("\t") || !caracteres[j].Equals(" ")) && j < caracteres.Length - 1)
                        {
                            aux += caracteres[j];
                            j++;
                        }
                        aux += caracteres[j];
                        auxTokens.Add(new Token(aux, i, j));
                        aux = "";
                    }
                    else if (caracteres[j].Equals("\t") || caracteres[j].Equals(" "))
                    {
                        if (!aux.Equals(""))
                        {
                            auxTokens.Add(new Token(aux, i, j));
                        }
                        aux = "";
                    }
                    else if (EsSimbolo(caracteres[j]) && caracteres[j + 1].Equals("="))
                    {
                        if (!EsSimboloA(caracteres[j]))
                        {
                            auxTokens.Add(new Token(caracteres[j] + caracteres[j + 1], i, j));
                            j++;
                        }
                        else
                        {
                            auxTokens.Add(new Token(caracteres[j], i, j));
                        }
                        aux = "";
                    }
                    else if (EsSimbolo(caracteres[j]))
                    {
                        if (!aux.Equals(""))
                        {
                            auxTokens.Add(new Token(aux, i, j));
                        }
                        auxTokens.Add(new Token(caracteres[j], i, j));
                        aux = "";
                    }/*
                    if(j >= caracteres.Length - 2 && caracteres[j + 1].Equals("\n"))
                    {
                        auxTokens.Add(new Token("\n", i, j + 1));
                    }*/
                }

                if (!aux.Equals("") && !aux.Equals(" "))
                {
                    auxTokens.Add(new Token(aux, i, 0));
                    aux = "";
                }

                auxTokens.Add(new Token("\n", i, caracteres.Length));

                if (i >= codigo.Length - 1 && !aux.Equals(" ") && !aux.Equals(""))
                {
                    auxTokens.Add(new Token(aux, i, codigo.Length - 1));
                }
            }

            tokens = new Token[auxTokens.Count];
            for (int i = 0; i < auxTokens.Count; i++)
            {
                tokens[i] = auxTokens[i];
            }

            for (int i = 0; i < auxTokens.Count; i++)
            {
                if (EsCadena(tokens[i].lexema))
                {

                    tokens[i].token = Token.TokenType.VALUE;
                    tokens[i].descripcion = "Cadena";
                    tokens[i].Simbolo = new Simbolo<object>(Simbolo<object>.SimboloTipo.CADENA, tokens[i].lexema);
                }
                else if (EsPalabraReservada(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.KEYWORD;
                    tokens[i].descripcion = "Palabra Reservada";
                }
                else if (EsSimboloA(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.SEPARATOR;
                    tokens[i].descripcion = "Agrupación";
                }
                else if (EsNumero(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.VALUE;
                    tokens[i].descripcion = "Número";
                    tokens[i].Simbolo = new Simbolo<object>(Simbolo<object>.SimboloTipo.NUMERO, int.Parse(tokens[i].lexema));
                    Console.WriteLine(tokens[i].Simbolo.ToString());
                }
                else if (EsOperadorA(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.ARITH_OPERATOR;
                    tokens[i].descripcion = "Operador Aritmético";
                    tokens[i].Simbolo = new Simbolo<object>(Simbolo<object>.SimboloTipo.OPERADOR_ARITMETICO, tokens[i].lexema);
                }
                else if (EsOperadorL(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.BOOLEAN_OPERATOR;
                    tokens[i].descripcion = "Operador Lógico";
                    tokens[i].Simbolo = new Simbolo<object>(Simbolo<object>.SimboloTipo.OPERADOR_BOOLEANO, tokens[i].lexema);
                }
                else if (EsComparador(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.COMPARATOR;
                    tokens[i].descripcion = "Comparador";
                }
                else if (tokens[i].lexema.Equals(","))
                {
                    tokens[i].token = Token.TokenType.SEPARATOR;
                    tokens[i].descripcion = "Separador";
                }
                else if (tokens[i].lexema.Equals("\n"))
                {
                    tokens[i].token = Token.TokenType.SEPARATOR;
                    tokens[i].descripcion = "Salto de línea";
                }
                else if (EsBoolean(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.VALUE;
                    tokens[i].descripcion = "Booleano";
                    tokens[i].Simbolo = new Simbolo<object>(Simbolo<object>.SimboloTipo.BOOLEANO, bool.Parse(tokens[i].lexema));
                    Console.WriteLine(tokens[i].Simbolo.ToString());
                }
                else if (EsIdentificador(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.IDENTIFIER;
                    tokens[i].descripcion = "Identificador";
                }
                else
                {
                    tokens[i].token = Token.TokenType.NULL;
                    tokens[i].descripcion = "No definido";
                    throw new KyuInvalidTokenException(tokens[i]);
                }
            }
            return tokens;
        }
    }
}
