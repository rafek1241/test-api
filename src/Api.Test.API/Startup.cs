using Api.Test.API.Core;
using Api.Test.API.Filters;
using Api.Test.Infrastructure;
using Api.Test.Partner;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Api.Test.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(Startup).Assembly;
            services.AddMediatR(assembly);
            services.AddFluentValidation(new[] { assembly });
            services.AddLogging(x=>x.AddSerilog());
            services.AddMvc(
                cfg =>
                {
                    cfg.Filters.Add<GeneralExceptionFilter>();
                    cfg.Filters.Add<NoCookiePassedExceptionFilter>();
                    cfg.Filters.Add<ValidationExceptionFilter>();
                }
            );
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo()
                        {
                            Title = "My Api",
                            Version = "v1"
                        }
                    );
                    
                    c.EnableAnnotations();
                }
            );
            
            services.AddInfrastructure();
            services.AddPartnerServices();
            services.AddHttpContextAccessor();
            services.AddTransient<IPartnerReceiver, PartnerReceiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "My API v1"));
            app.UseEndpoints(x => x.MapDefaultControllerRoute());
        }
    }
}