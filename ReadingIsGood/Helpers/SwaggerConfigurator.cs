using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ReadingIsGood.Helpers
{
    public static class SwaggerConfigurator
    {
        public static void RegisterandConfigureSwaggerByToken(this IServiceCollection services,
            IConfiguration configuration, Assembly currentAssembly, string baseDirectory)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Reading Is Good API",
                    Description = "Reading Is Good API"
                });

                var xmlFile = "ReadingIsGoodApi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                //c.EnableAnnotations();

                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "ReadingIsGoodApi.xml");
                //c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition(configuration.GetSection("Swagger")["SecurityDefinitionType"], new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,

                    Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
                    {
                        Password = new Microsoft.OpenApi.Models.OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri(configuration.GetSection("Swagger")["AuthorizationUrl"]),
                            Scopes = new Dictionary<string, string>()
                            {
                                {
                                    configuration.GetSection("Swagger")["Scope"], configuration.GetSection("Swagger")["Scope"]

                                }
                            },
                            TokenUrl = new Uri(configuration.GetSection("Swagger")["TokenUrl"]),
                        }

                    }

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                                },
                                new List<string>() { configuration.GetSection("Swagger")["Scope"] }
                            }
                });
            });
        }
    }
}
