using System.Linq;
using AutoMapper;
using CESP.Core.Managers;
using CESP.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace CESP.Service
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(
                        new CespViewMappingProfile());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.RegisterRepositories(_configuration);
            services.RegisterManagers(_configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyHeader();
                });
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "CESP.Service", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseSwagger(c => c.PreSerializeFilters.Add((swagger, httpReq) => {
                var paths = swagger.Paths.ToDictionary(entry => entry.Key,
                    entry => entry.Value);
                foreach(var path in paths)
                {
                    swagger.Paths.Remove(path.Key);
                    swagger.Paths.Add($"/api{path.Key}", path.Value);
                }
            }));
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "CESP.Service V1");
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}