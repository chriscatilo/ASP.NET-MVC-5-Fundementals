using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KatanaIntro
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    class Program
    {
        static void Main(string[] args)
        {
            // start a web server listening to a given http address
            var uri = "http://localhost:8080";
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopping");
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseWelcomePage(); // show a welcome page as implemented by Microsoft.Owin.Diagnostics

            //app.Run(ctx =>
            //{
            //    return ctx.Response.WriteAsync("Hello World!");
            //});

            app.Use<HelloWorldComponent>();
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
