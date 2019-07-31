using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Okta.AspNetCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using MongoDB.Driver;
using Alpha.Mongo.Netcore.Repository;
using Alpha.Mongo.Netcore.Models;
using MongoDB.Bson;
using Alpha.Mongo.Netcore.Middlware;
using Alpha.Mongo.Netcore.Services;

namespace Alpha.Mongo.Netcore
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
            services.AddCors(options =>
            {
                // The CORS policy is open for testing purposes. In a production application, you should restrict it to known origins.
                options.AddPolicy(
                    "AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            })
            .AddOktaWebApi(new OktaWebApiOptions()
            {
                OktaDomain = Configuration["Okta:OktaDomain"],
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "The Net Core API", Version = "v1" });
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        new string[] {}
                    }
                });
            });
            RegisterDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseStaticFiles(); // For the wwwroot folder

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "Token")),
                    RequestPath = "/Token"
                });

            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Net Core API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware().Invoke
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton(new MongoClient("mongodb://mongodb0.example.com:27017/admin"));
            services.AddSingleton(new MongoCollectionFactory(new MongoClient("mongodb://localhost:27017/admin"), "firstdb"));
           //services.AddScoped(c => new MongoCollectionFactory(new MongoClient("mongodb://mongodb0.example.com:27017/admin"), "firstdb").GetCollection<Car>());
            //services.AddScoped(c => new MongoCollectionFactory(c.GetRequiredService<MongoClient>(), "firstdb").GetCollection<Car>());
            services.AddScoped(c => c.GetRequiredService<MongoCollectionFactory>().GetCollection<Car>());
            services.AddScoped(c => c.GetRequiredService<MongoCollectionFactory>().GetCollection<Boat>());
            services.AddScoped<IAlphaRepository<Car>, AlphaRepository<Car>>();
            services.AddScoped<IAlphaRepository<Boat>, AlphaRepository<Boat>>();
            services.AddScoped<IElasticSearch, ElasticSearch>();
        }
    }
}
