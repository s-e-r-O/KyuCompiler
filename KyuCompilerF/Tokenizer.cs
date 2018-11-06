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

        public bool EsCaracter(string cadena)
        {
            string patron = "^'[^\\\\']{1}'$|^'\\\\n'$|^'\\\\\\\\'$|^'\\\\\"'$|^'\\\\''$";
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

        public bool EsCadena(string cadena)
        {

            string patron = "^\"[\\w|\\s|\\W|\\D]*\"$";
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
                    else if (caracteres[j].Equals("\""))
                    {
                        aux = caracteres[j];
                        j++;
                        while (j < caracteres.Length - 1 && !caracteres[j].Equals("\""))
                        {
                            if (caracteres[j].Equals("\\") && (caracteres[j + 1].Equals("t") || caracteres[j + 1].Equals("n") || caracteres[j + 1].Equals("\"") || caracteres[j + 1].Equals("\\")))
                            {
                                aux += caracteres[j] + caracteres[j + 1];
                                j += 2;
                            }
                            else if(!caracteres[j].Equals("\\"))
                            {
                                aux += caracteres[j];
                                j++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (caracteres[j].Equals("\\"))
                        {
                            break;
                        }
                        aux += caracteres[j];
                        auxTokens.Add(new Token(aux, i + 1, j - aux.Length + 2));
                        aux = "";
                    }
                    else if (caracteres[j].Equals("'"))
                    {
                        aux = caracteres[j];
                        j++;
                        if (caracteres[j].Equals("\\") && (caracteres[j + 1].Equals("t") || caracteres[j + 1].Equals("n") || caracteres[j + 1].Equals("'") || caracteres[j + 1].Equals("\\")))
                        {
                            aux += caracteres[j] + caracteres[j + 1];
                            j += 2;
                        }
                        else if (!caracteres[j].Equals("\\"))
                        {
                            aux += caracteres[j];
                            j++;
                        }
                        else
                        {
                            break;
                        }
                        aux += caracteres[j];
                        auxTokens.Add(new Token(aux, i + 1, j - aux.Length + 1));
                        aux = "";
                    }
                    else if (caracteres[j].Equals("~") && EsNumero(caracteres[j + 1]))
                    {
                        aux = caracteres[j];
                        j++;
                        while (j < caracteres.Length - 1 && (!caracteres[j].Equals("\t") || !caracteres[j].Equals(" ")))
                        {
                            aux += caracteres[j];
                            j++;
                        }
                        aux += caracteres[j];
                        auxTokens.Add(new Token(aux, i + 1, j - aux.Length + 2));
                        aux = "";
                    }
                    else if (caracteres[j].Equals("\t") || caracteres[j].Equals(" "))
                    {
                        if (!aux.Equals(""))
                        {
                            auxTokens.Add(new Token(aux, i + 1, j - aux.Length + 1));
                        }
                        aux = "";
                    }
                    else if (EsSimbolo(caracteres[j]) && caracteres[j + 1].Equals("="))
                    {
                        if (!EsSimboloA(caracteres[j]))
                        {
                            auxTokens.Add(new Token(caracteres[j] + caracteres[j + 1], i + 1, j + 1));
                            j++;
                        }
                        else
                        {
                            auxTokens.Add(new Token(caracteres[j], i + 1, j + 1));
                        }
                        aux = "";
                    }
                    else if (EsSimbolo(caracteres[j]))
                    {
                        if (!aux.Equals(""))
                        {
                            auxTokens.Add(new Token(aux, i, j));
                        }
                        auxTokens.Add(new Token(caracteres[j], i + 1, j + 1));
                        aux = "";
                    }
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
                    tokens[i].Simbolo = new Simbolo(SimboloTipo.LIST_CHAR, tokens[i].lexema);
                }
                else if (EsCaracter(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.VALUE;
                    tokens[i].descripcion = "Caracter";
                    tokens[i].Simbolo = new Simbolo(SimboloTipo.CHAR, tokens[i].lexema);
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
                    tokens[i].Simbolo = new Simbolo(SimboloTipo.NUMERO, int.Parse(tokens[i].lexema));
                }
                else if (EsOperadorA(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.ARITH_OPERATOR;
                    tokens[i].descripcion = "Operador Aritmético";
                    tokens[i].Simbolo = new Simbolo(SimboloTipo.OPERADOR_ARITMETICO, tokens[i].lexema);
                }
                else if (EsOperadorL(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.BOOLEAN_OPERATOR;
                    tokens[i].descripcion = "Operador Lógico";
                    tokens[i].Simbolo = new Simbolo(SimboloTipo.OPERADOR_BOOLEANO, tokens[i].lexema);
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
                    tokens[i].Simbolo = new Simbolo(SimboloTipo.BOOLEANO, bool.Parse(tokens[i].lexema));
                }
                else if (EsIdentificador(tokens[i].lexema))
                {
                    tokens[i].token = Token.TokenType.IDENTIFIER;
                    tokens[i].descripcion = "Identificador";
                    tokens[i].Simbolo = new Simbolo() { Id = tokens[i].lexema };
                    TablaSimbolo.Tabla.AddID(tokens[i].lexema);
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
