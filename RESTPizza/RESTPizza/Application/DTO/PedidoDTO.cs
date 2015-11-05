using RESTPizza.Application.HATEOAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Application.DTO
{
    public class PedidoDTO
    {
        public int PedidoID { get; set; }

        public string NomeCliente { get; set; }

        public string TelefoneCliente { get; set; }

        public int? SenhaEspera { get; set; }

        public decimal? TempoEstimado { get; set; }

        public int Situacao { get; set; }

        public int PizzaID { get; set; }

        public List<Link> Links { get; set; }
    }
}
