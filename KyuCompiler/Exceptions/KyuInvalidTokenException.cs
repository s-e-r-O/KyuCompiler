using KyuCompiler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KyuCompiler.Exceptions
{
    class KyuInvalidTokenException : ApplicationException
    {
        Token token;
        public KyuInvalidTokenException(Token token)
        {
            this.token = token;
        }

        public override string ToString()
        {
            return String.Format("Kyu InvalidTokenException: \"{0}\" in ({1},{2})", token.lexema, token.linea, token.columna);
        }
    }
}
