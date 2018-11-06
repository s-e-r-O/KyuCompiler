using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Exceptions
{
    class KyuSemanticException : ApplicationException
    {
        string descripcion;
        public int Linea { get; set; }
        
        public KyuSemanticException(string descripcion)
        {
            this.descripcion = descripcion;
            this.Linea = -1;
        }

        public override string ToString()
        {
            return String.Format("Kyu {0} SemanticException: at line {1}", descripcion, Linea);
        }
    }
}
