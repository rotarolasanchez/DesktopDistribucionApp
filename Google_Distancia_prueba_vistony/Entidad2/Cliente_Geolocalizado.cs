using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class Cliente_Geolocalizado
    {
        public class Elementos
        {

            public string Codigo { get; set; }
            public string CustNum { get; set; }
            public string ShipToNum { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }

            public Elementos()
            {
                Codigo = "";
                CustNum = "";
                ShipToNum = "";
                Latitud = "";
                Longitud = "";
            }

            public Elementos(string codigo,string custnum,string shiptonum, string latitud, string longitud)
            {
                Codigo = codigo;
                CustNum = custnum;
                ShipToNum = shiptonum;
                Latitud = latitud;
                Longitud = longitud;
            }
        }
    }
}
