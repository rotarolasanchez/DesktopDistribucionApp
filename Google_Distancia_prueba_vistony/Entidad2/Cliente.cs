using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class Cliente
    {
             
        public class  Elementos
        {

            public string CustId { get; set; }
            public string Fecha{ get; set; }
            public string ShipTo { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }

            public Elementos()
            {
                CustId = "";
                Fecha = "";
                ShipTo = "";
                Latitud = "";
                Longitud = "";
            }

            public Elementos(string custid, string fecha,string shipto,string latitud, string longitud)
            {
                CustId = custid;
                Fecha = fecha;
                ShipTo = shipto;
                Latitud = latitud;
                Longitud = longitud;
            }
        }

    }
}


