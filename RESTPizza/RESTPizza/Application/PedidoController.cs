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
using System.Net;
using RESTPizza.Application.DTO;

namespace RESTPizza.Application
{
    public class PedidoController : ApiController
    {
        private PedidoService _pedidoService;
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
        [ResponseType(typeof(IEnumerable<PedidoDTO>))]
        public IHttpActionResult Obter()
        {
            var pedidosDTO = _pedidoService.Obter()
                                    .Where(p => p.Situacao == (int)Enums.SituacaoPedido.AguardandoAtendimento)
                                    .ToList()
                                    .Select(e => new PedidoDTO().InjectFrom(e))
                                    .Cast<PedidoDTO>()
                                    .Select(e => e.GerarLinks(_urlBase, PedidoEstadoAtualDaAplicacao.ObterPedido));


            return Ok(pedidosDTO);
        }

        [HttpPost]
        [ResponseType(typeof(PedidoDTO))]
        public IHttpActionResult Cadastrar(PedidoDTO pedidoDTO)
        {
            _HATEOASManager = new PedidoHATEOASManager(_urlBase, PedidoEstadoAtualDaAplicacao.CadastrarPedido);

            List<string> errosValidacao;

            var pedido = new Pedido();
            pedido.InjectFrom(pedidoDTO);

            _pedidoService.RealizarNovo(pedido, out errosValidacao);
            pedidoDTO.InjectFrom(pedido);
            pedidoDTO.Links = _HATEOASManager.ObterLinks(pedidoDTO);

            if (errosValidacao.Count == 0)
                return Created(new Uri(_urlBase + pedidoDTO.PedidoID), pedidoDTO);
            else
                return BadRequest(errosValidacao.Aggregate((a, b) => { return a + ", " + b; }));
        }

        [HttpPut]
        [Route("~/api/pedido/{id}/aprovar")]
        [ResponseType(typeof(PedidoDTO))]
        public IHttpActionResult Aprovar(int id)
        {
            var pedido = _pedidoService.Aprovar(id);
            var pedidoDTO = new PedidoDTO();
            pedidoDTO.InjectFrom(pedido);

            return Ok(pedido);
        }

        [HttpPut]
        [Route("~/api/pedido/{id}/rejeitar")]
        public IHttpActionResult Rejeitar(int id)
        {
            _pedidoService.Rejeitar(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
