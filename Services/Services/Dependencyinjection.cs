

using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace Services
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddTransient<IUsuarioServices, UsuarioServices>();
            services.AddTransient<IProductoServices, ProductoServices>();
            services.AddTransient<ICategoriaServices, CategoriaServices>();
            services.AddTransient<IProveedorServices, ProveedorService>();
            services.AddTransient<IInventarioService, InventarioService>();
            services.AddTransient<IPagoServices, PagoService>();
            services.AddTransient<IPedidoService, PedidoService>();


            return services;
        }
    }
}
