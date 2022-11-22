using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class PedidoController : Controller
    {
        private static Usuario SesionUsuario;
        // GET: Pedido
        public ActionResult Crear()
        {
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        public JsonResult Obtener()
        {
            
            List<Pedido> oListaPedido = CD_Pedido.Instancia.ObtenerPedidos();
            return Json(new { data = oListaPedido }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerUsuario()
        {
            Usuario rptUsuario = CD_Usuario.Instancia.ObtenerDetalleUsuario(SesionUsuario.IdUsuario);
            return Json(rptUsuario, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Pedido objeto)
        {

            bool respuesta = false;

            if (objeto.IdPedido == 0)
            {
                respuesta = CD_Pedido.Instancia.RegistrarPedido(objeto);
            }
            else
            {
                respuesta = CD_Pedido.Instancia.ModificarPedido(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Pedido.Instancia.EliminarPedido(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}