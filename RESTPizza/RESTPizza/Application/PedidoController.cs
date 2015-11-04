using RESTPizza.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RESTPizza.Application
{
    public class PedidoController : ApiController
    {
        public PedidoService _pedidoService { get; set; }

        public PedidoController()
        {
            _pedidoService = new PedidoService();
        }

        [ResponseType(typeof(Pedido))]
        public IHttpActionResult Get(int id)
        {
            var pedido = _pedidoService.Obter().Where(p => p.PedidoID == id).SingleOrDefault();

            return Ok(pedido);
        }

        [ResponseType(typeof(Pedido))]
        public IHttpActionResult Get()
        {
            var pedidos = _pedidoService.Obter().ToList();

            return Ok(pedidos);
        }

        [HttpPost]
        [ResponseType(typeof(Pizza))]
        public IHttpActionResult Cadastrar(Pedido pedido)
        {
            List<string> errosValidacao;
            _pedidoService.RealizarNovo(pedido, out errosValidacao);

            if (errosValidacao.Count == 0)
            {
                var urlCompleta = ConfigurationManager.AppSettings["UrlPrincipalRaiz"] + @"/api/pedido/" + pedido.PedidoID;
                return Created(new Uri(urlCompleta), pedido);
            }
            else
            {
                return BadRequest(errosValidacao.Aggregate((a, b) => { return a + ", " + b; }));
            }
        }
    }
}
