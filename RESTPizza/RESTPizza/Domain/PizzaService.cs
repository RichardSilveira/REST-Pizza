using RESTPizza.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Domain
{
    public class PizzaService
    {
        private RESTPizzaContext _context;
        private DbSet<Pizza> _dbSet;

        public PizzaService(RESTPizzaContext context = null)
        {
            _context = context ?? new RESTPizzaContext();
            _dbSet = _context.Set<Pizza>();
        }

        public List<Pizza> Obter()
        {
            return _dbSet.ToList();
        }

        public void Cadastrar(Pizza pizza, out List<string> erros)
        {
            erros = new List<string>();

            if (string.IsNullOrEmpty(pizza.Nome))
                erros.Add("O nome da Pizza não foi informado.");

            if (string.IsNullOrEmpty(pizza.Ingredientes))
                erros.Add("Os ingredientes da Pizza não foram informados.");

            if (erros.Count > 0) return;
            _dbSet.Add(pizza);
            _context.SaveChanges();
        }
    }
}
