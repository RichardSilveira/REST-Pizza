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
            var pedidoDTO = new PedidoDTO();
            var pedido = _pedidoService.Obter().Where(p => p.PedidoID == id).SingleOrDefault();
            pedidoDTO.InjectFrom(pedido);

            _HATEOASManager = new PedidoHATEOASManager(_urlBase, PedidoEstadoAtualDaAplicacao.ObterPedidoUnico,
                                                        pedidoDTO);

            pedidoDTO.Links = _HATEOASManager.ObterLinks();

            return Ok(pedidoDTO);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<PedidoDTO>))]
        public IHttpActionResult Obter()
        {
            var pedidosDTO = _pedidoService.Obter()
                                    .Where(p => p.Situacao == (int)Enums.SituacaoPedido.AguardandoAtendimento)
                                    .ToList()
                                    .Select(e => new PedidoItemDTO().InjectFrom(e))
                                    .Cast<PedidoItemDTO>()
                                    .Select(e => e.GerarLinks(_urlBase, PedidoEstadoAtualDaAplicacao.ObterPedidos));


            return Ok(pedidosDTO);
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

            _HATEOASManager = new PedidoHATEOASManager(_urlBase, PedidoEstadoAtualDaAplicacao.CadastrarPedido,
                                                        pedidoDTO);

            pedidoDTO.Links = _HATEOASManager.ObterLinks();

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

            return Ok(pedidoDTO);
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
