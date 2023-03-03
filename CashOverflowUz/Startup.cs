//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using CashOverflowUz.Brokers.Loggins;
using CashOverflowUz.Brokers.Storages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CashOverflowUz
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(Action =>
            {
                Action.SwaggerDoc(
                    name: "v1",
                   info: new OpenApiInfo { Title = "CashOverflowUz", Version = "v1" });
            });

            AddBrokers(services);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment enveriment)
        {
            if (enveriment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(Action =>
                Action.SwaggerEndpoint(
                    url:"/swagger/v1/swagger.json",
                    name:"CashOverflowUz v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());

        }
            private static void AddBrokers(IServiceCollection services)
            {
                services.AddTransient<IStorageBroker, StorageBroker>();
                services.AddTransient<ILoggingBroker, LoggingBroker>();
            }

    }
}
