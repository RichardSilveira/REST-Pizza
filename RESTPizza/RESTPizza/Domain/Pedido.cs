using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Domain
{
    public class Pedido
    {
        [Key]
        public int PedidoID { get; set; }

        public string SenhaEspera { get; set; }

        public decimal? TempoEstimado { get; set; }

        [Required]
        public int Situacao { get; set; }

        [Required]
        public int PizzaID { get; set; }

        public virtual Pizza Pizza { get; set; }
    }
}
//todo:próximo passo:Adicionar cliente ao pedido