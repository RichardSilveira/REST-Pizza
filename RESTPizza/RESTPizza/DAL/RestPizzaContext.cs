using RESTPizza.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.DAL
{
    public class RESTPizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        public RESTPizzaContext() : base("RESTPizzaContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}