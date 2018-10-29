using KyuCompilerF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Exceptions
{
    class KyuGrammarException : ApplicationException
    {
        Produccion p;
        string t;
        public KyuGrammarException(Produccion p, string t) {
            this.p = p;
            this.t = t;
        }

        public override string ToString()
        {
            return String.Format("Kyu InvalidGrammarException: For {0} with {1}", t, p.Cabeza);
        }
    }
}
