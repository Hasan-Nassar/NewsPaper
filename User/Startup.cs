using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using User.Persistence.Context;

namespace User
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
            
            services.AddConsulConfig(Configuration);
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());
            services.AddControllers();
            
            var x = Configuration.GetConnectionString("Default");
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
         
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping.MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User API",
                    Version = "v1",
                    Description = "Description for the API goes here.",

                });
               
            });
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            
            
            app.UseAuthentication();

            app.UseConsul(Configuration);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            app.UseSwagger();


            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
        }
    }
}