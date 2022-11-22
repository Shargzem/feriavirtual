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
    public class CD_Pedido
    {
        public static CD_Pedido _instancia = null;

        private CD_Pedido()
        {

        }

        public static CD_Pedido Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Pedido();
                }
                return _instancia;
            }
        }


        public List<Pedido> ObtenerPedidos()
        {
            List<Pedido> rptListaPedido = new List<Pedido>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPedido", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPedido.Add(new Pedido()
                        {
                            IdPedido = Convert.ToInt32(dr["IdPedido"].ToString()),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                            oUsuario = new Usuario() {Nombres = dr["Nombres"].ToString() },
                            TipoPedido = dr["TipoPedido"].ToString(),
                            PedidoDatos = dr["PedidoDatos"].ToString(),
                            PedidoDestino = dr["PedidoDestino"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"])

                        });
                    }
                    dr.Close();

                    return rptListaPedido;

                }
                catch (Exception ex)
                {
                    rptListaPedido = null;
                    return rptListaPedido;
                }
            }
        }


        public bool RegistrarPedido(Pedido oPedido)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("usp_RegistrarPedido", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", oPedido.IdUsuario);
                    cmd.Parameters.AddWithValue("TipoPedido", oPedido.TipoPedido);
                    cmd.Parameters.AddWithValue("PedidoDatos", oPedido.PedidoDatos);
                    cmd.Parameters.AddWithValue("PedidoDestino", oPedido.PedidoDestino);
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

        public bool ModificarPedido(Pedido oPedido)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarPedido", oConexion);
                    cmd.Parameters.AddWithValue("IdPedido", oPedido.IdPedido);
                    //cmd.Parameters.AddWithValue("IdUsuario", oPedido.IdUsuario);
                    cmd.Parameters.AddWithValue("TipoPedido", oPedido.TipoPedido);
                    cmd.Parameters.AddWithValue("PedidoDatos", oPedido.PedidoDatos);
                    cmd.Parameters.AddWithValue("PedidoDestino", oPedido.PedidoDestino);
                    cmd.Parameters.AddWithValue("Activo", oPedido.Activo);
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

        public bool EliminarPedido(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarPedido", oConexion);
                    cmd.Parameters.AddWithValue("IdPedido", id);
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
