using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Exceptions
{
    class KyuSemanticException : ApplicationException
    {
        public enum Type {
            NUMBER_OF_ARGUMENTS_INVALID,
            FUNCTION_ALREADY_DECLARED,
            FUNCTION_NOT_INITIALIZED,
            VARIABLE_ALREADY_DECLARED,
            VARIABLE_NOT_INITIALIZED,
            CONFLICTING_TYPES,
            INVALID_CONDITION,
        }

        Type tipo;
        string descripcion;
        public int Linea { get; set; }
        
        public KyuSemanticException(Type tipo, string descripcion)
        {
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.Linea = -1;
        }

        public override string ToString()
        {
            string[] type = tipo.ToString().ToLower().Split('_');
            string s = "";
            foreach (string t in type) {
                if (t.Length > 1)
                {
                    s += t[0].ToString().ToUpper() + t.Substring(1);
                }
            }
            return String.Format("Kyu{0}Exception: at line {1} ({2})", s, linea, descripcion);
        }
    }
}
