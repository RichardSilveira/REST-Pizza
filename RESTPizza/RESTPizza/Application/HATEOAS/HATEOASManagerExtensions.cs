using RESTPizza.Application.DTO;
using RESTPizza.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Application.HATEOAS
{
    public static class HATEOASManagerExtensions
    {
        public static PedidoItemDTO GerarLinks(this PedidoItemDTO pedidoItemDTO, string urlBase, PedidoEstadoAtualDaAplicacao estadoAtual)
        {
            var pedidoHATEOAS = new PedidoHATEOASManager(urlBase, estadoAtual, pedidoItemDTO: pedidoItemDTO);

            pedidoItemDTO.Links = pedidoHATEOAS.ObterLinks();

            return pedidoItemDTO;
        }
    }
}
