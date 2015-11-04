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
    public class PizzaController : ApiController
    {
        public PizzaService _pizzaService { get; set; }

        public PizzaController()
        {
            _pizzaService = new PizzaService();
        }

        [HttpGet]
        [ResponseType(typeof(Pizza))]
        public IHttpActionResult Obter(int id)
        {
            var pizza = _pizzaService.Obter().Where(p => p.PizzaID == id).SingleOrDefault();

            return Ok(pizza);
        }

        [HttpGet]
        [ResponseType(typeof(Pizza))]
        public IHttpActionResult Obter()
        {
            var pizzas = _pizzaService.Obter().ToList();

            return Ok(pizzas);
        }

        [HttpPost]
        [ResponseType(typeof(Pizza))]
        public IHttpActionResult Cadastrar(Pizza pizza)
        {
            List<string> errosValidacao;
            _pizzaService.Cadastrar(pizza, out errosValidacao);

            if (errosValidacao.Count == 0)
            {
                var urlCompleta = ConfigurationManager.AppSettings["UrlPrincipalRaiz"] + @"/api/pizza/" + pizza.PizzaID;
                return Created(new Uri(urlCompleta), pizza);
            }
            else
            {
                return BadRequest(errosValidacao.Aggregate((a, b) => { return a + ", " + b; }));
            }
        }
    }
}
