using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReviewsService_Core.Common;
using ReviewsService_Core.Data;
using ReviewsService_Core.Domain;
using ReviewsService_Core.Logic;
using ReviewsService_Service.Middlewares;
using System;

namespace ReviewsService_Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public static string connectionString;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Pagination", "www-authenticate")
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
                });
            });


            connectionString = Configuration.GetConnectionString("dbconn");
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddHttpContextAccessor();
            services.AddDbContext<UMSContext>(options =>
                    options.UseSqlServer(connectionString));

            AutoMapperConfig.RegisterMappings();
            UMSPoco.Setup(connectionString);

            services.AddTransient<IFactoryModule, FactoryModule>();
            services.AddTransient<ILogicModule, LogicModule>();
            services.AddScoped<IDataModule, DataModule>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Reviews service", Version = "V1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // add swagger
            var swaggerOptions = new ReviewsService_Core.Domain.Model.Helper.SwaggerOptions();
            Configuration.GetSection(nameof(Swashbuckle.AspNetCore.Swagger.SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseClientSecretMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
