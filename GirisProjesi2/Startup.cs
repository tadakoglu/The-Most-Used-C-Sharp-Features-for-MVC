using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GirisProjesi2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); // mvc mimarisini kullanığımı bildirdim.
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseMvc(m => {
                //m.MapRoute("defaultYeni", "{controller=AnaSayfa}/{action=Index}/{id?}");
                m.MapRoute(name: null, template: "{controller}/{action}/{id?}",defaults: new { controller = "AnaSayfa", action = "Index" });
            });
            // default {controller=Home}/{action=Index}/{id?} rotasını da kullanağımı bildirdim. yani Home/Index?id=xxx&id2=22 gibi..

        }
        
    }
}
