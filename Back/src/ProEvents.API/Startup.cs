using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using ProEvents.Application;
using ProEvents.Application.Contracts;
using ProEvents.Domain.Identity;
using ProEvents.Persistence;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.Contracts;

namespace ProEvents.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration
        {
            get;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProEventsContext>(context => context
                .UseSqlite(Configuration.GetConnectionString("Default")));

            services.AddIdentityCore<User>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                })
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddEntityFrameworkStores<ProEventsContext>()
                .AddDefaultTokenProviders();

            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter()))
                .AddNewtonsoftJson(options => options.SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ILotService, LotService>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IAccountService,AccountService>();

            services.AddScoped<IGeneralPersist, GeneralPersist>();
            services.AddScoped<IEventPersist, EventPersist>();
            services.AddScoped<ILotPersist,LotPersist>();
            services.AddScoped<IUserPersist,UserPersist>();

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",new OpenApiInfo { Title = "ProEvents.API",Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","ProEvents.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}