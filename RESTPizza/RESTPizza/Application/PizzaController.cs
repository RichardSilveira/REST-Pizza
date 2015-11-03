using RESTPizza.Domain;
using System;
using System.Collections.Generic;
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

        [ResponseType(typeof(Pizza))]
        public IHttpActionResult Get()
        {
            var pizzas = _pizzaService.Obter();

            return Ok(pizzas);
        }
    }
}
