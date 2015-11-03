using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Domain
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public string SenhaEspera { get; set; }
        public decimal? TempoEstimado { get; set; }
        public int Situacao { get; set; }
        public int PizzaID { get; set; }

        public virtual Pizza Pizza { get; set; }
    }
}
//todo:próximo passo:Adicionar cliente ao pedido