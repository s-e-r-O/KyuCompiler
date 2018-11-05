using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Models
{
    public class Token
    {
        public enum TokenType
        {
            VALUE,
            KEYWORD,
            SEPARATOR,
            BOOLEAN_OPERATOR,
            ARITH_OPERATOR,
            COMPARATOR,
            IDENTIFIER,
            PARSER,
            DOLLAR,
            NULL
        }
        public TokenType token { get; set; }//describe si es palabra reservada u otro
        public string lexema { get; set; }//lo que es
        public int linea { get; set; }
        public int columna { get; set; }
        public string descripcion { get; set; }//en caso de necesitar una descripción más detallada
        public Simbolo Simbolo { get; set; }

        public Token()
        {
            this.token = TokenType.NULL;
            this.lexema = "";
            this.linea = 0;
            this.columna = 0;
            this.descripcion = "";
        }

        public Token(TokenType token, string lexema, int linea, int columna)
        {
            this.token = token;
            this.lexema = lexema;
            this.linea = linea;
            this.columna = columna;
            this.descripcion = "";
        }

        public Token(string lexema, int linea, int columna)
        {
            this.token = TokenType.NULL;
            this.lexema = lexema;
            this.linea = linea;
            this.columna = columna;
        }

        override
        public string ToString()
        {
            return this.lexema + "\t es: " + this.token + "\tlinea: " + this.linea + "\tcolumna: " + this.columna + "\tdescripción: " + this.descripcion + "\tsimbolo: " + this.Simbolo.Tipo;
        }

        public string value()
        {
            switch (token)
            {
                case TokenType.IDENTIFIER:
                    return "i";
                case TokenType.VALUE:
                    return "v";
                case TokenType.BOOLEAN_OPERATOR:
                    return "b";
                case TokenType.ARITH_OPERATOR:
                    return "z";
                case TokenType.COMPARATOR:
                    return "x";
                case TokenType.DOLLAR:
                    return "$";
            }
            return lexema;
        }
    }
}
