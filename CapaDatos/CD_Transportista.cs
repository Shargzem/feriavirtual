using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Transportista
    {
        public static CD_Transportista _instancia = null;

        private CD_Transportista()
        {

        }

        public static CD_Transportista Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Transportista();
                }
                return _instancia;
            }
        }


        public List<Transportista> ObtenerTransportistas()
        {
            List<Transportista> rptListaTransportista = new List<Transportista>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerTransportista", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaTransportista.Add(new Transportista()
                        {
                            IdTransportista = Convert.ToInt32(dr["IdTransportista"].ToString()),
                            TipoDocumento = dr["TipoDocumento"].ToString(),
                            NumeroDocumento = dr["NumeroDocumento"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"])

                        });
                    }
                    dr.Close();

                    return rptListaTransportista;

                }
                catch (Exception ex)
                {
                    rptListaTransportista = null;
                    return rptListaTransportista;
                }
            }
        }


        public bool RegistrarTransportista(Transportista oTransportista)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarTransportista", oConexion);
                    cmd.Parameters.AddWithValue("TipoDocumento", oTransportista.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", oTransportista.NumeroDocumento);
                    cmd.Parameters.AddWithValue("Nombre", oTransportista.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", oTransportista.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", oTransportista.Telefono);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool ModificarTransportista(Transportista oTransportista)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarTransportista", oConexion);
                    cmd.Parameters.AddWithValue("IdTransportista", oTransportista.IdTransportista);
                    cmd.Parameters.AddWithValue("TipoDocumento", oTransportista.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", oTransportista.NumeroDocumento);
                    cmd.Parameters.AddWithValue("Nombre", oTransportista.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", oTransportista.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", oTransportista.Telefono);
                    cmd.Parameters.AddWithValue("Activo", oTransportista.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;

        }

        public bool EliminarTransportista(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarTransportista", oConexion);
                    cmd.Parameters.AddWithValue("IdTransportista", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


    }
}
