using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public Usuario oUsuario { get; set; }
        public string TipoPedido { get; set; }
        public string PedidoDatos { get; set; }
        public string PedidoDestino { get; set; }
        public bool Activo { get; set; }


    }
}
