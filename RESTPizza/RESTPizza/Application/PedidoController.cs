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
using Swashbuckle.Swagger.Annotations;

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

        /// <summary>
        /// Obter pedido
        /// </summary>
        /// <remarks>Obtém um pedido</remarks>
        /// <param name="id">ID do pedido</param>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(PedidoDTO))]
        [HttpGet]
        public IHttpActionResult Obter(int id)
        {
            var pedidoDTO = new PedidoDTO();
            var pedido = _pedidoService.Obter().Where(p => p.PedidoID == id).SingleOrDefault();

            if (pedido == null)
                return NotFound();

            pedidoDTO.InjectFrom(pedido);

            _HATEOASManager = new PedidoHATEOASManager(_urlBase, PedidoEstadoAtualDaAplicacao.ObterPedidoUnico,
                                                        pedidoDTO);

            pedidoDTO.Links = _HATEOASManager.ObterLinks();

            return Ok(pedidoDTO);
        }

        /// <summary>
        /// Obter lista de pedidos
        /// </summary>
        /// <remarks>Obtém lista de pedidos com situação 'Aguardando Atendimento'</remarks>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(IEnumerable<PedidoDTO>))]
        [HttpGet]
        public IHttpActionResult Obter()
        {
            var pedidosDTO = _pedidoService.Obter()
                                    .Where(p => p.Situacao == (int)Enums.SituacaoPedido.AguardandoAtendimento)
                                    .ToList()
                                    .Select(e => new PedidoItemDTO().InjectFrom(e))
                                    .Cast<PedidoItemDTO>()
                                    .Select(e => e.GerarLinks(_urlBase, PedidoEstadoAtualDaAplicacao.ObterPedidos));

            if (pedidosDTO == null)
                return NotFound();

            return Ok(pedidosDTO);
        }

        /// <summary>
        /// Cadastrar pedido
        /// </summary>
        /// <remarks>Cadastra um novo pedido</remarks>
        /// <param name="pedidoDTO">pedido a ser cadastrado</param>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Created, type: typeof(PizzaDTO))]
        [HttpPost]
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

        /// <summary>
        /// Aprovar pedido
        /// </summary>
        /// <remarks>Aprova o pedido para que as pizzas comecem a ser feitas</remarks>
        /// <param name="id">ID do pedido</param>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(PizzaDTO))]
        [HttpPut]
        [Route("~/api/pedido/{id}/aprovar")]
        public IHttpActionResult Aprovar(int id)
        {
            //todo:tratar o objectnotfound excpetion depois
            var pedido = _pedidoService.Aprovar(id);
            var pedidoDTO = new PedidoDTO();
            pedidoDTO.InjectFrom(pedido);

            return Ok(pedidoDTO);
        }

        /// <summary>
        /// Rejeitar pedido
        /// </summary>
        /// <remarks>Rejeita o pedido</remarks>
        /// <param name="id">ID do pedido</param>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [HttpPut]
        [Route("~/api/pedido/{id}/rejeitar")]
        public IHttpActionResult Rejeitar(int id)
        {
            //todo:tratar o objectnotfound excpetion depois
            _pedidoService.Rejeitar(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
