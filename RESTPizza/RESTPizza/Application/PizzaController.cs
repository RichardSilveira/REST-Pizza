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
        [HttpGet]
        [ResponseType(typeof(PizzaDTO))]
        public IHttpActionResult Obter(int id)
        {
            var pizza = _pizzaService.Obter().Where(p => p.PizzaID == id).SingleOrDefault();
            var pizzaDTO = new PizzaDTO().InjectFrom(pizza);

            return Ok(pizzaDTO);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<PizzaDTO>))]
        public IHttpActionResult Obter()
        {
            var pizzas = _pizzaService.Obter()
                                .ToList()
                                .Select(e => new PizzaDTO().InjectFrom(e))
                                .Cast<PizzaDTO>();

            return Ok(pizzas);
        }

        [HttpPost]
        [ResponseType(typeof(PizzaDTO))]
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