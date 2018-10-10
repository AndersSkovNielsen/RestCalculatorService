using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RestCalculatorService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Tilføj CORS (ALTID FØR MVC!)
            //CORS hentes via NuGet
            services.AddCors();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Igen, CORS FØR MVC!
            //CORS hentes via NuGet
            //Tillad alle handlinger fra alle
            app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin());

            //Tillad Get og Put fra alle
            //app.UseCors(options => options.WithMethods("GET", "PUT").AllowAnyOrigin());

            app.UseMvc();
        }
    }
}
