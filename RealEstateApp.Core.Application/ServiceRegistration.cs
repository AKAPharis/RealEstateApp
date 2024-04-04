using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RealEstateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Mapping
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Servies

            #endregion
        }
    }
}
