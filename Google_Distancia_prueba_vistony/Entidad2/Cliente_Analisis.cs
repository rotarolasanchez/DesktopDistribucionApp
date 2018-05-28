using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class Cliente_Analisis
    {
        public class Elementos
        {
            public string Row { get; set; }
            public string codcliori { get; set; }
            public string shiptonumori { get; set; }
            public string fechaori { get; set; }
            public string latitudori { get; set; }
            public string longitudori { get; set; }
            public string codclides { get; set; }
            public string shiptonumdes { get; set; }
            public string fechades { get; set; }
            public string latituddes { get; set; }
            public string longituddes { get; set; }
            public decimal distance { get; set; }

            public Elementos()
            {
                Row = "";
                codcliori = "";
                shiptonumori = "";
                fechaori = "";
                latitudori = "";
                longitudori = "";
                codclides = "";
                shiptonumdes = "";
                fechades = "";
                latituddes = "";
                longituddes = "";
                distance = 0;
            }

            public Elementos(string row,string codclior, string shiptonumor, string fechaor, string latitudor, string longitudor,
                string codclide, string shiptonumde, string fechade,string latitudde,string longitudde,decimal distanc  )
            {
                Row = row;
                codcliori = codclior;
                shiptonumori = shiptonumor;
                fechaori = fechaor;
                latitudori = latitudor;
                longitudori = longitudor;
                codclides = codclide;
                shiptonumdes = shiptonumde;
                fechades = fechade;
                latituddes = latitudde;
                longituddes = longitudde;
                distance = distanc;
            }
        }

    }
}
