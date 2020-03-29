using System.Linq;
using AutoMapper;
using CESP.Core.Managers;
using CESP.Core.Utils;
using CESP.Dal;
using CESP.Dal.Infrastructure;
using CESP.Dal.Mapping;
using CESP.Service.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CESP.Service
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AddMappings(services);
            services.RegisterRepositories(Configuration);
            services.RegisterManagers(Configuration);
            AddEmailSender(services);
            
            services.AddOptions();
            services.Configure<Credentials>(Configuration.GetSection("Credentials"));
            
            AddCorpPolocy(services);
            AddControllers(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CESP.Service", Version = "v1" });
                 c.OperationFilter<FormFileSwaggerFilter>();
                 c.OperationFilter<PasswordHeaderSwaggerFilter>();
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseCors("CorsPolicy");
            }
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CESP.Service V1");
            });
            
            if(env.IsEnvironment("Release"))
            {
                app.UseSwagger(c => c.PreSerializeFilters.Add((swagger, httpReq) => {
                    var paths = swagger.Paths.ToDictionary(entry => entry.Key,
                        entry => entry.Value);
                    foreach(var path in paths)
                    {
                        swagger.Paths.Remove(path.Key);
                        swagger.Paths.Add($"/api{path.Key}", path.Value);
                    }
                }));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          
            app.UseStaticFiles();
        }
        
        private void AddMappings(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new CespViewMappingProfile());
                    mc.AddProfiles(MappingProfilesDto.GetProfiles());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddCorpPolocy(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                });
            });
        }

        private void AddControllers(IServiceCollection services)
        {
            services.AddControllers(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddNewtonsoftJson();
        }

        private void AddEmailSender(IServiceCollection services)
        {
            var emailSenderSettings = Configuration.GetSection("EmailSenderSettings");
            
            services.AddSingleton<IEmailSender>(
                new EmailSender(emailSenderSettings.GetValue<string>("EmailAdmin"),
                    emailSenderSettings.GetValue<string>("PasswordEmailAdmin"),
                    emailSenderSettings.GetValue<string>("EmailManager"))
            );
        }
    }
}