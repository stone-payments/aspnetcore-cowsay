using System;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspnetCoreCowsay
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            var cowsayMessage = Environment.GetEnvironmentVariable("COWSAY_MESSAGE");
            app.Use((context,next)=>{

                if(context.Request.Path.Value == "/"){
                    string cow = Cowsay.GetCowsay(cowsayMessage, AnimalMode.Paranoid);
                    return context.Response.WriteAsync(cow.ToString());
                }
                return next();
            });
        }
    }
}
