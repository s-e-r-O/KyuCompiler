using KyuCompilerF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Utils
{
    public class KyuValues
    {
        private static Gramatica gramatica;
        public static Gramatica Gramatica
        {
            get
            {
                if (gramatica == null)
                {
                    gramatica = new Gramatica()
                    {
                        Produccciones = new List<Produccion>
                        {
                            new Produccion('S', "kyu# \n { \n M }"),
                            new Produccion('M', "I \n J"),
                            new Produccion('J', "M"),
                            new Produccion('J', Produccion.EPSILON),
                            new Produccion('I', "D"),
                            new Produccion('I', "H"),
                            new Produccion('I', "G"),
                            new Produccion('I', "L"),
                            new Produccion('I', "stop"),
                            new Produccion('I', "return E"),
                            new Produccion('D', "i K"),
                            new Produccion('D', "function i P is \n M done"),
                            new Produccion('K', "is B"),
                            new Produccion('K', "R"),
                            new Produccion('B', "E"),
                            new Produccion('B', "[ X ]"),
                            new Produccion('X', "E Z"),
                            new Produccion('X', Produccion.EPSILON),
                            new Produccion('Z', ", E Z"),
                            new Produccion('Z', Produccion.EPSILON),
                            new Produccion('H', "change A changed"),
                            new Produccion('A', "i N \n"),
                            new Produccion('N', ", A E"),
                            new Produccion('N', "\n E"),
                            new Produccion('P', "i Q"),
                            new Produccion('P', Produccion.EPSILON),
                            new Produccion('Q', ", i Q"),
                            new Produccion('Q', Produccion.EPSILON),
                            new Produccion('R', "E U"),
                            new Produccion('R', Produccion.EPSILON),
                            new Produccion('U', ", E U"),
                            new Produccion('U', Produccion.EPSILON),
                            new Produccion('L', "forevery i in i \n M done"),
                            new Produccion('L', "forever \n M done"),
                            new Produccion('G', "given E \n M V"),
                            new Produccion('V', "done"),
                            new Produccion('V', "otherwise \n M done"),
                            new Produccion('E', "C W"),
                            new Produccion('W', "b E"),
                            new Produccion('W', Produccion.EPSILON),
                            new Produccion('C', "O Y"),
                            new Produccion('C', "~ ( C )"),
                            new Produccion('Y', "x O"),
                            new Produccion('Y', Produccion.EPSILON),
                            new Produccion('O', "z O , O"),
                            new Produccion('O', "T"),
                            new Produccion('T', "i"),
                            new Produccion('T', "v")
                        }
                    };
                }
                return gramatica;
            }
        }
    }
}
