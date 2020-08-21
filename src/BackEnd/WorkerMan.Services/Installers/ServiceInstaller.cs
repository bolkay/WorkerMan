using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WorkerMan.Storage.Impl;

namespace WorkerMan.Services.Installers
{
    public static class ServiceInstaller
    {
        public static void InstallThirdPartyServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceInstaller).Assembly);

            services.AddSingleton<FirebaseStorageManager>();
        }

    }
}
