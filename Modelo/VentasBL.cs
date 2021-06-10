using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class VentasBL
    {
        public List<TotalVenta> MostrarVentasPorNombre(TotalVenta pEN)
        {
            IDbConnection _conn = DBComon.Conexion();
            _conn.Open();
            SqlCommand _Command = new SqlCommand("CONSULTAR_VENTAS_POR_NOMBRE", _conn as SqlConnection);
            _Command.CommandType = CommandType.StoredProcedure;
            _Command.Parameters.Add(new SqlParameter("@NOMBRE", pEN.Nombre));
            IDataReader _reader = _Command.ExecuteReader();
            List<TotalVenta> Lista = new List<TotalVenta>();
            while (_reader.Read())
            {
                TotalVenta _ventas = new TotalVenta();
                _ventas.id = _reader.GetInt64(0);
                _ventas.Nombre = _reader.GetString(1);
                _ventas.Total_ventas = _reader.GetInt64(2);
                _ventas.Estado = _reader.GetString(3);
                Lista.Add(_ventas);
            }
            _conn.Close();
            return Lista;
        }
    }
}
