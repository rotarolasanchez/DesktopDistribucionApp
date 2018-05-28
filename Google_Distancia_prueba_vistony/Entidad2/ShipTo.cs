using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class ShipTo
    {
        public class Elementos
        {

            public string CustId { get; set; }
            public string CustNum { get; set; }
            public string Name { get; set; }
            public string ShipToNum { get; set; }
            public string Address { get; set; }
            public string District { get; set; }
            public string Departmen { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }

            public Elementos()
            {
                CustId = "";
                CustNum = "";
                Name = "";
                ShipToNum = "";
                Address = "";
                District = "";
                Departmen = "";
                Latitud = "";
                Longitud = "";
            }

            public Elementos(string custid, string custnum, string name,string shiptonum, string address, 
                string district,string departmen,string latitud,string longitud)
            {
                CustId = custid;
                CustNum = custnum;
                Name = name;
                ShipToNum = shiptonum;
                Address = address;
                District = district;
                Departmen = departmen;
                Latitud = latitud;
                longitud = longitud;
            }
        }
    }
}
