using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentasWeb.Controllers
{
    public class TransportistaController : Controller
    {
        // GET: Cliente
        public ActionResult Crear()
        {
            return View();
        }

        public JsonResult Obtener()
        {
            List<Transportista> oListaTransportista = CD_Transportista.Instancia.ObtenerTransportistas();
            return Json(new { data = oListaTransportista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Transportista objeto)
        {
            bool respuesta = false;

            if (objeto.IdTransportista == 0)
            {
                respuesta = CD_Transportista.Instancia.RegistrarTransportista(objeto);
            }
            else
            {
                respuesta = CD_Transportista.Instancia.ModificarTransportista(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Transportista.Instancia.EliminarTransportista(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}