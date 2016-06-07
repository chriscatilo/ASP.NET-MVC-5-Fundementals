using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace KatanaIntro
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfiguerWebApi(app);

            app.Use<HelloWorldComponent>();
        }

        private void ConfiguerWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});

            app.UseWebApi(config);
        }
    }

    public class HelloWorldComponent
    {
        private AppFunc _next;

        public HelloWorldComponent(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            await Task.Run(() => 
            {
                var response = environment["owin.ResponseBody"] as Stream;

                using (var writer = new StreamWriter(response))
                {
                    foreach (var key in environment.Keys)
                    {
                        writer.WriteLineAsync(key);
                    }
                }
            });
        }
    }

}
