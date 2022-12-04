using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Coffee_Utilidades.DataModel;
using Coffee_Utilidades.Base.Configuration;
using Microsoft.EntityFrameworkCore;
using Coffee_Utilidades.Core.Service;

namespace Coffee_Utilidades.API
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
            services.AddCors();

            //configuração da conexão com o banco de dados
            Configs methodDb = new Configs();
            //services.AddDbContext<DataContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(methodDb.fnReadXML("CoffeeUsers")));

            services.AddDbContext<DataContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer("Server=tcp:coffeeusers.database.windows.net,1433;Initial Catalog=CoffeeUsers;Persist Security Info=False;User ID=desenvolvimento;Password=G@briel1010;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

            services.AddScoped<ProdutoService, ProdutoService>();

            services.AddControllers();
            //configuração de versionamento
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ApiVersionReader = new HeaderApiVersionReader("version");
            });

            services.AddVersionedApiExplorer(options =>
            {
                // Agrupar por número de versão
                options.GroupNameFormat = "'v'VVV";

                // Necessário para o correto funcionamento das rotas
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coffee_Utilidades.API", Version = "v1" });
                c.SwaggerDoc("v1.1", new OpenApiInfo { Title = "Coffee_Utilidades.API", Version = "v1.1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    // Geração de um endpoint do Swagger para cada versão descoberta
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                }              
                );

            }

            

            

            app.UseRouting();

            //app.UseCors(x => x.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader().WithOrigins("http://localhost:4200", "http://localhost:4200", "http://localhost:4200/produtos", "http://192.168.10.223:40", 
            //"http://192.168.10.223:40/produtos", "http://192.168.10.223:40/carrinho"));

            app.UseCors(x => x.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader().AllowAnyOrigin());
            app.UseHttpsRedirection();

            //app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
