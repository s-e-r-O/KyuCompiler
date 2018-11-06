using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Exceptions
{
    class KyuSemanticException
    {
        string descripcion;
        int linea;
        
        public KyuSemanticException(string descripcion, int linea)
        {
            this.descripcion = descripcion;
            this.linea = linea;
        }

        public override string ToString()
        {
            return String.Format("Kyu {0} SemanticException: at line {1}", descripcion, linea);
        }
    }
}
