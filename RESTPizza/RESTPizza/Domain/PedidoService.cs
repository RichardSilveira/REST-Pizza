using RESTPizza.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Domain
{
    public class PedidoService
    {
        private RESTPizzaContext _context;
        private DbSet<Pedido> _dbSet;

        public PedidoService(RESTPizzaContext context = null)
        {
            _context = context ?? new RESTPizzaContext();
            _dbSet = _context.Set<Pedido>();
        }

        public IQueryable<Pedido> Obter()
        {
            return _dbSet;
        }

        public void RealizarNovo(Pedido pedido, out List<string> erros)
        {
            erros = new List<string>();

            if (pedido == null)
            {
                erros.Add(string.Format(Mensagens.OBJETO_NAO_INFORMADO, "Pedido"));
                return;
            }

            if (pedido.PizzaID <= 0)
                erros.Add(string.Format(Mensagens.CAMPO_NAO_INFORMADO, "Pizza"));

            if (erros.Count > 0) return;

            pedido.Situacao = (int)Enums.SituacaoPedido.EmAtendimento;
            //todo:Aplicar regra para calcular tempo estimado
            _dbSet.Add(pedido);
            _context.SaveChanges();
        }


    }
}
