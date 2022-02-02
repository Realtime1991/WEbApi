using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Options;
using WebService2.RabbitMQ.Reciever;
using Serilog;


namespace WebService2
{
    public class Startup
    {
	//test HotFix
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging();
            services.AddSingleton(Log.Logger);

            services.AddOptions();

            
            var rabbitMqOptions1 = Configuration.GetSection("RabbitMqS1toS2").Get<RabbitMqOptions>();
            services.AddRabbitMqConnection(rabbitMqOptions1);
            services.AddRabbitMqRegistration(rabbitMqOptions1);
            var rabbitMqOptions2 = Configuration.GetSection("RabbitMqS2toS1").Get<RabbitMqOptions>();
            services.AddRabbitMqConnection(rabbitMqOptions2);
            services.AddRabbitMqRegistration(rabbitMqOptions2);

            services.AddEventBusHandling(EventBusExtension.GetHandlers(Log.Logger));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Plane Service #2",
                    Version = "v1",
                    Description = "Description: Service #2",
                    Contact = new OpenApiContact
                    {
                        Name = "Shishchenkov Maxim",
                        Email = "maxzvuchitgordo@yandex.ru"
                    }
                });
            });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebService2 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //Event Bus
            app.SubscribeToEvents();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
