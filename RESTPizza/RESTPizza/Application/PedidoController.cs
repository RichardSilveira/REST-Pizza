using RESTPizza.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Omu.ValueInjecter;
using RESTPizza.Application.HATEOAS;

namespace RESTPizza.Application
{
    public class PedidoController : ApiController
    {
        public PedidoService _pedidoService { get; set; }
        private string _urlBase;
        private PedidoHATEOASManager _HATEOASManager;

        public PedidoController()
        {
            _urlBase = ConfigurationManager.AppSettings["UrlPrincipalRaiz"] + @"/api/pedido/";
            _pedidoService = new PedidoService();
        }

        [HttpGet]
        [ResponseType(typeof(PedidoDTO))]
        public IHttpActionResult Obter(int id)
        {
            _HATEOASManager = new PedidoHATEOASManager(_urlBase, PedidoEstadoAtualDaAplicacao.ObterPedido);

            var pedidoDTO = new PedidoDTO();
            var pedido = _pedidoService.Obter().Where(p => p.PedidoID == id).SingleOrDefault();
            pedidoDTO.InjectFrom(pedido);

            pedidoDTO.Links = _HATEOASManager.ObterLinks(pedidoDTO);
            return Ok(pedidoDTO);
        }

        [HttpGet]
        [ResponseType(typeof(PedidoDTO))]
        public IHttpActionResult Obter()
        {
            var pedidos = _pedidoService.Obter().ToList();

            return Ok(pedidos);
        }

        [HttpPost]
        [ResponseType(typeof(PedidoDTO))]
        public IHttpActionResult Cadastrar(PedidoDTO pedidoDTO)
        {
            List<string> errosValidacao;

            var pedido = new Pedido();
            pedido.InjectFrom(pedidoDTO);

            _pedidoService.RealizarNovo(pedido, out errosValidacao);
            pedidoDTO.InjectFrom(pedido);

            if (errosValidacao.Count == 0)
                return Created(new Uri(_urlBase + pedidoDTO.PedidoID), pedidoDTO);
            else
                return BadRequest(errosValidacao.Aggregate((a, b) => { return a + ", " + b; }));
        }
    }
}
