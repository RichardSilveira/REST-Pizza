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
        public static PedidoDTO GerarLinks(this PedidoDTO pedidoDTO, string urlBase, PedidoEstadoAtualDaAplicacao estadoAtual)
        {
            var pedidoHATEOAS = new PedidoHATEOASManager(urlBase, estadoAtual);

            pedidoDTO.Links = pedidoHATEOAS.ObterLinks(pedidoDTO);

            return pedidoDTO;
        }
    }
}
