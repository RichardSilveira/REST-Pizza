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

        public PedidoHATEOASManager(string urlBase, PedidoEstadoAtualDaAplicacao estadoAtualAplicacao)
        {
            _urlBase = urlBase;
            _estadoAtual = estadoAtualAplicacao;
        }

        public List<Link> ObterLinks(PedidoDTO pedidoDTO)
        {
            var links = new List<Link>();

            switch (_estadoAtual)
            {
                case PedidoEstadoAtualDaAplicacao.ObterPedido:

                    links.Add(new Link()
                    {
                        rel = "self",
                        method = "GET",
                        href = _urlBase + pedidoDTO.PedidoID
                    });

                    if (pedidoDTO.Situacao == (int)Enums.SituacaoPedido.AguardandoAtendimento)
                    {
                        links.Add(new Link()
                        {
                            rel = "aprovar",
                            method = "PUT",
                            href = _urlBase + pedidoDTO.PedidoID + "aprovar"
                        });
                        links.Add(new Link()
                        {
                            rel = "rejeitar",
                            method = "PUT",
                            href = _urlBase + pedidoDTO.PedidoID + "rejeitar"
                        });
                    }
                    break;
                case PedidoEstadoAtualDaAplicacao.CadastrarPedido:
                    links.Add(new Link()
                    {
                        rel = "aprovar",
                        method = "PUT",
                        href = _urlBase + pedidoDTO.PedidoID + "aprovar"
                    });
                    links.Add(new Link()
                    {
                        rel = "rejeitar",
                        method = "PUT",
                        href = _urlBase + pedidoDTO.PedidoID + "rejeitar"
                    });
                    break;

                default:
                    break;
            }

            return links;
        }
    }

    public enum PedidoEstadoAtualDaAplicacao
    {
        ObterPedido,
        CadastrarPedido
    }


}
