using RESTPizza.Application.HATEOAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Application.DTO
{
    public class PedidoItemDTO
    {
        public int PedidoID { get; set; }

        public string NomeCliente { get; set; }

        public int Situacao { get; set; }

        public List<Link> Links { get; set; }
    }
}
