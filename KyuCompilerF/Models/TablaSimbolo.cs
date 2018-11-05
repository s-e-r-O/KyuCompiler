using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyuCompilerF.Models
{
    public class TablaSimbolo
    {
        private static TablaSimbolo tabla;
        public static TablaSimbolo Tabla
        {
            get
            {
                if (tabla == null)
                {
                    tabla = new TablaSimbolo();
                }
                return tabla;
            }
        }

        private Dictionary<string, Simbolo> Simbolos { get; set; }

        private TablaSimbolo()
        {
            Simbolos = new Dictionary<string, Simbolo>();
        }
        
        public bool IsInTable(string id)
        {
            return Simbolos.ContainsKey(id);
        }

        public bool Add(string id, Simbolo simbolo) {
            if (!IsInTable(id))
            {
                Simbolos.Add(id, simbolo);
                return true;
            }
            return false;
        }

        public bool Remove(string id)
        {
            if (IsInTable(id))
            {
                Simbolos.Remove(id);
                return true;
            }
            return false;
        }

        public Simbolo Get(string id)
        {
            return Simbolos[id];
        }

        public bool Update(string id, Simbolo simbolo)
        {
            if (!IsInTable(id))
            {
                return false;
            }
            Simbolos[id] = simbolo;
            return true;
        }

        public bool AddID(string id)
        {
            if (!IsInTable(id))
            {
                Simbolos.Add(id, new Simbolo() { Id = id });
                return true;
            }
            return false;
        }

        public bool IsInitialized(string id)
        {
            if (!IsInTable(id))
            {
                return false;
            }
            return Simbolos[id] != null;
        }
    }
}
