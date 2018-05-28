using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class Distribucion
    {
        public class Rows
        {
            public Elements[] elements { get; set; }

        }
        public class Elements
        {
            public Duration duration { get; set; }
            public Distance distance { get; set; }
        }
        public class Duration
        {
            public string text { get; set; }
            public string value { get; set; }
        }
        public class Distance
        {
            public string text { get; set; }
            public string value { get; set; }
        }

        public class Origen
        {
            public string origin_addresses { get; set; }
        }

        public class RootObject
        {
            public string[] destination_addresses { get; set; }
            public string[] origin_addresses { get; set; }
            public Rows[] rows { get; set; }
            public string status { get; set; }

        }
        public class Cliente_visita
        {
            public string codigo { get; set; }
            public string latitud { get; set; }
            public string longitud { get; set; }

            public Cliente_visita()
            {
                codigo = "";
                latitud = "";
                longitud = "";                
            }
            public Cliente_visita(string codigo1,string latitud1, string longitud1)
            {
                codigo = codigo1;
                latitud = latitud1;
                longitud = longitud1;                
            }

        }
        public class RutaCorta
        {

            public int codoridiscli { get; set; }
            public string coddesdiscli { get; set; }
            public string disdiscli { get; set; }
            public string tiemdiscli { get; set; }

            public RutaCorta()
            {
                codoridiscli    = 0;
                coddesdiscli    = "";
                disdiscli       = "";
                tiemdiscli      = "";
            }

            public RutaCorta(int ccodoridiscl, string coddesdiscl, string disdiscl, string tiemdiscl)
            {
                codoridiscli    = ccodoridiscl;
                coddesdiscli    = coddesdiscl;
                disdiscli       = disdiscl;
                tiemdiscli      = tiemdiscl;
            }
        }

    }
}
