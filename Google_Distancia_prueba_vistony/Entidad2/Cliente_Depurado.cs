using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class Cliente_Depurado
    {
        public class Elementos
        {

            
            public string LatitudOri { get; set; }
            public string LongitudOri { get; set; }
            public string ShipToNum { get; set; }
            public string Distance { get; set; }
            public string LatitudDes { get; set; }
            public string LongitudDes { get; set; }

            public Elementos()
            {
                string LatitudOri = "";
                string LongitudOri = "";
                string ShipToNum = "";
                string Distance = "";
                string LatitudDes = "";
                string LongitudDes = "";

        }

            public Elementos(string latitudori, string longitudori,string shiptonum, string distance,string latituddes,string longituddes)
            {
                LatitudOri = latitudori;
                LongitudOri = longitudori;
                ShipToNum = shiptonum;
                Distance = distance;
                LatitudDes = latituddes;
                LongitudDes = longituddes;
            }
        }
    }
}
