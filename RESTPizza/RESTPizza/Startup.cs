using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RESTPizza
{
    public class Startup
    {
        HttpConfiguration _configuration;

        public Startup()
        {
            _configuration = new HttpConfiguration();
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureWebApi();
            ConfigureJsonFormatter();

            app.UseWebApi(_configuration);
        }

        private void ConfigureWebApi()
        {
            _configuration = new HttpConfiguration();
            _configuration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }

        private void ConfigureJsonFormatter()
        {
            _configuration.Formatters.Clear();
            _configuration.Formatters.Add(new JsonMediaTypeFormatter());
            _configuration.Formatters.JsonFormatter.SerializerSettings =
                new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }
    }
}
