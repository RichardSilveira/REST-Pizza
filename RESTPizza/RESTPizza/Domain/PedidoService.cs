using RESTPizza.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
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

            if (string.IsNullOrEmpty(pedido.NomeCliente))
                erros.Add(string.Format(Mensagens.CAMPO_NAO_INFORMADO, "Nome do Cliente"));

            if (string.IsNullOrEmpty(pedido.TelefoneCliente))
                erros.Add(string.Format(Mensagens.CAMPO_NAO_INFORMADO, "Telefone do Cliente"));

            if (erros.Count > 0) return;

            pedido.Situacao = (int)Enums.SituacaoPedido.AguardandoAtendimento;

            _dbSet.Add(pedido);
            _context.SaveChanges();
        }

        public Pedido Aprovar(int pedidoID)
        {
            var pedido = this.Obter().SingleOrDefault(p => p.PedidoID == pedidoID);

            if (pedido == null)
                throw new ObjectNotFoundException(string.Format(Mensagens.OBJETO_NAO_ENCONTRADO, "Pedido"));

            pedido.Situacao = (int)Enums.SituacaoPedido.Aprovado;

            //Total de pedidos que estão em atendimento * 30 minutos (tempo médio de conclusão)
            pedido.TempoEstimado = this.Obter().Count(p => p.Situacao == (int)Enums.SituacaoPedido.Aprovado) * 30;

            _context.Entry(pedido).State = EntityState.Modified;
            _dbSet.Attach(pedido);
            _context.SaveChanges();

            return pedido;
        }

        public void Rejeitar(int pedidoID)
        {
            var pedido = this.Obter().SingleOrDefault(p => p.PedidoID == pedidoID);

            if (pedido == null)
                throw new ObjectNotFoundException(string.Format(Mensagens.OBJETO_NAO_ENCONTRADO, "Pedido"));

            pedido.Situacao = (int)Enums.SituacaoPedido.Rejeitado;

            _context.Entry(pedido).State = EntityState.Modified;
            _dbSet.Attach(pedido);
            _context.SaveChanges();
        }
    }
}
