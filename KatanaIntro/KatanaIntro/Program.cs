using Microsoft.Owin.Hosting;
using Owin;
using System;

namespace KatanaIntro
{
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
            app.UseWelcomePage(); // show a welcome page as implemented by Microsoft.Owin.Diagnostics


            //app.Run(ctx =>
            //{
            //    return ctx.Response.WriteAsync("Hello World!");
            //});
        }
    }

}
