using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HPlusSportsApi
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
            services.AddOptions();

            // Adding custom services for Dependency Injection
            //Cosmos DB service initialized with config
            IConfiguration dbConfig = Configuration.GetSection(Constants.KEY_DB_CONFIG);
            services.Configure<Services.CosmosDBServiceOptions>(dbConfig);

            //single doc client for performance
            var docClient = new DocumentClient(
                new Uri(Configuration[Constants.KEY_COSMOS_URI]),
                Configuration[Constants.KEY_COSMOS_KEY]);
            services.AddSingleton<DocumentClient>(docClient);

            //Add storage service implementations
            services.AddScoped<Services.IQueueService, Services.AzureQueueService>();
            services.AddScoped<Services.ITableService, Services.AzureTableService>();
            services.AddScoped<Services.IDocumentDBService, Services.CosmosDBService>();
            services.AddScoped<Services.IBlobService, Services.AzureBlobService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "1.0",
                        Title = "HPlus Sports API",
                        Description = "HPlus Sports API for Orders and Products"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "HPlus Sports API v1");
            });
        }
    }
}
