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
using RESTPizza.Application.DTO;
using Swashbuckle.Swagger.Annotations;
using System.Net;

namespace RESTPizza.Application
{
    public class PizzaController : ApiController
    {
        private PizzaService _pizzaService;
        private string _urlBase;

        public PizzaController()
        {
            _urlBase = ConfigurationManager.AppSettings["UrlPrincipalRaiz"] + @"/api/pizza/";
            _pizzaService = new PizzaService();
        }

        /// <summary>
        /// Obter pizza
        /// </summary>
        /// <remarks>Obtém uma pizza</remarks>
        /// <param name="id">ID da pizza</param>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(PizzaDTO))]
        [HttpGet]
        public IHttpActionResult Obter(int id)
        {
            var pizza = _pizzaService.Obter().Where(p => p.PizzaID == id).SingleOrDefault();

            if (pizza == null)
                return NotFound();

            var pizzaDTO = new PizzaDTO().InjectFrom(pizza);

            return Ok(pizzaDTO);
        }

        /// <summary>
        /// Obtém todas as pizzas
        /// </summary>
        /// <remarks>Obtém todas as pizzas cadastradas</remarks>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(IEnumerable<PizzaDTO>))]
        [HttpGet]
        public IHttpActionResult Obter()
        {
            var pizzas = _pizzaService.Obter()
                                .ToList()
                                .Select(e => new PizzaDTO().InjectFrom(e))
                                .Cast<PizzaDTO>();

            if (pizzas == null)
                return NotFound();

            return Ok(pizzas);
        }

        /// <summary>
        /// Cadastrar pizza
        /// </summary>
        /// <remarks>Cadastra uma nova pizza</remarks>
        /// <param name="pizzaDTO">pizza a ser cadastrada</param>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Created, type: typeof(PizzaDTO))]
        [HttpPost]
        public IHttpActionResult Cadastrar(PizzaDTO pizzaDTO)
        {
            List<string> errosValidacao;

            var pizza = new Pizza();
            pizza.InjectFrom(pizzaDTO);

            _pizzaService.Cadastrar(pizza, out errosValidacao);
            pizzaDTO.InjectFrom(pizza);

            if (errosValidacao.Count == 0)
                return Created(new Uri(_urlBase + pizzaDTO.PizzaID), pizzaDTO);
            else
                return BadRequest(errosValidacao.Aggregate((a, b) => { return a + ", " + b; }));
        }
    }
}