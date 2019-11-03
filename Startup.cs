using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apicenter.Data;
using apicenter.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace apicenter
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDbContext<DataContext>(options => 
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //start auto mapper config
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //end auto mapper config

            services.AddSwaggerGen(c =>
            {
                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "WebApi.XML";
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new Info { Title = "SALESTOOLS API CENTER", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
            });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SALESTOOLS API CENTER V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint((Configuration.GetConnectionString("Endpoint") + "/swagger/v1/swagger.json"), "SALESTOOLS API CENTER V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            
            app.UseSwagger();
                    
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
