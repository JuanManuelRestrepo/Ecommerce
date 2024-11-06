﻿

using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddTransient<IUsuarioServices, UsuarioServices>();
            services.AddTransient<IProductoServices, ProductoServices>();
         

            return services;
        }
    }
}
