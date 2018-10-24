using KyuCompilerF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Exceptions
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
