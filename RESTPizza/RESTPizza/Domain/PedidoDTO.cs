using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Domain
{
    public class PedidoDTO
    {
        public int PedidoID { get; set; }

        public string SenhaEspera { get; set; }

        public decimal? TempoEstimado { get; set; }

        public int Situacao { get; set; }

        public int PizzaID { get; set; }

        public List<Link> Links { get; set; }
    }
}
