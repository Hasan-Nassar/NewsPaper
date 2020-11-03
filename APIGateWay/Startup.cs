using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace APIGateWay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo{ Version = "v1", Title = "NewsPaper Api" });
            });
        
            services.AddControllers();
            services.AddOcelot().AddConsul();
        }
        
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/NewsPaper/v1/swagger.json", "NewsPaper API");
                c.SwaggerEndpoint("/User/v1/swagger.json", "User API ");
            });
        
            app.UseRouting();
       
            await app.UseOcelot();
        }
    }
}