using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class Cliente_Agrupado
    {
        public class Elementos
        {

            public string Grupo { get; set; }
            public string ShipToNum { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }

            public Elementos()
            {
                Grupo = "";
                ShipToNum = "";
                Latitud = "";
                Longitud = "";
            }

            public Elementos(string grupo,string shiptonum,string latitud, string longitud)
            {
                Grupo = grupo;
                ShipToNum = shiptonum;
                Latitud = latitud;
                Longitud = longitud;
            }
        }
    }
}
