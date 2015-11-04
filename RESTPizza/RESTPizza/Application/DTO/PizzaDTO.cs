using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Application.DTO
{
    public class PizzaDTO
    {
        public int PizzaID { get; set; }

        public string Nome { get; set; }

        public string Ingredientes { get; set; }
    }
}
