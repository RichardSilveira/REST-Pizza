using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPizza
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder mensagensUsuario = new StringBuilder();

            string baseUri = ConfigurationManager.AppSettings["UrlPrincipalRaiz"].ToString();

            Console.WriteLine("Iniciando web server do REST Pizza");

            WebApp.Start<Startup>(baseUri);

            mensagensUsuario.AppendLine(string.Format("Web server REST Pizza executando em {0}. Pressione ENTER para sair.", baseUri));
            mensagensUsuario.AppendLine("Para fazer uma chamada, siga o exemplo da url abaixo e lembre se de associar às chamadas o verbo (POST, PUT ou DELETE), no caso do GET é possível testar pelo próprio navegador, já para os outros tipos de chamadas é preciso usar aplicativos, como por exemplo o 'POSTMAN' que permitem fazer tal operação.");
            mensagensUsuario.AppendLine(string.Format("{0}/api/Pizza", baseUri));

            Console.WriteLine(mensagensUsuario.ToString());
            Console.ReadLine();
        }
    }
}
