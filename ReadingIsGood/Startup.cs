using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReadingIsGood.DatabaseContext;
using ReadingIsGood.Helpers;
using ReadingIsGood.Repositories.CustomerRepository;
using ReadingIsGood.Repositories.OrderItemRepository;
using ReadingIsGood.Repositories.OrderRepository;
using ReadingIsGood.Repositories.ProductRepository;
using ReadingIsGood.Repositories.StockRepository;
using ReadingIsGood.Services.CustomerService;
using ReadingIsGood.Services.OrderItemService;
using ReadingIsGood.Services.OrderService;
using ReadingIsGood.Services.ProductService;
using ReadingIsGood.Services.StockService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadingIsGood
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string AllCors = "allCors";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllCors,
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader()
                                    .AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("Location");
                });
            });
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.RegisterandConfigureSwaggerByToken(Configuration, Assembly.GetExecutingAssembly(), AppContext.BaseDirectory);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStockRepository, StockRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddEntityFrameworkNpgsql().AddDbContext<ReadingIsGoodDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(AllCors);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reading Is Good API v1");

            });

            app.MigrateDatabase<ReadingIsGoodDbContext>();
        }
    }
}
