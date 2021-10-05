using System;
using Boleyn.Countries.Behaviours;
using Boleyn.Countries.Content.Providers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using WorldBank.Models;

namespace Boleyn.Countries.Content
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Countries", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
                c.EnableAnnotations();
            });

            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddMediatR(typeof(Startup))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionsHandlerBehavior<,>));

            services.AddAutoMapper(typeof(Startup));
            services.AddHttpClient<IProvider<Country>, CountryProvider>(client =>
            {
                client.BaseAddress = new Uri("http://api.worldbank.org/v2/country/");
                client.DefaultRequestHeaders.Add("accept", "application/json");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Countries v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}