using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidad2
{
    public class Grupos
    {
        public class Elementos
        {
            public string CustId { get; set; }
            public string CustNum { get; set; }
            public string Shipto { get; set; }
            public int Cantidad { get; set; }
            

            public Elementos()
            {
                CustId = "";
                CustNum = "";
                Shipto = "";
                Cantidad = 0;
                
            }

            public Elementos(string custid,string custnum,string shipto, int cantidad)
            {
                CustId = custid;
                CustNum = custnum;
                Shipto = shipto;
                Cantidad = cantidad;                
            }
        }
    }
}
