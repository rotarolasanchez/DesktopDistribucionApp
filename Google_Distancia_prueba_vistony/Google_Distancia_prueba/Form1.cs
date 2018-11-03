using Entidad2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Google_Distancia_prueba
{
    public partial class Form1 : Form
    {
        public static List<Distribucion.RutaCorta> lista = new List<Distribucion.RutaCorta>();
        public static List<Distribucion.RutaCorta> listaclientes = new List<Distribucion.RutaCorta>();
        public static List<Grupos.Elementos> listacantidadclientes = new List<Grupos.Elementos>();
        public static List<Cliente.Elementos> listanuevosclientes = new List<Cliente.Elementos>();
        public static List<Cliente_Analisis.Elementos> cliente_analisis = new List<Cliente_Analisis.Elementos>();
        public static List<Cliente_Analisis.Elementos> cliente_analizado = new List<Cliente_Analisis.Elementos>();
        public static List<Cliente_Agrupado.Elementos> cliente_grupado = new List<Cliente_Agrupado.Elementos>();
        public static List<Cliente_Agrupado.Elementos> cliente_agrupado = new List<Cliente_Agrupado.Elementos>();
        public static List<Cliente_Depurado.Elementos> cliente_depurado = new List<Cliente_Depurado.Elementos>();
        public static List<Grupos.Elementos> lista_grupos = new List<Grupos.Elementos>();
        public static List<Cliente_Agrupado.Elementos> grupo_principal = new List<Cliente_Agrupado.Elementos>();
        public static List<Cliente_Geolocalizado.Elementos> lista_cliente_localizado = new List<Cliente_Geolocalizado.Elementos>();
        public static List<ShipTo.Elementos> lista_shipto = new List<ShipTo.Elementos>();

        public Form1()
        {
            InitializeComponent();
            this.btnprocesar.Click += new System.EventHandler(this.btnprocesar_Click);
            this.btncalcular.Click += new System.EventHandler(this.btncalcular_Click);
            this.btnRutaCorta.Click += new System.EventHandler(this.btnRutaCorta_Click);
            this.btnanalizar.Click += new System.EventHandler(this.btnanalizar_Click);
            this.btnEnviarDireccion.Click += new System.EventHandler(this.btnEnviarDireccion_Click);
            this.btnConsultarDespacho.Click += new System.EventHandler(this.btnConsultarDespacho_Click);
        }

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

        public class RutaCorta
        {
            public string codoridiscli { get; set; }
            public string coddesdiscli { get; set; }
            public string disdiscli { get; set; }
            public string tiemdiscli { get; set; }
        }
        public class Geocode
        {
            
            public Results[] results { get; set; }
            public string status { get; set; }

        }
        public class Results
        {
            public Geometry geometry { get; set; }
            public Address_Components[] address_components { get; set; }
        }
        public class Address_Components
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            
            public string[] types { get; set; }
        }

        public class Types
        {
            public string types { get; set; }
        }


        public class Geometry
        {
           public Location location { get; set; }
        }
        public class Location
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }
        public class Google
        {
            public Geocode[] geocode { get; set; }
        }
        public static List<Distribucion.RutaCorta> Buscar(Distribucion.RutaCorta ca)
        {
            var cn = new SqlConnection(Conexion.Cadena);
            cn.Open();
            string sql = @"select codoriDiscli, coddesDiscli, disDiscli, tiemDiscli from[DistribucionApp].[dbo].[DisCliente] " +
                        " where disDiscli = ( SELECT MIN(disDiscli) FROM [DistribucionApp].[dbo].[DisCliente] " +
                        " WHERE codoriDiscli =" + ca.codoridiscli + " and coddesDiscli in (" + ca.coddesdiscli + "))";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Distribucion.RutaCorta Ruta = new Distribucion.RutaCorta()
                {
                    codoridiscli = int.Parse(dr["codoriDiscli"].ToString()),
                    coddesdiscli = dr["coddesDiscli"].ToString(),
                    disdiscli = dr["disDiscli"].ToString(),
                    tiemdiscli = dr["tiemDiscli"].ToString()
                };
                lista.Add(Ruta);
            }
            cn.Close();
            return lista;
        }
        public static List<Distribucion.RutaCorta> Buscar2(Distribucion.RutaCorta ca)
        {
            var cn = new SqlConnection(Conexion.Cadena);
            cn.Open();
            string sql = @"select top 1 * from[DistribucionApp].[dbo].[DisCliente] " +
                        " where disDiscli = ( SELECT MIN(disDiscli) FROM [DistribucionApp].[dbo].[DisCliente] " +
                        " WHERE codoriDiscli =" + ca.codoridiscli + " and coddesDiscli in (" + ca.coddesdiscli + ") " +
                        "and coddesDiscli not in (" + ca.disdiscli + ")) and codoriDiscli=" + ca.codoridiscli+"";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Distribucion.RutaCorta Ruta = new Distribucion.RutaCorta()
                {
                    codoridiscli =  int.Parse(dr["codoriDiscli"].ToString()),
                    coddesdiscli =  dr["coddesDiscli"].ToString(),
                    disdiscli =     dr["disDiscli"].ToString(),
                    tiemdiscli =    dr["tiemDiscli"].ToString()
            };

                lista.Add(Ruta);
            }
            cn.Close();
            return lista;
        }

        private static Distribucion.RutaCorta Crear2(IDataReader lec)
        {
            var ca = new Distribucion.RutaCorta();
            ca.codoridiscli     = int.Parse(lec["codoriDiscli"].ToString());
            ca.coddesdiscli     = lec["coddesDiscli"].ToString();
            ca.disdiscli        = lec["disDiscli"].ToString();
            ca.tiemdiscli       = lec["tiemDiscli"].ToString();
            return ca;
        }

        public DataTable GetDataSource(string SQLCmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(("Server=PCFVIS-0098;uid=SA;pwd=Vistony01;Database=DistribucionApp"));
                cnn.Open();
                SqlDataAdapter daSource = new SqlDataAdapter(SQLCmd, cnn);
                ;
                daSource.Fill(dtData);
                cnn.Close();
                daSource.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return dtData;
        }

        public DataTable GetDataSourceDistribucion(string SQLCmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(("Server=192.168.254.6;uid=report;pwd=Report01;Database=DistribucionApp"));
                cnn.Open();
                SqlDataAdapter daSource = new SqlDataAdapter(SQLCmd, cnn);
                ;
                daSource.Fill(dtData);
                cnn.Close();
                daSource.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return dtData;
        }

        public int GetDataSourceDistancia(string SQLCmd)
        {

            int  numberRecord;
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(("Server=192.168.254.6;uid=report;pwd=Report01;Database=DistribucionApp"));
                cnn.Open();
                SqlCommand cmd = new SqlCommand(SQLCmd, cnn);
                SqlDataAdapter daSource = new SqlDataAdapter(SQLCmd, cnn);
                numberRecord = Convert.ToInt32(cmd.ExecuteScalar());
                cnn.Close();
                daSource.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                numberRecord = 0;
            }
            return numberRecord;
        }
        public string QueryConsultaCliente()
        {
            string QuerySQL = "";
            QuerySQL = " select custnum,latitude_c,longitude_c from [dbo].[HojaDespacho]"+
                        " where FechaDespacho = '2018-08-14'"+
                        " and latitude_c<>'0.00000000'"+
                        " and Latitude_c<>'1.11111000'";
            return QuerySQL;
        }
        public string QueryConsultaDistancia(string cli1,string cli2) {
            string QuerySQL = "";
            QuerySQL = " SELECT codoriDiscli,coddesDiscli,disDiscli,tiemDiscli" +
                       " FROM [DistribucionApp].[dbo].[DisCliente]" +
                       " WHERE codoriDiscli = '"+cli1+"' and coddesDiscli = '"+cli2+"'";
            return QuerySQL;
        }
        public string QueryInsertarDistancia(string cliori, string clides,string dist,string dura)
        {
            string QuerySQL = "";
            QuerySQL = " insert into [DistribucionApp].[dbo].[DisCliente]" +
                       " values  ('" + cliori + "','" + clides + "','" +dist+ "','" +dura+"')";
            return QuerySQL;
        }
        private void btncalcular_Click(object sender, System.EventArgs args)
        {
            if (dgvprueba.Rows.Count != 0)
            {
                ValidaCoordenada();
                ValidaDisCliente();
            }
            else
            {
                MessageBox.Show("dgvprueba esta vacio");
            }
        }

        private void ValidaCoordenada() {
            for (int i = 0; i < dgvprueba.Rows.Count ;i++)
            {
                for (int j = 0; j < dgvprueba.ColumnCount; j++)
                {
                    if (dgvprueba.Rows[i].Cells[j].Value == "")
                    {
                        if (j == 0)
                        {
                            MessageBox.Show("Codigo en blanco");
                        }
                        if (j == 1)
                        {
                            MessageBox.Show("La Latitud en blanco del Cliente: " + dgvprueba.Rows[i].Cells[0].Value + "esta en blanco");
                        }
                        if (j == 2)
                        {
                            MessageBox.Show("La Longitud del Cliente " + dgvprueba.Rows[i].Cells[0].Value + "Esta en blanco");
                        }
                    }
                    else
                    {

                    }
                }
            }
            MessageBox.Show("Paso Validacion Coordenada");
        }
        private void ValidaDisCliente()
        {
            string cli1 = "";
            string cli2 = "";
            string SQLQuery = "";
            string resultado = "";
            for (int i = 0; i < dgvprueba.Rows.Count; i++)
            {
                for (int j = 0; j < dgvprueba.Rows.Count; j++)
                {
                    if (dgvprueba.Rows[i].Cells[0].Value != dgvprueba.Rows[j].Cells[0].Value)
                    {
                        cli1 = Convert.ToString(dgvprueba.Rows[i].Cells[0].Value);
                        cli2 = Convert.ToString(dgvprueba.Rows[j].Cells[0].Value);
                        string SQLQueryConsulta = QueryConsultaDistancia(cli1, cli2);
                        resultado = Convert.ToString(GetDataSourceDistancia(SQLQueryConsulta));
                        if ((GetDataSourceDistancia(SQLQueryConsulta))<=0)
                        {
                            try
                            {
                                string latclie1 = "";
                                string lonclie1 = "";
                                string latclie2 = "";
                                string lonclie2 = "";
                                latclie1 = Convert.ToString(dgvprueba.Rows[i].Cells[1].Value);
                                lonclie1 = Convert.ToString(dgvprueba.Rows[i].Cells[2].Value);
                                latclie2 = Convert.ToString(dgvprueba.Rows[j].Cells[1].Value);
                                lonclie2 = Convert.ToString(dgvprueba.Rows[j].Cells[2].Value);
                                string url = @"https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + latclie1 + "," + lonclie1 + "&destinations=" + latclie2 + "," + lonclie2 + "&mode=driving&language=fr-FR&avoid=tolls&key=AIzaSyC_k2m7mxljS8M4kXZE41n1t4V_Usgkq_4";
                                var web_request = (HttpWebRequest)WebRequest.Create(url);
                                using (var response = web_request.GetResponse())
                                using (var reader = new StreamReader(response.GetResponseStream()))
                                {
                                    string resultado2 = reader.ReadToEnd();
                                    string jsonRes = Convert.ToString(resultado2);
                                    RootObject respuesta = JsonConvert.DeserializeObject<RootObject>(jsonRes);
                                    if (respuesta.status == "OK")
                                    {
                                        for (int k = 0; k < respuesta.rows.Length; k++)
                                        {
                                            string distance = respuesta.rows[k].elements[k].distance.value;
                                            string duration = respuesta.rows[k].elements[k].duration.value;
                    
                                            SQLQuery = QueryInsertarDistancia(cli1, cli2, distance, duration);
                                            GetDataSourceDistancia(SQLQuery);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {  
                                    MessageBox.Show(Convert.ToString(ex));
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Finalizo Insercion");
        }
        private void btnprocesar_Click(object sender, System.EventArgs args)
        {            
            String resultado = "";
            String SQLQuery = "";
            SQLQuery = QueryConsultaCliente();
            dgvprueba.DataSource = GetDataSourceDistribucion(SQLQuery);
        }
        public string QueryConsultaRutaCorta(string cli1, string cli2)
        {
            string QuerySQL = "";

            QuerySQL = "select codoriDiscli, coddesDiscli, disDiscli, tiemDiscli from[DistribucionApp].[dbo].[DisCliente]" +
                       "where disDiscli = ( SELECT MIN(disDiscli) FROM[DistribucionApp].[dbo].[DisCliente]" +
                        "WHERE codoriDiscli = '" + cli1 + "' and coddesDiscli in (" + cli2 + "));";
            return QuerySQL;
        }
       
        private void btnRutaCorta_Click(object sender, EventArgs e)
        {
            string clientes = "";
            string NoClientes = "";
            string prueba = "";
            string Codinicio="";
            Distribucion.RutaCorta RutaCorta = new Distribucion.RutaCorta();
            var ru = new Distribucion.RutaCorta();
            for (int l = 0; l < dgvprueba.Rows.Count; l++)
            {
                if (l == 0)
                {   
                    
                    for (int k = 0; k < dgvprueba.Rows.Count; k++)
                    {
                        Codinicio = "2";
                        if (!(Codinicio.Equals(dgvprueba.Rows[k].Cells[0].Value)))
                        {
                            clientes = clientes + "," + dgvprueba.Rows[k].Cells[0].Value;

                        }
                    }
                    clientes = clientes.ToString().Substring(1, clientes.Length -1 );
                    var aux = new Distribucion.RutaCorta()
                    {
                        codoridiscli = int.Parse(Codinicio),
                        coddesdiscli = clientes
                    };
                    Buscar(aux);
                }
                else
                {
                        Codinicio = "";
                        clientes = "";
                        NoClientes = "";
                        int contador = l;
                        Codinicio = lista[l-1].coddesdiscli.ToString();

                        for (int n = 0; n < lista.Count; n++)
                        {
                            NoClientes = NoClientes + "," + lista[n].codoridiscli;

                        }
                        NoClientes = NoClientes.ToString().Substring(1, NoClientes.Length - 1);

                        for (int m = 0; m < dgvprueba.Rows.Count; m++)
                        {
                        clientes = clientes + "," + dgvprueba.Rows[m].Cells[0].Value;
                        }
                        clientes = clientes.ToString().Substring(1, clientes.Length - 1);
                        var aux2 = new Distribucion.RutaCorta()
                        {
                        codoridiscli = int.Parse(Codinicio),
                        coddesdiscli = clientes,
                        disdiscli = NoClientes 
                        };

                        Buscar2(aux2);
                }
            }
            DataTable table = ConvertListToDataTable(lista);
            dgvprueba.DataSource = table;
        }           
        static DataTable ConvertListToDataTable(List<Entidad2.Distribucion.RutaCorta> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.Distribucion.RutaCorta));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.Distribucion.RutaCorta item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        static DataTable ConvertListToDataTable2(List<Entidad2.Cliente.Elementos> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.Cliente.Elementos));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.Cliente.Elementos item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        static DataTable ConvertListToDataTable3(List<Entidad2.Cliente_Analisis.Elementos> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.Cliente_Analisis.Elementos));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.Cliente_Analisis.Elementos item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private void dgvprueba_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public DataTable GetDataClientes(string SQLCmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(("Server=SERVER07;uid=ErpDesarrollo;pwd=Edesa200*;Database=BDVistonyApp"));
                cnn.Open();
                SqlDataAdapter daSource = new SqlDataAdapter(SQLCmd, cnn);
                ;
                daSource.Fill(dtData);
                cnn.Close();
                daSource.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return dtData;
        }

        public DataTable GetDataClientesLocal(string SQLCmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(("Server=PCFVIS-0098;uid=sa;pwd=Vistony01;Database=DistribucionApp"));
                cnn.Open();
                SqlDataAdapter daSource = new SqlDataAdapter(SQLCmd, cnn);
                ;
                daSource.Fill(dtData);
                cnn.Close();
                daSource.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return dtData;
        }
        public string QueryAnalisisCliente(string custid,string shiptonum)
        {
            string QuerySQL = "";

            QuerySQL = "  SELECT CustID,ShipToNum,fechaVisita FechaVisita,Latitude_c Latitud,Length_c Longitud" +
                            " FROM [BDVistonyApp].[dbo].[HojaRuta] " +
                             " where Latitude_c is not null" +
                           " and Latitude_c<> '' " +
                           " and Latitude_c <> 'NULL'" +
                           " and custid = '" +custid+"'"+
                           " and shiptonum = '" + shiptonum + "'";
            //QuerySQL = "SELECT distinct(CustID) FROM[DistribucionApp].[dbo].[Hoja_Ruta]" +
            //            "where CUSTID NOT IN(select codCliente from[DistribucionApp].[dbo].[Cliente]); ";
            return QuerySQL;
        }
        public static List<Distribucion.RutaCorta> ConsultarCliente(Distribucion.RutaCorta ca)
        {
            var cn = new SqlConnection(Conexion.Cadena);
            cn.Open();
            string sql = @"select codoriDiscli, coddesDiscli, disDiscli, tiemDiscli from[DistribucionApp].[dbo].[DisCliente] " +
                        " where disDiscli = ( SELECT MIN(disDiscli) FROM [DistribucionApp].[dbo].[DisCliente] " +
                        " WHERE codoriDiscli =" + ca.codoridiscli + " and coddesDiscli in (" + ca.coddesdiscli + "))";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Distribucion.RutaCorta Ruta = new Distribucion.RutaCorta()
                {
                    codoridiscli = int.Parse(dr["codoriDiscli"].ToString()),
                    coddesdiscli = dr["coddesDiscli"].ToString(),
                    disdiscli = dr["disDiscli"].ToString(),
                    tiemdiscli = dr["tiemDiscli"].ToString()
                };
                lista.Add(Ruta);
            }
            cn.Close();
            return lista;
        }

        public string QueryMapeoCoordenadas(string codcliori,string shiptonum,string fechaori,string latitudori,string longitudori)
        {
            string QuerySQL = "";

            QuerySQL = " SELECT ROW_NUMBER() over(order by Name asc) as Row_origen," +
                        "'" + codcliori+"' codorigen,'"+shiptonum+"' shiptonumori,'"+fechaori+"' fecorigen, '"+latitudori+"' latorigen, '"+longitudori+"' lonorigen," +
                        " CustID coddestino,shiptonum shiptonumdes,FechaVisita fecdestino,Latitude_c latdestino,Length_c londestino, ( 6371 * ACOS(" +
                                 " COS(RADIANS("+latitudori+"))" +
                                 " * COS(RADIANS(Latitude_c))" +
                                 " * COS(RADIANS(Length_c)" +
                                 " - RADIANS("+longitudori+"))" +
                                 " + SIN(RADIANS("+latitudori+"))" +
                                 " * SIN(RADIANS(Latitude_c))" +
                                                   " )) AS distance" +
                                " FROM [BDVistonyApp].[dbo].[HojaRuta] " +
                                " where CustID = '"+codcliori+"'" +
                                " and Latitude_c<> '' and Latitude_c<> 'NULL'" +
                                "and Latitude_c <> 'NULL' " +
                                "and shiptonum = '"+shiptonum+"'" +
                                " --HAVING distance< 1" +
                                " ORDER BY distance ASC";


            //QuerySQL = "SELECT distinct(CustID) FROM[DistribucionApp].[dbo].[Hoja_Ruta]" +
            //            "where CUSTID NOT IN(select codCliente from[DistribucionApp].[dbo].[Cliente]); ";
            return QuerySQL;
        }
        static DataTable ConvertListToDataTable4(List<Entidad2.Cliente_Depurado.Elementos> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.Cliente_Depurado.Elementos));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.Cliente_Depurado.Elementos item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        static DataTable ConvertListToDataTable5(List<Entidad2.Cliente_Agrupado.Elementos> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.Cliente_Agrupado.Elementos));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.Cliente_Agrupado.Elementos item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        static DataTable ConvertListToDataTable6(List<Entidad2.Grupos.Elementos> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.Grupos.Elementos));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.Grupos.Elementos item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        static DataTable ConvertListToDataTable7(List<Entidad2.Cliente_Geolocalizado.Elementos> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.Cliente_Geolocalizado.Elementos));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.Cliente_Geolocalizado.Elementos item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public string QueryCantidadClientes()
        {
            string QuerySQL = "";

            QuerySQL = "select a.CustID CustId,a.CustNum,a.ShipToNum,COUNT(a.CustID) Cantidad from  " +
                       "[BDVistonyApp].[dbo].[HojaRuta] a  " +
                       "inner join(SELECT CustID,ShipToNum, COUNT(CustID) as cantidad " +
                       "FROM [BDVistonyApp].[dbo].[HojaRuta] " +
                        "where Latitude_c is not null " +
                         " and Latitude_c <> ''" +
                         " and Latitude_c <> 'NULL' " +
                         " group by CustID,ShipToNum) b on " +
                         " a.CustID = b.CustID " +
                         " where cantidad > 0 and " +
                         " a.Latitude_c is not null " +
                         " and a.Latitude_c <> '' " +
                         " and a.Latitude_c <> 'NULL' " +
                         " group by a.CustID,a.ShipToNum,a.CustNum " +
                         " order by CustId ";
            return QuerySQL;
        }

        private void btnanalizar_Click(object sender, System.EventArgs args)
        {
            string SQLQuery = "";

            DataTable dataTable = new DataTable();
            dataTable = GetDataClientes(QueryCantidadClientes());
            //dgvprueba.DataSource = GetDataClientes(QueryCantidadClientes());
            for (int c = 0; c < dataTable.Rows.Count; c++)
            {
                var aux = new Grupos.Elementos()
                {
                    CustId = Convert.ToString(dataTable.Rows[c]["CustId"]),
                    CustNum = Convert.ToString(dataTable.Rows[c]["CustNum"]),
                    Shipto = Convert.ToString(dataTable.Rows[c]["ShipToNum"]),
                    Cantidad = Convert.ToInt32(dataTable.Rows[c]["Cantidad"])   
                };

                listacantidadclientes.Add(aux);
            }

            DataTable dataTable2 = new DataTable();
            for (int d = 0; d < listacantidadclientes.Count; d++)
            {
                listanuevosclientes.Clear();
                dataTable2 = GetDataClientes(QueryAnalisisCliente(listacantidadclientes[d].CustId.ToString(), listacantidadclientes[d].Shipto.ToString()));

                string clientes = "";
                string NoClientes = "";
                string prueba = "";
                string Codinicio = "";
                Cliente.Elementos cliente = new Cliente.Elementos();
                for (int l = 0; l < dataTable2.Rows.Count; l++)
                {
                    var aux = new Cliente.Elementos()
                    {
                        CustId = Convert.ToString(dataTable2.Rows[l]["CustID"]),
                        Fecha = Convert.ToString(dataTable2.Rows[l]["fechaVisita"]),
                        ShipTo = Convert.ToString(dataTable2.Rows[l]["ShipToNum"]),
                        Latitud = Convert.ToString(dataTable2.Rows[l]["Latitud"]),
                        Longitud = Convert.ToString(dataTable2.Rows[l]["Longitud"])
                    };

                    listanuevosclientes.Add(aux);
                }
                DataTable dataTable3 = new DataTable();

                    grupo_principal.Clear();
                    cliente_analisis.Clear();
                    cliente_analizado.Clear();
                    cliente_depurado.Clear();
                    cliente_grupado.Clear();
                    lista_grupos.Clear();
                    for (int k = 0; k < listanuevosclientes.Count; k++)
                    {
                        string Cliori = "";
                        string ShipTo = "";
                        string Fechaori = "";
                        string Latitudori = "";
                        string Longitudori = "";

                        Cliori = listanuevosclientes[k].CustId.ToString();
                        ShipTo = listanuevosclientes[k].ShipTo.ToString();
                        Fechaori = listanuevosclientes[k].Fecha.ToString();
                        Latitudori = listanuevosclientes[k].Latitud.ToString();
                        Longitudori = listanuevosclientes[k].Longitud.ToString();
                        dataTable3 = GetDataClientes(QueryMapeoCoordenadas(Cliori,ShipTo, Fechaori, Latitudori, Longitudori));
                        //MessageBox.Show("Se genero nueva Tabla");

                        for (int j = 0; j < dataTable3.Rows.Count; j++)
                        {
                            var aux = new Cliente_Analisis.Elementos()
                            {
                                Row = Convert.ToString(dataTable3.Rows[j]["Row_origen"]),
                                codcliori = Convert.ToString(dataTable3.Rows[j]["codorigen"]),
                                shiptonumori = Convert.ToString(dataTable3.Rows[j]["shiptonumori"]),
                                fechaori = Convert.ToString(dataTable3.Rows[j]["fecorigen"]),
                                latitudori = Convert.ToString(dataTable3.Rows[j]["latorigen"]),
                                longitudori = Convert.ToString(dataTable3.Rows[j]["lonorigen"]),
                                codclides = Convert.ToString(dataTable3.Rows[j]["coddestino"]),
                                shiptonumdes = Convert.ToString(dataTable3.Rows[j]["shiptonumdes"]),
                                fechades = Convert.ToString(dataTable3.Rows[j]["fecdestino"]),
                                latituddes = Convert.ToString(dataTable3.Rows[j]["latdestino"]),
                                longituddes = Convert.ToString(dataTable3.Rows[j]["londestino"]),
                                distance = Convert.ToDecimal(dataTable3.Rows[j]["distance"]),
                            };
                            cliente_analisis.Add(aux);
                        }


                    }
                    //el problema para las coordenadas que ingresan de 2 digitos
                    for (int l = 0; l < cliente_analisis.Count; l++)
                    {
                        decimal valordistancia = 0.100m;
                        decimal valorindefinido = 0.0000949352979660034m;

                        if (!(cliente_analisis[l].distance.Equals(0)) && (cliente_analisis[l].distance) < valordistancia && !((cliente_analisis[l].distance) == valorindefinido))
                        {
                            var con = new Cliente_Analisis.Elementos()
                            {
                                Row = cliente_analisis[l].Row.ToString(),
                                codcliori = cliente_analisis[l].codcliori.ToString(),
                                shiptonumori = cliente_analisis[l].shiptonumori.ToString(),
                                fechaori = cliente_analisis[l].fechaori.ToString(),
                                latitudori = cliente_analisis[l].latitudori.ToString(),
                                longitudori = cliente_analisis[l].longitudori.ToString(),
                                codclides = cliente_analisis[l].codclides.ToString(),
                                shiptonumdes = cliente_analisis[l].shiptonumdes.ToString(),
                                fechades = cliente_analisis[l].fechades.ToString(),
                                latituddes = cliente_analisis[l].latituddes.ToString(),
                                longituddes = cliente_analisis[l].longituddes.ToString(),
                                distance = cliente_analisis[l].distance,
                            };
                            cliente_analizado.Add(con);

                        }

                    }


                    for (int q = 0; q < cliente_analizado.Count; q++)
                    {
                        for (int r = 0; r < cliente_analizado.Count; r++)
                        {
                            string valor = "";

                            if (cliente_analizado[q].latitudori == cliente_analizado[r].latituddes &&
                               cliente_analizado[q].longitudori == cliente_analizado[r].longituddes &&
                               cliente_analizado[q].shiptonumori == cliente_analizado[r].shiptonumdes &&
                               cliente_analizado[q].distance == cliente_analizado[r].distance &&
                               cliente_analizado[q].latituddes == cliente_analizado[r].latitudori &&
                               cliente_analizado[q].longituddes == cliente_analizado[r].longitudori)
                            {
                                for (int s = 0; s < cliente_depurado.Count; s++)
                                {
                                    if (cliente_depurado[s].Distance == cliente_analizado[q].distance.ToString())
                                    {
                                        valor = cliente_depurado[s].Distance.ToString();
                                    }
                                }

                                if (valor != cliente_analizado[q].distance.ToString())
                                {
                                    var aux = new Cliente_Depurado.Elementos()
                                    {
                                        LatitudOri = cliente_analizado[q].latitudori,
                                        LongitudOri = cliente_analizado[q].longitudori,
                                        ShipToNum = cliente_analizado[q].shiptonumdes,
                                        Distance = Convert.ToString(cliente_analizado[q].distance),
                                        LatitudDes = cliente_analizado[q].latituddes,
                                        LongitudDes = cliente_analizado[q].longituddes,

                                    };
                                    cliente_depurado.Add(aux);
                                }



                            }
                        }

                    }

                    int A = 0;
                    for (int m = 0; m < cliente_depurado.Count; m++)
                    {
                        string valor = "";
                        string valor2 = "";
                        string valor3 = "";
                        string valor4 = "";
                        string valor5 = "";
                        string valor6 = "";
                        string valor7 = "";
                        string valor8 = "";
                        string valorC1 = "";
                        string valorC2 = "";
                        string grupo = "";
                        valor = cliente_depurado[m].LatitudOri.ToString();
                        valor2 = cliente_depurado[m].LongitudOri.ToString();
                        valor3 = cliente_depurado[m].LatitudDes.ToString();
                        valor4 = cliente_depurado[m].LongitudDes.ToString();

                        for (int ñ = 0; ñ < cliente_grupado.Count; ñ++)
                        {
                            if (valor == cliente_grupado[ñ].Latitud.ToString()
                               && valor2 == cliente_grupado[ñ].Longitud.ToString())
                            {
                                for (int r = 0; r < cliente_grupado.Count; r++)
                                {
                                    if (valor3 == cliente_grupado[r].Latitud && valor4 == cliente_grupado[r].Longitud)
                                    {
                                        valorC1 = cliente_grupado[r].Latitud.ToString();
                                        valorC2 = cliente_grupado[r].Longitud.ToString();
                                    }
                                }
                                if (valor3 != valorC1 && valor4 != valorC2)
                                {
                                    var aux = new Cliente_Agrupado.Elementos()
                                    {
                                        Grupo = cliente_grupado[ñ].Grupo.ToString(),
                                        Latitud = valor3,
                                        Longitud = valor4,
                                    };
                                    cliente_grupado.Add(aux);
                                    valor5 = cliente_grupado[ñ].Latitud.ToString();
                                    valor6 = cliente_grupado[ñ].Longitud.ToString();
                                }

                            }
                            if (valor3 == cliente_grupado[ñ].Latitud.ToString()
                               && valor4 == cliente_grupado[ñ].Longitud.ToString())
                            {
                                for (int r = 0; r < cliente_grupado.Count; r++)
                                {
                                    if (valor == cliente_grupado[r].Latitud && valor2 == cliente_grupado[r].Longitud)
                                    {
                                        valorC1 = cliente_grupado[r].Latitud.ToString();
                                        valorC2 = cliente_grupado[r].Longitud.ToString();
                                    }
                                }
                                if (valor != valorC1 && valor2 != valorC2)
                                {
                                    var aux = new Cliente_Agrupado.Elementos()
                                    {
                                        Grupo = cliente_grupado[ñ].Grupo.ToString(),
                                        ShipToNum = cliente_grupado[ñ].ShipToNum.ToString(),
                                        Latitud = valor,
                                        Longitud = valor2,
                                    };
                                    cliente_grupado.Add(aux);
                                    valor7 = cliente_grupado[ñ].Latitud.ToString();
                                    valor8 = cliente_grupado[ñ].Longitud.ToString();
                                }
                            }

                        }
                        if ((valor != valor5 && valor2 != valor6) && (valor3 != valor7) && (valor4 != valor8))
                        {
                            A++;
                            for (int r = 0; r < cliente_grupado.Count; r++)
                            {
                                if (valor == cliente_grupado[r].Latitud && valor2 == cliente_grupado[r].Longitud)
                                {
                                    valorC1 = cliente_grupado[r].Latitud.ToString();
                                    valorC2 = cliente_grupado[r].Longitud.ToString();
                                }
                            }
                            if (valor != valorC1 && valor2 != valorC2)
                            {
                                var aux = new Cliente_Agrupado.Elementos()
                                {
                                    Grupo = Convert.ToString(A),
                                    ShipToNum = cliente_depurado[m].ShipToNum.ToString(),
                                    Latitud = valor,
                                    Longitud = valor2,
                                };
                                cliente_grupado.Add(aux);
                            }

                            for (int r = 0; r < cliente_grupado.Count; r++)
                            {
                                if (valor3 == cliente_grupado[r].Latitud && valor4 == cliente_grupado[r].Longitud)
                                {
                                    valorC1 = cliente_grupado[r].Latitud.ToString();
                                    valorC2 = cliente_grupado[r].Longitud.ToString();
                                }
                            }
                            if (valor3 != valorC1 && valor4 != valorC2)
                            {
                                var aux2 = new Cliente_Agrupado.Elementos()
                                {
                                    Grupo = Convert.ToString(A),
                                    ShipToNum = cliente_depurado[m].ShipToNum.ToString(),
                                    Latitud = valor3,
                                    Longitud = valor4,
                                };
                                cliente_grupado.Add(aux2);
                            }

                        }

                        for (int n = 0; n < cliente_analizado.Count; n++)
                        {
                            if ((valor == cliente_analizado[n].latitudori.ToString()) &&
                                (valor2 == cliente_analizado[n].longitudori.ToString()) ||
                                (valor == cliente_analizado[n].latituddes.ToString()) &&
                                (valor2 == cliente_analizado[n].longituddes.ToString()) ||
                                (valor3 == cliente_analizado[n].latitudori.ToString()) &&
                                (valor3 == cliente_analizado[n].longitudori.ToString()) ||
                                (valor4 == cliente_analizado[n].latituddes.ToString()) &&
                                (valor4 == cliente_analizado[n].longituddes.ToString()))
                            {
                                if (valor == cliente_analizado[n].latitudori.ToString() &&
                                    valor2 == cliente_analizado[n].longitudori.ToString())
                                {
                                    for (int o = 0; o < cliente_grupado.Count; o++)
                                    {
                                        if (valor == cliente_grupado[o].Latitud.ToString() &&
                                            valor2 == cliente_grupado[o].Longitud.ToString())
                                        {
                                            grupo = cliente_grupado[o].Grupo.ToString();
                                        }
                                    }

                                    for (int p = 0; p < cliente_grupado.Count; p++)
                                    {
                                        if (cliente_analizado[n].latituddes.ToString()
                                            == cliente_grupado[p].Latitud &&
                                            cliente_analizado[n].longituddes.ToString()
                                            == cliente_grupado[p].Longitud)
                                        {
                                            cliente_grupado[p].Grupo = grupo;
                                        }

                                    }

                                }
                                if (valor == cliente_analizado[n].latituddes.ToString() &&
                                    valor2 == cliente_analizado[n].longituddes.ToString())
                                {
                                    for (int o = 0; o < cliente_grupado.Count; o++)
                                    {
                                        if (valor == cliente_grupado[o].Latitud.ToString() &&
                                            valor2 == cliente_grupado[o].Longitud.ToString())
                                        {
                                            grupo = cliente_grupado[o].Grupo.ToString();
                                        }
                                    }

                                    for (int p = 0; p < cliente_grupado.Count; p++)
                                    {
                                        if (cliente_analizado[n].latitudori.ToString()
                                            == cliente_grupado[p].Latitud &&
                                            cliente_analizado[n].longitudori.ToString()
                                            == cliente_grupado[p].Longitud)
                                        {
                                            cliente_grupado[p].Grupo = grupo;
                                        }

                                    }

                                }
                                if (valor3 == cliente_analizado[n].latitudori.ToString() &&
                                   valor4 == cliente_analizado[n].longitudori.ToString())
                                {
                                    for (int o = 0; o < cliente_grupado.Count; o++)
                                    {
                                        if (valor3 == cliente_grupado[o].Latitud.ToString() &&
                                            valor4 == cliente_grupado[o].Longitud.ToString())
                                        {
                                            grupo = cliente_grupado[o].Grupo.ToString();
                                        }
                                    }

                                    for (int p = 0; p < cliente_grupado.Count; p++)
                                    {
                                        if (cliente_analizado[n].latitudori.ToString()
                                            == cliente_grupado[p].Latitud &&
                                            cliente_analizado[n].longitudori.ToString()
                                            == cliente_grupado[p].Longitud)
                                        {
                                            cliente_grupado[p].Grupo = grupo;
                                        }

                                    }

                                }
                                if (valor3 == cliente_analizado[n].latituddes.ToString() &&
                                   valor4 == cliente_analizado[n].longituddes.ToString())
                                {
                                    for (int o = 0; o < cliente_grupado.Count; o++)
                                    {
                                        if (valor3 == cliente_grupado[o].Latitud.ToString() &&
                                            valor4 == cliente_grupado[o].Longitud.ToString())
                                        {
                                            grupo = cliente_grupado[o].Grupo.ToString();
                                        }
                                    }

                                    for (int p = 0; p < cliente_grupado.Count; p++)
                                    {
                                        if (cliente_analizado[n].latituddes.ToString()
                                            == cliente_grupado[p].Latitud &&
                                            cliente_analizado[n].longituddes.ToString()
                                            == cliente_grupado[p].Longitud)
                                        {
                                            cliente_grupado[p].Grupo = grupo;
                                        }

                                    }

                                }
                            }

                        }
                    }
                    string nombre_grupo = "";
                    for (int v = 0; v < cliente_grupado.Count; v++)
                    {
                        for (int y = 0; y < lista_grupos.Count; y++)
                        {
                            if (cliente_grupado[v].Grupo.ToString() == lista_grupos[y].CustId.ToString())
                            {
                                nombre_grupo = cliente_grupado[v].Grupo.ToString();
                            }
                        }
                        if (cliente_grupado[v].Grupo.ToString() == nombre_grupo)
                        {
                            for (int x = 0; x < lista_grupos.Count; x++)
                            {
                                if (cliente_grupado[v].Grupo == lista_grupos[x].CustId)
                                {
                                    lista_grupos[x].Cantidad = lista_grupos[x].Cantidad + 1;
                                }
                            }
                        }
                        if (cliente_grupado[v].Grupo.ToString() != nombre_grupo)
                        {
                            var aux3 = new Grupos.Elementos()
                            {
                                CustId = cliente_grupado[v].Grupo.ToString(),
                                CustNum = cliente_grupado[v].Grupo.ToString(),
                                Shipto = cliente_grupado[v].Grupo.ToString(),
                                Cantidad = 1

                            };
                            lista_grupos.Add(aux3);
                        }
                    }
                    int valmax = 0;
                    if (lista_grupos.Count > 0)
                    {
                        int valMax = lista_grupos.Max(Grupos => Grupos.Cantidad);
                        string grupoprincipal = "";
                        //MessageBox.Show("Se genero nueva Tabla", Convert.ToString(valMax));
                        for (int z = 0; z < lista_grupos.Count; z++)
                        {
                            if (valMax == lista_grupos[z].Cantidad)
                            {
                                grupoprincipal = lista_grupos[z].CustId.ToString();
                            }
                        }

                        for (int w = 0; w < cliente_grupado.Count; w++)
                        {
                            if (grupoprincipal == cliente_grupado[w].Grupo)
                            {
                                var aux4 = new Cliente_Agrupado.Elementos()
                                {
                                    Grupo = grupoprincipal,
                                    ShipToNum = cliente_grupado[w].ShipToNum,
                                    Latitud = cliente_grupado[w].Latitud,
                                    Longitud = cliente_grupado[w].Longitud

                                };
                                grupo_principal.Add(aux4);
                            }
                        }

                        double sumalat = 0;
                        double sumalon = 0;
                        double latpromedio = 0;
                        double lonpromedio = 0;
                        for (int t = 0; t < grupo_principal.Count; t++)
                        {
                            sumalat = sumalat + Convert.ToDouble(grupo_principal[t].Latitud);
                            sumalon = sumalon + Convert.ToDouble(grupo_principal[t].Longitud);
                        }

                        latpromedio = sumalat / valMax;
                        lonpromedio = sumalon / valMax;

                        var aux10 = new Cliente_Geolocalizado.Elementos()
                        {
                            Codigo = listacantidadclientes[d].CustId,
                            CustNum = listacantidadclientes[d].CustNum,
                            ShipToNum = listacantidadclientes[d].Shipto,
                            Latitud = latpromedio.ToString(),
                            Longitud = lonpromedio.ToString()

                        };
                        lista_cliente_localizado.Add(aux10);
                    }
                    
                
                if (listanuevosclientes.Count == 1)
                {
                    var aux11 = new Cliente_Geolocalizado.Elementos()
                    {
                        Codigo = listacantidadclientes[d].CustId.ToString(),
                        CustNum = listacantidadclientes[d].CustNum.ToString(),
                        ShipToNum = listacantidadclientes[d].Shipto.ToString(),
                        Latitud = listanuevosclientes[0].Latitud.ToString(),
                        Longitud = listanuevosclientes[0].Longitud.ToString()

                    };
                    lista_cliente_localizado.Add(aux11);
                    
                }
            }
            
            DataTable tableCliente = ConvertListToDataTable7(lista_cliente_localizado);
            dgvprueba.DataSource = tableCliente;        
            
        }

        public string  QueryEnviarDireccion()
        {
            string SQLQuery = "";
            SQLQuery = "select top 5000 a.CustID,a.CustNum,a.name," +
                        "replace(replace(replace(REPLACE(REPLACE(REPLACE(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(e.direccion, 'AV.', ''), 'NRO.', ''), 'N°', ''), 'Nª', ''), 'CAL.', ''), 'Nº', ''), 'S/N', ''), 'MZ', ''), ' LT ', ''), '-', ''), 'JR', ''), 'CENTRO', '')" +
                        ", " + "'/'" + ", ''), 'PJ.', ''), 'AV ', ''), 'CAR.', ''), '.', ''), ' NRO ', '') address,  " +
                        "b.ShipToNum, " +
                        "c.District_c district, Departmen_c departmen,b.Latitud_c latitud, b.Longitud_c longitud " +
                        "from customer a " +
                        "inner join ShipTo b on " +
                        "a.Company = b.Company and " +
                        "a.CustNum = b.CustNum " +
                        "inner join Region c on " +
                        "b.Company = c.Company and " +
                         "b.RegionCode_c = c.RegionCode " +
                         "inner join(SELECT company, custnum FROM InvcHead " +
                         "where invoicedate > '01-01-2017' or invoicebal<> 0 " +
                         "group by company,custnum) d on " +
                         "a.Company = d.Company and " +
                         "a.CustNum = d.CustNum " +
                         "inner join(SELECT SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(SUBSTRING(Address_c,1,  " +
                         "(case when charindex('urb',Address_c)> 0 then charindex('urb',Address_c)-1 " +
                         "when charindex('urb',Address_c)= 0 then LEN(Address_c) end)),1,  " +
                         "(case when charindex('(',Address_c)> 0 then charindex('(',Address_c)-1 " +
                         "when charindex('(',Address_c)= 0 then LEN(Address_c) end)  " +
                         "),1,(case when charindex('MZA',Address_c)> 0 then charindex('MZA',Address_c)-1 " +
                         "when charindex('MZA',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('BARRIO',Address_c)> 0 then charindex('BARRIO',Address_c)-1 " +
                         "when charindex('BARRIO',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('AAHH',Address_c)> 0 then charindex('AAHH',Address_c)-1 " +
                         "when charindex('AAHH',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('A.H.',Address_c)> 0 then charindex('A.H.',Address_c)-1 " +
                         "when charindex('A.H.',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('S/N',Address_c)> 0 then charindex('S/N',Address_c)-1 " +
                         "when charindex('S/N',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('MZ',Address_c)> 0 then charindex('MZ',Address_c)-1 " +
                         "when charindex('MZ',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('/',Address_c)> 0 then charindex('/',Address_c)-1 " +
                         "when charindex('/',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('P.J.',Address_c)> 0 then charindex('P.J.',Address_c)-1 " +
                         "when charindex('P.J.',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex(RTRIM(LTRIM(y.District_c)),Address_c)> 0 then charindex(RTRIM(LTRIM(y.District_c)),Address_c)-1 " +
                         "when charindex(RTRIM(LTRIM(y.District_c)),Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex('INT.',Address_c)> 0 then charindex('INT.',Address_c)-1 " +
                         "when charindex('INT.',Address_c)= 0 then LEN(Address_c) end)),1, " +
                         "(case when charindex(RTRIM(LTRIM(y.Departmen_c)),Address_c)> 0 then charindex(RTRIM(LTRIM(y.Departmen_c)),Address_c)-1 " +
                         "when charindex(RTRIM(LTRIM(y.Departmen_c)),Address_c)= 0 then LEN(Address_c) end))  " +
                        "direccion,x.Company,x.CustNum,x.ShipToNum FROM ShipTo x " +
                        "inner join Region y on " +
                        "x.Company = y.Company and " +
                        "x.RegionCode_c = y.RegionCode " +
                         ") e on " +
                         "b.Company = e.company and " +
                         "b.CustNum = e.CustNum and " +
                         "b.ShipToNum = e.ShipToNum " +
                         "where ValidPayer<>0 and " +
                         "ValidSoldTo <> 0 and " +
                         "ValidShipTo <> 0 " +
                         "and a.company = 'C001' " +
                         "and b.chkDisable_c = 0 " +
                         "and b.Latitud_c IN(1.111,1.11,1.1,1,0,1.1111) " +
                         "order by CustID ";


         return SQLQuery;
        }

        public string QueryPruebaGeocodigo()
        {
            string SQLQuery = "";
            SQLQuery = "select " +
                    "CustID_c CustID, " +
                    "D.CustNum CustNum, " +
                    "CustomerName_c name, " +
                    "Address_c address, " +
                    "d.ShipToNum, " +
                    "'' district, " +
                    "'' departmen, " +
                    "d.Latitud_c Latitud, " +
                    "d.Longitud_c Longitud " +
                    "from SERVER07.ERP10DB.DBO.UD30 a " +
                    "inner join SERVER07.ERP10DB.DBO.UD31 b on " +
                    "a.Company = b.Company and " +
                    "a.Key1 = b.key1 " +
                    "inner join SERVER07.ERP10DB.DBO.ShipHead c on " +
                    "b.Company = c.Company and " +
                    "b.PackNum_c = c.PackNum " +
                    "left outer join SERVER07.ERP10DB.DBO.ShipTo d on " +
                    "d.company = c.Company and " +
                    "d.CustNum = c.CustNum and " +
                    "d.ShipToNum = c.ShipToNum " +
                    "left outer join SERVER07.ERP10DB.DBO.Region e on " +
                    "e.Company = d.Company and " +
                    "e.RegionCode = d.RegionCode_c " +
                    "left outer join SERVER07.ERP10DB.DBO.Customer f on " +
                    "c.company = f.Company and " +
                    "c.CustNum = f.CustNum " +
                    "where DateShipping_c = '2018-04-05' and SalesRepCode_c = '10378592' " +
                    "and b.company = 'C001' ";

            return SQLQuery;

            
        }


        public DataTable GetDataERP10DB(string SQLCmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(("Server=192.168.254.7;uid=ErpDesarrollo;pwd=Edesa200*;Database=ERP10DB"));
                cnn.Open();
                SqlDataAdapter daSource = new SqlDataAdapter(SQLCmd, cnn);
                ;
                daSource.Fill(dtData);
                cnn.Close();
                daSource.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return dtData;
        }
        static DataTable ConvertListToDataTable8(List<Entidad2.ShipTo.Elementos> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Entidad2.ShipTo.Elementos));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (Entidad2.ShipTo.Elementos item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private void btnEnviarDireccion_Click(object sender, System.EventArgs args)
        {
            DataTable tableshipto = GetDataERP10DB(QueryPruebaGeocodigo());
            for (int sh = 0; sh < tableshipto.Rows.Count; sh++)
            {
                var shipto = new ShipTo.Elementos()
                {
                    CustId = Convert.ToString(tableshipto.Rows[sh]["custid"]),
                    CustNum = Convert.ToString(tableshipto.Rows[sh]["custnum"]),
                    Name = Convert.ToString(tableshipto.Rows[sh]["name"]),
                    ShipToNum = Convert.ToString(tableshipto.Rows[sh]["shiptonum"]),
                    Address = Convert.ToString(tableshipto.Rows[sh]["address"]),
                    District = Convert.ToString(tableshipto.Rows[sh]["district"]),
                    Departmen = Convert.ToString(tableshipto.Rows[sh]["departmen"]),
                    Latitud = Convert.ToString(tableshipto.Rows[sh]["Latitud"]),
                    Longitud = Convert.ToString(tableshipto.Rows[sh]["Longitud"]),

                };
                lista_shipto.Add(shipto);
            }
            

           /* for (int sh2 = 0; sh2<lista_shipto.Count; sh2++)
            {
                try
                {
                    string name = lista_shipto[sh2].Name.ToString();
                    string direccion = lista_shipto[sh2].Address.ToString();
                    string distrito = lista_shipto[sh2].District.ToString();
                    string departamento = lista_shipto[sh2].Departmen.ToString();

                   string url = @"https://maps.googleapis.com/maps/api/geocode/json?address=" + direccion + " - " + distrito + "&key=AIzaSyDAqwKC06eCZpcEasgP8DrRKU9kiUhR88s";
                    var web_request = (HttpWebRequest)WebRequest.Create(url);
                    using (var response = web_request.GetResponse())
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string resultado2 = reader.ReadToEnd();
                        string jsonRes2 = Convert.ToString(resultado2);
                        string Latitud = "";
                        string Longitud = "";
                        Geocode respuesta2 = JsonConvert.DeserializeObject<Geocode>(jsonRes2);
                        if (respuesta2.status == "OK")
                        {
                            for (int k = 0; k < respuesta2.results.Length; k++)
                            {
                                Latitud = respuesta2.results[k].geometry.location.lat;
                                Longitud = respuesta2.results[k].geometry.location.lng;
                                //MessageBox.Show(Latitud);
                                //MessageBox.Show(Longitud);
                            }
                            lista_shipto[sh2].Latitud = Latitud;
                            lista_shipto[sh2].Longitud = Longitud;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }

            }
            */

            for (int zzz = 0; zzz < lista_shipto.Count; zzz++)
            {
                try
                {
                    string latitud = lista_shipto[zzz].Latitud.ToString();
                    string longitud = lista_shipto[zzz].Longitud.ToString();
                    //string distrito = lista_shipto[sh2].District.ToString();
                    //string departamento = lista_shipto[sh2].Departmen.ToString();

                    string url = @"https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitud + " , " + longitud + "&key=AIzaSyDAqwKC06eCZpcEasgP8DrRKU9kiUhR88s";
                    var web_request = (HttpWebRequest)WebRequest.Create(url);
                    using (var response = web_request.GetResponse())
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string resultado3 = reader.ReadToEnd();
                        string jsonRes3 = Convert.ToString(resultado3);
                        string distrito1 = "";
                        string distrito2 = "";
                        Geocode respuesta3 = JsonConvert.DeserializeObject<Geocode>(jsonRes3);
                        if (respuesta3.status == "OK")
                        {
                            for (int k = 0; k < respuesta3.results.Length; k++)
                            {
                                for (int j = 0; j < respuesta3.results[k].address_components.Length; j++)
                                {
                                    for (int l = 0; l < respuesta3.results[k].address_components[j].types.Length; l++)
                                    {
                                        if (respuesta3.results[k].address_components[j].types[l].Equals("locality"))
                                        {
                                            if (!(lista_shipto[zzz].District.Equals(respuesta3.results[k].address_components[j].long_name)))
                                            {
                                                lista_shipto[zzz].Latitud = "";
                                                lista_shipto[zzz].Longitud = "";
                                            }
                                        }
                                    }
                                }
                             
                                //MessageBox.Show(Latitud);
                                //MessageBox.Show(Longitud);
                            }
                            //lista_shipto[sh2].Latitud = Latitud;
                            //lista_shipto[sh2].Longitud = Longitud;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }

            }

            DataTable tableshipto2 = ConvertListToDataTable8(lista_shipto);
            dgvprueba.DataSource = tableshipto2;



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultarDespacho_Click(object sender, EventArgs e)
        {
            String resultado = "";
            String SQLQuery = "";
            SQLQuery = QueryConsultaPrueba();
            dgvprueba.DataSource = GetDataClientesPrueba(SQLQuery);
        }

        public string QueryConsultaPrueba()
        {
            string QuerySQL = "";

            QuerySQL = "SELECT [codoriDiscli] "+
                       ",[coddesDiscli] "+
                       ",[disDiscli] "+
                       "FROM[DistribucionApp].[dbo].[DisCliente_Test2] "+
                       "where codoridiscli in ( "+
                       "'1','2','3','4','5','6')";
            return QuerySQL;
        }

        public DataTable GetDataClientesPrueba(string SQLCmd)
        {
            DataTable dtData = new DataTable();
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(("Server=192.168.254.6;uid=sa;pwd=W3bv1st0;Database=DistribucionApp"));
                cnn.Open();
                SqlDataAdapter daSource = new SqlDataAdapter(SQLCmd, cnn);
                ;
                daSource.Fill(dtData);
                cnn.Close();
                daSource.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return dtData;
        }
    }
}
