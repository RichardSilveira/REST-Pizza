using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza.Domain
{
    public static class Enums
    {
        public enum SituacaoPedido
        {
            AguardandoAtendimento = 1,
            EmAtendimento,
            Rejeitado
        }
    }
}
//todo:Talvez add um 'CanceladoPeloCliente'