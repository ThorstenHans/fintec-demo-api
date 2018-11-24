using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using AutoMapper;
using FintecDemo.API.Configuration;
using FintecDemo.API.Database;
using FintecDemo.API.Repositories;
using FintecDemo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace FintecDemo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private CorsConfiguration _GetCorsConfiguration()
        {
            var corsConfig = new CorsConfiguration();
            Configuration.GetSection("cors").Bind(corsConfig);
            return corsConfig;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddApiDocs(services);
            services.AddDbContext<FintecDbContext>(options => { options.UseInMemoryDatabase("FintecDB"); });

            services
                .AddScoped<IExchangeRepository, ExchangeRepository>()
                .AddScoped<IExchangeService, ExchangeService>();

            services
                .AddScoped<IStockRepository, StockRepository>()
                .AddScoped<IStockService, StockService>();

            AddResponseCompression(services);
            if (!_GetCorsConfiguration().Disable)
            {
                services.AddCors();
            }

            services.AddAutoMapper(config => { config.CreateMissingTypeMaps = false; });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

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
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "FinTec demo API documentation";
                options.RoutePrefix = "docs";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "FinTec Demo API");
            });
            var corsConfig = _GetCorsConfiguration();
            if (!corsConfig.Disable)
            {
                app.UseCors(options =>
                {
                    if (corsConfig.AllowAnyOrigin)
                    {
                        options.AllowAnyOrigin();
                    }
                    else
                    {
                        options.WithOrigins(corsConfig.Origins);
                    }

                    if (corsConfig.AllowAnyHeader)
                    {
                        options.AllowAnyHeader();
                    }
                    else
                    {
                        options.WithHeaders(corsConfig.Headers);
                    }

                    if (corsConfig.AllowAnyMethod)
                    {
                        options.AllowAnyMethod();
                    }
                    else
                    {
                        options.WithMethods(corsConfig.Methods);
                    }
                });
            }

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void AddApiDocs(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.SwaggerDoc("v1", new Info
                {
                    Title = "FinTec Demo API",
                    Description = "API for financial domain demo applications",
                    License = new License
                    {
                        Name = "MIT",
                        Url = "https://choosealicense.com/licenses/mit/#"
                    },
                    Version = "1.0.0",
                    Contact = new Contact
                    {
                        Name = "Thorsten Hans",
                        Email = "thorsten.hans@gmail.com",
                        Url = "https://thorsten-hans.com"
                    }
                });
            });
        }

        private void AddResponseCompression(IServiceCollection services)
        {
            services.AddResponseCompression(config =>
            {
                config.EnableForHttps = true;
                config.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(config => { config.Level = CompressionLevel.Fastest; });
        }
    }
}
