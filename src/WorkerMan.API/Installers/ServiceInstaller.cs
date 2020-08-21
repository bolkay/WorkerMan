using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkerMan.API.Configuration;
using WorkerMan.Business.Implementation;
using WorkerMan.Business.Interfaces;
using WorkerMan.CrossCutting.Contexts;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.Persistence.Implementation;
using WorkerMan.Persistence.Interfaces;
using WorkerMan.Persistence.Lookup;
using WorkerMan.Services.Configuration;
using WorkerMan.Services.Implementation;
using WorkerMan.Services.Interfaces;

namespace WorkerMan.API.Installers
{
    public static class ServiceInstaller
    {
        public static void InstallWorkerManRepositories(this IServiceCollection services)
        {
            services.AddSingleton(typeof(RepositoryMapper<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

        }

        public static void InstallWorkerManBusinesses(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseBusiness<>), typeof(BaseBusiness<>));
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<ICompanyBusiness, CompanyBusiness>();
        }
        public static void InstallWorkerManServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
        public static void InstallOptionsServices(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection identitySection = configuration.GetSection("WorkerManIdentityOptions");
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySection["Key"])),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = identitySection["Issuer"],
                ValidAudience = identitySection["Audience"],
                ValidateIssuerSigningKey=true,
                SaveSigninToken=true,
                
            };

            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                });

            services.AddSingleton(tokenValidationParameters);

            GlobalWorkerManOptions globalWorkerManOptions = new GlobalWorkerManOptions();
            configuration.Bind("GlobalWorkerManOptions", globalWorkerManOptions);

            services.AddSingleton(globalWorkerManOptions);

            WorkerManIdentityOptions workerManIdentityOptions = new WorkerManIdentityOptions();
            configuration.Bind("WorkerManIdentityOptions", workerManIdentityOptions);

            services.AddSingleton(workerManIdentityOptions);

        }
        public static void InstallFluentValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssembly(typeof(ServiceInstaller).Assembly);
            });
        }
        public static void InstallDatabaseEssentials(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkerManContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WorkerManConnection"), sqlServerOptions =>
                 {
                     sqlServerOptions.EnableRetryOnFailure();
                     sqlServerOptions.MigrationsAssembly(typeof(ServiceInstaller).Assembly.GetName().Name);
                 });
            });
            services.AddIdentity<WorkerManUser, WorkerManRole>(setup =>
            {
                setup.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 8,
                };

            }).AddEntityFrameworkStores<WorkerManContext>()
            .AddDefaultTokenProviders();
        }
    }
}
