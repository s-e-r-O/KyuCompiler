using KyuCompiler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KyuCompiler.Exceptions
{
    class KyuSyntaxException : ApplicationException
    {
        Token token;
        public KyuSyntaxException(Token token)
        {
            this.token = token;
        }

        public override string ToString()
        {
            string type = token.token.ToString().ToLower();
            if (type.Length > 1) {
                type = type[0].ToString().ToUpper() + type.Substring(1);
            }
            return String.Format("Kyu {0}SyntaxException: \"{1}\" in ({2},{3})", type, token.lexema, token.linea, token.columna);
        }
    }
}
