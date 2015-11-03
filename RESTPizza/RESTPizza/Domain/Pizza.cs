using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Domain
{
    public class Pizza
    {
        [Key]
        public int PizzaID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Ingredientes { get; set; }
    }
}
