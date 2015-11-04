using RESTPizza.Application.DTO;
using RESTPizza.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Application.HATEOAS
{
    public class PedidoHATEOASManager
    {
        private string _urlBase;
        private PedidoEstadoAtualDaAplicacao _estadoAtual;
        private PedidoDTO _pedidoDTO;
        private PedidoItemDTO _pedidoItemDTO;

        public PedidoHATEOASManager(string urlBase, PedidoEstadoAtualDaAplicacao estadoAtualAplicacao, PedidoDTO pedidoDTO = null, PedidoItemDTO pedidoItemDTO = null)
        {
            _urlBase = urlBase;
            _estadoAtual = estadoAtualAplicacao;
            _pedidoDTO = pedidoDTO;
            _pedidoItemDTO = pedidoItemDTO;
        }

        public List<Link> ObterLinks()
        {
            var links = new List<Link>();

            switch (_estadoAtual)
            {
                case PedidoEstadoAtualDaAplicacao.ObterPedidoUnico:
                    if (_pedidoDTO.Situacao == (int)Enums.SituacaoPedido.AguardandoAtendimento)
                        links.AddRange(ObterLinksParaPedidoAguardandoAtendimento(_pedidoDTO.PedidoID));

                    break;

                case PedidoEstadoAtualDaAplicacao.ObterPedidos://Objeto recebido será o 'PedidoItemDTO'
                    links.Add(new Link()
                    {
                        rel = "self",
                        method = "GET",
                        href = _urlBase + _pedidoItemDTO.PedidoID
                    });

                    if (_pedidoItemDTO.Situacao == (int)Enums.SituacaoPedido.AguardandoAtendimento)
                        links.AddRange(ObterLinksParaPedidoAguardandoAtendimento(_pedidoItemDTO.PedidoID));

                    break;

                case PedidoEstadoAtualDaAplicacao.CadastrarPedido:
                    links.AddRange(ObterLinksParaPedidoAguardandoAtendimento(_pedidoDTO.PedidoID));

                    break;

                default:
                    break;
            }

            return links;
        }

        private List<Link> ObterLinksParaPedidoAguardandoAtendimento(int pedidoID)
        {
            var links = new List<Link>();

            links.Add(new Link()
            {
                rel = "aprovar",
                method = "PUT",
                href = _urlBase + pedidoID + "/aprovar"
            });
            links.Add(new Link()
            {
                rel = "rejeitar",
                method = "PUT",
                href = _urlBase + pedidoID + "/rejeitar"
            });

            return links;
        }
    }

    public enum PedidoEstadoAtualDaAplicacao
    {
        ObterPedidoUnico,
        ObterPedidos,
        CadastrarPedido
    }
}
