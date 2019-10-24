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
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

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
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new CespViewMappingProfile());
                    mc.AddProfiles(MappingProfilesDto.GetProfiles());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.RegisterRepositories(Configuration);
            services.RegisterManagers(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyHeader();
                });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddOptions();
            services.Configure<Credentials>(Configuration.GetSection("Credentials"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "CESP.Service", Version = "v1"});
                c.OperationFilter<FormFileSwaggerFilter>();
                c.OperationFilter<PasswordHeaderSwaggerFilter>();
            });
            
            var emailSenderSettings = Configuration.GetSection("EmailSenderSettings");
            
            services.AddSingleton<IEmailSender>(
                    new EmailSender(emailSenderSettings.GetValue<string>("EmailAdmin"),
                        emailSenderSettings.GetValue<string>("PasswordEmailAdmin"),
                        emailSenderSettings.GetValue<string>("EmailManager"))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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