using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KyuCompilerF
{
    public class Lector { 

        private string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        public string[] Leer(string filePath, bool relativeToSource)
        {
            string[] codigo;
            List<string> lineas = new List<string>();
            string actualFilePath = relativeToSource ? _filePath + "/" + filePath : filePath;
            StreamReader reader;
            try
            {
                reader = new StreamReader(actualFilePath);
                string linea = "";
                while (linea != null)
                {
                    linea = reader.ReadLine();
                    if (linea != null)
                    {
                        linea = linea.Trim();
                        linea = linea.Split(';')[0];
                        lineas.Add(linea);
                    }
                }

                codigo = new string[lineas.Count];
                for (int i = 0; i < codigo.Length; i++)
                {
                    codigo[i] = lineas[i];
                }

                reader.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                codigo = new string[0];
            }

            return codigo;
        }
    }
}
