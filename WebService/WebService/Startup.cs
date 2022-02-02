using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Service1.Models;
using Service1.RabbitMQ.Reciever;
using EventBus.Base.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Options;
using Serilog;


namespace Service1
{
    public class Startup
    {
//added Feature1
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

            services.AddDbContext<PlaneGeoLocationContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("MyWebApiConection")));

            services.AddOptions();
            var rabbitMqOptions1 = Configuration.GetSection("RabbitMqS1toS2").Get<RabbitMqOptions>();
            services.AddRabbitMqConnection(rabbitMqOptions1);
            services.AddRabbitMqRegistration(rabbitMqOptions1);
            Log.Warning(rabbitMqOptions1.Host.ToString());

            var rabbitMqOptions2 = Configuration.GetSection("RabbitMqS2toS1").Get<RabbitMqOptions>();
            services.AddRabbitMqConnection(rabbitMqOptions2);
            services.AddRabbitMqRegistration(rabbitMqOptions2);
            Log.Warning(rabbitMqOptions2.Host.ToString());



            services.AddEventBusHandling(EventBusExtension.GetHandlers(Log.Logger));

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Plane Service #1",
                    Version = "v1",
                    Description = "Description: Service #1",
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebService v1"));
                Log.Warning("DEVELOPMENT");
            }
            else
                Log.Warning("PROD?");

            

            app.UseHttpsRedirection();

            app.UseRouting();

            //Event Bus
            app.SubscribeToEvents();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }
            
            );
        }
    }
}
