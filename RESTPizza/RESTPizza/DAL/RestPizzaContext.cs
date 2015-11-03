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

        //public RestPizzaContext() : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=DemoForMoqHelper;Integrated Security=true;")
        public RESTPizzaContext() : base(@"Data Source=""127.0.0.1, 1433"";Initial Catalog=RESTPizza;User ID=sa;Password=123456")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}