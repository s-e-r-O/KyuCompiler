using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace KyuCompiler
{
    class Lector
    {
        public string[] Leer(string filePath)
        {
            string[] codigo;
            List<string> lineas = new List<string>();
            StreamReader reader;
            try
            {
                reader = new StreamReader(filePath);
                string linea = "";
                while (linea != null)
                {
                    linea = reader.ReadLine();
                    if (linea != null)
                    {
                        linea = linea.Trim();
                        linea = linea.Split(';')[0];
                        if (linea != "")
                        {
                            //linea = Regex.Replace(linea, @"\s+", " ");
                            lineas.Add(linea);
                        }
                    }
                }

                codigo = new string[lineas.Count];
                for (int i = 0; i < codigo.Length; i++)
                {
                    codigo[i] = lineas[i];
                }
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
