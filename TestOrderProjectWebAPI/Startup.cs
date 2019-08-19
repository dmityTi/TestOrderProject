using System;
using System.IO;
using System.Reflection;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.Services;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TestOrderProjectWebAPI.Middlewares;

namespace TestOrderProjectWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string ConnectionString { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o =>
                {
//                    o.Filters.Add(new GlobalExceptionFilter());
                })
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();
//                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddXmlSerializerFormatters();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            
            ConnectionString = Configuration.GetConnectionString("OrderDBConnection");
            services.AddDbContext<OrderContext>(options =>
            {
//                options.UseInMemoryDatabase("TestOrderDB");
                options.UseSqlServer(ConnectionString);
//                options.EnableSensitiveDataLogging();
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "XmlAPI",
                    Description = "ASP.NET Core Web API"
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            Mapping.MappingConfig.RegisterMapping();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "XmlOrderAPI V1");
            });
            
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}